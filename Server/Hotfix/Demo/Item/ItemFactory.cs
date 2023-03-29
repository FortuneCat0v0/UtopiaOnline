using System;

namespace ET
{
    [FriendClass(typeof(Item))]
    public static class ItemFactory
    {
        public static Item Create(Entity parent, int configId)
        {
            if ( !ItemConfigCategory.Instance.Contain(configId))
            {
                Log.Error($"当前所创建的物品id 不存在: {configId}");
                return null;
            }
            Item item = parent.AddChild<Item, int>(configId);
            item.RandomQuality();
            AddComponentByItemType(item);
            return item;
        }

        public static void AddComponentByItemType(Item item)
        {
            switch ((ItemType)item.Config.Type)
            {
                case ItemType.Weapon:
                {
                    item.AddComponent<EquipInfoComponent>();
                    break;
                }
                case ItemType.Armor:
                {
                    item.AddComponent<EquipInfoComponent>();
                    break;
                }
                case ItemType.Ring:
                {
                    item.AddComponent<EquipInfoComponent>();
                    break;
                }
                case ItemType.Prop:
                {
                    item.AddComponent<EquipInfoComponent>();
                     break;
                }
                case ItemType.Head:
                {
                    item.AddComponent<EquipInfoComponent>();
                    break;               
                }
                case ItemType.Clothes:
                {
                    item.AddComponent<EquipInfoComponent>();
                    break;                 
                }
                case ItemType.Shoes:
                {
                    item.AddComponent<EquipInfoComponent>();
                    break;
                }
                case ItemType.Shield:
                {
                    item.AddComponent<EquipInfoComponent>();
                    break;
                }
            }
        }

      
        
    }
}