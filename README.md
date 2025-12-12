# 진짜 로켓으로 배달합니다! 코팡 익스프레스
PC 플랫폼의 싱글 플레이 2D 생존게임입니다.

This repository is a **fork of the original team project**, reorganized for portfolio purposes.

</br>

## 시연 영상
* 본인이 구현한 기능 위주로 편집함
* 인벤토리, 퀵슬롯, 분해시스템, 제작 시스템, 수리 시스템, 상자 시스템 및 아이템 랜덤 등장 시스템 등

https://github.com/user-attachments/assets/bb31a20d-8be6-4c7d-a0d2-a13bee7e7d16

</br>

📌 Project Overview

| 항목 | 내용 |
|------|------|
| **프로젝트 유형** | 팀 프로젝트 (10인, 기획 5인 클라 5인) |
| **장르** | 싱글 플레이, 2D 생존 게임 |
| **엔진** | Unity |
| **언어** | C# |
| **개발 방식** | Git Flow / Feature Branch |
| **진행 기간** | *2025.06.19 ~ 2025.07.08* |
| **팀 규모** | *10명* |
| **개인 역할** | Content & System Programming |

<br>

## 🧑‍💻 My Contributions

### 🎯 Gameplay & Core Systems
- **인벤토리 시스템 전면 구현**  
- **아이템 데이터 구조 설계 및 사용 로직 구현**  
- **아이템 분해/제작 시스템** 구현  
- **베이스 캠프 수리 컨텐츠** 개발  
- **아이템 등장 확률(가중치 기반) 시스템** 설계  

### 🗂 Data & Tools
- **데이터 테이블 기반 아이템 구조 설계**  
- ScriptableObject 자동 생성용 **데이터 유틸리티 도구 제작**  
- 아이템/제작/분해/소모품 데이터 직관적 관리 구조 확립  

### 🎨 UI & UX
- 인벤토리 및 제작/분해 UI **기획 + 디자인 + 구현**  
- 아이템 획득/이용 과정에서의 **직관적 UX 플로우 구축**  

<br>

## 🛠 Tech Stack

### ✔ Engine & Language
- Unity 2022 / C#

### ✔ Tools & Libraries
- Git / GitHub

### ✔ Workflows
- Feature Branch 기반 브랜치 전략  
- 코드 리뷰 및 팀 협업 경험  

<br>

## 📁 My Code Overview

하기 경로에 제가 직접 구현한 스크립트들이 정리되어 있습니다.

Assets/LHW/Scripts/

### 🔹 인벤토리 및 핫바 시스템
- InventoryManager.cs
- ItemSlotUnit.cs

📂 Path: `Assets/LHW/Scripts/Inventory/`

### 🔹 아이템, 확률 테이블, 레시피 등 SO
- ItemSO.cs
- BoxProbSO.cs
- BoxSetUpASO.cs
- CraftingRecipe.cs

📂 Path: `Assets/LHW/Scripts/Crafting/SOCreator/`


### 🔹 아이템 분해 시스템
- DecompositionSystem.cs

📂 Path: `Assets/LHW/Scripts/Decomposition/`

### 🔹 아이템 제작 시스템
- CraftingController.cs

📂 Path: `Assets/LHW/Scripts/Crafting/UI/`

### 🔹 ScriptableObject 생성 유틸리티
- TableItemToSO.cs
- TableBoxSetUpATableToSO.cs

📂 Path: `Assets/LHW/Scripts/Crafting/SOCreator/Editor/`

### 🔹 박스 시스템 및 아이템 랜덤 확률 등장 시스템
- BoxSystem.cs
- BoxItemRandomSystem.cs

📂 Path: `Assets/LHW/Scripts/Box/`

## ▶ How to Run
1. 저장소를 클론합니다.  
2. Unity **2022.3.61f1** 버전으로 프로젝트를 엽니다.  
3. `MainScene`을 실행하여 테스트합니다.
