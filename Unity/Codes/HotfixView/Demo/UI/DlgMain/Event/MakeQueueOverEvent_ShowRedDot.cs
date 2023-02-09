using ET.EventType;

namespace ET
{
    
    public class MakeQueueOverEvent_ShowRedDot : AEventClass<EventType.MakeQueueOver>
    {

        protected override void Run(object a)
        {
            EventType.MakeQueueOver args = a as EventType.MakeQueueOver;
            bool isExist = args.ZoneScene.GetComponent<ForgeComponent>().IsExistMakeQueueOver();
            if (isExist)
            {
                RedDotHelper.ShowRedDotNode(args.ZoneScene, "Forge");
            }
            else
            {
                if (RedDotHelper.IsLogicAlreadyShow(args.ZoneScene, "Forge"))
                {
                    RedDotHelper.HideRedDotNode(args.ZoneScene, "Forge");
                }
            }
        }
    }
}