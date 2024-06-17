using System;

[System.Serializable]
public class Character
{
    // 캐릭터 스텟
    public int id;
    public string name;
    public string gender;
    public int age;
    public int month;
    public int hp;
    public int mp;
    public int intelligence;
    public int stress;
    public int gold;
    public bool endingAchieved;
    public DateTime lastGrowUpTime;

    // 기본 생성자
    public Character() : this(0, "", "소녀", 10) { }

    // 파라미터를 받는 생성자
    public Character(int id, string name, string gender, int age)
    {
        this.id = id;
        this.name = name;
        this.gender = gender;
        this.age = age;
        this.month = 0;
        this.stress = 0;
        this.gold = 1000;
        this.endingAchieved = false;
        this.lastGrowUpTime = DateTime.Now;

        // 초기 스텟 계산
        this.hp = CalculateInitialStat(age);
        this.mp = CalculateInitialStat(age);
        this.intelligence = CalculateInitialStat(age);
    }

    // 초기 스탯 계산 메서드
    private int CalculateInitialStat(int age)
    {
        return 10 + (age - 10) * 5;
    }

    // 성장 시간 업데이트 메서드
    public void UpdateLastGrowUpTime()
    {
        // month가 증가할 때마다 lastGrowUpTime 업데이트
        this.lastGrowUpTime = DateTime.Now;
    }
}
