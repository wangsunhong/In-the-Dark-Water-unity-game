using UnityEngine;
using System.Collections;

/// <summary>
/// 用户点击后，向Adapter传递结束场景消息
/// </summary>
public class TitleSwitcher : MonoBehaviour {

    [SerializeField]
    private float waitTime = 3.0f;

    private bool pushed = false;
    private bool fade = false;
   
    void Start()
    {
        guiText.enabled = false;
        Color basecolor = guiText.material.color;
        guiText.material.color = new Color(basecolor.r, basecolor.g, basecolor.b, 0.0f);
    }

    void Update()
    {
        if (!guiText.enabled) return;

        if ( !pushed && Input.GetMouseButtonDown(0))
        {
            pushed = true;
            audio.Play();
            // 传递场景结束消息
            GameObject adapter = GameObject.Find("/Adapter");
            if (adapter) adapter.SendMessage("OnSceneEnd");
            else Debug.Log("adapter is not exist...");
        }
	}

    /// <summary>
    /// 淡入淡出结束时调用
    /// </summary>
    void OnEndTextFade()
    {
        if (!guiText.enabled) return;
        StartCoroutine("Delay");
    }

    /// <summary>
    /// 启动切换器
    /// </summary>
    void OnStartSwitcher()
    {
        Debug.Log("OnStartSwitcher");
        guiText.enabled = true;
        fade = true;
        SendMessage("OnTextFadeIn");
    }
    /// <summary>
    /// 重置关卡
    /// </summary>
    void OnStageReset()
    {
        guiText.enabled = false;
        Color basecolor = guiText.material.color;
        guiText.material.color = new Color(basecolor.r, basecolor.g, basecolor.b, 0.0f);
        pushed = false;
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(waitTime);
        // FadeIn和FadeOut交替执行
        fade = !fade;
        if (fade) SendMessage("OnTextFadeIn");
        else SendMessage("OnTextFadeOut");
    }

}
