using System.Buffers;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

//add linerenderer to see bullet path for player feedback
public class GunScript : MonoBehaviour
{
    //gun part references
    public GameObject barrel;
    public XRGrabInteractable gun;
    private Transform gunBarrelTrans;
    public Material gunBarrelMaterial;
    //gun bullet and range size
    public float sphereRadius = 0.5f;
    public float range = 100f;


    // line renderer
    public LineRenderer line;
    public float lineDuration = 0.05f;
    private float lineDisableTime;

    Material material;
    Color color;

    public LayerMask hitMask;
    
    private void Start()
    {
        gunBarrelTrans = barrel.transform;
        material = barrel.GetComponent<MeshRenderer>().material;
        color = material.color;
        //gun = GetComponentInParent<XRGrabInteractable>();
    }
    private void OnEnable()
    {
        gun.activated.AddListener(OnActivate);
        gun.deactivated.AddListener(OnDeactivate);
    }

    private void OnDisable()
    {
        gun.activated.RemoveListener(OnActivate);
        gun.deactivated.RemoveListener(OnDeactivate);
    }
    
    public void OnActivate(ActivateEventArgs args)
    {
        material.color = Color.yellow;
        Shoot();
        Debug.Log("GunShot");
    }

    public void OnDeactivate(DeactivateEventArgs args) 
    {
        material.color = gunBarrelMaterial.color;
    }
    void Shoot()
    {


        RaycastHit hit;

        Vector3 start = gunBarrelTrans.position;
        Vector3 direction = gunBarrelTrans.forward;

        Vector3 end = start + direction * range;
        //kill first enemy hit
        if (Physics.SphereCast(start, sphereRadius, direction, out hit, range, hitMask))
        {
            end = hit.point;

            if (hit.collider.CompareTag("GunEnemy"))
            {
                Destroy(hit.collider.gameObject);
                GameManager.Instance.EnemyKilled();
            }
        }

        ShowShotLine(start, end);
    }
    void ShowShotLine(Vector3 start, Vector3 end)
    {
        line.SetPosition(0, start);
        line.SetPosition(1, end);

        line.enabled = true;
        lineDisableTime = Time.time + lineDuration;
    }

    private void Update()
    {
        //show gun aim direction
        if (line.enabled && Time.time >= lineDisableTime)
        {
            line.enabled = false;
        }
    }
}
