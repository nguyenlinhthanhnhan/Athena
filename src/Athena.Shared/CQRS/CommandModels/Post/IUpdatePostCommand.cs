﻿namespace Athena.Shared.CQRS.CommandModels.Post;

public interface IUpdatePostCommand
{
    long Id { get; set; }

    string? Title { get; set; }

    string? MetaTitle { get; set; }

    string? Slug { get; set; }

    string? Content { get; set; }

    string? ShortDescription { get; set; }

    string? ImageUrl { get; set; }

    string? ImageAlt { get; set; }

    bool IsPublished { get; set; }
}