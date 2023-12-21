using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonWeaponScript : MonoBehaviour
{
    [SerializeField] Collider hitbox;
    [SerializeField] float damage;
    GameObject player;
    Character character;
    SkeletonScript skeleton;
    // Start is called before the first frame update
    void Start()
    {
        skeleton = GetComponent<SkeletonScript>();
        player = skeleton.GetPlayer();
        character = player.GetComponent<Character>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            character.Hurt(damage);
        }
    }
}
