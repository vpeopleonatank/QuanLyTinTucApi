using HD.Station.QuanLyTinTuc.Abstractions.Data;
using HD.Station.QuanLyTinTuc.Abstractions.Stores;
using HD.Station.QuanLyTinTuc.Abstractions.Abstractions;

namespace HD.Station.QuanLyTinTuc.Abstractions.Security;

public class CurrentUser : ICurrentUser
{
    // private readonly IUserManagement _userManagement;

    private readonly INewsStore _newsStore;

    public User? User { get; private set; }

    public CurrentUser(INewsStore newsStore)
    {
      _newsStore = newsStore;
    }

    public async Task SetIdentifier(long identifier)
    {
        User = await _newsStore.GetUserById(identifier);
    }
}
