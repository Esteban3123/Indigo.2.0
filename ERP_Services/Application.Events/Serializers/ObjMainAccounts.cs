using Application.Events.Models;
using Application.Events.Models.MainAccounts;
using System;

namespace Application.Events.Serializers
{
    public class ObjMainAccounts : IDittoDocument
    {
        /// <summary>
        /// Return JSON Type : MainAccountsEntity
        /// </summary>
        /// <param name="MainAccountsEntity"></param>
        /// <returns></returns>
        public object Generate(object obj, QueueParameter parameter)
        {
            Domain.Entities.MainAccounts MainAccountsEntity = obj as Domain.Entities.MainAccounts;

            MMainAccounts mMainAccounts = new MMainAccounts();
            MethodsMainAccounts methodsMainAccounts = new MethodsMainAccounts();
            mMainAccounts.LegalBook = methodsMainAccounts.GetLegalBook(MainAccountsEntity.LegalBookId);
            mMainAccounts.AccountLevel = methodsMainAccounts.GetAccountLevel(MainAccountsEntity.IdAccountLevel);
            mMainAccounts.AccountClass = methodsMainAccounts.GetAccountClass(MainAccountsEntity.IdAccountClass);
            mMainAccounts.Number = MainAccountsEntity.Number;
            mMainAccounts.Nature = Convert.ToInt16(MainAccountsEntity.Nature);
            mMainAccounts.Name = MainAccountsEntity.Name;
            mMainAccounts.Parent = methodsMainAccounts.GetParent(MainAccountsEntity.IdParent ?? 0);
            mMainAccounts.HandlesThirdParty = Convert.ToInt16(MainAccountsEntity.HandlesThirdParty);
            mMainAccounts.CloseThirdParty = Convert.ToInt16(MainAccountsEntity.CloseThirdParty);
            mMainAccounts.ThirdParty = methodsMainAccounts.GetThirdParty(MainAccountsEntity.IdThirdParty ?? 0);
            mMainAccounts.ReconcileAccount = Convert.ToInt16(MainAccountsEntity.ReconcileAccount);
            mMainAccounts.Availability = Convert.ToInt16(MainAccountsEntity.Availability);
            mMainAccounts.HandlesCostCenter = Convert.ToInt16(MainAccountsEntity.HandlesCostCenter);
            mMainAccounts.RetencionType = Convert.ToInt16(MainAccountsEntity.RetencionType);
            mMainAccounts.FreelancerCategory = Convert.ToInt16(MainAccountsEntity.FreelancerCategory);
            mMainAccounts.AllowsMovement = Convert.ToInt16(MainAccountsEntity.AllowsMovement);
            mMainAccounts.Status = Convert.ToInt16(MainAccountsEntity.Status);
            mMainAccounts.CreationUser = MainAccountsEntity.CreationUser;
            mMainAccounts.CreationDate = Convert.ToString(MainAccountsEntity.CreationDate);
            mMainAccounts.ModificationUser = MainAccountsEntity.ModificationUser;
            mMainAccounts.ModificationDate = Convert.ToString(MainAccountsEntity.ModificationDate);
            mMainAccounts.ShowCGN2 = Convert.ToInt16(MainAccountsEntity.ShowCGN2);
            return mMainAccounts;
        }
    }
}
