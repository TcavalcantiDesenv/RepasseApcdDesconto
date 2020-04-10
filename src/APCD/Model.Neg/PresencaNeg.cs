using Model.Dao;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Neg
{
    public class PresencaNeg
    {
        private PresencaDao objPresencaDao;
        public PresencaNeg()
        {
            objPresencaDao = new PresencaDao();
        }
        public void create(Presenca objPresenca)
        {
            if (objPresenca.Nome == "" || objPresenca.Nome == null)
            {
                return;
            }
            //se nao tem erro
            objPresencaDao.create(objPresenca);
            return;
        }
        public void update(Presenca objPresenca)
        {
            if (objPresenca.Nome == "" || objPresenca.Nome == null)
            {
                return;
            }

            //se nao tem erro
            objPresencaDao.update(objPresenca);
            return;
        }
        public void delete(Presenca objPresenca)
        {
            if (objPresenca.Nome == "" || objPresenca.Nome == null)
            {
                return;
            }

            //se nao tem erro
            objPresencaDao.delete(objPresenca);
            return;
        }
        public List<Presenca> BuscarPorId(Presenca obj)
        {
            return objPresencaDao.BuscarPorId(obj);
        }
        public List<Presenca> BuscarTodos()
        {
            return objPresencaDao.BuscarTodos();
        }
        public List<Presenca> BuscarPorData(Presenca obj)
        {
            return objPresencaDao.BuscarPorData(obj);
        }
    }
}
