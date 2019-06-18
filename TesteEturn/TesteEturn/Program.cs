using System;
using System.Collections.Generic;

namespace TesteEturn
{
    class Program
    {
        public enum cidades
        {
            A = 0,
            B = 1,
            C = 2,
            D = 3,
            E = 4
        }
        
        public class Graph
        {
            public List<Edge> edgesList;
            public int vertices, edges;

            public List<int>[] adjList;

            public Graph(int vertices, int edges)
            {
                this.vertices = vertices;
                this.edges = edges;
                edgesList = new List<Edge>(edges);

                initAdjList();
            }

            private void initAdjList()
            {
                adjList = new List<int>[vertices];

                for (int i = 0; i < vertices; i++)
                {
                    adjList[i] = new List<int>();
                }
            }

            public void addEdge(int u, int v, int dist)
            {
                adjList[u].Add(v);
                edgesList.Add(new Edge(u, v, dist));
            }            
        }

        public class Edge
        {
            public int src, dest;
            public int weight;

            public Edge(int src, int dest, int weight)
            {
                this.src = src;
                this.dest = dest;
                this.weight = weight;
            }
        }

        static void Main(string[] args)
        {
            int noOfVertices = 5;
            int noOfEdges = 9;
            Graph graph = new Graph(noOfVertices, noOfEdges);

            graph.addEdge((int)cidades.A, (int)cidades.B, 5);
            graph.addEdge((int)cidades.A, (int)cidades.D, 5);
            graph.addEdge((int)cidades.A, (int)cidades.E, 7);
            graph.addEdge((int)cidades.B, (int)cidades.C, 4);
            graph.addEdge((int)cidades.C, (int)cidades.D, 8);
            graph.addEdge((int)cidades.C, (int)cidades.E, 2);
            graph.addEdge((int)cidades.D, (int)cidades.C, 8);
            graph.addEdge((int)cidades.D, (int)cidades.E, 6);
            graph.addEdge((int)cidades.E, (int)cidades.B, 3);

            string cids;
            int distancia;
            string origem;
            string destino;
            int numMaxParadas;
            int numExtParadas;
            int qntCaminhos;
            int menorDistanciaResp;
            int distanciaMaxima;
            int numeroViagens;
            int tipo = 1;
            while (tipo != 0)
            {
                Console.Write("Qual tipo de questão você quer testar:");
                Console.Write("\n1 - Soma de distancias(Ex:A-B-C);");
                Console.Write("\n2 - O numero de viagens comecando do ponto A ao B, com um numero de paradas máximas;");
                Console.Write("\n3 - O numero de viagens comecando do ponto A ao B, com um numero de paradas exatas;");
                Console.Write("\n4 - Ir do ponto A ao ponto B com a menor distancia;");
                Console.Write("\n5 - O numero de viagens comecando do ponto A ao B, com uma distância máxima definida.");
                Console.Write("\n0 - Sair!");
                Console.Write("\nSelecione o tipo:");
                tipo = Convert.ToInt32(Console.ReadLine());

                switch (tipo)
                {
                    case 1:
                        Console.WriteLine("Forneça o caminho que deseja calcular a distância. Separados por ',' virgula. \nEx: A,B,C.\n");
                        cids = Console.ReadLine().ToUpper();
                        distancia = calculoDistanciaEntrePontos(graph, cids);

                        if (distancia == -1)
                        {
                            Console.WriteLine("Rota não existente");
                            Console.ReadKey();
                            break;
                        }
                        Console.WriteLine("A distancia entre os pontos {0} é: {1}", cids, distancia);
                        Console.ReadKey();
                        break;

                    case 2:
                        Console.WriteLine("Forneça a origem.");
                        origem = Console.ReadLine().ToUpper();
                        Console.WriteLine("Forneça o destino.");
                        destino = Console.ReadLine().ToUpper();
                        Console.WriteLine("Qual o numero máximo de paradas?");
                        numMaxParadas = Int32.Parse(Console.ReadLine());

                        qntCaminhos = numeroCaminhosPorMaxParada(graph, (int)Enum.Parse(typeof(cidades), origem), (int)Enum.Parse(typeof(cidades), destino), numMaxParadas);

                        Console.WriteLine("O número máximo de caminhos entre {0} e {1} com no máximo {2} paradas é: {3}", origem, destino, numMaxParadas, qntCaminhos);
                        Console.ReadKey();
                        break;

                    case 3:
                        Console.WriteLine("Forneça a origem.");
                        origem = Console.ReadLine().ToUpper();
                        Console.WriteLine("Forneça o destino.");
                        destino = Console.ReadLine().ToUpper();
                        Console.WriteLine("Qual o numero exato de paradas?");
                        numExtParadas = Int32.Parse(Console.ReadLine());

                        qntCaminhos = numeroCaminhosPorExtParada(graph, (int)Enum.Parse(typeof(cidades), origem), (int)Enum.Parse(typeof(cidades), destino), numExtParadas);

                        Console.WriteLine("O número máximo de caminhos entre {0} e {1} com exatamente {2} paradas é: {3}", origem, destino, numExtParadas, qntCaminhos);
                        Console.ReadKey();
                        break;

                    case 4:
                        Console.WriteLine("Forneça a origem.");
                        origem = Console.ReadLine().ToUpper();
                        Console.WriteLine("Forneça o destino.");
                        destino = Console.ReadLine().ToUpper();

                        menorDistanciaResp = menorDistancia(graph, origem, destino);

                        Console.WriteLine("A menor distância entre {0} e {1} é: {2}", origem, destino, menorDistanciaResp);
                        Console.ReadKey();
                        break;

                    case 5:
                        Console.WriteLine("Forneça a origem.");
                        origem = Console.ReadLine().ToUpper();
                        Console.WriteLine("Forneça o destino.");
                        destino = Console.ReadLine().ToUpper();
                        Console.WriteLine("Qual a ditância máxima permitida?");
                        distanciaMaxima = Int32.Parse(Console.ReadLine());

                        numeroViagens = numeroViagemDistanciaMaxima(graph, (int)Enum.Parse(typeof(cidades), origem), (int)Enum.Parse(typeof(cidades), destino), distanciaMaxima);

                        Console.WriteLine("O número viagens possíveis entre {0} e {1} com a distância máxima de {2} é: {3}", origem, destino, distanciaMaxima, numeroViagens);
                        Console.ReadKey();
                        break;

                    case 0:
                        Console.WriteLine("Saindo...");
                        Console.ReadKey();
                        break;

                    default:
                        Console.WriteLine("Forneca uma opção válida...");
                        Console.ReadKey();
                        break;

                }
            }                   
        }

