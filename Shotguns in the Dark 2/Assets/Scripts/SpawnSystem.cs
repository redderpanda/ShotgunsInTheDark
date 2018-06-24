using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSystem : MonoBehaviour {
    public Transform[] monsterspawner;
    public Transform monster;
    int randomspawnpoint;
    int randomweaponspawnpoint, randomweapon;
    public int counter;
    bool monsterspawnAllowed;
    //bool weaponspawnAllowed;

    // Use this for initialization
    void Start () {
        monsterspawnAllowed = true;
        //weaponspawnAllowed = true;
        InvokeRepeating("SpawnMonster", 0, 20);
    }

    //Checks if there is a Monster player being controlled or if there is a MonsterItem in the game.
    //If not then it spawns a MonsterItem in by selecting a random spawn point that is deisgnated for the monster.
    //If there is a Monster or MonsterItem in the field then it does not spawn a new one.
    //Reminder: destroy gameobject upon pickup.
    void SpawnMonster()
    {
        if (GameObject.FindGameObjectWithTag("Monster") || GameObject.FindGameObjectWithTag("MonsterItem"))
        {
            monsterspawnAllowed = false;
        }
        else
        {
            monsterspawnAllowed = true;
        }

        if (monsterspawnAllowed)
        {
            randomspawnpoint = Random.Range(0, monsterspawner.Length);
            Instantiate(monster, monsterspawner[randomspawnpoint].position, Quaternion.identity);
        }
    }

}
