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
        length = GetComponent<SpriteRenderer>().bounds.size.x;//��ǰspriteRenderҲ����ͼƬ�Ŀ�
        xPosition = transform.position.x;
        
    }

    // Update is called once per frame
    void Update()
    {
        float distanceMoved = cam.transform.position.x * (1 - parallaxEffect); 
        //�����x������ٶ����� - ͼƬ׼���ƶ��ľ��룬��������ĵ��뱳�����ĵ��x�����
        float distanceToMove = cam.transform.position.x * parallaxEffect;
        Debug.Log("distanceToMove:" + xPosition * distanceToMove);
        transform.position = new Vector2(xPosition + distanceToMove, transform.position.y); //������ƶ���������ٶȵ�parallaxEffect����
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
