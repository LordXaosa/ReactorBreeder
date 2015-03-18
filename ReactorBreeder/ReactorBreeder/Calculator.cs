using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ReactorBreeder
{
    public class Calculator
    {
        FastRandom rnd = new FastRandom();
        Random rnd2 = new Random(Environment.TickCount+(int)DateTime.Now.Ticks);
        RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
        HashSet<Group> groups = new HashSet<Group>();
        bool[, ,] reactors;
        public bool[, ,] reactorsMax;
        bool[, ,] reactorsChecked;
        public double maxEnergy = 0;
        public string result = "";
        public string resultPic = "";

        int maxx, maxy, maxz;

        public Calculator(int x, int y, int z, int max, int cycles)
        {
            maxx = x;
            maxy = y;
            maxz = z;
            maxEnergy = max;
            for (int i = 0; i < cycles; i++)
            {
                Calculate(x, y, z);
                Form1.current++;
            }
        }

        private void Calculate(int X, int Y, int Z)
        {
            groups = new HashSet<Group>();

            reactors = new bool[X, Y, Z];
            byte[] array = new byte[1024];
            rng.GetBytes(array);
            int divider = 0;
            do
            {
                divider = array[rnd.Next(1024)];
            } while (divider == 0);
            for (int x = 0; x < X; x++)
            {
                for (int y = 0; y < Y; y++)
                {
                    for (int z = 0; z < Z; z++)
                    {
                        reactors[x, y, z] = array[rnd.Next(1024)] < array[rnd2.Next(1024)]/divider;
                    }
                }
            }
            /*bool temp = rnd.Next(2000001) <= 1000000;
            for (int x = 0; x < X; x++)
            {
                for (int y = 0; y < Y; y++)
                {
                    for (int z = 0; z < Z; z++)
                    {
                        if (temp && !reactors[x, y, z] && rnd.Next(2000001) >= 1000000)
                        {
                            reactors[x, y, z] = true;
                        }
                    }
                }
            }*/

            reactorsChecked = new bool[X, Y, Z];
            for (int x = 0; x < X; x++)
            {
                for (int y = 0; y < Y; y++)
                {
                    for (int z = 0; z < Z; z++)
                    {
                        if (!reactorsChecked[x, y, z])
                        {
                            CheckGroup(x, y, z, null);
                        }
                    }
                }
            }
            double sum = 0;
            int blocks = 0;
            foreach (Group g in groups)
            {
                float dims = g.MaxX - g.MinX + g.MaxY - g.MinY + g.MaxZ - g.MinZ + 3;
                sum += (2000000.0f / (1.0f + Math.Pow(1.000696f, (-0.333f * Math.Pow((dims / 3.0f), 1.7)))) - 1000000.0f + 25.0f * g.Blocks);
                blocks += g.Blocks;
            }

            if (sum > maxEnergy)
            {
                maxEnergy = sum;

                reactorsMax = reactors;

                result = maxEnergy.ToString() + " (" + blocks + " blocks) (" + groups.Count + " groups)";
                StringBuilder sb = new StringBuilder();
                resultPic = "";

                for (int z = 0; z < Z; z++)
                {
                    for (int y = 0; y < Y; y++)
                    {
                        for (int x = 0; x < X; x++)
                        {
                            if (reactors[x, y, z])
                            {
                                sb.Append("⬛");
                            }
                            else
                            {
                                sb.Append("⛶");
                            }
                        }
                        sb.Append(Environment.NewLine);
                    }
                    sb.Append("======Level " + (z + 1) + "======" + Environment.NewLine);
                }
                resultPic = sb.ToString();
            }

        }
        public struct Point
        {
            public int X, Y, Z;
        }
        void CheckGroup(int x, int y, int z, Group group)
        {
            if (x >= maxz || x < 0 || y >= maxy || y < 0 || z >= maxz || z < 0)
            {
                return;
            }

            if (reactorsChecked[x, y, z])
            {
                return;
            }

            if (reactors[x, y, z])
            {
                if (group == null)
                {
                    group = new Group();
                    group.MaxX = x;
                    group.MaxY = y;
                    group.MaxZ = z;
                    group.MinX = x;
                    group.MinY = y;
                    group.MinZ = z;
                    group.Blocks = 0;
                }
                else
                {
                    group.MaxX = Math.Max(group.MaxX, x);
                    group.MaxY = Math.Max(group.MaxY, y);
                    group.MaxZ = Math.Max(group.MaxZ, z);
                    group.MinX = Math.Min(group.MinX, x);
                    group.MinY = Math.Min(group.MinY, y);
                    group.MinZ = Math.Min(group.MinZ, z);
                    group.Blocks += 1;
                }
                Stack<Point> points = new Stack<Point>();
                points.Push(new Point() { X = x, Y = y, Z = z });

                while (points.Count > 0)
                {
                    Point p = points.Pop();
                    if (p.X >= maxx || p.X < 0 || p.Y >= maxy || p.Y < 0 || p.Z >= maxz || p.Z < 0)
                    {
                        continue;
                    }
                    if (reactors[p.X, p.Y, p.Z] && !reactorsChecked[p.X, p.Y, p.Z])
                    {
                        group.Blocks++;
                        reactorsChecked[p.X, p.Y, p.Z] = true;
                        group.MaxX = Math.Max(group.MaxX, p.X);
                        group.MaxY = Math.Max(group.MaxY, p.Y);
                        group.MaxZ = Math.Max(group.MaxZ, p.Z);
                        group.MinX = Math.Min(group.MinX, p.X);
                        group.MinY = Math.Min(group.MinY, p.Y);
                        group.MinZ = Math.Min(group.MinZ, p.Z);

                        points.Push(new Point() { X = p.X + 1, Y = p.Y, Z = p.Z });
                        points.Push(new Point() { X = p.X - 1, Y = p.Y, Z = p.Z });

                        points.Push(new Point() { X = p.X, Y = p.Y + 1, Z = p.Z });
                        points.Push(new Point() { X = p.X, Y = p.Y - 1, Z = p.Z });

                        points.Push(new Point() { X = p.X, Y = p.Y, Z = p.Z + 1 });
                        points.Push(new Point() { X = p.X, Y = p.Y, Z = p.Z - 1 });
                    }

                }
                groups.Add(group);
                /*if (!groups.Contains(group))
                {
                    groups.Add(group);
                }*/
            }
        }

    }
}
