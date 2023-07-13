using HD.Station.QuanLyTinTuc.Abstractions.DTO;

namespace HD.Station.QuanLyTinTuc.Abstractions.Abstractions;

public interface ICommentsService
{
    public Task<MultipleCommentsResponse> GetComments(CommentsListQuery request);

}
