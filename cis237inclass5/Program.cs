using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cis237inclass5
{
    class Program
    {
        static void Main(string[] args)
        {
            // Make new instance of the Cars Collection
            CarsAStaffenEntities carsTestEntities = new CarsAStaffenEntities();

            //*************************************
            //List out all of the cars in the table
            //*************************************

            Console.WriteLine("Print out the list");

            foreach(Car car in carsTestEntities.Cars)
            {
                Console.WriteLine(car.id + " " + car.make + " " + car.model);
            }

            //*************************************
            // Find a specific one by any property
            //*************************************


            // Call the Where method on the table Cars and pass in a lambda expression
            // for the criteria we are looking for. There is nothing special about the word
            // car in the part that reads: car => car.id == "V0...." It could be any characters
            // we want it to be. It is just a variable name for the current car we are considering
            // in the expression. This will automagically loop through all the Cars and run the
            // expression against each of them. When the result is finally true, it will return
            // that car. 
            Car carToFind = carsTestEntities.Cars.Where(car => car.id == "V0LCD1814").First();

            // We can look for a specific model from the database. With a where clause based on any
            // criteria we want, we can narrow it down. Here we are doing it with the Car's model
            // instead of its id. 

            Car otherCarToFind = carsTestEntities.Cars.Where(car => car.model == "Challenger").First();

            // Print them out.
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Find 2 specific cars");
            Console.WriteLine(carToFind.id + " " + carToFind.make + " " + carToFind.model);
            Console.WriteLine(otherCarToFind.id + " " + otherCarToFind.make + " " + otherCarToFind.model);

            //************************************
            // Find a car based on the primary key
            //************************************

            // Pull out a car from the table based on the id which is the primary key
            // If the record doesn't exist in the database, it will return null, so check
            // what you get back and see if it is null. If so, it doesn't exist. 
            Car foundCar = carsTestEntities.Cars.Find("V0LCD1814");

            // Print it out
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Print out a found car using the Find Method.");
            Console.WriteLine(foundCar.id + " " + foundCar.make + " " + foundCar.model);

            //********************************
            // Add a new Car to the database.
            //********************************

            // Make an instance of a new car
            Car newCarToAdd = new Car();

            // Assign properties to the parts of the model
            newCarToAdd.id = "88888";
            newCarToAdd.make = "Nissan";
            newCarToAdd.model = "GT-R";
            newCarToAdd.horsepower = 550;
            newCarToAdd.cylinders = 8;
            newCarToAdd.year = "2016";
            newCarToAdd.type = "Car";


            
            // Use a try catch to ensure that they can't add a car with an id that already exists.
            try
            {
                // Add the new car to the Cars Collection.
                carsTestEntities.Cars.Add(newCarToAdd);

                // This method call actually does the work of saving the changes to the database.
                carsTestEntities.SaveChanges();
            }

            catch (Exception e)
            {
                // Remove the new car from the Cars Collection since we can't save it. 
                carsTestEntities.Cars.Remove(newCarToAdd);

                Console.WriteLine("Can't add the record. Already have one with that primary key.");
            }
            

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Just added a new car. Going to fetch it and print it to verify");
            carToFind = carsTestEntities.Cars.Find("88888");
            Console.WriteLine(carToFind.id + " " + carToFind.make + " " + carToFind.model);

            //*********************
            // How to do an update
            //*********************

            // Get a car out of the database that we would like updated.
            Car carToFindForUpdate = carsTestEntities.Cars.Find("88888");

            // Output the car to find.
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("About to do an update on a car.");

            Console.WriteLine(carToFindForUpdate.id + " " + carToFindForUpdate.make + " " + carToFindForUpdate.model);
            Console.WriteLine("Doing the update now.");



            // Update some of the properties of the car we found. Don't need to update all of
            // them if we don't want to. I chose these 4.

            carToFindForUpdate.make = "Nissan";
            carToFindForUpdate.model = "GT-RRRRRRR";
            carToFindForUpdate.horsepower = 1250;
            carToFindForUpdate.cylinders = 16;

            // Save the changes to the database. Since when we pulled out th eone to update, all
            // we were really doing was getting a reference to the one in the collection we
            // wanted to update. There is no need to "put" the car back into the Cars collection.
            // It is still there. All we have to do is save the changes. 
            carsTestEntities.SaveChanges();

            // Get a car out of the database that we that we just updated. This will ensure that our
            // save actually saved our changes
            carToFindForUpdate = carsTestEntities.Cars.Find("88888");

            //Output the updated car
            Console.WriteLine("Outputting the updated car that was retrieved from the database.");
            Console.WriteLine(carToFindForUpdate.id + " " + carToFindForUpdate.make + " " + carToFindForUpdate.model);

            //*********************
            // How to do a delete
            //*********************

            // Get a car out of the database that we would like to delete.
            Car carToFindForDelete = carsTestEntities.Cars.Find("88888");

            // Remove the car from the Cars collection.
            carsTestEntities.Cars.Remove(carToFindForDelete);

            // Save the changes to the database.
            carsTestEntities.SaveChanges();

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Deleted the added car. Looking to see if it is still in the database.");

            // Try to get the car out of the database and print it out. 
            try
            {
                carToFindForDelete = carsTestEntities.Cars.Find("88888");
                Console.WriteLine(carToFindForDelete.id + " " + carToFindForDelete.make + " " + carToFindForDelete.model);
            }
            catch (Exception e) // Not in the database.
            {
                Console.WriteLine("This model you are looking for does not exist." + e.ToString() + " " + e.StackTrace);
            }

            // Also going to see if we can do a test for null instead of using a try catch.
            if (carToFindForDelete == null)
            {
                Console.WriteLine("Yes, we can do a test for null as well.");
            }
        }
    }
}
