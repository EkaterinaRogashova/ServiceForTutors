using ServiceForTutorContracts.BindingModels;
using ServiceForTutorContracts.BusinessLogicContracts;
using ServiceForTutorContracts.SearchModels;
using ServiceForTutorContracts.StoragesContracts;
using ServiceForTutorContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceForTutorBusinessLogic.BusinessLogic
{
    public class ScheduleLogic : IScheduleLogic
    {
        private readonly IScheduleStorage _scheduleStorage;
        public ScheduleLogic(IScheduleStorage scheduleStorage)
        {
            _scheduleStorage = scheduleStorage;
        }
        public bool Create(ScheduleBindingModel model)
        {
            CheckModel(model);
            var result = _scheduleStorage.Insert(model);

            if (result == null)
            {
                return false;
            }
            return true;
        }

        public bool Delete(ScheduleBindingModel model)
        {
            if (_scheduleStorage.Delete(model) == null)
            {
                return false;
            }
            return true;
        }

        public ScheduleViewModel? ReadElement(ScheduleSearchModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            var element = _scheduleStorage.GetElement(model);
            if (element == null)
            {
                return null;
            }
            return element;
        }

        public List<ScheduleViewModel>? ReadList(ScheduleSearchModel? model)
        {
            var list = model == null ? _scheduleStorage.GetFullList() : _scheduleStorage.GetFilteredList(model);
            if (list == null)
            {
                return null;
            }
            return list;
        }

        public bool Update(ScheduleBindingModel model)
        {
            if (_scheduleStorage.Update(model) == null)
            {
                return false;
            }
            return true;
        }
        private void CheckModel(ScheduleBindingModel model, bool withParams = true)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (!withParams)
            {
                return;
            }
            var searchModel = new ScheduleSearchModel
            {
                DateTimeStart = model.DateTimeStart,
                DateTimeEnd = model.DateTimeEnd,
            };

            var existingSlots = _scheduleStorage.GetFilteredList(searchModel);

            foreach (var slot in existingSlots)
            {
                if (IsTimeSlotOverlapping(model.DateTimeStart, model.DateTimeEnd, slot.DateTimeStart, slot.DateTimeEnd))
                {
                    throw new InvalidOperationException("Невозможно добавить временной слот, так как в это время уже есть занятие.");
                }
            }
        }

        private bool IsTimeSlotOverlapping(DateTime start1, DateTime end1, DateTime start2, DateTime end2)
        {
            return (start1 < end2 && end1 > start2);
        }
    }
}
