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














