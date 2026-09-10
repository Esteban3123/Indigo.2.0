
-- ==================================================================================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 13/07/2016
-- Description:	Procedimiento que se encarga de guardar masivamente la distribución de mano de obra
-- ==================================================================================================
CREATE PROCEDURE [InteropCost].[SP_ConfirmMasiveInteropCostDistributionManpower] 
	@ListIdsXml as Xml,
	@Year as int,
	@Month as int,
	@OperatingUnitId int,
	@CodeUser as varchar(20)
AS
BEGIN

	--Tabla temporal para obtener los ids del xml(Id de la liquidación y Id del empleado)
	declare @TableIds table(LiquidationId int, EmployeeId int)
	--Id de la cabecera de la distribución de mano de obra
	declare @HeaderId int
	--Mensajes que se retorna en el caso que se ejecute todo el procedimiento de forma correcta
	declare @MessagesReturn varchar(max) = 'Se guardaron los registros correctamente con códigos: '

	Begin try
	
		--Se obtienen los Ids y se insertan en la tabla temporal
		insert into @TableIds
		select 
		t.x.value('LiquidationId[1]','int') as LiquidationId,
		t.x.value('EmployeeId[1]','int') as EmployeeId
		from @ListIdsXml.nodes('/Ids') t(x)

		--Se valida que exista secuencia numérica para el form correspondiente
		declare @idSequenceDetail int
		select @idSequenceDetail = bsd.Id  
		from InteropCost.InteropCostSecuenceDetail bsd 
		inner join InteropCost.InteropCostSecuence bs on bs.Id = bsd.SequenseInteropCostId 
		inner join Common.Sequense cs on cs.Id = bsd.IdSequense
		where bs.IdForm = '1203' --and bsd.IdOperatingUnit = @OperatingUnitId
		if (@idSequenceDetail is null) --Si no existe la secuencia
		Begin
			select 999 as CodeMessage, 'El formulario no tiene parametrizada la secuencia numérica'  as Message
			return
		End

		--Se valida que la secuencia numérica sea secuencial y no manual
		declare @IsManual bit
		declare @Sequential bit
		select @IsManual = bs.IsManual, @Sequential = bs.Sequential 
		from InteropCost.InteropCostSecuenceDetail bsd 
		inner join InteropCost.InteropCostSecuence bs on bs.Id = bsd.SequenseInteropCostId 
		inner join Common.Sequense cs on cs.Id = bsd.IdSequense
		where bs.IdForm = '1203' --and bsd.IdOperatingUnit = @OperatingUnitId
		if @IsManual = 1 or @Sequential = 0
		Begin
			select 999 as CodeMessage, 'La secuencia numérica para la confirmación masiva no puede ser manual y tiene que ser secuencial'  as Message
			return
		End

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
		--Código de la cabecera
		declare @Code varchar(max) = ''
		--Unidad funcional para la validacion en el error
		declare @FuntionalUnitCodeName varchar(max)
		--Nit y nombre del empleado
		declare @EmployeeNitName varchar(max)
		
		--Variable para errores a devolver
		declare @errors varchar(max) = ''

		--Contador para asignar las comas al mensaje
		declare @Cont int = 1

		Open InfoItem
		Fetch Next From InfoItem Into @LiquidationId, @EmployeeId
		While @@fetch_status = 0
		Begin

			--Se valida que el empleado no haya sido guardado en la distribucion de mano de obra con el mes y el año previamente
			if (select COUNT(*) from InteropCost.DistributionManpower where [EmployeeId] = @EmployeeId and [Year] = @Year and [Month] = @Month) > 0
			Begin
				--Se pasa a la siguiente posicion del cursor
				Fetch Next From InfoItem Into @LiquidationId, @EmployeeId
				continue
			End

			--Consultamos la liquidación de cada empleado para sacar algunos valores
			select 
			@HoursWorked = l.DaysWorked * c.HoursDaily,
			@TotalAccrued = l.TotalAccrued,
			@TotalProvision = l.ProvisionsValue,
			@TotalParafiscal = l.ParafiscalContribution,
			@HandlesTurnsChart = p.HandlesTurnsChart
			from Payroll.Liquidation l
			inner join Payroll.Contract c on c.Id = l.ContractId
			inner join Payroll.Position p on p.Id = c.PositionId
			where l.Id = @LiquidationId
			
			--Se consultan los patronales
			select @TotalEmployerContribution = SUM(ld.DeductedValue)
			from Payroll.Liquidation l
			inner join Payroll.LiquidationDetail ld on ld.PayrollId = l.Id
			inner join Payroll.Concept c on ld.ConceptId = c.Id
			where l.EmployeeId = @EmployeeId and YEAR(l.PayrollDateLiquidated) = @Year and MONTH(l.PayrollDateLiquidated) = @Month
			and l.RegisterStatus = 'C' and c.ConceptType = 3
			
			if @TotalEmployerContribution is null
			Begin
				set @TotalEmployerContribution = 0
			End

			--Se genera la secuencia numérica para la distribución de mano de obra
			declare @pattern varchar(300)
			declare @NextS int
			select @pattern = cs.Pattern, @NextS = bsd.[Next] , @idSequenceDetail = bsd.Id  
			from InteropCost.InteropCostSecuenceDetail bsd 
			inner join InteropCost.InteropCostSecuence bs on bs.Id = bsd.SequenseInteropCostId
			inner join Common.Sequense cs on cs.Id = bsd.IdSequense
			where bs.IdForm = '1203'
			if (@idSequenceDetail is null)
			Begin
				Close InfoItem
				Deallocate InfoItem
				select 999 as CodeMessage, 'Secuencia no encontrada para generar la distribución de mano de obra' as Message
				return
			End
			select @Code = dbo.GetSequence('',@pattern,@NextS)
			update InteropCost.InteropCostSecuenceDetail set [Next] += 1 where Id = @idSequenceDetail

			--Se genera la descripción de la cabecera del empleado
			select @EmployeeNitName = t.Nit +  ' - ' + t.Name
			from Payroll.Employee e
			inner join Common.ThirdParty t on t.Id = e.ThirdPartyId
			where e.Id = @EmployeeId

			--Se guarda la cabecera de la distribución de mano de obra
			insert into InteropCost.DistributionManpower([Code], [EmployeeId], [Description], [Year], [Month], [HoursWorked], [TotalAccrued],
			[TotalProvision], [TotalEmployerContribution], [TotalParafiscal], [Status], [CreationUser], [CreationDate])
			values
			(@Code, @EmployeeId, @EmployeeNitName, @Year, @Month, @HoursWorked, @TotalAccrued, @TotalProvision, @TotalEmployerContribution, 
			@TotalParafiscal, 1, @CodeUser, [Common].[GETDATE]())

			--Se obtiene el id de la cabecera
			set @HeaderId = SCOPE_IDENTITY()

			--Se va asignando los códigos generados al mensaje
			if @Cont = 1
			Begin
				set @MessagesReturn = @MessagesReturn + @Code
			End
			Else
			Begin
				set @MessagesReturn = @MessagesReturn + ', ' + @Code
			End

			if @HandlesTurnsChart = 1 --Si maneja cuadro de turno
			Begin
			
				--Se busca en schedule(cuadro de turno)
				declare @Period varchar(max) = cast(@Month as varchar(max)) + '/' + cast(@Year as varchar(max))
				
				if @Month < 10 --Si el mes es menor a 10 osea dos digitos se concatena un cero
				Begin
					set @Period = '0' + @Period
				End
				
				if (select COUNT(*) from Payroll.Schedule where EmployeeId = @EmployeeId and Period = @Period) > 0 --Si encuentra turnos para ese periodo
				Begin
				
					--Se asigna las horas trabajadas
					select @HoursWorked = SUM(s.TotalHour) 
					from Payroll.Schedule s
					inner join Payroll.FunctionalUnit fu on fu.Id = s.FunctionalUnitId
					where s.EmployeeId = @EmployeeId and s.Period = @Period and fu.ProductionCenterId is not null

					if @HoursWorked = 0 --Si es igual a cero se saca el valor de la liquidacion
					Begin
						select 
						@HoursWorked = l.DaysWorked * c.HoursDaily
						from Payroll.Liquidation l
						inner join Payroll.Contract c on c.Id = l.ContractId
						inner join Payroll.Position p on p.Id = c.PositionId
						where l.Id = @LiquidationId
					End
					
					if @HoursWorked = 0 --Si es cero se salta al siguiente
					Begin
						--Se pasa a la siguiente posicion del cursor
						Fetch Next From InfoItem Into @LiquidationId, @EmployeeId
						continue
					End

					--- Actualizo las horas
					update InteropCost.DistributionManpower set HoursWorked = HoursWorked where Id = @HeaderId

					--Se valida que las unidades funcionales de los cuadros de turno tengan asociado un centro de produccion
					if (select COUNT(*)
					from Payroll.Schedule s
					inner join Payroll.FunctionalUnit fu on fu.Id = s.FunctionalUnitId
					where s.EmployeeId = @EmployeeId and s.Period = @Period and fu.ProductionCenterId is null) > 0 
					Begin
						select  @errors = stuff((select distinct N'; La unidad funcional ' + fu.Code + ' no tiene parametrizado centro de producción'
						from Payroll.Schedule s
						inner join Payroll.FunctionalUnit fu on fu.Id = s.FunctionalUnitId
						where s.EmployeeId = @EmployeeId and s.Period = @Period and fu.ProductionCenterId is null
						for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
						Close InfoItem
						Deallocate InfoItem
						select 999 as CodeMessage, @errors as Message
						return
					End
					--Se inserta en la tabla temporal del detalle de distribucion de mano de obra
					insert into InteropCost.DistributionManpowerDetail(DistributionManpowerId, ProductionCenterId, HoursQuantity, TotalAccrued, TotalProvision, TotalEmployerContribution, TotalParafiscal)
					select @HeaderId, fu.ProductionCenterId, s.TotalHour,  (s.TotalHour / @HoursWorked) * @TotalAccrued, (s.TotalHour / @HoursWorked) * @TotalProvision, (s.TotalHour / @HoursWorked) * @TotalEmployerContribution, (s.TotalHour / @HoursWorked) * @TotalParafiscal
					from Payroll.Schedule s
					inner join Payroll.FunctionalUnit fu on fu.Id = s.FunctionalUnitId
					where s.EmployeeId = @EmployeeId and s.Period = @Period and fu.ProductionCenterId is not null
					
				End
				Else --Si no encuentra turnos para ese periodo
				Begin
				
					--Se valida que la unidad funcional tenga asociado el centro de producción
					if (select 
					COUNT(*)
					from Payroll.Liquidation l
					inner join Payroll.Contract c on c.Id = l.ContractId
					inner join Payroll.FunctionalUnit fu on fu.Id = c.FunctionalUnitId
					where l.Id = @LiquidationId and fu.ProductionCenterId is null) > 0
					Begin
						select @FuntionalUnitCodeName = fu.Code + ' - ' + fu.Name from Payroll.Liquidation l inner join Payroll.Contract c on c.Id = l.ContractId inner join Payroll.FunctionalUnit fu on fu.Id = c.FunctionalUnitId where l.Id = @LiquidationId and fu.ProductionCenterId is null
						Close InfoItem
						Deallocate InfoItem
						select 999 as CodeMessage, 'La unidad funcional ' + @FuntionalUnitCodeName + ' no tiene parametrizado un centro de producción' as Message
						return
					End

					if @HoursWorked = 0 --Si es cero se salta al siguiente
					Begin
						--Se pasa a la siguiente posicion del cursor
						Fetch Next From InfoItem Into @LiquidationId, @EmployeeId
						continue
					End

					--Se inserta en la tabla temporal
					insert into InteropCost.DistributionManpowerDetail(DistributionManpowerId, ProductionCenterId, HoursQuantity, TotalAccrued, TotalProvision, TotalEmployerContribution, TotalParafiscal)
					select @HeaderId, fu.ProductionCenterId, l.DaysWorked * c.HoursDaily,  ((l.DaysWorked * c.HoursDaily) / @HoursWorked) * @TotalAccrued, ((l.DaysWorked * c.HoursDaily) / @HoursWorked) * @TotalProvision, ((l.DaysWorked * c.HoursDaily) / @HoursWorked) * @TotalEmployerContribution, ((l.DaysWorked * c.HoursDaily) / @HoursWorked) * @TotalParafiscal
					from Payroll.Liquidation l
					inner join Payroll.Contract c on c.Id = l.ContractId
					inner join Payroll.FunctionalUnit fu on fu.Id = c.FunctionalUnitId
					where l.Id = @LiquidationId

				End

			End
			Else --Si no maneja cuadro de turno
			Begin

				--Se valida que la unidad funcional tenga asociado el centro de producción
				if (select 
				COUNT(*)
				from Payroll.Liquidation l
				inner join Payroll.Contract c on c.Id = l.ContractId
				inner join Payroll.FunctionalUnit fu on fu.Id = c.FunctionalUnitId
				where l.Id = @LiquidationId and fu.ProductionCenterId is null) > 0
				Begin
					select @FuntionalUnitCodeName = fu.Code + ' - ' + fu.Name from Payroll.Liquidation l inner join Payroll.Contract c on c.Id = l.ContractId inner join Payroll.FunctionalUnit fu on fu.Id = c.FunctionalUnitId where l.Id = @LiquidationId and fu.ProductionCenterId is null
					Close InfoItem
					Deallocate InfoItem
					select 999 as CodeMessage, 'La unidad funcional ' + @FuntionalUnitCodeName + ' no tiene parametrizado un centro de producción' as Message
					return
				End

				if @HoursWorked = 0 --Si es cero se salta al siguiente
				Begin
					--Se pasa a la siguiente posicion del cursor
					Fetch Next From InfoItem Into @LiquidationId, @EmployeeId
					continue
				End

				--Se inserta en la tabla temporal
				insert into InteropCost.DistributionManpowerDetail(DistributionManpowerId, ProductionCenterId, HoursQuantity, TotalAccrued, TotalProvision, TotalEmployerContribution, TotalParafiscal)
				select @HeaderId, fu.ProductionCenterId, l.DaysWorked * c.HoursDaily,  ((l.DaysWorked * c.HoursDaily) / @HoursWorked) * @TotalAccrued, ((l.DaysWorked * c.HoursDaily) / @HoursWorked) * @TotalProvision, ((l.DaysWorked * c.HoursDaily) / @HoursWorked) * @TotalEmployerContribution, ((l.DaysWorked * c.HoursDaily) / @HoursWorked) * @TotalParafiscal
				from Payroll.Liquidation l
				inner join Payroll.Contract c on c.Id = l.ContractId
				inner join Payroll.FunctionalUnit fu on fu.Id = c.FunctionalUnitId
				where l.Id = @LiquidationId

			End
			
			--Se aumenta el contador
			set @Cont = @Cont + 1

			--Se pasa a la siguiente posicion del cursor
			Fetch Next From InfoItem Into @LiquidationId, @EmployeeId
			continue

		End
		Close InfoItem
		Deallocate InfoItem

		select 0 as CodeMessage, @MessagesReturn as Message
		
	end try
	begin catch
		select 999 as CodeMessage, ERROR_MESSAGE() as Message
	end catch

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que confirma y guarda de forma masiva la distribución del costo de mano de obra para un mes y año determinados, procesando una lista de liquidaciones de nómina y empleados enviada en formato XML. Para cada empleado, valida que no exista una distribución previa en el período indicado, calcula las horas trabajadas (días laborados por horas diarias del contrato), el total devengado, provisiones, aportes patronales y parafiscales consultando las tablas de liquidación, contrato y cargo de nómina. Antes de registrar, verifica que el formulario de distribución de mano de obra (código 1203) tenga configurada una secuencia numérica automática y secuencial en el módulo de interoperabilidad de costos, generando y actualizando dicho consecutivo por cada registro guardado. Se usa para imputar masivamente el gasto de personal a los centros o unidades de costo correspondientes dentro del módulo de costos de interoperabilidad.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'PROCEDURE', @level1name = N'SP_ConfirmMasiveInteropCostDistributionManpower';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'PROCEDURE', @level1name = N'SP_ConfirmMasiveInteropCostDistributionManpower';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Procesa masivamente, desde un XML de pares liquidación/empleado, la generación de cabeceras y detalles de distribución de mano de obra por centro de producción para un mes/año, prorrateando devengados, provisiones, aportes patronales y parafiscales según horas trabajadas o turnos.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmMasiveInteropCostDistributionManpower';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'@ListIdsXml debe contener nodos /Ids con elementos LiquidationId y EmployeeId; Debe existir parametrización de secuencia numérica para IdForm=''1203'' en InteropCostSecuence/InteropCostSecuenceDetail/Common.Sequense; La secuencia del formulario debe ser automática (IsManual=0) y secuencial (Sequential=1); Cada LiquidationId debe existir en Payroll.Liquidation con su Contract y Position asociados; El empleado debe tener un ThirdParty asociado para construir la descripción ''Nit - Nombre''', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmMasiveInteropCostDistributionManpower';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Sólo se procesan empleados que aún no tengan distribución de mano de obra registrada para el mismo Year/Month; El consecutivo (Code) se genera vía dbo.GetSequence usando el patrón de Common.Sequense y se incrementa Next en InteropCostSecuenceDetail por cada cabecera creada; La secuencia utilizada es siempre la del formulario IdForm=''1203''; Los aportes patronales se obtienen sumando LiquidationDetail.DeductedValue de conceptos con ConceptType=3 sobre liquidaciones del empleado en el período (PayrollDateLiquidated) con RegisterStatus=''C''; Cada detalle de DistributionManpowerDetail se prorratea proporcionalmente: (HorasFuente / HoursWorked) * Total<X> para Accrued, Provision, EmployerContribution y Parafiscal; Sólo se generan detalles para unidades funcionales que tengan ProductionCenterId asignado; La cabecera se inserta con Status=1 y CreationUser=@CodeUser y CreationDate=Common.GETDATE(); Description de la cabecera se construye como ''Nit - Nombre'' del tercero asociado al empleado; El procedimiento maneja errores de ejecución vía TRY/CATCH retornando CodeMessage=999 con ERROR_MESSAGE(); En éxito retorna CodeMessage=0 con la lista de códigos generados', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmMasiveInteropCostDistributionManpower';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Distribución de mano de obra; Liquidación de nómina; Aportes patronales; Parafiscales; Provisiones de nómina; Devengado; Cuadro de turnos; Unidad funcional; Centro de producción; Secuencia numérica de formulario; Empleado/Tercero (Nit y nombre); Contrato laboral; Cargo (Position)', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmMasiveInteropCostDistributionManpower';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si No existe registro en InteropCostSecuenceDetail con bs.IdForm=''1203'' → Retorna CodeMessage=999 con mensaje ''El formulario no tiene parametrizada la secuencia numérica'' y termina; si La secuencia tiene IsManual=1 o Sequential=0 → Retorna CodeMessage=999 indicando que la secuencia debe ser automática y secuencial; no procesa; si El empleado ya tiene registro en DistributionManpower para el mismo Year y Month → Se omite ese empleado y continúa con el siguiente del cursor else Procesa la liquidación e inserta cabecera; si Position.HandlesTurnsChart = 1 y existen registros en Payroll.Schedule para el EmployeeId y Period (MM/YYYY) → Calcula HoursWorked como SUM(TotalHour) de Schedule sólo donde FunctionalUnit.ProductionCenterId IS NOT NULL e inserta detalle prorrateando por turno else Si no hay turnos para el período, valida la unidad funcional del contrato e inserta detalle con base en DaysWorked*HoursDaily de la liquidación; si HandlesTurnsChart=1 y existen turnos cuya FunctionalUnit no tiene ProductionCenterId → Cierra cursor, retorna CodeMessage=999 listando las unidades funcionales sin centro de producción y termina; si No maneja cuadro de turnos y la unidad funcional del contrato no tiene ProductionCenterId → Cierra cursor, retorna CodeMessage=999 con mensaje indicando la unidad funcional sin centro de producción; si Con turnos, SUM(TotalHour) = 0 → Recalcula HoursWorked como DaysWorked*HoursDaily de la liquidación; si HoursWorked finalmente = 0 → Se omite el empleado y se avanza al siguiente del cursor; si @TotalEmployerContribution resulta NULL al sumar conceptos tipo 3 → Se asigna 0; si @Month < 10 → Se antepone ''0'' al período para formar ''MM/YYYY''', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmMasiveInteropCostDistributionManpower';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.GetSequence; Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmMasiveInteropCostDistributionManpower';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'InteropCost.InteropCostSecuenceDetail; InteropCost.InteropCostSecuence; Common.Sequense; InteropCost.DistributionManpower; Payroll.Liquidation; Payroll.Contract; Payroll.Position; Payroll.LiquidationDetail; Payroll.Concept; Payroll.Employee; Common.ThirdParty; Payroll.Schedule; Payroll.FunctionalUnit', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmMasiveInteropCostDistributionManpower';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmMasiveInteropCostDistributionManpower';
-- GO
