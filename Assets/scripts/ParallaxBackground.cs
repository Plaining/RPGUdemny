using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private GameObject cam;
    [SerializeField] private float parallaxEffect;

    private float xPosition;
    private float length;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = GameObject.Find("Main Camera");
        length = GetComponent<SpriteRenderer>().bounds.size.x;//当前spriteRender也就是图片的宽
        xPosition = transform.position.x;
        
    }

    // Update is called once per frame
    void Update()
    {
        float distanceMoved = cam.transform.position.x * (1 - parallaxEffect); 
        //相机的x方向的速度向量 - 图片准备移动的距离，即相机中心点与背景中心点的x轴距离
        float distanceToMove = cam.transform.position.x * parallaxEffect;
        Debug.Log("distanceToMove:" + xPosition * distanceToMove);
        transform.position = new Vector2(xPosition + distanceToMove, transform.position.y); //比相机移动多了相机速度的parallaxEffect倍速
        if (distanceMoved > xPosition + length)
        {
            xPosition = xPosition + length;
        }
        else if (distanceMoved < xPosition - length)
        {
            xPosition = xPosition - length;
        }

    }
}