        private static int calculoDistanciaEntrePontos(Graph g, string pontos)
        {
            int dist = 0;
            List<string> cid = new List<string>();
            cid.AddRange(pontos.Split(','));

            for (int i = 0; i < cid.Count-1; i++) {
                var d = 0;
                foreach (var edge in g.edgesList)
                {
                    if(edge.src == (int)Enum.Parse(typeof(cidades), cid[i]) && edge.dest == (int)Enum.Parse(typeof(cidades), cid[i + 1]))
                    {
                        d += edge.weight;
                        break;
                    }
                }                

                if (d > 0)
                {
                    dist += d;
                }
                else
                {
                    return -1;
                }
            }

            return dist;
        }
        
        private static int numeroCaminhosPorMaxParada(Graph g, int origem, int destino, int numMaxParadas)
        {
            List<int> pathList = new List<int>();

            pathList.Add(origem);

            int retorno = 0;

            numeroCaminhosPorMaxParadaRec(g, origem, destino, pathList, numMaxParadas, ref retorno);
            return retorno;
        }

        //Recursive
        private static void numeroCaminhosPorMaxParadaRec(Graph g, int u, int d, List<int> localPathList, int numMaxParadas, ref int retorno)
        {
            if ((u.Equals(d) && localPathList.Count-1 <= numMaxParadas) && (u.Equals(d) && localPathList.Count > 1))
            {
                retorno++;
                return;
            }else if (localPathList.Count - 1 > numMaxParadas)
                    return;
             
            foreach (int i in g.adjList[u])
            {
                localPathList.Add(i);
                numeroCaminhosPorMaxParadaRec(g, i, d, localPathList, numMaxParadas, ref retorno);

                localPathList.Remove(i);
            }
        }

