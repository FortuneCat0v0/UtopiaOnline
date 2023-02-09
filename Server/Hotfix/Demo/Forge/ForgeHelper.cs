namespace ET
{
    [FriendClassAttribute(typeof(ET.ForgeComponent))]
    public static class ForgeHelper
    {
        public static void SyncAllProduction(Unit unit)
        {
            ForgeComponent forgeComponent = unit.GetComponent<ForgeComponent>();

            M2C_AllProductionList m2CAllProductionList = new M2C_AllProductionList();
            for (int i = 0; i < forgeComponent.ProductionsList.Count; i++)
            {
              m2CAllProductionList.ProductionProtoList.Add(forgeComponent.ProductionsList[i].ToMessage());   
            }
            MessageHelper.SendToClient(unit,m2CAllProductionList);
        }
    }
}