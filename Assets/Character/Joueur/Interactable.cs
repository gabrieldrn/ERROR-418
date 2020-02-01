using UnityEngine;

public class Interactable : MonoBehaviour
{

    public float radius = 0.5f;               // Distance d'inétéraction par rapport au serveur
    public Transform interactionTransform;  // The transform from where we interact in case you want to offset it

    bool isFocus = false;   // Is this interactable currently being focused?

    bool hasInteracted = false; // Est-ce qu'on a déjà intéragit avec le serveur ?
    Transform player;

    public virtual void Interact()
    {
        // Lorsqu'on intéragit avec le serveur
        Debug.Log("Interacting with " + transform.name);
    }

    void Update()
    {
        // Si l'on a pas encore intéragit avec l'objet
        if (isFocus && !hasInteracted)
        {
            // Si l'on est assez prêt
            float distance = Vector3.Distance(player.position, interactionTransform.position);
            if (distance <= radius)
            {
                // Interaction avec l'objet
                Interact();
                hasInteracted = true;
            }
        }
    }

    public void OnFocused(Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
        hasInteracted = false;
    }

    public void OnDefocused()
    {
        isFocus = false;
        player = null;
        hasInteracted = false;
    }

    void OnDrawGizmosSelected()
    {
        if (interactionTransform == null)
            interactionTransform = transform;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }

}