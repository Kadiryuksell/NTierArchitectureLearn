﻿using FluentValidation;
using NTierArchitecture.Business.Features.Products.Createproduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierArchitecture.Business.Features.Validators
{
    public sealed class CreateProductValidator:AbstractValidator<CreateProductCommand>
    {
        public CreateProductValidator()
        {
            RuleFor(p=>p.Name).NotEmpty().WithMessage("Ürün adı boş olamaz");
            RuleFor(p => p.Name).NotNull().WithMessage("Ürün adı boş olamaz");
            RuleFor(p => p.Name).MinimumLength(3).WithMessage("Ürün adı en az 3 karakter olmalıdır.");
            RuleFor(p => p.CategoryId).NotNull().WithMessage("Kategori boş olamaz.");
            RuleFor(p => p.CategoryId).NotEmpty().WithMessage("Kategori boş olamaz.");
            RuleFor(p => p.Price).GreaterThan(0).WithMessage("Ürün fiyatı 0 olamaz.");
            RuleFor(p => p.Quantity).GreaterThan(0).WithMessage("Ürün miktarı 0 olamaz.");
        }
    }
}
