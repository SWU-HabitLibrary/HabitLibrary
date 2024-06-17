using System;

[System.Serializable]
public class Character
{
    // ĳ���� ����
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

    // �⺻ ������
    public Character() : this(0, "", "�ҳ�", 10) { }

    // �Ķ���͸� �޴� ������
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

        // �ʱ� ���� ���
        this.hp = CalculateInitialStat(age);
        this.mp = CalculateInitialStat(age);
        this.intelligence = CalculateInitialStat(age);
    }

    // �ʱ� ���� ��� �޼���
    private int CalculateInitialStat(int age)
    {
        return 10 + (age - 10) * 5;
    }

    // ���� �ð� ������Ʈ �޼���
    public void UpdateLastGrowUpTime()
    {
        // month�� ������ ������ lastGrowUpTime ������Ʈ
        this.lastGrowUpTime = DateTime.Now;
    }
}
