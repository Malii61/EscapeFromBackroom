using UnityEngine;

public class FinishLine : MonoBehaviour
{
    [SerializeField] Door mainGateLeftDoor;
    [SerializeField] Door mainGateRightDoor;
    private void OnTriggerEnter(Collider other)
    {
        if (mainGateLeftDoor.IsOpen() || mainGateRightDoor.IsOpen())
            Debug.Log("BÝTTÝ");
    }
}
