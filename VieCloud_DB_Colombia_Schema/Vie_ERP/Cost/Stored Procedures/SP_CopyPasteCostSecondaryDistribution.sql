
-- =============================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 09/11/2016
-- Description:	CopyPaste de distribución secundaria
-- =============================================
CREATE PROCEDURE [Cost].[SP_CopyPasteCostSecondaryDistribution]
	@XmlObject as xml,
	@DistributionSecondaryId as int
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
			if (select count(*) from Cost.ViewCostProductionCenterByCostSecondaryDistribution 
			where DistributionSecondaryId = @DistributionSecondaryId and Code = @ProductionCenterCode) = 0
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
			from Cost.ViewCostProductionCenterByCostSecondaryDistribution 
			where DistributionSecondaryId = @DistributionSecondaryId and Code = @ProductionCenterCode

			--Se valida que el código de la unidad de medida esté dentro de la consulta de la vista ViewMeasurementUnitBySecondaryDistribution
			if (select count(*) from Cost.ViewMeasurementUnitByCostSecondaryDistribution
			where DistributionSecondaryId = @DistributionSecondaryId and Code = @MeasurementUnitCode) = 0
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
			from Cost.ViewMeasurementUnitByCostSecondaryDistribution
			where DistributionSecondaryId = @DistributionSecondaryId and Code = @MeasurementUnitCode

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
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que permite copiar y pegar filas de distribución secundaria de costos, recibiendo los datos en formato XML y validando cada registro contra las distribuciones secundarias existentes. Para cada fila verifica que el centro de producción (unidad organizativa que genera costos) y la unidad de medida sean válidos para la distribución secundaria indicada, y que la cantidad y el valor sean numéricos correctos. Usa las vistas ViewCostProductionCenterByCostSecondaryDistribution y ViewMeasurementUnitByCostSecondaryDistribution para cruzar y enriquecer los códigos pegados con sus identificadores y descripciones completas. Retorna el resultado fila por fila indicando si cada registro fue procesado exitosamente o el mensaje de error correspondiente, facilitando la carga masiva de distribuciones secundarias de costos desde una grilla o plantilla.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_CopyPasteCostSecondaryDistribution';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_CopyPasteCostSecondaryDistribution';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Valida y enriquece un listado XML de items de distribución secundaria de costos (centro de producción, unidad de medida, cantidad y valor) para soportar la operación de copiar/pegar en el formulario.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CopyPasteCostSecondaryDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML debe seguir la estructura /Data/Row con los nodos esperados (CountFields, ProductionCenterCode, MeasurementUnitCode, Quantity, Value, etc.); Debe existir una distribución secundaria identificada cuyo Id se recibe como parámetro; Las vistas ViewCostProductionCenterByCostSecondaryDistribution y ViewMeasurementUnitByCostSecondaryDistribution deben estar disponibles y filtrar por permisos del usuario para la distribución indicada', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CopyPasteCostSecondaryDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Cada item del XML termina con StatusField 0 (con mensaje de error específico) o 1 (''Ok''); El número de posición reportado en los mensajes corresponde al orden de procesamiento del cursor (1-based); El Value final de los registros válidos se recalcula siempre como CostValue * Quantity, sobreescribiendo el Value original del XML; La validación de existencia de centro de producción y unidad de medida se hace siempre acotada al DistributionSecondaryId recibido (respeto a permisos/alcance); Solo se permite editar el costo unitario si la unidad de medida tiene AllowEditCostValue=1', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CopyPasteCostSecondaryDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Distribución secundaria de costos; Centro de producción; Unidad de medida; Costo unitario (CostValue); Cantidad; Copiar/Pegar de items de costo', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CopyPasteCostSecondaryDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RESULT_SET: Siempre se retorna el conjunto de filas procesadas con StatusField (1=Ok, 0=error), MessageField y los Ids/Descripciones resueltos, tanto en éxito como en catch; [UPDATE] @TableXmlObject: Cuando CountFields <> 3 y <> 4, marca el registro con StatusField=0 y mensaje ''no tiene la estructura requerida''; [UPDATE] @TableXmlObject: Cuando el ProductionCenterCode no existe en ViewCostProductionCenterByCostSecondaryDistribution para la distribución, marca StatusField=0 con mensaje ''no existe o no tiene permiso''; [UPDATE] @TableXmlObject: Cuando el MeasurementUnitCode no existe en ViewMeasurementUnitByCostSecondaryDistribution para la distribución, marca StatusField=0 con mensaje ''no existe o no tiene permiso''; [UPDATE] @TableXmlObject: Cuando ISNUMERIC(Quantity) <> 1, marca StatusField=0 con mensaje ''la cantidad no es numérico''; [UPDATE] @TableXmlObject: Cuando AllowEditCostValue=1 y Value no vacío y ISNUMERIC(Value) <> 1, marca StatusField=0 con mensaje ''el valor no es numérico''; [UPDATE] @TableXmlObject: Cuando todas las validaciones pasan, asigna StatusField=1, MessageField=''Ok'', resuelve Ids y descripciones (Code - Name) de centro y unidad y recalcula Value = CostValue * Quantity', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CopyPasteCostSecondaryDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si CountFields <> 3 AND CountFields <> 4 → Marca el registro como inválido por estructura y pasa al siguiente else Continúa con validaciones de negocio; si AllowEditCostValue = 1 AND Value <> '''' → Usa el Value provisto del XML como CostValue (previa validación numérica) else Mantiene el CostValue obtenido de la vista de la unidad de medida', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CopyPasteCostSecondaryDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Cost.ViewCostProductionCenterByCostSecondaryDistribution; Cost.ViewMeasurementUnitByCostSecondaryDistribution', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CopyPasteCostSecondaryDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CopyPasteCostSecondaryDistribution';
-- GO
