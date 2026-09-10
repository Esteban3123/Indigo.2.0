

CREATE VIEW [Report].[ViewListadoPersonalRetirado] as
SELECT 
 CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,  
 ROW_NUMBER() OVER(ORDER BY TP.Nit ASC) AS Row#,
 TP.Nit AS CÉDULA_CIUDADANÍA, TP.NAME NOMBRE_EMPLEADO, BO.Name AS SEDE, pos.Name AS CARGO, CC.Name AS CENTRO_DE_COSTO, C.JobBondingDate AS FECHA_INGRESO, 
 RR.Name AS RAZÓN_RETIRO, 
 YEAR(C.RetirementDate) AS AÑO_RETIRO,
 (SELECT DATENAME (MONTH, DATEADD(MONTH, MONTH(C.RetirementDate)- 1,'1900-01-01')) MES_RETIRO) AS MES_RETIRO,
 DAY(C.RetirementDate) AS DÍA_RETIRO,
 ET.Name AS TIPO_EMPLEADO
, CAST(e.DateModified AS date) AS 'FECHA BUSQUEDA'
, YEAR(e.DateModified) AS 'AÑO FECHA BUSQUEDA'
, MONTH(e.DateModified) AS 'MES AÑO FECHA BUSQUEDA'
, CASE MONTH(e.DateModified) 
	WHEN 1 THEN 'ENERO'
	WHEN 2 THEN 'FEBRERO'
	WHEN 3 THEN 'MARZO'
	WHEN 4 THEN 'ABRIL'
	WHEN 5 THEN 'MAYO'
	WHEN 6 THEN 'JUNIO'
	WHEN 7 THEN 'JULIO'
	WHEN 8 THEN 'AGOSTO'
	WHEN 9 THEN 'SEPTIEMBRE'
	WHEN 10 THEN 'OCTUBRE'
	WHEN 11 THEN 'NOVIEMBRE'
	WHEN 12 THEN 'DICIEMBRE'
  END AS 'MES NOMBRE FECHA BUSQUEDA'
, DAY(e.DateModified) AS 'DIA FECHA BUSQUEDA',
CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
FROM Payroll.Employee e
 Inner Join Common.ThirdParty tp ON tp.Id = e.ThirdPartyId
 Inner Join Payroll.[Contract] c ON c.EmployeeId = e.Id
 Inner Join Payroll.FunctionalUnit fu ON fu.Id = c.FunctionalUnitId
 Inner Join Payroll.BranchOffice bo ON bo.Id = fu.BranchOfficeId
 Inner Join Payroll.CostCenter cc ON cc.Id = e.CostCenterId
 Inner Join Payroll.Position pos ON pos.Id = c.PositionId
 Inner Join Payroll.RetirementReason rr ON rr.Id = c.RetirementReasonId
 Inner Join Payroll.EmployeeType et ON et.Id = e.EmployeeTypeId
WHERE c.Status IN (2,5) And Valid = 0
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporte orientada a consultas de auditoría y seguimiento de talento humano. Consolida información de empleados retirados (contratos con estado 2 o 5 y marca de invalidez en 0), exponiendo datos de identificación, sede, cargo, centro de costo, fecha de ingreso, causal y fecha de retiro desagregada por año, mes y día. Incluye la fecha de última modificación del registro del empleado y un timestamp de actualización en zona horaria de Pakistán.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewListadoPersonalRetirado';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewListadoPersonalRetirado';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista al personal retirado con sus datos contractuales (sede, cargo, centro de costo, motivo y fecha de retiro) para fines de reportería.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewListadoPersonalRetirado';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El empleado debe tener un contrato vinculado en Payroll.Contract con FunctionalUnit, Position, RetirementReason y EmployeeType existentes (INNER JOIN).; El contrato debe estar en estado retirado: c.Status IN (2,5).; El registro del empleado debe estar marcado como Valid = 0.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewListadoPersonalRetirado';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen contratos con Status 2 o 5 (interpretados como estados de retiro).; Solo se incluyen registros con Valid = 0.; El identificador de compañía se obtiene de DB_NAME() truncado a 9 caracteres.; La columna ULT_ACTUAL se calcula convirtiendo GETDATE() a la zona horaria ''Pakistan Standard Time''.; El número de fila (Row#) se asigna ordenando por Nit del tercero ascendente.; Solo aparece personal con todas las relaciones obligatorias presentes (sede, unidad funcional, cargo, centro de costo, motivo de retiro y tipo de empleado), debido al uso de INNER JOIN.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewListadoPersonalRetirado';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Personal retirado; Motivo de retiro; Centro de costo; Cargo; Sede / Sucursal; Tipo de empleado; Contrato laboral; Cédula de ciudadanía; Fecha de ingreso; Fecha de retiro', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewListadoPersonalRetirado';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.ViewListadoPersonalRetirado: Devuelve una fila por empleado cuyo contrato tiene Status IN (2,5) y Valid = 0, incluyendo nombre del mes de retiro en español y marca temporal convertida a la zona ''Pakistan Standard Time''.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewListadoPersonalRetirado';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si MONTH(e.DateModified) entre 1 y 12 → Traduce el número de mes a su nombre en español (ENERO..DICIEMBRE) en la columna MES NOMBRE FECHA BUSQUEDA.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewListadoPersonalRetirado';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.Employee; Common.ThirdParty; Payroll.Contract; Payroll.FunctionalUnit; Payroll.BranchOffice; Payroll.CostCenter; Payroll.Position; Payroll.RetirementReason; Payroll.EmployeeType', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewListadoPersonalRetirado';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewListadoPersonalRetirado';
GO
