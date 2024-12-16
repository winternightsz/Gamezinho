using Godot;
using System;

public partial class Player : CharacterBody2D
{
    private Vector2 velocity;
    [Export]
    public AnimatedSprite2D animar;
    private bool podeMover = false;
    private RayCast2D rayCastInteracao;

    public override void _Ready()
    {
        // Referência ao RayCast2D no jogador
        //rayCastInteracao = GetNode<RayCast2D>("RayCastInteracao");
    }

    public void Animacao()
    {
        if (Input.IsActionPressed("ui_up"))
        {
            animar.Play("walk_back");
            //rayCastInteracao.TargetPosition = new Vector2(0, -16); // Aponta para cima
        }
        else if (Input.IsActionPressed("ui_down"))
        {
            animar.Play("walk_front");
            //rayCastInteracao.TargetPosition = new Vector2(0, 16); // Aponta para baixo
        }
        else if (Input.IsActionPressed("ui_right"))
        {
            animar.FlipH = true;
            animar.Play("walk_left");
            //rayCastInteracao.TargetPosition = new Vector2(16, 0); // Aponta para a direita
        }
        else if (Input.IsActionPressed("ui_left"))
        {
            animar.FlipH = false;
            animar.Play("walk_left");
            //rayCastInteracao.TargetPosition = new Vector2(-16, 0); // Aponta para a esquerda
        }
        else
        {
            animar.Play("default"); // Animação parada
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        velocity = Velocity;
        if (!podeMover)
        {
            Velocity = Vector2.Zero;
            return;
        }

        // Obtém a direção do input e aplica o movimento
        Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
        velocity = direction.Normalized() * 100;

        Velocity = velocity;
        Animacao();
        //VerificarInteracao();
        MoveAndSlide();
    }
    public void PermitirMovimento()
    {
        GD.Print("Movimento do jogador permitido.");
        podeMover = true;
    }

    public void BloquearMovimento()
    {
        podeMover = false;
    }
}
