using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorEvent : MonoBehaviour
{
    public GameObject sword;
    private BoxCollider warriorCollider;
    NewAiBehaviour newAiBehaviour;
    // Start is called before the first frame update
    void Start()
    {
        newAiBehaviour = GetComponentInParent<NewAiBehaviour>();
        warriorCollider = sword.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   /* public void ActivateSwordCollider()
    {
        warriorCollider.enabled = true;
    }*/
    public void AttackEffect()
    {
        GameObject arrowFX = Instantiate(newAiBehaviour.slash, newAiBehaviour.spawnWarriorFX.position, newAiBehaviour.spawnWarriorFX.rotation);
    }
}
