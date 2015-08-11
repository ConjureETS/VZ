using System;
using UnityEngine;
using System.Collections;
using System.Linq;

public class TagManager : MonoBehaviour
{
	// Use this for initialization
    private static readonly String[] availableTags = UnityEditorInternal.InternalEditorUtility.tags;
    // TODO FIND A WAY TO ADD EXCEPTION ERROR IF THE USER OMMIT TO ADD TAGS!!!
    public static String Human = availableTags[7];
    public static String VampirePlayer = availableTags[8];
    public static String ZombiePlayer = availableTags[9];
}
