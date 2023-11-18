using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageEvent : MonoBehaviour
{
    NewAiBehaviour newAiBehaviour;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        newAiBehaviour = GetComponentInParent<NewAiBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ShootFireball()
    {
        GameObject arrowFX = Instantiate(newAiBehaviour.fireball, newAiBehaviour.spawnMageFX.position, newAiBehaviour.spawnMageFX.rotation);
        Rigidbody bulletRigidbody = arrowFX.GetComponent<Rigidbody>();
        bulletRigidbody.AddForce(transform.forward * speed, ForceMode.Impulse);
    }
}
