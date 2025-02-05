using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

[System.Serializable]
public class AccountSubDetail
{
    public Image Face_Image;
    public Text Name_Text;
    public Text ZhuangTime_Text;
    public Text HuTime_Text;
    public Text PaoTime_Text;
    public Text BaoTime_Text;
    public Text Bao2Time_Text;
    public Text ZhaTime_Text;
    public Text ID_Text;
    public Image WinFlag_Image;
    public Image PaoFlag_Image;
    public Image RoomOwnerFlag_Image;
    public TextMeshProUGUI Score_TextMeshProUGUI;
    //结算界面新的显示需求
    public ScrollRect ScrollView_ScrollRect;
    public Image ScrollView_Image;
    public Mask Viewport_Mask;
    public Image Viewport_Image;
    public GridLayoutGroup Content_GridLayoutGroup;
    public ContentSizeFitter Content_ContentSizeFitter;
    public InningRankSub InningRankSub;
    public Image InningRankSub_image;
}