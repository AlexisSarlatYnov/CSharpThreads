using Minage.Mines.MineThreads;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Minage
{
    class Program
    {
        public static int nbMineurs = 10;
        public static int nbSacsMax = 20000000;
        public static int nbSacs = 0;
        public static int nbPepites = 0;
        public static bool estFini = false;
        public static Object _lock = new Object();
        static void Main(string[] args)
        {
            /*Console.WriteLine("Minage mine 1 !");

            Minage1();

            nbSacs = 0;
            nbPepites = 0;
            estFini = false;*/

            Console.WriteLine("Minage mine 2 !");

            Minage2();

            nbSacs = 0;
            nbPepites = 0;
            estFini = false;

            Console.WriteLine("Minage mine 3 !");

            Minage3();
        }

        public static void MineurMiner()
        {
            while(estFini != true)
            {
                lock (_lock)
                {
                    nbSacs += 1;
                    nbPepites += MinageThreads.MinagePepitesRandom(Thread.CurrentThread.Name);
                }
            }
        }

        public static void SupervisionMine()
        {
            while (estFini != true)
            {
                Console.WriteLine("Actuellement nous avons " + nbSacs.ToString() + " sacs avec un total de " + nbPepites.ToString() + " pépites d'or avec pour objectif " + nbSacsMax.ToString() + " sacs d'or ! Il s'agit du travail de " + nbMineurs.ToString() + " mineurs !");
                Thread.Sleep(100);
            }
        }

        public static void Minage1()
        {
            MinageThreads.HelloWorld();
            Console.ReadLine();

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Thread[] threads = new Thread[nbMineurs];

            Thread supervisionThread = new Thread(SupervisionMine);
            supervisionThread.Start();

            for (int i = 0; i < nbMineurs; i++)
            {
                threads[i] = new Thread(MineurMiner);
                threads[i].Start();
                threads[i].Name = "Thread" + (i + 1).ToString();
                Console.WriteLine("Nom thread : " + threads[i].Name);
            }

            while (estFini != true)
            {
                if (nbSacs >= nbSacsMax)
                {
                    estFini = true;
                }
            }

            stopWatch.Stop();
            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = stopWatch.Elapsed;

            Console.WriteLine("Actuellement nous avons " + nbSacs.ToString() + " sacs avec un total de " + nbPepites.ToString() + " pépites d'or avec pour objectif " + nbSacsMax.ToString() + " sacs d'or ! Il s'agit du travail de " + nbMineurs.ToString() + " mineurs ! Durée du travail : " + ts.ToString() + " !");
            Console.ReadLine();
        }

        public static void Minage2()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            ThreadPool.QueueUserWorkItem(new WaitCallback(SupervisionMine2));

            for (int i = 0; i < nbMineurs; i++)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(MineurMiner2));
            }

            while (estFini != true)
            {
                if (nbSacs >= nbSacsMax)
                {
                    estFini = true;
                }
            }

            stopWatch.Stop();
            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = stopWatch.Elapsed;

            Console.WriteLine("Actuellement nous avons " + nbSacs.ToString() + " sacs avec un total de " + nbPepites.ToString() + " pépites d'or avec pour objectif " + nbSacsMax.ToString() + " sacs d'or ! Il s'agit du travail de " + nbMineurs.ToString() + " mineurs ! Durée du travail : " + ts.ToString() + " !");
            Console.ReadLine();
        }

        public static void MineurMiner2(Object callback)
        {
            while (estFini != true)
            {
                nbSacs += 1;
                nbPepites += MinageThreads.MinagePepitesRandom(nbSacs.ToString());
            }
        }

        public static void SupervisionMine2(Object callback)
        {
            while (estFini != true)
            {
                Console.WriteLine("Actuellement nous avons " + nbSacs.ToString() + " sacs avec un total de " + nbPepites.ToString() + " pépites d'or avec pour objectif " + nbSacsMax.ToString() + " sacs d'or ! Il s'agit du travail de " + nbMineurs.ToString() + " mineurs !");
                Thread.Sleep(100);
            }
        }

        public static void Minage3()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Task[] tasks = new Task[nbMineurs];

            Task supervisionThread = Task.Run(() => SupervisionMine3());

            for (int i = 0; i < nbMineurs; i++)
            {
                tasks[i] = Task.Run(() => MineurMiner3());
                /*threads[i] = new Thread(MineurMiner);
                threads[i].Start();
                threads[i].Name = "Thread" + (i + 1).ToString();
                Console.WriteLine("Nom thread : " + threads[i].Name);*/
            }

            while (estFini != true)
            {
                if (nbSacs >= nbSacsMax)
                {
                    estFini = true;
                }
            }

            stopWatch.Stop();
            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = stopWatch.Elapsed;

            Console.WriteLine("Actuellement nous avons " + nbSacs.ToString() + " sacs avec un total de " + nbPepites.ToString() + " pépites d'or avec pour objectif " + nbSacsMax.ToString() + " sacs d'or ! Il s'agit du travail de " + nbMineurs.ToString() + " mineurs ! Durée du travail : " + ts.ToString() + " !");
            Console.ReadLine();
        }

        public static void MineurMiner3()
        {
            while (estFini != true)
            {
                nbSacs += 1;
                nbPepites += MinageThreads.MinagePepitesRandom(nbSacs.ToString());
            }
        }

        public static void SupervisionMine3()
        {
            while (estFini != true)
            {
                Console.WriteLine("Actuellement nous avons " + nbSacs.ToString() + " sacs avec un total de " + nbPepites.ToString() + " pépites d'or avec pour objectif " + nbSacsMax.ToString() + " sacs d'or ! Il s'agit du travail de " + nbMineurs.ToString() + " mineurs !");
                Thread.Sleep(100);
            }
        }
    }
}
