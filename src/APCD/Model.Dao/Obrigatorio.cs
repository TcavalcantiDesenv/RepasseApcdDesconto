using System.Collections.Generic;

namespace Model.Dao
{
    public interface Obrigatorio<qualquerClasse>
    {
        void create(qualquerClasse obj);
        void delete(qualquerClasse obj);
        void update(qualquerClasse obj);
        bool find(qualquerClasse obj);
        List<qualquerClasse> findAll();
    }
}
