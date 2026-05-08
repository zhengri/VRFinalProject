
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class BladeScript : MonoBehaviour
{
    public GameObject blade;
    public XRGrabInteractable sword;
    public Material bladeMaterialOn;
    public Material bladeMaterialOff;
    public Material material;
    public bool bladeStatus = false;
    private void Start()
    {
        
        material = blade.GetComponent<MeshRenderer>().material;
    }

    private void OnEnable()
    {
        sword.activated.AddListener(OnActivate);
        sword.deactivated.AddListener(OnDeactivate);
        sword.selectExited.AddListener(OnRelease);
    }

    public void OnDisable()
    {
        sword.activated.RemoveListener(OnActivate);
        sword.deactivated.RemoveListener(OnDeactivate);
        sword.selectExited.RemoveListener(OnRelease);
    }

    //change colors and turn blade on and off on each activation.
    public void OnActivate(ActivateEventArgs args)
    {
        if (bladeStatus == false)
        {
            material.color = bladeMaterialOn.color;
            bladeStatus = true;

        }
        else if (bladeStatus == true)
        {
            material.color = bladeMaterialOff.color;
            bladeStatus = false;
        }
    }
    public void OnDeactivate(DeactivateEventArgs args)
    {

    }
    //turns sword on when released
    public void OnRelease(SelectExitEventArgs args)
    {
        material.color = bladeMaterialOff.color;
        bladeStatus = false;
    }
    //when sword hits a sword enemy, kills them
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("SwordEnemy") && bladeStatus == true)
        {

            Destroy(other.gameObject);
            Debug.Log("EnemyKilled");
            GameManager.Instance.EnemyKilled();
        }
    }

    void Update()
    {
        
        
    }
}
