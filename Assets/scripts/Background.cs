using UnityEngine;

public class Background : MonoBehaviour
{
    private float count;
    public float res;
    public float position_X;
    public float offset;
    public float speed;
    private Renderer MyRenderer;
    public GameObject HeroObject;

    private Hero characterControllerScript;

    void Awake()
    {
        characterControllerScript = HeroObject.GetComponent<Hero>();
    }

    void Start()
    {
        MyRenderer = GetComponent<Renderer>();
        //position_X = characterControllerScript.GetComponent<Rigidbody2D>().position.x;
    }

    void Update()
    {
        //speed = HeroObject.GetComponent<Rigidbody2D>().velocity.x;
        //if (Hero.Instance.move == 1)
        //{
        //    offset += 0.00125f;
        //}
        //if (Hero.Instance.move == -1)
        //{
        //    offset -= 0.00125f;
        //}
        //else
        //{

        //}

        position_X = characterControllerScript.GetComponent<Rigidbody2D>().position.x;


        offset = position_X * speed;
        MyRenderer.material.mainTextureOffset = new Vector2(offset, 0);
    }
}
