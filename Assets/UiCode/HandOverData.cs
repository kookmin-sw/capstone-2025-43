using System;
using System.Collections.Generic;
using UnityEditor.AddressableAssets.Build.BuildPipelineTasks;
using UnityEngine;

[System.Serializable]
public class HandOverData
{
    [Header("#Position")]
    public string[] unitPositions = new string[9];


    [Header("# Node & Edge")]
    public List<vector_localinfo> list_localinfos = new List<vector_localinfo>();
    public List<Edge> roads = new List<Edge>();

    [Header("# OpenLocal")]
    public Vector2 openLocal;

    /*[Header("# OwnHeroList")]
    public List<string> ownHero = new List<string>() { "HeroMage", "Werewolf", "NagaWizard", "BlackKnight", "FishMan" };*/
}
