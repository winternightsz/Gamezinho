using Godot;
using System.Collections.Generic;
using System.Text.Json;

public partial class Jogo : Node
{
    [Signal]
    public delegate void EstadoAtualizadoEventHandler();

    public int diaAtual = 1; // Dia atual no jogo
    public int pontuacaoTotal = 0; // Pontuação acumulada do jogador
    public string modoAtual = ""; // Modo de jogo escolhido ("computacao" ou "general")

    public Dictionary<int, string> horario = new Dictionary<int, string>(); // Horário das matérias
    private Dictionary<string, object> dialogosAtuais; // Diálogos carregados para o dia atual
    private bool tutorialExibido = false;
    public override void _Ready()
    {
        GD.Print($"Jogo inicializado. Dia atual: {diaAtual}, Pontuação: {pontuacaoTotal}");
    }

    public void DefinirModo(string modo)
    {
        modoAtual = modo;
        GD.Print($"Modo de jogo definido: {modoAtual}");
        CarregarMaterias();
        CarregarDialogos(diaAtual); // Carrega os diálogos do primeiro dia
    }

    private void CarregarMaterias()
    {
        if (modoAtual == "computacao")
        {
            horario = GerarHorario(new List<string> { "Algoritmos", "Banco de Dados", "Redes", "IA", "Engenharia de Software" });
        }
        else if (modoAtual == "geral")
        {
            horario = GerarHorario(new List<string> { "História", "Geografia", "Matemática", "Ciências", "Filosofia" });
        }
        else
        {
            GD.PrintErr("Modo de jogo inválido!");
        }
    }

    private Dictionary<int, string> GerarHorario(List<string> materias)
    {
        var novoHorario = new Dictionary<int, string>();
        for (int dia = 1; dia <= 10; dia++)
        {
            novoHorario[dia] = materias[(dia - 1) % materias.Count];
        }
        return novoHorario;
    }



    public Dictionary<string, object> CarregarDialogos(int dia)
    {
        string caminho = $"res://assets/json/dialogos_{modoAtual}.json";

        try
        {
            using var arquivo = FileAccess.Open(caminho, FileAccess.ModeFlags.Read);
            string conteudo = arquivo.GetAsText();
            dialogosAtuais = JsonSerializer.Deserialize<Dictionary<string, object>>(conteudo);
            GD.Print($"Diálogos carregados para o dia {dia}: {caminho}");
            return dialogosAtuais;
        }
        catch (System.Exception e)
        {
            GD.PrintErr($"Erro ao carregar o arquivo: {caminho}. Detalhes: {e.Message}");
        }

        return null;
    }


    public string ObterDialogo(string categoria, string chave)
    {
        if (dialogosAtuais != null && dialogosAtuais.ContainsKey(categoria))
        {
            var categoriaDialogos = dialogosAtuais[categoria] as Dictionary<string, object>;
            if (categoriaDialogos != null && categoriaDialogos.ContainsKey(chave))
            {
                return categoriaDialogos[chave].ToString();
            }
        }
        GD.PrintErr($"Diálogo não encontrado: Categoria {categoria}, Chave {chave}");
        return "Texto não encontrado.";
    }

    public void MostrarDialogosNoTextbox(List<string> falas)
    {
        Node cenaAtual = GetTree().CurrentScene;
        Textbox textbox = cenaAtual.GetNode<Textbox>("Textbox");
        textbox.MostrarFalas(falas);
    }

    /// <summary>
    /// Carrega as falas do tutorial a partir de um arquivo JSON.
    /// </summary>
    public List<string> CarregarFalasTutorial()
    {
        string caminho = "res://assets/json/tutorial.json";

        try
        {
            using var arquivo = FileAccess.Open(caminho, FileAccess.ModeFlags.Read);
            string conteudo = arquivo.GetAsText();
            var dados = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(conteudo);

            if (dados != null && dados.ContainsKey(modoAtual))
            {
                return dados[modoAtual];
            }
        }
        catch (System.Exception e)
        {
            GD.PrintErr($"Erross ao carregar o tutorial: {e.Message}");
        }

        return new List<string> { "Erro ao carregar o tutorial." };
    }


    /// <summary>
    /// Mostra as falas do tutorial no Textbox.
    /// </summary>
    public void MostrarInstrucoesIniciais()
    {
        

        Node cenaAtual = GetTree().CurrentScene;
        Textbox textbox = cenaAtual.GetNode<Textbox>("Textbox");
        Player jogador = cenaAtual.GetNode<Player>("Player");
        if (tutorialExibido)
        {
            GD.Print("Tutorial já exibido, ignorando.");
            jogador.PermitirMovimento();
            return;
        }
        // Bloqueia o movimento do jogador enquanto o tutorial está ativo
        jogador.BloquearMovimento();

        // Carrega as falas do tutorial e exibe no Textbox
        var falas = CarregarFalasTutorial();
        textbox.MostrarFalas(falas);

        // Conecta o evento de término do diálogo para liberar o movimento
        if (!textbox.IsConnected("DialogoConcluido", new Callable(jogador, nameof(Player.PermitirMovimento))))
        {
            textbox.Connect("DialogoConcluido", new Callable(jogador, nameof(Player.PermitirMovimento)));
        }

        tutorialExibido = true;
    }


    public string ObterMateriaDoDia()
    {
        if (horario.ContainsKey(diaAtual))
        {
            return horario[diaAtual];
        }
        GD.PrintErr($"Dia {diaAtual} não encontrado no horário.");
        return "Nenhuma matéria";
    }

    public void AvancarDia()
    {
        if (diaAtual < 10)
        {
            diaAtual++;
            GD.Print($"Avançando para o dia {diaAtual}. Matéria: {ObterMateriaDoDia()}");
            CarregarDialogos(diaAtual); // Carrega os diálogos para o novo dia
            EmitSignal(nameof(EstadoAtualizado));
        }
        else
        {
            GD.Print("Jogo finalizado!");
            GetTree().ChangeSceneToFile("res://FinalDoJogo.tscn");
        }
    }

    public void AdicionarPontuacao(int pontos)
    {
        pontuacaoTotal += pontos;
        GD.Print($"Pontuação atual: {pontuacaoTotal}");
        EmitSignal(nameof(EstadoAtualizado));
    }
}
