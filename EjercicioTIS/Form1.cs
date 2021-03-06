﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace EjercicioTIS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            fill_listbox();
            boldDays();
        }

        void fill_listbox() { 
            string connString = "datasource=localhost;port=3306;username=root;password=";
            string Query = "SELECT * FROM agenda.clientes ;";
            MySqlConnection conDataBase = new MySqlConnection(connString);
            MySqlCommand cmdDatabase = new MySqlCommand(Query, conDataBase);
            MySqlDataReader myReader;

            try
            {
                conDataBase.Open();
                myReader = cmdDatabase.ExecuteReader();
                
                while (myReader.Read()){
                    string nombre = myReader.GetString("nombre");
                    string apellido = myReader.GetString("apellido");
                    listBox1.Items.Add(nombre + " " + apellido);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al intentar conectar con la base de datos");
            }
        }

        public void boldDays()
        {
            string connString = "datasource=localhost;port=3306;username=root;password=";
            string Query = "SELECT concat(fecha) FROM agenda.clientes";
            MySqlConnection conDataBase = new MySqlConnection(connString);
            MySqlCommand cmdDatabase = new MySqlCommand(Query, conDataBase);
            MySqlDataAdapter DataAdapter = new MySqlDataAdapter(Query, conDataBase);
            MySqlDataReader myReader;
            conDataBase.Open();

            DataSet ds = new DataSet();
            DataAdapter.Fill(ds, "agenda.clientes");

            int tam_arr = ds.Tables[0].Rows.Count; //Obteniendo el tamano del arreglo
            int i = 0;
            string[] arr = new string[tam_arr];

            foreach (DataRow dr in ds.Tables[0].Rows) //Recorriendo las filas
            {
                arr[i] = dr[0] + "";
                i++;
            }

            DateTime[] fechasMarcadas = new DateTime[arr.Length];

            for (i = 0; i < arr.Length; i++)
            {
                int ano, mes, dia;
                ano = Int32.Parse(arr[i].Substring(0, 4));
                mes = Int32.Parse(arr[i].Substring(5, 2));
                dia = Int32.Parse(arr[i].Substring(8, 2));

                fechasMarcadas[i] = new DateTime(ano, mes, dia);
            }

            monthCalendar1.BoldedDates = fechasMarcadas;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string connString = "datasource=localhost;port=3306;username=root;password=";
            string Query = "INSERT INTO agenda.clientes (nombre, apellido) VALUES('" + this.textBox1.Text + "','" + this.textBox2.Text + "');";
            MySqlConnection conDataBase = new MySqlConnection(connString);
            MySqlCommand cmdDatabase = new MySqlCommand(Query, conDataBase);
            MySqlDataReader myReader;

            if ((string.IsNullOrWhiteSpace(textBox1.Text)) && (string.IsNullOrWhiteSpace(textBox2.Text)))
            {
                MessageBox.Show("Favor de llenar los campos");
            }
            else {
                try
                {
                    conDataBase.Open();
                    myReader = cmdDatabase.ExecuteReader();
                    MessageBox.Show("Cliente Guardado");
                    while (myReader.Read())
                    {

                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Error al intentar conectar con la base de datos");
                }
                conDataBase.Close();
                listBox1.Items.Add(textBox1.Text + " " + textBox2.Text);
            }
            
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connString = "datasource=localhost;port=3306;username=root;password=";
            string Query = "UPDATE agenda.clientes SET fecha = '" + this.dateTimePicker1.Text + "' WHERE id = '" + (this.listBox1.SelectedIndex + 1) +"' ";
            string Query1 = "SELECT fecha from agenda.clientes ;";
            MySqlConnection conDataBase = new MySqlConnection(connString);
            MySqlCommand cmdDatabase = new MySqlCommand(Query, conDataBase);
            MySqlCommand cmdDatabase1 = new MySqlCommand(Query1, conDataBase);
            MySqlDataReader myReader;
            string fecha_ocupada = this.dateTimePicker1.Text;
            try
            {
                conDataBase.Open();
                myReader = cmdDatabase.ExecuteReader();
                MessageBox.Show("Cliente " + this.listBox1.SelectedItem.ToString() + " agendado para la fecha " + this.dateTimePicker1.Text);
                while (myReader.Read())
                {

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al intentar conectar con la base de datos");
            }
            conDataBase.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
        }
    }
}
