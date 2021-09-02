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




















