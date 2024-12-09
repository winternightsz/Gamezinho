using Godot;

public partial class FinalDoJogo : Node2D
{
    private Label mensagem;
    private Label pontuacao;
    private Button botaoReiniciar;
    private Button botaoSair;

    private const int PontosNecessariosParaPassar = 25; // Pontos mínimos para passar de ano

    public override void _Ready()
    {
        mensagem = GetNode<Label>("Mensagem");
        pontuacao = GetNode<Label>("Pontuacao");
        botaoReiniciar = GetNode<Button>("BotaoReiniciar");
        botaoSair = GetNode<Button>("BotaoSair");

        botaoReiniciar.Connect("pressed", new Callable(this, nameof(ReiniciarJogo)));
        botaoSair.Connect("pressed", new Callable(this, nameof(SairDoJogo)));

        MostrarResultado();
    }

    private void MostrarResultado()
    {
        // Obtém o jogo principal
        Jogo jogo = (Jogo)GetNode("/root/Jogo");
        int pontosTotais = jogo.pontuacaoTotal;

        // Atualiza a pontuação exibida
        pontuacao.Text = $"Pontuação Total: {pontosTotais}";

        // Verifica se passou de ano
        if (pontosTotais >= PontosNecessariosParaPassar)
        {
            mensagem.Text = "Parabéns, você passou de ano na faculdade!";
        }
        else
        {
            mensagem.Text = "Você não passou, tente novamente!";
        }
    }

private void ReiniciarJogo()
{
    GetTree().ChangeSceneToFile("res://CenarioPrincipal.tscn"); // Reinicia o jogo
}

private void SairDoJogo()
{
    GetTree().Quit(); // Encerra o jogo
}

}
