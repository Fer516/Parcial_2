using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empleados
{
    class Empleados
    {
        
        private string nombre;
        private string dui;
        private double salario;
        private double afp;
        private double iss;
        private string codigo;
        private double descuento;
        private double salarioliquido;


        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
        public string Dui
        {
            get { return dui; }
            set { dui = value; }
        }

        public string Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }
        public double Salario
        {
            get { return salario; }
            set { salario = value; }
        }

        public double Iss
        {
            get { return iss; }
            set { iss = value; }
        }
        public double Descuento
        {
            get { return descuento; }
            set { descuento = value; }
        }

        public double Salarioliquido
        {
            get { return salarioliquido; }
            set { salarioliquido = value; }
        }
        public double   Afp
        {
            get { return afp; }
            set { afp = value; }
        }


        public double AFP(double Salario)
        
        {
            double CalcAFP = Salario * 0.0625;
            return CalcAFP;
        }

        public double ISSS(double Salario)
        {
            double CalcIss = Salario * 0.03;
            return CalcIss;
        }
        public double DESCUENTO(double AFP, double ISS)
        {
            double CalcDes = AFP + ISS;
            return CalcDes;
        }
        public double SALARIOLIQUIDO(double Salario, double DESCUENTO)
        {
            double CalcDes = Salario - DESCUENTO;
            return CalcDes;
        }

    }
}
