using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddressableManager : MonoBehaviour
{

    [SerializeField] private AssetReference playerAssetRefrence;
    
    // Start is called before the first frame update
    void Start()
    {
        Addressables.InitializeAsync().Completed += AddressableManager_Completed;
    }

    private void AddressableManager_Completed(AsyncOperationHandle<IResourceLocator> obj)
    {
        playerAssetRefrence.InstantiateAsync().Completed += (GameObject) =>
        {

        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
