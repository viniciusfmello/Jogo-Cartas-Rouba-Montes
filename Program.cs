﻿using System.IO;
class Program
{
    static Cartas cartas = new Cartas();
    static List<Cartas> cartasBaralho = new List<Cartas>();
    static List<Jogador> ListaJogadores = new List<Jogador>();
    static List<Cartas> ListaDeCartasAreaDescarte = new List<Cartas>();
    static string caminhoArquivoLogs = "Logs.txt";
    static string caminhoArquivoRankings = "Rankings.txt";
    static void Main()
    {
        using (StreamWriter escritorArquivo = new StreamWriter(caminhoArquivoLogs))
        {
            escritorArquivo.WriteLine("**** LOGS DA PARTIDA ****");
        }
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
                        Console.WriteLine("\nNão se preocupe!! Temos um arquivo de texto que explica todo o jogo. Confira- na pasta, dê uma olhada!");
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
        /*
        Nesse método criamos um questionário para sabermos informações básicas do jogo, como por exemplo quantos jogadores vão jogar e a quantidade de baralho que os jogadores querem.
        Caso o jogador digite uma opção que não está disponível, uma mensagem de erro é exibida e o jogador é redirecionado para escolher uma opção disponível.

        */
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
                                using (StreamWriter escritorArquivo = new StreamWriter(caminhoArquivoLogs))
                                {
                                    escritorArquivo.WriteLine($"- {quantJogadores} jogadores vão jogar o jogo");
                                }
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
                                using (StreamWriter escritorArquivo = new StreamWriter(caminhoArquivoLogs, true))
                                {
                                    escritorArquivo.WriteLine($"- Esse jogo terá {quantBaralhos} baralho(s), total de {53 * quantBaralhos} cartas");
                                }
                                CadastrarJogadores(quantJogadores);
                                IniciarPartida(quantBaralhos, quantJogadores, caminhoArquivoLogs);
                                break;
                            }
                        }
                        break;
                    }
                    break;
                case 2:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Obrigado pela oportunidade de conhecer nosso jogo, volte quando estiver pronto!");
                    Console.ResetColor();
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
    static void IniciarPartida(int quantBaralhos, int quantJogadores, string caminhoArquivoLogs)
    {
        Queue<Jogador> rodada = new Queue<Jogador>(quantJogadores);
        Partida partida = new Partida();
        for (int i = 0; i < quantBaralhos; i++)
        {
            cartas.AdicionarCartas(cartasBaralho); //Adiciona cartas ao baralho
        }
        //Embaralha todas cartas
        List<Cartas> BaralhoEmbaralhado = partida.EmbaralharCartas(cartasBaralho, quantBaralhos, caminhoArquivoLogs);
        //Adiciona as cartas no baralho
        Stack<Cartas> MonteCompras = partida.Baralho(BaralhoEmbaralhado);
        for (int i = 0; i < quantJogadores; i++)
        {
            rodada.Enqueue(ListaJogadores[i]);
        }
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Clear();
        Console.WriteLine("\n**** INICIANDO RODADA ****\n");
        Console.ResetColor();
        //Começa a rodada
        while (MonteCompras.Count > 0)
        {
            using (StreamWriter escritorArquivo = new StreamWriter(caminhoArquivoLogs, true))
                {
                    escritorArquivo.Write($"O baralho tem {MonteCompras.Count} cartas.");
                }
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
                    cartaDaVez = MonteCompras.Pop();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"**** A CARTA DA VEZ RETIRADA DO BARALHO É: {cartaDaVez.GetNumero()} {cartaDaVez.GetNaipe()}\n");
                    Console.ResetColor();

                    //Escreve nos logs
                    using (StreamWriter escritorArquivo = new StreamWriter(caminhoArquivoLogs, true))
                    {
                        escritorArquivo.WriteLine($"- O jogador da vez é o(a) [{jogador.getNome()}] e a carta da vez retirada por ele é a {cartaDaVez.GetNumero()} {cartaDaVez.GetNaipe()}");
                        escritorArquivo.WriteLine($"Cartas na área de descarte: ");
                    }
                    Console.WriteLine("------------------------------");
                    Console.WriteLine("CARTAS DA MESA (ÁREA DE DESCARTE)");
                    if (ListaDeCartasAreaDescarte.Count > 0)
                    {
                        for (int i = 0; i < ListaDeCartasAreaDescarte.Count; i++)
                        {
                            Console.WriteLine($"{ListaDeCartasAreaDescarte[i].GetNumero()} {ListaDeCartasAreaDescarte[i].GetNaipe()}");
                            //escreve nos logs
                            using (StreamWriter escritorArquivo = new StreamWriter(caminhoArquivoLogs, true))
                            {
                                escritorArquivo.Write($"{ListaDeCartasAreaDescarte[i].GetNumero()} {ListaDeCartasAreaDescarte[i].GetNaipe()}");
                            }
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n\nÁREA DE DESCARTE VAZIA\n");
                        Console.ResetColor();
                    }
                    Console.WriteLine("\n------------------------------");

                    partida.IniciarPartida(MonteCompras, jogador, cartaDaVez, ListaJogadores, ListaDeCartasAreaDescarte, caminhoArquivoLogs);
                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n**** PIN INCORRETO ****\n");
                    Console.ResetColor();
                    using (StreamWriter escritorArquivo = new StreamWriter(caminhoArquivoLogs, true))
                    {
                        escritorArquivo.WriteLine($"- O jogador {jogador.getNome()} digitou um PIN incorreto");
                    }
                }
            }
        }
        /*
        Aqui é apenas um menu fim de rodada, onde os jogadores tem a opção de escolher o rank de outro jogador.
        */
        OrdenarGanhadores(ListaJogadores);
        Console.WriteLine("--- MENU FIM DE RODADA ---");
        Console.WriteLine("1) Deseja iniciar uma nova rodada?\n2) Deseja ver o ranking de algum jogador?\n3) Sair do jogo.");
        int continuar = int.Parse(Console.ReadLine());
        switch (continuar) {
            case 1: 
            IniciarPartida(quantBaralhos, quantJogadores, caminhoArquivoLogs);
            break;
            case 2:
            string nome = "";
            Console.WriteLine("Insira o nome do jogador que você deseja pesquisar o rank: ");
            nome = Console.ReadLine();
            BuscarRankingJogador(ListaJogadores, nome);
            break;
            case 3:
            Console.WriteLine("Obrigado por jogar nosso jogo!"); 
            break;
            default: 
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("**** OPÇÃO INVÁLIDA ****");
            Console.ResetColor();
            break;
        }
    }
    static void CadastrarJogadores(int quantJogadores)
    {
        /*
        Esse método foi criado para cadastrar os jogadores, criamos um for para rodar a quantidade de jogadores e adicionar o nome de cada um. Também criamos um pin para cada jogador,
        o mínimo de cada pin é 4 caracteres, caso o pin tiver menos que 4, uma mensagem de erro é exbida, mas caso o pin tenha 4 caracteres, uma mensagem dizendo que o jogador foi
        cadastrado é exibida.
        */
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
                    using (StreamWriter escritorArquivo = new StreamWriter(caminhoArquivoLogs, true))
                    {
                        escritorArquivo.WriteLine($"- Jogador de nome [{nomeJogador}] foi cadastrado com o PIN [{pin}]");
                    }
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
    static void OrdenarGanhadores(List<Jogador> ListaJogadores)
    {
        /*
        Criamos esse método para ordenar os jogadores pela quantidade de cartas no monte de cada um.
        */
        OrdenarQtdCartasNoMonte(ListaJogadores);
        static void OrdenarQtdCartasNoMonte(List<Jogador> ListaJogadores)
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
        PodioJogadores(ListaJogadores, caminhoArquivoRankings);
        static void PodioJogadores(List<Jogador> ListaJogadores, string caminhoArquivoRankings)
        {
            /*
            Nesse método, verificamos o pódio da rodada jogada, tratando empates nas vitórias e colocando em ordem de acordo com a quantidade de cartas no monte de cada jogador. 
            Para isso, montamos um while que vai percorrer a lista de jogadores e montar um pódio(com no máximo 3 posições) e desenvolvemos uma lógica pra caso desse empate, ou seja,
            duas pessoas ocupaasem a mesma posição, a variável que controla a quantidade de jogadores no pódio aumentasse para ser no máximo 3 posições no pódio(1°, 2° e 3°)
            */
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n**** PÓDIO DESSA RODADA ****\n");
            Console.ResetColor();
            int cont = 0, posicao = 3;
            while (cont < posicao && cont < ListaJogadores.Count)
            {
                List<Cartas> MonteJogador = ListaJogadores[cont].monteJogador.ToList();
                bool empatou = false;
                if (cont + 1 < ListaJogadores.Count)
                {
                    if (ListaJogadores[cont].monteJogador.Count == ListaJogadores[cont + 1].monteJogador.Count)
                    {
                        Console.WriteLine($"\n**** HOUVE UM EMPATE ENTRE OS JOGADORES {ListaJogadores[cont].getNome()} e {ListaJogadores[cont + 1].getNome()} ****\n");
                        Console.WriteLine($"Nome Jogador: {ListaJogadores[cont].getNome()}\nPosição: {cont + 1}\nQuantidade de cartas no monte: {ListaJogadores[cont].getQtdCartasMonte()}\nCartas ordenadas do monte do jogador:");
                        MostrarMonteOrdenado(MonteJogador);
                        Console.WriteLine($"Nome Jogador: {ListaJogadores[cont + 1].getNome()}\nPosição: {cont + 1}\nQuantidade de cartas no monte: {ListaJogadores[cont + 1].getQtdCartasMonte()}\nCartas ordenadas do monte do jogador:");
                        MonteJogador = ListaJogadores[cont + 1].monteJogador.ToList();
                        MostrarMonteOrdenado(MonteJogador);
                        ListaJogadores[cont].ranking.Enqueue(cont + 1);
                        ListaJogadores[cont + 1].ranking.Enqueue(cont + 1);
                         using (StreamWriter escritorArquivo = new StreamWriter(caminhoArquivoRankings, true))
                        {
                            escritorArquivo.WriteLine($"- O jogador [{ListaJogadores[cont].getNome()}] ficou na posição {cont + 1}");
                            escritorArquivo.WriteLine($"- O jogador [{ListaJogadores[cont + 1].getNome()}] ficou na posição {cont + 1}");
                        }
                        cont += 2;
                        posicao++;
                        empatou = true;
                       
                    }
                }
                if(!empatou)
                {
                    Console.WriteLine($"Nome Jogador: {ListaJogadores[cont].getNome()}\nPosição: {cont + 1}\nQuantidade de cartas no monte: {ListaJogadores[cont].getQtdCartasMonte()}\nCartas ordenadas do monte do jogador:");
                    MostrarMonteOrdenado(MonteJogador);
                    ListaJogadores[cont].ranking.Enqueue(cont);
                    using (StreamWriter escritorArquivo = new StreamWriter(caminhoArquivoRankings, true))
                        {
                            escritorArquivo.WriteLine($"- O jogador [{ListaJogadores[cont].getNome()}] ficou na posição {cont + 1}");
                        }
                    cont++;
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
    static void BuscarRankingJogador(List<Jogador> ListaJogadores, string nome) {
        /*
        Esse método foi criado para buscar o ranking de um jogador. Para isso utilizamos um for para percorrer a lista de jogadores, se o nome digitado for encontrado
        na lista, então uma mensagem dizendo que o jogador foi encontrado é exibida, junto com as últimas 5 posições do jogador. Para encontrar a posição do jogador no rank, utilizamos
        um foreach para percorrer, na posição do jogador encontrada, todos os ranks das últimas 5 partidas jogadas do jogador em questão.
        */
        int cont = 0;
        for (int i = 0; i < ListaJogadores.Count; i++) {
            if(nome == ListaJogadores[i].getNome()) {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"*** O JOGADOR FOI ENCONTRADO ***\nAS 5 ÚLTIMAS POSIÇÕES DO JOGADOR {ListaJogadores[i].getNome()} FORAM: ");
                Console.ResetColor();       
                foreach (var rank in ListaJogadores[i].ranking){
                    Console.WriteLine($"PARTIDA {cont + 1} POSIÇÃO: {rank}°");
                    cont ++;
                }
            }
        }
    }
}