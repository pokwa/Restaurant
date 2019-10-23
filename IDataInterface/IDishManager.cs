using System;
using System.Collections.Generic;
using System.Text;

namespace IDataInterface
{
    public interface IDishManager
    {
        List<Dish> GetMenu();
        Dish GetDishByDishNumber(int dishNumber);
    }
}
