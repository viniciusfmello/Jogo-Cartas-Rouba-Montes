﻿using System.IO;
class Program
{
    static Cartas cartas = new Cartas();
    static List<Cartas> cartasBaralho = new List<Cartas>();
    static List<Jogador> ListaJogadores = new List<Jogador>();
    static List<Cartas> ListaDeCartasAreaDescarte = new List<Cartas>();

    static StreamWriter escritorArquivo = new StreamWriter("Logs.txt", true);
    static void Main()
    {
        criarRegras();

        while (true)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\nBEM VINDO AO JOGO ROUBA MONTES ");
            Console.ResetColor();
            Console.WriteLine("\nVocê já conhece esse jogo?");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("1) Sim");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("2) Não");
            Console.ResetColor();
            try
            {
                int opcao = int.Parse(Console.ReadLine());
                switch (opcao)
                {
                    case 1:
                        Console.WriteLine("\nBeleza, vamos aos próximo passo.");
                        QuestionarioIniciarJogo();
                        break;
                    case 2:
                        Console.WriteLine("\nNão se preocupe!! Temos um arquivo de texto com todas as regras do jogo disponibilizados na pasta, dê uma olhada!");
                        int preparado = 0;
                        Console.WriteLine("Quando estiverem prontos, digite 1: ");
                        while (preparado != 1)
                        {
                            preparado = int.Parse(Console.ReadLine());
                            if (preparado == 1)
                            {
                                Console.WriteLine("Bora pra próxima etapa!");
                                QuestionarioIniciarJogo();
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("\n**** VOCÊ DIGITOU UM VALOR INVÁLIDO ****\n");
                                Console.ResetColor();
                            }
                        }
                        break;
                }
                break;
            }
            catch (FormatException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n**** ERRO!! NOSSO MENU FUNCIONA APENAS COM NÚMEROS ****\n");
                Console.ResetColor();
            }
        }
    }
    static void QuestionarioIniciarJogo()
    {
        int quantJogadores;
        Console.WriteLine("\nVocê deseja iniciar uma partida?");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\n1)Sim");
        Console.ResetColor();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("2)Não");
        Console.ResetColor();
        try
        {
            int iniciar = int.Parse(Console.ReadLine());
            switch (iniciar)
            {
                case 1:
                    while (true)
                    {
                        Console.WriteLine("\nQuantos jogadores vão jogar o jogo?");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Mínimo 2 - Máximo 8");
                        Console.ResetColor();
                        while (true)
                        {
                            quantJogadores = int.Parse(Console.ReadLine());
                            if (quantJogadores < 2 || quantJogadores > 8)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("\n****É NECESSÁRIO NO MÍNIMO 2 JOGADORES E NO MÁXIMO 8****");
                                Console.ResetColor();
                            }
                            else
                            {
                                Partida partida = new Partida();
                                break;
                            }
                        }
                        int quantBaralhos;
                        Console.WriteLine("\nQuantos baralhos serão usados no jogo?");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Mínimo 1 baralho (54 cartas)");
                        Console.ResetColor();
                        while (true)
                        {
                            quantBaralhos = int.Parse(Console.ReadLine());
                            if (quantBaralhos < 1)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("\n****É NECESSÁRIO NO MÍNIMO 1 BARALHO****");
                                Console.ResetColor();
                            }
                            else
                            {
                                CadastrarJogadores(quantJogadores);
                                IniciarPartida(quantBaralhos, quantJogadores);

                                break;
                            }
                        }
                        break;
                    }
                    break;
                case 2:

                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n****OPÇÃO INVÁLIDA****");
                    Console.ResetColor();
                    break;

            }
        }
        catch (FormatException)
        {
            Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n**** ERRO!! NOSSO MENU FUNCIONA APENAS COM NÚMEROS ****\n");
                Console.ResetColor();
        }
    }
    static void IniciarPartida(int quantBaralhos, int quantJogadores)
    {
        Queue<Jogador> rodada = new Queue<Jogador>(quantJogadores);
        Partida partida = new Partida();
        for (int i = 0; i < quantBaralhos; i++)
        {
            cartas.AdicionarCartas(cartasBaralho); //Adiciona cartas ao baralho
        }
        //Embaralha todas cartas
        List<Cartas> BaralhoEmbaralhado = partida.EmbaralharCartas(cartasBaralho, quantBaralhos);
        //Adicio as cartas no baralho
        partida.Baralho(BaralhoEmbaralhado);

        Stack<Cartas> BaralhoMesa = partida.Baralho(BaralhoEmbaralhado);
        for (int i = 0; i < quantJogadores; i++)
        {
            rodada.Enqueue(ListaJogadores[i]);
        }
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Clear();
        Console.WriteLine("\n**** INICIANDO RODADA ****\n");
        Console.ResetColor();
        //Começa a rodada
        while (BaralhoMesa.Count > 0)
        {
            Jogador jogador = rodada.Dequeue();
            rodada.Enqueue(jogador);
            Cartas cartaDaVez;
            Console.WriteLine($"O Jogador da vez é o {jogador.getNome()}, digite seu PIN");
            while (true)
            {
                string pin = Console.ReadLine();
                if (pin == jogador.getPin())
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n**** PIN VALIDADO ****\n");
                    Console.ResetColor();
                    cartaDaVez = BaralhoMesa.Pop();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"**** A CARTA DA VEZ RETIRADA DO BARALHO É: {cartaDaVez.GetNumero()} {cartaDaVez.GetNaipe()}\n");
                    Console.ResetColor();
                    Console.WriteLine("------------------------------");
                    Console.WriteLine("CARTAS DA MESA (ÁREA DE DESCARTE)");
                    if (ListaDeCartasAreaDescarte.Count > 0)
                    {
                        for (int i = 0; i < ListaDeCartasAreaDescarte.Count; i++)
                        {
                            Console.WriteLine($"{ListaDeCartasAreaDescarte[i].GetNumero()} {ListaDeCartasAreaDescarte[i].GetNaipe()}");
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n\nÁREA DE DESCARTE VAZIA\n");
                        Console.ResetColor();
                    }
                    Console.WriteLine("\n------------------------------");

                    partida.IniciarPartida(BaralhoMesa, jogador, cartaDaVez, ListaJogadores, ListaDeCartasAreaDescarte);
                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n**** PIN INCORRETO ****\n");
                    Console.ResetColor();
                }
            }
        }
        OrdenarGanhadores(ListaJogadores);
    }
    static void CadastrarJogadores(int quantJogadores)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\n**** CADASTRO DE JOGADOR ****\n");
        Console.ResetColor();
        for (int i = 0; i < quantJogadores; i++)
        {
            Console.WriteLine("Digite o nome do jogador");
            string nomeJogador = Console.ReadLine().ToUpper();
            while (true)
            {
                Console.WriteLine("\nInsira o PIN desejado:");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("(O PIN deve ter 4 caracteres)");
                Console.ResetColor();
                string pin = Console.ReadLine();
                if (pin.Length == 4)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n**** JOGADOR CADASTRADO ****");
                    Console.ResetColor();
                    Jogador jogador = new Jogador(nomeJogador, pin);
                    ListaJogadores.Add(jogador);
                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n**** O PIN DEVE TER 4 CARACTERES ****");
                    Console.ResetColor();
                }
            }
        }
    }
    static void criarRegras()
    {
        string caminhoarquivo = "Regras.txt";
        string texto = "ROUBA MONTESn\nObjetivo do jogo:\n Acumular o maior monte de cartas.\n\n Regras: Um jogador distribui 4 cartas para cada participante e vira 4 cartas na mesa.\n O primeiro  jogador deve veri car se alguma carta que ele tem na mão é igual a alguma carta da mesa.\n Se for igual, ele junta as duas cartas em seu monte. Caso a carta seja igual a à carta do topo do monte adversário, ele poderá roubar esse monte, pegando todas as cartas.\n Caso não tenha uma carta igual a qualquer uma da mesa, deverá descartar uma carta da mão virada para cima no centro da mesa. Quando todos os jogadores estiverem sem cartas na mão, são distribuídas mais quatro para cada um, até que o baralho acabe.\n O jogo termina quando não houver mais cartas para serem distribuídas, e ganha quem tiver o maior monte. O coringa pode ser colocado em cima do monte do jogador para protegê-lo de ser roubado, e dura até que outra carta seja colocada por cima do monte.";
        StreamWriter regras = new StreamWriter(caminhoarquivo);
        regras.Write(texto);
        regras.Close();
    }
    static void OrdenarGanhadores(List<Jogador> ListaJogadores)
    {
        OrdenarQtdCartasNaMao(ListaJogadores);
        static void OrdenarQtdCartasNaMao(List<Jogador> ListaJogadores)
        {
            Jogador temp;
            for (int i = 0; i < (ListaJogadores.Count - 1); i++)
            {
                for (int j = ListaJogadores.Count - 1; j > i; j--)
                {
                    if (ListaJogadores[j].monteJogador.Count > ListaJogadores[j - 1].monteJogador.Count)
                    {
                        temp = ListaJogadores[j];
                        ListaJogadores[j] = ListaJogadores[j - 1];
                        ListaJogadores[j - 1] = temp;
                    }
                }
            }
        }

        PodioJogadores(ListaJogadores);
        static void PodioJogadores(List<Jogador> ListaJogadores)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n**** PÓDIO DESSA RODADA ****\n");
            Console.ResetColor();
            int cont = 0, posicao = 3;
            while (cont < posicao && cont < ListaJogadores.Count)
            {
                List<Cartas> MonteJogador = ListaJogadores[cont].monteJogador.ToList();
                if (cont + 1 < ListaJogadores.Count)
                {
                    if (cont + 1 < ListaJogadores.Count && ListaJogadores[cont].monteJogador.Count == ListaJogadores[cont + 1].monteJogador.Count)
                    {

                        Console.WriteLine($"\n**** HOUVE UM EMPATE ENTRE OS JOGADORES {ListaJogadores[cont].getNome()} e {ListaJogadores[cont + 1].getNome()} ****\n");
                        Console.WriteLine($"Nome Jogador: {ListaJogadores[cont].getNome()}\nPosição: {cont + 1}\nQuantidade de cartas no monte: {ListaJogadores[cont].getQtdCartasMonte()}\nCartas ordenadas do monte do jogador:");
                        MostrarMonteOrdenado(MonteJogador);
                        Console.WriteLine($"Nome Jogador: {ListaJogadores[cont + 1].getNome()}\nPosição: {cont + 1}\nQuantidade de cartas no monte: {ListaJogadores[cont + 1].getQtdCartasMonte()}\nCartas ordenadas do monte do jogador:");
                        MonteJogador = ListaJogadores[cont + 1].monteJogador.ToList();
                        MostrarMonteOrdenado(MonteJogador);
                        ListaJogadores[cont].ranking.Enqueue(cont + 1);
                        ListaJogadores[cont + 1].ranking.Enqueue(cont + 1);
                        cont += 2;
                        posicao++;
                    }
                }
                else
                {
                    Console.WriteLine($"Nome Jogador: {ListaJogadores[cont].getNome()}\nPosição: {cont}\nQuantidade de cartas no monte: {ListaJogadores[cont].getQtdCartasMonte()}\nCartas ordenadas do monte do jogador:");
                    MostrarMonteOrdenado(MonteJogador);
                    ListaJogadores[cont].ranking.Enqueue(cont);
                    ++cont;
                }

            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n**** RANKING ORDENANDO PELA QUANTIDADE DE CARTAS NO MONTE ****\n");
            Console.ResetColor();
            for (int i = 0; i < ListaJogadores.Count; i++)
            {
                Console.WriteLine($"Nome Jogador: {ListaJogadores[i].getNome()}\nPosição: {i + 1}\nQuantidade de cartas no monte: {ListaJogadores[i].getQtdCartasMonte()} ****\n");
            }
        }
        static void MostrarMonteOrdenado(List<Cartas> MonteJogador)
        {
            Cartas temp;
            if (MonteJogador.Count > 0)
            {
                for (int i = 0; i < (MonteJogador.Count - 1); i++)
                {
                    for (int j = MonteJogador.Count - 1; j > i; j--)
                    {
                        if (MonteJogador[j].GetNumero() < MonteJogador[j - 1].GetNumero())
                        {
                            temp = MonteJogador[j];
                            MonteJogador[j] = MonteJogador[j - 1];
                            MonteJogador[j - 1] = temp;
                        }
                    }
                }
                Console.Write("[");
                foreach (var a in MonteJogador)
                {
                    Console.Write(a.GetNumero() + " " + a.GetNaipe() + " ");
                }
                Console.WriteLine("]");
                Console.WriteLine("------------------\n");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n**** O JOGADOR NÃO POSSUI MONTE ****\n");
                Console.ResetColor();
                Console.WriteLine("------------------\n");
            }
        }
    }
}