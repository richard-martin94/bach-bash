namespace bach_bash.DTOs.ChallengeDtos;

public record CreateChallengeDto(String Title, String Description, Int32 Points, Guid BashId);