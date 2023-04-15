using System;

namespace ET
{
    public class C2Match_StartMatchHandler: AMActorRpcHandler<MatchInfoUnit, C2Match_StartMatch, Match2C_StartMatch>
    {
        protected override async ETTask Run(MatchInfoUnit matchInfoUnit, C2Match_StartMatch request, Match2C_StartMatch response, Action reply)
        {
            MatchComponent matchComponent = matchInfoUnit.DomainScene().GetComponent<MatchComponent>();
            // 将玩家放入匹配池，进行匹配
            matchComponent.AddMatchPool(matchInfoUnit.Id);
            reply();
            await ETTask.CompletedTask;
        }
    }
}