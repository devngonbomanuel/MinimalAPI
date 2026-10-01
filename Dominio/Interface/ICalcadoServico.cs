using Minimal.Dominio.DTOs;
using Minimal.Dominio.Entities;

namespace Minimal.Dominio.Interface;

public interface ICalcadoServico
{
        Calcado? BuscaPorId(int? id);
        
        List<Calcado> Todos();
        
        void Adicionar(Calcado calcado);

        void Atualizar(Calcado calcado);

        void Apagar(Calcado calcado);
} 