using Students.Landing.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Students.Landing.Core.Interfaces
{
    public interface IOrganisationPracticeFieldRepository
    {
        Task<IEnumerable<OrganisationPracticeField>> GetOrgFieldsByMajorAsync(Guid majorId);
    }
}
