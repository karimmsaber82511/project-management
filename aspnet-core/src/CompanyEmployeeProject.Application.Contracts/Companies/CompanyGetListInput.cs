using Volo.Abp.Application.Dtos;

namespace CompanyEmployeeProject.Companies
{
    public class CompanyGetListInput : PagedAndSortedResultRequestDto
    {
        public string? Filter { get; set; }
    }
}
