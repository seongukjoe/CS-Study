# OIS 렌즈 채결기 TEACH

## 개요
> 이전 자료를 바탕으로, 각 부품의 티칭 방식을 정리한다. 이후 실제 티칭 과정을 하며 내용 추가 가능.

## 목차


### VCM Loading Unit 티칭
* VCM Picker Tool 교체
* VCM Tray 1EA 투입
* TEACH - VCM PICKER - HEAD XY - STAGE PICK POSITION XY 순서대로 진행
* Z축 경고 팝업 시 TEACH - VCM PICKER - SETTING - T READY POSITION Z에 현재 위치 입력  
 ***최종 완료 후 반드시 T READY POSITION Z 값은 0으로 설정***
 * STAGE FIRST PCIK POSITION X, STAGE PICK POSITION Y, Z, T 세팅
 * INDEX 위의 VCM CLAMP 교체 및 MASTER JIG로 세팅
 * SEMI AUTO - PICK
 * INDEX 위에 MASTER JIG 넣고 JOG 이동하여 위치 세팅 (INDEX PLACE POSITION, CLAMP PLACE POSITION)
 * INDEX PLACE POSITION T의 값을 READY POSITION T에도 입력
 * 최종 세팅 완료 후 SEMI AUTO - PICK - PLACE 순서대로 실행

### VCM Unloading Unit 티칭
* SIDE ANGLE(UNLOADER) UNIT TOOL 교체, SIDE ANGLE JIG 교체 및 세팅  
   - TEACH - INDEX - OPTION - SIDE ANGLE(UNLOADER)
   - VACUUM, BLOW, PICKUP, PICKER DOWN, FORWARD, BACKWARD 버튼 사용
* TEACH - UNLOAD PICKER - HEAD XY - INDEX PICK POSITION XY 순서대로 진행
* INDEX PICK POSITION XYZT 세팅
* INDEX PICK POSITION T 값을 READY POSITION T에도 입력
* SEMI AUTO - PICK 으로 PICK UP 동작 확인
* UNLOAD TRAY 1EA 투입
* SEMI AUTO - PICK - STAGE PLACE POSITION XY 순서대로 진행
* STAGE FIRST PLACE POSITION X, STAGE PLACE POSITION Y,Z,T 세팅  
    - 첫 번째 위치: 설비 정면에서 봤을 때 왼쪽 상단
    - STAGE PLACE POSITION Z 세팅 시 자재가 TRAY에 닿지 않고 약간 공중에서 떨어트리는 높이로 세팅
* SIDE ANGLE JIG에 자재 넣고 SEMI AUTO - PICK - STAGE PLACE (PICK & PLACE 세팅상태 확인)
* SIDE ANGLE JIG에 자재 넣고 SEMI AUTO - PICK - STAGE PLACE POSITION XY 순서대로 진행
* NG TRAY 첫 번째 위치 세팅  
    - 첫 번째 위치: 설비 정면에서 벌때 왼쪽 상단
    - NG TRAY PLACE POISTION XZ 세팅
    - STAGE FIRST PLACE POSTION 의 값을 NG TRAY PLACE POSITION T에도 입력
    - Y 방향 세팅은 TEACH - UNLOADER - STAGE - JOG 이동하여 세팅  
    - UNLOADER - SETTING - T NG TRAY FIRST PLACE POSITION Y에 현재 위치 Y값 입력
 * 최종 세팅 완료 후 SEMI AUTO로 작업 상태 확인  

### Lens Picker Unit 티칭
* VCM 자재만 놓고 VCM VISION 티칭
* LENS PICKER TOOL 제거
* TEACH - INDEX - OPTION - VCM VISION - READY POSITION 순서대로 진행
* TEACH - LENS PICKER - INDEX PLACE POSITION XY 순서대로 진행
* SETUP - MOTION - 11 LENS HEAD Z 더블 클릭 - SERVO OFF
* 샤프트에 LENS PICKER TOOL 조립
* LENS TRAY 1EA 투입
* 첫 번째 위치에 MASTER JIG 넣고 TEACH - LENS PICKER - STAGE PICK POSITION XY 순서대로 진행
* UPPER VISION - LIVE - CROSS LINE 활성화 후 MASTER JIG 센터 티칭  
    - STAGE PICK POSITION XY 세팅
    - STAGE PICK POSITION Z는 LENS PICKUP 후 세팅
