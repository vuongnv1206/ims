namespace IMS.Contract.Common.Paging;

public class PagingResponseInfo
{
    public int ItemsPerPage { get; set; }
    public int CurrentPage { get; set; }
    public int ToTalPage
    {
        get
        {
            return (ToTalRecord / ItemsPerPage) + ((ToTalRecord % ItemsPerPage) == 0 ? 0 : 1);
        }
        set { }
    }
    public int ToTalRecord { get; set; }
}
