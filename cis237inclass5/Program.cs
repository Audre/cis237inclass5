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
        }
    }
}
