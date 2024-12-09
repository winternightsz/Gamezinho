using Godot;

public partial class Porta : Area2D
{
    [Export]
    public string nomeCenaQuiz = "res://Quiz.tscn";

    private void _on_Porta_body_entered(Node corpo)
    {
        if (corpo is Player)
        {
            GetTree().ChangeSceneToFile(nomeCenaQuiz);
        }
    }
	public void Abrir()
    {
        GD.Print("Entrando no quiz..." + nomeCenaQuiz);
        GetTree().ChangeSceneToFile(nomeCenaQuiz);
    }
}
