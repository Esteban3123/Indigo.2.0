

CREATE VIEW [Payroll].[ViewReportLaborCertification]
AS
SELECT
em.Id,
c.[Status],
co.Name AS 'CompanyName',
p.FirstLastName, 
p.SecondLastName, 
p.FirstName, 
p.SecondName, 
p.IdentificationNumber, 
ci.Name AS 'CityName', 
c.JobBondingDate, 
po.Name AS 'Rol', 
c.BasicSalary,
ct.Name,
(SELECT top 1 thi.Name FROM Payroll.Employee AS emp INNER JOIN Common.ThirdParty AS thi ON thi.Id = emp.ThirdPartyId WHERE emp.Id = bo.PayrollManagerId) AS 'HumanResource',
(SELECT top 1 pos.Name FROM Payroll.[Contract] AS con INNER JOIN Payroll.Position AS pos ON pos.Id = con.PositionId INNER JOIN Payroll.Employee AS emp ON emp.Id = con.EmployeeId INNER JOIN Common.ThirdParty AS thir ON thir.Id = emp.ThirdPartyId WHERE emp.Id = bo.PayrollManagerId AND con.[Status] = '1') AS 'ProfessionHumanResource'
, jt.Code + ' - ' + jt.[Name] as JobBondingType
FROM 
Payroll.[Contract] AS c
INNER JOIN Payroll.Employee AS em ON em.Id = c.EmployeeId
INNER JOIN Common.ThirdParty AS thi ON thi.Id = em.ThirdPartyId
INNER JOIN Common.Person AS p ON p.Id = thi.PersonId
LEFT JOIN Common.City AS ci ON ci.Id = p.IdentificacionCityId
INNER JOIN Payroll.Position AS po ON po.Id = c.PositionId
INNER JOIN Payroll.ContractType AS ct ON ct.Id = c.ContractTypeId
inner join Payroll.JobBondingType as jt on jt.Id = ct.JobBondingTypeId
INNER JOIN Payroll.[Group] AS g ON g.Id = c.GroupId
INNER JOIN Payroll.Company AS co ON co.Id = g.CompanyId
inner join Payroll.FunctionalUnit as fu on fu.Id = c.FunctionalUnitId
INNER JOIN Payroll.BranchOffice AS bo on bo.Id = fu.BranchOfficeId
WHERE c.[Status] = '1' and c.Valid =1
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que genera el reporte de certificación laboral de empleados activos con contratos vigentes. Integra datos del contrato (estado, fecha de vinculación, salario básico, tipo de contrato, tipo de vinculación laboral), información personal del empleado (nombres, apellidos, número de identificación, ciudad de expedición del documento), la empresa empleadora, el cargo o rol desempeñado, y el responsable del área de recursos humanos junto con su profesión. Se utiliza para emitir constancias o certificados de trabajo que acreditan la relación laboral vigente de un empleado, incluyendo el tipo de vinculación (fijo, indefinido, obra o labor), el salario básico y la empresa a la que pertenece. Solo expone contratos en estado activo y válidos.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'ViewReportLaborCertification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'ViewReportLaborCertification';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista que consolida la información laboral y personal de empleados con contrato vigente para emitir certificaciones laborales, incluyendo datos de la empresa, cargo, salario, tipo de vinculación y responsable de recursos humanos.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportLaborCertification';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen contratos en Payroll.Contract con Status=''1'' y Valid=1; Cada contrato debe tener empleado, posición, tipo de contrato, tipo de vinculación, grupo, compañía, unidad funcional y sucursal asociados (INNER JOIN obligatorios); El empleado debe tener un ThirdParty y una Person asociados; La sucursal (BranchOffice) tiene asignado un PayrollManager para resolver el responsable de recursos humanos', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportLaborCertification';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo expone contratos vigentes y válidos (Status=''1'' AND Valid=1); El responsable de recursos humanos se deriva siempre desde la sucursal de la unidad funcional del contrato, no desde el contrato directamente; El campo JobBondingType siempre se presenta con formato ''Código - Nombre''; La ciudad es opcional (LEFT JOIN); el resto de relaciones son obligatorias para que la fila aparezca', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportLaborCertification';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'certificación laboral; contrato vigente; empleado; cargo; salario básico; tipo de vinculación laboral; tipo de contrato; sucursal; unidad funcional; responsable de nómina (PayrollManager); compañía empleadora', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportLaborCertification';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Payroll.ViewReportLaborCertification: Devuelve únicamente contratos con Status=''1'' y Valid=1, expandiendo nombre de compañía, ciudad de identificación, cargo (Rol), salario básico, tipo de contrato y tipo de vinculación concatenado como ''Code - Name''.; [RETURN_RESULT] Payroll.ViewReportLaborCertification: Para cada fila resuelve ''HumanResource'' como el nombre del ThirdParty del PayrollManager de la sucursal a la que pertenece la unidad funcional del contrato (TOP 1).; [RETURN_RESULT] Payroll.ViewReportLaborCertification: Para cada fila resuelve ''ProfessionHumanResource'' como el nombre del cargo (Position) del PayrollManager, considerando solo su contrato activo con Status=''1'' (TOP 1).', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportLaborCertification';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si c.Status = ''1'' AND c.Valid = 1 → El contrato se incluye en la certificación laboral else El contrato se excluye del resultado; si Subconsulta ProfessionHumanResource: con.Status = ''1'' → Se toma el cargo del contrato activo del PayrollManager else No se retorna cargo del responsable de RRHH; si LEFT JOIN con Common.City sobre p.IdentificacionCityId → Si la persona no tiene ciudad de identificación, CityName queda en NULL pero la fila se conserva', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportLaborCertification';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.Contract; Payroll.Employee; Common.ThirdParty; Common.Person; Common.City; Payroll.Position; Payroll.ContractType; Payroll.JobBondingType; Payroll.Group; Payroll.Company; Payroll.FunctionalUnit; Payroll.BranchOffice', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportLaborCertification';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportLaborCertification';
GO
