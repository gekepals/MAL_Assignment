using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;
using System.Reflection;

namespace MAL_ASS1
{
    public partial class Skaters : Form
    {
        //variabelen
        int w, h, R1, R2;
        int k, n, N, dikte,t;
        double r, d, Ep;
        double[,] positie, prob;
        int[] A;
		double[] totalQ, Qskater;
        double[,] Q;
        bool go, greedy;
        private Panel panel1;
        private Button Start;
        private Button Pause;
		private Button Greed;
        private Button button3;
        private TextBox SkatersAmount;
        private Label SelectPlayers;
        private Button Go;
		Random rand = new Random();

        public Skaters()
        {
            InitializeComponent();

			//toekennen waardes
			t = 0;
            k = 60;
            n = 360 / k;
            w = panel1.Width;
            h = panel1.Height;
            R1 = 10;
            R2 = 0;
            N = 15;
            r = 10;
            d = 10;
            dikte = 10;
            go = false;
			greedy = true;
			Ep = (double)0.01;

            //om het flikkeren van de skaters te voorkomen
            typeof(Panel).InvokeMember("DoubleBuffered",
            BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
            null, panel1, new object[] { true });

            //N-waarde uit tekstbox halen
            string skaters = SkatersAmount.Text;

            //positie voor alle N skaters
            positie = new double[N, 2];
            //actie-probabilities
            prob = new double[N, n];
            Q = new double[N, n];
			totalQ = new double[n];
			for (int r = 0; r < N; r++)
			{
				for (int t = 0; t<n; t++)
				{
					prob[r, t] = (double)(1.0 / (double)n);
					Q[r, t] = (double)(1.0);
				}

			}

            //evenementen
            panel1.Paint += this.teken;
            Start.Click += start;
            Pause.Click += pauze;
			Greed.Click += greed;
            button3.Click += stop;
            Go.Click += player_amount;

            //eerste skaters op panel zetten
            startpositie();
			acties();
        }

        private Thread animatie;

        //start-button
        private void start(object obj, EventArgs ea)
        {
            go = true;
            animatie = new Thread(movement);
            animatie.Start();
        }

        //pauze-button
        private void pauze(object obj, EventArgs ea)
        {
            go = false;
        }

        //stop-button
        private void stop(object obj, EventArgs ea)
        {
            go = false;
            startpositie();
        }

		private void greed (object obj, EventArgs ea)
		{
			greedy = !greedy;
		}

        //GO-button
        private void player_amount(object obj, EventArgs ea)
        {
            int skaters = int.Parse(SkatersAmount.Text);
            if (skaters <= 100)
                N = skaters;
            else
                N = 100;
            startpositie();
            start(obj, ea);
        }

        public void startpositie()
        {
            Random rnd = new Random();
            for (int t = 0; t < N; t++)
            {
                int z = rnd.Next(0, panel1.Width - (dikte / 2));
                int c = rnd.Next(0, panel1.Height - (dikte / 2));
                positie[t, 0] = z;
                positie[t, 1] = c;
            }
        }

        public void acties()
        {
            //mogelijke acties die genomen mogen worden
              A = new int[n];
            for (int t = 0; t < n; t++)
            {
                A[t] = k * t;
            }
        }

		//voert een beweging uit (als deze niet tot botsen komt)
		public void movement()
		{
			while (go)
			{
				for (int i = 0; i < N; i++)
				{
					//voor ChooseAction methode uit voor elke skater
					int a = ChooseAction(i);

					//x en y posities uit array positie halen
					double x = positie[i, 0];
					double y = positie[i, 1];

					//cosinus en sinus toepassen op volgende x en y positie
					x = x + (double)(Math.Cos(Math.PI * A[a] / (double)180) * (double)d);
					y = y + (double)(Math.Sin(Math.PI * A[a] / (double)180) * (double)d);

					//ervoor zorgen dat het bord doorlopend is
					if (x > w) x = x - w;
					if (x < 0) x = x + w;
					if (y > h) y = y - h;
					if (y < 0) y = y + h;
					bool b = botsen(x, y, i);
					//check voor botsen - als botsen = false, dan movement uitvoeren
					if (b == false)
					{
						//movement uitvoeren, dus posities aanpassen
						positie[i, 0] = x;
						positie[i, 1] = y;
						//Movement Reinforcement R1 uitvoeren
						MovementReinf(R1, a, i);
					}
					if (b == true)
					{
						//Movement Reinforcement R2 uitvoeren
						MovementReinf(R2, a, i);
					}
				}
				if (t % 10 == 0)
				{
					Console.WriteLine(string.Join(" , ", totalQ) + " , " +t);
					Console.WriteLine(greedy);
				}
				Thread.Sleep(15);
                this.panel1.Invalidate();
				t++;

			}
		}

