using Model.Dao;
using Model.Entity;
using System;
using System.Collections.Generic;

namespace Model.Neg
{
    public class EventosNeg
    {
        private EventosDao objEventosDao;

        public EventosNeg()
        {
            objEventosDao = new EventosDao();
        }

        public void create(Eventos objEventos)
        {
            bool verificacao = true;
            int Id_Evento = objEventos.Id_Evento;
            string Id_Loja = objEventos.Id_Loja.ToString();
            string Dt_Inicio = objEventos.Dt_Inicio.ToString();
            string Dt_Fim = objEventos.Dt_Fim.ToString();
            string Tipo_Evento = objEventos.Tipo_Evento;
            string Titulo = objEventos.Titulo;
            string Tipo_Exibicao = objEventos.Tipo_Exibicao;

            DateTime End = objEventos.Fim;
            bool IsFullDay = objEventos.IsFullDay;
            DateTime Start = objEventos.Start;
            string Subject = objEventos.Subject;
            string ThemeColor = objEventos.ThemeColor;
            string Description = objEventos.Description;



            Eventos objEventos1 = new Eventos();
            objEventos1.Id_Evento = objEventos.Id_Evento;
            verificacao = !objEventosDao.find(objEventos1);
            if (!verificacao)
            {
                objEventos.Estados = 9;
                return;
            }


            //se nao tem erro
            objEventos.Estados = 99;
            objEventosDao.create(objEventos);
            return;
        }
        public void update(Eventos objEventos)
        {
            bool verificacao = true;
            //begin validar codigo retorna estado=1
            int Id_Evento = objEventos.Id_Evento;
            string Id_Loja = objEventos.Id_Loja.ToString();
            string Dt_Inicio = objEventos.Dt_Inicio.ToString();
            string Dt_Fim = objEventos.Dt_Fim.ToString();
            string Tipo_Evento = objEventos.Tipo_Evento;
            string Titulo = objEventos.Titulo;
            string Tipo_Exibicao = objEventos.Tipo_Exibicao;

            DateTime Fim = objEventos.Fim;
            bool IsFullDay = objEventos.IsFullDay;
            DateTime Start = objEventos.Start;
            string Subject = objEventos.Subject;
            string ThemeColor = objEventos.ThemeColor;
            string Description = objEventos.Description;


            // Inicia validacao

            /// ???????????

            //se nao tem erro
            objEventos.Estados = 99;
            objEventosDao.update(objEventos);
            return;
        }
        public void delete(Eventos objEventos)
        {
            bool verificacao = true;
            //verificando se existe
            Eventos objEventosAux = new Eventos();
            objEventosAux.Id_Evento = objEventos.Id_Evento;
            verificacao = objEventosDao.find(objEventosAux);
            if (!verificacao)
            {
                objEventos.Estados = 33;
                return;
            }


            objEventos.Estados = 99;
            objEventosDao.delete(objEventos);
            return;
        }
        public bool find(Eventos objEventos)
        {
            return objEventosDao.find(objEventos);
        }
        public List<Eventos> findAll()
        {
            return objEventosDao.findAll();
        }
        public List<Eventos> findAllEventos(Eventos objEventos)
        {
            return objEventosDao.findAllEventos(objEventos);
        }
    }
}
