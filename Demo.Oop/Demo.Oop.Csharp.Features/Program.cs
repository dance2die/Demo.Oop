﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Demo.Oop.Csharp.Features
{
	/// <remarks>
	/// Most stuff is taken from Pluralsight tutorial on Co/Contravariance tutorial by Scott Allen.
	/// </remarks>
	public class Program
	{
		public static void Main(string[] args)
		{
			IRepository<Employee> employeeRepository = new Repository<Employee>
				{
					new Employee {EmployeeId = 1, Name = "Aries"},
					new Employee {EmployeeId = 2, Name = "Bob"},
					new Employee {EmployeeId = 3, Name = "Cathy"}
				};

			AddManagers(employeeRepository);
			DumpPeople(employeeRepository);
		}

		private static void AddManagers(IWriteOnlyRepository<Manager> employeeRepository)
		{
			var manager = new Manager
				{
				Name = "David", 
				EmployeeId = 4,
				Underlings =
					{
					new Employee {EmployeeId = 1, Name = "Aries"},
					new Employee {EmployeeId = 2, Name = "Bob"}
					}
				};
			employeeRepository.Add(manager);
		}

		/// <summary>
		/// To test Covariance.
		/// </summary>
		/// <param name="personRepository"></param>
		private static void DumpPeople(IReadOnlyRepository<Person> personRepository)
		{
			var people = personRepository.FindAll();
			foreach (var person in people)
			{
				Console.WriteLine(person.Name);
			}
		}
	}

	public class Person
	{
		public string Name { get; set; }
	}

	public class Employee : Person
	{
		public int EmployeeId { get; set; }
	}

	public class Manager : Employee
	{
		public List<Employee> Underlings { get; set; }

		public Manager()
		{
			Underlings = new List<Employee>();
		}
	}

	public interface IReadOnlyRepository<out T>
	{
		IQueryable<T> FindAll();
	}

	public interface IWriteOnlyRepository<in T>
	{
		void Add(T entity);
		void Remove(T entity);
	}

	public interface IRepository<T> : IReadOnlyRepository<T>, IWriteOnlyRepository<T>
	{
	}

	public class Repository<T> : IRepository<T>, IEnumerable<T>
	{
		private readonly List<T> _entities = new List<T>(); 

		public void Add(T entity)
		{
			_entities.Add(entity);
		}

		public void Remove(T entity)
		{
			_entities.Remove(entity);
		}

		public IQueryable<T> FindAll()
		{
			return _entities.AsQueryable();
		}

		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return _entities.GetEnumerator();
		}

		public IEnumerator GetEnumerator()
		{
			return _entities.GetEnumerator();
		}
	}
}
