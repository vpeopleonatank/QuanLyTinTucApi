- Scaffold model class and dbcontext from database
```
dotnet ef dbcontext scaffold "Server=localhost;Database=QuanLyTinTuc;User Id=SA;Password=Sql_password;Trusted_Connection=False;TrustServerCertificate=True;MultipleActiveResultSets=true" Microsoft.EntityFrameworkCore.SqlServer --context QuanLyTinTucDbContext --output-dir ../HD.Station.QuanLyTinTuc.Abstractions/Data --context-dir ../HD.Station.QuanLyTinTuc.SqlServer/Stores
```
