using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorio_9
{
    internal class Program
    {
        static void Main()
        {
            //Parte 1
            var tienda = new TiendaDonLucas();
            bool continuar = true;

            while (continuar)
            {
                Console.Clear();
                Console.Write("================================" +
                 "\nTienda de Don Lucas" +
                 "\n================================" +
                 "\n1: Registrar venta" +
                 "\n2: Registrar devolución" +
                 "\n3: Cerrar Caja" +
                 "\n================================" +
                 "\nIngrese una opción: ");
                int opcion = int.Parse(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        tienda.RegistrarVenta();
                        break;
                    case 2:
                        tienda.RegistrarDevolucion();
                        break;
                    case 3:
                        tienda.CerrarCaja();
                        break;
                    default:
                        continuar = false;
                        break;
                }
            }
        }
    }

    class TiendaDonLucas
    {
        public TiendaDonLucas()
        {
            productosVendidos["Limpieza"] = 0;
            productosVendidos["Abarrotes"] = 0;
            productosVendidos["Golosinas"] = 0;
            productosVendidos["Electrónicos"] = 0;

            productosDevueltos["Limpieza"] = 0;
            productosDevueltos["Abarrotes"] = 0;
            productosDevueltos["Golosinas"] = 0;
            productosDevueltos["Electrónicos"] = 0;

            inventario["Limpieza"] = 0;
            inventario["Abarrotes"] = 0;
            inventario["Golosinas"] = 0;
            inventario["Electrónicos"] = 0;

            cajaPorRubro["Limpieza"] = 0.0;
            cajaPorRubro["Abarrotes"] = 0.0;
            cajaPorRubro["Golosinas"] = 0.0;
            cajaPorRubro["Electrónicos"] = 0.0;
        }

        private Dictionary<string, int> productosVendidos = new Dictionary<string, int>();
        private Dictionary<string, int> productosDevueltos = new Dictionary<string, int>();
        private Dictionary<string, int> inventario = new Dictionary<string, int>();
        private Dictionary<string, double> cajaPorRubro = new Dictionary<string, double>();

        public void RegistrarVenta()
        {
            Console.Clear();
            Console.Write("================================" +
                     "\nRegistrar Venta de:" +
                     "\n================================" +
                     "\n1: Limpieza" +
                     "\n2: Abarrotes" +
                     "\n3: Golosinas" +
                     "\n4: Electrónicos" +
                     "\n5: <- Regresar" +
                     "\n================================" +
                     "\nIngrese una opción: ");
                     int opcion = int.Parse(Console.ReadLine());

            if (opcion >= 1 && opcion <= 4)
            {
                string rubro = GetRubro(opcion);
                Console.Clear();
                Console.Write("============================" +
                   "\nRegistrar Venta de " + rubro +
                   "\n============================" +
                   "\nIngrese cantidad (unidades): ");            
                int cantidad = int.Parse(Console.ReadLine());
                Console.Write("Ingrese Precio: S/.");
                double precio = double.Parse(Console.ReadLine());

                productosVendidos[rubro] += cantidad;
                inventario[rubro] += cantidad;
                cajaPorRubro[rubro] += cantidad * precio;

                Console.Clear();
                Console.WriteLine("Se han ingresado " +cantidad +" unidades");
                Console.WriteLine("Se han ingresado " + cantidad * precio +"en caja");
                Console.WriteLine("=======================");
                Console.WriteLine("1: Registrar más productos de " + rubro);
                Console.WriteLine("2: <- Regresar");
                Console.WriteLine("=======================");
                Console.Write("Ingrese una opción: ");
                int seguir = int.Parse(Console.ReadLine());

                if (seguir == 1)
                {
                    RegistrarVenta();
                }
            }
        }

        public void RegistrarDevolucion()
        {
            Console.Clear();
            Console.WriteLine("=======================");
            Console.WriteLine("Registrar Devolución de:");
            Console.WriteLine("=======================");
            Console.WriteLine("1: Limpieza");
            Console.WriteLine("2: Abarrotes");
            Console.WriteLine("3: Golosinas");
            Console.WriteLine("4: Electrónicos");
            Console.WriteLine("5: <- Regresar");
            Console.WriteLine("=======================");
            Console.Write("Ingrese una opción: ");
            int opcion = int.Parse(Console.ReadLine());

            //Parte 4 
            if (opcion >= 1 && opcion <= 4)
            {
                string rubro = GetRubro(opcion);
                Console.Clear();
                Console.WriteLine("=======================");
                Console.WriteLine("Registrar Devolución de "+ rubro);
                Console.WriteLine("=======================");
                Console.Write("Ingrese cantidad (Unidades): ");
                int cantidad = int.Parse(Console.ReadLine());
                Console.Write("Ingrese Precio: S/.");
                double precio = double.Parse(Console.ReadLine());

                productosDevueltos[rubro] += cantidad;
                inventario[rubro] -= cantidad;
                cajaPorRubro[rubro] -= cantidad * precio;

                Console.Clear();
                Console.WriteLine("Se han regresado "+ cantidad + " unidades");
                Console.WriteLine("Se han devuelto " + cantidad * precio + " de caja");
                Console.WriteLine("=======================");
                Console.WriteLine("1: Devolver más productos de " + rubro);
                Console.WriteLine("2: <- Regresar");
                Console.WriteLine("=======================");
                Console.Write("Ingrese una opción: ");
                int seguir = int.Parse(Console.ReadLine());

                if (seguir == 1)
                {
                    RegistrarDevolucion();
                }
            }
        }

        private string GetRubro(int opcion)
        {
            switch (opcion)
            {
                case 1:
                    return "Limpieza";
                case 2:
                    return "Abarrotes";
                case 3:
                    return "Golosinas";
                case 4:
                    return "Electrónicos";
                default:
                    return "";
            }
        }
        public void CerrarCaja()
        {
            Console.Clear();
            Console.WriteLine("=========================");
            Console.WriteLine("Cerrando Caja");
            Console.WriteLine("=========================");
            Console.WriteLine("Totales");
            Console.WriteLine("=========================");

            foreach (var rubro in productosVendidos.Keys)
            {
                Console.WriteLine($" {rubro,-9} | {productosVendidos[rubro],-2} vendidos");
                Console.WriteLine($" {"",-9} | {productosDevueltos[rubro],-2} devueltos");
                Console.WriteLine($" {"",-9} | {inventario[rubro],-2}  en total");
                Console.WriteLine($" {"",-9} | $/{cajaPorRubro[rubro],-2:f2} en caja");
                Console.WriteLine("=========================");
            }

            double totalCaja = cajaPorRubro.Values.Sum();
            Console.WriteLine("Queda en caja $/" + totalCaja.ToString("C"));
            Console.ReadLine();
        }
    }
}