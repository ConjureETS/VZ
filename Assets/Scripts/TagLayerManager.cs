using System;
using UnityEngine;
using System.Collections;
using System.Linq;

public class TagLayerManager : MonoBehaviour
{
	// Use this for initialization
    //private static readonly string[] availableTags = UnityEditorInternal.InternalEditorUtility.tags;
    // TODO FIND A WAY TO ADD EXCEPTION ERROR IF THE USER OMMIT TO ADD TAGS!!!
    public static string Human = "Human";
    public static string VampirePlayer = "VampirePlayer";
    public static string ZombiePlayer = "ZombiePlayer";
    // TODO remove hard coding layers
    public static int HumanLayerIndex = 8;
    public static int VampireLayerIndex = 9;
    public static int ZombieLayerIndex = 10;
}
