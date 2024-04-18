using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.TextCore.Text;

public class CharacterStateJSON : MonoBehaviour
{
    // ĳ���� ������ JSON���� �����ϴ� ��ũ��Ʈ(**��ũ��Ʈ �и� �� json manager ����, �ڵ� ���� ����)
    string fileName = "character";
    string path = Application.dataPath + "/Saves/";

   public Character LoadToJson()
   {
        Character loadCharacter = new Character();

        string jsonData = File.ReadAllText(path + fileName + ".json");
        loadCharacter = JsonUtility.FromJson<Character>(jsonData);

        return loadCharacter;
   }

   public void SaveToJson(Character newCharacter)
    {
        string data = JsonUtility.ToJson(newCharacter);
        Debug.Log("������ ������ : " + data);
        File.WriteAllText(path + fileName + ".json", data);
        // ���߿� ��� �����ϱ�
    }
}
