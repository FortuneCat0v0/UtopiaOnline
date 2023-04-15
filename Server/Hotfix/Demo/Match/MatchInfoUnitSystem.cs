namespace ET
{
    public class MatchInfoUnitSystem: DestroySystem<MatchInfoUnit>
    {
        public override void Destroy(MatchInfoUnit self)
        {
            self.GateSessionActorId = 0;
        }
    }
}