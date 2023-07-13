using HD.Station.QuanLyTinTuc.Abstractions.DTO;

namespace HD.Station.QuanLyTinTuc.Abstractions.Abstractions;

public interface ICommentsService
{
    public Task<MultipleCommentsResponse> GetComments(CommentsListQuery request);
    public Task DeleteComment(DeleteCommentQuery request);
    public Task<SingleCommentResponse> AddNewComment(NewCommentQuery request);
}
