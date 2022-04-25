
using Api.Models;
using Data.Contracts;
using Entities;
using Microsoft.AspNetCore.Mvc;
using WebFramework.Api;

namespace Api.Controllers.v1
{
    
    public class CompanyController : CrudController<CompanyDto, Company>
    {
        public CompanyController(IGenericRepository<Company> storeRepository) 
            : base(storeRepository)
        {
        }
    }
}