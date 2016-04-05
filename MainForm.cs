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
using MySql.Data.MySqlClient;		// Added to use the myql APIs with csharp.
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
			MySqlCommand comando;				// 'comando' MysqlAPI Object var, used to execute querys
			MySqlDataReader lector;				// 'lector' MysqlAPI Object var, used to scan the SQL result
			MySqlConnection conn;				// 'conn' MysqlAPI Object var, used to make a connection
			string myConnectionString;			// 'myConnectionString' String var, used to string connection
			// String connection (host/IP, user, password) 
			myConnectionString = "server=127.0.0.1;uid=root;" +
			"pwd=usbw;database=test;";
			try						// Used to trow exception when if some internal error 
			{
				conn = new MySqlConnection();					// Exec. the connection
				conn.ConnectionString = myConnectionString;			// Exec. the string connection
				conn.Open();							// Opening connection
				comando = new MySqlCommand("select * from table1",conn);	// Exec. Query in the server
				lector=comando.ExecuteReader();					// get the result 
				textBox1.Text = "";						// Clean GUI 
				string salida = "";						// 
				while(lector.Read())						// Loop reading the results
				{
					salida = lector.GetInt32("id") +" " + lector.GetString("name");	// get string to show
					textBox1.AppendText(salida +"\r\n");				// next line
				}		
				lector.Close();							// Close result
				conn.Close();							// Close connection
			}
			catch (MySql.Data.MySqlClient.MySqlException ex)			// If error occurs 
			{
			MessageBox.Show(ex.Message);						// Show in the GUI popup the error
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
