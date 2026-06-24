using bach_bash.Models;

namespace bach_bash.DTOs;

public record UpdateBashDto(String Title, Guid OwnerId, List<Challenge> Challenges);