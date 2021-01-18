﻿using UnityEngine;
[CreateAssetMenu(fileName ="NPC資料",menuName ="BoGao/NPC資料")]
public class NPCDate : ScriptableObject
{
    [Header("第一段對話"), TextArea(1,5)]
    public string dialougA;
    [Header("第二段對話"), TextArea(1, 5)]
    public string dialougB;
    [Header("第三段對話"), TextArea(1, 5)]
    public string dialougC;
    [Header("任務目標需求數量")]
    public int count;
    [Header("已經取得項目數量")]
    public int countCurrent;

}