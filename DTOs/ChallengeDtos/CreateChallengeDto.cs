using bach_bash.Models;

namespace bach_bash.DTOs.ChallengeDtos;

public record CreateChallengeDto(Guid Id, String Title, String Description, Int32 Points);