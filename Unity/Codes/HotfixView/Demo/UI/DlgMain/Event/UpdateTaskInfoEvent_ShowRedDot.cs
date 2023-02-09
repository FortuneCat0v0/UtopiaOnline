using ET.EventType;

namespace ET
{
    public class UpdateTaskInfoEvent_ShowRedDot: AEvent<EventType.UpdateTaskInfo>
    {
        protected override void Run(UpdateTaskInfo args)
        {
            bool isExist = args.ZoneScene.GetComponent<TasksComponent>().IsExistTaskComplete();
            if (isExist)
            {
                RedDotHelper.ShowRedDotNode(args.ZoneScene, "Task");
            }
            else
            {
                if (RedDotHelper.IsLogicAlreadyShow(args.ZoneScene, "Task"))
                {
                    RedDotHelper.HideRedDotNode(args.ZoneScene, "Task");
                }
            }
            args.ZoneScene.GetComponent<UIComponent>()?.GetDlgLogic<DlgTask>()?.Refresh();
            
        }
    }
}