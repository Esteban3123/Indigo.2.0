-- =============================================
-- Author:		Daniel Eduardo Arévalo
-- Create date: 19/11/2019
-- Description:	Procedimiento que se encarga de actualizar los datos del Archivo de Seguridad Social
-- =============================================
CREATE PROCEDURE [Payroll].[SP_SaveMassiveVerifyAutoliquidation] 
	@XmlObject as Xml,
	@CodeUser as varchar(20)
AS
BEGIN

	--Tabla que almacena los errores o aciertos de cada item que se recorre para poder devolverlos
	declare @TableReturn table(Id int IDENTITY PRIMARY KEY, CodeResult int, MessageResult varchar(max))

	--Tabla para almacenar los items del listado que viene en el xml
	declare @TableXmlObject table(Id int, Nit varchar(50), IBCOtrosParafiscales varchar(50), VSP varchar(1), FechaInicioVSP date, ValorVSP varchar(50), VST varchar(1), SLN varchar(1), FechaInicioSLN date,
									FechaFinalSLN date, BaseCalculoSLN varchar(50), IBCSLN varchar(50), IGE varchar(1), FechaInicioIGE date, FechaFinalIGE date, BaseCalculoIGE varchar(50), BaseIGE varchar(50), LMA varchar(1),
									FechaInicioLMA date, FechaFinLMA date, BaseCalculoLMA varchar(50), IBCLMA varchar(50), VAC varchar(1), FechaInicioVacaciones date, FechaFinVacaciones date, BaseCalculoVAC varchar(50),
									BaseLiqVAC varchar(50), IRL varchar(1), FechaInicioIRL date, FechaFinIRL date, BaseCalculoIRL varchar(50), BaseIRL varchar(50), DiasAFP varchar(2), DiasEPS varchar(2), DiasARP varchar(2), DiasCCF varchar(2),
									ValorVST varchar(50), IBCAFP varchar(50), IBCEPS varchar(50), IBCARP varchar(50), IBCCCF varchar(50), TarifaAFP VARCHAR(50), AporteAFP varchar(50), FSPSubcuentaSolidaridad varchar(50), FSPSubcuentaSubsistencia varchar(50),
									TarifaEPS VARCHAR(50), AporteEPS varchar(50), TarifaARP VARCHAR(50), AporteARP varchar(50), TarifaCCF VARCHAR(50), AporteCCF varchar(50), TarifaSENA VARCHAR(50), AporteSENA varchar(50), TarifaICBF VARCHAR(50), 
									AporteICBF varchar(50), TarifaEspecialPensiones varchar(50), Observaciones varchar(500))

	
	Begin try
	
		insert into @TableXmlObject
		select 
		t.x.value('Id[1]','int') as Id,		
		t.x.value('Nit[1]','varchar(50)') as Nit,
		t.x.value('IBCOtrosParafiscales[1]','varchar(50)') as IBCOtrosParafiscales,
		t.x.value('VSP[1]','varchar(1)') as VSP,
		t.x.value('FechaInicioVSP[1]','date') as FechaInicioVSP,
		case when t.x.value('ValorVSP[1]','varchar(50)') = '' then 0 else t.x.value('ValorVSP[1]','varchar(50)') end as ValorVSP,
		t.x.value('VST[1]','varchar(1)') as VST,
		t.x.value('SLN[1]','varchar(1)') as SLN,
		t.x.value('FechaInicioSLN[1]','date') as FechaInicioSLN ,
		t.x.value('FechaFinalSLN[1]','date') as FechaFinalSLN ,
		case when t.x.value('BaseCalculoSLN[1]','varchar(50)') = '' then 0 else t.x.value('BaseCalculoSLN[1]','varchar(50)') end as BaseCalculoSLN,
		case when t.x.value('IBCSLN[1]','varchar(50)') = '' then 0 else t.x.value('IBCSLN[1]','varchar(50)') end as IBCSLN,
		t.x.value('IGE[1]','varchar(1)') as IGE,
		t.x.value('FechaInicioIGE[1]','date') as FechaInicioIGE,
		t.x.value('FechaFinalIGE[1]','date') as FechaFinalIGE,
		case when t.x.value('BaseCalculoIGE[1]','varchar(50)') = '' then 0 else t.x.value('BaseCalculoIGE[1]','varchar(50)') end as BaseCalculoIGE,
		case when t.x.value('BaseIGE[1]','varchar(50)') = '' then 0 else t.x.value('BaseIGE[1]','varchar(50)') end as BaseIGE,
		t.x.value('LMA[1]','varchar(1)') as LMA,
		t.x.value('FechaInicioLMA[1]','date') as FechaInicioLMA,
		t.x.value('FechaFinLMA[1]','date') as FechaFinLMA,
		case when t.x.value('BaseCalculoLMA[1]','varchar(50)') = '' then 0 else t.x.value('BaseCalculoLMA[1]','varchar(50)') end as BaseCalculoLMA,
		case when t.x.value('IBCLMA[1]','varchar(50)') = '' then 0 else t.x.value('IBCLMA[1]','varchar(50)') end as IBCLMA,
		t.x.value('VAC[1]','varchar(1)') as VAC,
		t.x.value('FechaInicioVacaciones[1]','date') as FechaInicioVacaciones,
		t.x.value('FechaFinVacaciones[1]','date') as FechaFinVacaciones,
		case when t.x.value('BaseCalculoVAC[1]','varchar(50)') = '' then 0 else t.x.value('BaseCalculoVAC[1]','varchar(50)') end as BaseCalculoVAC,
		case when t.x.value('BaseLiqVAC[1]','varchar(50)') = '' then 0 else t.x.value('BaseLiqVAC[1]','varchar(50)') end as BaseLiqVAC,
		t.x.value('IRL[1]','varchar(1)') as IRL,
		t.x.value('FechaInicioIRL[1]','date') as FechaInicioIRL,
		t.x.value('FechaFinIRL[1]','date') as FechaFinIRL,
		case when t.x.value('BaseCalculoIRL[1]','varchar(50)') = '' then 0 else t.x.value('BaseCalculoIRL[1]','varchar(50)') end as BaseCalculoIRL,
		case when t.x.value('BaseIRL[1]','varchar(50)') = '' then 0 else t.x.value('BaseIRL[1]','varchar(50)') end as BaseIRL,
		t.x.value('DiasAFP[1]','varchar(2)') as DiasAFP,
		t.x.value('DiasEPS[1]','varchar(2)') as DiasEPS,
		t.x.value('DiasARP[1]','varchar(2)') as DiasARP,
		t.x.value('DiasCCF[1]','varchar(2)') as DiasCCF,
		case when t.x.value('ValorVST[1]','varchar(50)') = '' then 0 else t.x.value('ValorVST[1]','varchar(50)') end as ValorVST,
		case when t.x.value('IBCAFP[1]','varchar(50)') = '' then 0 else t.x.value('IBCAFP[1]','varchar(50)') end as IBCAFP,
		case when t.x.value('IBCEPS[1]','varchar(50)') = '' then 0 else t.x.value('IBCEPS[1]','varchar(50)') end as IBCEPS,
		case when t.x.value('IBCARP[1]','varchar(50)') = '' then 0 else t.x.value('IBCARP[1]','varchar(50)') end as IBCARP,
		case when t.x.value('IBCCCF[1]','varchar(50)') = '' then 0 else t.x.value('IBCCCF[1]','varchar(50)') end as IBCCCF,
		t.x.value('TarifaAFP[1]','varchar(50)') as TarifaAFP,
		case when t.x.value('AporteAFP[1]','varchar(50)') = '' then 0 else t.x.value('AporteAFP[1]','varchar(50)') end as AporteAFP,
		case when t.x.value('FSPSubcuentaSolidaridad[1]','varchar(50)') = '' then 0 else t.x.value('FSPSubcuentaSolidaridad[1]','varchar(50)') end as FSPSubcuentaSolidaridad,
		case when t.x.value('FSPSubcuentaSubsistencia[1]','varchar(50)') = '' then 0 else t.x.value('FSPSubcuentaSubsistencia[1]','varchar(50)') end as FSPSubcuentaSubsistencia,
		t.x.value('TarifaEPS[1]','varchar(50)') as TarifaEPS,
		case when t.x.value('AporteEPS[1]','varchar(50)') = '' then 0 else t.x.value('AporteEPS[1]','varchar(50)') end as AporteEPS,
		t.x.value('TarifaARP[1]','varchar(50)') as TarifaARP,
		case when t.x.value('AporteARP[1]','varchar(50)') = '' then 0 else t.x.value('AporteARP[1]','varchar(50)') end as AporteARP,
		t.x.value('TarifaCCF[1]','varchar(50)') as TarifaCCF,
		case when t.x.value('AporteCCF[1]','varchar(50)') = '' then 0 else t.x.value('AporteCCF[1]','varchar(50)') end as AporteCCF,
		t.x.value('TarifaSENA[1]','varchar(50)') as TarifaSENA,
		case when t.x.value('AporteSENA[1]','varchar(50)') = '' then 0 else t.x.value('AporteSENA[1]','varchar(50)') end as AporteSENA,
		t.x.value('TarifaICBF[1]','varchar(50)') as TarifaICBF,
		case when t.x.value('AporteICBF[1]','varchar(50)') = '' then 0 else t.x.value('AporteICBF[1]','varchar(50)') end as AporteICBF,
		case when t.x.value('TarifaEspecialPensiones[1]','varchar(50)') = '' then 0 else t.x.value('TarifaEspecialPensiones[1]','varchar(50)') end as TarifaEspecialPensiones,
		t.x.value('Observaciones[1]','varchar(500)') as Observaciones
		from @XmlObject.nodes('/Data/Row') t(x)
		
		
		--Id del registro
		declare @Id as int
		
		-- Nit		
		declare @Nit as varchar(50)

		--Otros Parafiscales
		declare @IBCOtrosParafiscales as varchar(50)

		--VSP
		declare @VSP as varchar(1)

		--FechaInicioVSP
		declare @FechaInicioVSP as DATE

		-- ValorVSP
		declare @ValorVSP as varchar(50)

		--VST
		declare @VST as varchar(1)

		-- SLN
		declare @SLN as varchar(1)

		-- FechaInicioSLN
		declare @FechaInicioSLN as date

		-- FechaFinalSLN
		declare @FechaFinalSLN as date

		-- BaseCalculoSLN
		declare @BaseCalculoSLN as varchar(50)

		-- IBCSLN
		declare @IBCSLN as varchar(50)

		-- IGE
		declare @IGE as varchar(1)

		-- FechaInicioIGE
		declare @FechaInicioIGE as date = NULL

		-- FechaFinalIGE
		declare @FechaFinalIGE as DATE

		-- BaseCalculoIGE
		declare @BaseCalculoIGE as varchar(50)

		-- BaseIGE
		declare @BaseIGE as varchar(50)

		-- LMA
		declare @LMA as varchar(1)

		-- FechaInicioLMA
		declare @FechaInicioLMA as date

		-- FechaFinLMA
		declare @FechaFinLMA as DATE

		-- BaseCalculoLMA
		declare @BaseCalculoLMA as varchar(50)

		-- IBCLMA
		declare @IBCLMA as varchar(50)

		-- VAC
		declare @VAC as varchar(1)

		-- FechaInicioVacaciones
		declare @FechaInicioVacaciones as DATE

		-- FechaFinVacaciones
		declare @FechaFinVacaciones as DATE

		-- BaseCalculoVAC
		declare @BaseCalculoVAC as varchar(50)

		-- BaseLiqVAC
		declare @BaseLiqVAC as varchar(50)

		-- IRL
		declare @IRL as varchar(1)

		-- FechaInicioIRL
		declare @FechaInicioIRL as DATE

		-- FechaFinIRL
		declare @FechaFinIRL as DATE

		-- BaseCalculoIRL
		declare @BaseCalculoIRL as varchar(50)

		-- BaseIRL
		declare @BaseIRL as varchar(50)

		--DiasAFP
		declare @DiasAFP as varchar(2)

		-- DiasEPS
		declare @DiasEPS as varchar(2)

		-- DiasARP
		declare @DiasARP as varchar(2)

		-- DiasCCF
		declare @DiasCCF as varchar(2)

		-- ValorVST
		declare @ValorVST as varchar(50)

		-- IBCAFP
		declare @IBCAFP as varchar(50)

		-- IBCEPS
		declare @IBCEPS as varchar(50)

		-- IBCARP
		declare @IBCARP as varchar(50)

		-- IBCCCF
		declare @IBCCCF as varchar(50)

		-- TarifaAFP
		declare @TarifaAFP as varchar(50)

		-- AporteAFP
		declare @AporteAFP as varchar(50)

		-- FSPSubcuentaSolidaridad
		declare @FSPSubcuentaSolidaridad as varchar(50)

		-- FSPSubcuentaSubsistencia
		declare @FSPSubcuentaSubsistencia as varchar(50)

		-- TarifaEPS
		declare @TarifaEPS as varchar(50)

		-- AporteEPS
		declare @AporteEPS as varchar(50)

		-- TarifaARP
		declare @TarifaARP as varchar(50)

		-- AporteARP
		declare @AporteARP as varchar(50)

		-- TarifaCCF
		declare @TarifaCCF as varchar(50)

		-- AporteCCF
		declare @AporteCCF as varchar(50)

		-- TarifaSENA
		declare @TarifaSENA as varchar(50)

		-- AporteSENA
		declare @AporteSENA as varchar(50)

		-- TarifaICBF
		declare @TarifaICBF as varchar(50)

		-- AporteICBF
		declare @AporteICBF as varchar(50)

		-- TarifaEspecialPensiones
		declare @TarifaEspecialPensiones as varchar(50)

		-- Observaciones
		declare @Observaciones as varchar(500)


		--Se recorre el cursor
		declare InfoItem Cursor For Select [Id], Nit, IBCOtrosParafiscales, VSP , FechaInicioVSP, ValorVSP, VST, SLN, FechaInicioSLN,
									FechaFinalSLN, BaseCalculoSLN , IBCSLN , IGE , FechaInicioIGE, FechaFinalIGE, BaseCalculoIGE , BaseIGE , LMA ,
									FechaInicioLMA , FechaFinLMA , BaseCalculoLMA , IBCLMA , VAC, FechaInicioVacaciones , FechaFinVacaciones , BaseCalculoVAC ,
									BaseLiqVAC , IRL , FechaInicioIRL , FechaFinIRL , BaseCalculoIRL , BaseIRL , DiasAFP , DiasEPS , DiasARP , DiasCCF ,
									ValorVST , IBCAFP , IBCEPS, IBCARP , IBCCCF , TarifaAFP, AporteAFP , FSPSubcuentaSolidaridad , FSPSubcuentaSubsistencia ,
									TarifaEPS , AporteEPS , TarifaARP , AporteARP , TarifaCCF , AporteCCF , TarifaSENA , AporteSENA , TarifaICBF , 
									AporteICBF , TarifaEspecialPensiones , Observaciones  From @TableXmlObject

		Open InfoItem

		Fetch Next From InfoItem Into @Id, @Nit, @IBCOtrosParafiscales, @VSP , @FechaInicioVSP, @ValorVSP, @VST, @SLN, @FechaInicioSLN,
									@FechaFinalSLN, @BaseCalculoSLN , @IBCSLN , @IGE , @FechaInicioIGE, @FechaFinalIGE, @BaseCalculoIGE , @BaseIGE , @LMA ,
									@FechaInicioLMA , @FechaFinLMA , @BaseCalculoLMA , @IBCLMA , @VAC, @FechaInicioVacaciones , @FechaFinVacaciones , @BaseCalculoVAC ,
									@BaseLiqVAC , @IRL , @FechaInicioIRL , @FechaFinIRL , @BaseCalculoIRL , @BaseIRL , @DiasAFP , @DiasEPS , @DiasARP , @DiasCCF ,
									@ValorVST , @IBCAFP , @IBCEPS, @IBCARP , @IBCCCF , @TarifaAFP, @AporteAFP , @FSPSubcuentaSolidaridad , @FSPSubcuentaSubsistencia ,
									@TarifaEPS , @AporteEPS , @TarifaARP , @AporteARP , @TarifaCCF , @AporteCCF , @TarifaSENA , @AporteSENA , @TarifaICBF , 
									@AporteICBF , @TarifaEspecialPensiones , @Observaciones

		While @@fetch_status = 0
		Begin
			
			IF (SELECT COUNT(*) FROM Payroll.VerifyAutoliquidationFile where Id = @Id and RegisterStatus = 1) > 0 BEGIN
				insert into @TableReturn(CodeResult, MessageResult) values(999, 'No se puede guardar el empleado con cédula ' + @Nit + ' porque el registro ya está confirmado')
				--Se pasa a la siguiente posicion del cursor
				GoTo NextFetch
			END

			--Se valida que los días de pensión sea mayor a cero
			if @DiasAFP < 0
			Begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				insert into @TableReturn(CodeResult, MessageResult) values(999, 'No se puede guardar el empleado con cédula ' + @Nit + ' porque los días de pensión son negativos')
				--Se pasa a la siguiente posicion del cursor
				GoTo NextFetch
			End

			--Se valida que los días de salud sea mayor a cero
			if @DiasEPS < 0
			Begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				insert into @TableReturn(CodeResult, MessageResult) values(999, 'No se puede guardar el empleado con cédula ' + @Nit + ' porque los días de salud son negativos')
				--Se pasa a la siguiente posicion del cursor
				GoTo NextFetch
			End

			--Se valida que los días de riesgo sea mayor a cero
			if @DiasARP < 0
			Begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				insert into @TableReturn(CodeResult, MessageResult) values(999, 'No se puede guardar el empleado con cédula ' + @Nit + ' porque los días de riesgo son negativos')
				--Se pasa a la siguiente posicion del cursor
				GoTo NextFetch
			End

			--Se valida que los días de caja sea mayor a cero
			if @DiasCCF < 0
			Begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				insert into @TableReturn(CodeResult, MessageResult) values(999, 'No se puede guardar el empleado con cédula ' + @Nit + ' porque los días de Caja son negativos')
				--Se pasa a la siguiente posicion del cursor
				GoTo NextFetch
			End

			if @FechaInicioVSP = '1900-01-01' BEGIN
				SET @FechaInicioVSP = NULL
			END

			IF @FechaInicioSLN = '1900-01-01' BEGIN
				SET @FechaInicioSLN = NULL
			END

			IF @FechaFinalSLN = '1900-01-01' BEGIN
				SET @FechaFinalSLN = NULL
			END
								
			IF @FechaInicioIGE = '1900-01-01' BEGIN
				SET @FechaInicioIGE = NULL
			END

			IF @FechaFinalIGE = '1900-01-01' BEGIN
				SET @FechaFinalIGE = NULL
			END

			IF @FechaInicioVacaciones = '1900-01-01' BEGIN
				SET @FechaInicioVacaciones = NULL
			END

			IF @FechaFinVacaciones = '1900-01-01' BEGIN
				SET @FechaFinVacaciones = NULL
			END

			IF @FechaInicioIRL = '1900-01-01' BEGIN
				SET @FechaInicioIRL = NULL
			END

			IF @FechaFinIRL = '1900-01-01' BEGIN
				SET @FechaFinIRL = NULL
			END

			 
			-- Hago el update de los campos
			UPDATE Payroll.VerifyAutoliquidationFile
			SET  IBCOtrosParafiscales = @IBCOtrosParafiscales,
				VSP = @VSP,
				VST = @VST,
				SLN = @SLN,
				SanctionInitialDate = @FechaInicioSLN,
				SanctionEndDate = @FechaInicioSLN,
				BaseLiquidacionSLN = @BaseCalculoSLN,
				IBCSLN = @IBCSLN,
				IGE = @IGE,
				AmbulatoryDisabilityInitialDate = @FechaInicioIGE,
				AmbulatoryDisabiltyEndDate = @FechaFinalIGE,
				BaseLiquidacionIGE = @BaseCalculoIGE,
				BaseIGE = @BaseIGE,
				LMA = @LMA,
				MaternityLeaveInitialDate = @FechaInicioLMA,
				MaternityLeaveEndDate = @FechaFinLMA,
				BaseLiquidacionLMA = @BaseCalculoLMA,
				BaseLMA = @IBCLMA,
				VAC = @VAC,
				VacationInitialDate = @FechaInicioVacaciones,
				VacationEndDate = @FechaFinVacaciones,
				BaseLiquidacionVAC = @BaseCalculoVAC,
				BaseVAC = @BaseLiqVAC,
				IRL = @IRL,
				FechaInicioIRL = @FechaInicioIRL,
				FechaFinIRL = @FechaFinIRL,
				BaseLiquidacionIRL = @BaseCalculoIRL,
				BaseIRL = @BaseIRL,
				PensionDays = @DiasAFP,
				HealthDays = @DiasEPS,
				ProfessionalRiskDays = @DiasARP,
				CompensationFundDays = @DiasCCF,
				VSTValue = @ValorVST,
				IBCPension = @IBCAFP,
				IBCHealth = @IBCEPS,
				IBCProfessionalRisk = @IBCARP,
				IBCCompensationFund = @IBCCCF,
				RateContributionPension = @TarifaAFP,
				ValuePension = @AporteAFP,
				RateContributorCCF = @TarifaCCF,
				ValueContributionCCF = @AporteCCF,
				RateContributorSENA = @TarifaSENA,
				ValueSena = @AporteSENA,
				RateContributionICBF = @TarifaICBF,
				ValueICBF = @AporteICBF,
				TarifaEspecialPensiones = @TarifaEspecialPensiones,
				Observations = @Observaciones
			WHERE Id = @Id
			

			--Se agrega a la tabla que retorno el ok
			insert into @TableReturn(CodeResult, MessageResult) values(0, 'El empleado con cédula ' + @Nit + ' se guardó correctamente')

			NextFetch:
			--Se pasa a la siguiente posicion del cursor
			Fetch Next From InfoItem Into @Id, @Nit, @IBCOtrosParafiscales, @VSP , @FechaInicioVSP, @ValorVSP, @VST, @SLN, @FechaInicioSLN,
									@FechaFinalSLN, @BaseCalculoSLN , @IBCSLN , @IGE , @FechaInicioIGE, @FechaFinalIGE, @BaseCalculoIGE , @BaseIGE , @LMA ,
									@FechaInicioLMA , @FechaFinLMA , @BaseCalculoLMA , @IBCLMA , @VAC, @FechaInicioVacaciones , @FechaFinVacaciones , @BaseCalculoVAC ,
									@BaseLiqVAC , @IRL , @FechaInicioIRL , @FechaFinIRL , @BaseCalculoIRL , @BaseIRL , @DiasAFP , @DiasEPS , @DiasARP , @DiasCCF ,
									@ValorVST , @IBCAFP , @IBCEPS, @IBCARP , @IBCCCF , @TarifaAFP, @AporteAFP , @FSPSubcuentaSolidaridad , @FSPSubcuentaSubsistencia ,
									@TarifaEPS , @AporteEPS , @TarifaARP , @AporteARP , @TarifaCCF , @AporteCCF , @TarifaSENA , @AporteSENA , @TarifaICBF , 
									@AporteICBF , @TarifaEspecialPensiones , @Observaciones

			continue

		End

		Close InfoItem
		Deallocate InfoItem

		select * from @TableReturn
		
	end try
	begin catch
		delete from @TableReturn
		insert into @TableReturn(CodeResult, MessageResult) values(888, CAST(ERROR_MESSAGE() as varchar(500)))
		select * from @TableReturn
	end catch

END