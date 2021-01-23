using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GrzesiukiewiczL4
{
    public class SG_DBManager
    {
        private static string SG_connectionString = ConfigurationManager.ConnectionStrings["SG_DB"].ConnectionString;
        //**********INSERTS**********
        public static void addRecord(string SG_filename, string SG_owner)
        {
            string SG_query = "INSERT INTO SG_filelist values(@SG_filename, @SG_owner, @SG_uploadTime);";
            using (SqlConnection SG_connection = new SqlConnection(SG_connectionString))
            {
                SqlCommand SG_cmd = new SqlCommand(SG_query, SG_connection);
                SG_cmd.Parameters.AddWithValue("SG_filename", SG_filename);
                SG_cmd.Parameters.AddWithValue("SG_owner", SG_owner);
                SG_cmd.Parameters.AddWithValue("SG_uploadTime", DateTime.Now.ToString("dd.MM.yyyy HH:mm"));
                
                try
                {
                    SG_connection.Open();
                    SG_cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e.Message);
                }
                finally
                {
                    SG_connection.Close();
                }
            }


        }

        //**********SELECTS**********
        public static ArrayList dbFileList()
        {
            ArrayList SG_filenames = new ArrayList();
            using (SqlConnection SG_connection = new SqlConnection(SG_connectionString))
            {
                string SG_query = "SELECT [SG_filename] from SG_filelist;";
                SqlCommand SG_cmd = new SqlCommand(SG_query, SG_connection);
                try
                {
                    SG_connection.Open();
                    SqlDataReader SG_reader = SG_cmd.ExecuteReader();
                    while (SG_reader.Read())
                    {
                        SG_filenames.Add(SG_reader[0]);
                    }
                    SG_reader.Close();
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e.Message);
                }
                finally
                {
                    SG_connection.Close();
                }
            }
            return SG_filenames;
        }

        public static int numberOfFilesForUser(string SG_username)
        {
            int SG_count = 0;
            using (SqlConnection SG_connection = new SqlConnection(SG_connectionString))
            {
                string SG_query = "SELECT * from SG_filelist WHERE ([SG_owner] = @SG_username);";
                SqlCommand SG_cmd = new SqlCommand(SG_query, SG_connection);
                SG_cmd.Parameters.AddWithValue("SG_username", SG_username);
                try
                {
                    SG_connection.Open();
                    SqlDataReader SG_reader = SG_cmd.ExecuteReader();
                    while (SG_reader.Read())
                    {
                        SG_count++;
                    }
                    SG_reader.Close();
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e.Message);
                }
                finally
                {
                    SG_connection.Close();
                }
            }
            return SG_count;
        }
        public static bool userExists(string SG_userToCheck)
        {
            bool SG_ifExists = false;
            using (SqlConnection SG_connection = new SqlConnection(SG_connectionString))
            {
                string SG_query = "SELECT [SG_userID] from SG_users;";
                SqlCommand SG_cmd = new SqlCommand(SG_query, SG_connection);
                try
                {
                    SG_connection.Open();
                    SqlDataReader SG_reader = SG_cmd.ExecuteReader();
                    while (SG_reader.Read())
                    {
                        SG_ifExists = SG_userToCheck.Equals((string)SG_reader[0]);
                    }
                    SG_reader.Close();
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e.Message);
                }
                finally
                {
                    SG_connection.Close();
                }
            }
            return SG_ifExists;
        }

        public static ArrayList getUserList(string SG_activeUser)
        {
            bool SG_allUsers = SG_activeUser.Equals("all");
            ArrayList SG_userList = new ArrayList();
            using (SqlConnection SG_connection = new SqlConnection(SG_connectionString))
            {
                SG_connection.Open();
                string SG_query = SG_allUsers
                    ? "SELECT SG_userID FROM SG_users where SG_isActive = 1"
                    : "SELECT SG_userID FROM SG_users where SG_isActive = 1 AND NOT SG_userID = @SGuserID";
                SqlCommand SG_cmd = new SqlCommand(SG_query, SG_connection);
                if (!SG_allUsers) SG_cmd.Parameters.AddWithValue("SGuserID", SG_activeUser);
                try
                {
                    SqlDataReader SG_reader = SG_cmd.ExecuteReader();
                    while (SG_reader.Read())
                    {
                        string SG_usr = (string)SG_reader[0];
                        SG_userList.Add(SG_usr);
                    }
                    SG_reader.Close();
                }
                catch (SqlException e)
                {
                    System.Diagnostics.Debug.WriteLine(e.Message);
                }
                finally
                {
                    SG_connection.Close();
                }

            }
            if (SG_userList.Count == 0)
            {
                SG_userList.Add("Brak użytkowników");
            }
            return SG_userList;
        }

        public static bool ifLogged(string SG_userToCheck)
        {
            bool SG_isLogged = false;
            using (SqlConnection SG_connection = new SqlConnection(SG_connectionString))
            {
                string SG_query = "SELECT SG_userID from SG_users where SG_isActive = 1;";
                SqlCommand SG_cmd = new SqlCommand(SG_query, SG_connection);
                try
                {
                    SG_connection.Open();
                    SqlDataReader SG_reader = SG_cmd.ExecuteReader();
                    while (SG_reader.Read())
                    {
                        string SG_readUser = (string)SG_reader[0];
                        SG_isLogged = SG_userToCheck.Equals(SG_readUser);
                    }
                    SG_reader.Close();
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e.Message);
                }
                finally
                {
                    SG_connection.Close();
                }
            }
            return SG_isLogged;
        }

        //**********UPDATES**********

        public static void changeLoggedState(bool SG_logged, string SG_userToChange)
        {
            using (SqlConnection SG_connection = new SqlConnection(SG_connectionString))
            {
                string SG_query = "UPDATE SG_users SET [SG_isActive] = @SG_logged WHERE SG_userID = @SG_userID; ";
                SqlCommand SG_cmd = new SqlCommand(SG_query, SG_connection);
                SG_cmd.Parameters.AddWithValue("SG_logged", SG_logged ? 1 : 0);
                SG_cmd.Parameters.AddWithValue("SG_userID", SG_userToChange);
                try
                {
                    SG_connection.Open();
                    SG_cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e.Message);
                }
                finally
                {
                    SG_connection.Close();
                }
            }
        }

        public static void deactivateUsers()
        {
            using (SqlConnection SG_connection = new SqlConnection(SG_connectionString))
            {
                string SG_query = "UPDATE SG_users SET SG_isActive = 0;";
                SqlCommand SG_cmd = new SqlCommand(SG_query, SG_connection);
                try
                {
                    SG_connection.Open();
                    SG_cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e.Message);
                }
                finally
                {
                    SG_connection.Close();
                }
            }
        }

        public static void updateCounters(string SG_username, bool SG_resizeOrChange)
        {
            using (SqlConnection SG_connection = new SqlConnection(SG_connectionString))
            {
                // SG_resizeOrChange = true - resize ++, false - change ++
                string SG_query = "UPDATE SG_users SET " +
                    (SG_resizeOrChange ? "[SG_imageResized] = SG_imageResized + 1 " : "[SG_imageChanged] = SG_imageChanged + 1 ") +
                    "WHERE ([SG_userID] = @SG_userID);";
                SqlCommand SG_cmd = new SqlCommand(SG_query, SG_connection);
                SG_cmd.Parameters.AddWithValue("SG_userID", SG_username);
                try
                {
                    SG_connection.Open();
                    SG_cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e.Message);
                }
                finally
                {
                    SG_connection.Close();
                }
            }
        }

        //**********DELETES**********

        public static void deleteFileRecord(string SG_filename)
        {
            using (SqlConnection SG_connection = new SqlConnection(SG_connectionString))
            {
                string SG_query = "DELETE from SG_filelist WHERE ([SG_filename] = @SG_filename);";
                SqlCommand SG_cmd = new SqlCommand(SG_query, SG_connection);
                SG_cmd.Parameters.AddWithValue("SG_filename", SG_filename);
                try
                {
                    SG_connection.Open();
                    SG_cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e.Message);
                }
                finally
                {
                    SG_connection.Close();
                }
            }
        }
    }
}