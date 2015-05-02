using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Oop.Csharp.Features
{
	/// <remarks>
	/// Most stuff is taken from Pluralsight tutorial on Co/Contravariance tutorial by Scott Allen.
	/// </remarks>
	public class Program
	{
		public static void Main(string[] args)
		{
			IRepository<Employee> employeeRepository = new Repository<Employee>();
			DumpPeople(employeeRepository);
		}

		private static void DumpPeople(IRepository<Person> personRepository)
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
	}

	public class Repository<T> : IRepository<T>
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
	}

	public interface IRepository<T>
	{
		void Add(T entity);
		void Remove(T entity);
		IQueryable<T> FindAll();
	}
}
