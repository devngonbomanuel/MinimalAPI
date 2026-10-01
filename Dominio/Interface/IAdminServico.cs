using Minimal.Dominio.DTOs;
using Minimal.Dominio.Entities;

namespace Minimal.Dominio.Interface;

public interface IAdminServico
{
        Admin? Login(LoginDTO loginDTO);

        List<Admin> Todos();

        Admin Criar(Admin admin);

        Admin? BuscaPorId (int id);
} 