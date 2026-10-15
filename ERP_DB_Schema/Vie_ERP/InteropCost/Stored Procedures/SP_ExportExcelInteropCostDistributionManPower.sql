
-- ==================================================================================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 18/07/2016
-- Description:	Procedimiento que se encarga de obtener los valores para exportarlos a excel
-- Modified by:	Nicolas Pulido
-- Modified date: 15/03/2017
-- Modified Description: Se eliminan y se agregan unos nuevos campos, para exportarlos a excel
-- ==================================================================================================
CREATE PROCEDURE [InteropCost].[SP_ExportExcelInteropCostDistributionManPower] 
	@ListIdsXml as Xml,
	@Year as int,
	@Month as int,
	@OperatingUnitId int,
	@CodeUser as varchar(20)
AS
BEGIN
	
	-- si el mes tiene un solo digito, le agrego un cero antes del numero para realizar la consulta correctamente
	if (LEN(@Month)='1') begin
		
		declare @RealMonth as varchar(2) = '0' + cast(@Month as varchar(2))
		
	end

	--Tabla temporal para obtener los ids del xml(Id de la liquidación y Id del empleado)
	declare @TableIds table(LiquidationId int, EmployeeId int)

	
	--Tabla de la cabecera para devolver al form y exportar a excel
	declare @TableHeader table(
	[Id] INT IDENTITY(1,1), 
	[EmployeeId] int, 
	[EmployeeName] varchar(max), 
	GroupName varchar(max), 
	EmployeeTypeName varchar(max), 
	EmployeeDateLiquidated date, 
	FunctionalUnitName varchar(max), 
	CostCenterCode int, 
	CostCenterName varchar(max), 
	BasicSalary Numeric(18,0), 
	ConceptLiquidation varchar(max), 
	[HoursWorked] int, 
	ConceptValue numeric(18,0), 
	HealthFoundNit int, 
	HealthFoundName varchar(max), 
	PensionFoundNit int, 
	PensionFoundName varchar(max), 
	SenaContributionValue numeric(18,0), 
	FamilyCompensationFoundContributionValue numeric(18,0), 
	ICBFContributionValue numeric(18,0), 
	ParafiscalContributionValue numeric(18,0), 
	OccupationalRisksContributionValue numeric(18,0), 
	EmployerHealthContributionValue numeric(18,0), 
	EmployerPensionContributionValue numeric(18,0), 
	EmployerTotalValue numeric(18,0), 
	EmployeeHealthContribution numeric(18,0), 
	EmployeePensionContribution numeric(18,0), 
	TotalEmployeeSocialSecurityContributionValue numeric(18,0), 
	UnemploymentAccumulated numeric(18,0), 
	ProvisionInterestsUnemployment numeric(18,0), 
	ProvisionVacation numeric(18,0),
	VacationBonus numeric(18,0),
	JuneBonus numeric(18,0),
	ChristmasBonus numeric(18,0),
	YearServiceBonus numeric(18,0),
	SpecialRecreationBonus numeric(18,0),
	TotalSocialBenefits numeric(18,0),
	[CreationUser] varchar(20), 
	[CreationDate] datetime)


	--Tabla de los detalles para devolver al form y exportar a excel
	declare @TableDetail table([ParentId] int, [ProductionCenterDescription] varchar(max), [HoursQuantity] int)

	Begin try
		
		--Se obtienen los Ids y se insertan en la tabla temporal
		insert into @TableIds
		select 
		t.x.value('LiquidationId[1]','int') as LiquidationId,
		t.x.value('EmployeeId[1]','int') as EmployeeId
		from @ListIdsXml.nodes('/Ids') t(x)
		
		--Se recorre la tabla de ids para realizar las operaciones
		Declare @LiquidationId int
		Declare @EmployeeId int
		Declare InfoItem Cursor For Select LiquidationId, EmployeeId From @TableIds
		
		--Horas trabajadas
		declare @HoursWorked int = 0

		--Total devengado
		declare @TotalAccrued decimal = 0
		--Total provisiones
		declare @TotalProvision decimal = 0
		--Total aportes patronales
		declare @TotalEmployerContribution decimal = 0
		--Total parafiscales
		declare @TotalParafiscal decimal = 0
		--Permite saver si maneja cuadro de turno
		declare @HandlesTurnsChart bit
		--Nit del empleado
		declare @EmployeeNit varchar(max)
		--Id de la cabecera
		declare @Id int = 1
		
		--Fecha para sacar el nombre del mes
		--declare @DateMonthName datetime = cast('01/'+ @Month + '/'+ @Year as datetime)
		
		
		
		-- Tabla temporal unidades funcionales
		declare @tmpFunctionalUnit table(tmpId int identity(1,1), tmpIdFunctionalUnit int)

		Open InfoItem
		Fetch Next From InfoItem Into @LiquidationId, @EmployeeId
		
		While @@fetch_status = 0
		Begin

			--Fecha de liquidación
			declare @DateLiquidated date 
			-- Nombre del empleado
			declare @EmployeeName varchar(max)
			-- Grupo de la Liquidación
			declare @GroupName varchar(max)
			-- Tipo de Empleado
			declare @EmployeeTypeName varchar(max)
			-- Nombre de la Unidad Funcional
			declare @FunctionalUnitName varchar(max)
			-- Código del Centro de Costo
			declare @CostCenterCode int
			-- Nombre del Centro de Costo
			declare @CostCenterName varchar(max)
			-- Valor del Salario Básico del Empleado en la Liquidación
			declare @BasicSalary decimal = 0
			-- Concepto de la Liquidación
			declare @ConceptLiquidation varchar(max)
			-- Valor del Concepto
			declare @ConceptValue decimal = 0
			-- Nit del fondo de salud
			declare @HealthFoundNit int
			-- Nombre del fondo de salud
			declare @HealthFoundName varchar(max)
			-- Nit del fondo de salud
			declare @PensionFoundNit int
			-- Nombre del fondo de salud
			declare @PensionFoundName varchar(max)
			-- Aporte SENA
			declare @SenaContributionValue decimal = 0
			-- Aporte caja de compensación
			declare @FamilyCompensationFoundContributionValue decimal = 0
			-- Aporte ICBF
			declare @ICBFContributionValue decimal = 0
			-- Total parafiscales
			declare @ParafiscalContributionValue decimal = 0
			-- Total ARL
			declare @OccupationalRisksContributionValue decimal = 0
			-- Aporte salud patrono
			declare @EmployerHealthContributionValue decimal = 0
			-- Aporte pensión patrono
			declare @EmployerPensionContributionValue decimal = 0
			-- Total aportes patronales
			declare @EmployerTotalValue decimal = 0
			-- Aporte salud empleado
			declare @EmployeeHealthContribution decimal = 0
			-- Aporte pensión empleado
			declare @EmployeePensionContribution decimal = 0
			-- Total aportes seguridad social empleados
			declare @TotalEmployeeSocialSecurityContributionValue decimal = 0
			-- Provisión cesantías
			declare @UnemploymentAccumulated decimal = 0
			-- Provisión intereses cesantías
			declare @ProvisionInterestsUnemployment decimal = 0
			-- Provisión vacaciones
			declare @ProvisionVacation decimal = 0
			-- Prima de vacaciones
			declare @VacationBonus decimal = 0
			-- Prima de junio
			declare @JuneBonus decimal = 0
			-- Prima de navidad
			declare @ChristmasBonus decimal = 0
			-- Bonificación por año de servicio
			declare @YearServiceBonus decimal = 0
			-- Bonificación especial de recreación
			declare @SpecialRecreationBonus decimal = 0
			-- Total prestaciones sociales
			declare @TotalSocialBenefits decimal = 0
			--- Id unidad funcional
			declare @FunctionalUnitId int

			DECLARE @ContractHour int
			
			
			--Se valida que el empleado no haya sido guardado en la distribucion de mano de obra con el mes y el año previamente
			if (select COUNT(*) from InteropCost.DistributionManpower where [EmployeeId] = @EmployeeId and [Year] = @Year and [Month] = @Month) > 0
			Begin
				--Se pasa a la siguiente posicion del cursor
				Fetch Next From InfoItem Into @LiquidationId, @EmployeeId
				continue
			End
	
			--Consultamos la liquidación de cada empleado para sacar algunos valores

			select 
			@DateLiquidated = L.PayrollDateLiquidated,
			@TotalAccrued = l.TotalAccrued,
			@TotalProvision = l.ProvisionsValue,
			@TotalParafiscal = l.ParafiscalContribution,
			@HandlesTurnsChart = p.HandlesTurnsChart,
			@SenaContributionValue = l.SenaContributionValue,
			@FamilyCompensationFoundContributionValue = l.FamilyCompensationFundContributionValue,
			@ICBFContributionValue = l.ICBFContributionValue,
			@ParafiscalContributionValue = l.ParafiscalContribution,
			@OccupationalRisksContributionValue = l.OccupationalRisksContributionValue,
			@EmployerHealthContributionValue = l.EmployerHealthContributionValue, 
			@EmployerPensionContributionValue = l.EmployerPensionContributionValue,
			@EmployerTotalValue = @EmployerHealthContributionValue + @EmployerPensionContributionValue,
			@EmployeeHealthContribution = l.EmployeeHealthContributionValue,
			@EmployeePensionContribution = l.PensionContributionValue,
			@TotalEmployeeSocialSecurityContributionValue = @EmployeeHealthContribution + @EmployeePensionContribution,
			@UnemploymentAccumulated = l.UnemploymentAccumulated,
			@ProvisionInterestsUnemployment = l.ProvisionInterestsUnemployment, 
			@ContractHour = C.HoursDaily			
			from Payroll.Liquidation l
			inner join Payroll.[Contract] c on c.Id = l.ContractId
			inner join Payroll.Position p on p.Id = c.PositionId
			inner join payroll.[Group] g on g.Id = l.GroupId
			inner join Payroll.CostCenter cc on cc.Id = l.CostCenterId
			where l.Id = @LiquidationId

			-- VACACIONES (ConceptClass = 030)
			select @ProvisionVacation = ld.ConceptTotalValue
			from Payroll.LiquidationDetail ld 
			inner join Payroll.Liquidation l on l.Id = ld.PayrollId
			where l.Id = @LiquidationId and ld.ConceptClass = '030'

			-- Prima de Vacaciones (ConceptClass = 049)
			select @VacationBonus = ld.ConceptTotalValue
			from Payroll.LiquidationDetail ld 
			inner join Payroll.Liquidation l on l.Id = ld.PayrollId
			where l.Id = @LiquidationId and ld.ConceptClass = '049'

			-- Bonificacion por Año de Servicio (ConceptClass = 046)
			select @YearServiceBonus = ld.ConceptTotalValue
			from Payroll.LiquidationDetail ld 
			inner join Payroll.Liquidation l on l.Id = ld.PayrollId
			where l.Id = @LiquidationId and ld.ConceptClass = '046'

			-- Bonificacion por Año de Servicio (ConceptClass = 046)
			select @SpecialRecreationBonus = ld.ConceptTotalValue
			from Payroll.LiquidationDetail ld 
			inner join Payroll.Liquidation l on l.Id = ld.PayrollId
			where l.Id = @LiquidationId and ld.ConceptCode = '097'
			
			-- PRIMAS DE JUNIO
			SELECT @JuneBonus = IP.TotalAccrued 
			FROM Payroll.IncentivePayment IP, Payroll.[Contract] C
			WHERE IP.ContractId = C.Id and C.EmployeeId = @EmployeeId
			AND IP.RegisterStatus = 2 AND IP.[Period] = 1 AND YEAR(IP.PeriodEndDate) = YEAR(@DateLiquidated)

			-- PRIMAS DE DICIEMBRE
			SELECT @ChristmasBonus = IP.TotalAccrued 
			FROM Payroll.IncentivePayment IP, Payroll.[Contract] C
			WHERE IP.ContractId = C.Id and C.EmployeeId = @EmployeeId
			AND IP.RegisterStatus = 2 AND IP.[Period] = 2 AND YEAR(IP.PeriodEndDate) = YEAR(@DateLiquidated)

		
			-- Total Horas al Mes
			DECLARE @TotalMonthlyHour int = @ContractHour * 30

			-- Se consulta el total de las prestaciones sociales
			set @TotalSocialBenefits = @UnemploymentAccumulated + @ProvisionInterestsUnemployment + @ProvisionVacation + @VacationBonus + @JuneBonus + @ChristmasBonus + @YearServiceBonus + @SpecialRecreationBonus

			--Se consultan los patronales
			select @TotalEmployerContribution = SUM(ld.DeductedValue)			
			from Payroll.Liquidation l
			inner join Payroll.LiquidationDetail ld on ld.PayrollId = l.Id
			inner join Payroll.Concept c on ld.ConceptId = c.Id
			where l.EmployeeId = @EmployeeId and YEAR(l.PayrollDateLiquidated) = @Year and MONTH(l.PayrollDateLiquidated) = @Month
			and l.RegisterStatus = 'C' and c.ConceptType = 3

			-- Se consulta el nit y nombre del fondo de pensión
			select @PensionFoundNit = tp.Nit,
			@PensionFoundName = tp.[Name]
			from Payroll.Liquidation l
			inner join Payroll.LiquidationDetail ld on ld.PayrollId = l.Id
			inner join Payroll.Concept c on ld.ConceptId = c.Id
			inner join Payroll.Fund f on f.Id = l.PensionFundId
			inner join Common.ThirdParty tp on tp.Id = f.ThirdPartyId
			where l.EmployeeId = @EmployeeId and YEAR(l.PayrollDateLiquidated) = @Year and MONTH(l.PayrollDateLiquidated) = @Month
			and l.RegisterStatus = 'C' and c.ConceptType = 3

			-- Se consulta el nit y nombre del fondo de salud
			select @HealthFoundNit = tp.Nit,
			@HealthFoundName = tp.[Name]
			from Payroll.Liquidation l
			inner join Payroll.LiquidationDetail ld on ld.PayrollId = l.Id
			inner join Payroll.Concept c on ld.ConceptId = c.Id
			inner join Payroll.Fund f on f.Id = l.HealthFundId
			inner join Common.ThirdParty tp on tp.Id = f.ThirdPartyId
			where l.EmployeeId = @EmployeeId and YEAR(l.PayrollDateLiquidated) = @Year and MONTH(l.PayrollDateLiquidated) = @Month
			and l.RegisterStatus = 'C' and c.ConceptType = 3

			if @TotalEmployerContribution is null
			Begin
				set @TotalEmployerContribution = 0
			End
			
			--Se busca en schedule(cuadro de turno)
			declare @Period varchar(max) = cast(@RealMonth as varchar(max))+'/'+cast(@Year as varchar(max))
			
			--Se genera la descripción de la cabecera del empleado
			select @EmployeeNit = t.Nit, @EmployeeName = t.[Name], @EmployeeTypeName = et.[Name]
			from Payroll.Employee e
			inner join Common.ThirdParty t on t.Id = e.ThirdPartyId
			inner join Payroll.EmployeeType et on et.Id = e.EmployeeTypeId
			where e.Id = @EmployeeId

			--print @FunctionalUnitsConsecutive
			
			-- se obtienen las unidades funcionales que tiene el empleado en el periodo a liquidar
			/*
			select @FunctionalUnitId = sd.ScheduleFunctionalUnitId
			from Payroll.ScheduleDetail sd
			inner join Payroll.FunctionalUnit fu on fu.Id = sd.ScheduleFunctionalUnitId 
			inner join Payroll.Schedule s on s.FunctionalUnitId = sd.ScheduleFunctionalUnitId 
			where sd.EmployeeId = @EmployeeId and DATEPART(YYYY,sd.DateDetail) = @Year and DATEPART(MM,sd.DateDetail) = @RealMonth
			group by sd.ScheduleFunctionalUnitId*/
			
			-- se limpian los datos de la tabla temporal de unidades funcionales @tmpFunctionalUnit
			delete from @tmpFunctionalUnit
			
			-- se guardan las unidades funcionales obtenidas de una consulta en la tabla temporal @tmpFunctionalUnit

			declare @CantidadUnidadFuncional int = 0
			SELECT @CantidadUnidadFuncional = COUNT(sd.ScheduleFunctionalUnitId) from Payroll.ScheduleDetail sd where sd.EmployeeId = @EmployeeId and DATEPART(YYYY,sd.DateDetail) = @Year and DATEPART(MM,sd.DateDetail) = @RealMonth AND SD.TotalNumberHours > 0
			group by sd.ScheduleFunctionalUnitId

			IF @CantidadUnidadFuncional > 0 BEGIN
				-- El empleado tuvo cuadro de turnos
				insert into @tmpFunctionalUnit(tmpIdFunctionalUnit)
				select sd.ScheduleFunctionalUnitId from Payroll.ScheduleDetail sd where sd.EmployeeId = @EmployeeId and DATEPART(YYYY,sd.DateDetail) = @Year and DATEPART(MM,sd.DateDetail) = @RealMonth
				group by sd.ScheduleFunctionalUnitId

			END ELSE BEGIN
				insert into @tmpFunctionalUnit(tmpIdFunctionalUnit)
				SELECT C.FunctionalUnitId FROM Payroll.Liquidation L, Payroll.[Contract] C WHERE L.ContractId = C.Id and L.Id = @LiquidationId
			END

			-- se declara la variable que va a guardar los ids de las unidades funcionales asignados en el cursor de la tabla temporal @tmpFunctionalUnit
			declare @IdFunctionalUnitCursor int

			-- se declara el cursor functionalunit_cursor que va a contener los ids de las unidades funcionales(@tmpFunctionalUnit)
			DECLARE functionalunit_cursor CURSOR FOR   
			select tmpIdFunctionalUnit from @tmpFunctionalUnit

			OPEN functionalunit_cursor  
			FETCH NEXT FROM functionalunit_cursor   
			INTO @IdFunctionalUnitCursor

			WHILE @@FETCH_STATUS = 0  
			BEGIN   

				-- Creo cursor para recorrer el detalle de la Liquidación
					
					-- se asigna la unidad funcional del cursor a la variable @FunctionalUnitId
					SET @FunctionalUnitId = @IdFunctionalUnitCursor

					DECLARE @ConceptId int
					DECLARE @ConceptTotalValue numeric(18,0)

					DECLARE DetalleLiquidacion_cursor CURSOR FOR   
					SELECT ConceptId, ConceptTotalValue FROM Payroll.LiquidationDetail WHERE PayrollId = @LiquidationId

					OPEN DetalleLiquidacion_cursor  
					FETCH NEXT FROM DetalleLiquidacion_cursor   
					INTO @ConceptId, @ConceptTotalValue

					WHILE @@FETCH_STATUS = 0  
					BEGIN   

						DECLARE @ConceptRealTotalValue DECIMAL = @ConceptTotalValue
						DECLARE @RealProvisionVacation DECIMAL = @ProvisionVacation
						declare @RealSenaContributionValue decimal = @SenaContributionValue
						declare @RealFamilyCompensationFoundContributionValue decimal = @FamilyCompensationFoundContributionValue
						declare @RealICBFContributionValue decimal = @ICBFContributionValue
						declare @RealParafiscalContributionValue decimal = @ParafiscalContributionValue
						declare @RealOccupationalRisksContributionValue decimal = @OccupationalRisksContributionValue
						declare @RealEmployerHealthContributionValue decimal = @EmployerHealthContributionValue
						declare @RealEmployerPensionContributionValue decimal = @EmployerPensionContributionValue
						declare @RealEmployerTotalValue decimal = @EmployerTotalValue
						declare @RealEmployeeHealthContribution DECIMAL = @EmployeeHealthContribution
						declare @RealEmployeePensionContribution decimal = @EmployeePensionContribution
						declare @RealTotalEmployeeSocialSecurityContributionValue decimal = @TotalEmployeeSocialSecurityContributionValue
						declare @RealUnemploymentAccumulated decimal = @UnemploymentAccumulated
						declare @RealProvisionInterestsUnemployment decimal = @ProvisionInterestsUnemployment
						DECLARE @RealVacationBonus DECIMAL = @VacationBonus
						DECLARE @RealJuneBonus decimal = @JuneBonus
						DECLARE @RealChristmasBonus decimal = @ChristmasBonus
						DECLARE @RealTotalSocialBenefits DECIMAL = @TotalSocialBenefits

						DECLARE @RealTotalSpecialRecreationBonus DECIMAL = @SpecialRecreationBonus
						DECLARE @RealTotalYearServiceBonus DECIMAL = @YearServiceBonus

		
						if @HandlesTurnsChart = 1 --Si maneja cuadro de turno
						Begin
				
							if (select COUNT(*) from Payroll.Schedule where EmployeeId = @EmployeeId and [Period] = @Period and TotalHour > 0) > 0 --Si encuentra turnos para ese periodo
							Begin
					
								select @TotalMonthlyHour = SUM(s.TotalHour)
								from Payroll.Schedule s
								where s.EmployeeId = @EmployeeId and s.[Period] = @Period

								--Se asigna las horas trabajadas
					
								select @HoursWorked = s.TotalHour, @FunctionalUnitName = fu.[Name]
								from Payroll.Schedule s
								inner join Payroll.FunctionalUnit fu on fu.Id = s.FunctionalUnitId
								where s.EmployeeId = @EmployeeId and s.[Period] = @Period and s.FunctionalUnitId = @FunctionalUnitId
						
								--print @HoursWorked

								--Se inserta en la tabla temporal del detalle de distribucion de mano de obra
								insert into @TableDetail(ParentId, ProductionCenterDescription, HoursQuantity)
								select @Id, fu.Code+ ' - '+ fu.Name, s.TotalHour 
								from Payroll.Schedule s
								inner join Payroll.FunctionalUnit fu on fu.Id = s.FunctionalUnitId
								where s.EmployeeId = @EmployeeId and s.Period = @Period and fu.ProductionCenterId is not null

								IF @HoursWorked > 0 BEGIN
									SET @ConceptRealTotalValue = (@ConceptTotalValue * @HoursWorked) / @TotalMonthlyHour
									SET @RealProvisionVacation = (@ProvisionVacation * @HoursWorked) / @TotalMonthlyHour 
									SET @RealSenaContributionValue = (@SenaContributionValue * @HoursWorked) / @TotalMonthlyHour 
									SET @RealFamilyCompensationFoundContributionValue = (@FamilyCompensationFoundContributionValue * @HoursWorked) / @TotalMonthlyHour 
									SET @RealICBFContributionValue = (@ICBFContributionValue * @HoursWorked) / @TotalMonthlyHour 
									SET @RealParafiscalContributionValue = (@ParafiscalContributionValue * @HoursWorked) / @TotalMonthlyHour 
									SET @RealOccupationalRisksContributionValue = (@OccupationalRisksContributionValue * @HoursWorked) / @TotalMonthlyHour 
									SET @RealEmployerHealthContributionValue = (@EmployerHealthContributionValue * @HoursWorked) / @TotalMonthlyHour 
									SET @RealEmployerPensionContributionValue = (@EmployerPensionContributionValue * @HoursWorked) / @TotalMonthlyHour 
									SET @RealEmployerTotalValue = (@EmployerTotalValue * @HoursWorked) / @TotalMonthlyHour 
									SET @RealEmployeeHealthContribution = (@EmployeeHealthContribution * @HoursWorked) / @TotalMonthlyHour 
									SET @RealEmployeePensionContribution = (@EmployeePensionContribution * @HoursWorked) / @TotalMonthlyHour 
									SET @RealTotalEmployeeSocialSecurityContributionValue = (@TotalEmployeeSocialSecurityContributionValue * @HoursWorked) / @TotalMonthlyHour 
									SET @RealUnemploymentAccumulated = (@UnemploymentAccumulated * @HoursWorked) / @TotalMonthlyHour 
									SET @RealProvisionInterestsUnemployment = (@ProvisionInterestsUnemployment * @HoursWorked) / @TotalMonthlyHour 
									SET @RealVacationBonus = (@VacationBonus * @HoursWorked) / @TotalMonthlyHour 
									SET @RealJuneBonus = (@JuneBonus * @HoursWorked) / @TotalMonthlyHour 
									SET @RealChristmasBonus = (@ChristmasBonus * @HoursWorked) / @TotalMonthlyHour 
									SET @RealTotalSocialBenefits = (@TotalSocialBenefits * @HoursWorked) / @TotalMonthlyHour 
									SET @RealTotalSpecialRecreationBonus = (@SpecialRecreationBonus * @HoursWorked) / @TotalMonthlyHour 
									SET @RealTotalYearServiceBonus = (@YearServiceBonus * @HoursWorked) / @TotalMonthlyHour 

								END ELSE BEGIN
									SET @ConceptRealTotalValue = 0
									SET @RealProvisionVacation = 0
									SET @RealSenaContributionValue = 0
									SET @RealFamilyCompensationFoundContributionValue = 0
									SET @RealICBFContributionValue = 0
									SET @RealParafiscalContributionValue = 0
									SET @RealOccupationalRisksContributionValue = 0
									SET @RealEmployerHealthContributionValue = 0
									SET @RealEmployerPensionContributionValue = 0
									SET @RealEmployerTotalValue = 0
									SET @RealEmployeeHealthContribution = 0
									SET @RealEmployeePensionContribution = 0
									SET @RealTotalEmployeeSocialSecurityContributionValue = 0
									SET @RealUnemploymentAccumulated = 0
									SET @RealProvisionInterestsUnemployment = 0
									SET @RealVacationBonus = 0
									SET @RealJuneBonus = 0
									SET @RealChristmasBonus = 0
									SET @RealTotalSocialBenefits = 0
									SET @RealTotalSpecialRecreationBonus = 0
									SET @RealTotalYearServiceBonus = 0
								
									
								END
					
							End
							Else --Si no encuentra turnos para ese periodo
							Begin
				
								--Se inserta en la tabla temporal
								insert into @TableDetail(ParentId, ProductionCenterDescription, HoursQuantity)
								select @Id,fu.Code+ ' - '+ fu.Name, l.DaysWorked * c.HoursDaily
								from Payroll.Liquidation l
								inner join Payroll.Contract c on c.Id = l.ContractId
								inner join Payroll.FunctionalUnit fu on fu.Id = c.FunctionalUnitId
								where l.Id = @LiquidationId
						
							End
				

						End
						Else --Si no maneja cuadro de turno
						Begin
							--Se inserta en la tabla temporal
							insert into @TableDetail(ParentId, ProductionCenterDescription, HoursQuantity)
							select @Id,fu.Code + ' - '+ fu.Name, l.DaysWorked * c.HoursDaily
							from Payroll.Liquidation l
							inner join Payroll.Contract c on c.Id = l.ContractId
							inner join Payroll.FunctionalUnit fu on fu.Id = c.FunctionalUnitId
							where l.Id = @LiquidationId
					
						End


						-- Se revisa el Concepto y los datos correspondientes
						-- Cargo el Concepto
						DECLARE @NombreConcepto as varchar(200)
						DECLARE @CodigoConcepto as varchar(5)

						SELECT @CodigoConcepto = Code, @NombreConcepto = [Name] 
						FROM Payroll.Concept 
						WHERE Id = @ConceptId

						-- Cargo los Datos de las Unidades Funcionales ylos Centros de Costo
						DECLARE @NombreUnidadFuncional as varchar(200)
						DECLARE @CodigoUnidadFuncional as varchar(20)
						DECLARE @NombreCentroCosto varchar(200)
						DECLARE @CodigoCentroCosto varchar(20)

						SELECT @CodigoUnidadFuncional = FU.Code, @NombreUnidadFuncional = FU.[Name], @NombreCentroCosto = CC.[Name], @CodigoCentroCosto = CC.Code
						FROM Payroll.FunctionalUnit FU, Payroll.CostCenter CC
						WHERE FU.Id = @IdFunctionalUnitCursor AND FU.CostCenterId = CC.Id

						INSERT INTO @TableHeader
						SELECT 
						l.EmployeeId,
						@EmployeeNit + ' - ' + @EmployeeName,
						g.[Name],
						@EmployeeTypeName,
						l.PayrollDateLiquidated,
						@CodigoUnidadFuncional + ' - ' + @NombreUnidadFuncional,
						@CodigoCentroCosto,
						@NombreCentroCosto,
						c.BasicSalary,
						@CodigoConcepto + ' - ' + @NombreConcepto,
						@HoursWorked,
						@ConceptRealTotalValue,
						@HealthFoundNit,
						@HealthFoundName,
						@PensionFoundNit,
						@PensionFoundName,
						@RealSenaContributionValue,
						@RealFamilyCompensationFoundContributionValue,
						@RealICBFContributionValue,
						@RealParafiscalContributionValue,
						@RealOccupationalRisksContributionValue,
						@RealEmployerHealthContributionValue, 
						@RealEmployerPensionContributionValue,
						@RealEmployerHealthContributionValue + @RealEmployerPensionContributionValue,
						@RealEmployeeHealthContribution,
						@RealEmployeePensionContribution,
						@RealEmployeeHealthContribution + @RealEmployeePensionContribution,
						@RealUnemploymentAccumulated,
						@RealProvisionInterestsUnemployment, 
						@RealProvisionVacation,
						@RealVacationBonus,
						@RealJuneBonus,
						@RealChristmasBonus,
						@RealTotalYearServiceBonus,
						@RealTotalSpecialRecreationBonus,
						@RealTotalSocialBenefits,
						@CodeUser,
						[Common].[GETDATE]()			
						from Payroll.Liquidation l
						inner join Payroll.[Contract] c on c.Id = l.ContractId
						inner join Payroll.Position p on p.Id = c.PositionId
						inner join payroll.[Group] g on g.Id = l.GroupId
						inner join Payroll.CostCenter cc on cc.Id = l.CostCenterId
						where l.Id = @LiquidationId
						

					
					FETCH NEXT FROM DetalleLiquidacion_cursor   
					INTO @ConceptId, @ConceptTotalValue
					END   
					CLOSE DetalleLiquidacion_cursor;  
					DEALLOCATE DetalleLiquidacion_cursor;  


				FETCH NEXT FROM functionalunit_cursor   
				INTO @IdFunctionalUnitCursor
			END   
			CLOSE functionalunit_cursor;  
			DEALLOCATE functionalunit_cursor;  


			-- Se cargan las horas trabajadas por unidad funcional
			--select top(@FunctionalUnitsConsecutive) @HoursWorked = SUM(sd.totalnumberhours) from Payroll.ScheduleDetail sd
			--inner join Payroll.Employee e on e.id = sd.EmployeeId
			----inner join Common.ThirdParty tp on tp.id = e.ThirdPartyId
			----inner join Payroll.FunctionalUnit fu on fu.id = sd.FunctionalUnitId
			--where e.Id = @EmployeeId and sd.ScheduleFunctionalUnitId = @FunctionalUnitId and DATEPART(YYYY,sd.DateDetail) = @Year and DATEPART(MM,sd.DateDetail) = @RealMonth
			--GROUP BY sd.ScheduleFunctionalUnitId

			--set @FunctionalUnitsConsecutive = @FunctionalUnitsConsecutive + 1

			--print @FunctionalUnitId
			--set @FunctionalUnitsConsecutive = @FunctionalUnitsConsecutive + 1
			
			--Se aumenta el id
			set @Id = @Id + 1
			
			--Se pasa a la siguiente posicion del cursor
			Fetch Next From InfoItem Into @LiquidationId, @EmployeeId
			continue
			

		End
		
		Close InfoItem
		Deallocate InfoItem
		select * from @TableHeader ORDER BY [EmployeeId], FunctionalUnitName, ConceptLiquidation
		--select * 
		--from @TableHeader th
		--inner join @TableDetail td on td.ParentId = th.Id
		
	end try
	begin catch
		
		select 999 as CodeMessage, ERROR_MESSAGE() + ', LINEA: ' +  cast(ERROR_LINE() as varchar(max)) as Message, '' as CodeAccountPayable, 0 as AccountPayableId
	
		--select * 
		--from @TableHeader
		--inner join @TableDetail td on td.ParentId = th.Id
	end catch

END