using Xunit;
using Moq;
using CoreBusiness;
using UseCases.DataStorePluginInterfaces;
using UseCases.PatientsUseCases;
using System;

namespace UseCases.Tests.PatientsUseCases
{
    public class AddPatientUseCaseTests
    {
        [Fact]
        public void Execute_Should_Add_Patient_Successfully()
        {
            var newPatient = new Patient {PatientId = 1, DoctorId = 5, PatientName = "Bella", Type = "Dog", Breed = "Labrador", Age = 3, OwnerInfo = "John Doe", Diagnosis = "Healthy, routine check-up"};

            var mockPatientsRepository = new Mock<IPatientsRepository>();
            var addPatientUseCase = new AddPatientUseCase(mockPatientsRepository.Object);

            addPatientUseCase.Execute(newPatient);
        }
    }
}
