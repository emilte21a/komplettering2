using System;
using Raylib_cs;



public class WorldGeneration
{
    const int cellSize = 20;
    Random rand = new Random();
    public List<Rectangle> BlockList = new List<Rectangle>();

    public WorldGeneration()
    {
        GenerateWorld();
        GenerateScoreBoards();
    }

    public void GenerateWorld()
    {
        for (var i = 0; i < 5; i++)
        {
            BlockList.Add(new Rectangle(380 + cellSize * i, 540, cellSize, cellSize));
        }
        for (var i = 0; i < 5; i++)
        {
            BlockList.Add(new Rectangle(700 + cellSize * i, 540, cellSize, cellSize));
        }

        BlockList.Add(new Rectangle(0, 700, 1920, 40));
    }

    public void DrawWorld()
    {
        foreach (var block in BlockList)
        {
            Raylib.DrawRectangleRec(block, Color.BLACK);
        }
    }

    public bool IsColliding(Rectangle rect)
    {
        foreach (var block in BlockList)
        {
            if (Raylib.CheckCollisionRecs(rect, block) && rect.y + 20 > block.y)
            {
                return true;
            }
        }
        return false;
    }

    public bool IsCollidingSides(Rectangle rect)
    {
        foreach (var block in BlockList)
        {
            if (Raylib.CheckCollisionRecs(rect, block) &&
            (rect.x > block.x /*&& Raylib.IsKeyDown(KeyboardKey.KEY_A) && rect.y > block.y */||
            rect.x < block.x + cellSize /* && Raylib.IsKeyDown(KeyboardKey.KEY_D) && rect.y > block.y*/))
            {
                return true;
            }
        }
        return false;
    }

    public float GetFloorY(Rectangle rect)
    {
        float floorY = 100;

        foreach (var item in BlockList)
        {
            if (Raylib.CheckCollisionRecs(rect, item))
            {
                if (item.y > floorY)
                {
                    floorY = item.y;
                }
            }
        }

        return floorY;
    }

    public List<Rectangle> ScoreBoards = new List<Rectangle>();

    public void GenerateScoreBoards()
    {
        for (var i = 0; i < 3; i++)
        {
            ScoreBoards.Add(new Rectangle(400 + cellSize * rand.Next(0, 10) * i, 30, rand.Next(10, 30), rand.Next(10, 30)));
        }
    }

    public void DrawScoreBoards()
    {
        foreach (var item in ScoreBoards)
        {
            Raylib.DrawRectangleRec(item, Color.GOLD);
        }
    }

    public bool CheckScoreBoardCollisions(Rectangle rect)
    {
        foreach (var scoreboard in ScoreBoards)
        {
            if (Raylib.CheckCollisionRecs(scoreboard, rect))
            {
                return true;
            }
        }
        return false;
    }
}

