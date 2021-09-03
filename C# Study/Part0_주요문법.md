## C# 배열
> 배열은 동일한 데이터 다입 요소들로 구성된 데이터 집합이다.
```cs
// 1 차원 배열
int[] arr = new int[10]; 
int[] arr = {1, 2, 3, 4, 5, 6, 7};

// 2 차원 배열
string[,] = {{"ab","cd"},{"ef","gh"}};

// 3 차원 배열
int[,,] Cubes;
```
> 위의 예제는 크기가 정해진 배열이다. C#에서는 가변 배열을 활용할 수 있는 데, 이는 첫번째 차원의 크기는 컴파일 타임에 확정되어야 하고, 그 이상 차원은 런타임시 동적으로 서로 다른 크기의 배열로 지정할 수 있는 것이다. 
```cs
// Jagged Array (가변 배열)
// 1차 배열 크기 3은 명시해야한다.
int[][] A = new int[3][];
// 각 1차 배열 요소당 서로 다른 크기의 배열 할당 가능
A[0] = new int[2];
A[1] = new int[3] {1, 2, 3};
A[2] = new int[4];

```
> 배열을 매개변수로 사용하여 함수로 전달하는 경우 보내는 쪽에서는 배열명을 사용하고, 받는 쪽에서는 동일한 배열타입의 배열을 받아들인다.
```cs
static void Main(string[] args)
{
    int[] arr = {1, 2, 3, 4, 5};
    run(arr);    // 배열명으로 전달
}
static int run(int[] arr)   // 배열타입으로 받음
{
    int sum = 0;
    foreach(var i in arr) sum += i;
    Console.WriteLine(sum);
    return;
}
```
## C# String
> C#의 문자열(string)은 문자(character)의 집합체이다. 문자열 안에 있는 각 문자를 액세스하고 싶으면, [index#]을 사용하여 문자 요소를 액세스한다. 
> E.g.) string s = "abced" -> s[0] = 'a'; s[3] = 'e';
> 문자배열(char array)을 문자열(string)으로 변환하기 위해서는 new string(문자배열)을 사용한다.
> E.g)
```cs
string s = "C# studies";

char[] charArray = str.ToCharArray();
char[] charArray2 = {'a','b','c','d'};
s = new string(charArray2);
```
> C#에서의 string은 Immutable, 즉 한번 문자열이 설정되면, 다시 변경할 수 없으므로 문자열 갱신이 많은 프로그램에는 적당하지 않다. 반면 mutable 타입인 StringBuilder 클래스는 문자열 갱신이 많은 곳에서 자주 사용되는데 이는 이 클래스가 별도 메모리를 생성, 소멸하지 않고 일정한 버퍼를 갖고 문자열 갱신을 효율적으로 처리할 수 있기 떄문이다. 

## C# Method
> 클래스내에서 일련의 코드 블럭을 실행시키는 함수르 메서드라 부른다. 
### Pass by Value
> C#은 메서드에 인수를 전달할 떄, 디폴트로 값을 복사해서 전달하는 Pass by Value 방식을 따른다. 만약 전달된 인수를 메서드 내에서 변경한다해도 메서드가 끝나고 함수가 리턴된 후, 전달되었던 인수의 값은 호출자에서 원래 값 그대로 유지된다.
> E.g.)
```cs
class Program
{
    private void run(int a)
    {
        a += 1;
    }
    static void Main(string[] args)
    {
        Program p = new Program();
        int val = 100;
        p.run(val);
        // val 값은 그대로 100이다. 
    }
}
```
### Pass by Reference
> 메서드에 파라미터를 전달할 때, 만약 레퍼런스로 전달하고자 하면 ref를 사용한다. 이 경우, 메서드 내에서 변경된 값은 리턴 후에도 유효하다. 
> 이와 비슷한 기능으로는 out이 있는데, out을 사용하는 파라미터는 메서드 내에서 그 값을 반드시 지정하여 전달해야한다.
> 둘의 차이점은 ref는 변수가 사전에 초기화되어야 하지만, out은 초기화할 필요는 없다. 
```cs
static int RefTest(ref int a, ref int b)
{
  a++; b++;
  return a*b;
}

static int OutTest(out int a, out int b)
{
    a = 100;
    b = 200;
    return a + b;
}

static void Main(string[] args)
{
    int x = 1;
    int y = 4;
    int result = RefTest(ref x, ref y); // 레퍼런스 매개변수 설정
    
    int a, b;
    int OutResult = OutTest(out a, out b);
    // OutReslut = 300, a = 100, b = 200
}
```

## C# Indexer
> 인덱서는 클래스나 구조체의 인스턴스르 배열처럼 인덱싱할 수 있다. 입력 파라미터인 인덱스도 여러 데이터 타입으로 사용이 가능하고 주로 int 나 string 타입을 사용하여 인덱스값을 주는 것이 일반적이다. 
```cs
class MyClass
{
    private const int Max = 10;
    private string name;
    
    // 내부의 정수 데이터
    privte intp[ data = new int[NAX]
    
    // 인덱서 정의, int 파라미터 사용
    public int this[int index]
    {
        get
        {
            if (indxe < 0 || index >= MAX)
            {
                throw new IndexOutOfRangeException();
            }
            else
            {
                // 정수배열로부터 값 리턴
                return data[index];
            }
        
        }
        set
        {
            if(!(index<0 || index>=MAX))
            {
                data[index] = value;
            }
        }
    }
}

class Program
{
    statuc void Main(string[] args)
    {
        MyClass cls = new MyClass();
        cls[1] = 1000;  // set 사용
        int i = cls[1]l // get 사용
    
    }
}
```
### 인덱서 개요
> * 인덱서를 사용하면 배열과 유사한 방식으로 개체를 인덱싱할 수 있다.
> * get 접근자는 값을 반환하고 set 접근자는 값을 할당한다.
> * this 키워드는 인덱서를 정의하는 데 사용
> * value 키워드는 set 접근자가 할당하는 값을 정의
> 

## C# static 메서드
> 정적(static) 메서드는 인스턴스 메서드와는 달리 클래스로부터 객체를 생성하지 않고 직접 [클래스명, 메서드명] 형식으로 호출하는 메서드이다. 
> static 메서드는 인스턴스 객체로부터 호출될 수 없으며, 반드시 클래스명과 함께 사용된다. 

```cs
public class MyClass
{
    private int val = 1;
    // Instance Method
    public int InstRun()
    {
        return val;
    }
    
    // Static Method
    public static int Run()
    {
        return 1;
    }
}

public class Client
{
    public void Test()
    {
        // Call Instance Method
        MyClass myClass = new MyClass();
        int i = myClass.InstRun();
        
        // Call Static Method
        int j = MyClass.Run();
    }
}
```
### Static 속성, 필드
> Static 필드의 경우 Non-Static 필드들은 클래스 인스턴스를 생성할 때마다 메모리에 매번 새로 생성되게 되는 반면, static 필드는 프로그램 실행 후 해당 클래스가 처음으로 사용될 때 한번 초기화되어 계속 동일한 메모리를 사용하게 된다. 












