using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(InitializeSavedGame))]
public class SaveManager : MonoBehaviour {

    public static SaveManager Instance { set; get; }

    string fileName;

    [HideInInspector]
    public SaveState[] saveState;

    [HideInInspector]
    public InitializeSavedGame initializeGame;

    void Start() {
        DontDestroyOnLoad(gameObject);
        Instance = this;
        initializeGame = GetComponent<InitializeSavedGame>();

        saveState = new SaveState[3];

        for(int i = 0; i < saveState.Length; i++) {
            saveState[i] = new SaveState();
        }

        fileName = "LightsUpSave" + SaveSlot.saveSlot.ToString() + ".txt";
    }

    public void save() {
        fileName = "LightsUpSave" + SaveSlot.saveSlot.ToString() + ".txt";
        SaveGenerator.Serialize(saveState[SaveSlot.saveSlot], fileName);
    }

    public void load() {
        fileName = "LightsUpSave" + SaveSlot.saveSlot.ToString() + ".txt";
        if (File.Exists(fileName)) {
            StreamReader reader = new StreamReader(fileName);
            XmlSerializer xml = new XmlSerializer(typeof(SaveState));
            saveState[SaveSlot.saveSlot] = (SaveState)xml.Deserialize(reader);
            SceneLoader.sceneName = saveState[SaveSlot.saveSlot].playerLocation;
            reader.Close();
        } else {
            saveState[SaveSlot.saveSlot] = new SaveState();
            saveState[SaveSlot.saveSlot].playerPosition = GameObject.Find("PlayerStartingPoint").transform.position;
        }

        save();
    }

    public void ResetSave() {
        File.Delete(fileName[SaveSlot.saveSlot].ToString());
    }

    public void SavePlayer(Vector3 latestPosition, string playerName, string playerLocation, float health, float mana, bool hasWeapon) {
        saveState[SaveSlot.saveSlot].playerPosition = latestPosition;
        saveState[SaveSlot.saveSlot].playerName = playerName;
        saveState[SaveSlot.saveSlot].playerLocation = playerLocation;
        saveState[SaveSlot.saveSlot].health = health;
        saveState[SaveSlot.saveSlot].mana = mana;
        saveState[SaveSlot.saveSlot].hasWeapon = hasWeapon;
        save();
    }
}

public class SaveState {
    public string playerName;
    public string playerLocation;
    public Vector3 playerPosition;
    public float health, mana;
    public bool hasWeapon;
}

public static class SaveGenerator {

    public static string Serialize(SaveState toSerialize, string serialize) {
        StreamWriter writer = new StreamWriter(serialize);
        XmlSerializer xml = new XmlSerializer(typeof(SaveState));
        xml.Serialize(writer, toSerialize);
        writer.Close();
        return writer.ToString();
    }
}
