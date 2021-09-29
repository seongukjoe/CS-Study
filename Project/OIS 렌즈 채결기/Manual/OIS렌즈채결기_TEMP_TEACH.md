## 모델 교체시 세팅 과정 및 티칭


### JigPocket 교체 및 세팅
* VCM Vision 위치로 Index 10 JigPocket 이동
* 이전 포켓 분리 및 조립
* 새로운 Pocket 교체 후에 빨간색 기준 네모 또는 파란색 크로스라인 설정
* Teach - Index - Step Move으로 Index 회전
* lensheight 측정 유닛 모델 툴로 교체
* 자재 정상 체결 후 Lense Height Measure 클릭


### VCM Loading Unit 티칭
* VCM Picker tool 교체 + VCM Tray 1ea 투입
* Teach - VCM Picker = Heady XY - Stage Pick Position XY 버튼 누름
* Head XY, Head Z 화살표 사용하여 첫 번째 Pick 위치 세팅
  ***첫번째 위치 - 설비 정면에서 봤을 때 왼쪽 상단***
 * Z축 경고 팝업 시, TEACH - VCM PICKER - SETTING - T READY POSITION Z에 현재 위치 입력
 ***최조 완료 후에 반드시 T Ready Position Z 값은 0으로 세팅***
 * Stage Furst Pick Position X, Stage Pick Position Y, Z , T 세팅
 * Index 위의 VCMP 클램프 교체 및 Master Jig로 세팅
 * SEMI auto - pick
 * Inde 위에 Master Jig 넣고 jog 이동을 통해 위치 세팅
 * 최종 세팅 완료 후, SEMI Auto - Pick - Place로 세팅 상태 확인


### Lens Picker Teaching
* Lens Picker Tool이 없는 상태에서 Index 10에 Master Jig 넣고 X, Y 위치 티칭
* Lens Picker Tool 조립
* Master Jig 사용하여 Lens Camera Center X, Y 티칭
* Master Jig 사용하여 실제 Lens Pickup하는 X, Y Position 메모장 기록
* Camera Distance X, Y 계산 및 적용
* VCM Vision Teaching
* Index 10 Locking 하지 않은 VCM + Lens 넣은 상태로 Lens Picker Tool Theta 및 Z축 높이 티칭
***Z축 높이 티칭 시 주의사항: Jog로 내렸을 때 Encoder 값이 변하지 않는 높이에서 +20um세팅***
* 앞선 과정에서 세팅한 Theta값 모든 Point 적용(Index Place T, Stage Pick T, Bottom T, Ready T)
* Bottom Vision Teaching - 초점 흐리면 Bottom Z 높이 변경(변경 시 Ready Z도 동일한 값 적용)
* Upeer Vision Teaching
* 자재 없는 상테에서 Vacuum On 했을 때, 공압스위치 세팅(Vacuum On 상테에서 -2.0kbps)
* 



















