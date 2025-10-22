using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class ParallaxScript : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private float length, startpos;

    public GameObject cam;
    public float parallaxEffect;
    public float smoothingX = 1f; 
    public float smoothingY = 1f;
    void Start()
    {
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
       
    }

    void Update()
    {
        float temp = (cam.transform.position.x * (1 - parallaxEffect));
        float dist = (cam.transform.position.x * parallaxEffect);

        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);

//        int x = (int)transform.position.x*100;

  //      transform.position = new Vector2(x/100, transform.position.y);


        if (temp > startpos + length) startpos += length;
        else if (temp < startpos - length) startpos -= length;
    
    }


}