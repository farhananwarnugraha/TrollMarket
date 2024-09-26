using System.Transactions;
using Microsoft.AspNetCore.Mvc.Rendering;
using TrollMarketBusiness;
using TrollMarketWeb.ViewModels.History;

namespace TrollMarketWeb;

public class HistoryService
{
    private readonly IOrderProduct _repository;

    public HistoryService(IOrderProduct repository)
    {
        _repository = repository;
    }

    public HistoryIndexViewModel Get(int pageNumber, int pageSize, string sellerName, string buyerName){
        var model = _repository.Get(pageNumber, pageSize, sellerName, buyerName)
            .Select(
                tarnsaction => new HistoryViewModel(){
                    OrderDate = tarnsaction.OrderDate?.ToString("dd/MM/yyyy"),
                    NameSeller = tarnsaction.Product.SellerUsernameNavigation?.FullName,
                    NameBauyer = tarnsaction.UsernameBuyerNavigation?.FullName,
                    ProductName = tarnsaction.Product.ProductName,
                    Quantity = tarnsaction.Quantity,
                    ShipperName = tarnsaction.Shipper?.ShipperName,
                    TotalPrice = tarnsaction.TotalPrice
                }
            );
        return new HistoryIndexViewModel(){
            HistoryTransactions = model.ToList(),
            Pagination = new PaginationViewModel(){
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRows = _repository.Count()
            },
            Sellers = _repository.GetSellers().Select(s=>new SelectListItem(){
                Text = s,
                Value = s
            }).ToList(),
            Buyers = _repository.GetSellers().Select(b=>new SelectListItem(){
                Text = b,
                Value = b
            }).ToList(),
            Seller = sellerName??"",
            Buyer = buyerName??""
        };
    }
}
