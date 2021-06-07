using System;
using System.Collections.Generic;
using DataAccess.Models;
using DataAccess.Repositories;
using DataAccess.Repositories.Implementation;

namespace DataAccessSeed
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                for (int i = 3; i < 15; i++)
                {
                    IDecreeRepository decreeRepository = new DecreeRepository();

                    Car car1 = new Car
                    {
                        Number = $"NUMBER{i}",
                        Model = "Moskvitch"
                    };

                    List<Decree> decrees = new List<Decree>
                    {
                        new Decree
                        {
                            RulingNumber = $"TEST{i}",
                            PdfPath = "Path",
                            Place = "Kharkiv",
                            EvacuationAddress = null,
                            EvacuationDateTime = DateTime.UtcNow,
                            PaymentStatus = true,
                            ActData = DateTime.UtcNow,
                            Car = car1
                        }
                    };

                    foreach (var decree in decrees)
                    {
                        decreeRepository.Create(decree);
                    }
                }

                Console.WriteLine("Done!");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.InnerException?.InnerException?.Message);
            }

            Console.Read();
        }
    }
}