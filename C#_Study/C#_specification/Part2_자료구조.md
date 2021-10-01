# 자료구조

* 배열
> 연속적인 메모리상에 동일한 타입의 요소를 일렬로 저장하는 자료구조로 인덱스를 사용하여 직접 액세스가능하다. 
> 배열의 크기는 고정되어 있다. 
```cs
int[] arr = new int[100];
```

* 동적 배열
> 배열은 고정된 크기와 연속된 배열요소의 집합으로 초기화를 하는 경우 미리 배열 요소의 수를 정해야하지만, 경우에 따라 몇 개나 필요한 지 미리 알수 없는 경우가 있을 수 있다. 이러한 경우 동적 배열이 쓰이며 지원하는 클래스로는 ArrayList와 List<T>가 있다. 
```cs
  // ArratList
  // ArrayList는 배열 요소를 읽어 사용할 때, object를 리턴하므로 일반적으로 원하는 타입으로 먼저 캐스팅한 후 사용
ArrayList arrlist = new ArraList();
arrlist.Add(10);
arrlist.Add(20);
arrlist.Add(30);
  // int로 캐스팅
int a = (int) arrlist[1];
```

```cs
  // ArrayList와 달리 캐스팅 할 필요없이 바로 값은 반환할 수 있다. 
List<int> myList = new List<int>();
myList.Add(10);
myList.Add(20);
myList.Add(30);
int val = myList[1];
```

```cs
  // Sorted List는 Key 값으로 value를 찾는 Map ADT 타입을 내부적으로 배열을 이용해 구현한 클래스이다. 
  SortedList<int, string> list = new SortedList<int, string>();
  list.Add(1001, "Tim");
  list.Add(1002, "Tin");
  list.Add(1003, "Tio");
  
  foreach(KeyValuePair<int, string> kv in list)
  {
      Console.WriteList("{0} : {1}", kv.Key, kv.Value);
  }
  
// ------------------- Console Output
// 1001 : Tim
// 1002 : Tin
// 1003 : Tio
// ------------------------------------
```
```cs
  // ConcurrentBag 클래스
  // 멀티쓰레딩 환경에서 리스트를 보다 간편하게 사용할 수 있는 새로운 클래스
  // 리스트와 비슷하게 객체들의 컬렉션을 저장하는데, 리스트와는 달리 입력 순서를 보장하지는 않는다. 
  // 데이터 저장은 add로 읽기는 foreach문 혹은 trypeek(), trytake() 메서드를 사용함
  
using System;
using System.Collections;
using System.Collections.Concurrent; // ConcurrentBag
using System.Threading;
using System.Threading.Tasks;

namespace ConcurrentApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var bag = new ConcurrentBag<int>();

            // 데이타를 Bag에 넣는 쓰레드
            Task t1 = Task.Factory.StartNew(() =>
            {
                for (int i = 0; i < 100; i++)
                {
                    bag.Add(i);
                    Thread.Sleep(100);
                }
            });

            // Bag에서 데이타를 읽는 쓰레드
            Task t2 = Task.Factory.StartNew(() =>
            {
                int n = 1;               
                // Bag 데이타 내용을 10번 출력함
                while (n <= 10)
                {                    
                    Console.WriteLine("{0} iteration", n);
                    int count = 0;

                    // Bag에서 데이타 읽기
                    foreach (int i in bag)
                    {
                        Console.WriteLine(i);
                        count++;
                    }
                    Console.WriteLine("Count={0}", count);

                    Thread.Sleep(1000);
                    n++;
                }
            });

            // 두 쓰레드가 끝날 때까지 대기
            Task.WaitAll(t1, t2);
        }
    }
}
```

* 링크드리스트
> 링크드 리스트는 데이터를 포함하는 노드들을 연결하여 컬렉션을 만든 자료 구조로서 각 노드는 데이터와 다음/이전 링크 포인터를 갖게 된다. 단일 연결 리스트는 노드를 다음 링크로만 연결한 리스트이고 이중 연결 리스트는 각 노드를 다음 링크와 이전 일ㅇ크 모두 연결한 리스트이다. 

```cs
  LinkedList<string> list = new LinkedList<string>();
  list.AddLast("A");
  list.AddLast("B");
  list.AddLast("C");
  
  LinkedListNode<string> node = list.Find("B");
  LinkedListNode<string> newNode = new LinkedListNode<string>("C");
  
  list.AddAfter(node, newNode);

```

* Queue
  > FIFO 자료구조 형식
```cs
  Queue<int> q = new Queue<int>();
  q.Enqueue(10);
  q.Enqueue(20);
  q.Enqueue(30);
  
  int next = q.Dequeue();
  next = q.Dequeue();
  
```










