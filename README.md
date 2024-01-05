# ConsoleDungeonRPG
## 구현 기능 목록

### 필수

1. 게임 시작 화면
1. 상태보기
1. 인벤토리
1. 상점

### 선택

1. 아이템 정보를 클래스 / 구조체로 활용해 보기
1. 아이템 정보를 배열로 관리하기
1. 아이템 추가하기 - 나만의 새로운 아이템을 추가해보기
1. 판매하기 기능추가 (난이도 - ★☆☆☆☆)
1. 장착 개선 (난이도 - ★★☆☆☆)
1. 던전입장 기능 추가 (난이도 - ★★★☆☆)
1. 휴식기능 추가 (난이도 - ★☆☆☆☆)
1. 레벨업 기능 추가 (난이도 - ★★☆☆☆)
1. 게임 저장하기 추가 (난이도 - ★★★★★★)

### 개별추가

1. 게임 진행도 초기화
1. 일부 텍스트에 색상을 입혀 가독성 향상
1. 아이템 타입(포션) 추가
1. 체력이 0 이하일 시, 던전 입장 불가능
1. 던전 입장 장면에서 캐릭터 스테이터스 일부 표시

## 장면 및 기능 구성
- 스파르타 마을
    - 상태 보기
    - 인벤토리
        - 장착 관리
    - 상점
        - 아이템 구매
        - 아이템 판매
    - 던전 입장
        - 쉬운 던전
        - 일반 던전
        - 어려운 던전
    - 휴식하기
        - 휴식하기
    - 저장하기
    - 불러오기
    - 초기화
    - 종료

## 소스코드 설명
정제가 덜 되어 설명이 지저분할 수 있음

### 아이템 클래스 정의
- Item 클래스에 아이템의 구성요소를 필드로 두어 관리

### 캐릭터 관련 기본적인 스테이터스들
- 플레이어가 여러명이 아니기 때문에, 클래스를 두지 않고 전역 필드로 사용

### 전체적인 게임아이템을 관리하는 List와, 인벤토리 List
- Item 클래스 형태를 멤버로 갖는 리스트 형태로 관리
- ※ 참고 : 초기화를 진행하지 않은 필드는 ["", 0, False] 등의 값을 가진다.

### Main()
- ShowMainMenu() 메서드를 호출하여 게임 시작

### ShowMainMenu()
- 시작 씬(스파르타 마을)
- 모든 장면은 기본적으로 while문을 사용하여 구현하였고, 잘못된 입력을 받을 시 현재 메뉴를 다시 보여줌.

### ShowCharacterStatus()
- 상태보기 씬
- while문 안에는 최소한의 코드를 두어 자원을 절약
- $문자열 적극 사용

### ShowInventory()
- 인벤토리 씬
- 필요 시, alertMessage에 문구를 저장하여 화면 갱신 시 보여줌. 
- inventory.Count만큼 반복하여 아이템의 정보를 표시.
- GetItemStats() 메서드를 통해 아이템의 모든 능력치를 문자열로 반환받는다.
- string equipped = inventory[i].IsEquipped ? "[E]" : "";
- inventory[i].IsEquipped 가 참일 경우, "[E]"를, 거짓일 경우 ""를 equipped 에 저장한다.

### ManageEquipment()
- 인벤토리 - 장착 관리 씬
- 장비 장착 목적의 입력값에 대해
- int형으로 Parse가 가능하면 true를 반환하고, else if 문 내에서 itemIndex 변수에 저장
- 또한 itemIndex가 인벤토리 크기 범위 내인지 확인하여 만족한다면 장비를 장착하거나 해제

### ShowShop(), PurchaseItem(), SellItem()
- 상점 및 아이템 구매/판매 씬
- 아이템목록 표시 시, IsPurchased 가 true일 경우, "구매완료" 문구를 함께 표시
- 아이템 구매를 성공할 경우, 해당 Item 객체는 inventory 리스트에 추가하여 관리할 수 있도록 함. 복사본이 아닌 객체 참조 형태.
- 아이템 판매를 성공할 경우, 85% 가격을 돌려받으며, IsEquipped을 false로, inventory.RemoveAt(index)를 통해 List에서 제거한다.

### EnterDungeon(), ProcessDungeon()
- 던전 입장 및 결과 씬
- 던전 관련 장면을 구현. 선택한 난이도의 던전 입장 시, 캐릭터 스테이터스와 확률에 따라 성공과 실패를 한다.
- 캐릭터 스테이터스를 요구하기 때문에, 구현 기능목록에는 없었지만 캐릭터 스테이터스를 기본적으로 보여주도록 함.
- 여러가지 계산 관련하여 코드가 길어졌다.
- 체력이 0 이하일 경우, 던전에 진입 불가. 이것도 요구사항에는 없었지만 기본적으로 이 정도 제한은 당연하다고 생각하여 구현.

### Rest()
- 휴식하기 씬
- 500골드를 지불하고 체력을 100으로 만든다

### string GetItemStats(Item item)
- item의 공격력, 방어력 중 0보다 높은 능력치를 집계하여 문자열로 반환

### string GetDifficultyString(DungeonDifficulty difficulty)
- 던전 Difficulty에 대한 한글로 된 문자열 반환

### string PadRightToLength(string str, int totalLength)
- 문자열의 우측을 공백으로 채움

### LevelUp()
- 던전 탐험 시 레벨업 로직을 작성
- 게임데이터를 로드 하였을 경우에도 올바르게 적용되어야하기 때문에, '레벨에 따라' 기본 스텟을 연산하는 로직을 작성하였다.

### SaveGame()
- 레벨, 이름, 직업, 기본공격력, 기본방어력, 현재체력, 골드, 경험치, 인벤토리목록+장착여부, 상점템구매여부
- savegame.txt로 저장하여 보안요소는 없다.

### LoadGame()
- 인벤토리 및 장착여부와 상점아이템 구매 여부를 반영하는 데에 코드가 길어졌다.
- inventory.Clear(); 로 쉽게 리스트 비우기가 가능
- 아이템들은 모두 객체참조를 하여 아이템 내 필드를 쉽게 관리할 수 있도록 하였다.

### ResetGame()
- 진행도를 초깃값으로 초기화
