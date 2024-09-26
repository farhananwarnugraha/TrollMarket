using System;

namespace TrollMarketWeb.ViewModels;

public class InvoiceViewModel
{
    public string  InvoiceNumber { get; set; }
    public string CustomerName { get; set; }
    public string InvoiceDate { get; set; }
    public List<InvoiceViewModel> Item { get; set; }
    
}
