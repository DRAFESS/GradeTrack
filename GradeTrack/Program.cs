Dictionary<string, Dictionary<string, List<float>>> NomesRegistrados = new Dictionary<string, Dictionary<string, List<float>>>();

void MenuInicial()
{
    Console.WriteLine("Terminal de notas de Matemática");
    Console.WriteLine("1. Registrar Aluno");
    Console.WriteLine("2. Validar Notas de Aluno");
    Console.Write("Escolha uma opção: ");

    string escolha = Console.ReadLine()!;

    switch (escolha)
    {
        case "1":
            RegistrarAluno();
            break;
        case "2":
            ValidarNotas();
            break;
        default:
            Console.WriteLine("Opção inválida.");
            break;
    }
}

void RegistrarAluno()
{
    Console.Write("Digite o nome do aluno que deseja registrar: ");
    string NomeAluno = Console.ReadLine()!;

    if (!NomesRegistrados.ContainsKey(NomeAluno))
    {
        // Inicializa o dicionário para armazenar as notas por bimestre
        var notasPorBimestre = new Dictionary<string, List<float>>();

        for (int i = 1; i <= 4; i++) // Assumindo 4 bimestres
        {
            Console.WriteLine($"Digite as notas do {i}º Bimestre (separe por vírgula): ");
            string[] notasInput = Console.ReadLine()!.Split(',');

            List<float> notas = new List<float>();
            foreach (string nota in notasInput)
            {
                if (float.TryParse(nota.Trim(), out float notaConvertida))
                {
                    notas.Add(notaConvertida);
                }
                else
                {
                    Console.WriteLine($"Nota inválida: {nota}. Ignorando...");
                }
            }

            notasPorBimestre.Add($"{i}º Bimestre", notas);
        }

        // Adiciona o aluno e suas notas ao dicionário principal
        NomesRegistrados.Add(NomeAluno, notasPorBimestre);

        Console.WriteLine($"Aluno {NomeAluno} registrado com sucesso!");
    }
    else
    {
        Console.WriteLine("Esse aluno já está registrado.");
    }

    Thread.Sleep(3000);
    Console.Clear();
    MenuInicial();
}

void ValidarNotas()
{
    Console.Write("Digite o nome do aluno que deseja validar as notas: ");
    string NomeAluno = Console.ReadLine()!;

    // Verifica se o aluno está registrado no dicionário
    if (NomesRegistrados.ContainsKey(NomeAluno))
    {
        // Exibe as notas do aluno
        Console.WriteLine($"Notas do aluno {NomeAluno}:");

        float somaTotal = 0;
        int quantidadeNotas = 0;

        // Percorre os bimestres e exibe as notas
        foreach (var bimestre in NomesRegistrados[NomeAluno])
        {
            Console.WriteLine($"{bimestre.Key}: {string.Join(", ", bimestre.Value)}");

            // Calcula a soma total das notas e a quantidade de notas
            somaTotal += bimestre.Value.Sum();
            quantidadeNotas += bimestre.Value.Count;
        }

        // Calcula a média
        float media = CalcularMedia(somaTotal, quantidadeNotas);
        Console.WriteLine($"Média final: {media:F2}");
    }
    else
    {
        Console.WriteLine("Aluno não registrado.");
    }

    Thread.Sleep(3000);
    Console.Clear();
    MenuInicial();
}

float CalcularMedia(float somaTotal, int quantidadeNotas)
{
    if (quantidadeNotas == 0) return 0;
    return somaTotal / quantidadeNotas;
}

MenuInicial();
