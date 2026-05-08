using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
public class NadeScript : MonoBehaviour
{
    public GameObject Nade;
    public XRGrabInteractable bomb;
    public Material material;
    public float explosionRadius;
    public bool exploded = false;
    public bool armed = false;
    public Rigidbody rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Level") == true)
        {
            if (exploded) return;

            exploded = true;
            Debug.Log("exploded");
            Explode();
        }
        
    }
    private void OnEnable()
    {
        bomb.selectEntered.AddListener(OnGrab);
        bomb.selectExited.AddListener(OnRelease);

    }
    private void OnDisable()
    {
        bomb.selectEntered.RemoveListener(OnGrab);
        bomb.selectExited.RemoveListener(OnRelease);
    }

    public void OnGrab(SelectEnterEventArgs args)
    {
        armed = true;

        rb.isKinematic = true;

        Debug.Log("ongrab armed");
    }
    //nade is live only after being picked up and thrown
    public void OnRelease(SelectExitEventArgs args)
    {
        armed = true;

        // enable physics again so it can be thrown
        rb.isKinematic = false;
        rb.useGravity = true;

        Debug.Log("onrelease armed");
    }

    //only explodes when nade is armmed and hits something
    void Explode()
    {
        if (armed == false) return;
        Collider[] hits = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("NadeEnemy") && armed == true)
            {
                Destroy(hit.gameObject);
                Debug.Log("killed nadeenemy");
                GameManager.Instance.EnemyKilled();
            }
        }
        Destroy(gameObject);

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
