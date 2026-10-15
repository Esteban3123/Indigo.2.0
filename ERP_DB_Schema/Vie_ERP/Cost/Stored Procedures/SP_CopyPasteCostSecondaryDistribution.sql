
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