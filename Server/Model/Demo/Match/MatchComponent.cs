using System.Collections.Generic;
using MongoDB.Driver;

namespace ET
{
    public enum RoomState
    {
        Free,
        Game,
    }

    [ChildType(typeof (MatchInfoUnit))]
    [ComponentOf(typeof (Scene))]
    public class MatchComponent: Entity, IAwake, IDestroy
    {
        public List<long> MatchInfoUnitsList = new List<long>();
        public Dictionary<long, MatchInfoUnit> MatchInfoUnitsDict = new Dictionary<long, MatchInfoUnit>();
        public int needPlayers;
        public long Timer;
        public Dictionary<string, RoomState> roomDict = new Dictionary<string, RoomState>();
    }
}