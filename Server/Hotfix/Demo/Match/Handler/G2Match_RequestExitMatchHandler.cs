using System;

namespace ET
{
    public class G2Match_RequestExitMatchHandler: AMActorRpcHandler<MatchInfoUnit, G2Match_RequestExitMatch, Match2G_RequestExitMatch>
    {
        protected override async ETTask Run(MatchInfoUnit unit, G2Match_RequestExitMatch request, Match2G_RequestExitMatch response, Action reply)
        {
            MatchComponent matchComponent = unit.DomainScene().GetComponent<MatchComponent>();
            matchComponent.Remove(unit.Id);
            reply();
            await ETTask.CompletedTask;
        }
    }
}