using PeopleProTraining.Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleProTraining.Dal.Interfaces
{
    public interface IPeopleProRepo : IDisposable
    {
        #region access

        #region employees
        IQueryable<Employee> GetEmployees();
        IEnumerable<Employee> GetEmployees(Func<Employee, bool> predicate);

        Employee GetEmployee(Func<Employee, bool> predicate);
        Employee GetEmployee(int id);
        #endregion
        #endregion

        #region Employees

        IEnumerable<Employee> GetAllEmployees();

        Employee GetEmployeeById(int id);

        void UpdateEmployee(Employee employee);

        void InsertEmployee(Employee employee);

        void SaveEmployee(Employee employee);

        void DeleteEmployee(Employee employee);

        #endregion
        #region Buildings

        IEnumerable<Building> GetAllBuildings();

        Building GetBuildingById(int id);

        void SaveBuilding(Building building);

        void DeleteBuilding(Building building);

        void UpdateBuilding(Building building);

        void InsertBuilding(Building building);

        #endregion
        #region Departments

        IEnumerable<Department> GetAllDepartments();

        Department GetDepartmentById(int id);

        void UpdateDepartment(Department department);

        void InsertDepartment(Department department);

        void SaveDepartment(Department department);

        void DeleteDepartment(Department department);

        #endregion

    }
}
