using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.UI;
using System.IO;
using System;
using ItemInformation;
using CharecterInformation;
using Managers;
class people
{
    public string name;
    public int age;

    public people(string name, int age)
    {
        this.name = name;
        this.age = age;
    }
}

public interface ILoader<Key,Value>
{
    Dictionary<Key,Value> MakeList();
}

[Serializable]
public class Player
{
    public int PlayerID;
    public PlayerInfo playerInfo;
    public Item[] items;
    public CharacterStat[] characters;
    public Player(int PlayerID,PlayerInfo playerInfo,Item[] items,CharacterStat[] characters)
    {
        this.PlayerID = PlayerID;
        this.playerInfo = playerInfo;
        this.items = items;
        this.characters = characters;
    }
}
[Serializable]
public class PlayerInfo
{
    public int Gold;
    public int Gem;

    public PlayerInfo(int Gold,int Gem)
    {
        this.Gold = Gold;
        this.Gem = Gem;
    }
}

[Serializable]
public class PlayerData : ILoader<int, Player>
{
    //Json에서 불러올 파일형싱을 전부 마춰주어야 한다.
    public List<Player> Player = new List<Player>();
    //public Dictionary<int, Player> MakeDict()
    //{
    //    Dictionary<int, Player> dict = new Dictionary<int, Player>();
    //    foreach (Player player in Player)
    //    {
    //        dict.Add(player.PlayerID, player);
    //        Debug.Log(player.PlayerID);
    //        Debug.Log(player.playerInfo.Gold);
    //        Debug.Log(player.playerInfo.Gem);
    //    }
    //    Debug.Log(dict);
    //    return dict;
    //}

    public Dictionary<int, Player> MakeList()
    {
        Dictionary<int, Player> dict = new Dictionary<int, Player>();
        foreach (Player player in Player)
        {
            dict.Add(player.PlayerID, player);
            Debug.Log(player.PlayerID);
            Debug.Log(player.playerInfo.Gold);
            Debug.Log(player.playerInfo.Gem);
        }
        Debug.Log(dict);
        return dict;
    }
}

public class SLManager : MonoBehaviour
{
    public Text tx;
    //List<people> data = new List<people>();

    List<Player> data = new List<Player>();
    private void Start()
    {
        /*
        data.Add(new people("test1", 10));
        data.Add(new people("test2", 20));
        data.Add(new people("test3", 30));
        //*/
        Init();
    }

    public void Save()
    {
        string jdata = JsonConvert.SerializeObject(data);
        //byte[] bytes = System.Text.Encoding.UTF8.GetBytes(jdata);
       // string format = System.Convert.ToBase64String(bytes);

        File.WriteAllText(Application.dataPath + "/NKStudio.json", jdata);
    }

    public void Load()
    {
        string jdata = File.ReadAllText(Application.dataPath + "/NKStudio.json");

        //byte[] bytes = System.Convert.FromBase64String(jdata);
        //string reformat = System.Text.Encoding.UTF8.GetString(bytes);

        tx.text = jdata;
        data = JsonConvert.DeserializeObject<List<Player>>(jdata);
        print(data[0].PlayerID);
    }
    public Dictionary<int, Player> PlayerDataDic { get; private set; } = new Dictionary<int, Player>();
    Item[] Items;
    public void Init()
    {
        DataManager.Instance.Load(TableType.ItemInformation);
        DataManager.Instance.Load(TableType.CharacterInformation);

        //Item[] items = { new Item() };
        CharacterStat[] characters = { new CharacterStat() };
        //data.Add(new Player(1, new PlayerInfo(1000, 100), items, characters));

        Save();
    }

    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = Resources.Load<TextAsset>($"Data/Player/{path}");
        print(textAsset.text);
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }


}
