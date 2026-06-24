using bach_bash.Models;

namespace bach_bash.DTOs.SubmissionDtos;

public record UpdateSubmissionDto(Guid BasherId, Guid ChallengeId);