        private static int numeroCaminhosPorExtParada(Graph g, int origem, int destino, int numExtParadas)
        {
            List<int> pathList = new List<int>();

            pathList.Add(origem);

            int retorno = 0;

            numeroCaminhosPorExtParadaRec(g, origem, destino, pathList, numExtParadas, ref retorno);
            return retorno;
        }

        //Recursive
        private static void numeroCaminhosPorExtParadaRec(Graph g, int u, int d, List<int> localPathList, int numExtParadas, ref int retorno)
        {
            if ((u.Equals(d) && localPathList.Count - 1 == numExtParadas) && (u.Equals(d) && localPathList.Count > 1))
            {
                retorno++;
                return;
            
            }else if (localPathList.Count - 1 > numExtParadas)
                    return;

            foreach (int i in g.adjList[u])
            { 
                localPathList.Add(i);
                numeroCaminhosPorExtParadaRec(g, i, d, localPathList, numExtParadas, ref retorno);
 
                localPathList.Remove(i);
            }
        }

        //BellmanFord
        private static int menorDistancia(Graph g, string origem, string destino)
        {
            int V = g.vertices;
            int E = g.edges;
            int[] distance = new int[V];
            int[] parent = new int[V];

            for (int i = 0; i < V; i++)
                distance[i] = int.MaxValue;

            distance[(int)Enum.Parse(typeof(cidades), origem)] = 0;

            for (int i = 1; i <= V - 1; i++)
            {
                for (int j = 0; j < E; j++)
                {
                    int u = g.edgesList[j].src;
                    int v = g.edgesList[j].dest;
                    int weight = g.edgesList[j].weight;

                    //modificação para não considerar o vertice inicial como esforço 0
                    if (distance[u] != int.MaxValue && (distance[u] + weight < distance[v] || distance[v] == 0))
                    {
                        distance[v] = distance[u] + weight;
                        parent[v] = u;
                    }
                }
            }
            return distance[(int)Enum.Parse(typeof(cidades), destino)];            
        }

        private static int numeroViagemDistanciaMaxima(Graph g, int origem, int destino, int distanciaMax)
        {
            List<int> pathList = new List<int>();

            pathList.Add(origem);

            int retorno = 0;
            int distanciaPercorrida = 0;

            numeroViagemDistanciaMaximaRec(g, origem, destino, pathList, distanciaMax, ref retorno, ref distanciaPercorrida);
            return retorno;
        }

        //Recursive
        private static void numeroViagemDistanciaMaximaRec(Graph g, int u, int d, List<int> localPathList, int distanciaMax, ref int retorno, ref int distanciaPercorrida)
        {
            if ((u.Equals(d) && distanciaPercorrida <= distanciaMax) && (u.Equals(d) && distanciaPercorrida > 1))
            {
                distanciaPercorrida = 0;
                retorno++;
                return;
            }
            else if (distanciaPercorrida > distanciaMax)
                return;

            foreach (int i in g.adjList[u])
            {
                localPathList.Add(i);
                foreach (var edge in g.edgesList)
                {
                    if (edge.src == u && edge.dest == d)
                    {
                        distanciaPercorrida += edge.weight;
                        break;
                    }
                }
                numeroViagemDistanciaMaximaRec(g, i, d, localPathList, distanciaMax, ref retorno, ref distanciaPercorrida);

                localPathList.Remove(i);
            }
        }

    }
}
