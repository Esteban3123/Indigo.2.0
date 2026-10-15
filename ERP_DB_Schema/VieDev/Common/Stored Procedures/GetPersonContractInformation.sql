-- =============================================  
-- Author:  <Author,,Name>  
-- ALTER date: <ALTER Date,,>  
-- Description:[Payroll].[GetPersonContractInfomation] <Description,,>  
-- =============================================  
CREATE PROCEDURE [Common].[GetPersonContractInformation]  
-- Add the parameters for the stored procedure here  
@IdentificationNumber VARCHAR(20)
AS
    BEGIN
        SELECT Contract.InitialContractNumber, 
               Position.Name AS PositionName, 
               Contract.ContractInitialDate, 
               Contract.JobBondingDate, 
               Contract.ContractEndingDate, 
               Contract.BasicSalary, 
               ContractType.SalaryType, 
               CostCenter.Name AS CostCenterName, 
               FunctionalUnit.Name AS FunctionalUnitName, 
               [Group].Name AS GroupName, 
               Employee.ProcedureTypeRTF, 
               Person.HousingType, 
               Employee.HousingDeductionValue, 
               Employee.EducationDeductionValue, 
               Employee.DeclarantType, 
               Employee.HealthContributorRTF
        FROM Payroll.Contract
             INNER JOIN Payroll.FunctionalUnit ON Contract.FunctionalUnitId = FunctionalUnit.Id
             INNER JOIN Payroll.Position ON Contract.PositionId = Payroll.Position.Id
             INNER JOIN Payroll.ContractType ON Contract.ContractTypeId = ContractType.Id
             INNER JOIN Payroll.Employee ON Payroll.Contract.EmployeeId = Payroll.Employee.Id
             INNER JOIN Payroll.CostCenter ON Employee.CostCenterId = CostCenter.Id
             INNER JOIN Common.ThirdParty ON Employee.ThirdPartyId = ThirdParty.Id
             INNER JOIN Common.Person ON ThirdParty.PersonId = Person.Id
             INNER JOIN Payroll.Liquidation ON Payroll.Liquidation.ContractId = Contract.Id
             INNER JOIN Payroll.[Group] ON Payroll.[Group].Id = Payroll.Liquidation.GroupId
        WHERE(Person.IdentificationNumber = @IdentificationNumber
              AND Payroll.Contract.Valid = 1
              AND Contract.STATUS = 1);
    END;