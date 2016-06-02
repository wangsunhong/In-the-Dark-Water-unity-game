using UnityEngine;
using System.Collections;

/// <summary>
/// 碰撞特效专用
/// 在调用OnHit时，将播放粒子特效和音效
/// </summary>
public class HitEffector : MonoBehaviour {

    [SerializeField]
    private bool valid = true;

    void Start()
    { 
    }

    // 如果无效则提前调用
    void OnInvalidEffect()
    {
        Debug.Log("HitEffector.OnInvalid");
        valid = false;
    }

    // 碰撞时的行为管理和结束时间
    void OnHit()
    {

        Debug.Log("HitEffector.OnHit:" + transform.parent.gameObject.transform.parent.tag);
        if (valid)
        {
            if (particleSystem)
            {
                Debug.Log("HitEffector => particle.Play");
                particleSystem.Play();
            }
            if (audio)
            {
                Debug.Log("HitEffector => audio.Play");
                audio.Play();
            }
        }
        else Debug.Log("HitEffector.OnHit: Invalid");
    }

    public bool IsFinished()
    {
        bool result = true;
        if (particleSystem) result = result && !particleSystem.isPlaying;
        if (audio) result = result && !audio.isPlaying;
        return result;
    }
    public bool IsPlaying()
    {
        bool result = false;
        if (particleSystem) result = result || particleSystem.isPlaying;
        if (audio) result = result || audio.isPlaying;
        return result;
    }
}
