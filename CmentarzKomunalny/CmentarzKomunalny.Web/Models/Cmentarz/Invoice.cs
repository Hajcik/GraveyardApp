using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmentarzKomunalny.Web.Models.Cmentarz
{   // invoice = faktura
    public class Invoice : Order
    {
        public int Id { get; set; }
        public DateTime DateOfIssue { get; set; }   // data wystawienia
        public DateTime DateSale { get; set; }      // data sprzedazy
        public DateTime DateOfPayment { get; set; }
        public float PaymentBrutto { get; set; }
        // kwota burtto faktury nie zawsze będzie
        // tożsama z kwotą do zapaty np. gdy występowały zaliczki
        
        // jeśli klient chce rozlozyc oplaty na raty to proszony jest o kontakt z biurem bezpośrednio
    }
}
