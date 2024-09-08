using FluentValidation;
using NTierArchitecture.Business.Features.Auth.Register;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierArchitecture.Business.Features.Validators
{
    public sealed class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(p => p.UserName).NotNull().WithMessage("Kullanıcı adı boş olamaz");
            RuleFor(p=>p.UserName).NotEmpty().WithMessage("Kullanıcı adı boş olamaz");
            RuleFor(p => p.UserName).MinimumLength(3).WithMessage("Kullanıcı adı en az 3 karakter olmalıdır");
            RuleFor(p => p.Name).NotEmpty().WithMessage("Ad alanı boş olamaz");
            RuleFor(p => p.Name).NotNull().WithMessage("Ad alanı boş olamaz");
            RuleFor(p => p.Name).MinimumLength(3).WithMessage("Ad alanı en az 3 karakter olmalıdır");
            RuleFor(p => p.LastName).NotEmpty().WithMessage("Soyad alanı boş olamaz");
            RuleFor(p => p.LastName).NotNull().WithMessage("Soyad alanı boş olamaz");
            RuleFor(p => p.LastName).MinimumLength(3).WithMessage("Soyad alanı en az 3 karakter olmalıdır");
            RuleFor(p => p.Email).NotNull().WithMessage("Mail adresi boş olamaz");
            RuleFor(p => p.Email).NotEmpty().WithMessage("Mail adresi boş olamaz");
            RuleFor(p => p.Email).MinimumLength(3).WithMessage("Mail adresi en az 3 karakter olmalıdır");
            RuleFor(p => p.Email).EmailAddress().WithMessage("Geçerli bir mail adresi giriniz.");
            RuleFor(p => p.Password).NotNull().WithMessage("Şifre boş olamaz");
            RuleFor(p => p.Password).NotEmpty().WithMessage("Şifre boş olamaz");
            RuleFor(p => p.Password).MinimumLength(6).WithMessage("Şifre en az 6 karakter olmalıdır");
            RuleFor(p => p.Password).Matches("[A-Z]").WithMessage("Şifre en az 1 büyük harf içermelidir");
            RuleFor(p => p.Password).Matches("[0-9]").WithMessage("Şifre en az 1 rakam içermelidir");
            RuleFor(p => p.Password).Matches("[a-z]").WithMessage("Şifre en az 1 küçük harf içermelidir");
            RuleFor(p => p.Password).Matches("[^a-zA-Z0-9]").WithMessage("Şifre en az 1 adet özel karakter içermelidir");


        }
    }
}
