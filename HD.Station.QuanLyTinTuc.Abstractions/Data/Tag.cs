namespace HD.Station.QuanLyTinTuc.Abstractions.Data;

public partial class Tag
{
    public int TagId { get; set; }

    public string? TagName { get; set; }

    public virtual ICollection<ArticleTag> ArticleTags { get; set; } = new List<ArticleTag>();
}
