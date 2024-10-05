using Petpals.Exceptions;
using Petpals.Model;
using Petpals.Repository;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petpals.MainModule
{
        public class MainMenu
        {
            private AdoptionEvent adoptionEvent;

           
            public MainMenu()
            {
                adoptionEvent = new AdoptionEvent();
            }

            public void DisplayMenu()
            {
                bool exit = false;

                while (!exit)
                {
                    Console.WriteLine("\n---- Pet Adoption Menu ----");
                    Console.WriteLine("1. Register a Pet for Adoption");
                    Console.WriteLine("2. List Available Pets");
                    Console.WriteLine("3. Adopt a Pet");
                   Console.WriteLine("4. List of Adoption Event");
                   Console.WriteLine("5. Register for an event");
                Console.WriteLine("6. Record a donation");
                Console.WriteLine("7. Host an Event");
                Console.WriteLine("8. Exit");
                    Console.Write("Please select an option: ");

                    int choice;
                    bool validChoice = int.TryParse(Console.ReadLine(), out choice);

                    if (validChoice)
                    {
                        switch (choice)
                        {
                            case 1:
                                RegisterPet();
                                break;
                            case 2:
                                ListAvailablePets();
                                break;
                            case 3:
                                Adopt();
                                break;
                        case 4:
                            GetUpcomingEvents();
                            break;
                        case 5:
                            RegisterEparticipant();
                            break;
                            case 6:
                            RecordDonation();
                            break;
                        case 7:
                            HostEvent();
                            break;
                            case 8:
                                exit = true;
                                Console.WriteLine("Exiting...");
                                break;
                            default:
                                Console.WriteLine("Invalid choice. Please select again.");
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Please enter a valid number.");
                    }
                }
            }

        private void HostEvent()
        {
            Console.Write("Enter the event name: ");
            string name=Console.ReadLine();
            Console.WriteLine("Enter date");
            DateTime datetime = DateTime.Parse(Console.ReadLine());
            Console.Write("Enter the event location: ");
            string location=Console.ReadLine();
            if (adoptionEvent.HostEvent(name, datetime, location))
            {
                Console.WriteLine("Event added successfully");
            }
            else
            {
                Console.WriteLine("Unexpected error");
            }
        }

        private void RecordDonation()
        {
            try
            {
                Console.Write("Enter donor name: ");
                string donorName = Console.ReadLine();
                Console.WriteLine("Enter date");
                DateTime donationDate = DateTime.Now;

                Console.Write("Enter donation amount: ");
                decimal donationAmount;

                // Validate and parse the donation amount
                while (true)
                {
                    if (decimal.TryParse(Console.ReadLine(), out donationAmount) && donationAmount > 0)
                    {
                        // Check for minimum donation amount
                        if (donationAmount < 10)
                        {
                            throw new InsufficientFundsException("Donation amount must be at least $10.");
                        }

                        Console.WriteLine("Donated Successfully");
                        break; 
                    }
                    else
                    {
                        Console.Write("Please enter a valid positive donation amount: ");
                    }
                }

              
                CashDonation donor = new CashDonation(donorName, donationAmount, donationDate);

              
            }
            catch (InsufficientFundsException ex)
            {
               
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (FormatException ex)
            {
                
                Console.WriteLine("Error: Please enter a valid donation amount.");
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
        }



        private void RegisterEparticipant()
        {
            Console.WriteLine("Enter event id");
            int eventid = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter participant name");
            string name=Console.ReadLine();
            Console.WriteLine("Enter participant type");
            string type = Console.ReadLine();
            bool isRegisterd = adoptionEvent.RegisterEParticipant(eventid,name,type);
            if (isRegisterd)
            {
                Console.WriteLine($"Registration Successful with name :{name}");
            }

        }

        private void GetUpcomingEvents()
        {
            List<AdoptionEvents> lists =adoptionEvent.GetUpcomingEvents();
            foreach(var item in lists)
            {
                Console.WriteLine($"Event name :{item.EventName}");
                Console.WriteLine($"Event date :{item.EventDate}");
                Console.WriteLine($"Event venue :{item.Venue}");
            }
        }

       
        private void RegisterPet()
        {
            try
            {
                Console.Write("Enter Pet Name: ");
                string name = Console.ReadLine();

                Console.Write("Enter Pet Age: ");
              
                if (!int.TryParse(Console.ReadLine(), out int age) || age <= 0)
                {
                    throw new InvalidPetAgeException("Pet age must be a positive integer.");
                }

                Console.Write("Enter Pet Breed: ");
                string breed = Console.ReadLine();

              
                adoptionEvent.RegisterParticipant(name, age, breed);
            }
            catch (InvalidPetAgeException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
            
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
        }

    
        private void ListAvailablePets()
        {
            List<Pet> availablePets = adoptionEvent.GetAvailablePets();

            Console.WriteLine("\n--- Available Pets ---");
            if (availablePets.Count == 0)
            {
                Console.WriteLine("No pets available for adoption.");
            }
            else
            {
                foreach (var pet in availablePets)
                {
                    try
                    {
                        
                        if (pet == null)
                        {
                            throw new NullReferencePetException("Pet object is null.");
                        }

                      
                        if (string.IsNullOrEmpty(pet.Name))
                        {
                            throw new NullReferencePetException("Pet Name is null or empty.");
                        }
                        if (pet.Age == null)
                        {
                            throw new NullReferencePetException("Pet Age is null.");
                        }
                        if (string.IsNullOrEmpty(pet.Breed))
                        {
                            throw new NullReferencePetException("Pet Breed is null or empty.");
                        }

                      
                        Console.WriteLine($"Name: {pet.Name}, Age: {pet.Age}, Breed: {pet.Breed}");
                    }
                    catch (NullReferencePetException ex)
                    {
                       
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
            }
            Console.WriteLine();
        }


     

        private void Adopt()
            {
               
            Console.Write("Enter the Pet ID of the pet you want to adopt: ");
            int petID = int.Parse(Console.ReadLine());

            bool success = adoptionEvent.Adopt(petID);

            if (success)
            {
                Console.WriteLine("Congratulations! You have adopted the pet.");
            }
            else
            {
                Console.WriteLine("Sorry, the pet could not be adopted. Please check the Pet ID.");
            }
            Console.WriteLine();

        }

        }
    }


