namespace ET
{
    
    public class SceneChangeFinish_ShowCurrentSceneUI: AEventAsync<EventType.SceneChangeFinish>
    {
        protected override async ETTask Run(EventType.SceneChangeFinish args)
        {
            args.ZoneScene.GetComponent<UIComponent>().CloseWindow(WindowID.WindowID_Loading);
            args.ZoneScene.GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_Main);
            await ETTask.CompletedTask;
        }
    }
    
}