using System.ComponentModel.DataAnnotations;

namespace Sneaker_Shop_API.Dto;

public record CreateSneakerDto(
    [Required] string Model,
    [Required] string[] Colors,
    [Required] double Price,
    bool? InStock
);

public record UpdateSneakerDto(
    string? Model,
    string[]? Colors,
    double? Price,
    bool? InStock
);

public record ViewSneakerDto(
    int Id,
    string? Model,
    double? Price
);