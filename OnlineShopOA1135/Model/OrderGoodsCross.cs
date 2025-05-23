﻿using System;
using System.Collections.Generic;

namespace OnlineShopOA1135.Model;

public partial class OrderGoodsCross
{
    public int OrderId { get; set; }

    public int GoodsId { get; set; }

    public int? Quantity { get; set; }

    public virtual Good Goods { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;
}
