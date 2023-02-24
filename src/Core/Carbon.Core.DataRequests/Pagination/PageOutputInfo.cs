namespace Carbon.Core.DataRequests.Pagination;

public record PageOutputInfo(
    int PageNumber,
    int PageSize,
    int TotalCount,
    int TotalPages,
    bool HasPreviousPage,
    bool HasNextPage);