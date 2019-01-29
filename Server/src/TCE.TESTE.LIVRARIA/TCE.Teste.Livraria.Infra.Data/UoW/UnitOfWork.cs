using System;
using System.Collections.Generic;
using System.Text;
using TCE.Teste.Livraria.Domain.Interfaces;
using TCE.Teste.Livraria.Infra.Data.Context;

namespace TCE.Teste.Livraria.Infra.Data.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LivrariaDbContext _context;

        public UnitOfWork(LivrariaDbContext context)
        {
            _context = context;
        }

        public bool Commit()
        {
            return _context.SaveChanges() > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