* TEACH - LENS PICKER - STAGE PICK POSITION XY - PICKER 순서대로 진행
* SETUP - MOTION - 11 LENS HEAD Z 클릭 - SERVO OFF
* 샤프트 위, 아래 조절하면서 XY 위치 세팅
* 현재 위치 X 값 - STAGE PICK POSITION X = CAMERA DISTANCE OFFSET X 입력
* 현재 위치 Y 값 - STAGE PICK POSITION Y = CAMERA DISTANCE OFFSET Y 입력
* MASTER JIG를 LENS TRAY에서 제거
* 정상 체결 자재를 INDEX에 놓고 INDEX PLACE POSITION XY 버튼 누름
* HEAD Z JOG 버튼으로 Z축 DOWN 후 T 값 세팅
    - INDEX PLACE POSITION T 값을 모든 T값에 세팅
    - READY POSITION T, STAGE PICK POSITION T, BOTTOM CAM POSITION T
* HEAD Z JOG 버튼으로 Z축 값 변화 없을 떄까지 DOWN 시켜 변화 없는 위치값 -10um 세팅
* INDEX에서 역으로 LENS PICKUP 후 BOTTOM CAMERA POSITION XY 버튼 누름
* UNDER VISION 티칭
* TEACH - LENS PICKER - STAGE PICK POSITION XY - PICKER 버튼 누름
* HEAD Z축 JOG 버튼으로 Z축 값 변화 없을 때까지 DOWN 시켜 변화 없는 위치값 -10um 세팅
    - STAGE PICK POSITION Z 세팅
* VACUUM OFF 후 HEAD Z축 JOG 버튼으로 UP, DOWN, READY POSITION Z 버튼 누름
* STAGE PICK POSITION XY 버튼 누름
* UPPER VISION 티칭
* 최종 세팅 완료 후 SEMI AUTO로 작업 상태 확인
* LENS 없는 상태에서 VACUUM ON 했을 때 공압스위치 세팅(VACUUM ON 상태값에서 -2.0Kpa)  

### Bonder Unit 티칭
* 정상 채결 자재 INDEX에 세팅
* TEACH - BONDER - BONDER #1 - CAMERA CENTER XY POSITION 버튼 누름
    - VISION 프로그램 - POINT #1 - LIVE - CROSSLINE
    - BONDER #1 CAMERA CENTER POSITION XY 세팅
    - 초점 맞지 않는 경우 BONDER #1 CAMERA CENTER POSITION Z 세팅
* POINT #1 VISION 티칭
* 토출 높이 세팅
    - SHILECAN과 노즐이 거의 닿을 정도의 높이로 세팅
    - BONDER #1 JETTING POSITION Z, BONDER #1 SAMPLE POSITION Z에 동일하게 세팅
* READY POSITION XYZ 버튼 누름
* CALIBRATION
    - MOVE SAMPLE
    - TRIGGER
    - MOVE CAMERA
    - CAMERA Z POSITION
    - VISION - POINT #1 -  LIVE - CROSS LINE
    - JOG로 조정 후에 BONDER #1 CAMERA DISTANCE OFFSET X, Y 클릭하여 입력
* RECIPE - POINT, PATTERN_LINE, PATTENR_ARC 에서 RECIPE 설계
* 최종 세팅 완료 후 SEMI AUTO로 작업 상태 확인
    - BONDER #1 CHECK CAMERA
    - BONDER #1 JETTING
* BONDER #2 도 위와 동일한 순서로 진행

### 면각도 UNIT 티칭
* EPOXY 도포 및 UV 경화된 자재 1EA 세팅
* SIDE ANGLE JIG에 놓고 TEACH - INDEX - SIDE ANGLE MEASURE 버튼 누름
    - RECIPE - GENERAL - USE SIDE ANGLE 선택되어 있는 지 확인
    - 면각도 모니터에서 운전모드 상태인지 확인 (화면에는 설정모드라고 표시됨)
* 기준 화상 등록
* 면각도 파라미터 세팅








