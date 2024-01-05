using System;
using System.Collections.Generic;

namespace Chapter2
{
    internal class Program
    {

        enum ItemType
        {
            Armor,
            Weapon,
            Potion
        }
        enum DungeonDifficulty
        {
            Easy,
            Normal,
            Hard
        }

        // 아이템 클래스 정의
        class Item
        {
            public string Name { get; set; }
            public int Attack { get; set; }
            public int Defense { get; set; }
            public string Description { get; set; }
            public bool IsEquipped { get; set; }
            public int Price { get; set; }
            public bool IsPurchased { get; set; }
            public ItemType Type { get; set; }
            public bool IsAvailableInShop { get; set; }
        }

        static int level = 1;
        static string name = "말랑단단장";
        static string job = "전사";
        static float baseAttack = 10;
        static int baseDefense = 5;
        static int health = 100;
        static int gold = 2500;

        static int currentDungeonClears = 0; // 현재 레벨에서의 던전 클리어 횟수


        // 인벤토리 리스트
        static List<Item> inventory = new List<Item>();

        // 상점 아이템 목록
        static List<Item> gameItems = new List<Item>
        {
            new Item { Name = "수련자 갑옷", Defense = 5, Type = ItemType.Armor, Description = "수련에 도움을 주는 갑옷입니다.", Price = 1000, IsPurchased = false, IsAvailableInShop = true },
            new Item { Name = "무쇠갑옷", Defense = 9, Type = ItemType.Armor, Description = "무쇠로 만들어져 튼튼한 갑옷입니다.", Price = 2000, IsPurchased = false, IsAvailableInShop = true },
            new Item { Name = "스파르타의 갑옷", Defense = 15, Type = ItemType.Armor, Description = "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", Price = 3500, IsPurchased = false, IsAvailableInShop = true },

            new Item { Name = "낡은 검", Attack = 2, Type = ItemType.Weapon, Description = "쉽게 볼 수 있는 낡은 검 입니다.", Price = 600, IsPurchased = false, IsAvailableInShop = true },
            new Item { Name = "청동 도끼", Attack = 5, Type = ItemType.Weapon, Description = "어디선가 사용됐던거 같은 도끼입니다.", Price = 1500, IsPurchased = false, IsAvailableInShop = true },
            new Item { Name = "스파르타의 창", Attack = 7, Type = ItemType.Weapon, Description = "스파르타의 전사들이 사용했다는 전설의 창입니다.", Price = 2500, IsPurchased = false, IsAvailableInShop = true },

            new Item { Name = "힘의 비약", Attack = 1, Type = ItemType.Potion, Description = "마을 뒷편의 폭포수가 담기면 영험한 기운이 솟아납니다.", Price = 200, IsPurchased = false, IsAvailableInShop = true },
            new Item { Name = "방어의 비약", Defense = 1, Type = ItemType.Potion, Description = "마을 우물의 물이 담기면 수호신의 기운이 깃듭니다.", Price = 200, IsPurchased = false, IsAvailableInShop = true }
        };

        static void Main(string[] args)
        {
            ShowMainMenu();
        }

        static void ShowMainMenu()
        {
            bool isRunning = true;
            string alertMessage = "";

            while (isRunning)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("[스파르타 마을]");
                Console.ResetColor();
                Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
                Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
                Console.WriteLine("");
                Console.WriteLine("1. 상태 보기");
                Console.WriteLine("2. 인벤토리");
                Console.WriteLine("3. 상점");
                Console.WriteLine("4. 던전 입장");
                Console.WriteLine("5. 휴식하기");
                Console.WriteLine("");
                Console.WriteLine("6. 저장하기");
                Console.WriteLine("7. 불러오기");
                Console.WriteLine("8. 초기화");
                Console.WriteLine("");
                Console.WriteLine("0. 종료");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.Write(alertMessage);
                Console.Write("원하시는 행동을 입력해주세요.\n>> ");

                string input = Console.ReadLine();

                if (input == "1")
                {
                    ShowCharacterStatus();
                }
                else if (input == "2")
                {
                    ShowInventory();
                }
                else if (input == "3")
                {
                    ShowShop();
                }
                else if (input == "4")
                {
                    EnterDungeon();
                }
                else if (input == "5")
                {
                    Rest();
                }
                else if (input == "6")
                {
                    alertMessage = SaveGame();
                }
                else if (input == "7")
                {
                    alertMessage = LoadGame();
                }
                else if (input == "8")
                {
                    alertMessage = ResetGame();
                }
                else if (input == "0")
                {
                    isRunning = false; // 게임 종료
                    Console.WriteLine("");
                    Console.WriteLine(" ¯\\_(ツ)_/¯ 게임을 종료합니다 ¯\\_(ツ)_/¯");
                }
            }
        }

