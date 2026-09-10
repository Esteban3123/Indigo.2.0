-- =============================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 09/11/2016
-- Description:	CopyPaste de distribución secundaria
-- =============================================
CREATE PROCEDURE [InteropCost].[SP_CopyPasteSecondaryDistribution]
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
			if (select count(*) from InteropCost.ViewProductionCenterBySecondaryDistribution 
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
			from InteropCost.ViewProductionCenterBySecondaryDistribution 
			where DistributionSecondaryId = @DistributionSecondaryId and Code = @ProductionCenterCode

			--Se valida que el código de la unidad de medida esté dentro de la consulta de la vista ViewMeasurementUnitBySecondaryDistribution
			if (select count(*) from InteropCost.ViewMeasurementUnitBySecondaryDistribution
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
			from InteropCost.ViewMeasurementUnitBySecondaryDistribution
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
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de costos que permite copiar y pegar filas de distribución secundaria enviadas en formato XML hacia una distribución secundaria destino, identificada por su ID. Recorre cada fila del XML validando que el centro de producción y la unidad de medida existan y tengan permiso en la distribución secundaria indicada (consultando las vistas ViewProductionCenterBySecondaryDistribution y ViewMeasurementUnitBySecondaryDistribution), y que la cantidad y el valor sean numéricos y coherentes con el costo permitido de la unidad de medida. Devuelve para cada fila un estado de éxito o error con su mensaje descriptivo, permitiendo al usuario del módulo de costos importar masivamente líneas de distribución secundaria desde otra distribución mediante un proceso de copiar/pegar.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'PROCEDURE', @level1name = N'SP_CopyPasteSecondaryDistribution';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'PROCEDURE', @level1name = N'SP_CopyPasteSecondaryDistribution';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Valida y enriquece línea a línea un listado XML de ítems de distribución secundaria de costos (centro de producción, unidad de medida, cantidad y valor) y devuelve cada fila marcada como válida o con su mensaje de error.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_CopyPasteSecondaryDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'@XmlObject debe contener nodos /Data/Row con los elementos esperados (CountFields, StatusField, MessageField, ProductionCenterCode/Id/Description, MeasurementUnitCode/Id/Description, Quantity, Value); @DistributionSecondaryId debe corresponder a una distribución secundaria visible en las vistas ViewProductionCenterBySecondaryDistribution y ViewMeasurementUnitBySecondaryDistribution para resolver códigos y permisos', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_CopyPasteSecondaryDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Cada fila del XML debe tener exactamente 3 o 4 campos para considerarse estructuralmente válida; Solo se aceptan centros de producción y unidades de medida visibles en las vistas filtradas por @DistributionSecondaryId (control de existencia y permisos); El valor final de cada ítem se recalcula como CostValue * Quantity; Solo se permite sobrescribir el CostValue desde el XML si la unidad de medida tiene AllowEditCostValue = 1; Los errores por fila no abortan el lote: el cursor continúa con la siguiente fila y cada fila lleva su propio StatusField/MessageField; Ante excepción no controlada (CATCH) igualmente devuelve el contenido de la tabla temporal procesada hasta el momento', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_CopyPasteSecondaryDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'distribución secundaria de costos; centro de producción; unidad de medida; valor de costo; cantidad', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_CopyPasteSecondaryDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @TableXmlObject: Inserta una fila por cada nodo /Data/Row del XML recibido; [UPDATE] @TableXmlObject: Cuando una validación falla (estructura, código de centro/unidad, cantidad o valor no numérico), actualiza StatusField=0 y MessageField con el detalle indicando la posición; [UPDATE] @TableXmlObject: Cuando todas las validaciones pasan, actualiza StatusField=1, MessageField=''Ok'' y completa ProductionCenterId, ProductionCenterDescription (Code + '' - '' + Name), MeasurementUnitId, MeasurementUnitDescription y Value = CostValue * Quantity; [RETURN_RESULT] @TableXmlObject: Devuelve SELECT * de la tabla temporal con todas las filas validadas (tanto en TRY como en CATCH)', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_CopyPasteSecondaryDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @CountFields <> 3 AND @CountFields <> 4 → marca StatusField=0 con mensaje ''no tiene la estructura requerida'' y salta al siguiente registro; si No existe el ProductionCenterCode en ViewProductionCenterBySecondaryDistribution para el DistributionSecondaryId dado → marca StatusField=0 con mensaje ''no existe o no tiene permiso'' del centro de producción y continúa; si No existe el MeasurementUnitCode en ViewMeasurementUnitBySecondaryDistribution para el DistributionSecondaryId dado → marca StatusField=0 con mensaje ''no existe o no tiene permiso'' de la unidad de medida y continúa; si ISNUMERIC(@Quantity) <> 1 → marca StatusField=0 con mensaje ''la cantidad no es numérico'' y continúa; si @AllowEditCostValue = 1 AND @Value <> '''' AND ISNUMERIC(@Value) <> 1 → marca StatusField=0 con mensaje ''el valor no es numérico'' y continúa; si @AllowEditCostValue = 1 AND @Value <> '''' (numérico) → usa @Value del XML como CostValue else conserva CostValue obtenido de la vista de la unidad de medida', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_CopyPasteSecondaryDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'InteropCost.ViewProductionCenterBySecondaryDistribution; InteropCost.ViewMeasurementUnitBySecondaryDistribution', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_CopyPasteSecondaryDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_CopyPasteSecondaryDistribution';
-- GO
