using Raylib_cs;
using System;
using System.Numerics;
using System.Collections.Generic;
public class Player
{

    WorldGeneration world = new();
    public Rectangle playerRect = new Rectangle(640, 360, 20, 20);

    public Vector2 GravityVector = new Vector2(0, 0);

    public float speed = 4;

    public float playerX(float playerxpos)
    {
        if (Raylib.IsKeyDown(KeyboardKey.KEY_D) || Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT))
        {
            playerxpos += speed;
        }
        else if (Raylib.IsKeyDown(KeyboardKey.KEY_A) || Raylib.IsKeyDown(KeyboardKey.KEY_LEFT))
        {
            playerxpos -= speed;
        }
        return playerxpos;
    }



    public float gravity(Rectangle rect, float fallingSpeed)
    {
        if (GravityVector.Y > 9)
        {
            GravityVector.Y = 9;
        }
        if (GravityVector.Y < -9)
        {
            GravityVector.Y = -9;
        }

        if (world.IsColliding(rect))
        {
            GravityVector.Y = 0;
            rect.y = world.GetFloorY(rect) - rect.height;
        }
        else
        {
            GravityVector.Y += fallingSpeed;
            rect.y += GravityVector.Y;
        }

        if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE) || Raylib.IsKeyPressed(KeyboardKey.KEY_UP))
        {
            GravityVector.Y = -8;
        }

        return rect.y;
    }


    List<Bullet> bullets = new List<Bullet>();
    Vector2 playerPos;
    Vector2 bulletDirection;

    public void Action(int bulletSpeed)
    {
        Vector2 mousePos = Raylib.GetMousePosition();
        Vector2 diff = mousePos - new Vector2(playerRect.x, playerRect.y);
        bulletDirection = Vector2.Normalize(diff);

        if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON) || Raylib.IsKeyDown(KeyboardKey.KEY_R))
        {
            bullets.Add(new Bullet(new Vector2(playerRect.x, playerRect.y), bulletDirection, bulletSpeed));
        }

        List<Bullet> bulletsToRemove = new List<Bullet>();

        foreach (var bullet in bullets)
        {
            bullet.Update();
            if (world.IsColliding(bullet.Bounds))
            {
                bulletsToRemove.Add(bullet);
            }
            
        }

        //Ta bort de bullets som ska tas bort
        foreach (var bulletToRemove in bulletsToRemove)
        {
            bullets.Remove(bulletToRemove);
        }
    }

    public void DrawBullets()
    {
        foreach (var bullet in bullets)
        {
            bullet.Draw();
        }
    }

    int score;
    public int ScoreGetter(){
        foreach (var bullet in bullets)
        {
            if (world.CheckScoreBoardCollisions(bullet.Bounds))
            {
                score++;
            }
        }
        return score;
    }
}

public class Bullet
{
    Vector2 direction;
    int speed;
    Rectangle bulletRect;
    public Vector2 Position { get; private set; }
    public ref Rectangle Bounds => ref bulletRect;

    public Bullet(Vector2 position, Vector2 direction, int speed)
    {
        Position = position;
        this.direction = direction;
        this.speed = speed;
        bulletRect = new Rectangle(position.X, position.Y, 5, 5);
    }

    public void Update()
    {
        Position += direction * speed;
        bulletRect.x = Position.X;
        bulletRect.y = Position.Y;
    }
    public void Draw()
    {
        Raylib.DrawRectangleRec(bulletRect, Color.RED);
    }
}