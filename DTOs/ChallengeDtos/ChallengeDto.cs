using bach_bash.Models;

namespace bach_bash.DTOs.ChallengeDtos;

public record ChallengeDto(Guid Id, Guid BashId, String Title, String Description, Int32 Points);