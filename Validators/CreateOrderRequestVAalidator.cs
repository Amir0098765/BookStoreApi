using FluentValidation;
using BookStoreApi.DTOs;
namespace BookStoreApi.Validators
{
    public class CreateOrderRequestValidator : AbstractValidator<CreateOrderRequest>
    {
        public CreateOrderRequestValidator()
        {
            RuleFor(x => x.Quantity)
                .GreaterThanOrEqualTo(1)
                .WithMessage("Quantity must be at least 1");

            RuleFor(x => x.BookId)
                .GreaterThanOrEqualTo(1)
                .WithMessage("Book id must be at least 1");
        }
    }
}
