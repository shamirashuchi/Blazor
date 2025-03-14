﻿using EmployeeManagement.Models;
using EmployeeManagement.web.Components.Pages;
using System.Net.Http;
using System.Net.Http.Json;

namespace EmployeeManagement.web.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly HttpClient httpClient;

        public DepartmentService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<Department> GetDepartment(int id)
        {
            return await httpClient.GetFromJsonAsync<Department>($"api/departments/{id}");
        }

        public async Task<IEnumerable<Department>> GetDepartments()
        {
            return await httpClient.GetFromJsonAsync<Department[]>("api/departments");
        }
    }
}

