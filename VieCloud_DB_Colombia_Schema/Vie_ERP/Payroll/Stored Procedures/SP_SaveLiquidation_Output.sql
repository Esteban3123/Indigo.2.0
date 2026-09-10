-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2021-04-12
-- Description:	Procedimiento que se encarga de guardar, actualizar, anular, confirmar, reversar una liquidación de nómina
-- =============================================
CREATE PROCEDURE [Payroll].[SP_SaveLiquidation_Output]
    @LiquidationXml AS XML,
	@UserCode AS VARCHAR(20),
	------------------------------------------------------
	@CodeResult Int OUTPUT,
	@MessageResult VARCHAR(MAX) OUTPUT,
	------------------------------------------------------
	@Id INT OUTPUT,
	@Code VARCHAR(20) OUTPUT
AS
BEGIN
	SET NOCOUNT ON
	
	--Se declaran las variables para obtener la cabecera
	DECLARE @ICBFThirdParty INT,
			@SENAThirdParty INT,
			------------------------------
			@Message VARCHAR(MAX)

	-- Grupos a Liquidar
	DECLARE @Groups TABLE
	(
		GroupId int,
		GroupCode VARCHAR(20),
		GroupName VARCHAR(150),
		PayrollInitialDate DATE,
		PayrollEndDate DATE
	)

	-- Cabecera de Liquidacion
	DECLARE @Liquidation TABLE
	(
		Id int identity(1,1),
		PayrollId int,
		GroupId int,
		EmployeeId int,
		CostCenterId int,
		InitialContractNumber int,
		ContractId int,
		WorkCenterId int,
		RegisterStatus char(1),
		PayrollDateLiquidated date,
		LiquidationPeriod char(1),
		BasicSalary numeric(18, 0),
		SalaryType char(1),
		PayrollDays tinyint,
		DaysWorked tinyint,
		ProvisionDays tinyint,
		RecargoNocturno numeric(18, 0),
		RecargoNocturnoFestivo numeric(18, 0),
		Overtime numeric(18, 0),
		EveningOvertime numeric(18, 0),
		DiurnalOvertime numeric(18, 0),
		HoursHolidays tinyint,
		HolidaysEveningHours tinyint,
		ValueTransportingRelief numeric(18, 0),
		VacationDays tinyint,
		VacationValueEnjoy numeric(18, 0),
		VacationValueLiquidated numeric(18, 0),
		BonusValueServices numeric(18, 0),
		ValueOtherBonuses numeric(18, 0),
		PensionFundId int,
		PensionContributionValue numeric(18, 0),
		PensionContributionDays smallint,
		PensionEnrollmentDays smallint,
		EmployerPensionContributionValue numeric(18, 0),
		VoluntaryPensionFundId int,
		VoluntaryContributionPensionValue numeric(18, 0),
		PensionSolidarityFundId int,
		PensionSolidarityFundValueContribution numeric(18, 0),
		PensionSolidarityFundContributionDays smallint,
		PensionSolidarityFundEnrollmentDays smallint,
		EmployeeHealthContributionValue numeric(18, 0),
		HealthFundId int,
		QuoteHealthDays smallint,
		AffiliateHealthDays smallint,
		EmployerHealthContributionValue numeric(18, 0),
		AdditionalPUTValue numeric(18, 0),
		HealthJCBLicenses numeric(18, 0),
		HealthContributionLicensesValue numeric(18, 0),
		HealthLicensesDays tinyint,
		VoluntaryHealthFundId int,
		VoluntaryHealthContributionValue numeric(18, 0),
		TotalBaseRetention numeric(18, 0),
		ExemptValueRetention numeric(18, 0),
		RealBaseRetention numeric(18, 0),
		CalculatedWithholdingValue numeric(18, 0),
		RetentionPercentageApplied tinyint,
		AccumulatedOtherAccrued numeric(18, 0),
		AccumulatedOtherDeducted numeric(18, 0),
		DeductingAccumulated numeric(18, 0),
		PeriodJCB numeric(18, 0),
		PensionJCB numeric(18, 0),
		HealthJCB numeric(18, 0),
		AccumulatedBenefit numeric(18, 0),
		AccumulatedDisabilityValue numeric(18, 0),
		DisabilityDays tinyint,
		AmbulatoryDisabilityDays tinyint,
		AmbulatoryDisabilityInitialDate date,
		AmbulatoryDisabilityEndDate date,
		AmbulatoryDisabilityAuthorizationNumber varchar(30),
		AmbulatoryDisabilityValue numeric(18, 0),
		DisabilityHospitalInitialDate date,
		DisabilityHospitalEndDate date,
		DisabilityHospitalReleasedNumber varchar(30),
		DisabilityHospitalDays tinyint,
		DisabilityHospitalValue numeric(18, 0),
		MaternityLeaveDays numeric(18, 0),
		MaternityLeaveInitialDate date,
		MaternityLeaveEndDate date,
		MaternityLeaveAutorizationNumber varchar(30),
		MaternityLeaveValue numeric(18, 0),
		VacationInitialDate date,
		VacationEndDate date,
		LicenseDays tinyint,
		LicenseValue numeric(18, 0),
		UnpaidLicenseValue numeric(18, 0),
		UnpaidLicenseAutorizarionNumber varchar(30),
		UnpaidLicenseDays tinyint,
		UnpaidLicenseInitialDate date,
		UnpaidLicenseEndDate date,
		SanctionInitialDate date,
		SanctionEndDate date,
		SanctionValue numeric(18, 0),
		SanctionDays tinyint,
		OccupationalRisksContributionValue numeric(18, 0),
		QuotedOccupationalRisksDays tinyint,
		OccupationalRisksDisabilityValue numeric(18, 0),
		OccupationalRisksDays tinyint,
		OccupationalRisksDisabilityAutorizationNumber varchar(30),
		OccupationalRisksDisabilityInitialDate date,
		OccupationalRisksDisabilityEndDate date,
		OccupationalRisksFundId int,
		PermissionsValue numeric(18, 0),
		UnemploymentAccumulated numeric(18, 0),
		CompensationAccumulated numeric(18, 0),
		VacationHealthJCB tinyint,
		VacationHealthContributionValueEmployee numeric(18, 0),
		VacationHealthContributionValueEmployer numeric(18, 0),
		VacationPensionJCB tinyint,
		VacationPensionContributionValueEmployee numeric(18, 0),
		VacationPensionContributionValueEmployer numeric(18, 0),
		AccountingVouchersNumber int,
		TotalAccrued numeric(18, 0),
		TotalDeducted numeric(18, 0),
		TotalPaid numeric(18, 0),
		PermissionDays tinyint,
		SenaContributionValue numeric(18, 0),
		FamilyCompensationFundContributionValue numeric(18, 0),
		ICBFContributionValue numeric(18, 0),
		ParafiscalContribution numeric(18, 0),
		ProvisionsValue numeric(18, 0),
		ProvisionVacation numeric(18, 0),
		ProvisionIncentive numeric(18, 0),
		ProvisionInterestsUnemployment numeric(18, 0),
		VacationNumberBussinesDays tinyint,
		CompletePayroll bit,
		PayrollProcessDate date,
		PayrollConfirmationDate date,
		RetirementLiquidationDate date,
		PayrollProcessUser int,
		PayrollConfirmationUser int,
		QuotedCompensationDays tinyint,
		IBCUnemployment numeric(18, 0),
		IBCUnemploymentNoSanctions numeric(18, 0),
		IBCSENA numeric(18, 0),
		IBCCompensationFund numeric(18, 0),
		IBCICBF numeric(18, 0),
		IBCVacation numeric(18, 0),
		IBCIncentivePayment numeric(18, 0),
		IBCOccupationalRisks numeric(18, 0),
		BankId int,
		BankAccountNumber varchar(20),
		UnemployementFundId int,
		HousingDeductionValue decimal(18, 0),
		DependentsDeduction decimal(18, 0),
		ProcedureTypeRTF tinyint
	)

	-- Detalle de Liquidacion
	DECLARE @LiquidationDetail TABLE
	(
		Id int identity(1,1),
		PayrollId int,
		RegisterStatus int,
		PayrollDate date,
		LiquidationPeriod char(1),
		ConceptClass char(3),
		ConceptId int,
		ConceptCode varchar(4),
		ConceptDetail varchar(300),
		ConceptTotalValue numeric(18, 0),
		AccruedValue numeric(18, 0),
		DeductedValue numeric(18, 0),
		RetentionBase decimal(18, 0),
		RetentionPercentage decimal(6, 3),
		InitialBalance numeric(18, 0),
		ConceptType char(1),
		AgreementsId int,
		AgreementsDId int,
		ConceptFormulate varchar(max),
		ReplaceConceptFormulate varchar(max),
		DistribuirGasto bit,
		InabilityCollect numeric(18, 0),
		SpendingInability numeric(18, 0),
		TotalNumberHours int,
		IdThirdParty int,
		TypeArticleRTF tinyint,
		RetentionId int
	)

	DECLARE @LiquidationMessage TABLE
	(
		Id int identity(1,1),
		PayrollId int,
		Message VARCHAR(500)
	)

	BEGIN TRY
		-- Insertamos los grupos a calcular
		INSERT INTO @Groups (GroupId)
			SELECT DISTINCT
				t.x.value('GroupId[1]','INT')
			FROM @LiquidationXml.nodes('/Liquidation') t(x)

		-- Determinamos la fecha a liquidar
		UPDATE tg
			SET tg.GroupCode = g.Code,
				tg.GroupName = g.Name,
				tg.PayrollInitialDate = g.NextDateLiquidation,
				tg.PayrollEndDate = CASE g.Liquidation
										WHEN 1 THEN DATEADD(DAY, -1, DATEADD(MONTH, 1, g.NextDateLiquidation))
										WHEN 2 THEN 
											CASE 
												WHEN DAY(g.NextDateLiquidation) = 1 THEN DATEFROMPARTS(YEAR(g.NextDateLiquidation), MONTH(g.NextDateLiquidation), 15)
												WHEN DAY(g.NextDateLiquidation) = 16 THEN DATEADD(DAY, -1, DATEADD(MONTH, 1, g.NextDateLiquidation))
											END
									END
		FROM @Groups tg
		JOIN Payroll.[Group] g WITH (NOLOCK) ON tg.GroupId = g.Id

		-- Insertamos los empleados a liquidar
		INSERT INTO @Liquidation
		(
			GroupId, EmployeeId, WorkCenterId, ContractId, RegisterStatus, PayrollDateLiquidated
		)
		SELECT c.GroupId, c.EmployeeId, e.WorkCenterId, c.Id, '', tg.PayrollEndDate
		FROM @Groups tg
		JOIN Payroll.Contract c WITH (NOLOCK) ON tg.GroupId = c.GroupId
		JOIN Payroll.Employee e WITH (NOLOCK) ON c.EmployeeId = e.Id
		WHERE c.Status IN (1, 4, 5) AND c.LiquidationPayroll = 1
			AND c.ContractEndingDate >= tg.PayrollInitialDate AND c.ContractEndingDate <= tg.PayrollEndDate
			AND ISNULL(c.LastLiquidationDate, DATEADD(DAY, 1, tg.PayrollInitialDate)) < tg.PayrollInitialDate

		-- Se obtiene el tercero del ICBF
		SELECT @ICBFThirdParty = tp.Id
		FROM Common.ThirdParty tp WITH (NOLOCK)
		WHERE tp.Nit = '899999239'

		-- Se obtiene el tercero del SENA
		SELECT @SENAThirdParty = tp.Id
		FROM Common.ThirdParty tp WITH (NOLOCK)
		WHERE tp.Nit = '899999034'

		/***********************************************  VALIDACIONES ***********************************************/

		IF NOT EXISTS (SELECT 1 FROM @Liquidation)
		BEGIN
			SELECT @CodeResult = 999,
				   @MessageResult = 'No se encontraron empleados para liquidar'
			RETURN
		END

		IF NOT EXISTS (SELECT 1 FROM Payroll.PayrollSettings WITH (NOLOCK))
		BEGIN
			SELECT @CodeResult = 999,
				   @MessageResult = 'No se encontraron Parámetros de nómina definidos'
			RETURN
		END

		IF EXISTS (SELECT 1 FROM Payroll.PayrollSettings WITH (NOLOCK) WHERE IdRetentionConcepts IS NULL)
		BEGIN
			SELECT @CodeResult = 999,
				   @MessageResult = 'No ha configurado el concepto de retención para el pago de dicho concepto. Agréguelo en parámetros de nómina'
			RETURN
		END

		IF EXISTS
		(
			SELECT 1
			FROM @Groups tg
			LEFT JOIN Payroll.AuthorizationConceptGroup acg WITH (NOLOCK) ON tg.GroupId = acg.GroupId
			WHERE acg.Id IS NULL
		)
		BEGIN
			SELECT @Message = STUFF((
					SELECT DISTINCT CHAR(13) + CHAR(10) + ' - ' + tg.GroupCode + ' - ' + tg.GroupName
					FROM @Groups tg
					LEFT JOIN Payroll.AuthorizationConceptGroup acg WITH (NOLOCK) ON tg.GroupId = acg.GroupId
					WHERE acg.Id IS NULL
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			SELECT @CodeResult = 999,
					@MessageResult = 'No ha autorizado ningún concepto de nómina para los siguientes grupos: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '')
			RETURN
		END

		IF @ICBFThirdParty IS NULL
		BEGIN
			SELECT @CodeResult = 999,
				   @MessageResult = 'No ha configurado el concepto de retención para el pago de dicho concepto. Agréguelo en parámetros de nómina'
			RETURN
		END

		IF @SENAThirdParty IS NULL
		BEGIN
			SELECT @CodeResult = 999,
				   @MessageResult = 'No ha configurado el concepto de retención para el pago de dicho concepto. Agréguelo en parámetros de nómina'
			RETURN
		END
		
		/************************************************** PROCESO **************************************************/

SELECT * FROM @Liquidation

		/************************************************* RESULTADO *************************************************/

		SELECT @CodeResult = 0, 
			   @MessageResult = 'Se realizó la liquidación de nómina correctamente'
	END TRY
	BEGIN CATCH
		SELECT @CodeResult = 999, 
			   @MessageResult = 'SP_SaveLiquidation: ' + ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(10))
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento central de nómina que permite guardar, actualizar, anular, confirmar, reversar y procesar liquidaciones de nómina para uno o varios grupos de empleados. Recibe la información completa de la liquidación en formato XML, valida y cruza datos contra los grupos de nómina, contratos laborales y empleados activos, y persiste todos los conceptos devengados y deducidos (salario, horas extras, vacaciones, incapacidades, aportes a seguridad social, parafiscales, retención en la fuente, provisiones, entre otros). Es el punto de entrada principal para el ciclo de liquidación de nómina, cubriendo desde la liquidación periódica hasta la liquidación definitiva por retiro, y devuelve códigos de resultado para confirmar el éxito o fallo de la operación.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_SaveLiquidation_Output';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_SaveLiquidation_Output';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Prepara la liquidación de nómina identificando empleados elegibles por grupo y validando la configuración global previa antes de ejecutar el cálculo.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_SaveLiquidation_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir al menos un registro en Payroll.PayrollSettings.; El campo IdRetentionConcepts en Payroll.PayrollSettings no puede ser NULL.; Todos los grupos recibidos en el XML deben tener al menos un registro en Payroll.AuthorizationConceptGroup.; Debe existir un tercero en Common.ThirdParty con Nit=''899999239'' (ICBF).; Debe existir un tercero en Common.ThirdParty con Nit=''899999034'' (SENA).; Debe existir al menos un contrato elegible (Status IN (1,4,5), LiquidationPayroll=1, ContractEndingDate dentro del rango del periodo y no liquidado previamente en el periodo).', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_SaveLiquidation_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El tercero ICBF se identifica siempre por Nit=''899999239'' y el SENA por Nit=''899999034'' (NITs hardcoded en Colombia).; Solo se liquidan contratos cuyo ContractEndingDate cae dentro del rango [PayrollInitialDate, PayrollEndDate] del grupo.; No se reliquidan contratos cuya LastLiquidationDate sea posterior o igual a PayrollInitialDate.; Los códigos de error de negocio se reportan siempre con CodeResult=999; el éxito con CodeResult=0.; El procedimiento usa WITH (NOLOCK) en todas las consultas a tablas físicas de soporte (lectura sucia tolerada).', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_SaveLiquidation_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Liquidación de nómina; Grupo de nómina; Periodicidad mensual y quincenal; Contrato laboral; ICBF; SENA; Concepto de retención; Autorización de conceptos por grupo; Tercero (NIT); Fecha de finalización de contrato', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_SaveLiquidation_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] OUTPUT @CodeResult/@MessageResult: Si no hay empleados elegibles tras filtrar contratos, retorna CodeResult=999 con mensaje ''No se encontraron empleados para liquidar''.; [RETURN_RESULT] OUTPUT @CodeResult/@MessageResult: Si no existe Payroll.PayrollSettings o IdRetentionConcepts es NULL, retorna 999 indicando falta de parámetros/concepto de retención.; [RETURN_RESULT] OUTPUT @CodeResult/@MessageResult: Si algún grupo no tiene conceptos autorizados en Payroll.AuthorizationConceptGroup, retorna 999 listando los códigos y nombres de los grupos sin autorización.; [RETURN_RESULT] OUTPUT @CodeResult/@MessageResult: Si no existe el tercero ICBF (Nit 899999239) o SENA (Nit 899999034), retorna 999 indicando falta de configuración del concepto de retención.; [RETURN_RESULT] RESULTSET @Liquidation: Cuando se superan todas las validaciones, devuelve el conjunto de empleados a liquidar (SELECT * FROM @Liquidation) y CodeResult=0 con mensaje de éxito.; [RETURN_RESULT] OUTPUT @CodeResult/@MessageResult: En caso de excepción, captura el error y retorna 999 con prefijo ''SP_SaveLiquidation:'' incluyendo ERROR_MESSAGE y ERROR_LINE.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_SaveLiquidation_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Payroll.Group.Liquidation = 1 (mensual) → PayrollEndDate = último día del mes siguiente a NextDateLiquidation (DATEADD(DAY,-1,DATEADD(MONTH,1,NextDateLiquidation))); si Payroll.Group.Liquidation = 2 (quincenal) y DAY(NextDateLiquidation)=1 → PayrollEndDate = día 15 del mismo mes else Si DAY(NextDateLiquidation)=16, PayrollEndDate = último día del mes (DATEADD(DAY,-1,DATEADD(MONTH,1,NextDateLiquidation))); si Contract.Status IN (1,4,5) AND LiquidationPayroll=1 AND ContractEndingDate dentro del periodo AND (LastLiquidationDate IS NULL o anterior a PayrollInitialDate) → El contrato/empleado se incluye en la tabla de liquidación con PayrollDateLiquidated = PayrollEndDate', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_SaveLiquidation_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.Group; Payroll.Contract; Payroll.Employee; Common.ThirdParty; Payroll.PayrollSettings; Payroll.AuthorizationConceptGroup', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_SaveLiquidation_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_SaveLiquidation_Output';
-- GO
