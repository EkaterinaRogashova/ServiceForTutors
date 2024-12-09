using Microsoft.AspNetCore.Mvc;
using ServiceForTutorContracts.BindingModels;
using ServiceForTutorContracts.BusinessLogicContracts;
using ServiceForTutorContracts.SearchModels;
using ServiceForTutorContracts.ViewModels;
using ServiceForTutorDatabaseImplements.Models;

namespace ServiceForTutorRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TariffPlanController : Controller
    {
        private readonly ITariffPlanLogic _logic;
        public TariffPlanController(ITariffPlanLogic logic)
        {
            _logic = logic;
        }

        [HttpGet]
        public List<TariffPlanViewModel>? GetActiveTariffPlanList(string status)
        {
            try
            {
                return _logic.ReadList(new TariffPlanSearchModel
                {
                    Status = status
                });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public void CreateTariffPlan(TariffPlanBindingModel model)
        {
            try
            {
                _logic.Create(model);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public void UpdateTariffPlan(TariffPlanBindingModel model)
        {
            try
            {
                _logic.Update(model);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet]
        public TariffPlanViewModel? GetTariffPlan(int tariffPlanId)
        {
            try
            {
                return _logic.ReadElement(new TariffPlanSearchModel
                {
                    Id = tariffPlanId
                });
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
