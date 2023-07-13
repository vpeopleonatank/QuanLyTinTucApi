namespace HD.Station.QuanLyTinTuc.Abstractions.Abstractions;

public interface IAuditableEntity
{
    DateTime CreatedAt { get; set; }

    DateTime UpdatedAt { get; set; }
}
