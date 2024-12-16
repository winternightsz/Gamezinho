using Godot;
using System;

public partial class Area : Area2D
{
    [Export]
    public string cenaDestino = "res://quiz.tscn";

    public override void _Ready()
    {
        Connect("body_entered", new Callable(this, nameof(EntrouArea)));
        Connect("body_exited", new Callable(this, nameof(SaiuArea)));
    }

    private void EntrouArea(Node2D corpo)
    {
        if (corpo is Player)
        {
            GD.Print($"Player entrou na área! Cena de destino: {cenaDestino}");
            GetTree().ChangeSceneToFile(cenaDestino);
        }
    }

    private void SaiuArea(Node2D corpo)
    {
        if (corpo is Player)
        {
            GD.Print("Player saiu da área.");
        }
    }
}
