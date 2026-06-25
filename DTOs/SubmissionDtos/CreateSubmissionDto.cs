namespace bach_bash.DTOs.SubmissionDtos;

public record CreateSubmissionDto(Guid BasherId, Guid ChallengeId, Int16 Place);