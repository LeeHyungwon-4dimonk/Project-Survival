# Project Survival
경일아카데미 사전합반 1조

## 코드 컨벤션

1. 싱글톤 인스턴스명 : Instance
2. 클래스 지역변수명 앞에 언더바+카멜케이스
3. 함수 지역변수명 카멜케이스
4. 함수명 파스칼케이스
5. 모든 변수는 private으로 선언
6. public이 필요한 경우 프로퍼티 사용
7. 프로퍼티명은 파스칼케이스

예시
```cs
// 인스턴스명 Instance
    public static GameManager Instance { get; private set; } 
    
// private 선언 및 언더바+카멜케이스
	private int _winScore;

// public은 프로퍼티로 사용 및 프로퍼티명 파스칼케이스
    public int WinScore { get { return _winScore; } set { _winScore = value; } }

// 함수명 파스칼케이스, 지역변수명 카멜케이스
	public void SaveData(GameData gameData){
		_gameData = gameData;
	}

```


## 깃허브 커밋 컨벤션

1. 헤더와 제목은 '영어'로
2. 내용은 한글로 알아볼 수 있게
3. 헤더와 내용의 첫 글자는 대문자로


```

[Header] Subject

내용

```

| 헤더               | 내용                                          |
| ---------------- | ------------------------------------------- |
| Feat             | 새로운 기능을 추가할 경우                              |
| Fix              | 버그를 고친 경우                                   |
| Design           | CSS 등 사용자 UI 디자인 변경                         |
| !BREAKING CHANGE | 커다란 API 변경의 경우                              |
| !HOTFIX          | 급하게 치명적인 버그를 고쳐야하는 경우                       |
| Style            | 코드 포맷 변경, 세미 콜론 누락, 코드 수정이 없는 경우            |
| Refactor         | 프로덕션 코드 리팩토링                                |
| Comment          | 필요한 주석 추가 및 변경                              |
| Docs             | 문서를 수정한 경우                                  |
| Test             | 테스트 추가, 테스트 리팩토링(프로덕션 코드 변경 X)              |
| Chore            | 빌드 테스트 업데이트, 패키지 매니저를 설정하는 경우(프로덕션 코드 변경 X) |
| Rename           | 파일 혹은 폴더명을 수정하거나 옮기는 작업만인 경우                |
| Remove           | 파일을 삭제하는 작업만 수행한 경우                         |
