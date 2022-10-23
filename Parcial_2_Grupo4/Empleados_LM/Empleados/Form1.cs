using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Empleados
{
    public partial class Form1 : Form
    {
      
        Empleados Empleado = new Empleados();
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void label1_Click_2(object sender, EventArgs e)
        {

        }

        private void btAFP_Click(object sender, EventArgs e)
        {
            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text == "")
            {
                errorProvider1.SetError(txtNombre, "No ingresó el nombre");
                txtNombre.Focus();
                return;
            }

            errorProvider1.SetError(txtNombre, "");

            if (txtDUI.Text == "")
            {
                errorProvider1.SetError(txtDUI, "No ingresó el DUI");
                txtNombre.Focus();
                return;
            }

            errorProvider1.SetError(txtNombre, "");


            double Salario;

            if (!double.TryParse(txtSalario.Text, out Salario))

            {
                errorProvider1.SetError(txtSalario, "No ingresó el salario de forma correcta");
                txtSalario.Focus();
                return;
            }
            errorProvider1.SetError(txtSalario, "");



            Empleado.Nombre = txtNombre.Text;
            Empleado.Dui = txtDUI.Text;
            Empleado.Salario = Convert.ToDouble(txtSalario.Text);
            Empleado.Afp = Empleado.AFP(Empleado.Salario);
            txtAFP.Text = Empleado.AFP(Empleado.Salario).ToString();
            Empleado.Codigo = txtCodigo.Text;
            Empleado.Iss = Empleado.ISSS(Empleado.Salario);
            Empleado.Descuento = Empleado.DESCUENTO(Empleado.Afp, Empleado.Iss);
            Empleado.Salarioliquido = Empleado.SALARIOLIQUIDO(Empleado.Salario, Empleado.Descuento);
            labelRegistro.Text = "¡Registro guardado!";

        SqlConnection conexion = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Planilla.mdf;Integrated Security=True;Connect Timeout=30");
            conexion.Open();
            
            string cadena = "insert into [Empleados] (Nombre, DUI, Salario, AFP, Codigo, Iss, Salarioliquido, Descuento) values ('" + Empleado.Nombre + "','" + Empleado.Dui + "','" + Empleado.Salario + "','" + Empleado.Afp + "','" + Empleado.Codigo + "','" + Empleado.Iss + "','" + Empleado.Salarioliquido + "','" + Empleado.Descuento + "')";
            SqlCommand comando = new SqlCommand(cadena, conexion);
            comando.ExecuteNonQuery();
            MessageBox.Show("Los datos se guardaron correctamente");
            
            conexion.Close();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection conexion = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Planilla.mdf;Integrated Security=True;Connect Timeout=30");
            conexion.Open();

            Int32 cod;

            if (!Int32.TryParse(textBox1.Text, out cod))

            {
                errorProvider1.SetError(textBox1, "No ingresó el salario de forma correcta");
                txtSalario.Focus();
                return;
            }
            errorProvider1.SetError(textBox1, "");


            string cadena = "select id, nombre, dui, salario, afp, codigo, iss, salarioliquido, descuento from Empleados where Id=" + cod;
            SqlCommand comando = new SqlCommand(cadena, conexion);
            SqlDataReader registro = comando.ExecuteReader();
            if (registro.Read())
            {

                dataGridView1.Rows[0].Cells[0].Value = registro.GetInt32(0);
                dataGridView1.Rows[0].Cells[1].Value = registro.GetString(1);
                dataGridView1.Rows[0].Cells[2].Value = registro.GetString(2);
                dataGridView1.Rows[0].Cells[3].Value = registro.GetDecimal(3);
                dataGridView1.Rows[0].Cells[4].Value = registro.GetDecimal(4);
                dataGridView1.Rows[0].Cells[5].Value = registro.GetString(5);
                dataGridView1.Rows[0].Cells[6].Value = registro.GetDecimal(6);
                dataGridView1.Rows[0].Cells[7].Value = registro.GetDecimal(7);
                dataGridView1.Rows[0].Cells[8].Value = registro.GetDecimal(8);
            }
            else
                MessageBox.Show("No existe un Empleado con el código ingresado");
            conexion.Close();

        }

        private void button3_Click(object sender, EventArgs e)
        {

            SqlConnection conexion = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Planilla.mdf;Integrated Security=True;Connect Timeout=30");
            conexion.Open();
            try
            {
                string cod = dataGridView1.CurrentRow.Cells[0].Value.ToString();

                string Dnombre = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                string Ddui = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                string Dsalario = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                string DAFP = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                string Dcodigo = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                string Diss = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                string Dsalarioliquido = dataGridView1.CurrentRow.Cells[7].Value.ToString();
                string Ddescuento = dataGridView1.CurrentRow.Cells[8].Value.ToString();

                string cadena = "update [Empleados] set nombre='" + Dnombre + "', dui='" + Ddui + "', salario='" + Dsalario + "', afp='" + DAFP + "',  codigo='" + Dcodigo + "',  iss='" + Diss + "',  salarioliquido='" + Dsalarioliquido + "',  descuento='" + Ddescuento + "'  where Id=" + cod;
                SqlCommand comando = new SqlCommand(cadena, conexion);
                int cant;
                cant = comando.ExecuteNonQuery();
                if (cant == 1)
                {
                    this.dataGridView1.DataSource = null;
                    this.dataGridView1.Rows.Clear();
                    MessageBox.Show("Se modificaron los datos del empleado");
                }
            }
            catch (Exception) { MessageBox.Show("Debe seleccionar un registro"); }
            conexion.Close();

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            SqlConnection conexion = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Planilla.mdf;Integrated Security=True;Connect Timeout=30");
            conexion.Open();

            string cadena = "select id, nombre, dui, salario, afp, codigo, iss, salarioliquido, descuento from Empleados";
            SqlCommand comando = new SqlCommand(cadena, conexion);
            SqlDataReader registro = comando.ExecuteReader();
            this.dataGridView1.DataSource = null;
            this.dataGridView1.Rows.Clear();
            if (registro.HasRows)
            {
                while (registro.Read())
                {
                    int n = dataGridView1.Rows.Add();

                    dataGridView1.Rows[n].Cells[0].Value = registro.GetInt32(0);
                    dataGridView1.Rows[n].Cells[1].Value = registro.GetString(1);
                    dataGridView1.Rows[n].Cells[2].Value = registro.GetString(2);
                    dataGridView1.Rows[n].Cells[3].Value = registro.GetDecimal(3);
                    dataGridView1.Rows[n].Cells[4].Value = registro.GetDecimal(4);
                    dataGridView1.Rows[n].Cells[5].Value = registro.GetString(5);
                    dataGridView1.Rows[n].Cells[6].Value = registro.GetDecimal(6);
                    dataGridView1.Rows[n].Cells[7].Value = registro.GetDecimal(7);
                    dataGridView1.Rows[n].Cells[8].Value = registro.GetDecimal(8);
                }
            }
            else
                MessageBox.Show("No existen un registros");
            conexion.Close();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection conexion = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Planilla.mdf;Integrated Security=True;Connect Timeout=30");
            conexion.Open();
            try
            {
                string cod = dataGridView1.CurrentRow.Cells[0].Value.ToString();

                string cadena = "delete from [Empleados] where Id=" + cod;
                SqlCommand comando = new SqlCommand(cadena, conexion);


                int cant;
                cant = comando.ExecuteNonQuery();
                if (cant == 1)
                {
                    this.dataGridView1.DataSource = null;
                    this.dataGridView1.Rows.Clear();
                    MessageBox.Show("Se borró el registro");
                }

            }
            catch (Exception) { MessageBox.Show("Debe seleccionar un registro"); }
            conexion.Close();

        }
        private void buttom3_Click(object sender, EventArgs e)
        {
            SqlConnection conexion = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Planilla.mdf;Integrated Security=True;Connect Timeout=30");
            conexion.Open();
            try
            {
                string cod = dataGridView1.CurrentRow.Cells[0].Value.ToString();

                string cadena = "delete from [Empleados] where Id=" + cod;
                SqlCommand comando = new SqlCommand(cadena, conexion);


                int cant;
                cant = comando.ExecuteNonQuery();
                if (cant == 1)
                {
                    this.dataGridView1.DataSource = null;
                    this.dataGridView1.Rows.Clear();
                    MessageBox.Show("Se borró el registro");
                }

            }
            catch (Exception) { MessageBox.Show("Debe seleccionar un registro"); }
            conexion.Close();

        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            SqlConnection conexion = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Planilla.mdf;Integrated Security=True;Connect Timeout=30");
            conexion.Open();
            try
            {
                string cod = dataGridView1.CurrentRow.Cells[0].Value.ToString();

                string Dnombre = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                string Ddui = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                string Dsalario = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                string DAFP = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                string Dcodigo = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                string Diss = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                string Dsalarioliquido = dataGridView1.CurrentRow.Cells[7].Value.ToString();
                string Ddescuento = dataGridView1.CurrentRow.Cells[8].Value.ToString();

                string cadena = "update [Empleados] set nombre='" + Dnombre + "', dui='" + Ddui + "', salario='" + Dsalario + "', afp='" + DAFP + "' ,  codigo='" + Dcodigo + "',  iss='" + Diss + "',  salarioliquido='" + Dsalarioliquido + "',  descuento='" + Ddescuento + "'  where Id="  + cod;
                SqlCommand comando = new SqlCommand(cadena, conexion);
                int cant;
                cant = comando.ExecuteNonQuery();
                if (cant == 1)
                {
                    this.dataGridView1.DataSource = null;
                    this.dataGridView1.Rows.Clear();
                    MessageBox.Show("Se modificaron los datos del empleado");
                }
            }
            catch (Exception) { MessageBox.Show("Debe seleccionar un registro"); }
            conexion.Close();

        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void txtSalario_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtAFP_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDUI_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {

        }

        private void labelDUI_Click(object sender, EventArgs e)
        {

        }

        private void labelNombre_Click(object sender, EventArgs e)
        {

        }

        private void btnconsultarco_Click(object sender, EventArgs e)
        {
            SqlConnection conexion = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Planilla.mdf;Integrated Security=True;Connect Timeout=30");
            conexion.Open();
            try
            {
                string cod = dataGridView1.CurrentRow.Cells[5].Value.ToString();

                string cadena = "delete from [Empleados] where Id=" + cod;
                SqlCommand comando = new SqlCommand(cadena, conexion);


                int cant;
                cant = comando.ExecuteNonQuery();
                if (cant == 1)
                {
                    this.dataGridView1.DataSource = null;
                    this.dataGridView1.Rows.Clear();
                    MessageBox.Show("Se borró el registro");
                }


            }
            catch (Exception) { MessageBox.Show("Debe seleccionar un registro"); }
            conexion.Close();
        }
    }
}

