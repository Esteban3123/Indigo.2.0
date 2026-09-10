

-- =============================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 08/05/2017
-- Description:	Procedimiento que se encarga de guardar en las tablas de Liquidation y LiquidationDetail de Payroll
-- =============================================
CREATE PROCEDURE [Payroll].[SP_SaveInitialBalancePayroll] 
	@XmlObject as Xml,
	@CodeUser as varchar(20)
AS
BEGIN

	--Tabla que almacena los errores o aciertos de cada item que se recorre para poder devolverlos
	declare @TableReturn table(Id int IDENTITY PRIMARY KEY, CodeResult int, MessageResult varchar(max))

	--Tabla para almacenar los items del listado que viene en el xml
	declare @TableXmlObject table(Id int IDENTITY PRIMARY KEY, Nit varchar(50), EmployeeId int, PayrollDate varchar(20), DaysWorked varchar(50), TotalAccrued varchar(50), TotalDeducted varchar(50), 
	PensionJCB varchar(50), PensionContributionValue varchar(50), ConceptIdPensionContributionValue int, EmployerPensionContributionValue varchar(50), ConceptIdEmployerPensionContributionValue int, 
	HealthJCB varchar(50), EmployeeHealthContributionValue varchar(50), ConceptIdEmployeeHealthContributionValue int, EmployerHealthContributionValue varchar(50), ConceptIdEmployerHealthContributionValue int, 
	IBCIncentivePayment varchar(50), ProvisionIncentive varchar(50), ConceptIdProvisionIncentive int, IBCVacation varchar(50), ProvisionVacation varchar(50), ConceptIdProvisionVacation int, 
	IBCUnemployment varchar(50), UnemploymentAccumulated varchar(50), ConceptIdUnemploymentAccumulated int, ProvisionInterestsUnemployment varchar(50), ConceptIdProvisionInterestsUnemployment int,	
	AmbulatoryDisabilityValue varchar(50), ConceptIdAmbulatoryDisabilityValue int, DisabilityHospitalValue varchar(50), ConceptIdDisabilityHospitalValue int, MaternityLeaveValue varchar(50), 
	ConceptIdMaternityLeaveValue int, IBCSENA varchar(50), SenaContributionValue varchar(50), ConceptIdSenaContributionValue int, IBCICBF varchar(50), ICBFContributionValue varchar(50), 
	ConceptIdICBFContributionValue int, IBCCompensationFund varchar(50), FamilyCompensationFundContributionValue varchar(50), ConceptIdFamilyCompensationFundContributionValue int, RealBaseRetention varchar(50), 
	CalculatedWithholdingValue varchar(50), ConceptIdCalculatedWithholdingValue int, RecargoNocturno varchar(50), ConceptIdRecargoNocturno int, TotalPaid decimal(18,0), 
	SanctionDays varchar(50), Overtime varchar(50), ConceptIdOvertime int, RecargoNocturnoFestivo varchar(50), ConceptIdRecargoNocturnoFestivo int, ValorDominicalOrdinario varchar(50), ConceptIdValorDominical int)

	--Id de la liquidación
	declare @LiquidationId as int

	--Id del grupo
	declare @GroupId as int

	--Id del centro costo
	declare @CostCenterId as int

	--Id del contrato
	declare @ContractId as int

	--Id del centro de trabajo
	declare @WorkCenterId as int

	--Salario basico
	declare @BasicSalary as numeric(18,0)

	--Tipo de salario
	declare @SalaryType as char(1)

	--Id fondo de salud
	declare @HealthFundId as int

	--Id fondo de pension
	declare @PensionFundId as int

	--Clase del concepto
	declare @ConceptClass as char(3)

	--Código del concepto
	declare @ConceptCode as varchar(4)

	--Detalle del concepto
	declare @ConceptDetail as varchar(300)
	
	--Tipo de concepto
	declare @ConceptType as tinyint

	Begin try
	
		insert into @TableXmlObject
		select 
		t.x.value('Nit[1]','varchar(50)') as Nit,
		t.x.value('EmployeeId[1]','int') as EmployeeId,
		t.x.value('PayrollDate[1]','varchar(20)') as PayrollDate,
		t.x.value('DaysWorked[1]','varchar(50)') as DaysWorked,
		t.x.value('TotalAccrued[1]','varchar(50)') as TotalAccrued,
		case when t.x.value('TotalDeducted[1]','varchar(50)') = '' then 0 else t.x.value('TotalDeducted[1]','varchar(50)') end as TotalDeducted,
		case when t.x.value('PensionJCB[1]','varchar(50)') = '' then 0 else t.x.value('PensionJCB[1]','varchar(50)') end as PensionJCB,
		case when t.x.value('PensionContributionValue[1]','varchar(50)') = '' then 0 else t.x.value('PensionContributionValue[1]','varchar(50)') end as PensionContributionValue,
		t.x.value('ConceptIdPensionContributionValue[1]','int') as ConceptIdPensionContributionValue,
		case when t.x.value('EmployerPensionContributionValue[1]','varchar(50)') = '' then 0 else t.x.value('EmployerPensionContributionValue[1]','varchar(50)') end as EmployerPensionContributionValue,
		t.x.value('ConceptIdEmployerPensionContributionValue[1]','int') as ConceptIdEmployerPensionContributionValue,
		case when t.x.value('HealthJCB[1]','varchar(50)') = '' then 0 else t.x.value('HealthJCB[1]','varchar(50)') end as HealthJCB,
		case when t.x.value('EmployeeHealthContributionValue[1]','varchar(50)') = '' then 0 else t.x.value('EmployeeHealthContributionValue[1]','varchar(50)') end as EmployeeHealthContributionValue,
		t.x.value('ConceptIdEmployeeHealthContributionValue[1]','int') as ConceptIdEmployeeHealthContributionValue,
		case when t.x.value('EmployerHealthContributionValue[1]','varchar(50)') = '' then 0 else t.x.value('EmployerHealthContributionValue[1]','varchar(50)') end as EmployerHealthContributionValue,
		t.x.value('ConceptIdEmployerHealthContributionValue[1]','int') as ConceptIdEmployerHealthContributionValue,
		case when t.x.value('IBCIncentivePayment[1]','varchar(50)') = '' then 0 else t.x.value('IBCIncentivePayment[1]','varchar(50)') end as IBCIncentivePayment,
		case when t.x.value('ProvisionIncentive[1]','varchar(50)') = '' then 0 else t.x.value('ProvisionIncentive[1]','varchar(50)') end as ProvisionIncentive,
		t.x.value('ConceptIdProvisionIncentive[1]','int') as ConceptIdProvisionIncentive,
		case when t.x.value('IBCVacation[1]','varchar(50)') = '' then 0 else t.x.value('IBCVacation[1]','varchar(50)') end as IBCVacation,
		case when t.x.value('ProvisionVacation[1]','varchar(50)') = '' then 0 else t.x.value('ProvisionVacation[1]','varchar(50)') end as ProvisionVacation,
		t.x.value('ConceptIdProvisionVacation[1]','int') as ConceptIdProvisionVacation,
		case when t.x.value('IBCUnemployment[1]','varchar(50)') = '' then 0 else t.x.value('IBCUnemployment[1]','varchar(50)') end as IBCUnemployment,
		case when t.x.value('UnemploymentAccumulated[1]','varchar(50)') = '' then 0 else t.x.value('UnemploymentAccumulated[1]','varchar(50)') end as UnemploymentAccumulated,
		t.x.value('ConceptIdUnemploymentAccumulated[1]','int') as ConceptIdUnemploymentAccumulated,
		case when t.x.value('ProvisionInterestsUnemployment[1]','varchar(50)') = '' then 0 else t.x.value('ProvisionInterestsUnemployment[1]','varchar(50)') end as ProvisionInterestsUnemployment,
		t.x.value('ConceptIdProvisionInterestsUnemployment[1]','int') as ConceptIdProvisionInterestsUnemployment,
		case when t.x.value('AmbulatoryDisabilityValue[1]','varchar(50)') = '' then 0 else t.x.value('AmbulatoryDisabilityValue[1]','varchar(50)') end as AmbulatoryDisabilityValue,
		t.x.value('ConceptIdAmbulatoryDisabilityValue[1]','int') as ConceptIdAmbulatoryDisabilityValue,
		case when t.x.value('DisabilityHospitalValue[1]','varchar(50)') = '' then 0 else t.x.value('DisabilityHospitalValue[1]','varchar(50)') end as DisabilityHospitalValue,
		t.x.value('ConceptIdDisabilityHospitalValue[1]','int') as ConceptIdDisabilityHospitalValue,
		case when t.x.value('MaternityLeaveValue[1]','varchar(50)') = '' then 0 else t.x.value('MaternityLeaveValue[1]','varchar(50)') end as MaternityLeaveValue,
		t.x.value('ConceptIdMaternityLeaveValue[1]','int') as ConceptIdMaternityLeaveValue,
		case when t.x.value('IBCSENA[1]','varchar(50)') = '' then 0 else t.x.value('IBCSENA[1]','varchar(50)') end as IBCSENA,
		case when t.x.value('SenaContributionValue[1]','varchar(50)') = '' then 0 else t.x.value('SenaContributionValue[1]','varchar(50)') end as SenaContributionValue,
		t.x.value('ConceptIdSenaContributionValue[1]','int') as ConceptIdSenaContributionValue,
		case when t.x.value('IBCICBF[1]','varchar(50)') = '' then 0 else t.x.value('IBCICBF[1]','varchar(50)') end as IBCICBF,
		case when t.x.value('ICBFContributionValue[1]','varchar(50)') = '' then 0 else t.x.value('ICBFContributionValue[1]','varchar(50)') end as ICBFContributionValue,
		t.x.value('ConceptIdICBFContributionValue[1]','int') as ConceptIdICBFContributionValue,
		case when t.x.value('IBCCompensationFund[1]','varchar(50)') = '' then 0 else t.x.value('IBCCompensationFund[1]','varchar(50)') end as IBCCompensationFund,
		case when t.x.value('FamilyCompensationFundContributionValue[1]','varchar(50)') = '' then 0 else t.x.value('FamilyCompensationFundContributionValue[1]','varchar(50)') end as FamilyCompensationFundContributionValue,
		t.x.value('ConceptIdFamilyCompensationFundContributionValue[1]','int') as ConceptIdFamilyCompensationFundContributionValue,
		case when t.x.value('RealBaseRetention[1]','varchar(50)') = '' then 0 else t.x.value('RealBaseRetention[1]','varchar(50)') end as RealBaseRetention,
		case when t.x.value('CalculatedWithholdingValue[1]','varchar(50)') = '' then 0 else t.x.value('CalculatedWithholdingValue[1]','varchar(50)') end as CalculatedWithholdingValue,
		t.x.value('ConceptIdCalculatedWithholdingValue[1]','int') as ConceptIdCalculatedWithholdingValue,
		case when t.x.value('RecargoNocturno[1]','varchar(50)') = '' then 0 else t.x.value('RecargoNocturno[1]','varchar(50)') end as RecargoNocturno,
		t.x.value('ConceptIdRecargoNocturno[1]','int') as ConceptIdRecargoNocturno,
		t.x.value('TotalPaid[1]','decimal(18,0)') as TotalPaid,
		case when t.x.value('SanctionDays[1]','varchar(50)') = '' then 0 else t.x.value('SanctionDays[1]','varchar(50)') end as SanctionDays,
		case when t.x.value('Overtime[1]','varchar(50)') = '' then 0 else t.x.value('Overtime[1]','varchar(50)') end as Overtime,
		t.x.value('ConceptIdOvertime[1]','int') as ConceptIdOvertime,
		-- CONCEPTOS NUEVOS
		case when t.x.value('RecargoNocturnoFestivo[1]','varchar(50)') = '' then 0 else t.x.value('RecargoNocturnoFestivo[1]','varchar(50)') end as RecargoNocturnoFestivo,
		t.x.value('ConceptIdRecargoNocturnoFestivo[1]','int') as ConceptIdRecargoNocturnoFestivo,
		case when t.x.value('ValorDominicalOrdinario[1]','varchar(50)') = '' then 0 else t.x.value('ValorDominicalOrdinario[1]','varchar(50)') end as ValorDominicalOrdinario,
		t.x.value('ConceptIdValorDominical[1]','int') as ConceptIdValorDominical
		from @XmlObject.nodes('/Data/Row') t(x)
		
		--Id del registro
		declare @Id as int
				
		--Nit del tercero
		declare @Nit as varchar(50)

		--Id del empleado
		declare @EmployeeId as int

		--Fecha de nómina
		declare @PayrollDate as varchar(20)

		--Días trabajados
		declare @DaysWorked as varchar(50)

		--Total devengado
		declare @TotalAccrued as varchar(50)

		--Total deducido
		declare @TotalDeducted as varchar(50)

		--Base pensión
		declare @PensionJCB as varchar(50)

		--Aporte pension empleado
		declare @PensionContributionValue as varchar(50)
		
		--Id del concepto para aporte pension empleado
		declare @ConceptIdPensionContributionValue as int

		--Aporte pension patrono
		declare @EmployerPensionContributionValue as varchar(50)
		
		--Id del concepto para aporte pension patrono
		declare @ConceptIdEmployerPensionContributionValue as int

		--Base salud
		declare @HealthJCB as varchar(50)

		--Aporte salud empleado
		declare @EmployeeHealthContributionValue as varchar(50)
		
		--Id del concepto para aporte salud empleado
		declare @ConceptIdEmployeeHealthContributionValue as int

		--Aporte salud patrono
		declare @EmployerHealthContributionValue as varchar(50)
		
		--Id del concepto para aporte salud patrono
		declare @ConceptIdEmployerHealthContributionValue as int

		--Base primas
		declare @IBCIncentivePayment as varchar(50)

		--Provision primas
		declare @ProvisionIncentive as varchar(50)
		
		--Id del concepto para provision primas
		declare @ConceptIdProvisionIncentive as int

		--Base vacaciones
		declare @IBCVacation as varchar(50)

		--Provision vacaciones
		declare @ProvisionVacation as varchar(50)
		
		--Id del concepto para provision vacaciones
		declare @ConceptIdProvisionVacation as int

		--Base cesantias
		declare @IBCUnemployment as varchar(50)

		--Provision cesantias
		declare @UnemploymentAccumulated as varchar(50)
		
		--Id del concepto para provision cesantias
		declare @ConceptIdUnemploymentAccumulated as int

		--Provision intereses cesantias
		declare @ProvisionInterestsUnemployment as varchar(50)
		
		--Id del concepto para provision intereses cesantias
		declare @ConceptIdProvisionInterestsUnemployment as int

		--Valor incapacidad ambulatoria
		declare @AmbulatoryDisabilityValue as varchar(50)
		
		--Id del concepto para valor incapacidad ambulatoria
		declare @ConceptIdAmbulatoryDisabilityValue as int

		--Valor incapacidad hospitalaria
		declare @DisabilityHospitalValue as varchar(50)
		
		--Id del concepto para valor incapacidad hospitalaria
		declare @ConceptIdDisabilityHospitalValue as int

		--Valor licencia maternidad
		declare @MaternityLeaveValue as varchar(50)
		
		--Id del concepto para valor licencia maternidad
		declare @ConceptIdMaternityLeaveValue as int

		--Base sena
		declare @IBCSENA as varchar(50)

		--Aporte sena
		declare @SenaContributionValue as varchar(50)
		
		--Id del concepto para aporte sena
		declare @ConceptIdSenaContributionValue as int

		--Base icbf
		declare @IBCICBF as varchar(50)

		--Aporte icbf
		declare @ICBFContributionValue as varchar(50)
		
		--Id del concepto para aporte icbf
		declare @ConceptIdICBFContributionValue as int

		--Base caja compensacion
		declare @IBCCompensationFund as varchar(50)

		--Aporte caja compensacion
		declare @FamilyCompensationFundContributionValue as varchar(50)
		
		--Id del concepto para aporte caja compensacion
		declare @ConceptIdFamilyCompensationFundContributionValue as int

		--Base retención
		declare @RealBaseRetention as varchar(50)

		--Valor retencion
		declare @CalculatedWithholdingValue as varchar(50)
		
		--Id del concepto para valor retencion
		declare @ConceptIdCalculatedWithholdingValue as int

		--Recargos
		declare @RecargoNocturno as varchar(50)
		
		--Id del concepto para recargo
		declare @ConceptIdRecargoNocturno as int

		--Días de sanción
		declare @SanctionDays as varchar(50)

		--Horas extras
		declare @Overtime as varchar(50)
		
		--Id del concepto para horas extras
		declare @ConceptIdOvertime as int

		--Recargos Nocturnos Festivos
		declare @RecargoNocturnoFestivo as varchar(50)
		
		--Id del concepto para Recargos Nocturnos Festivos
		declare @ConceptIdRecargoNocturnoFestivo as int

		--Valor Dominical
		declare @ValorDominicalOrdinario as varchar(50)
		
		--Id del concepto para Valor Dominical
		declare @ConceptIdValorDominical as int

		--Se recorre el cursor
		declare InfoItem Cursor For Select [Id], [Nit], [EmployeeId], [PayrollDate], [DaysWorked], [TotalAccrued], [TotalDeducted], [PensionJCB], [PensionContributionValue], [ConceptIdPensionContributionValue], 
		[EmployerPensionContributionValue], [ConceptIdEmployerPensionContributionValue], [HealthJCB], [EmployeeHealthContributionValue], [ConceptIdEmployeeHealthContributionValue], [EmployerHealthContributionValue], 
		[ConceptIdEmployerHealthContributionValue], [IBCIncentivePayment], [ProvisionIncentive], [ConceptIdProvisionIncentive], [IBCVacation], [ProvisionVacation], [ConceptIdProvisionVacation], 
		[IBCUnemployment], [UnemploymentAccumulated], [ConceptIdUnemploymentAccumulated], [ProvisionInterestsUnemployment], [ConceptIdProvisionInterestsUnemployment], [AmbulatoryDisabilityValue], 
		[ConceptIdAmbulatoryDisabilityValue], [DisabilityHospitalValue], [ConceptIdDisabilityHospitalValue], [MaternityLeaveValue], [ConceptIdMaternityLeaveValue], [IBCSENA], [SenaContributionValue], 
		[ConceptIdSenaContributionValue], [IBCICBF], [ICBFContributionValue], [ConceptIdICBFContributionValue], [IBCCompensationFund], [FamilyCompensationFundContributionValue], 
		[ConceptIdFamilyCompensationFundContributionValue], [RealBaseRetention], [CalculatedWithholdingValue], [ConceptIdCalculatedWithholdingValue], [RecargoNocturno], [ConceptIdRecargoNocturno],
		[SanctionDays], [Overtime], [ConceptIdOvertime], [RecargoNocturnoFestivo], [ConceptIdRecargoNocturnoFestivo], [ValorDominicalOrdinario], [ConceptIdValorDominical] From @TableXmlObject

		Open InfoItem

		Fetch Next From InfoItem Into @Id, @Nit, @EmployeeId, @PayrollDate, @DaysWorked, @TotalAccrued, @TotalDeducted, @PensionJCB, @PensionContributionValue, @ConceptIdPensionContributionValue, 
		@EmployerPensionContributionValue, @ConceptIdEmployerPensionContributionValue, @HealthJCB, @EmployeeHealthContributionValue, @ConceptIdEmployeeHealthContributionValue, @EmployerHealthContributionValue, 
		@ConceptIdEmployerHealthContributionValue, @IBCIncentivePayment, @ProvisionIncentive, @ConceptIdProvisionIncentive, @IBCVacation, @ProvisionVacation, @ConceptIdProvisionVacation, @IBCUnemployment, 
		@UnemploymentAccumulated, @ConceptIdUnemploymentAccumulated, @ProvisionInterestsUnemployment, @ConceptIdProvisionInterestsUnemployment, @AmbulatoryDisabilityValue, @ConceptIdAmbulatoryDisabilityValue, 
		@DisabilityHospitalValue, @ConceptIdDisabilityHospitalValue, @MaternityLeaveValue, @ConceptIdMaternityLeaveValue, @IBCSENA, @SenaContributionValue, @ConceptIdSenaContributionValue, @IBCICBF,
		@ICBFContributionValue, @ConceptIdICBFContributionValue, @IBCCompensationFund, @FamilyCompensationFundContributionValue, @ConceptIdFamilyCompensationFundContributionValue, @RealBaseRetention,
		@CalculatedWithholdingValue, @ConceptIdCalculatedWithholdingValue, @RecargoNocturno, @ConceptIdRecargoNocturno, @SanctionDays, @Overtime, @ConceptIdOvertime, 
		@RecargoNocturnoFestivo, @ConceptIdRecargoNocturnoFestivo, @ValorDominicalOrdinario, @ConceptIdValorDominical

		While @@fetch_status = 0
		Begin
			
			--Se valida que los días trabajados sea mayor a cero
			if @DaysWorked <= 0
			Begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				insert into @TableReturn(CodeResult, MessageResult) values(999, 'No se puede guardar el empleado con cédula ' + @Nit + ' porque los días trabajados tiene que ser mayor a cero')
				--Se pasa a la siguiente posicion del cursor
				GoTo NextFetch
			End

			--Se valida que el total devengado sea mayor a cero
			if @TotalAccrued <= 0
			Begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				insert into @TableReturn(CodeResult, MessageResult) values(999, 'No se puede guardar el empleado con cédula ' + @Nit + ' porque el total devengado tiene que ser mayor a cero')
				--Se pasa a la siguiente posicion del cursor
				GoTo NextFetch
			End

			--Se valida que el total deducido sea mayor a cero, siempre y cuando tenga valor porque no es obligatorio
			if @TotalDeducted < 0
			Begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				insert into @TableReturn(CodeResult, MessageResult) values(999, 'No se puede guardar el empleado con cédula ' + @Nit + ' porque el total deducido tiene que ser mayor a cero')
				--Se pasa a la siguiente posicion del cursor
				GoTo NextFetch
			End

			--Se valida que el empleado no tenga registros en la tabla Liquidation con el mismo mes y el mismo año
			if (select COUNT(*) from Payroll.Liquidation where EmployeeId = @EmployeeId and MONTH(PayrollDateLiquidated) = MONTH(@PayrollDate) and YEAR(PayrollDateLiquidated) = YEAR(@PayrollDate)) > 0
			Begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				insert into @TableReturn(CodeResult, MessageResult) values(999, 'No se puede guardar el empleado con cédula ' + @Nit + ' porque ya tiene un registro con el mes ' + CAST(MONTH(@PayrollDate) as varchar(50)) + ' y el año ' + CAST(YEAR(@PayrollDate) as varchar(50)))
				--Se pasa a la siguiente posicion del cursor
				GoTo NextFetch
			End

			--Se obtienen los campos necesarios del empleado
			select @GroupId = c.GroupId, @CostCenterId = e.CostCenterId, @ContractId = c.Id, @WorkCenterId = e.WorkCenterId, @BasicSalary = c.BasicSalary, @SalaryType = ct.SalaryType
			from Payroll.Employee e
			inner join Payroll.[Contract] c on c.EmployeeId = e.Id
			inner join Payroll.ContractType ct on ct.Id = c.ContractTypeId
			where e.Id = @EmployeeId and c.Valid = 1 and c.Status = 1
			
			--Id del fondo de salud
			select @HealthFundId = FundId from Payroll.FundContract where ContractId = @ContractId and State = 1 and FundType = 1 
			
			--Id del fondo de pension
			select @PensionFundId = FundId from Payroll.FundContract where ContractId = @ContractId and State = 1 and FundType = 2 

			-- Se agrega el InitialContractNumber pues no se estaba cargando
			DECLARE @InitialContractNumber int = 0

			SELECT @InitialContractNumber = InitialContractNumber FROM Payroll.Contract where Id = @ContractId

			--Se inserta en la cabecera Liquidation item a item
			insert into Payroll.Liquidation([GroupId], [EmployeeId], [CostCenterId], [InitialContractNumber], [ContractId], [WorkCenterId], [RegisterStatus], [PayrollDateLiquidated], [LiquidationPeriod], [BasicSalary], 
			[SalaryType], [PayrollDays], [DaysWorked], [ProvisionDays], [RecargoNocturno], [RecargoNocturnoFestivo], [Overtime], [EveningOvertime], [DiurnalOvertime], [HoursHolidays], [HolidaysEveningHours], 
			[ValueTransportingRelief], [VacationDays], [VacationValueEnjoy], [VacationValueLiquidated], [BonusValueServices], [ValueOtherBonuses], [PensionFundId], [PensionContributionValue], [PensionContributionDays], 
			[PensionEnrollmentDays], [EmployerPensionContributionValue], [VoluntaryPensionFundId], [VoluntaryContributionPensionValue], [PensionSolidarityFundId], [PensionSolidarityFundValueContribution], 
			[PensionSolidarityFundContributionDays], [PensionSolidarityFundEnrollmentDays], [EmployeeHealthContributionValue], [HealthFundId], [QuoteHealthDays], [AffiliateHealthDays], [EmployerHealthContributionValue], 
			[AdditionalPUTValue], [HealthJCBLicenses], [HealthContributionLicensesValue], [HealthLicensesDays], [VoluntaryHealthFundId], [VoluntaryHealthContributionValue], [TotalBaseRetention], 
			[ExemptValueRetention], [RealBaseRetention], [CalculatedWithholdingValue], [RetentionPercentageApplied], [AccumulatedOtherAccrued], [AccumulatedOtherDeducted], [DeductingAccumulated], [PeriodJCB], 
			[PensionJCB], [HealthJCB], [AccumulatedBenefit], [AccumulatedDisabilityValue], [DisabilityDays], [AmbulatoryDisabilityDays], [AmbulatoryDisabilityInitialDate], [AmbulatoryDisabilityEndDate], 
			[AmbulatoryDisabilityAuthorizationNumber], [AmbulatoryDisabilityValue], [DisabilityHospitalInitialDate], [DisabilityHospitalEndDate], [DisabilityHospitalReleasedNumber], [DisabilityHospitalDays], 
			[DisabilityHospitalValue], [MaternityLeaveDays], [MaternityLeaveInitialDate], [MaternityLeaveEndDate], [MaternityLeaveAutorizationNumber], [MaternityLeaveValue], [VacationInitialDate], [VacationEndDate], 
			[LicenseDays], [LicenseValue], [UnpaidLicenseValue], [UnpaidLicenseAutorizarionNumber], [UnpaidLicenseDays], [UnpaidLicenseInitialDate], [UnpaidLicenseEndDate], [SanctionInitialDate], [SanctionEndDate], 
			[SanctionValue], [SanctionDays], [OccupationalRisksContributionValue], [QuotedOccupationalRisksDays], [OccupationalRisksDisabilityValue], [OccupationalRisksDays], 
			[OccupationalRisksDisabilityAutorizationNumber], [OccupationalRisksDisabilityInitialDate], [OccupationalRisksDisabilityEndDate], [OccupationalRisksFundId], [PermissionsValue], [UnemploymentAccumulated], 
			[CompensationAccumulated], [VacationHealthJCB], [VacationHealthContributionValueEmployee], [VacationHealthContributionValueEmployer], [VacationPensionJCB], [VacationPensionContributionValueEmployee], 
			[VacationPensionContributionValueEmployer], [AccountingVouchersNumber], [TotalAccrued], [TotalDeducted], [TotalPaid], [PermissionDays], [SenaContributionValue], [FamilyCompensationFundContributionValue], 
			[ICBFContributionValue], [ParafiscalContribution], [ProvisionsValue], [ProvisionVacation], [ProvisionIncentive], [ProvisionInterestsUnemployment], [VacationNumberBussinesDays], [CompletePayroll], 
			[PayrollProcessDate], [PayrollConfirmationDate], [RetirementLiquidationDate], [PayrollProcessUser], [PayrollConfirmationUser], [QuotedCompensationDays], [IBCUnemployment], [IBCUnemploymentNoSanctions], 
			[IBCSENA], [IBCCompensationFund], [IBCICBF], [IBCVacation], [IBCIncentivePayment], [IBCOccupationalRisks], [BankId], [BankAccountNumber], [UnemployementFundId], [HousingDeductionValue], 
			[DependentsDeduction], [ProcedureTypeRTF])
			values
			(@GroupId, @EmployeeId, @CostCenterId, @InitialContractNumber, @ContractId, @WorkCenterId, 'S', @PayrollDate, 1, @BasicSalary, @SalaryType, 30, @DaysWorked, @DaysWorked, @RecargoNocturno, @RecargoNocturnoFestivo, @Overtime, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, @PensionFundId, @PensionContributionValue, @DaysWorked, 0, @EmployerPensionContributionValue, null, 0, null, 0, 0, 0, @EmployeeHealthContributionValue, @HealthFundId, 0, 0, 
			@EmployerHealthContributionValue, 0, 0, 0, 0, null, 0, @RealBaseRetention, 0, @RealBaseRetention, @CalculatedWithholdingValue, 0, 0, 0, 0, 0, @PensionJCB, @HealthJCB, 0, 0, 0, 0, null, null, null, 
			0, null, null, null, 0, @DisabilityHospitalValue, 0, null, null, null, @MaternityLeaveValue, null, null, 0, 0, 0, null, 0, null, null, null, null, 0, @SanctionDays, 0, 0, 0, 0, null, null, null, null, 0, 
			@UnemploymentAccumulated, 0, 0, 0, 0, 0, 0, 0, null, @TotalAccrued, @TotalDeducted, cast(@TotalAccrued as numeric(18,0)) - cast(@TotalDeducted as numeric(18,0)), 0, @SenaContributionValue, 
			@FamilyCompensationFundContributionValue, @ICBFContributionValue, 0, 0, @ProvisionVacation, @ProvisionIncentive, @ProvisionInterestsUnemployment, 0, 1, [Common].[GETDATE](), [Common].[GETDATE](), null, 999, 999, 0,
			@IBCUnemployment, 0, @IBCSENA, @IBCCompensationFund, @IBCICBF, @IBCVacation, @IBCIncentivePayment, 0, null, null, null, 0, 0, 1)	

			--Se obtiene el id de la cabecera
			set @LiquidationId = SCOPE_IDENTITY()

			--Se empiezan a insertar los detalles con cada valor

			--Si el aporte pension empleado es mayor a cero
			if @PensionContributionValue > 0
			Begin
				--Se obtienen los datos del concepto
				select @ConceptClass = ConceptClass, @ConceptCode = Code,  @ConceptDetail = [Name], @ConceptType = ConceptType from Payroll.Concept where Id = @ConceptIdPensionContributionValue

				insert into [Payroll].[LiquidationDetail]
			   ([PayrollId], [RegisterStatus], [PayrollDate], [LiquidationPeriod], [ConceptClass], [ConceptId], [ConceptCode], [ConceptDetail], [ConceptTotalValue], [AccruedValue], [DeductedValue], 
			   [RetentionBase], [RetentionPercentage], [InitialBalance], [ConceptType], [AgreementsId], [AgreementsDId], [ConceptFormulate], [ReplaceConceptFormulate], [DistribuirGasto], [InabilityCollect],
			   [SpendingInability], [TotalNumberHours], [IdThirdParty], [TypeArticleRTF])
			   values
			   (@LiquidationId, 1, @PayrollDate, '1', @ConceptClass, @ConceptIdPensionContributionValue, @ConceptCode, @ConceptDetail, @PensionContributionValue, 
			   case when @ConceptType = 1 or @ConceptType = 3 then @PensionContributionValue else 0 end, case when @ConceptType = 2 then @PensionContributionValue else 0 end, 0, 0, 
			   0, @ConceptType, null, null, null, null, 0, 0, 0, 0, null, 0)
			End

			--Si el aporte pension patrono es mayor a cero
			if @EmployerPensionContributionValue > 0
			Begin
				--Se obtienen los datos del concepto
				select @ConceptClass = ConceptClass, @ConceptCode = Code,  @ConceptDetail = [Name], @ConceptType = ConceptType from Payroll.Concept where Id = @ConceptIdEmployerPensionContributionValue

				insert into [Payroll].[LiquidationDetail]
			   ([PayrollId], [RegisterStatus], [PayrollDate], [LiquidationPeriod], [ConceptClass], [ConceptId], [ConceptCode], [ConceptDetail], [ConceptTotalValue], [AccruedValue], [DeductedValue], 
			   [RetentionBase], [RetentionPercentage], [InitialBalance], [ConceptType], [AgreementsId], [AgreementsDId], [ConceptFormulate], [ReplaceConceptFormulate], [DistribuirGasto], [InabilityCollect],
			   [SpendingInability], [TotalNumberHours], [IdThirdParty], [TypeArticleRTF])
			   values
			   (@LiquidationId, 1, @PayrollDate, '1', @ConceptClass, @ConceptIdEmployerPensionContributionValue, @ConceptCode, @ConceptDetail, @EmployerPensionContributionValue, 
			   case when @ConceptType = 1 or @ConceptType = 3 then @EmployerPensionContributionValue else 0 end, case when @ConceptType = 2 then @EmployerPensionContributionValue else 0 end, 0, 0, 
			   0, @ConceptType, null, null, null, null, 0, 0, 0, 0, null, 0)
			End

			--Si el aporte salud empleado es mayor a cero
			if @EmployeeHealthContributionValue > 0
			Begin
				--Se obtienen los datos del concepto
				select @ConceptClass = ConceptClass, @ConceptCode = Code,  @ConceptDetail = [Name], @ConceptType = ConceptType from Payroll.Concept where Id = @ConceptIdEmployeeHealthContributionValue

				insert into [Payroll].[LiquidationDetail]
			   ([PayrollId], [RegisterStatus], [PayrollDate], [LiquidationPeriod], [ConceptClass], [ConceptId], [ConceptCode], [ConceptDetail], [ConceptTotalValue], [AccruedValue], [DeductedValue], 
			   [RetentionBase], [RetentionPercentage], [InitialBalance], [ConceptType], [AgreementsId], [AgreementsDId], [ConceptFormulate], [ReplaceConceptFormulate], [DistribuirGasto], [InabilityCollect],
			   [SpendingInability], [TotalNumberHours], [IdThirdParty], [TypeArticleRTF])
			   values
			   (@LiquidationId, 1, @PayrollDate, '1', @ConceptClass, @ConceptIdEmployeeHealthContributionValue, @ConceptCode, @ConceptDetail, @EmployeeHealthContributionValue, 
			   case when @ConceptType = 1 or @ConceptType = 3 then @EmployeeHealthContributionValue else 0 end, case when @ConceptType = 2 then @EmployeeHealthContributionValue else 0 end, 0, 0, 
			   0, @ConceptType, null, null, null, null, 0, 0, 0, 0, null, 0)
			End

			--Si el aporte salud patrono es mayor a cero
			if @EmployerHealthContributionValue > 0
			Begin
				--Se obtienen los datos del concepto
				select @ConceptClass = ConceptClass, @ConceptCode = Code,  @ConceptDetail = [Name], @ConceptType = ConceptType from Payroll.Concept where Id = @ConceptIdEmployerHealthContributionValue

				insert into [Payroll].[LiquidationDetail]
			   ([PayrollId], [RegisterStatus], [PayrollDate], [LiquidationPeriod], [ConceptClass], [ConceptId], [ConceptCode], [ConceptDetail], [ConceptTotalValue], [AccruedValue], [DeductedValue], 
			   [RetentionBase], [RetentionPercentage], [InitialBalance], [ConceptType], [AgreementsId], [AgreementsDId], [ConceptFormulate], [ReplaceConceptFormulate], [DistribuirGasto], [InabilityCollect],
			   [SpendingInability], [TotalNumberHours], [IdThirdParty], [TypeArticleRTF])
			   values
			   (@LiquidationId, 1, @PayrollDate, '1', @ConceptClass, @ConceptIdEmployerHealthContributionValue, @ConceptCode, @ConceptDetail, @EmployerHealthContributionValue, 
			   case when @ConceptType = 1 or @ConceptType = 3 then @EmployerHealthContributionValue else 0 end, case when @ConceptType = 2 then @EmployerHealthContributionValue else 0 end, 0, 0, 
			   0, @ConceptType, null, null, null, null, 0, 0, 0, 0, null, 0)
			End

			--Si el provision primas es mayor a cero
			if @ProvisionIncentive > 0
			Begin
				--Se obtienen los datos del concepto
				select @ConceptClass = ConceptClass, @ConceptCode = Code,  @ConceptDetail = [Name], @ConceptType = ConceptType from Payroll.Concept where Id = @ConceptIdProvisionIncentive

				insert into [Payroll].[LiquidationDetail]
			   ([PayrollId], [RegisterStatus], [PayrollDate], [LiquidationPeriod], [ConceptClass], [ConceptId], [ConceptCode], [ConceptDetail], [ConceptTotalValue], [AccruedValue], [DeductedValue], 
			   [RetentionBase], [RetentionPercentage], [InitialBalance], [ConceptType], [AgreementsId], [AgreementsDId], [ConceptFormulate], [ReplaceConceptFormulate], [DistribuirGasto], [InabilityCollect],
			   [SpendingInability], [TotalNumberHours], [IdThirdParty], [TypeArticleRTF])
			   values
			   (@LiquidationId, 1, @PayrollDate, '1', @ConceptClass, @ConceptIdProvisionIncentive, @ConceptCode, @ConceptDetail, @ProvisionIncentive, 
			   case when @ConceptType = 1 or @ConceptType = 3 then @ProvisionIncentive else 0 end, case when @ConceptType = 2 then @ProvisionIncentive else 0 end, 0, 0, 
			   0, @ConceptType, null, null, null, null, 0, 0, 0, 0, null, 0)
			End

			--Si el provision vacaciones es mayor a cero
			if @ProvisionVacation > 0
			Begin
				--Se obtienen los datos del concepto
				select @ConceptClass = ConceptClass, @ConceptCode = Code,  @ConceptDetail = [Name], @ConceptType = ConceptType from Payroll.Concept where Id = @ConceptIdProvisionVacation

				insert into [Payroll].[LiquidationDetail]
			   ([PayrollId], [RegisterStatus], [PayrollDate], [LiquidationPeriod], [ConceptClass], [ConceptId], [ConceptCode], [ConceptDetail], [ConceptTotalValue], [AccruedValue], [DeductedValue], 
			   [RetentionBase], [RetentionPercentage], [InitialBalance], [ConceptType], [AgreementsId], [AgreementsDId], [ConceptFormulate], [ReplaceConceptFormulate], [DistribuirGasto], [InabilityCollect],
			   [SpendingInability], [TotalNumberHours], [IdThirdParty], [TypeArticleRTF])
			   values
			   (@LiquidationId, 1, @PayrollDate, '1', @ConceptClass, @ConceptIdProvisionVacation, @ConceptCode, @ConceptDetail, @ProvisionVacation, 
			   case when @ConceptType = 1 or @ConceptType = 3 then @ProvisionVacation else 0 end, case when @ConceptType = 2 then @ProvisionVacation else 0 end, 0, 0, 
			   0, @ConceptType, null, null, null, null, 0, 0, 0, 0, null, 0)
			End

			--Si el provision cesantias es mayor a cero
			if @UnemploymentAccumulated > 0
			Begin
				--Se obtienen los datos del concepto
				select @ConceptClass = ConceptClass, @ConceptCode = Code,  @ConceptDetail = [Name], @ConceptType = ConceptType from Payroll.Concept where Id = @ConceptIdUnemploymentAccumulated

				insert into [Payroll].[LiquidationDetail]
			   ([PayrollId], [RegisterStatus], [PayrollDate], [LiquidationPeriod], [ConceptClass], [ConceptId], [ConceptCode], [ConceptDetail], [ConceptTotalValue], [AccruedValue], [DeductedValue], 
			   [RetentionBase], [RetentionPercentage], [InitialBalance], [ConceptType], [AgreementsId], [AgreementsDId], [ConceptFormulate], [ReplaceConceptFormulate], [DistribuirGasto], [InabilityCollect],
			   [SpendingInability], [TotalNumberHours], [IdThirdParty], [TypeArticleRTF])
			   values
			   (@LiquidationId, 1, @PayrollDate, '1', @ConceptClass, @ConceptIdUnemploymentAccumulated, @ConceptCode, @ConceptDetail, @UnemploymentAccumulated, 
			   case when @ConceptType = 1 or @ConceptType = 3 then @UnemploymentAccumulated else 0 end, case when @ConceptType = 2 then @UnemploymentAccumulated else 0 end, 0, 0, 
			   0, @ConceptType, null, null, null, null, 0, 0, 0, 0, null, 0)
			End

			--Si el provision intereses cesantias es mayor a cero
			if @ProvisionInterestsUnemployment > 0
			Begin
				--Se obtienen los datos del concepto
				select @ConceptClass = ConceptClass, @ConceptCode = Code,  @ConceptDetail = [Name], @ConceptType = ConceptType from Payroll.Concept where Id = @ConceptIdProvisionInterestsUnemployment

				insert into [Payroll].[LiquidationDetail]
			   ([PayrollId], [RegisterStatus], [PayrollDate], [LiquidationPeriod], [ConceptClass], [ConceptId], [ConceptCode], [ConceptDetail], [ConceptTotalValue], [AccruedValue], [DeductedValue], 
			   [RetentionBase], [RetentionPercentage], [InitialBalance], [ConceptType], [AgreementsId], [AgreementsDId], [ConceptFormulate], [ReplaceConceptFormulate], [DistribuirGasto], [InabilityCollect],
			   [SpendingInability], [TotalNumberHours], [IdThirdParty], [TypeArticleRTF])
			   values
			   (@LiquidationId, 1, @PayrollDate, '1', @ConceptClass, @ConceptIdProvisionInterestsUnemployment, @ConceptCode, @ConceptDetail, @ProvisionInterestsUnemployment, 
			   case when @ConceptType = 1 or @ConceptType = 3 then @ProvisionInterestsUnemployment else 0 end, case when @ConceptType = 2 then @ProvisionInterestsUnemployment else 0 end, 0, 0, 
			   0, @ConceptType, null, null, null, null, 0, 0, 0, 0, null, 0)
			End

			--Si el valor incapacidad ambulatoria es mayor a cero
			if @AmbulatoryDisabilityValue > 0
			Begin
				--Se obtienen los datos del concepto
				select @ConceptClass = ConceptClass, @ConceptCode = Code,  @ConceptDetail = [Name], @ConceptType = ConceptType from Payroll.Concept where Id = @ConceptIdAmbulatoryDisabilityValue

				insert into [Payroll].[LiquidationDetail]
			   ([PayrollId], [RegisterStatus], [PayrollDate], [LiquidationPeriod], [ConceptClass], [ConceptId], [ConceptCode], [ConceptDetail], [ConceptTotalValue], [AccruedValue], [DeductedValue], 
			   [RetentionBase], [RetentionPercentage], [InitialBalance], [ConceptType], [AgreementsId], [AgreementsDId], [ConceptFormulate], [ReplaceConceptFormulate], [DistribuirGasto], [InabilityCollect],
			   [SpendingInability], [TotalNumberHours], [IdThirdParty], [TypeArticleRTF])
			   values
			   (@LiquidationId, 1, @PayrollDate, '1', @ConceptClass, @ConceptIdAmbulatoryDisabilityValue, @ConceptCode, @ConceptDetail, @AmbulatoryDisabilityValue, 
			   case when @ConceptType = 1 or @ConceptType = 3 then @AmbulatoryDisabilityValue else 0 end, case when @ConceptType = 2 then @AmbulatoryDisabilityValue else 0 end, 0, 0, 
			   0, @ConceptType, null, null, null, null, 0, 0, 0, 0, null, 0)
			End

			--Si el valor incapacidad hospitalaria es mayor a cero
			if @DisabilityHospitalValue > 0
			Begin
				--Se obtienen los datos del concepto
				select @ConceptClass = ConceptClass, @ConceptCode = Code,  @ConceptDetail = [Name], @ConceptType = ConceptType from Payroll.Concept where Id = @ConceptIdDisabilityHospitalValue

				insert into [Payroll].[LiquidationDetail]
			   ([PayrollId], [RegisterStatus], [PayrollDate], [LiquidationPeriod], [ConceptClass], [ConceptId], [ConceptCode], [ConceptDetail], [ConceptTotalValue], [AccruedValue], [DeductedValue], 
			   [RetentionBase], [RetentionPercentage], [InitialBalance], [ConceptType], [AgreementsId], [AgreementsDId], [ConceptFormulate], [ReplaceConceptFormulate], [DistribuirGasto], [InabilityCollect],
			   [SpendingInability], [TotalNumberHours], [IdThirdParty], [TypeArticleRTF])
			   values
			   (@LiquidationId, 1, @PayrollDate, '1', @ConceptClass, @ConceptIdDisabilityHospitalValue, @ConceptCode, @ConceptDetail, @DisabilityHospitalValue, 
			   case when @ConceptType = 1 or @ConceptType = 3 then @DisabilityHospitalValue else 0 end, case when @ConceptType = 2 then @DisabilityHospitalValue else 0 end, 0, 0, 
			   0, @ConceptType, null, null, null, null, 0, 0, 0, 0, null, 0)
			End

			--Si el valor licencia maternidad es mayor a cero
			if @MaternityLeaveValue > 0
			Begin
				--Se obtienen los datos del concepto
				select @ConceptClass = ConceptClass, @ConceptCode = Code,  @ConceptDetail = [Name], @ConceptType = ConceptType from Payroll.Concept where Id = @ConceptIdMaternityLeaveValue

				insert into [Payroll].[LiquidationDetail]
			   ([PayrollId], [RegisterStatus], [PayrollDate], [LiquidationPeriod], [ConceptClass], [ConceptId], [ConceptCode], [ConceptDetail], [ConceptTotalValue], [AccruedValue], [DeductedValue], 
			   [RetentionBase], [RetentionPercentage], [InitialBalance], [ConceptType], [AgreementsId], [AgreementsDId], [ConceptFormulate], [ReplaceConceptFormulate], [DistribuirGasto], [InabilityCollect],
			   [SpendingInability], [TotalNumberHours], [IdThirdParty], [TypeArticleRTF])
			   values
			   (@LiquidationId, 1, @PayrollDate, '1', @ConceptClass, @ConceptIdMaternityLeaveValue, @ConceptCode, @ConceptDetail, @MaternityLeaveValue, 
			   case when @ConceptType = 1 or @ConceptType = 3 then @MaternityLeaveValue else 0 end, case when @ConceptType = 2 then @MaternityLeaveValue else 0 end, 0, 0, 
			   0, @ConceptType, null, null, null, null, 0, 0, 0, 0, null, 0)
			End

			--Si el aporte sena es mayor a cero
			if @SenaContributionValue > 0
			Begin
				--Se obtienen los datos del concepto
				select @ConceptClass = ConceptClass, @ConceptCode = Code,  @ConceptDetail = [Name], @ConceptType = ConceptType from Payroll.Concept where Id = @ConceptIdSenaContributionValue

				insert into [Payroll].[LiquidationDetail]
			   ([PayrollId], [RegisterStatus], [PayrollDate], [LiquidationPeriod], [ConceptClass], [ConceptId], [ConceptCode], [ConceptDetail], [ConceptTotalValue], [AccruedValue], [DeductedValue], 
			   [RetentionBase], [RetentionPercentage], [InitialBalance], [ConceptType], [AgreementsId], [AgreementsDId], [ConceptFormulate], [ReplaceConceptFormulate], [DistribuirGasto], [InabilityCollect],
			   [SpendingInability], [TotalNumberHours], [IdThirdParty], [TypeArticleRTF])
			   values
			   (@LiquidationId, 1, @PayrollDate, '1', @ConceptClass, @ConceptIdSenaContributionValue, @ConceptCode, @ConceptDetail, @SenaContributionValue, 
			   case when @ConceptType = 1 or @ConceptType = 3 then @SenaContributionValue else 0 end, case when @ConceptType = 2 then @SenaContributionValue else 0 end, 0, 0, 
			   0, @ConceptType, null, null, null, null, 0, 0, 0, 0, null, 0)
			End

			--Si el aporte icbf es mayor a cero
			if @ICBFContributionValue > 0
			Begin
				--Se obtienen los datos del concepto
				select @ConceptClass = ConceptClass, @ConceptCode = Code,  @ConceptDetail = [Name], @ConceptType = ConceptType from Payroll.Concept where Id = @ConceptIdICBFContributionValue

				insert into [Payroll].[LiquidationDetail]
			   ([PayrollId], [RegisterStatus], [PayrollDate], [LiquidationPeriod], [ConceptClass], [ConceptId], [ConceptCode], [ConceptDetail], [ConceptTotalValue], [AccruedValue], [DeductedValue], 
			   [RetentionBase], [RetentionPercentage], [InitialBalance], [ConceptType], [AgreementsId], [AgreementsDId], [ConceptFormulate], [ReplaceConceptFormulate], [DistribuirGasto], [InabilityCollect],
			   [SpendingInability], [TotalNumberHours], [IdThirdParty], [TypeArticleRTF])
			   values
			   (@LiquidationId, 1, @PayrollDate, '1', @ConceptClass, @ConceptIdICBFContributionValue, @ConceptCode, @ConceptDetail, @ICBFContributionValue, 
			   case when @ConceptType = 1 or @ConceptType = 3 then @ICBFContributionValue else 0 end, case when @ConceptType = 2 then @ICBFContributionValue else 0 end, 0, 0, 
			   0, @ConceptType, null, null, null, null, 0, 0, 0, 0, null, 0)
			End

			--Si el aporte caja compensacion es mayor a cero
			if @FamilyCompensationFundContributionValue > 0
			Begin
				--Se obtienen los datos del concepto
				select @ConceptClass = ConceptClass, @ConceptCode = Code,  @ConceptDetail = [Name], @ConceptType = ConceptType from Payroll.Concept where Id = @ConceptIdFamilyCompensationFundContributionValue

				insert into [Payroll].[LiquidationDetail]
			   ([PayrollId], [RegisterStatus], [PayrollDate], [LiquidationPeriod], [ConceptClass], [ConceptId], [ConceptCode], [ConceptDetail], [ConceptTotalValue], [AccruedValue], [DeductedValue], 
			   [RetentionBase], [RetentionPercentage], [InitialBalance], [ConceptType], [AgreementsId], [AgreementsDId], [ConceptFormulate], [ReplaceConceptFormulate], [DistribuirGasto], [InabilityCollect],
			   [SpendingInability], [TotalNumberHours], [IdThirdParty], [TypeArticleRTF])
			   values
			   (@LiquidationId, 1, @PayrollDate, '1', @ConceptClass, @ConceptIdFamilyCompensationFundContributionValue, @ConceptCode, @ConceptDetail, @FamilyCompensationFundContributionValue, 
			   case when @ConceptType = 1 or @ConceptType = 3 then @FamilyCompensationFundContributionValue else 0 end, case when @ConceptType = 2 then @FamilyCompensationFundContributionValue else 0 end, 0, 0, 
			   0, @ConceptType, null, null, null, null, 0, 0, 0, 0, null, 0)
			End

			--Si el valor retencion es mayor a cero
			if @CalculatedWithholdingValue > 0
			Begin
				--Se obtienen los datos del concepto
				select @ConceptClass = ConceptClass, @ConceptCode = Code,  @ConceptDetail = [Name], @ConceptType = ConceptType from Payroll.Concept where Id = @ConceptIdCalculatedWithholdingValue

				insert into [Payroll].[LiquidationDetail]
			   ([PayrollId], [RegisterStatus], [PayrollDate], [LiquidationPeriod], [ConceptClass], [ConceptId], [ConceptCode], [ConceptDetail], [ConceptTotalValue], [AccruedValue], [DeductedValue], 
			   [RetentionBase], [RetentionPercentage], [InitialBalance], [ConceptType], [AgreementsId], [AgreementsDId], [ConceptFormulate], [ReplaceConceptFormulate], [DistribuirGasto], [InabilityCollect],
			   [SpendingInability], [TotalNumberHours], [IdThirdParty], [TypeArticleRTF])
			   values
			   (@LiquidationId, 1, @PayrollDate, '1', @ConceptClass, @ConceptIdCalculatedWithholdingValue, @ConceptCode, @ConceptDetail, @CalculatedWithholdingValue, 
			   case when @ConceptType = 1 or @ConceptType = 3 then @CalculatedWithholdingValue else 0 end, case when @ConceptType = 2 then @CalculatedWithholdingValue else 0 end, 0, 0, 
			   0, @ConceptType, null, null, null, null, 0, 0, 0, 0, null, 0)
			End

			--Si los recargos es mayor a cero
			if @RecargoNocturno > 0
			Begin
				--Se obtienen los datos del concepto
				select @ConceptClass = ConceptClass, @ConceptCode = Code,  @ConceptDetail = [Name], @ConceptType = ConceptType from Payroll.Concept where Id = @ConceptIdRecargoNocturno

				insert into [Payroll].[LiquidationDetail]
			   ([PayrollId], [RegisterStatus], [PayrollDate], [LiquidationPeriod], [ConceptClass], [ConceptId], [ConceptCode], [ConceptDetail], [ConceptTotalValue], [AccruedValue], [DeductedValue], 
			   [RetentionBase], [RetentionPercentage], [InitialBalance], [ConceptType], [AgreementsId], [AgreementsDId], [ConceptFormulate], [ReplaceConceptFormulate], [DistribuirGasto], [InabilityCollect],
			   [SpendingInability], [TotalNumberHours], [IdThirdParty], [TypeArticleRTF])
			   values
			   (@LiquidationId, 1, @PayrollDate, '1', @ConceptClass, @ConceptIdRecargoNocturno, @ConceptCode, @ConceptDetail, @RecargoNocturno, 
			   case when @ConceptType = 1 or @ConceptType = 3 then @RecargoNocturno else 0 end, case when @ConceptType = 2 then @RecargoNocturno else 0 end, 0, 0, 
			   0, @ConceptType, null, null, null, null, 0, 0, 0, 0, null, 0)
			End

			--Si las horas extras son mayor a cero
			if @Overtime > 0
			Begin
				--Se obtienen los datos del concepto
				select @ConceptClass = ConceptClass, @ConceptCode = Code,  @ConceptDetail = [Name], @ConceptType = ConceptType from Payroll.Concept where Id = @ConceptIdOvertime

				insert into [Payroll].[LiquidationDetail]
			   ([PayrollId], [RegisterStatus], [PayrollDate], [LiquidationPeriod], [ConceptClass], [ConceptId], [ConceptCode], [ConceptDetail], [ConceptTotalValue], [AccruedValue], [DeductedValue], 
			   [RetentionBase], [RetentionPercentage], [InitialBalance], [ConceptType], [AgreementsId], [AgreementsDId], [ConceptFormulate], [ReplaceConceptFormulate], [DistribuirGasto], [InabilityCollect],
			   [SpendingInability], [TotalNumberHours], [IdThirdParty], [TypeArticleRTF])
			   values
			   (@LiquidationId, 1, @PayrollDate, '1', @ConceptClass, @ConceptIdOvertime, @ConceptCode, @ConceptDetail, @Overtime, 
			   case when @ConceptType = 1 or @ConceptType = 3 then @Overtime else 0 end, case when @ConceptType = 2 then @Overtime else 0 end, 0, 0, 
			   0, @ConceptType, null, null, null, null, 0, 0, 0, 0, null, 0)
			End

			--Si los Recargos Nocturnos Festivos son mayor a cero
			if @RecargoNocturnoFestivo > 0
			Begin
				--Se obtienen los datos del concepto
				select @ConceptClass = ConceptClass, @ConceptCode = Code,  @ConceptDetail = [Name], @ConceptType = ConceptType from Payroll.Concept where Id = @ConceptIdRecargoNocturnoFestivo

				insert into [Payroll].[LiquidationDetail]
			   ([PayrollId], [RegisterStatus], [PayrollDate], [LiquidationPeriod], [ConceptClass], [ConceptId], [ConceptCode], [ConceptDetail], [ConceptTotalValue], [AccruedValue], [DeductedValue], 
			   [RetentionBase], [RetentionPercentage], [InitialBalance], [ConceptType], [AgreementsId], [AgreementsDId], [ConceptFormulate], [ReplaceConceptFormulate], [DistribuirGasto], [InabilityCollect],
			   [SpendingInability], [TotalNumberHours], [IdThirdParty], [TypeArticleRTF])
			   values
			   (@LiquidationId, 1, @PayrollDate, '1', @ConceptClass, @ConceptIdRecargoNocturnoFestivo, @ConceptCode, @ConceptDetail, @RecargoNocturnoFestivo, 
			   case when @ConceptType = 1 or @ConceptType = 3 then @RecargoNocturnoFestivo else 0 end, case when @ConceptType = 2 then @RecargoNocturnoFestivo else 0 end, 0, 0, 
			   0, @ConceptType, null, null, null, null, 0, 0, 0, 0, null, 0)
			End

			--Si los Valores Dominicales son mayor a cero
			if @ValorDominicalOrdinario > 0
			Begin
				--Se obtienen los datos del concepto
				select @ConceptClass = ConceptClass, @ConceptCode = Code,  @ConceptDetail = [Name], @ConceptType = ConceptType from Payroll.Concept where Id = @ConceptIdValorDominical

				insert into [Payroll].[LiquidationDetail]
			   ([PayrollId], [RegisterStatus], [PayrollDate], [LiquidationPeriod], [ConceptClass], [ConceptId], [ConceptCode], [ConceptDetail], [ConceptTotalValue], [AccruedValue], [DeductedValue], 
			   [RetentionBase], [RetentionPercentage], [InitialBalance], [ConceptType], [AgreementsId], [AgreementsDId], [ConceptFormulate], [ReplaceConceptFormulate], [DistribuirGasto], [InabilityCollect],
			   [SpendingInability], [TotalNumberHours], [IdThirdParty], [TypeArticleRTF])
			   values
			   (@LiquidationId, 1, @PayrollDate, '1', @ConceptClass, @ConceptIdValorDominical, @ConceptCode, @ConceptDetail, @ValorDominicalOrdinario, 
			   case when @ConceptType = 1 or @ConceptType = 3 then @ValorDominicalOrdinario else 0 end, case when @ConceptType = 2 then @ValorDominicalOrdinario else 0 end, 0, 0, 
			   0, @ConceptType, null, null, null, null, 0, 0, 0, 0, null, 0)
			End

			--Se agrega a la tabla que retorno el ok
			insert into @TableReturn(CodeResult, MessageResult) values(0, 'El empleado con cédula ' + @Nit + ' se guardó correctamente')

			NextFetch:
			--Se pasa a la siguiente posicion del cursor
			Fetch Next From InfoItem Into @Id, @Nit, @EmployeeId, @PayrollDate, @DaysWorked, @TotalAccrued, @TotalDeducted, @PensionJCB, @PensionContributionValue, @ConceptIdPensionContributionValue, 
			@EmployerPensionContributionValue, @ConceptIdEmployerPensionContributionValue, @HealthJCB, @EmployeeHealthContributionValue, @ConceptIdEmployeeHealthContributionValue, @EmployerHealthContributionValue, 
			@ConceptIdEmployerHealthContributionValue, @IBCIncentivePayment, @ProvisionIncentive, @ConceptIdProvisionIncentive, @IBCVacation, @ProvisionVacation, @ConceptIdProvisionVacation, @IBCUnemployment, 
			@UnemploymentAccumulated, @ConceptIdUnemploymentAccumulated, @ProvisionInterestsUnemployment, @ConceptIdProvisionInterestsUnemployment, @AmbulatoryDisabilityValue, @ConceptIdAmbulatoryDisabilityValue, 
			@DisabilityHospitalValue, @ConceptIdDisabilityHospitalValue, @MaternityLeaveValue, @ConceptIdMaternityLeaveValue, @IBCSENA, @SenaContributionValue, @ConceptIdSenaContributionValue, @IBCICBF,
			@ICBFContributionValue, @ConceptIdICBFContributionValue, @IBCCompensationFund, @FamilyCompensationFundContributionValue, @ConceptIdFamilyCompensationFundContributionValue, @RealBaseRetention,
			@CalculatedWithholdingValue, @ConceptIdCalculatedWithholdingValue, @RecargoNocturno, @ConceptIdRecargoNocturno, @SanctionDays, @Overtime, @ConceptIdOvertime, 
			@RecargoNocturnoFestivo, @ConceptIdRecargoNocturnoFestivo, @ValorDominicalOrdinario, @ConceptIdValorDominical

			continue

		End

		Close InfoItem
		Deallocate InfoItem

		select * from @TableReturn
		
	end try
	begin catch
		delete from @TableReturn
		insert into @TableReturn(CodeResult, MessageResult) values(888, CAST(ERROR_MESSAGE() as varchar(50)))
		select * from @TableReturn
	end catch

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que registra los saldos iniciales de nómina para un conjunto de empleados, recibiendo la información en formato XML con todos los conceptos de devengados y deducciones de cada persona. Parsea el XML y, por cada empleado, crea o actualiza los registros de liquidación de nómina (tablas Liquidation y LiquidationDetail del esquema Payroll), almacenando conceptos como aportes a pensión y salud del empleado y el empleador, provisiones de prima, vacaciones y cesantías, intereses de cesantías, parafiscales (SENA, ICBF, Caja de Compensación), retención en la fuente, recargos nocturnos, horas extras, incapacidades y licencias de maternidad. Se usa para la migración o carga inicial de datos históricos de nómina, garantizando que cada concepto quede vinculado a su identificador de concepto de nómina correspondiente, y devuelve un resultado por cada registro procesado indicando éxito o error.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_SaveInitialBalancePayroll';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_SaveInitialBalancePayroll';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Carga masiva de saldos iniciales de nómina: a partir de un XML inserta, para cada empleado, una cabecera en Payroll.Liquidation y los detalles correspondientes en Payroll.LiquidationDetail por cada concepto con valor > 0, devolviendo aciertos/errores por fila.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_SaveInitialBalancePayroll';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'@XmlObject debe contener nodos /Data/Row con los campos esperados de la liquidación (Nit, EmployeeId, PayrollDate, DaysWorked, TotalAccrued, etc.); El empleado debe existir en Payroll.Employee y tener un Contract activo (Valid=1 y Status=1) para poder obtener GroupId, CostCenterId, ContractId, WorkCenterId, BasicSalary y SalaryType; Deben existir registros en Payroll.FundContract con State=1 para FundType=1 (salud) y FundType=2 (pensión) asociados al contrato; Los Id de concepto recibidos en el XML deben existir en Payroll.Concept para poder leer ConceptClass, Code, Name y ConceptType; El empleado no debe tener ya una liquidación registrada en Payroll.Liquidation para el mismo mes y año de @PayrollDate; DaysWorked y TotalAccrued deben ser > 0 y TotalDeducted >= 0', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_SaveInitialBalancePayroll';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'TotalPaid en la cabecera siempre se calcula como TotalAccrued - TotalDeducted (cast a numeric(18,0)); PayrollDays se fija siempre en 30 y RegisterStatus en ''S'' al insertar la cabecera; PayrollConfirmationUser y PayrollProcessUser se graban siempre con el literal 999 (no con @CodeUser); Solo se generan filas en LiquidationDetail para conceptos cuyo valor es estrictamente mayor a cero; Cada empleado puede tener máximo una liquidación por mes/año (validación previa antes del INSERT); El contrato tomado debe estar vigente: Valid=1 y Status=1 en Payroll.Contract; FundContract se filtra siempre con State=1; FundType=1 corresponde a salud y FundType=2 a pensión; ProcedureTypeRTF se inserta siempre con valor 1 y CompletePayroll con 1; DaysWorked se utiliza tanto como días trabajados como ProvisionDays y PensionContributionDays en la cabecera; El XML siempre se itera por filas /Data/Row y los campos numéricos vacíos se normalizan a 0', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_SaveInitialBalancePayroll';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Liquidación de nómina; Saldo inicial de nómina; Empleado; Contrato laboral; Fondo de salud; Fondo de pensión; IBC (Ingreso Base de Cotización); Aportes a seguridad social (salud, pensión); Aportes parafiscales (SENA, ICBF, Caja de Compensación); Provisiones (primas, vacaciones, cesantías, intereses de cesantías); Incapacidades (ambulatoria, hospitalaria); Licencia de maternidad; Retención en la fuente; Horas extras y recargos nocturnos/festivos/dominicales; Concepto de nómina (devengado/deducido); Días trabajados/Días de sanción', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_SaveInitialBalancePayroll';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @DaysWorked <= 0 → Registra error 999 ''días trabajados tiene que ser mayor a cero'' y salta al siguiente registro sin insertar; si @TotalAccrued <= 0 → Registra error 999 ''total devengado tiene que ser mayor a cero'' y salta al siguiente registro; si @TotalDeducted < 0 → Registra error 999 ''total deducido tiene que ser mayor a cero'' y salta al siguiente registro; si Existe en Payroll.Liquidation un registro con mismo EmployeeId, mismo MES y mismo AÑO de PayrollDateLiquidated → Registra error 999 indicando que ya existe registro con ese mes/año y omite la inserción else Procede a obtener datos del contrato/empleado/fondos e insertar cabecera y detalles; si Cada valor de concepto (PensionContributionValue, EmployerPensionContributionValue, EmployeeHealthContributionValue, EmployerHealthContributionValue, ProvisionIncentive, ProvisionVacation, UnemploymentAccumulated, ProvisionInterestsUnemployment, AmbulatoryDisabilityValue, DisabilityHospitalValue, MaternityLeaveValue, SenaContributionValue, ICBFContributionValue, FamilyCompensationFundContributionValue, CalculatedWithholdingValue, RecargoNocturno, Overtime, RecargoNocturnoFestivo, ValorDominicalOrdinario) > 0 → Inserta una fila en Payroll.LiquidationDetail tomando ConceptClass/Code/Name/ConceptType desde Payroll.Concept else No genera detalle para ese concepto; si ConceptType = 1 o 3 → El valor del concepto se asigna a AccruedValue (devengado) y DeductedValue=0 else Si ConceptType = 2, se asigna a DeductedValue (deducido) y AccruedValue=0; si Ocurre cualquier excepción durante el procesamiento → En CATCH se vacía @TableReturn e inserta un registro con CodeResult=888 y los primeros 50 caracteres de ERROR_MESSAGE()', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_SaveInitialBalancePayroll';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_SaveInitialBalancePayroll';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.Liquidation; Payroll.Employee; Payroll.Contract; Payroll.ContractType; Payroll.FundContract; Payroll.Concept', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_SaveInitialBalancePayroll';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_SaveInitialBalancePayroll';
-- GO
