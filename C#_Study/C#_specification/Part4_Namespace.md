## Namespace
> .Net Framework는 무수하게 많은 클래스를 가지며, 이렇게 많은 클래스를 충돌없이 편리하게 사용과 관리하기 위해 .NET에서는 네임스페이스를 사>용한다. C#에서도 이러한 개념을 적용하여 클래스들이 대게 네임스페이스 안에서 정의된다. 



## 네임스페이스 사용
> 네임스페이스는 C# 프로그램 내에서 두가지 방법으로 많이 사용된다. 
> 1. .NET 클래스는 네임스페이스를 사용하여 많은 클래스를 구성한다.
> 2. 고유한 네임스페이스를 선언하면 대규모 프로그래밍 프로젝트에서 클래스 및 메서드 이름의 범위를 제어할 수 있다. 

## 네임스페이스 참초
> 네임스페이스를 사용하기 위한 두가지 방식이 있다. 첫째는 클래스명 앞에 네임스페이스 전부를 적는 경우와 둘째는 프로그램 맨 윗단에 해당 using을 사용하여 C# 파일에서 사용하고자 하는 네임스페이스를 한번 설정해주고 이후 해당 파일 내에서 네임스페이스 없이 직접 클래스를 사용하는 경우


### 네임스페이스 별칭
> using 지시문을 사용하여 네임스페이스에 대한 별칭을 만들 수도 있다. 네임스페이스 별칭 한정자 :: 를 사용하여 별칭이 지정된 네임스페이스의 구성원에 액세스 가능
```cs
using generics = System.Collections.Generic;

namespace AliasExample
{
  class TestClass
  {
    static void Main()
    {
        generics::Dictionary<string, int> dict = new generics::Dictionary<string, int> ()
        {
            ["A"] = 1;
            ["B"] = 2;
            ["C"] = 3;
        };
        foreach (var name in dict.Keys)
        {
            System.Console.WriteLine($"{name} {dict[name]}");
        }
        // Output:
        // A 1
        // B 2
        // C 3
    }
  }
}
```
### 네임스페이스를 사용한 범위 제어
> namespace 키워드를 범위를 선언하는데 사용할 수 있다. 프로젝트 내에서 범위를 만드는 기능은 코드 구성에 도움이 되며, 전역적으로 고유한 형식을 만들 수 있게 해줍니다. 
> 밑의 예제 코드에는 SampleClass라는 클래스는 서로 중첩된 두 개의 네임스페이스에 정의되어 있으며, 토큰은 호출되는 메서드를 구분하는 데 사용된다. 
```cs
namespace SampleNamespace
{
    class SampleClass
    {
        public void SampleMethod()
        {
            System.Console.WriteLine("Sample Method Inside SampleNamespace");
        }
    }
    namespace NestedNamespace
    {
        class SampleClasee
        {
            public void SampleMethod()
            {
                System.Console.WriteLine("SampleMethod Inside NestedNamespace");
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            SampleClass outer = new SampleClass();
            outer.SampleMethod();

            SampleNamespace.SampleClass outer2 = new SampleNamespace.SampleClass();
            outer2.SampleMethod();

            NestedNamespace.SampleClasee inner = new NestedNamespace.SampleClasee();
            inner.SampleMethod();
        }
    }
}
```

















