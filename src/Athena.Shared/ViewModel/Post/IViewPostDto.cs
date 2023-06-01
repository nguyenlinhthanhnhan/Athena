namespace Athena.Shared.ViewModel.Post;

public interface IViewPostDto
{
    long Id { get; set; }

    string? Title { get; set; }

    string? MetaTitle { get; set; }

    string? Slug { get; set; }

    string? Content { get; set; }

    string? ShortDescription { get; set; }

    string? ImageUrl { get; set; }

    string? ImageAlt { get; set; }
}