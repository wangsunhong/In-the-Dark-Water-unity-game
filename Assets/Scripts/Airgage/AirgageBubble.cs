using UnityEngine;
using System.Collections;

/// <summary>
/// 显示空气残留量的气泡出现的特效
/// </summary>
public class AirgageBubble : MonoBehaviour {

    void OnDisplayDamageLv(int value)
    {
        particleSystem.emissionRate = 5 + 10 * (float)(value);
    }

    void OnGameOver()
    {
        particleSystem.Stop();
    }
}
