//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MarketOtomasyon.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Urun
    {
        public int id { get; set; }
        public string Ad { get; set; }
        public decimal Fiyat { get; set; }
        public short StokMiktari { get; set; }
        public Nullable<int> KategoriID { get; set; }
    
        public virtual Kategori Kategori { get; set; }
    }
}
