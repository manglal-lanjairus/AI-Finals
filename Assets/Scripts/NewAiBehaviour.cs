using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum AI_Types
{
    Archer,
    Warrior,
    Mage,
    Ninja
}
public class NewAiBehaviour : MonoBehaviour
{
    Dictionary<AI_Types, int> aiModelIndices = new Dictionary<AI_Types, int>()
    {
        { AI_Types.Archer, 0 },
        { AI_Types.Warrior, 1 },
        { AI_Types.Mage, 2 },
        {AI_Types.Ninja,3 }
    };
    [Header("State")]
    public string currentState;
    public AI_Types aiTypes;
    [Header("References")]
    public List<GameObject> aiModel;
    public Animator animator;
    public NavMeshAgent agent;
    public CapsuleCollider capCollider;
    [Header("AttackRange")]
    public float range;
    [Header("EnemyValues")]
    public float health;
    public float damage;
    public new string tag = "Team1";
    public bool isDead;
    [Header("VFX")]
    public GameObject Arrow;
    public GameObject fireball;
    public Transform spawnArcherFX;
    public Transform spawnMageFX;
    // Start is called before the first frame update
    void Start()
    {
        capCollider = GetComponent<CapsuleCollider>();
        agent = GetComponent<NavMeshAgent>();
        Action<AI_Types> activateAiModel = (AI_Types aiType) => 
        {
            aiModel[aiModelIndices[aiType]].SetActive(true);
            animator = aiModel[aiModelIndices[aiType]].GetComponent<Animator>();
        };
        activateAiModel(aiTypes);

        switch (aiTypes)
        {
            case AI_Types.Archer:
                health = GameManager.Instance.archerHealth;
                range = GameManager.Instance.archerRange;
                damage = GameManager.Instance.archerDamage;
                break;
            case AI_Types.Warrior:
                break;
            case AI_Types.Mage:
                health = GameManager.Instance.mageHealth;
                range = GameManager.Instance.mageRange;
                break;
            case AI_Types.Ninja:
                health = GameManager.Instance.ninjaHealth;
                range = GameManager.Instance.ninjaRange;
                break;
            default:
                break;
        }

    }

    private void Update()
    {
        
    }
    // Update is called once per frame
    void LateUpdate()
    {
        if (animator != null)
        {
            AnimationClip currentClip = GetCurrentAnimatorClip(animator, 0);
            // Store the animation name as a string
            if (currentClip != null)
            {
                currentState = currentClip.name;
            }
            else
            {
                currentState = "No animation playing";
            }
        }
        else
        {
            currentState = "Animator reference not set";
        }
    }
    private AnimationClip GetCurrentAnimatorClip(Animator anim, int layer)
    {
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(layer);
        return anim.GetCurrentAnimatorClipInfo(layer)[0].clip;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Arrow"))
        {
            damage = GameManager.Instance.archerDamage;
            float archerDmg = damage;
            Destroy(other.gameObject);
            isEnemyAliveOrNo(archerDmg);
        }
        else if (other.gameObject.CompareTag("Fireball"))
        {
            damage = GameManager.Instance.mageDamage;
            float mageDmg = damage;
            Destroy(other.gameObject);
            isEnemyAliveOrNo(mageDmg);
        }
        
    }
    public float isEnemyAliveOrNo(float damage)
    {
        if (!(health <= 0))
        {
            health = health - damage;
            Debug.Log(aiTypes + " is hit with " + damage + " damage. Health remaining: " + health);
        }
        else
        {
            agent.speed = 0;
            animator.SetBool("isAttacking", false);
            animator.SetTrigger("isDead");
            capCollider.enabled = false;
            isDead = true;
            Destroy(this.gameObject, 2);
        }
        return 0;
    }
}
