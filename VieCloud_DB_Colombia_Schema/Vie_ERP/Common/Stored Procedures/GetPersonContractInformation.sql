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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta la información contractual vigente de un empleado a partir de su número de identificación (cédula). Integra datos del contrato activo (número inicial, fechas de inicio y fin, vinculación, salario básico y tipo de salario), el cargo desempeñado, el centro de costo, la unidad funcional y el grupo de nómina al que pertenece. Además recupera características personales y laborales del empleado como tipo de procedimiento RTF, tipo de vivienda, deducciones por educación y vivienda, tipo de declarante y contribución a salud. Se usa en el módulo de nómina para consultar el perfil contractual completo de un trabajador identificado por su documento.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'PROCEDURE', @level1name = N'GetPersonContractInformation';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'PROCEDURE', @level1name = N'GetPersonContractInformation';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consultar la información contractual y laboral vigente de una persona (contrato, cargo, salario, centro de costo, unidad funcional, grupo de nómina y datos tributarios/deducciones) a partir de su número de identificación.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'GetPersonContractInformation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir una persona en Common.Person con el número de identificación suministrado.; La persona debe estar vinculada como tercero (Common.ThirdParty) y tener registro de empleado activo en Payroll.Employee.; El empleado debe tener al menos un contrato vigente (Valid = 1 y STATUS = 1) con liquidación, cargo, tipo de contrato, unidad funcional, centro de costo y grupo de nómina asociados.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'GetPersonContractInformation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retorna información de contratos marcados como válidos (Valid = 1) y con estado activo (STATUS = 1).; La persona se identifica unívocamente por su número de identificación a través de la cadena Person → ThirdParty → Employee → Contract.; Solo se incluyen contratos que tengan al menos una liquidación asociada, dado que se exige INNER JOIN con Liquidation para obtener el grupo de nómina.; Cada contrato retornado debe tener obligatoriamente unidad funcional, cargo, tipo de contrato, centro de costo y grupo de nómina asignados (por uso de INNER JOIN).', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'GetPersonContractInformation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Contrato laboral; Empleado; Cargo/Posición; Tipo de contrato; Salario básico; Centro de costo; Unidad funcional; Grupo de nómina; Liquidación de nómina; Tipo de vivienda; Deducción por vivienda; Deducción por educación; Tipo de declarante; Aportante de salud (RTF); Tipo de procedimiento RTF', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'GetPersonContractInformation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Payroll.Contract: Cuando Person.IdentificationNumber coincide con el parámetro y Contract.Valid = 1 y Contract.STATUS = 1, retorna un resultset con los datos del contrato, cargo, tipo de salario, centros organizacionales y deducciones tributarias del empleado.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'GetPersonContractInformation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.Contract; Payroll.FunctionalUnit; Payroll.Position; Payroll.ContractType; Payroll.Employee; Payroll.CostCenter; Common.ThirdParty; Common.Person; Payroll.Liquidation; Payroll.Group', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'GetPersonContractInformation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'GetPersonContractInformation';
-- GO
