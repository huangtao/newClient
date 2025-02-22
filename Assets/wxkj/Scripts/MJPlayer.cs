﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using packet.mj;
using System;

public class MJPlayer : MonoBehaviour {
    public HandCardLayout handCardLayout;
	public DropCardLayout dropCardLayout;
	public TableCardLayout tableCardLayout;
    public ShouPaoCardLayout shouPaoCardLayout;
    public JiuYaoCardLayout jiuYaoCardLayout;
    public LiangCardLayout liangCardLayout;
    public Transform baoRoot;
    public Transform EffectPos;
    public MJHand MJHand;

    public int postion;
	public int index;
	public int otherPlayerLastDropCard = -1;

	public void Clear()
	{
        handCardLayout.Clear();
        dropCardLayout.Clear();
        tableCardLayout.Clear();

        shouPaoCardLayout.Clear();
        jiuYaoCardLayout.Clear();
        liangCardLayout.Clear();

        if (baoRoot.childCount > 0)
        {
            Transform old = baoRoot.GetChild(0);
            Game.PoolManager.CardPool.Despawn(old.gameObject);
        }
        //for(int i=0; i<handCardLayout.list.Count;i++)
        //{
        //    handCardLayout.list[i].reSetPoisiton -= handCardLayout.cardSelect;
        //    //handCardLayout.list[i].onSendMessage -= handCardLayout.cardPlay;
        //}
    }
    
	public MJPlayer NextPlayer
	{
		get
		{
			int nextIndex = index + 1;
			if (nextIndex == 4)
			{
				nextIndex = 0;
			}
			return Game.MJMgr.players[nextIndex];
		}
	}

	public MJPlayer PrevPlayer
	{
		get
		{
			int nextIndex = index - 1;
			if (nextIndex == -1)
			{
				nextIndex = 3;
			}
			return Game.MJMgr.players[nextIndex];
		}
	}

	public Vector3 DragCard(int card, bool isMy) //摸牌
	{
        Game.SoundManager.PlayGetCard();

        GameObject child = null;
        if (!isMy)
        {
            child = Game.PoolManager.CardPool.Spawn("Dragon_Blank");
        }
        else
        {
            child = Game.PoolManager.CardPool.Spawn(card.ToString());
        }

        if (null == child)
        {
            Debug.LogWarningFormat("没有找到牌模型 card：{0}", card);
        }

        Vector3 pos = handCardLayout.DragCard(card, child);
        EventDispatcher.DispatchEvent(MessageCommand.MJ_UpdatePlayPage);
        // TODO  摸的每张牌，上下可选择
        //child.GetComponent<MJEntity>().reSetPoisiton += handCardLayout.cardSelect;
        //child.GetComponent<MJEntity>().onSendMessage += handCardLayout.cardPlay;
        return pos;
    }

	public void Chi(GameOperChiArg chiArg)
	{
		int card = chiArg.targetCard;
		if(MJUtils.Chi() || MJUtils.TingChi())
        {
            Game.SocketGame.DoChi(chiArg.myCard1, chiArg.myCard2);
        }
	}

	public void Peng()
	{
        if (MJUtils.Peng()|| MJUtils.TingPeng())
        {
            int card = RoomMgr.actionNotify.pengArg;

            Game.SocketGame.DoPeng(card);
        }
    }

    public void AnGang()
    {
        if (MJUtils.AnGang())
        {
            int card = RoomMgr.actionNotify.gangList[0];
            Game.SocketGame.DoAnGang(card);
        }
    }

    public void BuGang()
    {
        if (MJUtils.BuGang())
        {
            int card = RoomMgr.actionNotify.gangList[0];
            Game.SocketGame.DoBuGang(card);
        }
    }

    public void ZhiGang()
    {
        if (MJUtils.ZhiGang())
        {
            int card = RoomMgr.actionNotify.gangList[0];
            Game.SocketGame.DoZhiGang(card);
        }
    }

    public void Ting()
    {
        if (MJUtils.Ting())
        {
            Game.SocketGame.DoTing();
        }
    }

    public void TingLiang(int card)
    {
        if(MJUtils.TingLiang())
        {
            Game.SocketGame.DoTingLiang(card);
        }
    }

    public void Hu()
	{
        if (MJUtils.Hu())
        {
            Game.SocketGame.DoHu();
        }
    }

    internal void Zhidui(int card)
    {
        if (MJUtils.TingZhidui())
        {
            Game.SocketGame.DoZhidui(card);
        }
    }
}
