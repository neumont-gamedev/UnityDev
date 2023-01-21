using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Collidable
{
    [SerializeField] GameObject pickupFX;

    void Start()
    {
        onEnter += OnCoinPickup;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCoinPickup(GameObject go)
    {
        if (go.TryGetComponent<RollerPlayer>(out RollerPlayer player))
        {
            player.AddPoints(100);
        }

        Instantiate(pickupFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
