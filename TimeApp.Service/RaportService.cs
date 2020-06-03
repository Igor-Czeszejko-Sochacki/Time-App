using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TimeApp.Model.DbModels;
using TimeApp.Model.Response;
using TimeApp.Repository;

namespace TimeApp.Service
{
    public class RaportService : IRaportService
    {
        private readonly IRepository<Raports> _raportrepo;

        public RaportService(IRepository<Raports> raportrepo)
        {
            _raportrepo = raportrepo;
        }
        public async Task<ResultDTO> AddRaport(int userId)
        {
            var result = new ResultDTO()
            {
                Response = null
            };
            try
            {
                await _raportrepo.Add(new Raports
                {
                    UserId = userId,             
                });
            }
            catch (Exception e)
            {
                result.Response = e.Message;
                return result;
            }
            return result;
        }

        public async Task<ResultDTO> PatchClosedStatus(int raportId, bool closedStatus)
        {
            var result = new ResultDTO()
            {
                Response = null
            };
            try
            {
                var raport = await _raportrepo.GetSingleEntity(x => x.Id == raportId);
                if (raport == null)
                    result.Response = "Raport not found";
                    raport.IsClosed =closedStatus;
                await _raportrepo.Patch(raport);
            }
            catch (Exception e)
            {
                result.Response = e.Message;
                return result;
            }
            return result;
        }

        public async Task<ResultDTO> PatchAcceptedStatus(int raportId, bool acceptedStatus)
        {
            var result = new ResultDTO()
            {
                Response = null
            };
            try
            {
                var raport = await _raportrepo.GetSingleEntity(x => x.Id == raportId);
                if (raport == null)
                    result.Response = "Raport not found";
                raport.IsAccepted = acceptedStatus;
                await _raportrepo.Patch(raport);
            }
            catch (Exception e)
            {
                result.Response = e.Message;
                return result;
            }
            return result;
        }

        public async Task<List<Raports>> GetAllRaports()
        {
            var raportsList = await _raportrepo.GetAll();
            return raportsList;
        }
    }
}
