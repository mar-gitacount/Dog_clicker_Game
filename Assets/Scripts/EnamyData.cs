using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]


public class EnamyData : ScriptableObject
{
    [SerializeField]private SpriteRenderer enamySpriteRenderer;
    public string enamyKinds;
    public int hp;
    public int attackPower;
    public float moveSpeed;
    public string picturePath;
    public Color color;
   
}
