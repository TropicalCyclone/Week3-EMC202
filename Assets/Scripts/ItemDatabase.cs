using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="NewItemDatabase", menuName ="Assets/Databases/Item Database")]
public class ItemDatabase : ScriptableObject {
    public List<ItemClass> allItems;

}
