
using Minimal.Dominio.Interface;
using Minimal.Dominio.DTOs;
using Minimal.Dominio.Entities;
using Minimal.Infraestrutura.Data;

namespace Minimal.Dominio.Services;

    public class CalcadoServico : ICalcadoServico
    {
        private readonly AppDbContext _contexto;

        public CalcadoServico (AppDbContext contexto)
        {
            _contexto = contexto;
        }

        public Calcado? BuscaPorId(int id)
        {
            return _contexto.Calcados.Where(v => v.Id == id).Find(); 
        }

        public List<Calcado> Todos()
        {
            return  _contexto.Calcados.ToList();
        }

        public void Adicionar(Calcado calcado)
        {
            _contexto.Calcados.Add(calcado);
            _contexto.SaveChanges();
        }

        public void Atualizar(Calcado calcado)
        {
            _contexto.Calcados.Update(calcado);
            _contexto.SaveChanges();

        }

        public void Apagar(Calcado calcado)
        {
            _contexto.Calcados.Remove(calcado);
            _contexto.SaveChanges();

        }
    }
