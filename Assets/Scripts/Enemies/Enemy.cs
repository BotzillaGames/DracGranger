using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Enemies", menuName = "ScriptableObjects/Enemies", order = 1)]

public class Enemy : ScriptableObject
{
     public GameObject prefab;
     public float velocity;
     public int numLifes;
     public string enemyType;

}