using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardController : MonoBehaviour {
    public List<EquippableItem> rewardList;


    public void SendReward(string name)
    {
        foreach(EquippableItem item in rewardList)
        {
            if (item.name == name)
            {
                FindObjectOfType<Inventory>().AddItem(item);
            }
        }
    }
	

}
