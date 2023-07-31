# Scaffold model class and dbcontext from database
```
dotnet ef dbcontext scaffold "Server=localhost;Database=QuanLyTinTuc;User Id=SA;Password=Sql_password;Trusted_Connection=False;TrustServerCertificate=True;MultipleActiveResultSets=true" Microsoft.EntityFrameworkCore.SqlServer --context QuanLyTinTucDbContext --output-dir ../HD.Station.QuanLyTinTuc.Abstractions/Data --context-dir ../HD.Station.QuanLyTinTuc.SqlServer/Stores
```
# Use user-secret to manage settings
- On windows, use below commands to override `appsettings.json`:
```
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:QLTTContext" "Data Source=(localdb)\mssqllocaldb;Database=QuanLyTinTuc;Trusted_Connection=True;MultipleActiveResultSets=true;" --project .\HD.Station.QuanLyTinTuc.Demo\
```