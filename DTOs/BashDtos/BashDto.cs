using bach_bash.Models;

namespace bach_bash.DTOs;

public record BashDto(Guid Id, string Title, Guid OwnerId, List<Challenge> Challenges);