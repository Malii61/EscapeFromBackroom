using Photon.Pun;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class HidedTextSpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> hidedTextSpawnPoints = new List<Transform>();
    [SerializeField] private List<Transform> hidedTexts = new List<Transform>();
    private System.Random rng = new System.Random();
    [SerializeField] SafeBoxUI safeBoxUI;
    PhotonView PV;
    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }
    private void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            var shuffledPointList = hidedTextSpawnPoints.OrderBy(a => rng.Next()).ToList();
            for (int i = 0; i < SafeBoxUI.PASSWORD_LENGTH; i++)
            {
                PV.RPC(nameof(SetHidedTextPunRpc), RpcTarget.All, parameters: new object[] { shuffledPointList[i].position, shuffledPointList[i].rotation, i, safeBoxUI.GetPasswordNumber(i) });
            }
        }
    }
    [PunRPC]
    private void SetHidedTextPunRpc(Vector3 position, Quaternion rotation, int order, int passwordNum)
    {
        Transform hidedText = hidedTexts[order];
        hidedText.position = position;
        hidedText.rotation = rotation;
        hidedText.GetComponentInChildren<TextMeshProUGUI>().text = order + 1 + ") " + passwordNum;
    }
}
