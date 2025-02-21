using UnityEngine;

[System.Serializable]
public class Dialogue {

    public string myName;
    [Header("Peter Dialogue")]
    [TextArea(3, 10)]
    public string[] peterSentences;

    [Header("Mint Dialogue")]
    [TextArea(3, 10)]
    public string[] mintSentences;
}
