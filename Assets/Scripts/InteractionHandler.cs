using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionHandler : MonoBehaviour
{
    public bool canInteract;
    private bool immInteractable;
    private int layerMask;
    private RaycastHit hit;

    private float attackCooltime;
    private bool inAttackMode;

    [SerializeField] private GameObject bulletPrefab;

    public delegate void OnMouseClick();
    public OnMouseClick onMouseClick;

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
            GameObject target = hit.collider.gameObject;
            if (target.layer == LayerMask.NameToLayer("Interactable") && target.GetComponent<IInteractable>().IsInteractable())
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
        hasInteractedWithObject = true;
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
