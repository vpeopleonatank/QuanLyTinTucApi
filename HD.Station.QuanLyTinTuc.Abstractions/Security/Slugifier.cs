using HD.Station.QuanLyTinTuc.Abstractions.Abstractions;

using Slugify;

namespace HD.Station.QuanLyTinTuc.Abstractions.Security;

public class Slugifier : ISlugifier
{
    private readonly ISlugHelper _slugHelper;

    public Slugifier(ISlugHelper slugHelper)
    {
        _slugHelper = slugHelper;
    }

    public string Generate(string text)
    {
        return _slugHelper.GenerateSlug(text);
    }
}
