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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el listado completo de información contractual y personal del personal activo con contrato vigente. Integra datos de identidad y demografía del empleado (tipo y número de documento, ciudad de expedición, género, estado civil, estrato, fecha de nacimiento, edad, grupo sanguíneo, tarjeta profesional, dirección y contactos), con su información laboral y contractual (cargo, sede, unidad funcional, centro de costo, tipo de empleado, fecha de ingreso, antigüedad en meses, tipo y modalidad de contrato, fecha fin de contrato, salario, datos bancarios para nómina). Además consolida las afiliaciones a seguridad social del empleado: EPS obligatoria y voluntaria, fondo de pensión obligatorio y voluntario, fondo de cesantías, ARL con su porcentaje de riesgo y caja de compensación. Complementa el perfil con nivel académico, profesión, y datos del familiar o beneficiario registrado. Es el informe maestro de planta de personal utilizado por Recursos Humanos para auditorías de nómina, seguimiento contractual y reportes de gestión humana.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'Informe_Listado_Personal';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'Informe_Listado_Personal';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un listado consolidado con la información personal, contractual, académica, financiera y de seguridad social del personal activo y vigente de la organización.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'Informe_Listado_Personal';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben existir contratos en Payroll.Contract con Status=1 y Valid=1 para que el empleado aparezca en el listado.; El empleado debe tener vínculos consistentes Person → ThirdParty → Employee → Contract.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'Informe_Listado_Personal';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen contratos vigentes (Status=1 y Valid=1).; Edad se calcula como diferencia en años entre la fecha de nacimiento y la fecha actual del sistema (Common.GETDATE).; Antigüedad se calcula como diferencia en meses entre JobBondingDate y la fecha actual.; Los fondos reportados (salud, pensión, cesantías, ARL, caja) solo consideran registros con State=1.; Para teléfonos, correo, tarjeta profesional, parentesco y relación familiar se toma solo el primer registro encontrado (TOP 1).; Los códigos de tipo de identificación, género, estado civil y estrato se traducen a descripciones legibles vía CASE.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'Informe_Listado_Personal';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Tipo y número de identificación; Empleado y contrato laboral; Cargo y unidad funcional; Centro de costo; Sede / sucursal; Grupo de nómina; Salario básico y cuenta bancaria de nómina; Fondos de seguridad social: salud, pensión, cesantías, ARL, caja de compensación; Aportes voluntarios a salud y pensión; Porcentaje de riesgo profesional (ARL); Nivel académico y profesión; Tarjeta profesional; Parentesco y beneficiario familiar; Datos demográficos: género, estado civil, estrato, grupo sanguíneo y RH; Antigüedad y vigencia de contrato', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'Informe_Listado_Personal';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve una fila por contrato activo (Status=1 AND Valid=1), ordenada por código de grupo y NIT del tercero descendente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'Informe_Listado_Personal';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si FundContract.FundType = 1 AND VoluntaryContribution = 0 AND State = 1 → Se reporta el fondo como entidad de SALUD obligatoria del empleado.; si FundContract.FundType = 1 AND VoluntaryContribution = 1 AND State = 1 → Se reporta el fondo como SALUD voluntaria.; si FundContract.FundType = 2 AND VoluntaryContribution = 0 AND State = 1 → Se reporta el fondo como PENSIÓN obligatoria.; si FundContract.FundType = 2 AND VoluntaryContribution = 1 AND State = 1 → Se reporta el fondo como PENSIÓN voluntaria.; si FundContract.FundType = 3 AND State = 1 → Se reporta el fondo como CESANTÍAS.; si FundContract.FundType = 4 AND State = 1 → Se reporta el fondo como ARL.; si FundContract.FundType = 5 AND State = 1 → Se reporta el fondo como CAJA DE COMPENSACIÓN.; si PhoneType.Id = 1 → Se toma como teléfono celular.; si PhoneType.Id = 2 → Se toma como teléfono fijo.; si Contract.RowType = 1 → Se etiqueta el contrato como INICIAL. else Si RowType = 2 se etiqueta como PRÓRROGA.; si Contract.BankAccountType = 1 → Cuenta bancaria de nómina marcada como AHORROS. else Si =2 se marca como CORRIENTE.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'Informe_Listado_Personal';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'Informe_Listado_Personal';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Common.Person; Common.ThirdParty; Common.City; Common.Address; Common.Email; Common.Phone; Common.PhoneType; Common.PersonStudy; Common.PersonProfession; Payroll.Employee; Payroll.Contract; Payroll.Position; Payroll.FunctionalUnit; Payroll.BranchOffice; Payroll.CostCenter; Payroll.EmployeeType; Payroll.ContractType; Payroll.Bank; Payroll.Group; Payroll.FundContract; Payroll.Fund; Payroll.StudyType; Payroll.Profession; Payroll.Kinship; Payroll.Relationship', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'Informe_Listado_Personal';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'Informe_Listado_Personal';
-- GO
