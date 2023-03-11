using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyFacility : MonoBehaviour
{
    public enum FacilityType { Inventory, Sever, RestoreStation, Counter, Table, Shop}
    public FacilityType facilityType;

    public Transform FocusPos;
}
