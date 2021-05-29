using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblesManager : MonoBehaviour
{
    public LoadZone load;
    public string levelId;

    private Collectible[] collectibles;

    // Start is called before the first frame update
    void Start()
    {
        load = FindObjectOfType<LoadZone>();
        InitCollectibles();        
    }

    private void InitCollectibles()
    {
        collectibles = FindObjectsOfType<Collectible>();
        load.GetCollectiblesForLevel(levelId);
        foreach (Collectible collectible in collectibles)
            if (load.IsCollected(collectible.metal.ToString()))
                collectible.gameObject.SetActive(false);
    }
}
