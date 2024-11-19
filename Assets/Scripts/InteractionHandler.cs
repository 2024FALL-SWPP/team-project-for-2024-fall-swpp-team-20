using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionHandler : MonoBehaviour
{
    public bool canInteract;
    private bool immInteractable;
    private int layerMask;
    private RaycastHit hit;

    private void Start()
    {
        layerMask = 1 << LayerMask.NameToLayer("Interactable");
        immInteractable = false;
        canInteract = false;
    }

    public void SetInteract(bool value)
    {
        canInteract = value;
    }

    private void Update()
    {
        if (GameManager.GetInstance().GetState() != GameState.Playing) return;

        if (canInteract && Physics.Raycast(transform.position, transform.forward, out hit, 5f, layerMask))
        {
            if (!immInteractable)
            {
                ShowInteractableUI(hit);
            }
        }
        else if (immInteractable)
        {
            HideInteractableUI();
        }

        Debug.DrawRay(transform.position, 5 * transform.forward, Color.red);
    }

    public void HandleInteraction()
    {
        if (GameManager.GetInstance().GetState() != GameState.Playing || !immInteractable) return;

        GameObject target = hit.transform.gameObject;
        var interactable = target.GetComponent<IInteractable>();
        interactable?.Interact(target);
    }

    private void ShowInteractableUI(RaycastHit hit)
    {
        immInteractable = true;
        GameManager.GetInstance().um.ShowInteractionInfo(hit);
    }

    private void HideInteractableUI()
    {
        immInteractable = false;
        GameManager.GetInstance().um.HideInteractionInfo();
    }
}
