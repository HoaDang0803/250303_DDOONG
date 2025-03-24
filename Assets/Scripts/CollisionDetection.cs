using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public WeaponController weaponController;
    //public GameObject hitEffect;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") && weaponController.isAttacking)
        {
            Debug.Log("Hit enemy");
            //Instantiate(hitEffect, new Vector3(other.transform.position.x, other.transform.position.y, other.transform.position.z), other.transform.rotation);
            //other.GetComponent<Animator>().SetTrigger("Hit");
        }
    }
}
