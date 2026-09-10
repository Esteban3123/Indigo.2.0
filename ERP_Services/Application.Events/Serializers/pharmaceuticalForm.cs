using Application.Events.Models;
using Application.Events.Models.pharmaceuticalForm;
using System;

namespace Application.Events.Serializers
{
    public class pharmaceuticalForm : IDittoDocument
    {
        /// <summary>
        /// Return JSON Type : pharmaceuticalForm
        /// </summary>
        /// <param name="pharmaceuticalForm"></param>
        /// <returns></returns>
        public object Generate(object obj, QueueParameter parameter)
        {
            Domain.Entities.PharmaceuticalForm pharmaceuticalForm = obj as Domain.Entities.PharmaceuticalForm;

            MpharmaceuticalForm mpharmaceuticalForm = new MpharmaceuticalForm();
            mpharmaceuticalForm.Code = pharmaceuticalForm.Code;
            mpharmaceuticalForm.Name = pharmaceuticalForm.Name;
            mpharmaceuticalForm.CrystalMedicalForm = pharmaceuticalForm.Code;
            mpharmaceuticalForm.Status = Convert.ToInt16(pharmaceuticalForm.Status);
            mpharmaceuticalForm.RequireStability = Convert.ToInt16(pharmaceuticalForm.RequireStability);
            mpharmaceuticalForm.CreationUser = pharmaceuticalForm.CreationUser;
            mpharmaceuticalForm.CreationDate = Convert.ToString(pharmaceuticalForm.CreationDate);
            mpharmaceuticalForm.ModificationUser = pharmaceuticalForm.ModificationUser;
            mpharmaceuticalForm.ModificationDate = Convert.ToString(pharmaceuticalForm.ModificationDate);
            return mpharmaceuticalForm;
        }
    }
}
