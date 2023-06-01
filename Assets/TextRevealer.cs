using UnityEngine;
using TMPro;
using Photon.Pun;

public class TextRevealer : MonoBehaviour, I_Interactable
{
    [SerializeField] TextMeshProUGUI text;
    private float targetAlpha = 0;
    private PhotonView PV;
    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }
    public void Interact(bool isPlayer = true) { }

    public void OnFaced()
    {
        PV.RPC(nameof(SetTargetAlphaPunRpc), RpcTarget.All, 1f);
    }

    public void OnInteractEnded()
    {
        PV.RPC(nameof(SetTargetAlphaPunRpc), RpcTarget.All, 0f);
    }
    [PunRPC]
    private void SetTargetAlphaPunRpc(float _targetAlpha)
    {
        targetAlpha = _targetAlpha;
    }
    private void Update()
    {
        text.alpha = Mathf.Lerp(text.alpha, targetAlpha, Time.deltaTime * 25);
    }
}
