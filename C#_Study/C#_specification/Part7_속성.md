## 속성  
> 속성은 전용 필드의 값을 읽거나 쓰거나 계산하는 유연한 메커니즘을 제공하는 멤버로, 공용 데이터 멤버인 것처럼 속성을 사용할 수 있지만, 실제로 접근자라는 특수 메서드이다.  
> 속성의 활용을 통해 데이터에 쉽게 액세스할 수 있으며, 메서드의 안정성과 유연성 수준을 올리는 데에도 도움이 된다. 

### 속성 개요  
* **get** 속성 접근자는 속성 값을 반환하는 데 사용되고 **set** 속성 접근자는 새 값을 할당하는 데 사용된다.  
* C#9 이상의 버전에서는 개체 생성 중에만 새 값을 할당하는 데 init 속성 접근자가 사용된다. 
* **value** 키워드는 set 또는 init 접근자가 할당하는 값을 정의하는 데 사용된다. 
* 속성은 읽기/쓰기(get, set O), 읽기 전용(get O, set X), 쓰기 전용(get X, set O)

### 지원 필드가 있는 속성
> 속성 구현에서의 한 가지 기본 패턴은 private 지원 필드를 사용하여 속성 값을 설정 및 검색하는 작업이 포함된다. get 접근자는 private 필드의 값을 반환하고 set 접근자는 private 필드에 값을 할당하기 전에 데이터 유효성 검사를 수행할 수 있습니다. 
> 다음 예제를 통해 get과 set의 사용 방식, 지원 필드 내에서의 속성의 활용, 데이터 유효성 검사를 확인할 수 있습니다. 
```cs
using System;

namespace practice
{
    class TimePeriod
    {
        private double _second;
        private double _hour{
            get{return _second / 3600;}
            set{
                if(value < 0 || value > 25){
                    throw new ArgumentOutOfRangeException($"{nameof(value)} bw 0 and 24.");

                    _second = value * 3600;
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            TimePeriod t = new TimePeriod();
            t._hour = 24;
            Console.WriteLine($"Time in hours: {t._hour}");
        }
    }
}
```

### 식 본문 정의
> 속성 접근자는 식의 결과를 할당하거나 반환하기만 하는 한 줄로 된 구문으로 구성되는 경우가 많습니다.  
> 이러한 속성은 식 본문 멤버로 구현할 수 있습니다. 식 본문 정의는 **=>** 기호와 속성에 할당하거나 속성에서 검색할 식으로 구성됩니다.  
E.g.)
```cs
using System;

public class Person
{
        private string _firstName;
        private string _lastName;
        
        public Person(string first, string last)
        {
                _firstName = first; 
                _lastName = last;
        }
        public string Name => $"{_firstName} {_lastName}";
}

public class Exmaple
{
        public static void Main()
        {
                var person = new Person("jam", "strraw");
                Console.WriteLine(person.name);
        }
}
```

### 자동으로 구현된 속성
> 경우에 따라 **get** 속성과 **set** 접근자에서 지원 필드에 값을 할당하거나 지원 필드에서 값을 검색하기만 하고 추가 논ㄴ리를 포함하지 않을 수 있습니다.  
> 자동 구현 속성을사용하면 코드를 간소화할 수 있을 뿐 아니라 C# 컴파일러에서 지원 필드를 투명하게 제공하도록 할 수 있습니다.  
> 속성에 **get** 및 **set** 접근자가 모두 포함된 경우 두 접근자를 모두 자동 구현해야 합니다. 




