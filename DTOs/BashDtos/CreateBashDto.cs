using bach_bash.Models;

namespace bach_bash.DTOs.BashDtos;

public record CreateBashDto(String Title, Guid OwnerId);