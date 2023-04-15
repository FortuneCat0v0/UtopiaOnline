using System.Collections.Generic;

namespace ET
{
    public class MatchComponentAwakeSystem: AwakeSystem<MatchComponent>
    {
        public override void Awake(MatchComponent self)
        {
            // TODO 这里的几个初始数据要写几个配置表
            self.roomDict.Add("Room1", RoomState.Free);
            self.roomDict.Add("Room2", RoomState.Free);
            self.needPlayers = 3;
            self.Timer = TimerComponent.Instance.NewRepeatedTimer(2000, TimerType.MatchRound, self);
        }
    }

    public class MatchComponentDestroySystem: DestroySystem<MatchComponent>
    {
        public override void Destroy(MatchComponent self)
        {
            foreach (var matchInfoUnit in self.MatchInfoUnitsDict.Values)
            {
                TimerComponent.Instance.Remove(ref self.Timer);
                matchInfoUnit?.Dispose();
            }
        }
    }

    [Timer(TimerType.MatchRound)]
    public class MatchComponentTimer: ATimer<MatchComponent>
    {
        public override void Run(MatchComponent self)
        {
            self.MatchProcess();
        }
    }

    [FriendClass(typeof (MatchInfoUnit))]
    [FriendClass(typeof (MatchComponent))]
    public static class MatchComponentSystem
    {
        public static void Add(this MatchComponent self, MatchInfoUnit matchInfoUnit)
        {
            if (self.MatchInfoUnitsDict.ContainsKey(matchInfoUnit.Id))
            {
                Log.Error($"matchInfoUnit is exist!:{matchInfoUnit.Id}");
                return;
            }

            self.MatchInfoUnitsDict.Add(matchInfoUnit.Id, matchInfoUnit);
        }

        public static MatchInfoUnit Get(this MatchComponent self, long id)
        {
            self.MatchInfoUnitsDict.TryGetValue(id, out MatchInfoUnit matchInfoUnit);
            return matchInfoUnit;
        }

        public static void Remove(this MatchComponent self, long id)
        {
            // TODO 要预防选取到玩家但是还没传送，玩家就选择退出了
            if (self.MatchInfoUnitsDict.TryGetValue(id, out MatchInfoUnit matchInfoUnit))
            {
                self.RemoveMatchPool(id);
                self.MatchInfoUnitsDict.Remove(id);
                matchInfoUnit?.Dispose();
            }
        }

        /// <summary>
        /// 将玩家放入匹配池进行匹配
        /// </summary>
        /// <param name="self"></param>
        /// <param name="id"></param>
        public static void AddMatchPool(this MatchComponent self, long id)
        {
            if (self.Get(id) != null)
            {
                self.MatchInfoUnitsList.Add(id);
            }
            else
            {
                Log.Debug("匹配服务器不存在该玩家的数据");
            }
        }

        public static void RemoveMatchPool(this MatchComponent self, long id)
        {
            if (self.MatchInfoUnitsList.Remove(id))
            {
                Log.Debug("成功从匹配池移除");
            }
        }

        /// <summary>
        /// 匹配程序,每隔一段时间执行
        /// </summary>
        /// <param name="self"></param>
        public static void MatchProcess(this MatchComponent self)
        {
            Log.Debug("开始匹配进程!!!!!!!!!!!!!!!!!!!!!!!");
            bool haveRoom = false;
            // 选取空闲房间
            string roomName = null;
            foreach (var item in self.roomDict)
            {
                if (item.Value == RoomState.Free)
                {
                    roomName = item.Key;
                    haveRoom = true;
                    break;
                }
            }

            // 按先入先选，选择玩家
            if (haveRoom)
            {
                List<long> players = new List<long>();
                // 匹配池玩家数量足够，匹配成功!!!
                if (self.MatchInfoUnitsList.Count >= self.needPlayers)
                {
                    // 将玩家从匹配队列添加到列表
                    for (int i = 0; i < self.needPlayers; i++)
                    {
                        players.Add(self.Get(self.MatchInfoUnitsList[0]).GateSessionActorId);
                        self.MatchInfoUnitsList.RemoveAt(0);
                    }

                    self.roomDict[roomName] = RoomState.Game;

                    // 匹配成功
                    MatchHelper.MatchSucceed(players);
                    return;
                }

                foreach (long l in self.MatchInfoUnitsList)
                {
                    players.Add(self.Get(l).GateSessionActorId);
                }

                // 匹配池玩家数量不足，通知玩家等待
                MatchHelper.NoticePlayerWait(players, "玩家数量不足，请等待");
                Log.Debug("Players的长度：" + players.Count);
            }
            else
            {
                List<long> players = new List<long>();
                foreach (long l in self.MatchInfoUnitsList)
                {
                    players.Add(self.Get(l).GateSessionActorId);
                }

                // 通知匹配池内的玩家请等待
                MatchHelper.NoticePlayerWait(players, "游戏房间空缺，请等待");
                Log.Debug("玩家长度：" + players.Count);
            }
        }
    }
}