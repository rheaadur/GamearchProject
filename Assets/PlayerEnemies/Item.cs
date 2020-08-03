using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    // Start is called before the first frame update
    public enum ItemType
    {
        Heart,
        Throwingitem,
        Key,

    }

    public ItemType itemType;
    public int Amount = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
