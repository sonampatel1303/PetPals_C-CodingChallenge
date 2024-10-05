using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Petpals.Model;
using System.Collections.Generic;
using Petpals.Utility;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;

namespace Petpals.Repository

{
    public class AdoptionEvent : IAdoptable
    {
        SqlConnection sqlConnection = null;
        SqlCommand cmd = null;

        public AdoptionEvent()
        {
            sqlConnection = new SqlConnection(DbConnUtil.GetConnString());
            cmd = new SqlCommand();
        }
        
        private List<Pet> Participants = new List<Pet>();

        public void RegisterParticipant(string name, int age, string breed)
        {
            // First, get the next available PetID
            string selectQuery = "SELECT ISNULL(MAX(PetID), 0) + 1 FROM Pets";

            SqlCommand cmd = new SqlCommand(selectQuery, sqlConnection);

            sqlConnection.Open(); 
            int nextPetID = (int)cmd.ExecuteScalar(); 
            sqlConnection.Close(); 

           
            string insertQuery = "INSERT INTO Pets (PetID, Name, Age, Breed, AvailableForAdoption) VALUES (@PetID, @Name, @Age, @Breed, 0)"; // 0 for not adopted

            cmd.CommandText = insertQuery;
            cmd.Parameters.Clear(); // Clear any existing parameters
            cmd.Parameters.AddWithValue("@PetID", nextPetID);
            cmd.Parameters.AddWithValue("@Name", name);
            cmd.Parameters.AddWithValue("@Age", age);
            cmd.Parameters.AddWithValue("@Breed", breed);

            sqlConnection.Open(); 
            cmd.ExecuteNonQuery(); 
            sqlConnection.Close(); 

            Console.WriteLine($"{name} registered for the adoption event with PetID {nextPetID}.");
        }

        public bool Adopt(int petID)
        {
            string updateQuery = "UPDATE Pets SET AvailableForAdoption = 0 WHERE PetID = @PetID AND AvailableForAdoption = 1";

            using (SqlConnection sqlConnection = new SqlConnection(DbConnUtil.GetConnString()))
            {
                try
                {
                    sqlConnection.Open(); 

                    using (SqlCommand cmd = new SqlCommand(updateQuery, sqlConnection))
                    {
                        cmd.Parameters.AddWithValue("@PetID", petID); 

                        int rowsAffected = cmd.ExecuteNonQuery(); 
                        return rowsAffected > 0;
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"SQL Error: {ex.Message}");
                    return false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    return false;
                }
            } 
        }


        public List<Pet> GetAvailablePets()
        {
            List<Pet> availablePets = new List<Pet>();

           
            string selectQuery = "SELECT PetID, Name, Age, Breed FROM Pets WHERE AvailableForAdoption = 1";
            SqlCommand cmd = new SqlCommand(selectQuery, sqlConnection);

            sqlConnection.Open(); 
            SqlDataReader reader = cmd.ExecuteReader(); 

            while (reader.Read())
            {
               
                Pet pet = new Pet
                {
                   
                    Name = (string)reader["Name"],
                    Age = (int)reader["Age"],
                    Breed = (string)reader["Breed"]
                };

                availablePets.Add(pet); 
            }

            reader.Close(); 
            sqlConnection.Close(); 

            return availablePets; 
        }


        public List<AdoptionEvents> GetUpcomingEvents()
        {
            List<AdoptionEvents> upcomingEvents = new List<AdoptionEvents>();

            string selectQuery = "SELECT EventName, EventDate, Location FROM AdoptionEvents"; 

            using (SqlConnection sqlConnection = new SqlConnection(DbConnUtil.GetConnString()))
            {
                sqlConnection.Open();

                using (SqlCommand cmd = new SqlCommand(selectQuery, sqlConnection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                           
                            AdoptionEvents eventDetails = new AdoptionEvents
                            {
                                EventName = (string)reader["EventName"],
                                EventDate = (DateTime)reader["EventDate"],
                                Venue = reader.IsDBNull(reader.GetOrdinal("Location")) ? null : (string)reader["Location"] // Handle possible nulls
                            };

                            upcomingEvents.Add(eventDetails);
                        }
                    }
                }
            } 

            return upcomingEvents;
        }


       
        public int GetNextParticipantId()
        {
            int nextId = 1; 

            string selectQuery = "SELECT ISNULL(MAX(ParticipanntID), 0) + 1 FROM participants";

            using (SqlConnection sqlConnection = new SqlConnection(DbConnUtil.GetConnString()))
            {
                sqlConnection.Open();
                using (SqlCommand cmd = new SqlCommand(selectQuery, sqlConnection))
                {
                    object result = cmd.ExecuteScalar();
                    if (result != null && int.TryParse(result.ToString(), out int maxId))
                    {
                        nextId = maxId;
                    }
                }
            }

            return nextId;
        }

        public bool RegisterEParticipant(int eventid, string participantName, string participantType)
        {
            int participantId = GetNextParticipantId(); 

            string insertQuery = "INSERT INTO participants (ParticipanntID, ParticipantName, ParticipantType,EventID) VALUES (@ParticipantID, @ParticipantName, @ParticipantType,@EventID)";

            using (SqlConnection sqlConnection = new SqlConnection(DbConnUtil.GetConnString()))
            {
                sqlConnection.Open(); 
                using (SqlCommand cmd = new SqlCommand(insertQuery, sqlConnection))
                {
                    cmd.Parameters.AddWithValue("@ParticipantID", participantId); 
                    cmd.Parameters.AddWithValue("@ParticipantName", participantName);
                    cmd.Parameters.AddWithValue("@ParticipantType", participantType);
                    cmd.Parameters.AddWithValue("@EventID", eventid);

                    int rowsAffected = cmd.ExecuteNonQuery(); 
                    return rowsAffected > 0;
                }
            }
        }

       
        public bool RecordDonation(CashDonation donor)
        {
            string insertQuery = "INSERT INTO Donations (DonationID, DonorName, DonationType, DonationAmount) VALUES (@DonationID, @DonorName, @DonationType, @DonationAmount)";
            string maxIdQuery = "SELECT ISNULL(MAX(DonationID), 0) FROM Donations"; 

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(DbConnUtil.GetConnString()))
                {
                    sqlConnection.Open(); 

                 
                    int newDonationId;
                    using (SqlCommand maxIdCmd = new SqlCommand(maxIdQuery, sqlConnection))
                    {
                        newDonationId = (int)maxIdCmd.ExecuteScalar() + 1; 
                    }

                    using (SqlCommand cmd = new SqlCommand(insertQuery, sqlConnection))
                    {
                        cmd.Parameters.AddWithValue("@DonationID", newDonationId); 
                        cmd.Parameters.AddWithValue("@DonorName", donor.DonorName);
                        cmd.Parameters.AddWithValue("@DonationType", "Cash");
                        cmd.Parameters.AddWithValue("@DonationAmount", donor.Amount);

                        int rowsAffected = cmd.ExecuteNonQuery(); 
                        return rowsAffected > 0; 
                    }
                }
            }
            catch (SqlException ex)
            {
               
                Console.WriteLine($"Database error: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }
        public bool HostEvent(string name,DateTime eventdate,string venue)
        {
         
            AdoptionEvents newevent= new AdoptionEvents();
            List<AdoptionEvents> upcomingEvents = new List<AdoptionEvents>();
            
            newevent.EventName = name;

            newevent.EventDate = eventdate;

            newevent.Venue = venue;

            upcomingEvents.Add(newevent);

         
            return true;
        }

    }
}



    




 


