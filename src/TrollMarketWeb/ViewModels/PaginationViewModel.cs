namespace TrollMarketWeb;

public class PaginationViewModel
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalRows { get; set; }
    public int TotalPage { 
        get{
            return (int)Math.Ceiling((double)TotalRows/PageSize);
        } 
    }
}

