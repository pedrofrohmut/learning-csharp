using Xunit;

using CloudCustomers.Api.UseCases;

namespace CloudCustomers.UnitTests.Systems.UseCases;

class TestUserUseCase
{
    [Fact]
    async void GetAllUsers_WhenCalled_InvokesHttpGetRequest()
    {
        // Given
        var useCase = new UserUseCases();
        // When
        await useCase.GetAllUsers();
        // Then
    }
}
