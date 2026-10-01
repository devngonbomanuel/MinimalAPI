
using Minimal.Dominio.Interface;
using Minimal.Dominio.DTOs;
using Minimal.Dominio.Entities;
using Minimal.Infraestrutura.Data;

namespace Minimal.Dominio.Services;

    public class AdminServico : IAdminServico
    {
        private readonly AppDbContext _contexto;

        public AdminServico(AppDbContext contexto)
        {
            _contexto =  _contexto;
        }

        public Admin? Login(LoginDTO loginDTO)
        {
            var adminstrador = _contexto.Administradores.Where(a => a.Email == loginDTO.Email && a.Senha == loginDTO.Senha).FirstOrDefault();
            return adminstrador;
        }

        public Admin Criar(Admin admin)
        {
            _contexto.Administradores.Add(admin);   
            _contexto.SaveChanges();       
            return adminstrador;
        }

        public List<Admin> Todos()
        {
            return  _contexto.Administradores.ToList();
        }

        public Admin? BuscaPorId(int id)
        {
            return _contexto.Admin.Where(v => v.Id == id).Find(); 
        }
    }
