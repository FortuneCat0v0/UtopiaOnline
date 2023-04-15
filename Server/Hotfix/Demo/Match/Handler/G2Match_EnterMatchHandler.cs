using System;

namespace ET
{
    [FriendClass(typeof (MatchInfoUnit))]
    public class G2Match_EnterMatchHandler: AMActorRpcHandler<Scene, G2Match_EnterMatch, Match2G_EnterMatch>
    {
        protected override async ETTask Run(Scene scene, G2Match_EnterMatch request, Match2G_EnterMatch response, Action reply)
        {
            MatchComponent matchComponent = scene.GetComponent<MatchComponent>();
            MatchInfoUnit matchInfoUnit = matchComponent.Get(request.UnitId);
            // 如果匹配服已经有玩家信息
            if (matchInfoUnit != null && !matchInfoUnit.IsDisposed)
            {
                matchInfoUnit.GateSessionActorId = request.GateSessionActorId;
                response.MatchInfoUnitInstanceId = matchInfoUnit.InstanceId;
                reply();
                return;
            }

            // 若没有则重新添加
            matchInfoUnit = matchComponent.AddChildWithId<MatchInfoUnit>(request.UnitId);
            matchInfoUnit.AddComponent<MailBoxComponent>();

            matchInfoUnit.GateSessionActorId = request.GateSessionActorId;
            response.MatchInfoUnitInstanceId = matchInfoUnit.InstanceId;
            matchComponent.Add(matchInfoUnit);

            reply();
            await ETTask.CompletedTask;
        }
    }
}