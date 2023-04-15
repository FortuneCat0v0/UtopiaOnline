using System.Collections.Generic;

namespace ET
{
    public static class MatchHelper
    {
        public static void MatchSucceed(List<long> players)
        {
            // 通知客户端进入游戏
            foreach (long player in players)
            {
                MessageHelper.SendActor(player, new Match2C_NoticePlayerEnter());
            }

            // TODO 通知服务端传送玩家
        }

        public static void NoticePlayerWait(List<long> players, string str)
        {
            foreach (long player in players)
            {
                MessageHelper.SendActor(player, new Match2C_NoticePlayerWait() { Message = str });
            }
        }
    }
}