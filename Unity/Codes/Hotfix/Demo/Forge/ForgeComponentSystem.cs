using ET.EventType;

namespace ET
{
    [Timer(TimerType.MakeQueueOver)]
    public class MakeQueueOverTimer : ATimer<ForgeComponent>
    {
        public override void Run(ForgeComponent t)
        {
            MakeQueueOver.Instance.ZoneScene = t.ZoneScene();
            Game.EventSystem.PublishClass( MakeQueueOver.Instance);
        }
    }
   
    
     public class ForgeComponentAwakeSystem : AwakeSystem<ForgeComponent>
     {
         public override void Awake(ForgeComponent self)
         {
          
         }
     }
     
     public class ForgeComponentDestroySystem : DestroySystem<ForgeComponent>
     {
         public override void Destroy(ForgeComponent self)
         {
             
             foreach (var production in self.ProductionsList)
             {
                 production?.Dispose();
             }

             ForeachHelper.Foreach<long,long>(self.ProductionTimerDict, (key, value) =>
             {
                 TimerComponent.Instance?.Remove(ref value);
             });
         }
     }
     
    [FriendClassAttribute(typeof(ET.ForgeComponent))]
    [FriendClassAttribute(typeof(ET.Production))]
    public static class ForgeComponentSystem
    {
        public static bool IsExistMakeQueueOver(this ForgeComponent self)
        {
            bool isCanRecive = false;
    
            for (int i = 0; i < self.ProductionsList.Count; i++)
            {
                Production production = self.ProductionsList[i];
                if (production.IsMakingState() && production.IsMakeTimeOver())
                {
                    isCanRecive = true;
                    break;
                }
            }
            return isCanRecive;
        }
        
        
        public static void AddOrUpdateProductionQueue(this ForgeComponent self, ProductionProto productionProto)
        {
            Production production = self.GetProductionById(productionProto.Id);

            if (production == null)
            {
                production = self.AddChild<Production>();
                self.ProductionsList.Add(production);
            }
            production.FromMessage(productionProto);

            
            if (self.ProductionTimerDict.TryGetValue(production.Id,out long timeId))
            {
                TimerComponent.Instance.Remove(ref timeId);
                self.ProductionTimerDict.Remove(production.Id);
            }

           
            if ( production.IsMakingState() && !production.IsMakeTimeOver() )
            {
                Log.Debug($"启动一个定时器!!!!:{production.TargetTime}");
                timeId = TimerComponent.Instance.NewOnceTimer(production.TargetTime, TimerType.MakeQueueOver, self);
                self.ProductionTimerDict.Add(production.Id,timeId);
            }

            MakeQueueOver.Instance.ZoneScene = self.ZoneScene();
            Game.EventSystem.PublishClass( MakeQueueOver.Instance);
        }


        public static Production GetProductionById(this ForgeComponent self, long productionId)
        {
            for (int i = 0; i < self.ProductionsList.Count; i++)
            {
                if ( self.ProductionsList[i].Id == productionId )
                {
                    return self.ProductionsList[i];
                }
            }
            return null;
        }
        
        public static Production GetProductionByIndex(this ForgeComponent self,int index)
        {
            for (int i = 0; i < self.ProductionsList.Count; i++)
            {
                if ( index == i )
                {
                    return self.ProductionsList[i];
                }
            }
            return null;
        }

        public static int GetMakeingProductionQueueCount(this ForgeComponent self)
        {
            int count = 0;
            for (int i = 0; i < self.ProductionsList.Count; i++)
            {
                Production production = self.ProductionsList[i];
                if (production.ProductionState == (int)ProductionState.Making)
                {
                    ++count;
                }
            }
            return count;
        }


    }
}