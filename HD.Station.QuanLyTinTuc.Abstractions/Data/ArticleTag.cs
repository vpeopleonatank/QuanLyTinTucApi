namespace HD.Station.QuanLyTinTuc.Abstractions.Data;

public partial class ArticleTag
{
    public int ArticleTagId { get; set; }

    public int? ArticleId { get; set; }

    public int? TagId { get; set; }

    public virtual required Article Article { get; set; }

    public virtual required Tag Tag { get; set; }
}
