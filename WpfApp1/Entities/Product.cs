//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WpfApp1.RulEntities1
{
    using System;
    using System.Collections.Generic;
    
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            this.Order = new HashSet<Order>();
        }
    
        public string ProductArticleNumber { get; set; }
        public int ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string ProductPhoto { get; set; }
        public int ProductCategory { get; set; }
        public int ProductManufacturer { get; set; }
        public decimal ProductCost { get; set; }
        public Nullable<byte> ProductDiscountAmount { get; set; }
        public int ProductQuantityInStock { get; set; }
        public string ProductStatus { get; set; }
        public int Unit { get; set; }
        public byte MaxDiscountAmount { get; set; }
        public int Suppliet { get; set; }
        public Nullable<int> CountinPack { get; set; }
        public Nullable<int> MinCount { get; set; }
    
        public virtual Manufactures Manufactures { get; set; }
        public virtual ProductName ProductName1 { get; set; }
        public virtual Suppliers Suppliers { get; set; }
        public virtual Unit Unit1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Order { get; set; }
        public string Background
        {
            get
            {
                if (this.ProductDiscountAmount > 15)
                    return "#7fff00";
                return "#fff";
            }
        }
        public string CostWithDiscount
        {
            get
            {
                if (this.MaxDiscountAmount > 0)
                {
                    var costWithDiscount = Convert.ToDouble(this.ProductCost) - Convert.ToDouble(this.ProductCost) * Convert.ToDouble(this.ProductDiscountAmount / 100.00);
                    return costWithDiscount.ToString();
                }
                return this.ProductCost.ToString();
            }
        }
        public string ImgPath
        {
            get
            {
                var path = "pack://application:,,,/Resources/" + this.ProductPhoto;
                return path;
            }
        }
    }
}
