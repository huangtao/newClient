﻿using UnityEngine;
using System.Collections.Generic;

public class MaterialManager : MonoBehaviour {
    public Material myCardMatOn;
    public Material myCardMatOff;

    public Material otherCardMat;

    public Material Flowlight_Fast;

    public void TurnOffHandCard() // 听牌，手牌的颜色转换
    {
        if (RoomMgr.actionNotify.tingList.Count > 0)
        {
            int count = Game.MJMgr.MyPlayer.handCardLayout.list.Count;
            for (int i = 0; i < count; i++)
            {
                MJEntity cardTran = Game.MJMgr.MyPlayer.handCardLayout.list[i];
                Renderer r = cardTran.GetComponent<Renderer>();
                int card = cardTran.Card;
                if (RoomMgr.IsTingDropCard(card))
                {
                    r.material = myCardMatOn;
                }
                else
                {
                    r.material = myCardMatOff;
                }
            }
        }
    }

    public void TurnOffAllHandCard()  //手牌变黑
    {
        int count = Game.MJMgr.MyPlayer.handCardLayout.list.Count;
        for (int i = 0; i < count; i++)
        {
            MJEntity cardTran = Game.MJMgr.MyPlayer.handCardLayout.list[i];
            Renderer r = cardTran.GetComponent<Renderer>();
            r.material = myCardMatOff;
        }
    }

    public void TurnOnHandCard()  //手牌变白
    {
        int count = Game.MJMgr.MyPlayer.handCardLayout.list.Count;
        for (int i = 0; i < count; i++)
        {
            Transform child = Game.MJMgr.MyPlayer.handCardLayout.list[i].transform;
            TurnOnCard(child);
        }
    }

    public void TurnOnCard(Transform child)
    {
        if (child.tag != "Dragon_Blank")
        {
            Renderer r = child.GetComponent<Renderer>();
            r.material = myCardMatOn;
        }        
    }
}
