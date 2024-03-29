# Winform Part 1  
### 사용자 입력 - 키보드, 마우스  

#### 개요
> Windows From에서 사용자 입력은 Windows 메시지 형식으로 애플리케이션에 전송됩니다.  
> 일련의 재정의 가능한 메서드는 해당 메시지를 처리하며, 이벤트를 발생시켜 키보드 입력 정보를 가져옵니다.   

#### 키보드 이벤트 
> 모든 Windows Forms 컨트롤은 마우스 및 키보드 입력과 관련된 이벤트 세트를 상속합니다.  
> 예를 들어, KeyPress 이벤트를 처리하여 누른 키의 문자 코드를 확인할 수 있습니다.  

#### 키보드 이벤트의 순서
> 한 컨트롤에서 발생할 수 있는 키보드 관련 이벤트는 세 가지이다. 다음 시퀀스는 키보드 이벤트의 일반적인 순서를 보여 줍니다.  
 1. 사용자가 "a" 키를 누르면 해당 키가 전처리되고, 디스패치된 다음, **KeyDown** 이벤트가 발생한다.
 2. 사용자가 "a" 키를 누르고 있으면 해당 키가 전처리되고, 디스패치된 다음, **KeyPress** 이벤트가 발생하며, 키를 누르고 있을 때 여러 차례 발생한다.
 3. 사용자가 "a" 키를 놓으면 해당 키가 전처리되고, 디스패치된 다음, **KeyUp** 이벤트가 발생한다.  

#### 키보드 키 이벤트 수정 방법
> Windows Froms는 키보드 입력을 사용 및 수정할 수 있는 기능을 제공합니다. 키 사용은 **메시지 큐 아래의 다른 메서드 및 이벤트가 키 값을 수신하지 않도록** 메서드 또는 이벤트 처리기 내에서 키를 처리하는 것을 가리킵니다. 

#### 키 사용
> KeyPress 이벤트 처리기에서 KeyPressEventArgs 클래스의 Handled 속성을 true로 설정합니다.
  
E.g.)
```cs
private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
{
    if(e.KeyChar == 'a' || e.KeyChar == 'A') e.Handled = true;
}
```

#### 보조 키 누름 확인 방법
> 사용자가 애플리케이션에 키를 입력할 때, SHIFT, ALT, CTRL 키와 같은 보조키를 모니터링할 수 있습니다.  
> KeyDown 이벤트를 처리하는 경우 이벤트 처리기가 수신한 KeyEventArgs.Modifiers 속성은 눌리는 보조 키를 지정하며, KeyEventArgs.KeyData 속성은 비트 OR로 결합된 모든 보조키와 함께 눌리는 문자를 지정합니다.  
> SHIFT의 키값은 다음과 같이 표시된다.  

```cs
Keys.Shift
Keys.ShiftKey
Keys.RShiftKey
Keys.:ShiftKey
```
> 보조 키로 이를 테스트하기 위한 올바른 값은 Keys.Shift로 CTRL과 ALT 역시 Keys.Control 및 Keys.Alt 값을 사용해야 합니다. 

#### 마우스
#### 마우스 이벤트
> 모든 Windows Form 컨트롤은 마우스 및 키보드 입력과 관련된 이벤트 세트를 상속합니다. 예를 들어 컨트롤은 MouseClick 이벤트를 처리하여 마우스 클릭 위치를 확인할 수 있습니다.  

#### 마우스 입력 설정 변경
> 컨트롤에서 파생하고 GetStyle 및 SetStyle 메서드를 사용하여 컨트롤이 마우스 입력을 처리하는 방법을 검색하고 변경할 수 있습니다. SetStyle 메서드는 ContorlStyles 값의 비트 조합을 사용하여 컨트롤이 표준 클릭, 두번 클릭 동작을 포함하는 지 또는 컨트롤이 자체 마우스 처리를 수행하는지를 결정합니다.  
  
  
#### 마우스 이벤트 사용  


| 마우스 이벤트 | Description |
|-------------|------------|
| Click | 마우스 단추를 놓을 때, 일반적으로 MouseUp 이벤트 전에 발생|
| MouseClick | 사용자가 마우스로 컨트롤을 클릭할 때 발생 |
| DoubleClick | 컨트롤을 두 번 클릭할 때 발생 |
| MouseDoubleClick | 사용자가 마우스로 컨트롤을 두 번 클릭할 때 발생 |
| MouseDown | 마우스 포인터가 컨트롤 위에 있고 사용자가 마우스 단추를 누를 때 발생 |
| MouseEnter | 컨트롤 형식에 따라 마우스 포인터가 컨트롤의 테두리 또는 클라이언트 영역에 들어 갈 때 발생 |
| MouseHover | 마우스 포인터가 중지하고 컨트롤 위에 있을 때 발생 |
| MouseLeave | 마우스 포인터가 컨트롤의 테두리 또는 클라이언트 영역을 벗어날 때 발생 |
| MouseMove | 마우스 포인터가 컨트롤 위에 있는 동안 이동할 때 발생 |
| MouseUp | 마우스 포인터가 컨트롤 위에 있고 사용자가 마우스 단추를 놓을 때 발생 |
| MouseWheel | 컨트롤에 포커스가 있는 동안 사용자가 마우스 휠을 회전할 때 발생 | 

#### 화면 좌표와 클라이언트 좌표 간의 변환
> 일부 마우스 위치 정보는 클라이언트 좌표로 표시되고 일부 정보는 화면 좌표로 표시되므로 좌표계 간에 지점을 변환해야할 수 도 있습니다. Contorl 클래스에서 사용할 수 있는 PointToClient 및 PointToScreen 메서드를 사용하면 이를 쉽게 수행할 수 있습니다.  

#### 표준 클릭 이벤트 동작
> 마우스 클릭 이벤트의 발생 순서는 다음과 같습니다.  
> **마우스 단추 한번 클릭에 대한 이벤트 순서**
1. MouseDown 이벤트
2. Click 이벤트
3. MouseClick 이벤트
4. MouseUp 이벤트  
> **마우스 단추 두번 클릭에 대해 발생하는 이벤트 순서**
1. MouseDown 이벤트
2. Click 이벤트
3. MouseClick 이벤트
4. MouseUp 이벤트
5. MouseDown 이벤트
6. DoubleClick 이벤트 (해당 컨트롤의 StandardDoubleClick 스타일 비트가 true로 설정되었는지에 따라 달라질 수 있음.)  
7. MouseDoubleClick 이벤트
8. MouseUp 이벤트  

#### 끌어서 놓기 이벤트

| 마우스 이벤트 | Description |
| --------- | ----------- |
| DragEnter | 개체를 컨트롤의 범위로 끌어올 때 발생 |
| DragOver | 마우스 포인터가 컨트롤의 범위 내에 있는 동안 개체를 끌 때 발생 |
| DragDrop | 끌어서 놓기 작업이 완료될 때 발생 |
| DragLeave | 컨트롤의 범위 밖으로 개체를 끌 때 발생 |








