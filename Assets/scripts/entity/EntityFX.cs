using System.Collections;
using UnityEngine;

public class EntityFX : MonoBehaviour
{
    private SpriteRenderer sr;

    [Header("Flash FX")]
    [SerializeField] private float flashDuration;
    [SerializeField] private Material hitMat;
    private Material originalMat;

    [Header("Ailment colors")]
    [SerializeField] private Color[] chillColors;
    [SerializeField] private Color[] igniteColors;
    [SerializeField] private Color[] shockColors;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        originalMat = sr.material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void MakeTransparent(bool _transparent)
    {
        if (_transparent)
        {
            sr.color = Color.clear;
        }
        else
        {
            sr.color = Color.white;
        }
    }

    private IEnumerator FlashFX()
    {
        sr.material = hitMat;
        Color currentColor = sr.color;
        sr.color = Color.white;

        yield return new WaitForSeconds(flashDuration);
        sr.material = originalMat;
        sr.color = currentColor;
    }

    private void RedColerBlink()
    {
        if (sr.color != Color.white)
        {
            sr.color = Color.white;
        }
        else
        {
            sr.color = Color.red;
        }
    }

    private void CancelColorChange()
    {
        CancelInvoke();
        sr.color = Color.white ;
    }
    public void ShockFxFor(float _seconds)
    {
        InvokeRepeating("ShockColorFx", 0, .3f);
        Invoke("CancelColorChange", _seconds);
    }
    public void ChillFxFor(float _seconds)
    {
        InvokeRepeating("ChillColorFx", 0, .3f);
        Invoke("CancelColorChange", _seconds);
    }
    public void IgniteFxFor(float _seconds)
    {
        InvokeRepeating("IgniteColorFx", 0, .3f);
        Invoke("CancelColorChange",_seconds);
    }
    public void IgniteColorFx()
    {
        if(sr.color != igniteColors[0])
        {
            sr.color = igniteColors[0];
        }
        else
        {
            sr.color = igniteColors[1];
        }
    }
    private void ShockColorFx()
    {
        if (sr.color != shockColors[0])
        {
            sr.color = shockColors[0];
        }
        else
        {
            sr.color = shockColors[1];
        }
    }
    private void ChillColorFx()
    {
        if (sr.color != chillColors[0])
        {
            sr.color = chillColors[0];
        }
        else
        {
            sr.color = chillColors[1];
        }
    }
}
