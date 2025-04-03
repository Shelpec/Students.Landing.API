using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Students.Landing.Core.Interfaces;
using Students.Landing.Core.Models;
using Students.Landing.Core.Models.DTOs;

namespace Students.Landing.Core.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly IGenericRepository<Application> _appRepo;
        private readonly IGenericRepository<OrganisationPracticeField> _cdRepo;
        private readonly IGenericRepository<User> _studentRepo;
        private readonly IGenericRepository<Major> _majorRepo;
        private readonly IGenericRepository<Organisation> _companyRepo;

        public ApplicationService(
            IGenericRepository<Application> appRepo,
            IGenericRepository<OrganisationPracticeField> cdRepo,
            IGenericRepository<User> studentRepo,
            IGenericRepository<Major> majorRepo,
            IGenericRepository<Organisation> companyRepo)
        {
            _appRepo = appRepo;
            _cdRepo = cdRepo;
            _studentRepo = studentRepo;
            _majorRepo = majorRepo;
            _companyRepo = companyRepo;
        }

        public async Task<Application?> GetByIdAsync(Guid id)
            => await _appRepo.GetByIdAsync(id);

        public async Task<IEnumerable<Application>> GetAllAsync()
            => await _appRepo.GetAllAsync();

        public async Task<Application> CreateApplication(Application app)
        {
            var cd = await _cdRepo.GetByIdAsync(app.PracticeFieldId);
            if (cd == null)
                throw new Exception("CompanyDirection not found!");

            if (cd.Used >= cd.Capacity)
                throw new Exception("No seats available for this direction!");

            app.SubmittedAt = DateTime.UtcNow;
            app.Status = ApplicationStatus.Pending;

            await _appRepo.AddAsync(app);
            await _appRepo.SaveAsync();

            return app;
        }


        public async Task<Application?> MarkAsCompleted(Guid applicationId)
        {
            var app = await _appRepo.GetByIdAsync(applicationId);
            if (app == null) return null;

            if (app.Status != ApplicationStatus.InProgress)
                throw new Exception("Application can be marked as 'Completed' only if it's currently 'InProgress'!");

            app.Status = ApplicationStatus.Completed;

            _appRepo.Update(app);
            await _appRepo.SaveAsync();

            return app;
        }

        public async Task<Application?> CancelApplication(Guid applicationId)
        {
            var app = await _appRepo.GetByIdAsync(applicationId);
            if (app == null) return null;

            if (app.Status == ApplicationStatus.InProgress)
                throw new Exception("Cannot cancel an application that is already 'InProgress'!");

            if (app.Status == ApplicationStatus.AcceptedByCompany)
            {
                var cd = await _cdRepo.GetByIdAsync(app.PracticeFieldId);
                if (cd != null)
                {
                    cd.Used--; // 🔹 Освобождаем место
                    _cdRepo.Update(cd);
                    await _cdRepo.SaveAsync();
                }
            }

            app.Status = ApplicationStatus.Cancelled;

            _appRepo.Update(app);
            await _appRepo.SaveAsync();

            return app;
        }
        //public async Task<IEnumerable<OrganisationDTO>> GetAvailableOrganisationsForStudent(Guid studentId)
        //{
        //    // 🔹 1. Получаем студента
        //    var student = await _studentRepo.GetByIdAsync(studentId);
        //    if (student == null)
        //        throw new Exception("Student not found!");

        //    // 🔹 2. Получаем его специальность
        //    var major = await _majorRepo.GetByIdAsync(student.MajorId);
        //    if (major == null)
        //        throw new Exception("Major not found!");

        //    // 🔹 3. Получаем направление специальности
        //    var directionId = major.SpecializationDirectionId;

        //    // 🔹 4. Получаем направления компаний с доступными местами
        //    var companyDirections = await _cdRepo.FindAll(cd =>
        //        cd.PracticeFieldId == directionId && cd.Used < cd.Capacity);

        //    var companyIds = companyDirections.Select(cd => cd.CompanyId).Distinct();

        //    // 🔹 5. Загружаем компании, но используем DTO для возврата
        //    var companies = await _companyRepo.FindAll(c => companyIds.Contains(c.Id));

        //    return companies.Select(c => new OrganisationDTO
        //    {
        //        Id = c.Id,
        //        Name = c.Name,
        //        Description = c.Description,
        //        Address = c.Address,
        //        ContactPhone = c.ContactPhone,
        //        WebsiteUrl = c.WebsiteUrl
        //    });
        //}

        //public async Task<IEnumerable<ApplicationDTO>> GetPendingApplicationsForUniversity(Guid institutionId)
        //{
        //    var applications = await _studentAppRepo.GetPendingApplicationsForUniversity(institutionId);

        //    return applications.Select(app => new ApplicationDTO
        //    {
        //        Id = app.Id,
        //        SubmittedAt = app.SubmittedAt,
        //        PracticeStart = app.PracticeStart,
        //        PracticeEnd = app.PracticeEnd,
        //        Status = app.Status.ToString(),
        //        IsUniversityApproved = app.IsUniversityApproved,
        //        StudentId = app.User?.Id ?? Guid.Empty,
        //        StudentFullName = app.User != null ? $"{app.User.FirstName} {app.User.LastName}" : "Неизвестный студент",
        //        StudentUniversity = app.User?.Major?.UniversityMajors.FirstOrDefault()?.University?.Name ?? "Неизвестный университет",
        //        StudentSpecialization = app.User?.Major?.Name ?? "Неизвестная специальность",
        //        CompanyDirectionId = app.PracticeFieldId,
        //        CompanyName = app.PracticeField?.?.Name ?? "Неизвестная компания"
        //    }).ToList();
        //}


        //public async Task<IEnumerable<ApplicationDTO>> GetPendingApplicationsForCompany(Guid companyId)
        //{
        //    var applications = await _studentAppRepo.GetPendingApplicationsForCompany(companyId);

        //    return applications.Select(app => new ApplicationDTO
        //    {
        //        Id = app.Id,
        //        SubmittedAt = app.SubmittedAt,
        //        PracticeStart = app.PracticeStart,
        //        PracticeEnd = app.PracticeEnd,
        //        Status = app.Status.ToString(),
        //        IsUniversityApproved = app.IsUniversityApproved,
        //        StudentId = app.User?.Id ?? Guid.Empty,
        //        StudentFullName = app.User != null ? $"{app.User.FirstName} {app.User.LastName}" : "Неизвестный студент",
        //        StudentUniversity = app.User?.Major?.UniversityMajors.FirstOrDefault()?.University?.Name ?? "Неизвестный университет",
        //        StudentSpecialization = app.User?.Major?.Name ?? "Неизвестная специальность",
        //        CompanyDirectionId = app.PracticeFieldId,
        //        CompanyName = app.PracticeField?.?.Name ?? "Неизвестная компания"
        //    }).ToList();
        //}



        public async Task<bool> DeleteAsync(Guid id)
        {
            var app = await _appRepo.GetByIdAsync(id);
            if (app == null) return false;

            _appRepo.Delete(app);
            await _appRepo.SaveAsync();
            return true;
        }
    }
}
