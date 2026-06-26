namespace bach_bash.DTOs.SubmissionDtos;

public record CreateSubmissionDto(Guid BasherId, Guid ChallengeId, short Place);