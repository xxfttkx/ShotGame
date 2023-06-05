using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordPanelUI : MonoBehaviour
{
    public Text playerHealthText;
    public Text killEnemyNumText;
    public Text shootBulletNumText;

    private void OnEnable()
    {
        EventHandler.UpdateRecordPanelUI += OnUpdateRecordPanelUI;
    }
    private void OnDisable()
    {
        EventHandler.UpdateRecordPanelUI -= OnUpdateRecordPanelUI;
    }

    //TODO 可以分开
    public void OnUpdateRecordPanelUI()
    {
        playerHealthText.text = RecordManager.Instance.playerHealth.ToString();
        killEnemyNumText.text = RecordManager.Instance.killEnemyNum.ToString();
        shootBulletNumText.text = RecordManager.Instance.shootBulletNum.ToString();
    }
}
