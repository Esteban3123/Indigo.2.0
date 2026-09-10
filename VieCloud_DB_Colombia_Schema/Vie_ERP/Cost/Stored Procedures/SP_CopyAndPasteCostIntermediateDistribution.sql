

-- =============================================
-- Author:		Diego A Roldan Lozano
-- Create date: 2025-07-02
-- Description:	CopyPaste de distribución intermedia
-- =============================================
CREATE PROCEDURE [Cost].[SP_CopyAndPasteCostIntermediateDistribution]
	@XmlObject as xml,
	@IntermediateDistributionId as int
AS
BEGIN
	
	--Tabla para almacenar los items del listado que viene en el xml
	declare @TableXmlObject table(Id int IDENTITY PRIMARY KEY, CountFields int, StatusField int, MessageField varchar(max), 
								  ProductionCenterCode varchar(20), ProductionCenterId int, ProductionCenterDescription varchar(100), 
								  MeasurementUnitCode varchar(20), MeasurementUnitId int, MeasurementUnitDescription varchar(100),
								  Quantity varchar(18), Value varchar(18))

	--Id centro de producción de cada item
	declare @ProductionCenterId int

	--Descripción centro de producción de cada item
	declare @ProductionCenterDescription varchar(100)

	--Id unidad de medida de cada item
	declare @MeasurementUnitId int

	--Descripción unidad de medida de cada item
	declare @MeasurementUnitDescription varchar(100)

	--Permite saber si el valor de la unidad de medida se puede cambiar
	declare @AllowEditCostValue bit

	--Costo de la unidad de medida
	declare @CostValue decimal

	Begin Try

		insert into @TableXmlObject
		select 
		t.x.value('CountFields[1]','int') as CountFields,
		t.x.value('StatusField[1]','int') as StatusField,
		t.x.value('MessageField[1]','varchar(max)') as MessageField,
		t.x.value('ProductionCenterCode[1]','varchar(20)') as ProductionCenterCode,
		t.x.value('ProductionCenterId[1]','int') as ProductionCenterId,
		t.x.value('ProductionCenterDescription[1]','varchar(100)') as ProductionCenterDescription,
		t.x.value('MeasurementUnitCode[1]','varchar(20)') as MeasurementUnitCode,
		t.x.value('MeasurementUnitId[1]','int') as MeasurementUnitId,
		t.x.value('MeasurementUnitDescription[1]','varchar(100)') as MeasurementUnitDescription,
		t.x.value('Quantity[1]','varchar(18)') as Quantity,
		t.x.value('Value[1]','varchar(18)') as Value
		from @XmlObject.nodes('/Data/Row') t(x)

		--Se declara el contador de posiciones para enviar en los mensajes de error
		declare @Position as int = 0					

		--Se declara un cursor y las variables que lleva el cursor
		declare @Id as int
		declare @CountFields as int
		declare @ProductionCenterCode as varchar(20)
		declare @MeasurementUnitCode as varchar(20)
		declare @Quantity as varchar(18)
		declare @Value as varchar(18)
		declare InfoItem Cursor For Select Id, CountFields, ProductionCenterCode, MeasurementUnitCode, Quantity, Value From @TableXmlObject

		Open InfoItem
		Fetch Next From InfoItem Into @Id, @CountFields, @ProductionCenterCode, @MeasurementUnitCode, @Quantity, @Value

		While @@fetch_status = 0
		Begin
			
			--Se incrementa la posicion
			set @Position = @Position + 1
			
			--Se valida la estructura de cada item del xml
			if @CountFields <> 3 And @CountFields <> 4
			Begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'El registro ' + convert(varchar(3),@Position) + ' no tiene la estructura requerida'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				Fetch Next From InfoItem Into @Id, @CountFields, @ProductionCenterCode, @MeasurementUnitCode, @Quantity, @Value
				continue
			End

			--Se valida que el código del centro de producción esté dentro de la consulta de la vista ViewProductionCenterBySecondaryDistribution
			if (select count(*) from Cost.[ViewCostProductionCenterByCostIntermediateDistribution] 
			WHERE IntermediateDistributionId = @IntermediateDistributionId and Code = @ProductionCenterCode) = 0
			Begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'El código del centro de producción del registro ' + convert(varchar(3),@Position) + ' no existe o no tiene permiso'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				Fetch Next From InfoItem Into @Id, @CountFields, @ProductionCenterCode, @MeasurementUnitCode, @Quantity, @Value
				continue
			End

			--Se asignan los campos que son necesarios para el centro de producción
			select  @ProductionCenterId = Id, @ProductionCenterDescription = Code + ' - ' + Name
			from Cost.[ViewCostProductionCenterByCostIntermediateDistribution] 
			where IntermediateDistributionId = @IntermediateDistributionId and Code = @ProductionCenterCode

			--Se valida que el código de la unidad de medida esté dentro de la consulta de la vista ViewMeasurementUnitBySecondaryDistribution
			if (select count(*) from Cost.ViewMeasurementUnitByCostIntermediateDistribution
			where IntermediateDistributionId = @IntermediateDistributionId and Code = @MeasurementUnitCode) = 0
			Begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'El código de la unidad de medida del registro ' + convert(varchar(3),@Position) + ' no existe o no tiene permiso'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				Fetch Next From InfoItem Into @Id, @CountFields, @ProductionCenterCode, @MeasurementUnitCode, @Quantity, @Value
				continue
			End

			--Se asignan los campos que son necesarios para la unidad de medida
			select @MeasurementUnitId = Id, @MeasurementUnitDescription = Code + ' - ' + Name, 
			@AllowEditCostValue = AllowEditCostValue, @CostValue = CostValue
			from Cost.ViewMeasurementUnitByCostIntermediateDistribution
			where IntermediateDistributionId = @IntermediateDistributionId and Code = @MeasurementUnitCode

			--Se valida que la cantidad sea numérico
			if ISNUMERIC(@Quantity) <> 1
			Begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'La cantidad del registro ' + convert(varchar(3),@Position) + ' no es numérico'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				Fetch Next From InfoItem Into @Id, @CountFields, @ProductionCenterCode, @MeasurementUnitCode, @Quantity, @Value
				continue
			End

			--Se verifica si la unidad de medida permite cambiar el valor
			if @AllowEditCostValue = 1
			Begin
				--Se verifica que el valor que viene del xml esté diligenciado
				if @Value <> ''
				Begin
					--Se valida que el valor sea numerico
					if ISNUMERIC(@Value) <> 1
					Begin
						--Se actualiza los campos con el estado en false y el mensaje de error
						update @TableXmlObject set StatusField = 0, MessageField = 'El valor del registro ' + convert(varchar(3),@Position) + ' no es numérico'
						where Id = @Id			
						--Se pasa a la siguiente posicion del cursor
						Fetch Next From InfoItem Into @Id, @CountFields, @ProductionCenterCode, @MeasurementUnitCode, @Quantity, @Value
						continue
					End

					--Se asigna el valor que viene del xml al valor de la unidad de medida
					set @CostValue = @Value
				End
			End

			--Se actualiza el valor
			set @Value = @CostValue * @Quantity

			--Se actualiza los campos con que se necesitan para armar el objeto en el formulario
			update @TableXmlObject set StatusField = 1, MessageField = 'Ok', ProductionCenterId = @ProductionCenterId, 
			ProductionCenterDescription = @ProductionCenterDescription, MeasurementUnitId = @MeasurementUnitId, 
			MeasurementUnitDescription = @MeasurementUnitDescription, Value = @Value
			where Id = @Id	

			Fetch Next From InfoItem Into @Id, @CountFields, @ProductionCenterCode, @MeasurementUnitCode, @Quantity, @Value	

		End

		Close InfoItem
		Deallocate InfoItem

		select * from @TableXmlObject

	End Try
	Begin Catch
		select * from @TableXmlObject
	End Catch

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de costos que permite copiar y pegar filas de una distribución intermedia de costos a partir de un XML con múltiples registros. Recibe un listado de ítems en formato XML (centro de producción, unidad de medida, cantidad y valor) junto con el identificador de la distribución intermedia de destino, valida cada registro contra las vistas de centros de producción y unidades de medida permitidos para esa distribución, y retorna el resultado de cada fila indicando si fue procesada correctamente o el motivo del error. Se usa en el módulo de costos para agilizar la carga masiva de datos en la distribución intermedia mediante operaciones de copiar y pegar desde interfaces de usuario.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_CopyAndPasteCostIntermediateDistribution';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_CopyAndPasteCostIntermediateDistribution';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Valida y enriquece filas de una distribución intermedia de costos provenientes de un XML (copiar/pegar), verificando estructura, existencia de centro de producción y unidad de medida, numericidad y calculando el valor total por cantidad.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteCostIntermediateDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML debe seguir la estructura /Data/Row con los nodos esperados (CountFields, ProductionCenterCode, MeasurementUnitCode, Quantity, Value, etc.).; Debe existir un IntermediateDistributionId válido contra el cual filtran las vistas de centros de producción y unidades de medida.; Cada fila del XML debe declarar CountFields igual a 3 o 4.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteCostIntermediateDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El valor final se calcula siempre como CostValue * Quantity sólo para los registros que pasan todas las validaciones.; El CostValue del XML solo reemplaza al de la unidad de medida si AllowEditCostValue = 1 y el valor está diligenciado y es numérico.; Los registros inválidos conservan StatusField=0 con un mensaje descriptivo y no recalculan Value.; Cualquier excepción del flujo igualmente retorna el estado actual de la tabla (no se relanza el error).; La validación de centro de producción y unidad de medida está acotada al IntermediateDistributionId recibido (control implícito de permisos/visibilidad).', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteCostIntermediateDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Distribución intermedia de costos; Centro de producción; Unidad de medida; Costo unitario; Cantidad; Copiar y pegar (carga masiva vía XML)', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteCostIntermediateDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @TableXmlObject: Carga inicial: por cada nodo /Data/Row del XML se inserta una fila con los valores parseados.; [UPDATE] @TableXmlObject: Cuando CountFields no es 3 ni 4 → StatusField=0 y MessageField indica que el registro no tiene la estructura requerida.; [UPDATE] @TableXmlObject: Cuando el código de centro de producción no existe en ViewCostProductionCenterByCostIntermediateDistribution para el IntermediateDistributionId → StatusField=0 y mensaje ''no existe o no tiene permiso''.; [UPDATE] @TableXmlObject: Cuando el código de unidad de medida no existe en ViewMeasurementUnitByCostIntermediateDistribution para el IntermediateDistributionId → StatusField=0 y mensaje ''no existe o no tiene permiso''.; [UPDATE] @TableXmlObject: Cuando ISNUMERIC(Quantity)<>1 → StatusField=0 y mensaje ''la cantidad no es numérico''.; [UPDATE] @TableXmlObject: Cuando AllowEditCostValue=1 y Value no vacío y ISNUMERIC(Value)<>1 → StatusField=0 y mensaje ''el valor no es numérico''.; [UPDATE] @TableXmlObject: Cuando todas las validaciones pasan → StatusField=1, MessageField=''Ok'', se asignan ProductionCenterId/Description, MeasurementUnitId/Description y Value = CostValue * Quantity.; [RETURN_RESULT] @TableXmlObject: Al finalizar (o en CATCH) se retorna el contenido completo de la tabla temporal con resultados de validación.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteCostIntermediateDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si CountFields <> 3 AND CountFields <> 4 → Marca el registro como inválido por estructura y salta al siguiente. else Continúa con validaciones de centro de producción.; si El código de centro de producción no se encuentra para el IntermediateDistributionId → Marca inválido por inexistencia/permiso y salta al siguiente. else Asigna Id y Descripción del centro de producción.; si El código de unidad de medida no se encuentra para el IntermediateDistributionId → Marca inválido por inexistencia/permiso y salta al siguiente. else Asigna Id, Descripción, AllowEditCostValue y CostValue de la unidad.; si ISNUMERIC(Quantity) <> 1 → Marca inválido por cantidad no numérica y salta al siguiente.; si AllowEditCostValue = 1 AND Value <> '''' → Si Value no es numérico marca inválido; si lo es, sobreescribe CostValue con el Value del XML. else Conserva el CostValue original de la unidad de medida.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteCostIntermediateDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Cost.ViewCostProductionCenterByCostIntermediateDistribution; Cost.ViewMeasurementUnitByCostIntermediateDistribution', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteCostIntermediateDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteCostIntermediateDistribution';
-- GO
