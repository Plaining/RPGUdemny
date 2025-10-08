using TMPro;
using UnityEngine;

public class Blackhole_hotkey_controller : MonoBehaviour
{
    private SpriteRenderer sr;
    private KeyCode myHotkey;
    private TextMeshProUGUI myText;
    private Transform myEnemy;
    private Blackhole_skill_controller blackHole;

    public void SetupHotKey(KeyCode _myHotKey, Transform _myEnermy, Blackhole_skill_controller _myBlackHole)
    {
        sr =GetComponent<SpriteRenderer>();
        myText = GetComponentInChildren<TextMeshProUGUI>();
        myEnemy = _myEnermy;
        blackHole = _myBlackHole;

        myHotkey = _myHotKey;
        myText.text = myHotkey.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(myHotkey))
        {
            blackHole.AddEnemyToList(myEnemy);
            myText.color = Color.clear;
            sr.color = Color.clear;
        }
    }
}
