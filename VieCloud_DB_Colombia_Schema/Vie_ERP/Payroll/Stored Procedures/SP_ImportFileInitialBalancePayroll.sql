-- =============================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 05/05/2017
-- Description:	Procedimiento que se encarga de importar el archivo de excel y validarlo
-- =============================================
CREATE PROCEDURE [Payroll].[SP_ImportFileInitialBalancePayroll]
	@XmlObject as Xml
AS
BEGIN
	
	--Tabla para almacenar los items del listado que viene en el xml
	declare @TableXmlObject table(Id int IDENTITY PRIMARY KEY, CountFields int, StatusField int, MessageField varchar(max), Nit varchar(50), NitName varchar(200), EmployeeId int, 
	PayrollDate varchar(20), DaysWorked varchar(50), TotalAccrued varchar(50), TotalDeducted varchar(50), PensionJCB varchar(50), PensionContributionValue varchar(50), 
	ConceptCodePensionContributionValue varchar(20), ConceptIdPensionContributionValue int,	EmployerPensionContributionValue varchar(50), ConceptCodeEmployerPensionContributionValue varchar(20), 
	ConceptIdEmployerPensionContributionValue int, HealthJCB varchar(50), EmployeeHealthContributionValue varchar(50), ConceptCodeEmployeeHealthContributionValue varchar(20), ConceptIdEmployeeHealthContributionValue int,
	EmployerHealthContributionValue varchar(50), ConceptCodeEmployerHealthContributionValue varchar(20), ConceptIdEmployerHealthContributionValue int, IBCIncentivePayment varchar(50),
	ProvisionIncentive varchar(50), ConceptCodeProvisionIncentive varchar(20), ConceptIdProvisionIncentive int, IBCVacation varchar(50), ProvisionVacation varchar(50), ConceptCodeProvisionVacation varchar(20), 
	ConceptIdProvisionVacation int,	IBCUnemployment varchar(50), UnemploymentAccumulated varchar(50), ConceptCodeUnemploymentAccumulated varchar(20), ConceptIdUnemploymentAccumulated int,
	ProvisionInterestsUnemployment varchar(50), ConceptCodeProvisionInterestsUnemployment varchar(20), ConceptIdProvisionInterestsUnemployment int,	AmbulatoryDisabilityValue varchar(50), 
	ConceptCodeAmbulatoryDisabilityValue varchar(20), ConceptIdAmbulatoryDisabilityValue int, DisabilityHospitalValue varchar(50), ConceptCodeDisabilityHospitalValue varchar(20), ConceptIdDisabilityHospitalValue int,
	MaternityLeaveValue varchar(50), ConceptCodeMaternityLeaveValue varchar(20), ConceptIdMaternityLeaveValue int, IBCSENA varchar(50), SenaContributionValue varchar(50), 
	ConceptCodeSenaContributionValue varchar(20), ConceptIdSenaContributionValue int, IBCICBF varchar(50), ICBFContributionValue varchar(50), ConceptCodeICBFContributionValue varchar(20), 
	ConceptIdICBFContributionValue int, IBCCompensationFund varchar(50), FamilyCompensationFundContributionValue varchar(50), ConceptCodeFamilyCompensationFundContributionValue varchar(20), 
	ConceptIdFamilyCompensationFundContributionValue int, RealBaseRetention varchar(50), CalculatedWithholdingValue varchar(50), ConceptCodeCalculatedWithholdingValue varchar(20), ConceptIdCalculatedWithholdingValue int,
	RecargoNocturno varchar(50), ConceptCodeRecargoNocturno varchar(20), ConceptIdRecargoNocturno int, TotalPaid decimal(18,0), SanctionDays varchar(50), Overtime varchar(50), ConceptCodeOvertime varchar(20),
	ConceptIdOvertime int, RecargoNocturnoFestivo varchar(50), ConceptCodeRecargoNocturnoFestivo varchar(20), ConceptIdRecargoNocturnoFestivo int, ValorDominicalOrdinario varchar(50), ConceptCodeValorDominical varchar(20),
	ConceptIdValorDominical int)

	--Id del empleado
	declare @EmployeeId as int

	--begin transaction
	Begin try
	
		insert into @TableXmlObject
		select 
		t.x.value('CountFields[1]','int') as CountFields,
		t.x.value('StatusField[1]','int') as StatusField,
		t.x.value('MessageField[1]','varchar(100)') as MessageField,
		t.x.value('Nit[1]','varchar(50)') as Nit,
		t.x.value('NitName[1]','varchar(200)') as NitName,
		t.x.value('EmployeeId[1]','int') as EmployeeId,
		t.x.value('PayrollDate[1]','varchar(20)') as PayrollDate,
		t.x.value('DaysWorked[1]','varchar(50)') as DaysWorked,
		t.x.value('TotalAccrued[1]','varchar(50)') as TotalAccrued,
		t.x.value('TotalDeducted[1]','varchar(50)') as TotalDeducted,
		t.x.value('PensionJCB[1]','varchar(50)') as PensionJCB,
		t.x.value('PensionContributionValue[1]','varchar(50)') as PensionContributionValue,
		t.x.value('ConceptCodePensionContributionValue[1]','varchar(20)') as ConceptCodePensionContributionValue,
		t.x.value('ConceptIdPensionContributionValue[1]','int') as ConceptIdPensionContributionValue,
		t.x.value('EmployerPensionContributionValue[1]','varchar(50)') as EmployerPensionContributionValue,
		t.x.value('ConceptCodeEmployerPensionContributionValue[1]','varchar(20)') as ConceptCodeEmployerPensionContributionValue,
		t.x.value('ConceptIdEmployerPensionContributionValue[1]','int') as ConceptIdEmployerPensionContributionValue,
		t.x.value('HealthJCB[1]','varchar(50)') as HealthJCB,
		t.x.value('EmployeeHealthContributionValue[1]','varchar(50)') as EmployeeHealthContributionValue,
		t.x.value('ConceptCodeEmployeeHealthContributionValue[1]','varchar(20)') as ConceptCodeEmployeeHealthContributionValue,
		t.x.value('ConceptIdEmployeeHealthContributionValue[1]','int') as ConceptIdEmployeeHealthContributionValue,
		t.x.value('EmployerHealthContributionValue[1]','varchar(50)') as EmployerHealthContributionValue,
		t.x.value('ConceptCodeEmployerHealthContributionValue[1]','varchar(20)') as ConceptCodeEmployerHealthContributionValue,
		t.x.value('ConceptIdEmployerHealthContributionValue[1]','int') as ConceptIdEmployerHealthContributionValue,
		t.x.value('IBCIncentivePayment[1]','varchar(50)') as IBCIncentivePayment,
		t.x.value('ProvisionIncentive[1]','varchar(50)') as ProvisionIncentive,
		t.x.value('ConceptCodeProvisionIncentive[1]','varchar(20)') as ConceptCodeProvisionIncentive,
		t.x.value('ConceptIdProvisionIncentive[1]','int') as ConceptIdProvisionIncentive,
		t.x.value('IBCVacation[1]','varchar(50)') as IBCVacation,
		t.x.value('ProvisionVacation[1]','varchar(50)') as ProvisionVacation,
		t.x.value('ConceptCodeProvisionVacation[1]','varchar(20)') as ConceptCodeProvisionVacation,
		t.x.value('ConceptIdProvisionVacation[1]','int') as ConceptIdProvisionVacation,
		t.x.value('IBCUnemployment[1]','varchar(50)') as IBCUnemployment,
		t.x.value('UnemploymentAccumulated[1]','varchar(50)') as UnemploymentAccumulated,
		t.x.value('ConceptCodeUnemploymentAccumulated[1]','varchar(20)') as ConceptCodeUnemploymentAccumulated,
		t.x.value('ConceptIdUnemploymentAccumulated[1]','int') as ConceptIdUnemploymentAccumulated,
		t.x.value('ProvisionInterestsUnemployment[1]','varchar(50)') as ProvisionInterestsUnemployment,
		t.x.value('ConceptCodeProvisionInterestsUnemployment[1]','varchar(20)') as ConceptCodeProvisionInterestsUnemployment,
		t.x.value('ConceptIdProvisionInterestsUnemployment[1]','int') as ConceptIdProvisionInterestsUnemployment,
		t.x.value('AmbulatoryDisabilityValue[1]','varchar(50)') as AmbulatoryDisabilityValue,
		t.x.value('ConceptCodeAmbulatoryDisabilityValue[1]','varchar(20)') as ConceptCodeAmbulatoryDisabilityValue,
		t.x.value('ConceptIdAmbulatoryDisabilityValue[1]','int') as ConceptIdAmbulatoryDisabilityValue,
		t.x.value('DisabilityHospitalValue[1]','varchar(50)') as DisabilityHospitalValue,
		t.x.value('ConceptCodeDisabilityHospitalValue[1]','varchar(20)') as ConceptCodeDisabilityHospitalValue,
		t.x.value('ConceptIdDisabilityHospitalValue[1]','int') as ConceptIdDisabilityHospitalValue,
		t.x.value('MaternityLeaveValue[1]','varchar(50)') as MaternityLeaveValue,
		t.x.value('ConceptCodeMaternityLeaveValue[1]','varchar(20)') as ConceptCodeMaternityLeaveValue,
		t.x.value('ConceptIdMaternityLeaveValue[1]','int') as ConceptIdMaternityLeaveValue,
		t.x.value('IBCSENA[1]','varchar(50)') as IBCSENA,
		t.x.value('SenaContributionValue[1]','varchar(50)') as SenaContributionValue,
		t.x.value('ConceptCodeSenaContributionValue[1]','varchar(20)') as ConceptCodeSenaContributionValue,
		t.x.value('ConceptIdSenaContributionValue[1]','int') as ConceptIdSenaContributionValue,
		t.x.value('IBCICBF[1]','varchar(50)') as IBCICBF,
		t.x.value('ICBFContributionValue[1]','varchar(50)') as ICBFContributionValue,
		t.x.value('ConceptCodeICBFContributionValue[1]','varchar(20)') as ConceptCodeICBFContributionValue,
		t.x.value('ConceptIdICBFContributionValue[1]','int') as ConceptIdICBFContributionValue,
		t.x.value('IBCCompensationFund[1]','varchar(50)') as IBCCompensationFund,
		t.x.value('FamilyCompensationFundContributionValue[1]','varchar(50)') as FamilyCompensationFundContributionValue,
		t.x.value('ConceptCodeFamilyCompensationFundContributionValue[1]','varchar(20)') as ConceptCodeFamilyCompensationFundContributionValue,
		t.x.value('ConceptIdFamilyCompensationFundContributionValue[1]','int') as ConceptIdFamilyCompensationFundContributionValue,
		t.x.value('RealBaseRetention[1]','varchar(50)') as RealBaseRetention,
		t.x.value('CalculatedWithholdingValue[1]','varchar(50)') as CalculatedWithholdingValue,
		t.x.value('ConceptCodeCalculatedWithholdingValue[1]','varchar(20)') as ConceptCodeCalculatedWithholdingValue,
		t.x.value('ConceptIdCalculatedWithholdingValue[1]','int') as ConceptIdCalculatedWithholdingValue,
		t.x.value('RecargoNocturno[1]','varchar(50)') as RecargoNocturno,
		t.x.value('ConceptCodeRecargoNocturno[1]','varchar(20)') as ConceptCodeRecargoNocturno,
		t.x.value('ConceptIdRecargoNocturno[1]','int') as ConceptIdRecargoNocturno,
		t.x.value('TotalPaid[1]','decimal(18,0)') as TotalPaid,
		t.x.value('SanctionDays[1]','varchar(50)') as SanctionDays,
		t.x.value('Overtime[1]','varchar(50)') as Overtime,
		t.x.value('ConceptCodeOvertime[1]','varchar(20)') as ConceptCodeOvertime,
		t.x.value('ConceptIdOvertime[1]','int') as ConceptIdOvertime,
		-- NUEVOS CONCEPTOS
		t.x.value('RecargoNocturnoFestivo[1]','varchar(50)') as RecargoNocturnoFestivo,
		t.x.value('ConceptCodeRecargoNocturnoFestivo[1]','varchar(20)') as ConceptCodeRecargoNocturnoFestivo,
		t.x.value('ConceptIdRecargoNocturnoFestivo[1]','int') as ConceptIdRecargoNocturnoFestivo,

		t.x.value('ValorDominicalOrdinario[1]','varchar(50)') as ValorDominicalOrdinario,
		t.x.value('ConceptCodeValorDominical[1]','varchar(20)') as ConceptCodeValorDominical,
		t.x.value('ConceptIdValorDominical[1]','int') as ConceptIdValorDominical

		from @XmlObject.nodes('/Data/Row') t(x)

		--Se declara el contador de posiciones para enviar en los mensajes de error
		declare @Position as int = 0

		--Id del registro
		declare @Id as int

		--Cantidad de columnas de cada registro
		declare @CountFields as int
		
		--Nit del tercero
		declare @Nit as varchar(50)

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

		--Codigo de concepto para aporte pension empleado
		declare @ConceptCodePensionContributionValue as varchar(20)

		--Id del concepto para aporte pension empleado
		declare @ConceptIdPensionContributionValue as int

		--Aporte pension patrono
		declare @EmployerPensionContributionValue as varchar(50)

		--Codigo de concepto para aporte pension patrono
		declare @ConceptCodeEmployerPensionContributionValue as varchar(20)

		--Id del concepto para aporte pension patrono
		declare @ConceptIdEmployerPensionContributionValue as int

		--Base salud
		declare @HealthJCB as varchar(50)

		--Aporte salud empleado
		declare @EmployeeHealthContributionValue as varchar(50)

		--Codigo de concepto para aporte salud empleado
		declare @ConceptCodeEmployeeHealthContributionValue as varchar(20)

		--Id del concepto para aporte salud empleado
		declare @ConceptIdEmployeeHealthContributionValue as int

		--Aporte salud patrono
		declare @EmployerHealthContributionValue as varchar(50)

		--Codigo de concepto para aporte salud patrono
		declare @ConceptCodeEmployerHealthContributionValue as varchar(20)

		--Id del concepto para aporte salud patrono
		declare @ConceptIdEmployerHealthContributionValue as int

		--Base primas
		declare @IBCIncentivePayment as varchar(50)

		--Provision primas
		declare @ProvisionIncentive as varchar(50)

		--Codigo de concepto para provision primas
		declare @ConceptCodeProvisionIncentive as varchar(20)

		--Id del concepto para provision primas
		declare @ConceptIdProvisionIncentive as int

		--Base vacaciones
		declare @IBCVacation as varchar(50)

		--Provision vacaciones
		declare @ProvisionVacation as varchar(50)

		--Codigo de concepto para provision vacaciones
		declare @ConceptCodeProvisionVacation as varchar(20)

		--Id del concepto para provision vacaciones
		declare @ConceptIdProvisionVacation as int

		--Base cesantias
		declare @IBCUnemployment as varchar(50)

		--Provision cesantias
		declare @UnemploymentAccumulated as varchar(50)

		--Codigo de concepto para provision cesantias
		declare @ConceptCodeUnemploymentAccumulated as varchar(20)

		--Id del concepto para provision cesantias
		declare @ConceptIdUnemploymentAccumulated as int

		--Provision intereses cesantias
		declare @ProvisionInterestsUnemployment as varchar(50)

		--Codigo de concepto para provision intereses cesantias
		declare @ConceptCodeProvisionInterestsUnemployment as varchar(20)

		--Id del concepto para provision intereses cesantias
		declare @ConceptIdProvisionInterestsUnemployment as int

		--Valor incapacidad ambulatoria
		declare @AmbulatoryDisabilityValue as varchar(50)

		--Codigo de concepto para valor incapacidad ambulatoria
		declare @ConceptCodeAmbulatoryDisabilityValue as varchar(20)

		--Id del concepto para valor incapacidad ambulatoria
		declare @ConceptIdAmbulatoryDisabilityValue as int

		--Valor incapacidad hospitalaria
		declare @DisabilityHospitalValue as varchar(50)

		--Codigo de concepto para valor incapacidad hospitalaria
		declare @ConceptCodeDisabilityHospitalValue as varchar(20)

		--Id del concepto para valor incapacidad hospitalaria
		declare @ConceptIdDisabilityHospitalValue as int

		--Valor licencia maternidad
		declare @MaternityLeaveValue as varchar(50)

		--Codigo de concepto para valor licencia maternidad
		declare @ConceptCodeMaternityLeaveValue as varchar(20)

		--Id del concepto para valor licencia maternidad
		declare @ConceptIdMaternityLeaveValue as int

		--Base sena
		declare @IBCSENA as varchar(50)

		--Aporte sena
		declare @SenaContributionValue as varchar(50)

		--Codigo de concepto para aporte sena
		declare @ConceptCodeSenaContributionValue as varchar(20)

		--Id del concepto para aporte sena
		declare @ConceptIdSenaContributionValue as int

		--Base icbf
		declare @IBCICBF as varchar(50)

		--Aporte icbf
		declare @ICBFContributionValue as varchar(50)

		--Codigo de concepto para aporte icbf
		declare @ConceptCodeICBFContributionValue as varchar(20)

		--Id del concepto para aporte icbf
		declare @ConceptIdICBFContributionValue as int

		--Base caja compensacion
		declare @IBCCompensationFund as varchar(50)

		--Aporte caja compensacion
		declare @FamilyCompensationFundContributionValue as varchar(50)

		--Codigo de concepto para aporte caja compensacion
		declare @ConceptCodeFamilyCompensationFundContributionValue as varchar(20)

		--Id del concepto para aporte caja compensacion
		declare @ConceptIdFamilyCompensationFundContributionValue as int

		--Base retención
		declare @RealBaseRetention as varchar(50)

		--Valor retencion
		declare @CalculatedWithholdingValue as varchar(50)

		--Codigo de concepto para valor retencion
		declare @ConceptCodeCalculatedWithholdingValue as varchar(20)

		--Id del concepto para valor retencion
		declare @ConceptIdCalculatedWithholdingValue as int

		--Recargos
		declare @RecargoNocturno as varchar(50)

		--Codigo de concepto para recragos
		declare @ConceptCodeRecargoNocturno as varchar(20)

		--Id del concepto para recargo
		declare @ConceptIdRecargoNocturno as int

		--Días de sanción
		declare @SanctionDays as varchar(50)

		--Horas extras
		declare @Overtime as varchar(50)

		--Codigo de concepto para horas extras
		declare @ConceptCodeOvertime as varchar(20)

		--Id del concepto para horas extras
		declare @ConceptIdOvertime as int

		----------------- NUEVOS CONCEPTOS AGREGADOS ------------------------

		--Valor Recargo Nocturno Festivo
		declare @RecargoNocturnoFestivo as varchar(50)

		--Codigo de concepto para Recargo Nocturno Festivo
		declare @ConceptCodeRecargoNocturnoFestivo as varchar(20)

		--Id del concepto para Recargo Nocturno Festivo
		declare @ConceptIdRecargoNocturnoFestivo as int

		--Valor Recargo Nocturno Festivo
		declare @ValorDominicalOrdinario as varchar(50)

		--Codigo de concepto para Recargo Nocturno Festivo
		declare @ConceptCodeValorDominical as varchar(20)

		--Id del concepto para Recargo Nocturno Festivo
		declare @ConceptIdValorDominical as int

		--Se recorre el cursor
		declare InfoItem Cursor For Select [Id], [CountFields], [Nit], [PayrollDate], [DaysWorked], [TotalAccrued], [TotalDeducted], [PensionJCB], [PensionContributionValue], 
		[ConceptCodePensionContributionValue], [ConceptIdPensionContributionValue], [EmployerPensionContributionValue], [ConceptCodeEmployerPensionContributionValue], [ConceptIdEmployerPensionContributionValue],
		[HealthJCB], [EmployeeHealthContributionValue], [ConceptCodeEmployeeHealthContributionValue], [ConceptIdEmployeeHealthContributionValue], [EmployerHealthContributionValue], 
		[ConceptCodeEmployerHealthContributionValue], [ConceptIdEmployerHealthContributionValue], [IBCIncentivePayment], [ProvisionIncentive], [ConceptCodeProvisionIncentive], [ConceptIdProvisionIncentive],
		[IBCVacation], [ProvisionVacation], [ConceptCodeProvisionVacation], [ConceptIdProvisionVacation], [IBCUnemployment], [UnemploymentAccumulated], [ConceptCodeUnemploymentAccumulated], 
		[ConceptIdUnemploymentAccumulated], [ProvisionInterestsUnemployment], [ConceptCodeProvisionInterestsUnemployment], [ConceptIdProvisionInterestsUnemployment],
		[AmbulatoryDisabilityValue], [ConceptCodeAmbulatoryDisabilityValue], [ConceptIdAmbulatoryDisabilityValue],
		[DisabilityHospitalValue], [ConceptCodeDisabilityHospitalValue], [ConceptIdDisabilityHospitalValue],
		[MaternityLeaveValue], [ConceptCodeMaternityLeaveValue], [ConceptIdMaternityLeaveValue], [IBCSENA], [SenaContributionValue], [ConceptCodeSenaContributionValue], [ConceptIdSenaContributionValue],
		[IBCICBF], [ICBFContributionValue], [ConceptCodeICBFContributionValue], [ConceptIdICBFContributionValue], [IBCCompensationFund],
		[FamilyCompensationFundContributionValue], [ConceptCodeFamilyCompensationFundContributionValue], [ConceptIdFamilyCompensationFundContributionValue], [RealBaseRetention],
		[CalculatedWithholdingValue], [ConceptCodeCalculatedWithholdingValue], [ConceptIdCalculatedWithholdingValue],
		[RecargoNocturno], [ConceptCodeRecargoNocturno], [ConceptIdRecargoNocturno], [SanctionDays], [Overtime], [ConceptCodeOvertime], [ConceptIdOvertime], 
		[RecargoNocturnoFestivo], [ConceptCodeRecargoNocturnoFestivo], [ConceptIdRecargoNocturnoFestivo], [ValorDominicalOrdinario], [ConceptCodeValorDominical], [ConceptIdValorDominical] 
		From @TableXmlObject

		Open InfoItem

		Fetch Next From InfoItem Into @Id, @CountFields, @Nit, @PayrollDate, @DaysWorked, @TotalAccrued, @TotalDeducted, @PensionJCB, @PensionContributionValue, @ConceptCodePensionContributionValue, 
		@ConceptIdPensionContributionValue, @EmployerPensionContributionValue, @ConceptCodeEmployerPensionContributionValue, @ConceptIdEmployerPensionContributionValue, @HealthJCB,
		@EmployeeHealthContributionValue, @ConceptCodeEmployeeHealthContributionValue, @ConceptIdEmployeeHealthContributionValue, @EmployerHealthContributionValue, 
		@ConceptCodeEmployerHealthContributionValue, @ConceptIdEmployerHealthContributionValue, @IBCIncentivePayment, @ProvisionIncentive, @ConceptCodeProvisionIncentive, @ConceptIdProvisionIncentive,
		@IBCVacation, @ProvisionVacation, @ConceptCodeProvisionVacation, @ConceptIdProvisionVacation, @IBCUnemployment, @UnemploymentAccumulated, @ConceptCodeUnemploymentAccumulated, @ConceptIdUnemploymentAccumulated,
		@ProvisionInterestsUnemployment, @ConceptCodeProvisionInterestsUnemployment, @ConceptIdProvisionInterestsUnemployment, 
		@AmbulatoryDisabilityValue, @ConceptCodeAmbulatoryDisabilityValue, @ConceptIdAmbulatoryDisabilityValue, @DisabilityHospitalValue, @ConceptCodeDisabilityHospitalValue, @ConceptIdDisabilityHospitalValue,
		@MaternityLeaveValue, @ConceptCodeMaternityLeaveValue, @ConceptIdMaternityLeaveValue, @IBCSENA, @SenaContributionValue, @ConceptCodeSenaContributionValue, @ConceptIdSenaContributionValue, @IBCICBF,
		@ICBFContributionValue, @ConceptCodeICBFContributionValue, @ConceptIdICBFContributionValue, @IBCCompensationFund,
		@FamilyCompensationFundContributionValue, @ConceptCodeFamilyCompensationFundContributionValue, @ConceptIdFamilyCompensationFundContributionValue, @RealBaseRetention,
		@CalculatedWithholdingValue, @ConceptCodeCalculatedWithholdingValue, @ConceptIdCalculatedWithholdingValue, @RecargoNocturno, @ConceptCodeRecargoNocturno, @ConceptIdRecargoNocturno,
		@SanctionDays, @Overtime, @ConceptCodeOvertime, @ConceptIdOvertime, @RecargoNocturnoFestivo, @ConceptCodeRecargoNocturnoFestivo, @ConceptIdRecargoNocturnoFestivo,
		@ValorDominicalOrdinario, @ConceptCodeValorDominical, @ConceptIdValorDominical

		While @@fetch_status = 0
		Begin
		
			--Se incrementa la posicion
			set @Position = @Position + 1
			
			--Se valida que cada registro tenga la estructura requerida
			if @CountFields <> 53
			Begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'El registro ' + convert(varchar(3),@Position) + ' no tiene la estructura requerida'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				GoTo NextFetch
			End		
			--Se valida que el campo del nit del tercero no este vacio
			if LEN(@Nit) = 0
			Begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'La cédula del registro ' + convert(varchar(3),@Position) + ' está vacía'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				GoTo NextFetch
			End

			--Se valida que el nit del tercero exista
			if (select COUNT(*) from Common.ThirdParty where Nit = LTRIM(RTRIM(@Nit))) = 0
			Begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'La cédula del registro ' + convert(varchar(3),@Position) + ' no existe como tercero en la BD'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				GoTo NextFetch
			End
			
			--Se actualiza el nombre y nit del tercero
			update @TableXmlObject set NitName = (select RTRIM(LTRIM(Nit)) + ' - ' + RTRIM(LTRIM([Name])) from Common.ThirdParty where Nit = @Nit) where Id = @Id
			
			--Se valida si el tercero existe como empleado
			if (select COUNT(*) from Payroll.Employee where ThirdPartyId = (select Id from Common.ThirdParty where Nit = LTRIM(RTRIM(@Nit)))) = 0
			Begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'La cédula del registro ' + convert(varchar(3),@Position) + ' no existe como empleado en la BD'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				GoTo NextFetch
			End

			--Se asigna el id del empleado
			set @EmployeeId = (select Id from Payroll.Employee where ThirdPartyId = (select Id from Common.ThirdParty where Nit = RTRIM(LTRIM(@Nit))))

			--Se valida que la fecha de nómina no este vacio
			if LEN(@PayrollDate) = 0
			Begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'La fecha nómina del registro ' + convert(varchar(3),@Position) + ' está vacía'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				GoTo NextFetch
			End
			--Se valida que la fecha de nómina sea una fecha
			if ISDATE(@PayrollDate) = 0
			Begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'La fecha de nómina del registro ' + convert(varchar(3),@Position) + ' no tiene formato de fecha'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				GoTo NextFetch
			End

			--Se valida que el empleado no haya sido agregado con el mismo mes y año 
			if (select COUNT(*) from @TableXmlObject where EmployeeId > 0 and EmployeeId = @EmployeeId and MONTH(PayrollDate) = MONTH(@PayrollDate) and YEAR(PayrollDate) = YEAR(@PayrollDate)) > 0
			Begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'El empleado del registro ' + convert(varchar(3),@Position) + ' ya existe en la lista con el mismo mes y año'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				GoTo NextFetch
			End

			--Se actualiza el id del empleado
			update @TableXmlObject set EmployeeId = @EmployeeId where Id = @Id

			--Se valida que el empleado tenga contrato valido y activo
			if	(select COUNT(*)
				from Payroll.Employee e
				inner join Payroll.[Contract] c on c.EmployeeId = e.Id
				inner join Payroll.ContractType ct on ct.Id = c.ContractTypeId
				where e.Id = @EmployeeId and c.Valid = 1 and c.Status = 1) = 0
			Begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'El empleado del registro ' + convert(varchar(3),@Position) + ' no tiene contrato asociado o está retirado'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				GoTo NextFetch
			End
			
			--Valida si la liquidacion del grupo es mensual o quincenal
			if (SELECT top 1 g.Liquidation
				from payroll.[Group] g
					JOIN Payroll.Contract c WITH(NOLOCK) ON g.Id = c.GroupId
					join Payroll.Employee e WITH(NOLOCK) on c.EmployeeId=e.Id
					join Common.ThirdParty th WITH(NOLOCK) ON e.ThirdPartyId=th.Id
				where th.Nit = @Nit and c.Valid=1 and c.Status=1) = 1
			BEGIN 

			--Se valida que el día de la fecha sea el ultimo día del mes
				if DAY(eomonth(@PayrollDate)) <> DAY(@PayrollDate)
				Begin
					--Se actualiza los campos con el estado en false y el mensaje de error
					update @TableXmlObject set StatusField = 0, MessageField = 'El día de la fecha de nómina del registro ' + convert(varchar(3),@Position) + ' no es el último día del mes'
					where Id = @Id			
					--Se pasa a la siguiente posicion del cursor
					GoTo NextFetch
				End
			
			ELSE

				--Se valida que el día de la fecha sea el ultimo día 15 o el ultimo del mes
				if (DAY(eomonth(@PayrollDate)) <> DAY(@PayrollDate) AND (DAY(@PayrollDate)<>15))
				Begin
					--Se actualiza los campos con el estado en false y el mensaje de error
					update @TableXmlObject set StatusField = 0, MessageField = 'El día de la fecha de nómina del registro ' + convert(varchar(3),@Position) + ' no es el quince o no es el ultimo día del mes'
					where Id = @Id			
					--Se pasa a la siguiente posicion del cursor
					GoTo NextFetch
				End
			END
		

			--Se valida que los días trabajados no venga vacío
			if LEN(@DaysWorked) = 0
			Begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'Los días trabajados del registro ' + convert(varchar(3),@Position) + ' está vacío'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				GoTo NextFetch
			End

			--Se valida que los días trabajados sea numerico
			if ISNUMERIC(@DaysWorked) = 0
			Begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'Los días trabajados del registro ' + convert(varchar(3),@Position) + ' no es un valor numérico'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				GoTo NextFetch
			End

			--Se valida que los días de nómina no sea negativo
			if @DaysWorked < 0
			Begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'Los días trabajados del registro ' + convert(varchar(3),@Position) + ' es negativo'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				GoTo NextFetch
			End

			-- Se valida que los días trabajdos sea mayor a 0 y menor a 30
			if @DaysWorked = 0 or @DaysWorked > 30
			Begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'Los días trabajados del registro ' + convert(varchar(3),@Position) + ' debe ser mayor a 0 y menor o igual a 30'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				GoTo NextFetch
			End
			
			--Se valida que el total devengado no venga vacío
			if LEN(@TotalAccrued) = 0
			Begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'El total devengado del registro ' + convert(varchar(3),@Position) + ' está vacío'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				GoTo NextFetch
			End

			--Se valida que el total devengado sea numerico
			if ISNUMERIC(@TotalAccrued) = 0
			Begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'El total devengado del registro ' + convert(varchar(3),@Position) + ' no es un valor numérico'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				GoTo NextFetch
			End

			--Se valida que el total devengado no sea negativo
			if @TotalAccrued < 0
			Begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'El total devengado del registro ' + convert(varchar(3),@Position) + ' es negativo'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				GoTo NextFetch
			End

			--Si el total deducido no esta vacío
			if LEN(@TotalDeducted) > 0
			Begin
				--Se valida que el total deducido sea numerico
				if ISNUMERIC(@TotalDeducted) = 0
				Begin
					--Se actualiza los campos con el estado en false y el mensaje de error
					update @TableXmlObject set StatusField = 0, MessageField = 'El total deducido del registro ' + convert(varchar(3),@Position) + ' no es un valor numérico'
					where Id = @Id			
					--Se pasa a la siguiente posicion del cursor
					GoTo NextFetch
				End

				--Se valida que el total deducido no sea negativo
				if @TotalDeducted < 0
				Begin
					--Se actualiza los campos con el estado en false y el mensaje de error
					update @TableXmlObject set StatusField = 0, MessageField = 'El total deducido del registro ' + convert(varchar(3),@Position) + ' es negativo'
					where Id = @Id			
					--Se pasa a la siguiente posicion del cursor
					GoTo NextFetch
				End
			End

			--Se valida que si la base de pensión viene con valor sea numerico
			if LEN(@PensionJCB) > 0
			Begin
				--Se valida que la base pension sea numerico
				if ISNUMERIC(@PensionJCB) = 0
				Begin
					--Se actualiza los campos con el estado en false y el mensaje de error
					update @TableXmlObject set StatusField = 0, MessageField = 'La base pensión del registro ' + convert(varchar(3),@Position) + ' no es un valor numérico'
					where Id = @Id			
					--Se pasa a la siguiente posicion del cursor
					GoTo NextFetch
				End

				--Se valida que la base pension no sea negativo
				if @PensionJCB < 0
				Begin
					--Se actualiza los campos con el estado en false y el mensaje de error
					update @TableXmlObject set StatusField = 0, MessageField = 'La base pensión del registro ' + convert(varchar(3),@Position) + ' es negativo'
					where Id = @Id			
					--Se pasa a la siguiente posicion del cursor
					GoTo NextFetch
				End
			End

			--Si el aporte de pension empleados no está vacio se realiza las validaciones
			if LEN(@PensionContributionValue) > 0
			Begin
				--Se valida que el aporte pension empleado sea numerico
				if ISNUMERIC(@PensionContributionValue) = 0
				Begin
					--Se actualiza los campos con el estado en false y el mensaje de error
					update @TableXmlObject set StatusField = 0, MessageField = 'El aporte pension empleados del registro ' + convert(varchar(3),@Position) + ' no es un valor numérico'
					where Id = @Id			
					--Se pasa a la siguiente posicion del cursor
					GoTo NextFetch
				End

				--Se valida que el aporte pension empleados sea mayor a cero para realizar las validaciones del concepto
				if @PensionContributionValue > 0
				Begin
					--Se valida que el codigo del concepto de aporte pension empleado no venga vacio
					if LEN(@ConceptCodePensionContributionValue) = 0
					Begin
						--Se actualiza los campos con el estado en false y el mensaje de error
						update @TableXmlObject set StatusField = 0, MessageField = 'El código de concepto de aporte pension empleados del registro ' + convert(varchar(3),@Position) + ' está vacío'
						where Id = @Id			
						--Se pasa a la siguiente posicion del cursor
						GoTo NextFetch
					End

					--Se valida que el codigo del concepto de aporte pension empleado exista
					if (select COUNT(*) from Payroll.Concept where Code = @ConceptCodePensionContributionValue) = 0
					Begin
						--Se actualiza los campos con el estado en false y el mensaje de error
						update @TableXmlObject set StatusField = 0, MessageField = 'El código de concepto de aporte pension empleados del registro ' + convert(varchar(3),@Position) + ' no existe en la BD'
						where Id = @Id			
						--Se pasa a la siguiente posicion del cursor
						GoTo NextFetch
					End

					--Se actualiza el id del concepto de aporte pension empleado
					update @TableXmlObject set ConceptIdPensionContributionValue = (select Id from Payroll.Concept where Code = @ConceptCodePensionContributionValue) where Id = @Id
				End				
			End

			--Si el aporte de pension patrono no está vacio se realiza las validaciones
			if LEN(@EmployerPensionContributionValue) > 0
			Begin
				--Se valida que el aporte pension patrono sea numerico
				if ISNUMERIC(@EmployerPensionContributionValue) = 0
				Begin
					--Se actualiza los campos con el estado en false y el mensaje de error
					update @TableXmlObject set StatusField = 0, MessageField = 'El aporte pension patrono del registro ' + convert(varchar(3),@Position) + ' no es un valor numérico'
					where Id = @Id			
					--Se pasa a la siguiente posicion del cursor
					GoTo NextFetch
				End

				--Se valida que el aporte pension patrono sea mayor a cero para realizar las validaciones del concepto
				if @EmployerPensionContributionValue > 0
				Begin
					--Se valida que el codigo del concepto de aporte pension patrono no venga vacio
					if LEN(@ConceptCodeEmployerPensionContributionValue) = 0
					Begin
						--Se actualiza los campos con el estado en false y el mensaje de error
						update @TableXmlObject set StatusField = 0, MessageField = 'El código de concepto de aporte pension patrono del registro ' + convert(varchar(3),@Position) + ' está vacío'
						where Id = @Id			
						--Se pasa a la siguiente posicion del cursor
						GoTo NextFetch
					End

					--Se valida que el codigo del concepto de aporte pension patrono exista
					if (select COUNT(*) from Payroll.Concept where Code = @ConceptCodeEmployerPensionContributionValue) = 0
					Begin
						--Se actualiza los campos con el estado en false y el mensaje de error
						update @TableXmlObject set StatusField = 0, MessageField = 'El código de concepto de aporte pension patrono del registro ' + convert(varchar(3),@Position) + ' no existe en la BD'
						where Id = @Id			
						--Se pasa a la siguiente posicion del cursor
						GoTo NextFetch
					End

					--Se actualiza el id del concepto de aporte pension patrono
					update @TableXmlObject set ConceptIdEmployerPensionContributionValue = (select Id from Payroll.Concept where Code = @ConceptCodeEmployerPensionContributionValue) where Id = @Id
				End				
			End

			--Se valida que si la base de salud viene con valor sea numerico
			if LEN(@HealthJCB) > 0
			Begin
				--Se valida que la base salud sea numerico
				if ISNUMERIC(@HealthJCB) = 0
				Begin
					--Se actualiza los campos con el estado en false y el mensaje de error
					update @TableXmlObject set StatusField = 0, MessageField = 'La base salud del registro ' + convert(varchar(3),@Position) + ' no es un valor numérico'
					where Id = @Id			
					--Se pasa a la siguiente posicion del cursor
					GoTo NextFetch
				End

				--Se valida que la base salud no sea negativo
				if @HealthJCB < 0
				Begin
					--Se actualiza los campos con el estado en false y el mensaje de error
					update @TableXmlObject set StatusField = 0, MessageField = 'La base salud del registro ' + convert(varchar(3),@Position) + ' es negativo'
					where Id = @Id			
					--Se pasa a la siguiente posicion del cursor
					GoTo NextFetch
				End
			End

			--Si el aporte salud empleado no está vacio se realiza las validaciones
			if LEN(@EmployeeHealthContributionValue) > 0
			Begin
				--Se valida que el aporte salud empleado sea numerico
				if ISNUMERIC(@EmployeeHealthContributionValue) = 0
				Begin
					--Se actualiza los campos con el estado en false y el mensaje de error
					update @TableXmlObject set StatusField = 0, MessageField = 'El aporte salud empleado del registro ' + convert(varchar(3),@Position) + ' no es un valor numérico'
					where Id = @Id			
					--Se pasa a la siguiente posicion del cursor
					GoTo NextFetch
				End

				--Se valida que el aporte salud empleado sea mayor a cero para realizar las validaciones del concepto
				if @EmployeeHealthContributionValue > 0
				Begin
					--Se valida que el codigo del concepto de aporte salud empleado no venga vacio
					if LEN(@ConceptCodeEmployeeHealthContributionValue) = 0
					Begin
						--Se actualiza los campos con el estado en false y el mensaje de error
						update @TableXmlObject set StatusField = 0, MessageField = 'El código de concepto de aporte salud empleado del registro ' + convert(varchar(3),@Position) + ' está vacío'
						where Id = @Id			
						--Se pasa a la siguiente posicion del cursor
						GoTo NextFetch
					End

					--Se valida que el codigo del concepto de aporte salud empleado exista
					if (select COUNT(*) from Payroll.Concept where Code = @ConceptCodeEmployeeHealthContributionValue) = 0
					Begin
						--Se actualiza los campos con el estado en false y el mensaje de error
						update @TableXmlObject set StatusField = 0, MessageField = 'El código de concepto de aporte salud empleado del registro ' + convert(varchar(3),@Position) + ' no existe en la BD'
						where Id = @Id			
						--Se pasa a la siguiente posicion del cursor
						GoTo NextFetch
					End

					--Se actualiza el id del concepto de aporte pension patrono
					update @TableXmlObject set ConceptIdEmployeeHealthContributionValue = (select Id from Payroll.Concept where Code = @ConceptCodeEmployeeHealthContributionValue) where Id = @Id
				End				
			End

			--Si el aporte salud patrono no está vacio se realiza las validaciones
			if LEN(@EmployerHealthContributionValue) > 0
			Begin
				--Se valida que el aporte salud patrono sea numerico
				if ISNUMERIC(@EmployerHealthContributionValue) = 0
				Begin
					--Se actualiza los campos con el estado en false y el mensaje de error
					update @TableXmlObject set StatusField = 0, MessageField = 'El aporte salud patrono del registro ' + convert(varchar(3),@Position) + ' no es un valor numérico'
					where Id = @Id			
					--Se pasa a la siguiente posicion del cursor
					GoTo NextFetch
				End

				--Se valida que el aporte salud patrono sea mayor a cero para realizar las validaciones del concepto
				if @EmployerHealthContributionValue > 0
				Begin
					--Se valida que el codigo del concepto de aporte salud patrono no venga vacio
					if LEN(@ConceptCodeEmployerHealthContributionValue) = 0
					Begin
						--Se actualiza los campos con el estado en false y el mensaje de error
						update @TableXmlObject set StatusField = 0, MessageField = 'El código de concepto de aporte salud patrono del registro ' + convert(varchar(3),@Position) + ' está vacío'
						where Id = @Id			
						--Se pasa a la siguiente posicion del cursor
						GoTo NextFetch
					End

					--Se valida que el codigo del concepto de aporte salud patrono exista
					if (select COUNT(*) from Payroll.Concept where Code = @ConceptCodeEmployerHealthContributionValue) = 0
					Begin
						--Se actualiza los campos con el estado en false y el mensaje de error
						update @TableXmlObject set StatusField = 0, MessageField = 'El código de concepto de aporte salud patrono del registro ' + convert(varchar(3),@Position) + ' no existe en la BD'
						where Id = @Id			
						--Se pasa a la siguiente posicion del cursor
						GoTo NextFetch
					End

					--Se actualiza el id del concepto de aporte pension patrono
					update @TableXmlObject set ConceptIdEmployerHealthContributionValue = (select Id from Payroll.Concept where Code = @ConceptCodeEmployerHealthContributionValue) where Id = @Id
				End				
			End

			--Se valida que si la base primas viene con valor sea numerico
			if LEN(@IBCIncentivePayment) > 0
			Begin
				--Se valida que la base primas sea numerico
				if ISNUMERIC(@IBCIncentivePayment) = 0
				Begin
					--Se actualiza los campos con el estado en false y el mensaje de error
					update @TableXmlObject set StatusField = 0, MessageField = 'La base primas del registro ' + convert(varchar(3),@Position) + ' no es un valor numérico'
					where Id = @Id			
					--Se pasa a la siguiente posicion del cursor
					GoTo NextFetch
				End

				--Se valida que la base primas no sea negativo
				if @IBCIncentivePayment < 0
				Begin
					--Se actualiza los campos con el estado en false y el mensaje de error
					update @TableXmlObject set StatusField = 0, MessageField = 'La base primas del registro ' + convert(varchar(3),@Position) + ' es negativo'
					where Id = @Id			
					--Se pasa a la siguiente posicion del cursor
					GoTo NextFetch
				End
			End

			--Si la provision primas no está vacio se realiza las validaciones
			if LEN(@ProvisionIncentive) > 0
			Begin
				--Se valida que provision primas sea numerico
				if ISNUMERIC(@ProvisionIncentive) = 0
				Begin
					--Se actualiza los campos con el estado en false y el mensaje de error
					update @TableXmlObject set StatusField = 0, MessageField = 'La provisión primas del registro ' + convert(varchar(3),@Position) + ' no es un valor numérico'
					where Id = @Id			
					--Se pasa a la siguiente posicion del cursor
					GoTo NextFetch
				End

				--Se valida que la provision primas sea mayor a cero para realizar las validaciones del concepto
				if @ProvisionIncentive > 0
				Begin
					--Se valida que el codigo del concepto de provision primas no venga vacio
					if LEN(@ConceptCodeProvisionIncentive) = 0
					Begin
						--Se actualiza los campos con el estado en false y el mensaje de error
						update @TableXmlObject set StatusField = 0, MessageField = 'El código de concepto de provision primas del registro ' + convert(varchar(3),@Position) + ' está vacío'
						where Id = @Id			
						--Se pasa a la siguiente posicion del cursor
						GoTo NextFetch
					End

					--Se valida que el codigo del concepto de provision primas exista
					if (select COUNT(*) from Payroll.Concept where Code = @ConceptCodeProvisionIncentive) = 0
					Begin
						--Se actualiza los campos con el estado en false y el mensaje de error
						update @TableXmlObject set StatusField = 0, MessageField = 'El código de concepto de provision primas del registro ' + convert(varchar(3),@Position) + ' no existe en la BD'
						where Id = @Id			
						--Se pasa a la siguiente posicion del cursor
						GoTo NextFetch
					End

					--Se actualiza el id del concepto de aporte pension patrono
					update @TableXmlObject set ConceptIdProvisionIncentive = (select Id from Payroll.Concept where Code = @ConceptCodeProvisionIncentive) where Id = @Id
				End				
			End

			--Se valida que si la base vacaciones viene con valor sea numerico
			if LEN(@IBCVacation) > 0
			Begin
				--Se valida que la base vacaciones sea numerico
				if ISNUMERIC(@IBCVacation) = 0
				Begin
					--Se actualiza los campos con el estado en false y el mensaje de error
					update @TableXmlObject set StatusField = 0, MessageField = 'La base vacaciones del registro ' + convert(varchar(3),@Position) + ' no es un valor numérico'
					where Id = @Id			
					--Se pasa a la siguiente posicion del cursor
					GoTo NextFetch
				End

				--Se valida que la base vacaciones no sea negativo
				if @IBCVacation < 0
				Begin
					--Se actualiza los campos con el estado en false y el mensaje de error
					update @TableXmlObject set StatusField = 0, MessageField = 'La base vacaciones del registro ' + convert(varchar(3),@Position) + ' es negativo'
					where Id = @Id			
					--Se pasa a la siguiente posicion del cursor
					GoTo NextFetch
				End
			End

			--Si la provision vacaciones no está vacio se realiza las validaciones
			if LEN(@ProvisionVacation) > 0
			Begin
				--Se valida que provision vacaciones sea numerico
				if ISNUMERIC(@ProvisionVacation) = 0
				Begin
					--Se actualiza los campos con el estado en false y el mensaje de error
					update @TableXmlObject set StatusField = 0, MessageField = 'La provisión vacaciones del registro ' + convert(varchar(3),@Position) + ' no es un valor numérico'
					where Id = @Id			
					--Se pasa a la siguiente posicion del cursor
					GoTo NextFetch
				End

				--Se valida que la provision vacaciones sea mayor a cero para realizar las validaciones del concepto
				if @ProvisionVacation > 0
				Begin
					--Se valida que el codigo del concepto de provision vacaciones no venga vacio
					if LEN(@ConceptCodeProvisionVacation) = 0
					Begin
						--Se actualiza los campos con el estado en false y el mensaje de error
						update @TableXmlObject set StatusField = 0, MessageField = 'El código de concepto de provision vacaciones del registro ' + convert(varchar(3),@Position) + ' está vacío'
						where Id = @Id			
						--Se pasa a la siguiente posicion del cursor
						GoTo NextFetch
					End

					--Se valida que el codigo del concepto de provision vacaciones exista
					if (select COUNT(*) from Payroll.Concept where Code = @ConceptCodeProvisionVacation) = 0
					Begin
						--Se actualiza los campos con el estado en false y el mensaje de error
						update @TableXmlObject set StatusField = 0, MessageField = 'El código de concepto de provision vacaciones del registro ' + convert(varchar(3),@Position) + ' no existe en la BD'
						where Id = @Id			
						--Se pasa a la siguiente posicion del cursor
						GoTo NextFetch
					End

					--Se actualiza el id del concepto de provision vacaciones
					update @TableXmlObject set ConceptIdProvisionVacation = (select Id from Payroll.Concept where Code = @ConceptCodeProvisionVacation) where Id = @Id
				End				
			End

			--Se valida que si la base cesantias viene con valor sea numerico
			if LEN(@IBCUnemployment) > 0
			Begin
				--Se valida que la base cesantias sea numerico
				if ISNUMERIC(@IBCUnemployment) = 0
				Begin
					--Se actualiza los campos con el estado en false y el mensaje de error
					update @TableXmlObject set StatusField = 0, MessageField = 'La base cesantías del registro ' + convert(varchar(3),@Position) + ' no es un valor numérico'
					where Id = @Id			
					--Se pasa a la siguiente posicion del cursor
					GoTo NextFetch
				End

				--Se valida que la base cesantias no sea negativo
				if @IBCUnemployment < 0
				Begin
					--Se actualiza los campos con el estado en false y el mensaje de error
					update @TableXmlObject set StatusField = 0, MessageField = 'La base cesantías del registro ' + convert(varchar(3),@Position) + ' es negativo'
					where Id = @Id			
					--Se pasa a la siguiente posicion del cursor
					GoTo NextFetch
				End
			End

			--Si la provision cesantias no está vacio se realiza las validaciones
			if LEN(@UnemploymentAccumulated) > 0
			Begin
				--Se valida que provision cesantias sea numerico
				if ISNUMERIC(@UnemploymentAccumulated) = 0
				Begin
					--Se actualiza los campos con el estado en false y el mensaje de error
					update @TableXmlObject set StatusField = 0, MessageField = 'La provisión cesantías del registro ' + convert(varchar(3),@Position) + ' no es un valor numérico'
					where Id = @Id			
					--Se pasa a la siguiente posicion del cursor
					GoTo NextFetch
				End

				--Se valida que la provision cesantias sea mayor a cero para realizar las validaciones del concepto
				if @UnemploymentAccumulated > 0
				Begin
					--Se valida que el codigo del concepto de provision cesantias no venga vacio
					if LEN(@ConceptCodeUnemploymentAccumulated) = 0
					Begin
						--Se actualiza los campos con el estado en false y el mensaje de error
						update @TableXmlObject set StatusField = 0, MessageField = 'El código de concepto de provision cesantías del registro ' + convert(varchar(3),@Position) + ' está vacío'
						where Id = @Id			
						--Se pasa a la siguiente posicion del cursor
						GoTo NextFetch
					End

					--Se valida que el codigo del concepto de provision cesantias exista
					if (select COUNT(*) from Payroll.Concept where Code = @ConceptCodeUnemploymentAccumulated) = 0
					Begin
						--Se actualiza los campos con el estado en false y el mensaje de error
						update @TableXmlObject set StatusField = 0, MessageField = 'El código de concepto de provision cesantías del registro ' + convert(varchar(3),@Position) + ' no existe en la BD'
						where Id = @Id			
						--Se pasa a la siguiente posicion del cursor
						GoTo NextFetch
					End

					--Se actualiza el id del concepto de provision cesantias
					update @TableXmlObject set ConceptIdUnemploymentAccumulated = (select Id from Payroll.Concept where Code = @ConceptCodeUnemploymentAccumulated) where Id = @Id
				End				
			End

			--Si la provision intereses cesantias no está vacio se realiza las validaciones
			if LEN(@ProvisionInterestsUnemployment) > 0
			Begin
				--Se valida que provision intereses cesantias sea numerico
				if ISNUMERIC(@ProvisionInterestsUnemployment) = 0
				Begin
					--Se actualiza los campos con el estado en false y el mensaje de error
					update @TableXmlObject set StatusField = 0, MessageField = 'La provisión intereses cesantías del registro ' + convert(varchar(3),@Position) + ' no es un valor numérico'
					where Id = @Id			
					--Se pasa a la siguiente posicion del cursor
					GoTo NextFetch
				End

				--Se valida que la provision intereses cesantias sea mayor a cero para realizar las validaciones del concepto
				if @ProvisionInterestsUnemployment > 0
				Begin
					--Se valida que el codigo del concepto de provision intereses cesantias no venga vacio
					if LEN(@ConceptCodeProvisionInterestsUnemployment) = 0
					Begin
						--Se actualiza los campos con el estado en false y el mensaje de error
						update @TableXmlObject set StatusField = 0, MessageField = 'El código de concepto de provision intereses cesantías del registro ' + convert(varchar(3),@Position) + ' está vacío'
						where Id = @Id			
						--Se pasa a la siguiente posicion del cursor
						GoTo NextFetch
					End

					--Se valida que el codigo del concepto de provision intereses cesantias exista
					if (select COUNT(*) from Payroll.Concept where Code = @ConceptCodeProvisionInterestsUnemployment) = 0
					Begin
						--Se actualiza los campos con el estado en false y el mensaje de error
						update @TableXmlObject set StatusField = 0, MessageField = 'El código de concepto de provision intereses cesantías del registro ' + convert(varchar(3),@Position) + ' no existe en la BD'
						where Id = @Id			
						--Se pasa a la siguiente posicion del cursor
						GoTo NextFetch
					End

					--Se actualiza el id del concepto de provision intereses cesantias
					update @TableXmlObject set ConceptIdProvisionInterestsUnemployment = (select Id from Payroll.Concept where Code = @ConceptCodeProvisionInterestsUnemployment) where Id = @Id
				End				
			End

			--Si el valor incapacidad ambularotia no está vacio se realiza las validaciones
			if LEN(@AmbulatoryDisabilityValue) > 0
			Begin
				--Se valida que el valor incapacidad ambulatoria sea numerico
				if ISNUMERIC(@AmbulatoryDisabilityValue) = 0
				Begin
					--Se actualiza los campos con el estado en false y el mensaje de error
					update @TableXmlObject set StatusField = 0, MessageField = 'El valor incapacidad ambulatoria del registro ' + convert(varchar(3),@Position) + ' no es un valor numérico'
					where Id = @Id			
					--Se pasa a la siguiente posicion del cursor
					GoTo NextFetch
				End

				--Se valida que el valor incapacidad ambulatoria sea mayor a cero para realizar las validaciones del concepto
				if @AmbulatoryDisabilityValue > 0
				Begin
					--Se valida que el codigo del concepto de el valor incapacidad ambulatoria no venga vacio
					if LEN(@ConceptCodeAmbulatoryDisabilityValue) = 0
					Begin
						--Se actualiza los campos con el estado en false y el mensaje de error
						update @TableXmlObject set StatusField = 0, MessageField = 'El código de concepto de valor incapacidad ambulatoria del registro ' + convert(varchar(3),@Position) + ' está vacío'
						where Id = @Id			
						--Se pasa a la siguiente posicion del cursor
						GoTo NextFetch
					End

					--Se valida que el codigo del concepto de el valor incapacidad ambulatoria exista
					if (select COUNT(*) from Payroll.Concept where Code = @ConceptCodeAmbulatoryDisabilityValue) = 0
					Begin
						--Se actualiza los campos con el estado en false y el mensaje de error
						update @TableXmlObject set StatusField = 0, MessageField = 'El código de concepto de valor incapacidad ambulatoria del registro ' + convert(varchar(3),@Position) + ' no existe en la BD'
						where Id = @Id			
						--Se pasa a la siguiente posicion del cursor
						GoTo NextFetch
					End

					--Se actualiza el id del concepto de valor incapacidad ambulatoria
					update @TableXmlObject set ConceptIdAmbulatoryDisabilityValue = (select Id from Payroll.Concept where Code = @ConceptCodeAmbulatoryDisabilityValue) where Id = @Id
				End				
			End

			--Si el valor incapacidad hospitalaria no está vacio se realiza las validaciones
			if LEN(@DisabilityHospitalValue) > 0
			Begin
				--Se valida que el valor incapacidad hospitalaria sea numerico
				if ISNUMERIC(@DisabilityHospitalValue) = 0
				Begin
					--Se actualiza los campos con el estado en false y el mensaje de error
					update @TableXmlObject set StatusField = 0, MessageField = 'El valor incapacidad hospitalaria del registro ' + convert(varchar(3),@Position) + ' no es un valor numérico'
					where Id = @Id			
					--Se pasa a la siguiente posicion del cursor
					GoTo NextFetch
				End

				--Se valida que el valor incapacidad hospitalaria sea mayor a cero para realizar las validaciones del concepto
				if @DisabilityHospitalValue > 0
				Begin
					--Se valida que el codigo del concepto de el valor incapacidad hospitalaria no venga vacio
					if LEN(@ConceptCodeDisabilityHospitalValue) = 0
					Begin
						--Se actualiza los campos con el estado en false y el mensaje de error
						update @TableXmlObject set StatusField = 0, MessageField = 'El código de concepto de valor incapacidad hospitalaria del registro ' + convert(varchar(3),@Position) + ' está vacío'
						where Id = @Id			
						--Se pasa a la siguiente posicion del cursor
						GoTo NextFetch
					End

					--Se valida que el codigo del concepto de el valor incapacidad hospitalaria exista
					if (select COUNT(*) from Payroll.Concept where Code = @ConceptCodeDisabilityHospitalValue) = 0
					Begin
						--Se actualiza los campos con el estado en false y el mensaje de error
						update @TableXmlObject set StatusField = 0, MessageField = 'El código de concepto de valor incapacidad hospitalaria del registro ' + convert(varchar(3),@Position) + ' no existe en la BD'
						where Id = @Id			
						--Se pasa a la siguiente posicion del cursor
						GoTo NextFetch
					End

					--Se actualiza el id del concepto de valor incapacidad hospitalaria
					update @TableXmlObject set ConceptIdDisabilityHospitalValue = (select Id from Payroll.Concept where Code = @ConceptCodeDisabilityHospitalValue) where Id = @Id
				End				
			End

			--Si el valor licencia maternidad no está vacio se realiza las validaciones
			if LEN(@MaternityLeaveValue) > 0
			Begin
				--Se valida que el valor licencia maternidad sea numerico
				if ISNUMERIC(@MaternityLeaveValue) = 0
				Begin
					--Se actualiza los campos con el estado en false y el mensaje de error
					update @TableXmlObject set StatusField = 0, MessageField = 'El valor licencia maternidad del registro ' + convert(varchar(3),@Position) + ' no es un valor numérico'
					where Id = @Id			
					--Se pasa a la siguiente posicion del cursor
					GoTo NextFetch
				End

				--Se valida que el valor licencia maternidad sea mayor a cero para realizar las validaciones del concepto
				if @MaternityLeaveValue > 0
				Begin
					--Se valida que el codigo del concepto de el valor licencia maternidad no venga vacio
					if LEN(@ConceptCodeMaternityLeaveValue) = 0
					Begin
						--Se actualiza los campos con el estado en false y el mensaje de error
						update @TableXmlObject set StatusField = 0, MessageField = 'El código de concepto de valor licencia maternidad del registro ' + convert(varchar(3),@Position) + ' está vacío'
						where Id = @Id			
						--Se pasa a la siguiente posicion del cursor
						GoTo NextFetch
					End

					--Se valida que el codigo del concepto de el valor licencia maternidad exista
					if (select COUNT(*) from Payroll.Concept where Code = @ConceptCodeMaternityLeaveValue) = 0
					Begin
						--Se actualiza los campos con el estado en false y el mensaje de error
						update @TableXmlObject set StatusField = 0, MessageField = 'El código de concepto de valor licencia maternidad del registro ' + convert(varchar(3),@Position) + ' no existe en la BD'
						where Id = @Id			
						--Se pasa a la siguiente posicion del cursor
						GoTo NextFetch
					End

					--Se actualiza el id del concepto de valor licencia maternidad
					update @TableXmlObject set ConceptIdMaternityLeaveValue = (select Id from Payroll.Concept where Code = @ConceptCodeMaternityLeaveValue) where Id = @Id
				End				
			End

			--Se valida que si la base sena viene con valor sea numerico
			if LEN(@IBCSENA) > 0
			Begin
				--Se valida que la base sena sea numerico
				if ISNUMERIC(@IBCSENA) = 0
				Begin
					--Se actualiza los campos con el estado en false y el mensaje de error
					update @TableXmlObject set StatusField = 0, MessageField = 'La base sena del registro ' + convert(varchar(3),@Position) + ' no es un valor numérico'
					where Id = @Id			
					--Se pasa a la siguiente posicion del cursor
					GoTo NextFetch
				End

				--Se valida que la base sena no sea negativo
				if @IBCSENA < 0
				Begin
					--Se actualiza los campos con el estado en false y el mensaje de error
					update @TableXmlObject set StatusField = 0, MessageField = 'La base sena del registro ' + convert(varchar(3),@Position) + ' es negativo'
					where Id = @Id			
					--Se pasa a la siguiente posicion del cursor
					GoTo NextFetch
				End
			End

			--Si el aporte sena no está vacio se realiza las validaciones
			if LEN(@SenaContributionValue) > 0
			Begin
				--Se valida que el aporte sena sea numerico
				if ISNUMERIC(@SenaContributionValue) = 0
				Begin
					--Se actualiza los campos con el estado en false y el mensaje de error
					update @TableXmlObject set StatusField = 0, MessageField = 'El aporte sena del registro ' + convert(varchar(3),@Position) + ' no es un valor numérico'
					where Id = @Id			
					--Se pasa a la siguiente posicion del cursor
					GoTo NextFetch
				End

				--Se valida que el aporte sena sea mayor a cero para realizar las validaciones del concepto
				if @SenaContributionValue > 0
				Begin
					--Se valida que el codigo del concepto de el aporte sena no venga vacio
					if LEN(@ConceptCodeSenaContributionValue) = 0
					Begin
						--Se actualiza los campos con el estado en false y el mensaje de error
						update @TableXmlObject set StatusField = 0, MessageField = 'El código de concepto de aporte sena del registro ' + convert(varchar(3),@Position) + ' está vacío'
						where Id = @Id			
						--Se pasa a la siguiente posicion del cursor
						GoTo NextFetch
					End

					--Se valida que el codigo del concepto de el aporte sena exista
					if (select COUNT(*) from Payroll.Concept where Code = @ConceptCodeSenaContributionValue) = 0
					Begin
						--Se actualiza los campos con el estado en false y el mensaje de error
						update @TableXmlObject set StatusField = 0, MessageField = 'El código de concepto de sena del registro ' + convert(varchar(3),@Position) + ' no existe en la BD'
						where Id = @Id			
						--Se pasa a la siguiente posicion del cursor
						GoTo NextFetch
					End

					--Se actualiza el id del concepto de aporte sena
					update @TableXmlObject set ConceptIdSenaContributionValue = (select Id from Payroll.Concept where Code = @ConceptCodeSenaContributionValue) where Id = @Id
				End				
			End

			--Se valida que si la base icbf viene con valor sea numerico
			if LEN(@IBCICBF) > 0
			Begin
				--Se valida que la base icbf sea numerico
				if ISNUMERIC(@IBCICBF) = 0
				Begin
					--Se actualiza los campos con el estado en false y el mensaje de error
					update @TableXmlObject set StatusField = 0, MessageField = 'La base ICBF del registro ' + convert(varchar(3),@Position) + ' no es un valor numérico'
					where Id = @Id			
					--Se pasa a la siguiente posicion del cursor
					GoTo NextFetch
				End

				--Se valida que la icbf sena no sea negativo
				if @IBCICBF < 0
				Begin
					--Se actualiza los campos con el estado en false y el mensaje de error
					update @TableXmlObject set StatusField = 0, MessageField = 'La base ICBF del registro ' + convert(varchar(3),@Position) + ' es negativo'
					where Id = @Id			
					--Se pasa a la siguiente posicion del cursor
					GoTo NextFetch
				End
			End

			--Si el aporte icbf no está vacio se realiza las validaciones
			if LEN(@ICBFContributionValue) > 0
			Begin
				--Se valida que el aporte icbf sea numerico
				if ISNUMERIC(@ICBFContributionValue) = 0
				Begin
					--Se actualiza los campos con el estado en false y el mensaje de error
					update @TableXmlObject set StatusField = 0, MessageField = 'El aporte ICBF del registro ' + convert(varchar(3),@Position) + ' no es un valor numérico'
					where Id = @Id			
					--Se pasa a la siguiente posicion del cursor
					GoTo NextFetch
				End

				--Se valida que el aporte icbf sea mayor a cero para realizar las validaciones del concepto
				if @ICBFContributionValue > 0
				Begin
					--Se valida que el codigo del concepto de el aporte icbf no venga vacio
					if LEN(@ConceptCodeICBFContributionValue) = 0
					Begin
						--Se actualiza los campos con el estado en false y el mensaje de error
						update @TableXmlObject set StatusField = 0, MessageField = 'El código de concepto de aporte ICBF del registro ' + convert(varchar(3),@Position) + ' está vacío'
						where Id = @Id			
						--Se pasa a la siguiente posicion del cursor
						GoTo NextFetch
					End

					--Se valida que el codigo del concepto de el aporte icbf exista
					if (select COUNT(*) from Payroll.Concept where Code = @ConceptCodeICBFContributionValue) = 0
					Begin
						--Se actualiza los campos con el estado en false y el mensaje de error
						update @TableXmlObject set StatusField = 0, MessageField = 'El código de concepto de aporte ICBF del registro ' + convert(varchar(3),@Position) + ' no existe en la BD'
						where Id = @Id			
						--Se pasa a la siguiente posicion del cursor
						GoTo NextFetch
					End

					--Se actualiza el id del concepto de aporte icbf
					update @TableXmlObject set ConceptIdICBFContributionValue = (select Id from Payroll.Concept where Code = @ConceptCodeICBFContributionValue) where Id = @Id
				End				
			End

			--Se valida que si la base caja compensacion viene con valor sea numerico
			if LEN(@IBCCompensationFund) > 0
			Begin
				--Se valida que la base caja compensacion sea numerico
				if ISNUMERIC(@IBCCompensationFund) = 0
				Begin
					--Se actualiza los campos con el estado en false y el mensaje de error
					update @TableXmlObject set StatusField = 0, MessageField = 'La base caja compensación del registro ' + convert(varchar(3),@Position) + ' no es un valor numérico'
					where Id = @Id			
					--Se pasa a la siguiente posicion del cursor
					GoTo NextFetch
				End

				--Se valida que la base caja compensación no sea negativo
				if @IBCCompensationFund < 0
				Begin
					--Se actualiza los campos con el estado en false y el mensaje de error
					update @TableXmlObject set StatusField = 0, MessageField = 'La base caja compensación del registro ' + convert(varchar(3),@Position) + ' es negativo'
					where Id = @Id			
					--Se pasa a la siguiente posicion del cursor
					GoTo NextFetch
				End
			End

			--Si el aporte caja compensacion no está vacio se realiza las validaciones
			if LEN(@FamilyCompensationFundContributionValue) > 0
			Begin
				--Se valida que el aporte caja compensacion sea numerico
				if ISNUMERIC(@FamilyCompensationFundContributionValue) = 0
				Begin
					--Se actualiza los campos con el estado en false y el mensaje de error
					update @TableXmlObject set StatusField = 0, MessageField = 'El aporte caja compensación del registro ' + convert(varchar(3),@Position) + ' no es un valor numérico'
					where Id = @Id			
					--Se pasa a la siguiente posicion del cursor
					GoTo NextFetch
				End

				--Se valida que el aporte caja comepnsación sea mayor a cero para realizar las validaciones del concepto
				if @FamilyCompensationFundContributionValue > 0
				Begin
					--Se valida que el codigo del concepto de el aporte caja compensacion no venga vacio
					if LEN(@ConceptCodeFamilyCompensationFundContributionValue) = 0
					Begin
						--Se actualiza los campos con el estado en false y el mensaje de error
						update @TableXmlObject set StatusField = 0, MessageField = 'El código de concepto de aporte caja compensación del registro ' + convert(varchar(3),@Position) + ' está vacío'
						where Id = @Id			
						--Se pasa a la siguiente posicion del cursor
						GoTo NextFetch
					End

					--Se valida que el codigo del concepto de el aporte caja compensación exista
					if (select COUNT(*) from Payroll.Concept where Code = @ConceptCodeFamilyCompensationFundContributionValue) = 0
					Begin
						--Se actualiza los campos con el estado en false y el mensaje de error
						update @TableXmlObject set StatusField = 0, MessageField = 'El código de concepto de aporte caja compensación del registro ' + convert(varchar(3),@Position) + ' no existe en la BD'
						where Id = @Id			
						--Se pasa a la siguiente posicion del cursor
						GoTo NextFetch
					End

					--Se actualiza el id del concepto de aporte caja compensacion
					update @TableXmlObject set ConceptIdFamilyCompensationFundContributionValue = (select Id from Payroll.Concept where Code = @ConceptCodeFamilyCompensationFundContributionValue) where Id = @Id
				End				
			End

			--Se valida que si la base retencion viene con valor sea numerico
			if LEN(@RealBaseRetention) > 0
			Begin
				--Se valida que la base retencion sea numerico
				if ISNUMERIC(@RealBaseRetention) = 0
				Begin
					--Se actualiza los campos con el estado en false y el mensaje de error
					update @TableXmlObject set StatusField = 0, MessageField = 'La base retención del registro ' + convert(varchar(3),@Position) + ' no es un valor numérico'
					where Id = @Id			
					--Se pasa a la siguiente posicion del cursor
					GoTo NextFetch
				End

				--Se valida que la base retencion no sea negativo
				if @RealBaseRetention < 0
				Begin
					--Se actualiza los campos con el estado en false y el mensaje de error
					update @TableXmlObject set StatusField = 0, MessageField = 'La base retención del registro ' + convert(varchar(3),@Position) + ' es negativo'
					where Id = @Id			
					--Se pasa a la siguiente posicion del cursor
					GoTo NextFetch
				End
			End

			--Si el valor retencion no está vacio se realiza las validaciones
			if LEN(@CalculatedWithholdingValue) > 0
			Begin
				--Se valida que el valor retencion sea numerico
				if ISNUMERIC(@CalculatedWithholdingValue) = 0
				Begin
					--Se actualiza los campos con el estado en false y el mensaje de error
					update @TableXmlObject set StatusField = 0, MessageField = 'El valor retención del registro ' + convert(varchar(3),@Position) + ' no es un valor numérico'
					where Id = @Id			
					--Se pasa a la siguiente posicion del cursor
					GoTo NextFetch
				End

				--Se valida que el valor retencion sea mayor a cero para realizar las validaciones del concepto
				if @CalculatedWithholdingValue > 0
				Begin
					--Se valida que el codigo del concepto de el valor retencion no venga vacio
					if LEN(@ConceptCodeCalculatedWithholdingValue) = 0
					Begin
						--Se actualiza los campos con el estado en false y el mensaje de error
						update @TableXmlObject set StatusField = 0, MessageField = 'El código de concepto de valor retención del registro ' + convert(varchar(3),@Position) + ' está vacío'
						where Id = @Id			
						--Se pasa a la siguiente posicion del cursor
						GoTo NextFetch
					End

					--Se valida que el codigo del concepto de el valor retencion
					if (select COUNT(*) from Payroll.Concept where Code = @ConceptCodeCalculatedWithholdingValue) = 0
					Begin
						--Se actualiza los campos con el estado en false y el mensaje de error
						update @TableXmlObject set StatusField = 0, MessageField = 'El código de concepto de valor retención del registro ' + convert(varchar(3),@Position) + ' no existe en la BD'
						where Id = @Id			
						--Se pasa a la siguiente posicion del cursor
						GoTo NextFetch
					End

					--Se actualiza el id del concepto de valor retencion
					update @TableXmlObject set ConceptIdCalculatedWithholdingValue = (select Id from Payroll.Concept where Code = @ConceptCodeCalculatedWithholdingValue) where Id = @Id
				End				
			End
			
			--Si el recargo no está vacio se realiza las validaciones
			if LEN(@RecargoNocturno) > 0
			Begin
				--Se valida que el recargo sea numerico
				if ISNUMERIC(@RecargoNocturno) = 0
				Begin
					--Se actualiza los campos con el estado en false y el mensaje de error
					update @TableXmlObject set StatusField = 0, MessageField = 'El recargo del registro ' + convert(varchar(3),@Position) + ' no es un valor numérico'
					where Id = @Id			
					--Se pasa a la siguiente posicion del cursor
					GoTo NextFetch
				End

				--Se valida que el recargo sea mayor a cero para realizar las validaciones del concepto
				if @RecargoNocturno > 0
				Begin
					--Se valida que el codigo del concepto de el recargo no venga vacio
					if LEN(@ConceptCodeRecargoNocturno) = 0
					Begin
						--Se actualiza los campos con el estado en false y el mensaje de error
						update @TableXmlObject set StatusField = 0, MessageField = 'El código de concepto de recargo del registro ' + convert(varchar(3),@Position) + ' está vacío'
						where Id = @Id			
						--Se pasa a la siguiente posicion del cursor
						GoTo NextFetch
					End

					--Se valida que el codigo del concepto de el recargo exista
					if (select COUNT(*) from Payroll.Concept where Code = @ConceptCodeRecargoNocturno) = 0
					Begin
						--Se actualiza los campos con el estado en false y el mensaje de error
						update @TableXmlObject set StatusField = 0, MessageField = 'El código de concepto de recargo del registro ' + convert(varchar(3),@Position) + ' no existe en la BD'
						where Id = @Id			
						--Se pasa a la siguiente posicion del cursor
						GoTo NextFetch
					End

					--Se actualiza el id del concepto del recargo
					update @TableXmlObject set ConceptIdRecargoNocturno = (select Id from Payroll.Concept where Code = @ConceptCodeRecargoNocturno) where Id = @Id
				End				
			End
			--Se valida que si los dias de sancion tiene valor
			if LEN(@SanctionDays) > 0
			Begin
				--Se valida que los dias de sancion sea numerico
				if ISNUMERIC(@SanctionDays) = 0
				Begin
					--Se actualiza los campos con el estado en false y el mensaje de error
					update @TableXmlObject set StatusField = 0, MessageField = 'Los días de sanción del registro ' + convert(varchar(3),@Position) + ' no es un valor numérico'
					where Id = @Id			
					--Se pasa a la siguiente posicion del cursor
					GoTo NextFetch
				End

				--Se valida que los dias de sancion no sea negativo
				if @SanctionDays < 0
				Begin
					--Se actualiza los campos con el estado en false y el mensaje de error
					update @TableXmlObject set StatusField = 0, MessageField = 'Los días de sanción del registro ' + convert(varchar(3),@Position) + ' es negativo'
					where Id = @Id			
					--Se pasa a la siguiente posicion del cursor
					GoTo NextFetch
				End
			End

			--Si las horas extras no está vacio se realiza las validaciones
			if LEN(@Overtime) > 0
			Begin
				--Se valida que las horas extras sea numerico
				if ISNUMERIC(@Overtime) = 0
				Begin
					--Se actualiza los campos con el estado en false y el mensaje de error
					update @TableXmlObject set StatusField = 0, MessageField = 'Las horas extras del registro ' + convert(varchar(3),@Position) + ' no es un valor numérico'
					where Id = @Id			
					--Se pasa a la siguiente posicion del cursor
					GoTo NextFetch
				End

				--Se valida que las horas extras sea mayor a cero para realizar las validaciones del concepto
				if @Overtime > 0
				Begin
					--Se valida que el codigo del concepto de las horas extras no venga vacio
					if LEN(@ConceptCodeOvertime) = 0
					Begin
						--Se actualiza los campos con el estado en false y el mensaje de error
						update @TableXmlObject set StatusField = 0, MessageField = 'El código de concepto de las horas extras del registro ' + convert(varchar(3),@Position) + ' está vacío'
						where Id = @Id			
						--Se pasa a la siguiente posicion del cursor
						GoTo NextFetch
					End

					--Se valida que el codigo del concepto de las horas extras exista
					if (select COUNT(*) from Payroll.Concept where Code = @ConceptCodeOvertime) = 0
					Begin
						--Se actualiza los campos con el estado en false y el mensaje de error
						update @TableXmlObject set StatusField = 0, MessageField = 'El código de concepto de las horas extras del registro ' + convert(varchar(3),@Position) + ' no existe en la BD'
						where Id = @Id			
						--Se pasa a la siguiente posicion del cursor
						GoTo NextFetch
					End

					--Se actualiza el id del concepto de las horas extras
					update @TableXmlObject set ConceptIdOvertime = (select Id from Payroll.Concept where Code = @ConceptCodeOvertime) where Id = @Id
				End	

				------- NUEVOS CONCEPTOS

				if @RecargoNocturnoFestivo > 0
				Begin
					--Se valida que el codigo del concepto de los Recargos Nocturnos Festivos
					if LEN(@ConceptCodeRecargoNocturnoFestivo) = 0
					Begin
						--Se actualiza los campos con el estado en false y el mensaje de error
						update @TableXmlObject set StatusField = 0, MessageField = 'El código de concepto de Recargos Nocturnas Festivos ' + convert(varchar(3),@Position) + ' está vacío'
						where Id = @Id			
						--Se pasa a la siguiente posicion del cursor
						GoTo NextFetch
					End

					--Se valida que el codigo del concepto de las horas extras exista
					if (select COUNT(*) from Payroll.Concept where Code = @ConceptCodeRecargoNocturnoFestivo) = 0
					Begin
						--Se actualiza los campos con el estado en false y el mensaje de error
						update @TableXmlObject set StatusField = 0, MessageField = 'El código de concepto de las horas Recargos Nocturnas Festivos ' + convert(varchar(3),@Position) + ' no existe en la BD'
						where Id = @Id			
						--Se pasa a la siguiente posicion del cursor
						GoTo NextFetch
					End

					--Se actualiza el id del concepto de las horas extras
					update @TableXmlObject set ConceptIdRecargoNocturnoFestivo = (select Id from Payroll.Concept where Code = @ConceptCodeRecargoNocturnoFestivo) where Id = @Id
				End	

				if @ValorDominicalOrdinario > 0
				Begin
					--Se valida que el codigo del concepto de Dominicales
					if LEN(@ConceptCodeValorDominical) = 0
					Begin
						--Se actualiza los campos con el estado en false y el mensaje de error
						update @TableXmlObject set StatusField = 0, MessageField = 'El código de concepto de Valor Dominical ' + convert(varchar(3),@Position) + ' está vacío'
						where Id = @Id			
						--Se pasa a la siguiente posicion del cursor
						GoTo NextFetch
					End

					--Se valida que el codigo del concepto de Valor Dominical
					if (select COUNT(*) from Payroll.Concept where Code = @ConceptCodeValorDominical) = 0
					Begin
						--Se actualiza los campos con el estado en false y el mensaje de error
						update @TableXmlObject set StatusField = 0, MessageField = 'El código de concepto de Valor Dominical ' + convert(varchar(3),@Position) + ' no existe en la BD'
						where Id = @Id			
						--Se pasa a la siguiente posicion del cursor
						GoTo NextFetch
					End

					--Se actualiza el id del concepto de las horas extras
					update @TableXmlObject set ConceptIdValorDominical = (select Id from Payroll.Concept where Code = @ConceptCodeValorDominical) where Id = @Id
				End	
							
			End
			
			--Se actualiza los campos con que se necesitan para armar el objeto en el formulario
			update @TableXmlObject set StatusField = 1, MessageField = 'OK', TotalPaid = cast(TotalAccrued as decimal(18,0)) - cast(TotalDeducted as decimal(18,0))
			where Id = @Id	
			
			NextFetch:
			--Se pasa a la siguiente posicion del cursor
				Fetch Next From InfoItem Into @Id, @CountFields, @Nit, @PayrollDate, @DaysWorked, @TotalAccrued, @TotalDeducted, @PensionJCB, @PensionContributionValue, @ConceptCodePensionContributionValue, 
				@ConceptIdPensionContributionValue, @EmployerPensionContributionValue, @ConceptCodeEmployerPensionContributionValue, @ConceptIdEmployerPensionContributionValue, @HealthJCB,
				@EmployeeHealthContributionValue, @ConceptCodeEmployeeHealthContributionValue, @ConceptIdEmployeeHealthContributionValue, @EmployerHealthContributionValue, 
				@ConceptCodeEmployerHealthContributionValue, @ConceptIdEmployerHealthContributionValue, @IBCIncentivePayment, @ProvisionIncentive, @ConceptCodeProvisionIncentive, @ConceptIdProvisionIncentive,
				@IBCVacation, @ProvisionVacation, @ConceptCodeProvisionVacation, @ConceptIdProvisionVacation, @IBCUnemployment, @UnemploymentAccumulated, @ConceptCodeUnemploymentAccumulated, @ConceptIdUnemploymentAccumulated,
				@ProvisionInterestsUnemployment, @ConceptCodeProvisionInterestsUnemployment, @ConceptIdProvisionInterestsUnemployment, 
				@AmbulatoryDisabilityValue, @ConceptCodeAmbulatoryDisabilityValue, @ConceptIdAmbulatoryDisabilityValue, @DisabilityHospitalValue, @ConceptCodeDisabilityHospitalValue, @ConceptIdDisabilityHospitalValue,
				@MaternityLeaveValue, @ConceptCodeMaternityLeaveValue, @ConceptIdMaternityLeaveValue, @IBCSENA, @SenaContributionValue, @ConceptCodeSenaContributionValue, @ConceptIdSenaContributionValue, @IBCICBF,
				@ICBFContributionValue, @ConceptCodeICBFContributionValue, @ConceptIdICBFContributionValue, @IBCCompensationFund,
				@FamilyCompensationFundContributionValue, @ConceptCodeFamilyCompensationFundContributionValue, @ConceptIdFamilyCompensationFundContributionValue, @RealBaseRetention,
				@CalculatedWithholdingValue, @ConceptCodeCalculatedWithholdingValue, @ConceptIdCalculatedWithholdingValue, @RecargoNocturno, @ConceptCodeRecargoNocturno, @ConceptIdRecargoNocturno,
				@SanctionDays, @Overtime, @ConceptCodeOvertime, @ConceptIdOvertime, @RecargoNocturnoFestivo, @ConceptCodeRecargoNocturnoFestivo, @ConceptIdRecargoNocturnoFestivo,
				@ValorDominicalOrdinario, @ConceptCodeValorDominical, @ConceptIdValorDominical
			continue

		End

		Close InfoItem
		Deallocate InfoItem
				
		--Se retorna la tabla
		select * from @TableXmlObject
		
	end try
	begin catch

		--rollback transaction
		select * from @TableXmlObject

	end catch

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de nómina que importa y valida un archivo Excel (enviado como XML) con los saldos iniciales de nómina de los empleados. Procesa cada registro del XML extrayendo datos clave del colaborador como NIT, días trabajados, total devengado, total deducido, aportes a pensión y salud (tanto del empleado como del empleador), provisiones de incentivos y vacaciones, entre otros conceptos de nómina. Cada campo incluye su valor, código de concepto e identificador de concepto, lo que permite cargar los saldos de apertura o migración de nómina con trazabilidad por concepto. Es utilizado típicamente en procesos de parametrización inicial o migración de datos de nómina cuando se integra un nuevo empleador o período de inicio en el sistema.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_ImportFileInitialBalancePayroll';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_ImportFileInitialBalancePayroll';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Valida fila por fila un lote XML de saldos iniciales de nómina (tercero, empleado, contrato, fecha según liquidación mensual/quincenal, valores numéricos y códigos de concepto), enriquece la tabla temporal con IDs y TotalPaid y la retorna.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ImportFileInitialBalancePayroll';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'@XmlObject debe contener nodos /Data/Row con los 53 campos esperados (CountFields, Nit, PayrollDate, DaysWorked, TotalAccrued, TotalDeducted, bases IBC y conceptos con Code/Id); Payroll.Concept debe contener los códigos referenciados por los conceptos cuando su valor monetario sea > 0; Common.ThirdParty debe contener el Nit del empleado y Payroll.Employee debe tener un registro asociado a ese ThirdPartyId; El empleado debe tener un Payroll.Contract con Valid=1 y Status=1 asociado a un Payroll.Group con Liquidation definida (1=mensual, otro=quincenal)', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ImportFileInitialBalancePayroll';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Cada fila procesada termina con StatusField=0 (con MessageField describiendo el error) o StatusField=1 con MessageField=''OK''; Solo cuando el valor monetario de un concepto es > 0 se exige y se resuelve su ConceptId contra Payroll.Concept; El NitName se normaliza al formato ''Nit - Name'' tomado de Common.ThirdParty; Solo se aceptan empleados con contrato vigente (Valid=1 y Status=1); No se permiten dos registros del mismo empleado en el mismo mes/año dentro del mismo lote; DaysWorked debe estar en el rango (0, 30]; TotalPaid se calcula siempre como TotalAccrued - TotalDeducted (decimal(18,0)) para los registros válidos; La estructura de cada fila del XML debe contener exactamente 53 campos (CountFields=53); El procedimiento nunca propaga excepción: ante error devuelve la tabla temporal tal como esté; El procedimiento no escribe en tablas físicas; solo valida y enriquece una tabla temporal y la retorna', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ImportFileInitialBalancePayroll';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Nómina; Empleado; Tercero (Nit); Contrato laboral; Tipo de contrato; Grupo de nómina; Liquidación mensual/quincenal; Concepto de nómina; IBC (base de cotización); Aporte a pensión empleado/patrono; Aporte a salud empleado/patrono; Provisión de primas; Provisión de vacaciones; Cesantías y intereses de cesantías; Incapacidad ambulatoria/hospitalaria; Licencia de maternidad; Aporte SENA; Aporte ICBF; Caja de compensación familiar; Retención en la fuente; Recargo nocturno; Recargo nocturno festivo; Valor dominical ordinario; Horas extras; Días de sanción; Días trabajados; Total devengado / deducido / pagado; Saldo inicial de nómina', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ImportFileInitialBalancePayroll';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si CountFields <> 53 → Marca el registro con StatusField=0 y mensaje ''no tiene la estructura requerida'' y salta al siguiente registro; si LEN(Nit)=0 o el Nit no existe en Common.ThirdParty o el tercero no existe como empleado en Payroll.Employee → Marca StatusField=0 con el mensaje correspondiente (cédula vacía / no existe como tercero / no existe como empleado) y salta al siguiente registro; si El empleado no tiene contrato con Valid=1 y Status=1 → Marca el registro como inválido con mensaje ''no tiene contrato asociado o está retirado''; si Payroll.Group.Liquidation = 1 (liquidación mensual) → Exige que la fecha de nómina sea el último día del mes (DAY(eomonth(PayrollDate))=DAY(PayrollDate)) else Para liquidación quincenal, exige que el día sea 15 o el último día del mes; si Existe en @TableXmlObject otro registro con el mismo EmployeeId y mismo MES y AÑO de PayrollDate → Marca el registro como duplicado: ''ya existe en la lista con el mismo mes y año''; si DaysWorked vacío, no numérico, negativo, =0 o >30 → Marca el registro inválido con el mensaje específico; si TotalAccrued vacío, no numérico o negativo → Marca el registro inválido; si Para cada concepto monetario (PensionContributionValue, EmployerPensionContributionValue, EmployeeHealthContributionValue, EmployerHealthContributionValue, ProvisionIncentive, ProvisionVacation, UnemploymentAccumulated, ProvisionInterestsUnemployment, AmbulatoryDisabilityValue, DisabilityHospitalValue, MaternityLeaveValue, SenaContributionValue, ICBFContributionValue, FamilyCompensationFundContributionValue, CalculatedWithholdingValue, RecargoNocturno, Overtime, RecargoNocturnoFestivo, ValorDominicalOrdinario): si el valor > 0 → Exige que el ConceptCode no esté vacío y exista en Payroll.Concept (Code), y resuelve el ConceptId vía SELECT Id FROM Payroll.Concept WHERE Code=@ConceptCode else Si el valor es vacío o 0, no se valida ni resuelve el concepto; si TRY exitoso al final del cursor → Devuelve SELECT * FROM @TableXmlObject con StatusField=1 y TotalPaid = TotalAccrued - TotalDeducted para los registros válidos else En CATCH, devuelve igualmente SELECT * FROM @TableXmlObject sin propagar error', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ImportFileInitialBalancePayroll';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Common.ThirdParty; Payroll.Employee; Payroll.Contract; Payroll.ContractType; Payroll.Group; Payroll.Concept', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ImportFileInitialBalancePayroll';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ImportFileInitialBalancePayroll';
-- GO
