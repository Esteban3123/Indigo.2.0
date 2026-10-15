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