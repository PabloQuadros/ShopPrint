using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace ShopPrint.Domain.Entities
{
    public interface IUser
    {
        //UserName(string) : o nome de usuário do usuário.É obrigatório e deve ser único.
        //Email(string): o endereço de e-mail do usuário.É opcional, mas se fornecido, deve ser único.
        //PasswordHash(string): o hash da senha do usuário.É obrigatório.
        //PhoneNumber(string): o número de telefone do usuário.É opcional.
        //PhoneNumberConfirmed(bool): indica se o número de telefone do usuário foi confirmado.É opcional.
        //TwoFactorEnabled(bool): indica se a autenticação de dois fatores está habilitada para o usuário.É opcional.
        //LockoutEnd (DateTimeOffset?): indica quando o bloqueio do usuário expira. Se null, o usuário não está bloqueado.É opcional.
        //LockoutEnabled (bool): indica se o bloqueio de conta está habilitado para o usuário.É opcional.
        //AccessFailedCount (int): o número de vezes que a autenticação falhou para o usuário.É opcional.
    }
}
