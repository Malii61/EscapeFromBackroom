using Photon.Pun;
using UnityEngine;

public class SafeBox : MonoBehaviour, I_Interactable
{
    [SerializeField] private Transform safeBoxUI;
    [SerializeField] private Transform keySpawnPosition;
    private bool isLocked = true;
    private PhotonView PV;
    private Animator anim;
    private void Awake()
    {
        PV = GetComponent<PhotonView>();
        anim = GetComponent<Animator>();
    }
    public void Interact(bool isPlayer = true)
    {
        if (isLocked)
        {
            safeBoxUI.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            CanvasManager.SetActivation(true);
        }
    }

    public void OnFaced()
    {
        InteractionText.Instance.SetText("Safe Box\n[E]");
    }

    public void OnInteractEnded()
    {
        InteractionText.Instance.DisableText();
    }
    public void OpenSafe()
    {
        PV.RPC(nameof(OpenSafePunRpc), RpcTarget.All);
    }
    [PunRPC]
    private void OpenSafePunRpc()
    {
        safeBoxUI.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        CanvasManager.SetActivation(false);
        anim.SetBool("isOpen", true);
        Destroy(GetComponent<BoxCollider>());
        if (PhotonNetwork.IsMasterClient)
        {
            //instantiate key
            KeySpawner.Instance.CreateKey(KeyType.BasemantKey, keySpawnPosition);
        }
    }
}
