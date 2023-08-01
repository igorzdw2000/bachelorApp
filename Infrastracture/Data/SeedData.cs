using Core.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastracture.Data
{
    public class SeedData
    {
        private readonly AppDbContext _context;

        public SeedData(AppDbContext context)
        {
            _context = context;
        }
        public void Seed()
        {
            if(!_context.Projects.Any())
            {
                var projects = new List<Project>
                {
                    new Project
                    {
                        ProjectId = 1,
                        Name = "Modernizacja domu",
                        Description = "Malowanie ścian, wymiana schodów, nowa instalacja",
                        Street = "ExampleStrasse 125",
                        City = "Amsterdam",
                        StartDate = new DateTime(2023,7,13),
                        ProjectValue = 2500,
                        Customer = new Customer
                        {
                            CustomerId = 1,
                            Name = "Jan",
                            Surname = "Kowalski",
                            Address = "ExampleStrasse 126",
                            Phone = "123-456-789",
                            Email = "example@test.com"
                        }
                    },
                     new Project
                    {
                        ProjectId = 2,
                        Name = "Modernizacja penthouse",
                        Description = "Malowanie ścian, wymiana schodów, nowa instalacja",
                        Street = "ExampleStrasse 125",
                        City = "Amsterdam",
                        StartDate = new DateTime(2023,7,13),
                        ProjectValue = 2500,
                        Customer = new Customer
                        {
                            CustomerId = 2,
                            Name = "Krzysztof",
                            Surname = "Kowalski",
                            Address = "ExampleStrasse 126",
                            Phone = "123-456-789",
                            Email = "example@test.com"
                        }
                    },
                      new Project
                    {
                        ProjectId = 3,
                        Name = "Modernizacja domu",
                        Description = "Malowanie ścian, wymiana schodów, nowa instalacja",
                        Street = "ExampleStrasse 125",
                        City = "Amsterdam",
                        StartDate = new DateTime(2023,7,13),
                        ProjectValue = 2500,
                        Customer = new Customer
                        {
                            CustomerId = 3,
                            Name = "Władysław",
                            Surname = "Kowalski",
                            Address = "ExampleStrasse 126",
                            Phone = "123-456-789",
                            Email = "example@test.com"
                        }
                    },

                };
                _context.Projects.AddRange(projects);
                _context.SaveChanges();
            }
            
        }
    }
}
