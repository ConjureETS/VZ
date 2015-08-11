using System;
using UnityEngine;
using System.Collections;

public class ZombieUnit : Unit 
{
    // Use this for initialization
    void Start()
    {
        InitializeDefaultTag();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void InitializeDefaultTag()
    {
        try
        {
            this.Tag = TagManager.ZombiePlayer; // set the tag to Zombie       
        }
        catch (IndexOutOfRangeException ex)
        {
            Debug.LogError("Please set a zombie Tag, check the Tag & layers in the inspector!\n" + ex);
        }

        // set the tag of the gameObject to zombie
        if (this.gameObject.tag != Tag)
        {
            this.gameObject.tag = Tag;
        }
    }
}
