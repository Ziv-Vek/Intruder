using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] int damage = 20;
    public int Damage { get { return damage; } }

}
