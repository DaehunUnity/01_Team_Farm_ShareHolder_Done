using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITextEffect : MonoBehaviour
{
    string TextEffect;
    public Text UItext;

    private void Awake()
    {
        UItext = GetComponent<Text>();
        TextEffect = UItext.text;
    }
    private void OnEnable()
    {
        UItext.text = "";
        StartCoroutine("TextTappingEffect");
    }

    IEnumerator TextTappingEffect()
    {
        for (int i = 0; i < TextEffect.Length; i++)
        {
            UItext.text += TextEffect[i];
            yield return new WaitForSeconds(0.03f);
        }
    }

    // Update is called once per frame
    void Update()
    {
      
    }
}
