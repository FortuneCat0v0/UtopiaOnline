using System;

namespace ET
{
    [FriendClassAttribute(typeof(ET.RankInfosComponent))]
    public class C2Rank_GetRanksInfoHandler : AMActorRpcHandler<Scene, C2Rank_GetRanksInfo, Rank2C_GetRanksInfo>
    {
        protected override async ETTask Run(Scene scene, C2Rank_GetRanksInfo request, Rank2C_GetRanksInfo response, Action reply)
        {
            RankInfosComponent rankInfosComponent = scene.GetComponent<RankInfosComponent>();
            int count = 0;
            foreach (var rankInfo in rankInfosComponent.SortedRankInfoList)
            {
                response.RankInfoProtoList.Add(rankInfo.Key.ToMessage());
                ++count;
                if (count >= 100)
                {
                    break;
                }
            }
            
            reply();
            await ETTask.CompletedTask;
        }
    }
}