        static void ShowCharacterStatus()
        {
            float totalAttack = baseAttack;
            int totalDefense = baseDefense;

            foreach (var item in inventory)
            {
                if (item.IsEquipped)
                {
                    totalAttack += item.Attack;
                    totalDefense += item.Defense;
                }
            }

            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("[상태 보기]");
                Console.ResetColor();
                Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");
                Console.WriteLine($"Lv. {level.ToString("D2")}");
                Console.WriteLine($"{name} ( {job} )");
                Console.WriteLine($"공격력 : {totalAttack} (+{totalAttack - baseAttack})");
                Console.WriteLine($"방어력 : {totalDefense} (+{totalDefense - baseDefense})");
                Console.WriteLine($"체 력 : {health}");
                Console.WriteLine($"Gold : {gold} G");
                Console.WriteLine("");
                Console.WriteLine("0. 나가기");
                Console.WriteLine("");
                Console.Write("원하시는 행동을 입력해주세요.\n>> ");

                string input = Console.ReadLine();

                if (input == "0")
                {
                    break; // [스파르타 마을]로 돌아감
                }
            }
        }

        static void ShowInventory()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("[인벤토리]");
                Console.ResetColor();
                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
                Console.WriteLine("");
                Console.WriteLine("[아이템 목록]");

                if (inventory.Count == 0)
                {
                    Console.WriteLine("보유한 아이템이 없습니다.");
                }
                else
                {
                    for (int i = 0; i < inventory.Count; i++)
                    {
                        string stats = GetItemStats(inventory[i]);
                        string equipped = inventory[i].IsEquipped ? "[E]" : "";
                        Console.WriteLine($"({i + 1}) {equipped}{inventory[i].Name} | {stats} | {inventory[i].Description}");
                    }
                }

                Console.WriteLine("");
                Console.WriteLine("1. 장착 관리");
                Console.WriteLine("");
                Console.WriteLine("0. 나가기");
                Console.WriteLine("");
                Console.Write("원하시는 행동을 입력해주세요.\n>> ");

                string input = Console.ReadLine();

                if (input == "1")
                {
                    ManageEquipment();
                }
                else if (input == "0")
                {
                    break; // [스파르타 마을]로 돌아감
                }
            }
        }

        static void ManageEquipment()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("[인벤토리 - 장착 관리]");
                Console.ResetColor();
                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
                Console.WriteLine("");
                Console.WriteLine("[아이템 목록]");

                for (int i = 0; i < inventory.Count; i++)
                {
                    string stats = GetItemStats(inventory[i]);
                    string equipped = inventory[i].IsEquipped ? "[E]" : "";
                    Console.WriteLine($"{i + 1}. {equipped}{inventory[i].Name} | {stats} | {inventory[i].Description}");
                }

                Console.WriteLine("");
                Console.WriteLine("0. 나가기");
                Console.WriteLine("");
                Console.Write("원하시는 행동을 입력해주세요.\n>> ");

                string input = Console.ReadLine();

                if (input == "0")
                {
                    break; // [인벤토리]로 돌아감
                }
                else if (int.TryParse(input, out int itemIndex) && itemIndex > 0 && itemIndex <= inventory.Count)
                {
                    Item selectedItem = inventory[itemIndex - 1];
                    if (selectedItem.Type != ItemType.Potion)
                    {
                        // 같은 타입의 다른 아이템을 해제
                        foreach (var item in inventory)
                        {
                            if (item.Type == selectedItem.Type && item != selectedItem)
                            {
                                item.IsEquipped = false;
                            }
                        }
                    }
                    // 선택한 아이템 장착 상태 토글
                    selectedItem.IsEquipped = !selectedItem.IsEquipped;
                }
            }
        }

        static void ShowShop()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("[상점]");
                Console.ResetColor();
                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
                Console.WriteLine("");
                Console.WriteLine("[보유 골드]");
                Console.WriteLine($"{gold} G");
                Console.WriteLine("");

                Console.WriteLine("[아이템 목록]");
                foreach (var item in gameItems.Where(item => item.IsAvailableInShop))
                {
                    string stats = GetItemStats(item);
                    string purchaseStatus = item.IsPurchased ? "구매완료" : $"{item.Price} G";
                    Console.WriteLine($"- {item.Name} | {stats} | {item.Description} | {purchaseStatus}");
                }

                Console.WriteLine("");
                Console.WriteLine("1. 아이템 구매");
                Console.WriteLine("2. 아이템 판매");
                Console.WriteLine("");
                Console.WriteLine("0. 나가기");
                Console.WriteLine("");
                Console.Write("원하시는 행동을 입력해주세요.\n>> ");

                string input = Console.ReadLine();

                if (input == "1")
                {
                    PurchaseItem();
                }
                else if (input == "2")
                {
                    SellItem();
                }
                else if (input == "0")
                {
                    break; // [스파르타 마을]로 돌아감
                }
            }
        }


        static void PurchaseItem()
        {
            string alertMessage = "";

            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("[상점 - 아이템 구매]");
                Console.ResetColor();
                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
                Console.WriteLine("");
                Console.WriteLine("[보유 골드]");
                Console.WriteLine($"{gold} G");
                Console.WriteLine("");

                Console.WriteLine("[아이템 목록]");
                for (int i = 0; i < gameItems.Count; i++)
                {
                    string stats = GetItemStats(gameItems[i]);
                    string purchaseStatus = gameItems[i].IsPurchased ? "구매완료" : $"{gameItems[i].Price} G";
                    Console.WriteLine($"{i + 1}. {gameItems[i].Name} | {stats} | {gameItems[i].Description} | {purchaseStatus}");
                }

                Console.WriteLine("");
                Console.WriteLine("0. 나가기");
                Console.WriteLine("");

                Console.Write(alertMessage);
                Console.Write("원하시는 행동을 입력해주세요.\n>> ");

                string input = Console.ReadLine();

                if (int.TryParse(input, out int itemIndex) && itemIndex > 0 && itemIndex <= gameItems.Count)
                {
                    Item selectedItem = gameItems[itemIndex - 1];
                    if (selectedItem.IsPurchased)
                    {
                        alertMessage = "이미 구매한 아이템입니다.\n\n";
                    }
                    else if (gold >= selectedItem.Price)
                    {
                        selectedItem.IsPurchased = true;
                        inventory.Add(selectedItem);
                        gold -= selectedItem.Price;
                        alertMessage = "구매를 완료했습니다.\n\n";
                    }
                    else
                    {
                        alertMessage = "Gold가 부족합니다.\n\n";
                    }
                }
                else if (input == "0")
                {
                    break; // [상점]으로 돌아감
                }
            }
        }

        static void SellItem()
        {
            string alertMessage = "";
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("[상점 - 아이템 판매]");
                Console.ResetColor();
                Console.WriteLine("필요한 아이템을 판매할 수 있는 상점입니다.\n");

                Console.WriteLine("[보유 골드]");
                Console.WriteLine($"{gold} G\n");

                Console.WriteLine("[아이템 목록]");
                for (int i = 0; i < inventory.Count; i++)
                {
                    string stats = GetItemStats(inventory[i]);
                    int sellPrice = (int)(inventory[i].Price * 0.85);
                    Console.WriteLine($"{i + 1}. {inventory[i].Name} | {stats} | {inventory[i].Description} | {sellPrice} G");
                }

                Console.WriteLine("");
                Console.WriteLine("0. 나가기");
                Console.WriteLine("");
                Console.Write(alertMessage);
                Console.Write("원하시는 행동을 입력해주세요.\n>> ");

                string input = Console.ReadLine();

                if (int.TryParse(input, out int itemIndex) && itemIndex > 0 && itemIndex <= inventory.Count)
                {
                    Item selectedItem = inventory[itemIndex - 1];
                    int sellPrice = (int)(selectedItem.Price * 0.85);
                    gold += sellPrice;
                    selectedItem.IsEquipped = false; // 장착 해제

                    // 상점에서 구매 여부 업데이트
                    var shopItem = gameItems.FirstOrDefault(item => item.Name == selectedItem.Name);
                    if (shopItem != null)
                    {
                        shopItem.IsPurchased = false;
                    }

                    inventory.RemoveAt(itemIndex - 1); // 아이템 목록에서 제거
                    alertMessage = $"{selectedItem.Name}를 {sellPrice} G에 판매했습니다.\n\n";
                }
                else if (input == "0")
                {
                    break; // [상점]으로 돌아감
                }
            }
        }

        static void EnterDungeon()
        {
            float totalAttack = baseAttack;
            int totalDefense = baseDefense;

            foreach (var item in inventory)
            {
                if (item.IsEquipped)
                {
                    totalAttack += item.Attack;
                    totalDefense += item.Defense;
                }
            }

            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("[던전 입장]");
                Console.ResetColor();
                Console.WriteLine("");

                string healthStr = $"│ 체력　 : {health}";
                string attackStr = $"│ 공격력 : {totalAttack}";
                string defenseStr = $"│ 방어력 : {totalDefense}";
                string goldStr = $"│ 골드　 : {gold}";
                Console.WriteLine("┌──────────────────────┐");
                Console.WriteLine(PadRightToLength(healthStr, 20) + "│");
                Console.WriteLine(PadRightToLength(attackStr, 20) + "│");
                Console.WriteLine(PadRightToLength(defenseStr, 20) + "│");
                Console.WriteLine(PadRightToLength(goldStr, 20) + "│");
                Console.WriteLine("└──────────────────────┘");

                if (health <= 0) Console.WriteLine("\n※ 던전 입장을 위해 체력이 1 이상 필요합니다");
                Console.WriteLine("");
                Console.WriteLine("1. 쉬운 던전   | 방어력 5 이상 권장");
                Console.WriteLine("2. 일반 던전   | 방어력 11 이상 권장");
                Console.WriteLine("3. 어려운 던전 | 방어력 17 이상 권장");
                Console.WriteLine("");
                Console.WriteLine("0. 나가기");
                Console.WriteLine("");
                

                Console.Write("원하시는 행동을 입력해주세요.\n>> ");
                string input = Console.ReadLine();

                if (input == "1" && health>0)
                {
                    ProcessDungeon(DungeonDifficulty.Easy, 5, 1000);
                }
                else if (input == "2" && health > 0)
                {
                    ProcessDungeon(DungeonDifficulty.Normal, 11, 1700);
                }
                else if (input == "3" && health > 0)
                {
                    ProcessDungeon(DungeonDifficulty.Hard, 17, 2500);
                }
                else if (input == "0")
                {
                    break; // [스파르타 마을]로 돌아감
                }
            }
        }

        static void ProcessDungeon(DungeonDifficulty difficulty, int recommendedDefense, int baseReward)
        {
            float totalAttack = baseAttack;
            int totalDefense = baseDefense;

            foreach (var item in inventory)
            {
                if (item.IsEquipped)
                {
                    totalAttack += item.Attack;
                    totalDefense += item.Defense;
                }
            }

            bool isSuccess = true; // 던전 성공 여부
            int healthLoss = new Random().Next(20, 36); // 기본 체력 감소량
            int reward = baseReward; // 기본 보상

            // 방어력이 권장 수치보다 낮은 경우 실패 확률 적용
            if (totalDefense < recommendedDefense)
            {
                isSuccess = new Random().NextDouble() >= 0.4;
                healthLoss += recommendedDefense - totalDefense; // 체력 감소량 증가
            }
            else
            {
                healthLoss -= totalDefense - recommendedDefense; // 체력 감소량 감소
            }

            // 체력 감소 적용
            health -= Math.Max(healthLoss, 0);

            // 보상 계산
            if (isSuccess)
            {
                int attackBonus = new Random().Next((int)totalAttack, (int)(totalAttack * 2 + 1));
                reward += (int)(baseReward * (attackBonus / 100.0));
            }

            // 결과 출력
            while (true)
            {
                Console.Clear();
                if (isSuccess)
                {

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("[던전 클리어]");
                    Console.ResetColor();
                    Console.WriteLine($"축하합니다!!\n{GetDifficultyString(difficulty)} 던전을 클리어 하였습니다.");
                    Console.WriteLine($"\n[탐험 결과]\n체력 {health + healthLoss} -> {health}\nGold {gold} G -> {gold + reward} G");
                    gold += reward;

                    // 레벨업 로직
                    currentDungeonClears++;
                    if (currentDungeonClears >= level)
                    {
                        LevelUp();
                        Console.WriteLine($"\n축하합니다! 레벨이 {level}로 상승했습니다.");
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("[던전 실패]");
                    Console.ResetColor();
                    Console.WriteLine($"아쉽게도 {GetDifficultyString(difficulty)} 던전을 클리어하지 못했습니다.");
                    Console.WriteLine($"\n[탐험 결과]\n체력 {health + healthLoss / 2} -> {health}");
                }
                Console.WriteLine("");
                Console.WriteLine("0. 나가기");
                Console.WriteLine("");
                Console.Write("원하시는 행동을 입력해주세요.\n>> ");

                string input = Console.ReadLine();

                if (input == "0")
                {
                    break; // [던전 입장]으로 돌아가기
                }
            }
        }

        static void Rest()
        {
            string alertMessage = "";
            while(true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("[휴식하기]");
                Console.ResetColor();
                Console.WriteLine($"500 G를 내면 체력을 회복할 수 있습니다. (보유 골드 : {gold} G)");
                Console.WriteLine("");
                Console.WriteLine("1. 휴식하기");
                Console.WriteLine("");
                Console.WriteLine("0. 나가기");
                Console.WriteLine("");
                Console.Write(alertMessage);
                Console.Write("원하시는 행동을 입력해주세요.\n>> ");

                string input = Console.ReadLine();

                if (input == "1")
                {
                    if (health == 100)
                    {
                        alertMessage = "체력이 이미 최대치입니다.\n\n";
                    }
                    else if (gold < 500)
                    {
                        alertMessage = "보유 골드가 부족합니다.\n\n";
                    }
                    else
                    {
                        gold -= 500;
                        health = 100;
                        alertMessage = "휴식을 완료했습니다. 체력이 100이 되었습니다.\n\n";
                    }
                }
                else if (input == "0")
                {
                    break; // [스파르타 마을]로 돌아가기
                }

            }
        }

        // 아이템 능력치 반환
        static string GetItemStats(Item item)
        {
            List<string> stats = new List<string>();
            if (item.Attack > 0)
            {
                stats.Add($"공격력 +{item.Attack}");
            }
            if (item.Defense > 0)
            {
                stats.Add($"방어력 +{item.Defense}");
            }
            return string.Join(" ", stats);
        }

        // 던전 Difficulty에 대한 문자열 반환
        static string GetDifficultyString(DungeonDifficulty difficulty)
        {
            switch (difficulty)
            {
                case DungeonDifficulty.Easy:
                    return "쉬운";
                case DungeonDifficulty.Normal:
                    return "일반";
                case DungeonDifficulty.Hard:
                    return "어려운";
                default:
                    return "알 수 없는";
            }
        }

        // 문자열의 우측을 공백으로 채움
        static string PadRightToLength(string str, int totalLength)
        {
            return str.PadRight(totalLength);
        }

        static void LevelUp()
        {
            level++;
            baseAttack = 10 + (level - 1) * 0.5f; // 레벨에 따른 기본 공격력 재계산
            baseDefense = 5 + (level - 1); // 레벨에 따른 기본 방어력 재계산
            currentDungeonClears = 0; // 클리어 횟수 초기화
        }

        static string SaveGame()
        {
            using (StreamWriter file = new StreamWriter("savegame.txt"))
            {
                file.WriteLine(level);
                file.WriteLine(name);
                file.WriteLine(job);
                file.WriteLine(baseAttack);
                file.WriteLine(baseDefense);
                file.WriteLine(health);
                file.WriteLine(gold);
                file.WriteLine(currentDungeonClears);

                // 인벤토리 저장 (아이템 이름과 장착 여부)
                foreach (var item in inventory)
                {
                    file.WriteLine($"{item.Name},{item.IsEquipped}");
                }
                // 상점템 구매 여부 저장
                foreach (var item in gameItems)
                {
                    if (item.IsPurchased)
                    {
                        file.WriteLine($"ShopItem,{item.Name}");
                    }
                }
            }
            return "진행도를 저장하였습니다.\n\n";
        }

        static string LoadGame()
        {
            if (File.Exists("savegame.txt"))
            {
                using (StreamReader file = new StreamReader("savegame.txt"))
                {
                    level = int.Parse(file.ReadLine());
                    name = file.ReadLine();
                    job = file.ReadLine();
                    baseAttack = float.Parse(file.ReadLine());
                    baseDefense = int.Parse(file.ReadLine());
                    health = int.Parse(file.ReadLine());
                    gold = int.Parse(file.ReadLine());
                    currentDungeonClears = int.Parse(file.ReadLine());

                    // 상점 아이템 구매 여부 초기화
                    foreach (var item in gameItems)
                    {
                        item.IsPurchased = false;
                    }

                    // 인벤토리 로드
                    inventory.Clear();
                    string line;
                    while ((line = file.ReadLine()) != null)
                    {
                        string[] parts = line.Split(',');
                        if (parts[0] == "ShopItem")
                        {
                            // 상점 아이템 구매 여부 로드
                            var itemToPurchase = gameItems.FirstOrDefault(item => item.Name == parts[1]);
                            if (itemToPurchase != null)
                            {
                                itemToPurchase.IsPurchased = true;
                            }
                        }
                        else
                        {
                            // 인벤토리 아이템 로드
                            var itemToAdd = gameItems.FirstOrDefault(item => item.Name == parts[0]);
                            if (itemToAdd != null)
                            {
                                inventory.Add(itemToAdd);
                                itemToAdd.IsEquipped = bool.Parse(parts[1]);
                            }
                        }
                    }
                }
                return "진행도가 로드되었습니다.\n\n";
            }
            else
            {
                return "저장된 데이터가 없습니다.\n\n";
            }
        }

        static string ResetGame()
        {
            level = 1;
            name = "말랑단단장"; 
            job = "전사"; 
            baseAttack = 10;
            baseDefense = 5;
            health = 100;
            gold = 2500;
            currentDungeonClears = 0;

            inventory.Clear(); // 인벤토리 초기화
            gameItems.ForEach(item => item.IsPurchased = false); // 상점 아이템 초기화

            return "진행도가 초기화되었습니다.\n\n";
        }
    }
}
