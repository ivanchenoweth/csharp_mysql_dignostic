/*
 * Created by Ivan R. Chenoweth
 * Simple Windows Form for Diagnostic connection to MySQL
 * Show data in a TextBox from a database 'test' and a table named 'table1'.
 * Build with C# Develop Version 5.
 * MySQL dumped e at the bottom
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace mysqlDiagnosticWF
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		void Button1Click(object sender, EventArgs e)
		{
			MySqlCommand comando;
			MySqlDataReader lector;
			
			MySqlConnection conn;
			string myConnectionString;
			
			myConnectionString = "server=127.0.0.1;uid=root;" +
			"pwd=usbw;database=test;";
			try
			{
				conn = new MySqlConnection();
				conn.ConnectionString = myConnectionString;
				conn.Open();
				comando = new MySqlCommand("select * from table1",conn);
				lector=comando.ExecuteReader();
				textBox1.Text = "";
				string salida = "";
				while(lector.Read())
				{
					salida = lector.GetInt32("id") +" " + lector.GetString("name");
					textBox1.AppendText(salida +"\r\n");
				}
				lector.Close();
				conn.Close();							
			}
			catch (MySql.Data.MySqlClient.MySqlException ex)
			{
			MessageBox.Show(ex.Message);
			}
		}
	}
}
/*
-- Recomended for developers in windows: usbwebserver (root:usbw)
CREATE DATABASE IF NOT EXISTS `test` DEFAULT CHARACTER SET latin1 COLLATE latin1_swedish_ci;
USE `test`;

CREATE TABLE IF NOT EXISTS `table1` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(50) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=3 ;
INSERT INTO `table1` (`id`, `name`) VALUES
(1, 'Prof.. Chenoweth'),
(2, 'Prof. FLores');
*/
