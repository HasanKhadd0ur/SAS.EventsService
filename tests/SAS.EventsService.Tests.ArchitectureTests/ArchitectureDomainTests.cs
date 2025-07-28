using FluentAssertions;
using NetArchTest.Rules;
using SAS.SharedKernel.DomainEvents;
using SAS.SharedKernel.Repositories;

namespace SAS.EventsService.Tests.ArchitectureTests
{
    public  class ArchitectureDomainTests : ArchitectureTest
    {

        #region Events Naming Convention 
        [Fact]
        public void Event_ShouldHave_NameEndingWith_Event()
        {
            var result = Types.InAssembly(SAS.EventsService.Domain.AssemblyReference.Assembly)
                .That()
                .ImplementInterface(typeof(IDomainEvent))
                .Should()
                .HaveNameStartingWith("Event")
                .GetResult();
            result.IsSuccessful.Should().BeTrue();
        }

        #endregion Events Naming Convention

        #region Repository Naming Convention 
        [Fact]
        public void Repositories_ShouldHave_NameEndingWith_Repository()
        {
            var result = Types.InAssembly(SAS.EventsService.Domain.AssemblyReference.Assembly)
                .That()
                .ImplementInterface(typeof(IRepository<,>))
                .Should()
                .HaveNameEndingWith("Repository")
                .GetResult();
            result.IsSuccessful.Should().BeTrue();
        }

        #endregion Repository Naming Convention


    }

}
