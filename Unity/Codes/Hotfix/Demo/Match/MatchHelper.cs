using System;

namespace ET
{
    public static class MatchHelper
    {
        public static async ETTask<int> StartMatch(Scene ZonScene)
        {
            Match2C_StartMatch match2CStartMatch;
            try
            {
                match2CStartMatch =
                        (Match2C_StartMatch)await ZonScene.GetComponent<SessionComponent>().Session.Call(new C2Match_StartMatch());
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
                return ErrorCode.ERR_NetWorkError;
            }

            if (match2CStartMatch.Error != ErrorCode.ERR_Success)
            {
                return match2CStartMatch.Error;
            }

            return ErrorCode.ERR_Success;
        }
    }
}