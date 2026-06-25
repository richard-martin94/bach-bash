namespace bach_bash.DTOs.ChallengeDtos;

public record ChallengeDto(Guid Id, String Title, String Description, Int32 Points, Guid BashId);