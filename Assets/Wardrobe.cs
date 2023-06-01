using ExitGames.Client.Photon;
using Photon.Pun;
using UnityEngine;

public class Wardrobe : MonoBehaviour
{
    private bool isPlayerInside = false;
    [SerializeField] private WardrobeDoor leftDoor;
    [SerializeField] private WardrobeDoor rightDoor;
    private void OnTriggerEnter(Collider other)
    {
        isPlayerInside = true;
    }
    private void OnTriggerExit(Collider other)
    {
        isPlayerInside = false;
    }
    public void CheckIfPlayerIsHided()
    {
        bool isHided = isPlayerInside && leftDoor.IsDoorClosed() && rightDoor.IsDoorClosed();
        Debug.Log("hided? " + isHided);
        PhotonNetwork.LocalPlayer.SetCustomProperties(new Hashtable { { "isHided", isHided } });
    }
}
