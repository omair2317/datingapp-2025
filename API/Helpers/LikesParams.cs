using System;

namespace API.Helpers;

public class LikesParams : PagingParams
{
    public required string MemberId { get; set; } = "";
    public required string Predicate { get; set; }   = "Liked";
}
