# TextRPG_Sparta

전체적으로 MVC 패턴을 지키고 확장이 유리하게 제작할 수 있도록 노력했습니다.
Model : Enums.cs, Item.cs, Job.cs, Player.cs, PlayerStat.cs, DataManager - 각 핵심 데이터만 담을 수 있도록 했습니다.
View : CoreUI.cs - 씬마다 무조건 보여줘야 하는 씬 이름, 설명, 선택 메뉴와 액션을 보여주는 공통 UI를 구현했습니다.
Controller : 각 씬들(FirstScene.cs, JobSelectionScene.cs, Store.cs, Town.cs, ViewStatueScene.cs, Inventory.cs) - 부모 클래스로 BaseScene.cs을 가집니다.

다만 Controller의 씬들이 View의 역할도 일부 담당하고 있는 것 같습니다.
아예 View에 데이터만을 전달할 수 있도록 하면 프로젝트에 비해 너무 파일이 많아질 것 같아서 직접 나누지는 않고 이곳에 알립니다.

아이템은 DataManager에 배열로 구현했고, 직업은 DataManager에 리스트로 구현했습니다.

C#의 기본적인 문법등은 익숙하지만 대략 강의 3주차 이상에 나오는 상속, 추상화, 파라미터 등등은 자주 다뤄보지 않아서
이번 기회에 최대한 해당 기능들을 사용하여 제작해보기로 했습니다.

이번 개인 프로젝트에서는 특히 추상화, 가상화를 주로 사용하면서 해당 문법에 대해서 많이 익힐 수 있는 시간이었고
생성자를 통해 자식 클래스에서 초기값 설정을 통해 메뉴 옵션과 액션이 반드시 필요한 구현이라는 것을 어필하도록 했습니다.
대부분의 씬에서 옵션과 액션은 BaseScene.cs에서 가상화로 제안한 메서드를 따라가지만 Store.cs 씬에서는 독자적인 구조를 제작했습니다.
