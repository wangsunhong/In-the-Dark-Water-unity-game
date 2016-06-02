using UnityEngine;
using System.Collections;

/// <summary>
/// 定期发出声音
/// 碰撞后的淡出处理
/// 确认碰撞特效结束后将销毁对象事件通知父对象
/// </summary>
public class Note : MonoBehaviour {
    [SerializeField]
    private float interval = 1.0f;  // 声音的间隔
    [SerializeField]
    private float offset = 0.0f;    // 最开始出现声音的时间偏移
    [SerializeField]
    private bool valid   = true;    // 值为true则有效

    private HitEffector hitEffector = null;
    private float counter = 0.0f;

	void Start () 
    {
        hitEffector = gameObject.GetComponentInChildren<HitEffector>();
        counter = offset;
    }

	void FixedUpdate () 
    {
        if (valid && !audio.isPlaying) 
        {
            Clock(Time.deltaTime);
        }
	}


    private void Clock(float step)
    {
        counter += step;
        if (counter >= interval)
        {
            audio.Play();
            counter = 0.0f;
        }
    }

    /// <summary>
    /// 声音有效／无效
    /// </summary>
    /// <param name="flag"></param>
    public void SetEnable(bool flag) { valid = flag; }

    void OnHit()
    {
        valid = false;
        // 使用Stop时声音可能会突然停顿，因此稍作处理令声音淡出
        //audio.Stop();
        StartCoroutine("Fadeout", 1.0f);
    }


    /// <summary>
    /// 淡出处理协程
    /// </summary>
    /// <param name="duration"></param>
    /// <returns></returns>
    private IEnumerator Fadeout(float duration)
    {
        // 淡出
        float currentTime = 0.0f;
        float waitTime = 0.02f;
        float firstVol = audio.volume;
        while (duration > currentTime)
        {
            audio.volume = Mathf.Lerp(firstVol, 0.0f, currentTime / duration);
            yield return new WaitForSeconds(waitTime);
            currentTime += waitTime;
        }

        // 淡出处理完全结束后销毁对象
        if (hitEffector)
        {
            while (hitEffector.IsPlaying())
            {
                yield return new WaitForSeconds(waitTime);
            }
        }
        // 发送销毁对象消息
        transform.parent.gameObject.SendMessage("OnDestroyLicense");
    }
}
