using System.Collections;

public static class ScoreKeeper{
	public static int score = 0;
	public static int totalScore = 0;
	public static int numPlayers = 0; //0 = mouse only | 1 = moveMe only | 2 = bothPlayers | 3 = both Connect | 4 = Mayhem!;
	
	public static int getScore()
	{
		return score;
	}
	
	public static void updateScore(int pts)
	{
		score += pts;
	}
	
	private static void resetScore()
	{
		score = 0;
	}
	
	public static void moveScore()
	{
		totalScore += score;
		resetScore();
	}
	
	public static int getTotal()
	{
		return totalScore;
	}
	
	public static void setPlayers(int players)
	{
		numPlayers = players;
	}
	
	public static int getPlayers()
	{
		return numPlayers;
	}
}
