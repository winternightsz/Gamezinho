using Godot;

public partial class MenuInicial : Node2D
{
    private Label titulo;
    private Label mensagem;
    private Button botaoComputacao;
    private Button botaoGeneral;
    private Button botaoSair;

    public override void _Ready()
    {
        // Referências aos botões
        titulo = GetNode<Label>("Titulo");
        mensagem = GetNode<Label>("Mensagem");
        botaoComputacao = GetNode<Button>("Botoes/BotaoComputacao");
        botaoGeneral = GetNode<Button>("Botoes/BotaoGeneral");
        botaoSair = GetNode<Button>("Botoes/BotaoSair");

        // Conecta os sinais dos botões
        botaoComputacao.Connect("pressed", new Callable(this, nameof(OnBotaoComputacaoPressed)));
        botaoGeneral.Connect("pressed", new Callable(this, nameof(OnBotaoGeralPressed)));
        botaoSair.Connect("pressed", new Callable(this, nameof(OnBotaoSairPressed)));

        ComecoJogo();
    }

    private void ComecoJogo(){
        titulo.Text = "TENSO";
        mensagem.Text = "Escolha um modo para jogar";
    }

    private void OnBotaoComputacaoPressed()
    {
        GD.Print("Modo Computação selecionado.");
        Jogo jogo = (Jogo)GetNode("/root/Jogo"); // Referência ao Autoload do jogo
        jogo.DefinirModo("computacao"); // Define o modo de jogo como Computação
        CarregarCenaPrincipal();
    }

    private void OnBotaoGeralPressed()
    {
        GD.Print("Modo General Knowledge selecionado.");
        Jogo jogo = (Jogo)GetNode("/root/Jogo"); // Referência ao Autoload do jogo
        jogo.DefinirModo("geral"); // Define o modo de jogo como General Knowledge
        CarregarCenaPrincipal();
    }

    private void CarregarCenaPrincipal()
    {
        GD.Print("Carregando cena principal...");
        GetTree().ChangeSceneToFile("res://mapa_1.tscn"); // Altere para o caminho correto da sua cena principal
    }

    private void OnBotaoSairPressed()
    {
        GD.Print("Saindo do jogo...");
        GetTree().Quit(); // Sai do jogo
    }
}
