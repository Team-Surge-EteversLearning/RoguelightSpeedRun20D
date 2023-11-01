using System.Collections.Generic;

public interface IProductMaker
{
    public List<ShopProduct> Make(string info);
}