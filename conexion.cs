using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Path = System.IO.Path;

namespace CarService
{
    #region uso de datos un usuario

    public class Login {


        public Login() { }

        [PrimaryKey,AutoIncrement]
        
        [MaxLength(20)]
        public int Id { set; get; }
        [MaxLength(10)]
        public string Usuario { get; set; }
        
        [MaxLength(20)]
        public string Password { get; set; }
    }

    #endregion


    #region Manejo de datos y conexion a DB

    public class Auxiliar 
    { 
        static object locker = new object();
        SQLiteConnection _connection;
        public Auxiliar()//Constructor Vacio 
        {

            _connection = ConectarBD();
            _connection.CreateTable<Login>();



        }   
        public SQLite.SQLiteConnection ConectarBD() 
        {
            SQLiteConnection conexionBaseDatos;
            string nombreArchivo = "Basedatos.db3";
            string ruta = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string completaRuta = Path.Combine(ruta, nombreArchivo);
            conexionBaseDatos = new SQLiteConnection(completaRuta);
            return conexionBaseDatos;

        }


        //Seleccionar 1 registro

        public Login SeleccionarUno(string NombreUsuario, string PasswordUsario) 
        {

            lock (locker) 
            {
                return _connection.Table<Login>().FirstOrDefault(x => x.Usuario == NombreUsuario && x.Password == PasswordUsario);

            }
        
        }

        //Seleccionar Varios

        public IEnumerable<Login> SeleccionarTodo() 
        {
            lock (locker) 
            {
                return (from i in _connection.Table<Login>() select i).ToList();

            
            }
        
        }

        //Save

        public int Guardar(Login registro) 
        {
            lock (locker) 
            {
                if (registro.Id == 0)
                {
                    return _connection.Insert(registro);

                }
                else 
                { 
                    return _connection.Update(registro);

                
                }
            
            }
        }

        //Eliminar

        public int Eliminar(int ID)
        { 
            lock(locker) 
            {
                return _connection.Delete<Login>(ID);

            }
        
        
        }

    }   

    #endregion

}