public struct ShopProduct
{
    int price;
    IProduct product;

    public ShopProduct(IProduct product, int price = 0)
    {
        this.price = price;
        this.product = product;
    }

    public int Price { get => price; set => price = value; }
    public IProduct Product { get => product; set => product = value; }
}