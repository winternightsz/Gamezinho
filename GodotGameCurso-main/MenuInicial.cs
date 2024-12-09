using Godot;

public partial class MenuInicial : Node2D
{
    private Button botaoIniciar;
    private Button botaoSair;

    public override void _Ready()
    {
        // Referências aos botões
        botaoIniciar = GetNode<Button>("Botoes/BotaoIniciar");
        botaoSair = GetNode<Button>("Botoes/BotaoSair");

        // Conecta os sinais dos botões
        botaoIniciar.Connect("pressed", new Callable(this, nameof(OnBotaoIniciarPressed)));
        botaoSair.Connect("pressed", new Callable(this, nameof(OnBotaoSairPressed)));
    }

    private void OnBotaoIniciarPressed()
    {
        GD.Print("Iniciando o jogo...");
        GetTree().ChangeSceneToFile("res://Mapa_1.tscn"); // Altere para o caminho correto da cena principal
    }

    private void OnBotaoSairPressed()
    {
        GD.Print("Saindo do jogo...");
        GetTree().Quit();
    }
}
