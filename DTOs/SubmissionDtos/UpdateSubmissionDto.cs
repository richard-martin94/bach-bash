namespace bach_bash.DTOs.SubmissionDtos;

public record UpdateSubmissionDto(Guid BasherId, Guid ChallengeId, Int16 Place);