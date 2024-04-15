using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Enemies", menuName = "ScriptableObjects/Enemies", order = 0)]

public class Enemy : ScriptableObject
{
     public GameObject prefab;
     public float velocity;
     public int numLifes;

}