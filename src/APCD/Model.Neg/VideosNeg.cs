using Model.Dao;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Neg
{
    public class VideosNeg
    {
        private VideosDao objVideosDao;

        public VideosNeg()
        {
            objVideosDao = new VideosDao();
        }

        public List<Videos> findAll()
        {
            return objVideosDao.findAll();
        }
    }
}
