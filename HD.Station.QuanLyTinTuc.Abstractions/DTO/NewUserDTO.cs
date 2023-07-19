using FluentValidation;

using HD.Station.QuanLyTinTuc.Abstractions.Stores;

namespace HD.Station.QuanLyTinTuc.Abstractions.DTO;

public class NewUserDto
{
    public required string Email { get; set; }

    public required string Password { get; set; }

    public required string Username { get; set; }

    public string? Role { get; set; }
}

public record NewUserRequest(NewUserDto User);

public class RegisterValidator : AbstractValidator<NewUserRequest>
{
    public RegisterValidator(INewsStore newsStore)
    {
        RuleFor(x => x.User.Email).NotNull().NotEmpty().EmailAddress().MaximumLength(50);
        RuleFor(x => x.User.Password).NotNull().NotEmpty().MinimumLength(8);
        RuleFor(x => x.User.Username).NotNull().NotEmpty();

        RuleFor(x => x.User.Username).MustAsync(
            async (username, cancellationToken) => !await newsStore.isUserExists(username)
        )
            .WithMessage("Username is already used");
    }
}
