using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Students.Landing.Core.Interfaces;
using Students.Landing.Core.Models;
using Students.Landing.Core.Models.DTOs;

namespace Students.Landing.Core.Services
{
    public class StudentApplicationService : IStudentApplicationService
    {
        private readonly IGenericRepository<StudentApplication> _appRepo;
        private readonly IGenericRepository<CompanyDirection> _cdRepo;
        private readonly IGenericRepository<Student> _studentRepo;
        private readonly IGenericRepository<Major> _majorRepo;
        private readonly IGenericRepository<Company> _companyRepo;
        private readonly IStudentApplicationRepository _studentAppRepo;

        public StudentApplicationService(
            IGenericRepository<StudentApplication> appRepo,
            IGenericRepository<CompanyDirection> cdRepo,
            IGenericRepository<Student> studentRepo,
            IGenericRepository<Major> majorRepo,
            IGenericRepository<Company> companyRepo,
            IStudentApplicationRepository studentAppRepo)
        {
            _appRepo = appRepo;
            _cdRepo = cdRepo;
            _studentRepo = studentRepo;
            _majorRepo = majorRepo;
            _companyRepo = companyRepo;
            _studentAppRepo = studentAppRepo;
        }

        public async Task<StudentApplication?> GetByIdAsync(Guid id)
            => await _appRepo.GetByIdAsync(id);

        public async Task<IEnumerable<StudentApplication>> GetAllAsync()
            => await _appRepo.GetAllAsync();

        public async Task<StudentApplication> CreateApplication(StudentApplication app)
        {
            var cd = await _cdRepo.GetByIdAsync(app.CompanyDirectionId);
            if (cd == null)
                throw new Exception("CompanyDirection not found!");

            if (cd.Used >= cd.Capacity)
                throw new Exception("No seats available for this direction!");

            app.SubmittedAt = DateTime.UtcNow;
            app.Status = ApplicationStatus.Pending;
            app.IsUniversityApproved = false;

            await _appRepo.AddAsync(app);
            await _appRepo.SaveAsync();

            return app;
        }

        public async Task<StudentApplication?> ApproveByUniversity(Guid applicationId)
        {
            var app = await _appRepo.GetByIdAsync(applicationId);
            if (app == null) return null;

            if (app.Status != ApplicationStatus.Pending)
                throw new Exception("University can approve only applications in 'Pending' status!");

            app.IsUniversityApproved = true;
            app.Status = ApplicationStatus.UniversityApproved;

            _appRepo.Update(app);
            await _appRepo.SaveAsync();

            return app;
        }

        public async Task<StudentApplication?> RejectByUniversity(Guid applicationId)
        {
            var app = await _appRepo.GetByIdAsync(applicationId);
            if (app == null) return null;

            if (app.Status != ApplicationStatus.Pending)
                throw new Exception("University can reject only applications in 'Pending' status!");

            app.Status = ApplicationStatus.RejectedByUniversity;

            _appRepo.Update(app);
            await _appRepo.SaveAsync();

            return app;
        }

        public async Task<StudentApplication?> AcceptByCompany(Guid applicationId)
        {
            var app = await _appRepo.GetByIdAsync(applicationId);
            if (app == null) return null;

            if (app.Status != ApplicationStatus.UniversityApproved)
                throw new Exception("Company can accept only applications in 'UniversityApproved' status!");

            var cd = await _cdRepo.GetByIdAsync(app.CompanyDirectionId);
            if (cd == null) return null;

            if (cd.Used >= cd.Capacity)
                throw new Exception("No seats left!");

            app.Status = ApplicationStatus.InProgress; // 🔹 Теперь студент проходит практику

            cd.Used++; // 🔹 Бронируем место в компании
            _cdRepo.Update(cd);

            _appRepo.Update(app);
            await _cdRepo.SaveAsync();
            await _appRepo.SaveAsync();

            return app;
        }

        public async Task<StudentApplication?> RejectByCompany(Guid applicationId)
        {
            var app = await _appRepo.GetByIdAsync(applicationId);
            if (app == null) return null;

            if (app.Status != ApplicationStatus.UniversityApproved)
                throw new Exception("Company can reject only applications in 'UniversityApproved' status!");

            app.Status = ApplicationStatus.RejectedByCompany;

            _appRepo.Update(app);
            await _appRepo.SaveAsync();

            return app;
        }

