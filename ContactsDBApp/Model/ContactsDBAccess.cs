using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsDBApp.Model
{
    class ContactsDBAccess
    {
        string connStr;
        MySqlConnection sqlConn;

        public ContactsDBAccess()
        {
            connStr = ConfigurationManager.ConnectionStrings["mySql"].ConnectionString;
            sqlConn = new MySqlConnection(connStr);
        }

        private string ExecuteQuery(string queryString)
        {
            string outputString = "";
            try
            {
                sqlConn.Open();
                MySqlCommand cmd = new MySqlCommand(queryString, sqlConn);

                MySqlDataReader reader = cmd.ExecuteReader();

                Console.WriteLine();

                while (reader.Read())
                {
                    for (var i = 0; i < reader.FieldCount; i++)
                    {
                        outputString += reader.GetString(i);
                        outputString += " ";
                    }
                    outputString += Environment.NewLine;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (sqlConn.State.ToString() == "Open")
                {
                    sqlConn.Close();
                }
            }
            return outputString;
        }

        public void AddContact(string name, string number)
        {
            this.ExecuteQuery("INSERT INTO CONTACTS(NAME, PHONE_NUMBER) VALUES ('" +
                name + "','" + number + "');");
        }

        public void DeleteContact(string name, string number)
        {
            this.ExecuteQuery("DELETE FROM CONTACTS WHERE NAME='" +
                name + "' AND PHONE_NUMBER='" + number + "';");
        }

        public string DisplayAllContacts()
        {
            return this.ExecuteQuery("SELECT * FROM CONTACTS;");
        }
    }
}
