using Microsoft.AspNetCore.Mvc.Rendering;
using TrollMarketWeb.ViewModels.User;

namespace TrollMarketWeb.ViewModels.History;

public class HistoryIndexViewModel
{
    public List<HistoryViewModel> HistoryTransactions { get; set; }
    public PaginationViewModel Pagination { get; set; }
    public string Seller { get; set; }
    public string Buyer { get; set; }
    public List<SelectListItem> Sellers { get; set; }
    public List<SelectListItem> Buyers { get; set; }
}
