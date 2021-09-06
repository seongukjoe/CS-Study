## Class And Structure
### Class에서의 Override 및 New 키워드 사용
> C# 언어는 서로 다른 라이브러리의 기본 및 파생 클래스 간 버전 관리를 개발하고 이전 버전과의 호환성을 유지할 수 있도록 설계된다. 
> 파생 클래스의 멤버와 동일한 이름의 기본 클래스에서 새 멤버가 추가되면 C# 언어는 이를 지원하고 예기치 않은 동작이 발생되지 않는다. 따라서, 메서드가 상속된 메서드를 재정의할지 아니면 메서드가 유사한 이름의 상속된 메서드를 숨기는 새 메서드인지를 명시적으로 지정해야한다. 
### Override 및 new 키워드 사용 예
> C#에서는 new 및 override 키워드를 사용하여 메서드가 상호 작용하는 방식을 지정할 수 있다. 
> override 한정자는 기본 클래스 virtual 메서드를 확장하고, new 한정자는 기본 클래스 메서드를 숨긴다. 
```cs
class BaseClass
{
    public void Method1()
    {
        Cosonle.WriteLine("Base = Method1");
    }
    
    // Added
    public void Method2()
    {
        Console.WriteLine("Base = Method2");
    }
    
    // To Avoid Warning
    public new void Method2() {....}
  
}
class DerivedClass : BaseClass
{
    public void Method2()
    {
        Console.WriteLine("Derived - Method2");
    }
}

class Program
{
    static void Main(string[] args)
    {
        BaseClass bc = new BaseClass();
        DerivedClass dc = new DerivedClass();
        BaseClass bcdc = new DerivedClass();
             
        bc.Method1();
        dc.Method1();
        dc.Method2();
        bcdc.Method1();
    }
}

// --------------------- Console Output
// Base - Method1
// Base - Method1
// Derived - Method2
// Base - Method1
// -----------------------------------

```
> DerivedClass는 BaseClass에서 상속된다. 
> 위의 코드에서 bc와 bcdc는 BaseClass 형식이기 때문에 캐스팅을 사용하지 않는 경우 Method1에 직접 액세스만 가능.
> dc 변수는 Method1 및 Method2 둘다에 액세스할 수 있다. 
> bcdc가 method2에 액세스하기 위해서는 BaseClass에 Method2를 추가하는 경우이다. 
> 하지만, BaseClass에 새로 Method2를 추가하는 경우에 경고가 발생한다. 이를 해결하기 위해 새로이 추가하는 Method2 정의에 new 키워드를 사용하는 것이 좋다. new 키워드는 코드 내의 관계를 유지하지만 경고는 표현하지 않는다. 

### ToString 메서드 재정의
> 사용자 지정 클래스 또는 구조체를 활용하여 ToString 메서드를 재정의해본다. 메서드를 재정의하는 방법은 다음과 같다.
> 1. 다음 한정자 및 반환 형식으로 ToString 메서드를 선언한다.
```cs
public override string ToString(){}
```
> 2. 문자열을 반환하도록 메서드를 구현
```cs
class Person
{
    public string Name {get; set;}
    public int Age {get; set;}
    
    public override string ToString()
    {
        return "Person: " + Name + " " + Age;
    }
}
```
> 다음과 같이 실행하여 메서드 테스트 가능
```cs
Person person =- new Person(Name = "Joe", Age = 30);
Console.WriteLine(person);

// --------- Console Output
// Person: Joe 30
// ------------------------
```






