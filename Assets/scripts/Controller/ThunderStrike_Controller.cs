using UnityEngine;

public class ThunderStrike_Controller : MonoBehaviour
{
    private PlayerStat playerStat;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerStat = PlayerManager.instance.GetComponent<PlayerStat>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
