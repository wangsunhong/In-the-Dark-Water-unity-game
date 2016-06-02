using UnityEngine;
using System.Collections;

/// <summary>
/// 声纳下方出现的消息
/// </summary>
public class SonarMessage : MonoBehaviour {

    [SerializeField]
    private string enemyDestroyed = "The enemy is destroyed!";
    [SerializeField]
    private string itemFound = "You found the item!";
    [SerializeField]
    private string itemLost = "The item was lost...";

    void Start() 
    {
        guiText.text = "";
        guiText.enabled = false;
    }

    /// <summary>
    /// 命中对象
    /// </summary>
    /// <param name="tag"></param>
    void OnHitObject( string tag )
    {
        if (tag.Equals("Enemy")) guiText.text = enemyDestroyed;
        else if (tag.Equals("Item")) guiText.text = itemFound;
        // 开始闪烁
        SendMessage("OnStartTextBlink");
    }

    /// <summary>
    /// 对象Lost
    /// </summary>
    /// <param name="tag"></param>
    void OnLostObject( string tag )
    {
        if (tag.Equals("Item"))
        {
            guiText.text = itemLost;
        }
        // 开始闪烁
        SendMessage("OnStartTextBlink");
    }


    void OnEndTextBlink()
    {
        guiText.enabled = false;
    }
}
