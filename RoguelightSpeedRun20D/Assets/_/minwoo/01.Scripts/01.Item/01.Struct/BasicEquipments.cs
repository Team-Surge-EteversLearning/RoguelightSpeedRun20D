public struct BasicEquipments
{
    int sellWhenClear;
    int price;
    int priceWeight;
    int modelIndex;
    EquipmentType type;

    public BasicEquipments(int sellWhenClear, int price, int priceWeight, EquipmentType type, int modelIndex)
    {
        this.sellWhenClear = sellWhenClear;
        this.price = price;
        this.priceWeight = priceWeight;
        this.type = type;
        this.modelIndex = modelIndex;
    }

    public int SellWhenClear { get => sellWhenClear; set => sellWhenClear = value; }
    public int Price { get => price; set => price = value; }
    public int PriceWeight { get => priceWeight; set => priceWeight = value; }
    public EquipmentType Type { get => type; set => type = value; }
    public int ModelIndex { get => modelIndex; set => modelIndex = value; }
}