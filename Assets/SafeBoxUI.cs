using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using StarterAssets;

public class SafeBoxUI : MonoBehaviour
{
    public const int PASSWORD_LENGTH = 4;
    private const float WRONG_PASSWORD_DAMAGE = 10f;
    List<int> nums = new List<int>();
    [SerializeField] TextMeshProUGUI passwordText;
    [SerializeField] SafeBox safeBox;
    private int password;
    private List<int> passwordNumbers = new List<int>();
    private PhotonView PV;
    private void Awake()
    {
        PV = GetComponent<PhotonView>();
        passwordText.text = "";
        if (PhotonNetwork.IsMasterClient)
        {
            password = Random.Range(1000, 9999);
            PV.RPC(nameof(SetPasswordPunRpc), RpcTarget.All, password);
        }
    }
    [PunRPC]
    private void SetPasswordPunRpc(int password)
    {
        for (int i = PASSWORD_LENGTH - 1; i >= 0; i--)
        {
            passwordNumbers.Add((password % (int)Mathf.Pow(10, i + 1)) / (int)Mathf.Pow(10, i));
        }
        Debug.Log(password);
    }
    private void Start()
    {
        gameObject.SetActive(false);
    }
    public void OnClick_NumberButton(int number)
    {
        if (nums.Count < PASSWORD_LENGTH)
        {
            nums.Add(number);
            UpdateText();
        }
    }
    public void OnClick_DeleteNumber()
    {
        if (nums.Count > 0)
        {
            nums.RemoveAt(nums.Count - 1);
            UpdateText();
        }
    }
    public void OnClick_CheckPassword()
    {
        if (passwordText.text == password.ToString())
        {
            safeBox.OpenSafe();
            GameFlowManager.Instance.AddInfo(PhotonNetwork.LocalPlayer.NickName + " opened the safe!");
        }
        else
            PlayerController.LocalInstance.TakeDamage(PlayerController.LocalInstance.transform.position,
                -PlayerController.LocalInstance.transform.forward,
                WRONG_PASSWORD_DAMAGE);
    }
    public void OnClick_Exit()
    {
        gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        CanvasManager.SetActivation(false);
    }
    private void UpdateText()
    {
        passwordText.text = "";
        foreach (int num in nums)
        {
            passwordText.text += num.ToString();
        }
    }
    public int GetPasswordNumber(int order)
    {
        return passwordNumbers[order];
    }
}
