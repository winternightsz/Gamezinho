using Godot;
using System.Collections.Generic;
using System.Text.Json;

public partial class Quiz : Node2D
{
    private Label perguntaLabel;
    private Button opcao1;
    private Button opcao2;
    private Button opcao3;

    private int indicePerguntaAtual = 0;
    private int pontosGanhos = 0;

    private List<Dictionary<string, object>> perguntasAtuais = new List<Dictionary<string, object>>();

    public override void _Ready()
    {
        var layout = GetNode<Control>("Layout");
        perguntaLabel = layout.GetNode<Label>("PerguntaLabel");
        var respostas = layout.GetNode<VBoxContainer>("Respostas");

        opcao1 = respostas.GetNode<Button>("Opcao1");
        opcao2 = respostas.GetNode<Button>("Opcao2");
        opcao3 = respostas.GetNode<Button>("Opcao3");

        var botao = opcao1.GetNode<TextureButton>("TextureButton");
        botao.Connect("pressed", new Callable(this, nameof(OnBotaoPressed)));

        opcao1.Connect("pressed", new Callable(this, nameof(OnOpcao1)));
        opcao2.Connect("pressed", new Callable(this, nameof(OnOpcao2)));
        opcao3.Connect("pressed", new Callable(this, nameof(OnOpcao3)));
        var camera = GetViewport().GetCamera2D();
        if (camera != null)
            GD.Print($"Câmera ativa: {camera.Name}");
        else
            GD.Print("Nenhuma câmera ativa nesta cena!");

        respostas.CustomMinimumSize = new Vector2(200, 300);



        CarregarPerguntas();
    }
    private void OnBotaoPressed()
    {
        GD.Print("Botão pressionado!");
    }

    private void CarregarPerguntas()
    {
        Jogo jogo = (Jogo)GetNode("/root/Jogo");
        string caminho = $"res://assets/json/quiz_{jogo.modoAtual}.json";

        try
        {
            using var arquivo = FileAccess.Open(caminho, FileAccess.ModeFlags.Read);
            string conteudo = arquivo.GetAsText();
            var dados = JsonSerializer.Deserialize<Dictionary<string, List<Dictionary<string, object>>>>(conteudo);

            if (dados != null && dados.ContainsKey($"dia{jogo.diaAtual}"))
            {
                perguntasAtuais = dados[$"dia{jogo.diaAtual}"];
                CarregarPerguntaAtual();
            }
            else
            {
                GD.PrintErr($"Perguntas para dia {jogo.diaAtual} não encontradas no arquivo {caminho}.");
            }
        }
        catch (System.Exception e)
        {
            GD.PrintErr($"Erro ao carregar perguntas: {e.Message}");
        }
    }

    private void CarregarPerguntaAtual()
    {
        if (indicePerguntaAtual < perguntasAtuais.Count)
        {
            var pergunta = perguntasAtuais[indicePerguntaAtual];
            perguntaLabel.Text = pergunta["pergunta"].ToString();
            var respostas = (JsonElement)pergunta["respostas"];
            opcao1.Text = respostas[0].ToString();
            opcao2.Text = respostas[1].ToString();
            opcao3.Text = respostas[2].ToString();
        }
        else
        {
            FinalizarQuiz();
        }
    }

    private void VerificarResposta(int indiceEscolhido)
    {
        var pergunta = perguntasAtuais[indicePerguntaAtual];
        int respostaCorreta = int.Parse(pergunta["correta"].ToString());

        if (indiceEscolhido == respostaCorreta)
        {
            pontosGanhos++;
        }

        indicePerguntaAtual++;
        CarregarPerguntaAtual();
    }

    private void FinalizarQuiz()
    {
        GD.Print($"Quiz concluído! Pontos ganhos: {pontosGanhos}");

        Jogo jogo = (Jogo)GetNode("/root/Jogo");
        jogo.AdicionarPontuacao(pontosGanhos);

        GetTree().ChangeSceneToFile("res://mapa_1.tscn");
    }

    private void OnOpcao1()
    {
        VerificarResposta(0);
    }

    private void OnOpcao2()
    {
        VerificarResposta(1);
    }

    private void OnOpcao3()
    {
        VerificarResposta(2);
    }
}
