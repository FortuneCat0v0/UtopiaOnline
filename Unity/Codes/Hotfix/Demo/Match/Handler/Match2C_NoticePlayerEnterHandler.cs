namespace ET
{
    public class Match2C_NoticePlayerEnterHandler: AMHandler<Match2C_NoticePlayerEnter>
    {
        protected override void Run(Session session, Match2C_NoticePlayerEnter message)
        {
            // TODO 场景转换逻辑
            Log.Debug("进入游戏");
        }
    }
}