-- =============================================
-- Author:		Jorge Eduardo Guerra Rojas
-- Create date: <01-08-2019>
-- Description:	<Lista Información Contractual del Personal Activo>
-- =============================================
CREATE PROCEDURE [dbo].[Informe_Listado_Personal]
--@Grupo  AS Int

AS

SELECT
CASE p.IdentificationType WHEN '0' THEN 'Cédula de Ciudadanía' WHEN '1' THEN 'Cédula de Extranjería' WHEN '2' THEN 'Tarjeta de Identidad' WHEN '3' THEN 'Registro Civil' WHEN '4' THEN 'Pasporte' 
WHEN '5' THEN 'Adulto Sin Identificación' WHEN '6' THEN 'Menor Sin Identificación' WHEN '7' THEN 'Nit' WHEN '8' THEN 'Número único de identificación personal (NU)' WHEN '9' THEN 'Cetrigicado Nacido Vivo (CN)' 
WHEN '10' THEN 'Carnet Diplomático (CD)' WHEN '11' THEN 'Salvoconducto (SC)' WHEN '12' THEN 'Permiso especial de Permanencia (PE)' END AS TIPO_DOCUMENTO,
P.IdentificationNumber AS NÚMERO_DOCUMENTO,
ce.Name AS CIUDAD_EXPEDICIÓN,
tp.name AS EMPLEADO, 
G.Code AS CÓDIGO_GRUPO,
G.Name AS NOMBRE_GRUPO,
pos.Name AS CARGO,
bo.Name AS SEDE,
fu.Name AS UNIDAD_FUNCIONAL,
cc.Name AS CENTRO_COSTO,
et.Name AS TIPO_EMPLEADO,
CASE p.Gender WHEN '1' THEN 'Masculino' WHEN '2' THEN 'Femenino' WHEN '3' THEN 'Otro' END AS GÉNERO,
CASE p.MaritalStatus WHEN '0' THEN 'Soltero' WHEN '1' THEN 'Casado' WHEN '2' THEN 'Divorciado' WHEN '3' THEN 'Viudo' WHEN '4' THEN 'Unión Libre' END AS ESTADO_CIVIL,
CASE p.SocioEconomicStatus WHEN '1' THEN 'Bajo - bajo' WHEN '2' THEN 'Bajo' WHEN '3' THEN 'Medio-bajo' WHEN '4' THEN 'Medio' WHEN '5' THEN 'Medio-alto' WHEN '6' THEN 'Alto' END AS ESTRATO,
p.BirthDate AS FECHA_NACIMIENTO,
DATEDIFF(YEAR,p.BirthDate,[Common].[GETDATE]()) AS EDAD,
cn.Name AS CIUDAD_NACIMIENTO,
p.BloodGroup AS GRUPO_SANGUINEO,
p.RH,
(SELECT TOP 1 ps.ProfessionalCardNumber FROM Common.PersonStudy ps WHERE ps.PersonId = p.Id) AS #TARJETA_PROFESIONAL,
ca.Addresss AS DIRECCION_RESIDENCIAL,
cr.Name AS CIUDAD_RESIDENCIAL,
(SELECT TOP 1 em.Email FROM Common.Email em WHERE em.IdPerson = p.Id) AS CORREO_ELECTRONICO,
(SELECT TOP 1 PH.Phone FROM Common.Phone PH, Common.PhoneType PT WHERE PH.IdPerson = p.Id AND PH.IdPhoneType = PT.Id AND PT.Id = 1) AS TELÉFONO_CELULAR,
(SELECT TOP 1 PH.Phone FROM Common.Phone PH, Common.PhoneType PT WHERE PH.IdPerson = p.Id AND PH.IdPhoneType = PT.Id AND PT.Id = 2) AS TELÉFONO_FIJO,
c.JobBondingDate AS FECHA_INGRESO,
DATEDIFF(MONTH,c.JobBondingDate,[Common].[GETDATE]()) AS ANTIGÜEDAD_MESES,
CASE c.RowType WHEN '1' THEN 'INICIAL' WHEN '2' THEN 'PRÓRROGA' END AS CONTRATO,
ct.Name AS TIPO_DE_CONTRATO,
C.ContractEndingDate AS FECHA_FIN_CONTRATO,
c.BasicSalary AS SALARIO,
b.Name AS BANCO,
c.BankAccountNumber AS #_DE_CUENTA_DE_NÓMINA,
CASE c.bankaccounttype WHEN 1 THEN 'AHORROS' WHEN 2 THEN 'CORRIENTE' END AS TIPO_DE_CUENTA,
(SELECT f.Name FROM Payroll.FundContract FC, Payroll.Fund F WHERE FC.ContractId = c.id and FC.FundType = 1 AND F.Id = FC.FundId and FC.State = 1 AND FC.VoluntaryContribution = 0) as SALUD,
(SELECT f.Name FROM Payroll.FundContract FC, Payroll.Fund F WHERE FC.ContractId = c.id and FC.FundType = 1 AND F.Id = FC.FundId and FC.State = 1 AND FC.VoluntaryContribution = 1) as SALUD_VOLUNTARIA,
(SELECT f.Name FROM Payroll.FundContract FC, Payroll.Fund F WHERE FC.ContractId = c.id and FC.FundType = 2 AND F.Id = FC.FundId and FC.State = 1 AND FC.VoluntaryContribution = 0) as PENSIÓN,
(SELECT f.Name FROM Payroll.FundContract FC, Payroll.Fund F WHERE FC.ContractId = c.id and FC.FundType = 2 AND F.Id = FC.FundId and FC.State = 1 AND FC.VoluntaryContribution = 1) as PENSIÓN_VOLUNTARIA,
(SELECT f.Name FROM Payroll.FundContract FC, Payroll.Fund F WHERE FC.ContractId = c.id and FC.FundType = 3 AND F.Id = FC.FundId and FC.State = 1 AND FC.VoluntaryContribution = 0) as CESANTÍAS,
(SELECT f.Name FROM Payroll.FundContract FC, Payroll.Fund F WHERE FC.ContractId = c.id and FC.FundType = 4 AND F.Id = FC.FundId and FC.State = 1 AND FC.VoluntaryContribution = 0) as ARL,
(SELECT f.Name FROM Payroll.FundContract FC, Payroll.Fund F WHERE FC.ContractId = c.id and FC.FundType = 5 AND F.Id = FC.FundId and FC.State = 1 AND FC.VoluntaryContribution = 0) as CAJA_COMPENSACIÓN,
e.ProfessionalRiskPercentage AS PORCENTAJE_ARL,
(SELECT TOP 1 ST.StudyLevel FROM Payroll.StudyType ST, Common.PersonStudy PS WHERE PS.StudyTypeId = ST.Id AND P.Id = PS.PersonId) AS NIVEL_ACADÉMICO,
(SELECT TOP 1 PP.Name FROM Payroll.Profession PP, Common.PersonProfession CPP WHERE PP.Id = CPP.IdProfession AND P.Id = CPP.PersonId) AS PROFESIÓN,
(SELECT TOP 1 K.Name FROM [Payroll].[Kinship] K, [Payroll].[Relationship] RS WHERE K.Id = RS.KinshipId AND RS.EmployeeId = E.Id) AS PARENTESCO,
(SELECT TOP 1 RS.Name FROM Payroll.[Relationship] RS WHERE rs.EmployeeId = e.Id) AS NOMBRE_FAMILIAR,
(SELECT TOP 1 RS.BirthDate FROM Payroll.[Relationship] RS WHERE rs.EmployeeId = e.Id) AS FECHA_NACIMIENTO_FAMILIAR
From Common.Person P
INNER  JOIN Common.ThirdParty tp ON tp.PersonId = P.id
INNER JOIN Payroll.Employee e ON e.ThirdPartyId = tp.Id
INNER JOIN Payroll.[Contract] c ON c.EmployeeId = e.Id
LEFT JOIN Payroll.Position pos ON pos.Id = c.PositionId
LEFT JOIN Common.City ce ON ce.Id = p.IdentificacionCityId
LEFT JOIN Payroll.FunctionalUnit fu ON fu.Id = c.FunctionalUnitId
LEFT JOIN Payroll.BranchOffice bo ON bo.Id = fu.BranchOfficeId
LEFT JOIN Payroll.CostCenter cc ON cc.Id = e.CostCenterId
LEFT JOIN Payroll.EmployeeType et ON et.Id = e.EmployeeTypeId
LEFT JOIN Common.City cn ON cn.Id = p.BirthCityId
LEFT JOIN Common.[Address] ca ON ca.IdPerson = p.Id
LEFT JOIN Common.City cr ON cr.Id = ca.CityId
LEFT JOIN Payroll.ContractType ct ON ct.Id = c.ContractTypeId
LEFT JOIN Payroll.Bank b ON b.Id = c.BankId
LEFT JOIN Payroll.[Group] g ON g.Id = c.GroupId
Where c.Status = 1 And c.Valid = 1
ORDER BY G.Code, TP.NIT DESC