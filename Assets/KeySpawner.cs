using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

public class KeySpawner : MonoBehaviour
{
    public static KeySpawner Instance { get; private set; } 

    [SerializeField] List<Transform> HouseKeyTransformList = new List<Transform>();
    [SerializeField] private Transform MainGateKeyTransform;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            // create house key
            int randomOrder = Random.Range(0, HouseKeyTransformList.Count);
            Transform keyTransform = HouseKeyTransformList[randomOrder];
            CreateKey(KeyType.HouseKey, keyTransform);

            // create main gate key
            CreateKey(KeyType.MainGateKey, MainGateKeyTransform);
        }
    }
    public void CreateKey(KeyType key, Transform keyTransform)
    {
        PhotonNetwork.Instantiate("PhotonPrefabs/Keys/" + key.ToString(), keyTransform.position, keyTransform.rotation);
    }

}
