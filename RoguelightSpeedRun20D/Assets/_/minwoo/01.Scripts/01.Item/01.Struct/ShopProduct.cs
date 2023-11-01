public struct ShopProduct
{
    int price;
    IProduct product;

    public ShopProduct(int price, IProduct product)
    {
        this.price = price;
        this.product = product;
    }

    public int Price { get => price; set => price = value; }
    public IProduct Product { get => product; set => product = value; }
}