using ET.EventType;

namespace ET
{
    public class MakeProdutionOverEvent_TaskUpdate : AEvent<MakeProdutionOver>
    {
        protected override void Run(MakeProdutionOver args)
        {
            args.Unit.GetComponent<TasksComponent>().TriggerTaskAction(TaskActionType.MakeItem,count:1,targetId : args.ProductionConfigId);
        }
    }
}