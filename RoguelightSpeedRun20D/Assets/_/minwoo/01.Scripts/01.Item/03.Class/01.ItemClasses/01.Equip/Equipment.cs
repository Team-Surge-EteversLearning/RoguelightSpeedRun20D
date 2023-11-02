public abstract class Equipment : IProduct
{
    protected string name;
    protected int sellWhenClear;
    protected EquipmentType type;

    public void Buy()
    {
        throw new System.NotImplementedException();
    }
}