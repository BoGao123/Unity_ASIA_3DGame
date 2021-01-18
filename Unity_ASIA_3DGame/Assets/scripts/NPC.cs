
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NPC : MonoBehaviour
{
    [Header("NPC資料")]
    public NPCDate data;
    [Header("對話框")]
    public GameObject dialoug;
    [Header("對話內容")]
    public Text textContent;
    [Header("對話者名稱")]
    public Text textName;
    [Header("對話間隔")]
    public float interval = 0.05f;
   
    public bool playerInArea;

    public enum NPCState
    {
        FirstDialoug, Missioning, Finish
    }

    public NPCState state = NPCState.FirstDialoug;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "機器人")
        {
            playerInArea = true;
            StartCoroutine(Dialoug());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "機器人")
        {
            playerInArea = false;
            StopDialoug();
        }
    }
    private void StopDialoug()
    {
        dialoug.SetActive(false);
        StopAllCoroutines();
    }
    private IEnumerator Dialoug()
    {
        dialoug.SetActive(true);

        textContent.text = "";
        textName.text = name;

        string dialougString = data.dialougB;

        switch (state)
        {
            case NPCState.FirstDialoug:
                dialougString = data.dialougA;
                break;
            case NPCState.Missioning:
                dialougString = data.dialougB;
                break;
            case NPCState.Finish:
                dialougString = data.dialougC;
                break;

        }
        
        for (int i = 0; i < dialougString.Length; i++)
        {
            textContent.text += dialougString[i] + "";
            yield return new WaitForSeconds(interval);
        }
    }
}