namespace bach_bash.DTOs.SubmissionDtos;

public record UpdateSubmissionDto(Guid BasherId, Guid ChallengeId, short Place);