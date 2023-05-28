using Raylib_cs;
public class Game
{
    const int screenWidth = 1280;
    const int screenHeight = 720;

    WorldGeneration world;
    Player player;
    public string CurrentScene;



    public Game()
    {
        Raylib.InitWindow(screenWidth, screenHeight, "boomba");
        Raylib.SetTargetFPS(60);

        world = new WorldGeneration();
        player = new Player();
        CurrentScene = "start";
    }

    public void Run()
    {
        while (!Raylib.WindowShouldClose())
        {
            Update();
            Draw();
        }

        Raylib.CloseWindow();
    }

    int score = 0;
    private void Update()
    {
        if (CurrentScene == "start")
        {
         
            player.playerRect.x = 640;
            player.playerRect.y = 360;
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
            {
                CurrentScene = "game";
                world.GenerateWorld();
                score = 0;
             
            }
        }

        else if (CurrentScene == "game")
        {
            score = player.ScoreGetter();
            player.playerRect.y = player.gravity(player.playerRect, 0.2f);
            world.IsCollidingSides(player.playerRect);
            world.IsColliding(player.playerRect);
            player.Action(15);
            player.playerRect.x = player.playerX(player.playerRect.x);
            if (score == 100)
            {
                CurrentScene = "win";
            }
        }
        else if (CurrentScene == "win")
        {
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
            {
                CurrentScene = "start";
                score = 0;
            }
        }
    }

    private void Draw()
    {
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.WHITE);

        if (CurrentScene == "start")
        {
            Raylib.DrawText("Shooting playground", 500, 500, 50, Color.ORANGE);
        }

        else if (CurrentScene == "game")
        {

            world.DrawWorld();
            Raylib.DrawRectangleRec(player.playerRect, Color.ORANGE);
            player.DrawBullets();
            world.DrawScoreBoards();
            Raylib.DrawText($"{score}", 18, 18, 50, Color.BLACK);
            Raylib.DrawText($"{score}", 20, 20, 50, Color.GOLD);
            Raylib.DrawText("Try shooting with left mouse click or P. Reach 100 to win", 302, 129, 20, Color.BEIGE);
            Raylib.DrawText("Try jumping with space bar or up-arrow key", 302, 149, 20, Color.BEIGE);
            Raylib.DrawText("Try moving around with WASD or left/right arrow keys", 302, 169, 20, Color.BEIGE);
        }
        else if (CurrentScene == "win")
        {
            Raylib.DrawText("You won!", 500, 500, 50, Color.GOLD);
        }
        Raylib.EndDrawing();
    }

}