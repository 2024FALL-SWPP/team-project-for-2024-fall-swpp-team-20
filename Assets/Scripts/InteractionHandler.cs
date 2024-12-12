using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionHandler : MonoBehaviour
{
    public bool canInteract;
    private bool immInteractable;
    private int layerMask;
    private GameObject target;
    private RaycastHit hit;

    private float attackCooltime;
    private bool inAttackMode;

    [SerializeField] private GameObject bulletPrefab;

    public delegate void OnMouseClick();
    public OnMouseClick onMouseClick;

    InteractableObject interactableObject;

    private bool hasInteractedWithObject;

    private void Awake()
    {
        layerMask = (1 << LayerMask.NameToLayer("Interactable")) | (1 << LayerMask.NameToLayer("Default"));
        immInteractable = false;
        canInteract = false;
    }

    public void SetMouseClickAction(int actionCode)
    {
        switch (actionCode)
        {
            case 0:
                onMouseClick = HandleInteraction;
                inAttackMode = false;
                break;
            case 1:
                onMouseClick = Attack;
                attackCooltime = 0f;
                inAttackMode = true;
                break;
            default:
                break;
        }
    }

    public void SetInteract(bool value)
    {
        canInteract = value;
    }

    private void Update()
    {
        if (inAttackMode) attackCooltime -= Time.deltaTime;
        if (canInteract && Physics.Raycast(transform.position, transform.forward, out hit, 5f, layerMask))
        {
            GameObject newTarget = hit.collider.gameObject;
            if (IsValidInteractable(newTarget))
            {
                if (target != null && target != newTarget)
                {
                    interactableObject?.EndGlow();
                }
                EnableInteraction(newTarget);
            }
            else if (immInteractable)
            {
                DisableInteraction();
            }
        }
        else if (immInteractable)
        {
            DisableInteraction();
        }
        Debug.DrawRay(transform.position, 5 * transform.forward, Color.red);
    }
    private bool IsValidInteractable(GameObject target)
    {
        return target.layer == LayerMask.NameToLayer("Interactable")
        && target.GetComponent<IInteractable>() != null
        && target.GetComponent<IInteractable>().IsInteractable();
    }

    private void DisableInteraction()
    {
        HideInteractableUI();
        if (interactableObject == null) return;
        interactableObject?.EndGlow();
        interactableObject = null;
        target = null;
    }

    private void EnableInteraction(GameObject newTarget)
    {
        ShowInteractableUI(newTarget);
        interactableObject = newTarget.GetComponent<InteractableObject>();
        interactableObject.StartGlow();
        target = newTarget;
    }

    public void HandleInteraction()
    {
        if (GameManager.GetInstance().GetState() != GameState.Playing || !immInteractable) return;

        GameObject target = hit.transform.gameObject;
        var interactable = target.GetComponent<IInteractable>();
        interactable?.Interact(target);
        interactableObject?.EndGlow();
        interactableObject = null;
        hasInteractedWithObject = true;
    }

    private void ShowInteractableUI(GameObject newTarget)
    {
        immInteractable = true;
        GameManager.GetInstance().um.ShowInteractionInfo(newTarget);
    }

    private void HideInteractableUI()
    {
        immInteractable = false;
        GameManager.GetInstance().um.HideInteractionInfo();
    }

    public void Attack()
    {
        if (attackCooltime > 0) return;
        Vector3 direction = transform.forward;
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        BulletBehaviour bulletBehaviour = bullet.GetComponent<BulletBehaviour>();
        bulletBehaviour.Shoot(direction);
        attackCooltime = 0.7f;
    }

    public bool HasInteractedWithObject()
    {
        return hasInteractedWithObject;
    }
}