        public async Task<StudentApplication?> MarkAsCompleted(Guid applicationId)
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

        public async Task<StudentApplication?> CancelApplication(Guid applicationId)
        {
            var app = await _appRepo.GetByIdAsync(applicationId);
            if (app == null) return null;

            if (app.Status == ApplicationStatus.InProgress)
                throw new Exception("Cannot cancel an application that is already 'InProgress'!");

            if (app.Status == ApplicationStatus.AcceptedByCompany)
            {
                var cd = await _cdRepo.GetByIdAsync(app.CompanyDirectionId);
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
        public async Task<IEnumerable<CompanyDTO>> GetAvailableCompaniesForStudent(Guid studentId)
        {
            // 🔹 1. Получаем студента
            var student = await _studentRepo.GetByIdAsync(studentId);
            if (student == null)
                throw new Exception("Student not found!");

            // 🔹 2. Получаем его специальность
            var major = await _majorRepo.GetByIdAsync(student.MajorId);
            if (major == null)
                throw new Exception("Major not found!");

            // 🔹 3. Получаем направление специальности
            var directionId = major.SpecializationDirectionId;

            // 🔹 4. Получаем направления компаний с доступными местами
            var companyDirections = await _cdRepo.FindAll(cd =>
                cd.SpecializationDirectionId == directionId && cd.Used < cd.Capacity);

            var companyIds = companyDirections.Select(cd => cd.CompanyId).Distinct();

            // 🔹 5. Загружаем компании, но используем DTO для возврата
            var companies = await _companyRepo.FindAll(c => companyIds.Contains(c.Id));

            return companies.Select(c => new CompanyDTO
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                Address = c.Address,
                ContactPhone = c.ContactPhone,
                WebsiteUrl = c.WebsiteUrl
            });
        }

        public async Task<IEnumerable<StudentApplicationDTO>> GetPendingApplicationsForUniversity(Guid universityId)
        {
            var applications = await _studentAppRepo.GetPendingApplicationsForUniversity(universityId);

            return applications.Select(app => new StudentApplicationDTO
            {
                Id = app.Id,
                SubmittedAt = app.SubmittedAt,
                PracticeStart = app.PracticeStart,
                PracticeEnd = app.PracticeEnd,
                Status = app.Status.ToString(),
                IsUniversityApproved = app.IsUniversityApproved,
                StudentId = app.Student?.Id ?? Guid.Empty,
                StudentFullName = app.Student != null ? $"{app.Student.FirstName} {app.Student.LastName}" : "Неизвестный студент",
                StudentUniversity = app.Student?.Major?.UniversityMajors.FirstOrDefault()?.University?.Name ?? "Неизвестный университет",
                StudentSpecialization = app.Student?.Major?.Name ?? "Неизвестная специальность",
                CompanyDirectionId = app.CompanyDirectionId,
                CompanyName = app.CompanyDirection?.Company?.Name ?? "Неизвестная компания"
            }).ToList();
        }


        public async Task<IEnumerable<StudentApplicationDTO>> GetPendingApplicationsForCompany(Guid companyId)
        {
            var applications = await _studentAppRepo.GetPendingApplicationsForCompany(companyId);

            return applications.Select(app => new StudentApplicationDTO
            {
                Id = app.Id,
                SubmittedAt = app.SubmittedAt,
                PracticeStart = app.PracticeStart,
                PracticeEnd = app.PracticeEnd,
                Status = app.Status.ToString(),
                IsUniversityApproved = app.IsUniversityApproved,
                StudentId = app.Student?.Id ?? Guid.Empty,
                StudentFullName = app.Student != null ? $"{app.Student.FirstName} {app.Student.LastName}" : "Неизвестный студент",
                StudentUniversity = app.Student?.Major?.UniversityMajors.FirstOrDefault()?.University?.Name ?? "Неизвестный университет",
                StudentSpecialization = app.Student?.Major?.Name ?? "Неизвестная специальность",
                CompanyDirectionId = app.CompanyDirectionId,
                CompanyName = app.CompanyDirection?.Company?.Name ?? "Неизвестная компания"
            }).ToList();
        }



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
