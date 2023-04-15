using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
	public static  class DlgMainSystem
	{
		public static void RegisterUIEvent(this DlgMain self)
		{
			self.View.E_RoleButton.AddListenerAsync(self.OnRoleButtonClickHandler);
			self.View.E_BattleButton.AddListener(self.OnBattleButtonClickHandler);
			self.View.E_BagButton.AddListener(self.OnBagButtonClickHandler);
			self.View.E_MakeButton.AddListener(self.OnMakeButtonClickHandler);
			self.View.E_TaskButton.AddListener(self.OnTaskButtonClickHandler);
			self.View.E_RankButton.AddListener(self.OnRankButtonClickHandler);
			self.View.E_MatchButton.AddListenerAsync(()=>
			{
				return self.OnMatchButtonClickHandler();
			});
			self.View.E_ChatButton.AddListener(self.OnChatButtonClickHandler);
			
			RedDotHelper.AddRedDotNodeView(self.ZoneScene(), "Role", self.View.E_RoleButton.gameObject, Vector3.one, new Vector3(75,55,0));
			RedDotHelper.AddRedDotNodeView(self.ZoneScene(), "Forge", self.View.E_MakeButton.gameObject, Vector3.one, new Vector3(75,55,0));
			RedDotHelper.AddRedDotNodeView(self.ZoneScene(), "Task", self.View.E_TaskButton.gameObject, Vector3.one, new Vector3(75,55,0));
		}

		public static void OnUnLoadWindow(this DlgMain self)
		{
			RedDotMonoView redDotMonoView = self.View.E_RoleButton.GetComponent<RedDotMonoView>();
			RedDotHelper.RemoveRedDotView(self.ZoneScene(),"Role",out redDotMonoView);
			
			redDotMonoView = self.View.E_MakeButton.GetComponent<RedDotMonoView>();
			RedDotHelper.RemoveRedDotView(self.ZoneScene(),"Forge",out redDotMonoView);
			
			redDotMonoView = self.View.E_TaskButton.GetComponent<RedDotMonoView>();
			RedDotHelper.RemoveRedDotView(self.ZoneScene(),"Task",out redDotMonoView);
		}
		
		
		public static void ShowWindow(this DlgMain self, Entity contextData = null)
		{
			self.Refresh().Coroutine();
			
			
		}

		public static async ETTask Refresh(this DlgMain self)
		{
			Unit unit = UnitHelper.GetMyUnitFromCurrentScene(self.ZoneScene().CurrentScene());
			NumericComponent numericComponent = unit.GetComponent<NumericComponent>();
			
			self.View.E_RoleLevelText.SetText($"Lv.{numericComponent.GetAsInt((int)NumericType.Level)}");
			self.View.E_GoldText.SetText(numericComponent.GetAsInt((int)NumericType.Gold).ToString());
			self.View.E_ExpText.SetText(numericComponent.GetAsInt((int)NumericType.Exp).ToString());

			await ETTask.CompletedTask;
		}
		
		public static async ETTask OnRoleButtonClickHandler(this DlgMain self)
		{
			try
			{
				await self.ZoneScene().GetComponent<UIComponent>().ShowWindowAsync(WindowID.WindowID_RoleInfo);
			}
			catch (Exception e)
			{
				Log.Error(e.ToString());
			}
		}
		
		public static  void OnBattleButtonClickHandler(this DlgMain self)
		{
			self.ZoneScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_Adventure);
		}
		
		public static  void OnBagButtonClickHandler(this DlgMain self)
		{
			self.ZoneScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_Bag);
		}
		
		public static  void OnMakeButtonClickHandler(this DlgMain self)
		{
			self.ZoneScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_Forge);
		}
		
		public static  void OnTaskButtonClickHandler(this DlgMain self)
		{
			self.ZoneScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_Task);
		}

		public static void OnRankButtonClickHandler(this DlgMain self)
		{
			self.ZoneScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_Rank);
		}

		public static async ETTask OnMatchButtonClickHandler(this DlgMain self)
		{
			// TODO 显示匹配UI
			try
			{
				int errorCode = await MatchHelper.StartMatch(self.ZoneScene());
				if (errorCode != ErrorCode.ERR_Success)
				{
					Log.Error(errorCode.ToString());
					return;
				}
			}
			catch (Exception e)
			{
				Log.Error(e.ToString());
			}
		}
		
		public static void OnChatButtonClickHandler(this DlgMain self)
		{
			self.ZoneScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_Chat);
		}
	}
}
