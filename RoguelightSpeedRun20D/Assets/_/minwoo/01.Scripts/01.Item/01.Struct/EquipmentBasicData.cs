
public struct EquipmentBasicData
{
    int sellWhenClear;
    int price;
    int priceWeight;
    EquipmentType type;

    public int SellWhenClear { get => sellWhenClear; set => sellWhenClear = value; }
    public int Price { get => price; set => price = value; }
    public int PriceWeight { get => priceWeight; set => priceWeight = value; }
    public EquipmentType Type { get => type; set => type = value; }
}
