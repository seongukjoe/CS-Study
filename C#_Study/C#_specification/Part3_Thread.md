# Thread

## Thread Class
> C#에서 쓰레드를 만드는 기본적인 클래스로 System.Threading.Thread라는 클래스가 있다. 이 클래스의 생성자에 실행하고자 하는 메서드를 대리자로 지정한 후 Thread 클래스 객체에서 Start() 메서드를 호출하면 새로운 쓰레드가 생성되어 실행되게 된다. 
> 쓰레드는 프로세스가 시작되면 공용 언어 런타임 애플리케이션 코드를 실행하는 단일 포그라운드에서 자동으로 생성된다. 프로세스는 주 포그라운드 스레드와 함께 프로세스와 관련 된 프로그램 코드의 일부를 실행하는 스레드를 하나 이상 만들 수 있습니다. 

## Thread 시작
> 쓰레드가 클래스 생성자에서 실행할 메서드를 나타네는 대리자를 제공하여 쓰레드를 시작한다. 그런 다음 메서드를 호출하여 실행을 시작합니다.
```cs
// 메서드에 인수가 없으면 ThreadStart 대리자를 생성자에 전달한다. 
public delegate void ThreadStart();
```

```cs
using System;
using System.Diagnostics;
using System.Threading;

public class Example
{
    public static void Main()
    {
        var th = new Thread(ExecuteInForeground);
        th.Start();
        Thread.Sleep(1000);
        Console.WriteLine("Main Thread ({0}) exiting...", Thread.CurrentThread.ManagedThreadId);
    }
    
    private static void ExecuteInForeground()
    {
        var sw = Stopwatch.StartNew();
        Console.WriteLine("Thread {0} : {1}, Priority {2}".
                          Thread.CurrentThread.ManagedThreadId,
                          Thread.CurrentThread.ThreadState,
                          Thread.CurrentThread.Priority);
       do{
          Console.WriteLine("Thread {0}: Elapsed {1:N2} seconds",
                            Thread.CurrentThread.ManagedThreadId,
                            sw.ElapsedMilliseconds / 1000.0);
          Thread.Sleep(500);

       }while(sw.ElapsedMilliseconds <= 5000);
       sw.Stop();
    

    }

}

// --------------------------------------- Console Output
//  Thread 3 : Running, Priority Normal
//  Thread 3: Elapsed 0.00 seconds
//  Thread 3: Elapsed 0.51 seconds
//  Main Thread (1) exiting...
//  Thread 3: Elapsed 1.02 seconds
//  Thread 3: Elapsed 1.53 seconds
//  Thread 3: Elapsed 2.04 seconds
//  Thread 3: Elapsed 2.55 seconds
//  Thread 3: Elapsed 3.05 seconds
//  Thread 3: Elapsed 3.57 seconds
//  Thread 3: Elapsed 4.08 seconds
//  Thread 3: Elapsed 4.58 seconds
// ------------------------------------------------------

```
## Thread 예제
> Thread 클래스의 생성자가 받아들이는 파라미터는 ThreadStart 대리자와 ParameterizedThreadStart 대리자가 있는데, 이 섹션은 파라미터를 직접 전달하지 않는 메서들에 사용하는 ThreadStart 델리게이트 사용 예제를 쓴다. ThreadStart 대리자는 public delegate void ThreadStart()와 같이 정의가 되어 리턴과 파라미터 모두 void임을 알 수 있듯이, 리턴과 파라미터가 없는 메서드는 대리자의 객체로 생성될 수 있다. 
```cs
class Program
{
    static void Main(string[] args)
    {
        // Run 메서드를 입력받아 ThreadStart 대리자 타입 생성 후 Thread 클래스에 전달
        Thread t1 = new Thread(new ThreadStart(Run));
        t1.Start();
        
        // 컴파일러가 Run() 메서드의 함수 프로토타입으로부터 ThreadStart 대리자 객체를 추론하여 생성
        Thread t2 = enw Thread(Run);
        t2.Start();
        
        // 익명 메서드를 사용하여 생성
        Thread t3 = new Thread(delegate(){Run();});
        t3.Start();
        
        // 람다식 활용
        Thread t4 = new Thread(() => Run());
        t4.Start();
        
        // 간략한 표현
        new Thread()) => Run()).Start();
        }
        static void Run(){....}
   }
}
```
> **다른 클래스 메서드** \
> 동일 클래스가 아닌 다른 클래스의 메서드를 쓰레드에 호출하기 위해서는 해당 클래스의 객체를 생성한 후, 그 객체의 메서드를 대리자로 쓰레드에 전달하면 된다.
```cs
class Diff
{
    public void Run(){.....}
}

class Program
{
    static void Main(string[] args)
    {
        // Diff 클래스의 Run 호출
        Diff obj = new Diff();
        Thread t = new Thread(obj.Run);
        t.start();
    }``````````````````````````````````````
}

```


















