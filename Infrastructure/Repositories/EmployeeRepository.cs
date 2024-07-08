﻿using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class EmployeeRepository(DbSet<Employee> employee) : Repository<Employee>(employee), IEmployeeRepository
    {
    }
}