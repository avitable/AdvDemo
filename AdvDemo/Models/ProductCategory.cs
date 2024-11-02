using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace AdvDemo.Models;

public partial class ProductCategory
{
    public int ProductCategoryId { get; set; }

    public int? ParentProductCategoryId { get; set; }

    public string Name { get; set; } = null!;

    [DisplayName("Row GUID")]
    public Guid Rowguid { get; set; }

    public DateTime ModifiedDate { get; set; }

    //public virtual ICollection<ProductCategory> InverseParentProductCategory { get; set; } = new List<ProductCategory>();

    public virtual ProductCategory? ParentProductCategory { get; set; }
}
