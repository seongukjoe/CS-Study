## OIS 렌즈채결기 메뉴얼
### 개요
> 본 문서에서는 OIS 렌즈채결기의 개괄적인 기구 구성, 장비의 동작, SETUP 방식 그리고 실제 동작에 있어서 유용한 부분을 다룰 것이다. 

### OIS 렌즈체결기 구성
![OIS_Lens_1](./img/OIS_Lens_1.jpg)

> OIS 렌즈채결기는 VCM에 렌즈를 조립, 본딩, UV 경화, 면각도 측정하는 장비로, Index Table과 총 31축의 제어 축이 사용되는 설비이다. 
> 장비의 대략적인 구동은 다음과 같이 이루어진다.
* VCM, Lens, Unload Magazine 트레이 투입
* 각 Unit 별 작업 시작 (E.g. Loading, Unloading, Check, Bonding, Curing)
* 모든 Unit의 작업 종료 후에 Index 회전
* 위의 과정 반복 시행, 자재 소진시 트레이 교환

### Main, Vision UI 주요 내용
> GUI의 모든 버튼의 내용을 설명하는 것이 아닌 주요하도 여겨지는 부분을 골라 내용 정리
> 
**Main UI - Auto - Interface**
> 컨트롤러의 연결 상태를 표시함.
> MC: Main 전원 연결 상태 표시
> 
> Main Air: Main 공압 연결 상태 표시
> 
> Dispenser: 디스펜서 컨트롤러 연결 상태 표시
> 
> Vision: 카메라 연결 상태 표시
> 
> Actuator: K-Star 프로그램 연결 상태 표시
> 
> UV Lamp: UV 컨트롤러 연결 상태 표시
> 
> Dsensor: LensHeight 측정 센서 연결 상태 표시
> 
> 위의 상태들이 초록불이 들어온 것을 확인 후에 장비를 동작시켜야 한다. 
> 

**Main UI - Auto - 동작 제어 버튼**
> 장비 Auto 동작 제어
> Initial: 모터 원점 동작 버튼
> Start: Auto 작업 시작 버튼
> Stop: Auto 작업 정지 버튼
> Lot End: 현재 Index에 올려진 자재만을 작업하는 경우 사용. VCM은 더 이상 투입하지 않고 Index에 들어가 있는 자재들만 작업한다.
> Auto 동작 중에 LOT END 버튼을 누르면 VCM Picker Unit은 멈추고 나머지 Unit은 작업을 합니다. 
> 
> Task Log: Log를 확인할 수 있는 창 팝업 버튼

**Main UI - Auto - Index Layout**
> 총 새산량, Ok Count, NG Count, Reset 버튼으로 초기화
> Index Zone의 작업 상태 표시, 황색: 작업중, 녹색: 작업완료, 적색: NG
> 
> 중앙의 녹색 번호는 VCM Load 부분의 현재 Index 번호
> 
**Main UI - Recipe(General) - LensHeight Option**
> LensHeight 측정 결과로 OK/NG 판정 선택 가능
> 1. Lens Height: Lens 바닥에서 Lens Height 측정 센서의 Tool이 Lens에 닿는 위치까지의 실측 높이
> 
> 2. Allow Min/Max: 측정 판정 최소/최대 값 설정 파라미터
> 

**Main UI - Recipe(General) - Sequence Option**
> USE JIG FLATNESSL: 수동으로 Jig 평탄도 측정 시 선택 ***Auto 모드로 실행 시 반드시 미선택***
> 
> USE VISION: Epoxy 유/무 확인 Vision 기능 사용 시 선택 ***Epoxy Bond를 사용하지 않는다면 반드시 미선택***
> 

**Main UI - Recipe(General) - Dispenser Option**
> USE IDLE #1, #2: Bonder Unit을 장시간 사용하지 않을 때 사용 ***AUTO 동작 실행 시 사용금지***
> 
**Main UI - TEACH**
> 이 후의 내용에서 심도있게 다룰 것
> 간단히 설명하면, 각 장비들의 위치와 조건을 잡아주는 작업으로 VCM Loader, Lens Picker와 같은 장비들의 Tray 개수, Delay Time, Ready Position, Stage Position, Offset과 같은 값을 설정한다. 
> 각각의 장비들은 화살표를 클릭하여 제어하며, Jog 모드는 연속적인 움직임으로 Speed Control을 통해 속도를 제어한다. Relative 모드는 클릭마다 움직이며 움직일 거리를 입력하여 제어한다. 







