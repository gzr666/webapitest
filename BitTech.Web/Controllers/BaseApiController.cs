using BitTech.Data.Repositories;
using BitTech.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace BitTech.Web.Controllers
{
    public class BaseApiController:ApiController
    {
        private ILearningRepository _repo;
        private ModelFactory _modelFactory;

        public BaseApiController(ILearningRepository repo)

        {
            _repo = repo;
        
        }

        protected ILearningRepository TheRepository {
        
            get { return _repo; }
        }

        protected ModelFactory TheModelFactory {

            get { 
            
                if (_modelFactory == null)
                {
                    _modelFactory = new ModelFactory(this.Request,_repo);
                }
                return _modelFactory;
            }

        }

    }
}