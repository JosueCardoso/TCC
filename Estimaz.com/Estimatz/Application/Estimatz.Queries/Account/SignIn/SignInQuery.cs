﻿using Estimatz.Entities.User;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Estimatz.Queries.Account.SignIn
{
    public class SignInQuery : IRequest<SignInUser>
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 8)]
        public string Password { get; set; }
    }
}
