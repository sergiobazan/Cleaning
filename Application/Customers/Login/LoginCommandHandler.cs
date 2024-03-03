using Application.Abstractions;
using Application.Abstractions.Behavior.Messaging;
using Domain.Abstractions;
using Domain.Customers;

namespace Application.Customers.Login;

public sealed class LoginCommandHandler : ICommandHandler<LoginCommand, string>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IJwtProvider _jwtProvider;

    public LoginCommandHandler(ICustomerRepository customerRepository, IJwtProvider jwtProvider)
    {
        _customerRepository = customerRepository;
        _jwtProvider = jwtProvider;
    }

    public async Task<Result<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var email = Email.Create(request.Email);
        var customer = await _customerRepository.GetByEmailAsync(email);

        if (customer is null)
        {
            return Result.Failure<string>(CustomerErrors.InvalidCredentials);
        } 

        string token = _jwtProvider.Generate(customer);

        return token;
    }
}
