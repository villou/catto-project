namespace catto.DTO;

public class ScoreDto
{
  public int Score { get; set; }
}

public class ScoreIndexDto
{
  public int Score { get; set; }
  public string? Username { get; set; }
  public string? Avatar { get; set; }
}