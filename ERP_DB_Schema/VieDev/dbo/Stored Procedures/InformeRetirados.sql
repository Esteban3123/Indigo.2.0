CREATE PROCEDURE [dbo].[InformeRetirados]
--@AnioRetiro AS Int
--@MesRetiro  AS Int

AS

SELECT 
TP.Nit AS CÉDULA_CIUDADANÍA, TP.NAME NOMBRE_EMPLEADO, BO.Name AS SEDE, pos.Name AS CARGO, CC.Name AS CENTRO_DE_COSTO, C.JobBondingDate AS FECHA_INGRESO, 
RR.Name AS RAZÓN_RETIRO, 
YEAR(C.RetirementDate) AS AÑO_RETIRO,
(SELECT DATENAME (MONTH, DATEADD(MONTH, MONTH(C.RetirementDate)- 1,'1900-01-01')) MES_RETIRO) AS MES_RETIRO,
DAY(C.RetirementDate) AS DÍA_RETIRO,
ET.Name AS TIPO_EMPLEADO
FROM Payroll.Employee e
Inner Join Common.ThirdParty tp ON tp.Id = e.ThirdPartyId
Inner Join Payroll.[Contract] c ON c.EmployeeId = e.Id
Inner Join Payroll.FunctionalUnit fu ON fu.Id = c.FunctionalUnitId
Inner Join Payroll.BranchOffice bo ON bo.Id = fu.BranchOfficeId
Inner Join Payroll.CostCenter cc ON cc.Id = e.CostCenterId
Inner Join Payroll.Position pos ON pos.Id = c.PositionId
Inner Join Payroll.RetirementReason rr ON rr.Id = c.RetirementReasonId
Inner Join Payroll.EmployeeType et ON et.Id = e.EmployeeTypeId
WHERE c.Status IN (2,5) And Valid = 0 --And YEAR(c.RetirementDate) = @AnioRetiro --And MONTH(c.RetirementDate) = @MesRetiro
ORDER BY c.RetirementDate Desc