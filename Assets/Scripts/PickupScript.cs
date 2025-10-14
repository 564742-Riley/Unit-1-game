using UnityEngine;

public class Pickup : MonoBehaviour
{
    public CoinManager cm;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

   
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (gameObject.CompareTag("coin"))
        {
            cm.coinCount++;
            Destroy(gameObject);
        }

    }
}
