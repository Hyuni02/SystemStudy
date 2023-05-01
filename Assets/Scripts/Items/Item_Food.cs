using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Food : Item_Useable
{
    [field:SerializeField] public int energy_recovery { get; private set; }
}
