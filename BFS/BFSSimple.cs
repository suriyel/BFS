﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFS
{
    public class BFSSimple<T>
    {
        private T[,] m_Map;
        private Func<T, bool> m_GridAvailableHandle;
        private int m_RowCount;
        private int m_ColCount;

        public BFSSimple(T[,] map, Func<T, bool> gridAvailableHandle)
        {
            m_Map = map;
            m_RowCount = map.GetLength(0);
            m_ColCount = map.GetLength(1);
            m_GridAvailableHandle = gridAvailableHandle;
        }

        public List<IPoint> FindPath(int startX, int startY, int endX, int endY)
        {
            var startPoint = new Point(startX, startY);
            var endPoint = new Point(endX, endY);
            bool[,] color = new bool[m_RowCount, m_ColCount];

            var result = new List<IPoint>();
            var queue = new Queue<Point>();
            queue.Enqueue(startPoint);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                //不能是已经染色的
                if (color[current.X, current.Y])
                {
                    continue;
                }

                //进行染色
                color[current.X, current.Y] = true;

                //是否是目标
                if (IsReachTarget(current, endPoint))
                {
                    while (current.Pre != null)
                    {
                        result.Add(current);
                        current = current.Pre;
                    }

                    //根节点也要添加进去
                    result.Add(current);
                    break;
                }

                //4个方向添加栅格
                EnqueueNextPoint(current, current.X - 1, current.Y, color, queue);
                EnqueueNextPoint(current, current.X + 1, current.Y, color, queue);
                EnqueueNextPoint(current, current.X, current.Y - 1, color, queue);
                EnqueueNextPoint(current, current.X, current.Y + 1, color, queue);
            }
            result.Reverse();
            return result;
        }

        private bool IsReachTarget(Point current, Point endPoint)
        {
            return current.X == endPoint.X && current.Y == endPoint.Y;
        }

        private void EnqueueNextPoint(Point current, int nextX, int nextY, bool[,] color, Queue<Point> queue)
        {
            if (IsPositionValid(nextX, nextY))
            {
                var nextPoint = new Point(nextX, nextY);
                //下个节点需要未被染色的以及可达的
                if (!color[nextPoint.X, nextPoint.Y] && m_GridAvailableHandle(m_Map[nextPoint.X, nextPoint.Y]))
                {
                    queue.Enqueue(nextPoint);

                    nextPoint.Pre = current;
                }
            }
        }

        private bool IsPositionValid(int x, int y)
        {
            return x >= 0 && x < m_RowCount && y >= 0 && y < m_ColCount;
        }

        private void ResetDistance(int[,] distance, Point startPoint)
        {
            for (int i = 0; i < m_RowCount; i++)
            {
                for (int j = 0; j < m_ColCount; j++)
                {
                    distance[i, j] = int.MaxValue;
                }
            }

            distance[startPoint.X, startPoint.Y] = 0;
        }
    }
}
