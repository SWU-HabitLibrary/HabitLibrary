using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.TextCore.Text;

public class CharacterStateJSON : MonoBehaviour
{
    // 캐릭터 스텟을 JSON으로 저장하는 스크립트(**스크립트 분리 및 json manager 생성, 코드 수정 예정)
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
        Debug.Log("저장할 데이터 : " + data);
        File.WriteAllText(path + fileName + ".json", data);
        // 나중에 경로 변경하기
    }
}
