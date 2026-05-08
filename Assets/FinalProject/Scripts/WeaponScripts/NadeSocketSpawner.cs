using UnityEngine;

public class NadeSocketSpawner : MonoBehaviour
{
    public GameObject nadePrefab;
    public Transform socketPoint;
    public float respawnCD = 2f;

    private GameObject currentNade;
    private bool respawning = false;

    void Start()
    {
        SpawnNade();
    }

    void Update()
    {
        // keep grenade snapped to socket while ungrabbed
        if (currentNade != null)
        {
            NadeScript nade = currentNade.GetComponent<NadeScript>();

            if (nade != null && !nade.armed)
            {
                currentNade.transform.position = socketPoint.position;
                currentNade.transform.rotation = socketPoint.rotation;
            }
        }

        // respawn after grenade destroyed
        if (currentNade == null && !respawning)
        {
            respawning = true;
            Invoke(nameof(SpawnNade), respawnCD);
        }
    }

    void SpawnNade()
    {
        respawning = false;

        if (currentNade != null) return;

        currentNade = Instantiate(
            nadePrefab,
            socketPoint.position,
            socketPoint.rotation
        );

        Rigidbody rb = currentNade.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
        }
    }
}