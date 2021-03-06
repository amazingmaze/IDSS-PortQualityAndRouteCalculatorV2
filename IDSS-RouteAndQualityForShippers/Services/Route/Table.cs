﻿using System;

namespace IDSS_RouteAndQualityForShippers.Services.Route
{
    /*
     * Simple Matrix class with basic functions
     */
    class Table : ICloneable
    {
        public static bool DEBUG = false;

        int _size;
        Double[,] _node_mat;
        public Double cost;
        public double[,] dmat
        {
            get { return _node_mat; }
            set { _node_mat = value; }
        }

        public int size
        {
            get { return _size; }
            set { _size = value; }
        }

        public Table(int size, int seed = 8)
        {
            this._size = size;
            _node_mat = new Double[size, size];
            Random gen = new Random(seed);
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    _node_mat[i, j] = gen.Next(100);
                }
            }
        }
        public Table(Double[,] tab)
        {
            this._size = tab.GetUpperBound(1) + 1;
            _node_mat = (Double[,])tab.Clone();
        }
        public void add2Column(int column, Double scalar)
        {
            rangecheck(column);
            for (int i = 0; i < _size; i++)
            {

                _node_mat[i, column] += scalar;

            }
        }
        public void add2row(int row, Double scalar)
        {
            rangecheck(row);
            for (int i = 0; i < _size; i++)
            {

                _node_mat[row, i] += scalar;

            }
        }
        public Double getMinCol(int column)
        {
            rangecheck(column);
            Double min = _node_mat[0, column];
            for (int i = 0; i < _size - 1; i++)
            {
                min = Math.Min(_node_mat[(i + 1), column], min);
            }
            return min;
        }
        public Double getMinrow(int row)
        {
            rangecheck(row);
            Double min = _node_mat[row, 0];
            for (int i = 0; i < _size - 1; i++)
            {
                min = Math.Min(_node_mat[row, (i + 1)], min);
            }
            return min;
        }
        public void setCol(int column, Double num)
        {
            rangecheck(column);
            for (int i = 0; i < _size; i++)
            {

                _node_mat[i, column] = num;

            }
        }
        public void setRow(int row, Double num)
        {
            rangecheck(row);
            for (int i = 0; i < _size; i++)
            {

                _node_mat[row, i] = num;

            }
        }
        public void rangecheck(int range)
        {
            try
            {
                if (range > _size - 1 || range < 0)
                    throw new IndexOutOfRangeException("argument out side range of table size");
            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine("", e);
            }

        }
        public void print()
        {
            for (int i = 0; i < _size; i++)
            {
                for (int j = 0; j < _size; j++)
                {
                    if (Double.IsPositiveInfinity(_node_mat[i, j]))
                    {
                        Console.Write("i");
                    }
                    else
                    {
                        System.Console.Write(_node_mat[i, j]);
                    }
                    System.Console.Write(" ");
                }
                System.Console.WriteLine("");
            }
            Console.WriteLine("cost : " + cost);
            Console.WriteLine("");
        }

        public object Clone()
        {
            Table t = new Table((Double[,])_node_mat.Clone());
            t.cost = 0;
            return t;
        }
    }
}
