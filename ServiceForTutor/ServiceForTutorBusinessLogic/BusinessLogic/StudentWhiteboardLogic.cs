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
    public class StudentWhiteboardLogic : IStudentWhiteboardLogic
    {
        private readonly IStudentWhiteboardStorage _boardStorage;
        public StudentWhiteboardLogic(IStudentWhiteboardStorage boardStorage)
        {
            _boardStorage = boardStorage;
        }
        public bool Create(StudentWhiteboardBindingModel model)
        {
            var result = _boardStorage.Insert(model);

            if (result == null)
            {
                return false;
            }
            return true;
        }

        public StudentWhiteboardViewModel? ReadElement(StudentWhiteboardSearchModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            var element = _boardStorage.GetElement(model);
            if (element == null)
            {
                return null;
            }
            return element;
        }

        public bool Update(StudentWhiteboardBindingModel model)
        {
            if (_boardStorage.Update(model) == null)
            {
                return false;
            }
            return true;
        }
    }
}