        public int ChooseAction(int t)
        {
            double[] sumProb;
            sumProb = new double[n];
            for (int s = 0; s < n; s++)
            {
                double sum = 0;
                for (int x = 0; x <= s; x++)
                {
                    sum += prob[t, x];
                }
                sumProb[s] = sum;
            }
			here:
            double r = (double)rand.NextDouble();

            for (t = 0; t < n; t++)
            {
				if (r < sumProb[t])
				{
					return t;
				}
            }
			goto here;
        }

        //checkt voor botsen - returnt true (botsen) of false (niet botsen)
        public bool botsen(double x, double y, int j)
        {
            for (int i = 0; i < N; i++)
            {
				if (i != j)
				{
					double xs = positie[i, 0];
					double ys = positie[i, 1];
					double c = (double)Math.Sqrt(Math.Pow(Math.Abs(x - xs), 2) + Math.Pow(Math.Abs(y - ys), 2));
					if (c < r)
						return true;

					double newy = 100;
					double newys = 1;
					double newx = 100;
					double newxs = 1;
					if (xs < d) { newxs = xs + (double)w; }
					if (x < d) { newx = x + (double)w; }
					if (ys < d) { newys = ys + (double)h; }
					if (y < d) {newy = y + (double)h;}
					double newc = (double)Math.Sqrt(Math.Pow(Math.Abs(newx - newxs), 2) + Math.Pow(Math.Abs(newy - newys), 2));
                    if (newc<r)
                        return true;
					
                }
            }
			return false;
        }

        public void MovementReinf(int reward, int action, int skater)
        {
			//updaten Q-values
			for (int i = 0; i < n; i++)
			{
				if (i == action)
				{
					Q[skater, action] = Q[skater, action] + (reward - Q[skater, action])/(double)n;
				}
				else
				{
					Q[skater, i] = Q[skater, i];
				}
			}
			//updaten probabilities-array with greedy algorithm
			if (greedy)
			{
				Qskater = new double[n];
				for (int i = 0; i < n; i++)
				{
					Qskater[i] = Q[skater, i];
				}

				double maxValue = 0;
				maxValue = Qskater.Max();
				int maxIndex = Qskater.ToList().IndexOf(maxValue);

				for (int t = 0; t < n; t++)
				{
					if (t == maxIndex)
					{
						prob[skater, t] = (double)1 - Ep;
					}
					else
					{
						prob[skater, t] = Ep / (double)N;

					}
				}
			}
			//updaten probabilities-array with proportional algorithm
			else
			{
				//berekenen sommatie
				double som = 0;
				for (int i = 0; i < n; i++)
				{
					som += Q[skater, i];
				}

				for (int t = 0; t < n; t++)
				{
					prob[skater, t] = Q[skater, t] / som;
				}
			}
            
			for (int t = 0; t<n; t++)
			{
				double r = 0;
				for (int s = 0; s<N; s++)
				{
					r += Q[s, t];
				}
				totalQ[t] = r/N;
			}
        }
	
        void teken(object obj, PaintEventArgs pea)
        {
            for (int i = 0; i < N; i++)
            {
				pea.Graphics.FillEllipse(Brushes.Red, (int)positie[i, 0], (int)positie[i, 1], dikte, dikte);
                this.Invalidate();
            }
        }
        
    }
}
