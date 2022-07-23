using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

public class ButtonWrapperScript : Button
{
    public TMP_Text buttonText;
    public float fadeOutDuration;
    public bool isFadedOut;
    // Start is called before the first frame update
    void Start()
    {
        isFadedOut = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void fadeOut()
    {
        StartCoroutine(FadeButtonOut());
    }

    IEnumerator FadeButtonOut()
    {
        float t = 0f;

        while (t < fadeOutDuration)
        {
            t += Time.deltaTime;

            if (t > fadeOutDuration)
            {
                t = fadeOutDuration;
            }

            float alphaValue = Mathf.Lerp(1, 0, t / fadeOutDuration);

            this.image.color = new Color(this.image.color.r,
                                                    this.image.color.g,
                                                    this.image.color.b,
                                                    alphaValue);
            buttonText.alpha = alphaValue;
            yield return null;
        }
            isFadedOut = true;
            this.gameObject.SetActive(false);
    }
}


[CustomEditor(typeof(ButtonWrapperScript))]
public class ButtonWrapperScriptEditor : UnityEditor.UI.ButtonEditor
{


    public override void OnInspectorGUI()
    {

        ButtonWrapperScript component = (ButtonWrapperScript)target;

        base.OnInspectorGUI();

        component.buttonText = (TMP_Text)EditorGUILayout.ObjectField("Button Text", component.buttonText, typeof(TMP_Text), true);
        component.fadeOutDuration = EditorGUILayout.FloatField(component.fadeOutDuration);

    }
}