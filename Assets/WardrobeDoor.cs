using UnityEngine;
using Photon.Pun;

public class WardrobeDoor : MonoBehaviour, I_Interactable
{
    private enum DoorDir
    {
        left,
        right
    }
    [SerializeField] private DoorDir doorDir;
    private Animator anim;
    [SerializeField] private Wardrobe wardrobe;
    private bool isOpen = false;
    private PhotonView PV;
    private string openDoorAnimBoolName;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
        anim = GetComponent<Animator>();
        SetAnimText();
    }

    private void SetAnimText()
    {
        switch (doorDir)
        {
            case DoorDir.left:
                openDoorAnimBoolName = "isLeftDoorOpen";
                break;
            case DoorDir.right:
                openDoorAnimBoolName = "isRightDoorOpen";
                break;
        }
    }

    public void Interact(bool isPlayer = true)
    {
        isOpen = !isOpen;
        PV.RPC(nameof(InteractPunRpc), RpcTarget.All, isOpen);

    }
    [PunRPC]
    private void InteractPunRpc(bool _isOpen)
    {
        isOpen = _isOpen;
        anim.SetBool(openDoorAnimBoolName, isOpen);
        wardrobe.CheckIfPlayerIsHided();

    }

    public void OnFaced()
    {
        InteractionText.Instance.SetText("[E]");
    }

    public void OnInteractEnded()
    {
        InteractionText.Instance.DisableText();
    }
    public bool IsDoorClosed()
    {
        return !isOpen;
    }
}
