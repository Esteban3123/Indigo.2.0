-- ===============================================================================================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2021-08-22
-- Description:	Procedimiento que se encarga de obtener los informacion de un soporte de pago de nomina electronica
-- ==============================================================================================================
CREATE PROCEDURE [Payroll].[SP_GetElectronicPayrollPaymentSupport]
	@ElectronicPayrollPaymentSupportId INT
AS
BEGIN
	SET NOCOUNT ON
	
	DECLARE @Table_Result AS TABLE
	(
		StateResult BIT,
		MessageResult VARCHAR(500),
		-------------------------  DATOS DEL CONTRATO -------------------------
		Year INT,
		Month TINYINT,
		LiquidationDateStart DATE,
		LiquidationDateEnd DATE,
		AdmissionDate DATE,
		RetirementDate DATE,
		TimeWorked INT,
		PayrollPeriod TINYINT,
		WorkerType VARCHAR(2),
		WorkerSubType VARCHAR(2),
		HighPensionRisk BIT,
		WorkplaceCountry VARCHAR(2),
		WorkplaceDepartment VARCHAR(2),
		WorkplaceCity VARCHAR(5),
		WorkplaceAddress VARCHAR(500),
		IntegralSalary BIT,
		ContractType TINYINT,
		BasicSalary DECIMAL(18,0),
		---------------------------  DATOS DEL PAGO ---------------------------
		PaymentForm TINYINT,
		PaymentMethod TINYINT,
		PaymentBank VARCHAR(500),
		PaymentAccountType VARCHAR(500),
		PaymentAccountNumber VARCHAR(500),
		PaymentDate DATE
	)

	BEGIN TRY
	
		INSERT INTO @Table_Result 
		(
			StateResult, MessageResult, Year, Month,
			LiquidationDateStart, LiquidationDateEnd
		)
			SELECT	1, CONCAT('Soporte de Pago de Nómina Electrónica del periodo: ', epps.Year, '-', RIGHT(CONCAT('00', epps.Month), 2)), epps.Year, epps.Month,
					DATEFROMPARTS(epps.Year, epps.Month, 1), DATEADD(DAY, -1, DATEADD(MONTH, 1, DATEFROMPARTS(epps.Year, epps.Month, 1)))
			FROM Payroll.ElectronicPayrollPaymentSupport epps WITH (NOLOCK)
			WHERE epps.Id = @ElectronicPayrollPaymentSupportId
		
		/************************************************* CONTRATOS *************************************************/

		DECLARE 
				@ContractId INT = 0,
				@LiquidationDateEnd DATE,
				---------------------------------------------------------------
				@AdmissionDate DATE,
				@RetirementDate DATE,
				@TimeWorked INT = 0

		SELECT 
			@ContractId = pl.ContractId,
			@LiquidationDateEnd = tr.LiquidationDateEnd
		FROM @Table_Result tr
		JOIN Payroll.ElectronicPayrollPaymentSupportDetail eppsd WITH (NOLOCK) ON @ElectronicPayrollPaymentSupportId = eppsd.ElectronicPayrollPaymentSupportId
		JOIN Payroll.Liquidation pl WITH (NOLOCK) ON eppsd.EntityName = 'Liquidation' AND eppsd.EntityId = pl.Id

		--Buscamos el contrato en liquidación de contrato
		IF @ContractId IS NULL OR  @ContractId = 0
		BEGIN
			SELECT 
				@ContractId = cl.ContractId
			FROM @Table_Result tr
			JOIN Payroll.ElectronicPayrollPaymentSupportDetail eppsd WITH (NOLOCK) ON @ElectronicPayrollPaymentSupportId = eppsd.ElectronicPayrollPaymentSupportId
			JOIN Payroll.ContractLiquidation cl WITH (NOLOCK) ON eppsd.EntityName = 'ContractLiquidation' AND eppsd.EntityId = cl.Id
		END

		--Buscamos el contrato en liquidación de primas
		IF @ContractId IS NULL OR  @ContractId = 0
		BEGIN
			SELECT 
				@ContractId = ic.ContractId
			FROM @Table_Result tr
			JOIN Payroll.ElectronicPayrollPaymentSupportDetail eppsd WITH (NOLOCK) ON @ElectronicPayrollPaymentSupportId = eppsd.ElectronicPayrollPaymentSupportId
			JOIN Payroll.IncentivePayment ic WITH (NOLOCK) ON eppsd.EntityName = 'IncentivePayment' AND eppsd.EntityId = ic.Id
		END
		
		-----------------------------------------------------------------------------------------------------------

		SELECT	@AdmissionDate = c.JobBondingDate,
				@RetirementDate = IIF(c.RetirementDate >= ISNULL(@RetirementDate, c.RetirementDate), ISNULL(c.RetirementDate, @RetirementDate), @RetirementDate),
				@TimeWorked = @TimeWorked + Payroll.fnCalculateDays360(c.JobBondingDate, IIF(c.RetirementDate IS NULL, @LiquidationDateEnd, c.RetirementDate))
		FROM Payroll.Contract c WITH (NOLOCK)
		WHERE c.Id = @ContractId
		

		/***********************************************  ASIGNACIONES ***********************************************/

		-- Fecha Ingreso, Fecha Retiro y periocidad de pago
		UPDATE tr
			SET tr.AdmissionDate = IIF(tr.Year = YEAR(@AdmissionDate) AND tr.Month = MONTH(@AdmissionDate), @AdmissionDate, DATEFROMPARTS(9999,12,31)),
				tr.RetirementDate = IIF(tr.Year = YEAR(@RetirementDate) AND tr.Month = MONTH(@RetirementDate), ISNULL(@RetirementDate, DATEFROMPARTS(9999,12,31)), DATEFROMPARTS(9999,12,31)),
				tr.TimeWorked = @TimeWorked,
				tr.PayrollPeriod = (
					CASE c.PaymentPeriod
						WHEN 1 THEN 5 -- Mensual
						WHEN 2 THEN 4 -- Quincenal
						ELSE 6 -- Otro
					END
				),
				tr.WorkerType = ISNULL(et.EmployeeClass, '01'), -- Dependiente
				tr.WorkerSubType = '00', -- No aplica
				tr.HighPensionRisk = 0,
				tr.WorkplaceCountry = 'CO',
				tr.WorkplaceDepartment = department.Code,
				tr.WorkplaceCity = city.Code,
				tr.WorkplaceAddress = bo.Address,
				tr.IntegralSalary = IIF(SalaryType = 2, 1, 0),
				tr.ContractType = CASE ct.ContractClass
									WHEN 3 THEN 1 -- Termino Fijo
									WHEN 4 THEN 2 -- Termino Indefinido
									WHEN 1 THEN 3 -- Obra o Labor
									WHEN 2 THEN 4 -- Aprendizaje lectiva
									WHEN 5 THEN 4 -- Aprendizaje Practica
									WHEN 6 THEN 5 -- Practicas Universitarias
								  END,
				tr.BasicSalary = c.BasicSalary,
				---------------------------------------------------------------
				tr.PaymentForm = 1, -- Contado
				tr.PaymentMethod = 47, -- Transferencia Bancaria
				tr.PaymentBank = b.Name,
				tr.PaymentAccountType = CASE BankAccountType
											WHEN 1 THEN 'Cuenta de Ahorros'
											WHEN 2 THEN 'Cuenta Corriente'
										END,
				tr.PaymentAccountNumber = c.BankAccountNumber,
				tr.PaymentDate = tr.LiquidationDateEnd
		FROM @Table_Result tr
		JOIN Payroll.Contract c WITH (NOLOCK) ON @ContractId = c.Id
		JOIN Payroll.Employee e WITH (NOLOCK) ON c.EmployeeId = e.Id
		JOIN Payroll.EmployeeType et WITH (NOLOCK) ON e.EmployeeTypeId = et.Id
		JOIN Payroll.ContractType ct WITH (NOLOCK) ON c.ContractTypeId = ct.Id
		JOIN Payroll.FunctionalUnit fu WITH (NOLOCK) ON c.FunctionalUnitId = fu.Id
		JOIN Payroll.BranchOffice bo WITH (NOLOCK) ON fu.BranchOfficeId = bo.Id
		JOIN Common.City city WITH (NOLOCK) ON bo.CityId = city.Id
		JOIN Common.Department department WITH (NOLOCK) ON city.DepartamentId = department.Id
		JOIN Common.Country country WITH (NOLOCK) ON department.CountryId = country.Id
		JOIN Payroll.Bank b WITH (NOLOCK) ON c.BankId = b.Id
	END TRY
	BEGIN CATCH	
		DELETE FROM @Table_Result

		INSERT INTO @Table_Result (StateResult, MessageResult)
			SELECT 0, CONCAT('Se presento un error obteniendo informacion del soporte de pago: ', ERROR_MESSAGE(), ' - Linea: ', ERROR_LINE())
	END CATCH

	SELECT * FROM @Table_Result
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Obtiene toda la información necesaria para generar y presentar un soporte de pago de nómina electrónica, dado el identificador único del comprobante. Consolida datos del período de liquidación (año, mes, fechas de inicio y fin), datos del contrato laboral del empleado (fecha de ingreso, retiro, tipo de contrato, salario básico, tipo de trabajador, lugar de trabajo) y datos del pago (forma de pago, banco, tipo y número de cuenta). Busca el contrato asociado recorriendo los distintos tipos de liquidación registrados en el detalle del soporte: nómina ordinaria (Liquidation), liquidación de contrato por retiro (ContractLiquidation) o pago de incentivos y primas (IncentivePayment). Se usa en el módulo de nómina electrónica para construir el comprobante digital de pago que se reporta ante la DIAN, permitiendo visualizar o imprimir el soporte por período para cada trabajador.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_GetElectronicPayrollPaymentSupport';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_GetElectronicPayrollPaymentSupport';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Construye el conjunto de datos requerido para emitir el soporte de pago de nómina electrónica de un empleado, consolidando información del contrato, sede laboral, periodo liquidado y medio de pago.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GetElectronicPayrollPaymentSupport';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir el soporte de pago electrónico identificado por el parámetro en Payroll.ElectronicPayrollPaymentSupport.; El soporte debe tener al menos un detalle en Payroll.ElectronicPayrollPaymentSupportDetail asociado a una entidad de tipo ''Liquidation'', ''ContractLiquidation'' o ''IncentivePayment''.; El contrato resuelto debe existir en Payroll.Contract con sus relaciones a empleado, tipo de empleado, tipo de contrato, unidad funcional, sucursal, ciudad, departamento, país y banco.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GetElectronicPayrollPaymentSupport';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El periodo del soporte se deriva siempre del Year/Month del registro: LiquidationDateStart = primer día del mes y LiquidationDateEnd = último día del mes.; WorkerType usa et.EmployeeClass o ''01'' (Dependiente) por defecto.; WorkerSubType siempre es ''00'' (no aplica) y HighPensionRisk siempre es 0.; WorkplaceCountry siempre se fija en ''CO'' (Colombia).; PaymentForm siempre es 1 (Contado) y PaymentMethod siempre es 47 (Transferencia Bancaria).; PaymentDate se iguala a LiquidationDateEnd (último día del mes liquidado).; TimeWorked se calcula con la función Payroll.fnCalculateDays360 (método 360 días).; El procedimiento prioriza Liquidation, luego ContractLiquidation y por último IncentivePayment para resolver el contrato.; Las fechas no aplicables al mes liquidado se representan con el centinela 9999-12-31.; Ante cualquier excepción se descarta el resultado parcial y se devuelve un mensaje de error trazado con línea.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GetElectronicPayrollPaymentSupport';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Soporte de pago de nómina electrónica; Liquidación de nómina; Liquidación de contrato; Pago de incentivos/primas; Contrato laboral; Tipo de contrato (término fijo/indefinido, obra labor, aprendizaje, prácticas); Periodicidad de pago (mensual/quincenal); Salario integral; Tipo de trabajador (dependiente); Alto riesgo pensional; Lugar de trabajo (país, departamento, ciudad, dirección); Forma y método de pago (contado, transferencia bancaria); Cuenta bancaria (ahorros/corriente); Tiempo trabajado (días 360); Fecha de ingreso y retiro', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GetElectronicPayrollPaymentSupport';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RETURN: Devuelve un único result set con StateResult=1 y los datos del contrato, periodo y medio de pago cuando la consulta es exitosa.; [RETURN_RESULT] RETURN: Si ocurre un error en el TRY, limpia el resultado e inserta un único registro con StateResult=0 y MessageResult que concatena ERROR_MESSAGE() y ERROR_LINE().', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GetElectronicPayrollPaymentSupport';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si El detalle del soporte referencia EntityName=''Liquidation'' → Toma el ContractId desde Payroll.Liquidation y la fecha de fin de liquidación (LiquidationDateEnd) del result set.; si ContractId no se obtuvo desde Liquidation (NULL o 0) → Busca el contrato en Payroll.ContractLiquidation usando EntityName=''ContractLiquidation''.; si ContractId aún es NULL o 0 tras ContractLiquidation → Busca el contrato en Payroll.IncentivePayment usando EntityName=''IncentivePayment''.; si c.PaymentPeriod del contrato → Mapea PayrollPeriod: 1→5 (Mensual), 2→4 (Quincenal), otro→6 (Otro).; si ct.ContractClass del tipo de contrato → Mapea ContractType: 3→1 Término Fijo, 4→2 Término Indefinido, 1→3 Obra/Labor, 2→4 Aprendizaje lectiva, 5→4 Aprendizaje práctica, 6→5 Prácticas Universitarias.; si c.SalaryType = 2 → Marca IntegralSalary=1; en caso contrario 0.; si BankAccountType del contrato → 1→''Cuenta de Ahorros'', 2→''Cuenta Corriente''.; si tr.Year=YEAR(@AdmissionDate) y tr.Month=MONTH(@AdmissionDate) → AdmissionDate del soporte se establece a la fecha real de ingreso; en otro caso se fija a 9999-12-31. else Se asigna 9999-12-31; si tr.Year=YEAR(@RetirementDate) y tr.Month=MONTH(@RetirementDate) → RetirementDate se establece a la fecha real de retiro; en otro caso 9999-12-31. else Se asigna 9999-12-31; si c.RetirementDate IS NULL al calcular tiempo trabajado → Se usa @LiquidationDateEnd como fecha final para fnCalculateDays360; en otro caso se usa c.RetirementDate.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GetElectronicPayrollPaymentSupport';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Payroll.fnCalculateDays360', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GetElectronicPayrollPaymentSupport';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.ElectronicPayrollPaymentSupport; Payroll.ElectronicPayrollPaymentSupportDetail; Payroll.Liquidation; Payroll.ContractLiquidation; Payroll.IncentivePayment; Payroll.Contract; Payroll.Employee; Payroll.EmployeeType; Payroll.ContractType; Payroll.FunctionalUnit; Payroll.BranchOffice; Common.City; Common.Department; Common.Country; Payroll.Bank', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GetElectronicPayrollPaymentSupport';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GetElectronicPayrollPaymentSupport';
-- GO
