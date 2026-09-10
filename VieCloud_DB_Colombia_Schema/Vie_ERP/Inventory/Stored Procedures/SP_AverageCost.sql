

-- =============================================
-- Author:		José Miguel Llanos
-- Create date: 09/07/2016
-- Description:	Procedimiento almacenado para calcular el costo promedio de un producto
-- =============================================
CREATE PROCEDURE [Inventory].[SP_AverageCost]
	
	 @MountParam as integer,
	 @YearParam as integer

AS
BEGIN
	
	-- Creamos la tabla temporal para insertar los registros de las diferentes consultas
	declare @ListProduct table(
		Id varchar(50) not null primary key,
		Mouth int not null,
		ProductId int not null,
		AverageCostInitial decimal(18,2) not null,
		AverageCostFinal decimal(18,2) null,
		DateInitial datetime null,
		DateFinal datetime null
	);

	--declare @ListProduct table(
	--	ProductId int not null,
	--	AverageCostInitial decimal(18,2) not null,
	--	DateFinal datetime not null
	--);

	SET NOCOUNT ON;
		
		declare @DateInitial as datetime = '01/' + RIGHT('0'+Cast(@MountParam as varchar(2)),2) + '/' + cast(@YearParam as varchar(4))

		-- insertamos a la tabla temporal el primer registro del kardex por producto filtrado por mes y año
		--INSERT INTO @ListProduct (Id,Mouth,ProductId,AverageCostInitial,AverageCostFinal,DateInitial)
		--SELECT DISTINCT cast(ProductId as varchar(11)) + '-' +CAST(DocumentDate as varchar(20)) ,RIGHT('0'+Cast(month(DocumentDate) as varchar(2)),2),ProductId, PreviousAverageCost,PreviousAverageCost,@DateInitial
		--FROM Inventory.Kardex as kp WITH (NOLOCK)
		--WHERE DocumentDate = (select min(DocumentDate) from Inventory.Kardex as k WITH (NOLOCK) where k.ProductId = kp.ProductId and MONTH(k.DocumentDate) = @MountParam And YEAR(k.DocumentDate) = @YearParam) 
		--And MONTH(DocumentDate) = @MountParam And YEAR(DocumentDate) = @YearParam 
		
		-- declaramos las variables que utilizaremos en el cursor para insertar los registros cuando el precio del producto varia en el periodo filtrado
		declare @PreviousProduct as int = 0
		declare @ProductIdCostAverageFinal as int
		declare @QuantityCostAverageFinal as int
		declare @UnitValueCostAverageFinal as decimal(18,2)
		declare @IvaValueCostAverageFinal as decimal(18,2)
		declare @DocumentDateAverageFinal as datetime
		declare @ValuePreviousAverageFinal as decimal(18,2) = 0
		declare @ValueCalculateAverageCostFinal as decimal(18,2)
		declare @DateFinal as datetime  
		declare @IdPrevious as varchar(50)
		declare @ValueCalculateAverageCostFinalPrevious as decimal(18,2) = 0

		--declaro un cursor para iterar los registros de los productos que cambiaron de precio en el periodo filtrado
		declare cursor_CostAverageFinal cursor for
		SELECT d.ProductId, d.Quantity, d.UnitValue, d.IvaValue, c.ConfirmationDate, DATEADD(SECOND , -1, DATEADD(Month, 1, @DateInitial))
		FROM Inventory.EntranceVoucherDetail AS d WITH (NOLOCK) INNER JOIN Inventory.EntranceVoucher AS c WITH (NOLOCK) ON d.EntranceVoucherId = c.Id
		where MONTH(ConfirmationDate) = @MountParam And YEAR(ConfirmationDate) = @YearParam ORDER BY d.ProductId, c.ConfirmationDate 
		-- abro el cursor e inicio la iteraccion
		open cursor_CostAverageFinal
			fetch next from cursor_CostAverageFinal
			into @ProductIdCostAverageFinal,@QuantityCostAverageFinal,@UnitValueCostAverageFinal,@IvaValueCostAverageFinal,@DocumentDateAverageFinal,@DateFinal

			WHILE @@FETCH_STATUS = 0
					BEGIN
						--validamos si el producto viene con valor 0 le asigno el producto que estamos iterando
						if @PreviousProduct = 0
						BEGIN
							set @PreviousProduct = @ProductIdCostAverageFinal
							set @ValueCalculateAverageCostFinalPrevious = @UnitValueCostAverageFinal
							set @DateInitial = '01/' + RIGHT('0'+Cast(@MountParam as varchar(2)),2) + '/' + cast(@YearParam as varchar(4))
						END
						-- si el producto de la iteraccion anterior es diferente al de la iteraccion actual igualamos el valor previo del producto a 0 y asignamos a la variable de producto anterior el valor del producto actual
						if @PreviousProduct <> @ProductIdCostAverageFinal
						BEGIN
							set @ValuePreviousAverageFinal = 0
							set @PreviousProduct = @ProductIdCostAverageFinal
							set @ValueCalculateAverageCostFinalPrevious = @UnitValueCostAverageFinal
							set @IdPrevious = ''
							set @DateInitial = '01/' + RIGHT('0'+Cast(@MountParam as varchar(2)),2) + '/' + cast(@YearParam as varchar(4))
						END
						-- si el valor previo es igual a 0 se le asigna el valor del producto que estamos iterando
						if @ValuePreviousAverageFinal = 0
						BEGIN
							set @ValuePreviousAverageFinal = @UnitValueCostAverageFinal
						END
						--if @ValueCalculateAverageCostFinalPrevious = 0
						--BEGIN
						--	set @ValueCalculateAverageCostFinalPrevious = @UnitValueCostAverageFinal
						--END

						-- si el valor previo es diferente al valor del producto que esta iterando procedo a insertarlo en la tabla temporal
						if @ValuePreviousAverageFinal <> @UnitValueCostAverageFinal
						BEGIN
						-- declaramos la variable que obtendra el valor de la cantidad anterior
							declare @QuantityCalculatedCostAverageFinal as int
							-- realizamos la consulta para obtener la cantidad existente del producto antes de la fecha de la iteraccion
							select @QuantityCalculatedCostAverageFinal = (tc.inicial + SUM(iif(tc.MovementType = 1, tc.quantity, 0) - iif(tc.MovementType = 2, tc.quantity, 0))) from
							(SELECT MovementType, k.ProductId, BatchSerialId, SUM(Quantity) as quantity ,d.cant as inicial
							FROM Inventory.Kardex as k WITH (NOLOCK) left outer join (select ProductId,sum(PreviousAmountWarehouse) as cant
							from inventory.kardex WITH (NOLOCK)
							where Id in (select min(Id) from Inventory.Kardex group by ProductId, WarehouseId)
							group by ProductId) as d on k.ProductId = d.ProductId
							WHERE (k.ProductId = @ProductIdCostAverageFinal) AND (AffectInventory = 1) AND (DocumentDate < @DocumentDateAverageFinal)
							GROUP BY MovementType, k.ProductId, BatchSerialId, d.cant) as tc group by tc.inicial 
							 
							 set @DateInitial = @DocumentDateAverageFinal
							-- se realiza el calculo del costo promedio 
							set @ValueCalculateAverageCostFinal = round(((@QuantityCalculatedCostAverageFinal * @ValuePreviousAverageFinal) + (@QuantityCostAverageFinal * @UnitValueCostAverageFinal)) / (IIF((@QuantityCalculatedCostAverageFinal + @QuantityCostAverageFinal) = 0, 1, @QuantityCalculatedCostAverageFinal + @QuantityCostAverageFinal)),0)

							-- inserta el registro en la tabla temporal
							INSERT INTO @ListProduct (Id,Mouth,ProductId,AverageCostInitial,AverageCostFinal,DateInitial,DateFinal) VALUES
							(cast(@ProductIdCostAverageFinal as varchar(11)) + '-' + CAST(@DateInitial as varchar(25)), RIGHT('0'+Cast(month(@DateInitial) as varchar(2)),2),@ProductIdCostAverageFinal, @ValueCalculateAverageCostFinalPrevious,@ValueCalculateAverageCostFinal,@DateInitial,@DateFinal)
							
							set @ValueCalculateAverageCostFinalPrevious = @ValueCalculateAverageCostFinal
							set @ValuePreviousAverageFinal = @UnitValueCostAverageFinal
							set @DateFinal = DATEADD(SECOND, -1, @DocumentDateAverageFinal)

							if @IdPrevious <> ''
							BEGIN
								update @ListProduct Set DateFinal = @DateFinal
								where Id = @IdPrevious

								set @IdPrevious = cast(@ProductIdCostAverageFinal as varchar(11)) + '-' + cast(@DateInitial as varchar(25))
							END

						END
						Else 
						BEGIN

							--set @DateFinal = DATEADD(millisecond, @DateInitial, -1)
							--@DocumentDateAverageFinal
							if (select count(*) from @ListProduct where Id = cast(@ProductIdCostAverageFinal as varchar(11)) + '-' + cast(@DateInitial as varchar(25))) = 0
							BEGIN
							INSERT INTO @ListProduct (Id, Mouth,ProductId,AverageCostInitial,AverageCostFinal,DateInitial,DateFinal) VALUES
							(cast(@ProductIdCostAverageFinal as varchar(11)) + '-' + CAST(@DateInitial as varchar(25)), RIGHT('0'+Cast(month(@DateInitial) as varchar(2)),2),@ProductIdCostAverageFinal, @UnitValueCostAverageFinal,@UnitValueCostAverageFinal,@DateInitial,@DateFinal)

							set @IdPrevious = cast(@ProductIdCostAverageFinal as varchar(11)) + '-' + cast(@DateInitial as varchar(25))
							END

						END

						FETCH NEXT FROM cursor_CostAverageFinal
						INTO @ProductIdCostAverageFinal,@QuantityCostAverageFinal,@UnitValueCostAverageFinal,@IvaValueCostAverageFinal,@DocumentDateAverageFinal,@DateFinal
					End
					-- cierro el cursor
			close cursor_CostAverageFinal
		    deallocate cursor_CostAverageFinal
			-- mostramos los registros cargados en la tabla temporal
	SELECT * FROM @ListProduct order by ProductId, DateInitial

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Calcula el costo promedio de los productos del inventario para un mes y año específicos. Recorre los comprobantes de entrada de mercancía (recepciones de proveedores) y el kardex de movimientos, iterando mediante un cursor los ítems cuyo precio unitario varió dentro del período, para determinar el costo promedio inicial y final de cada producto según las cantidades existentes antes y después de cada compra. Se utiliza en la gestión de inventario y valorización de existencias, permitiendo conocer el costo promedio ponderado de cada producto en un período contable, tomando como base los ingresos de bodega y los saldos anteriores registrados en el kardex.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_AverageCost';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_AverageCost';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Calcula el costo promedio ponderado de cada producto a lo largo de un mes/año dado, generando tramos con costo inicial, costo final y vigencia según las entradas de mercancía registradas en el periodo.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_AverageCost';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir información en Inventory.EntranceVoucher e Inventory.EntranceVoucherDetail con ConfirmationDate dentro del mes y año indicados.; Inventory.Kardex debe contener movimientos previos del producto para poder calcular la cantidad existente antes de cada entrada.; El mes recibido debe ser válido para construir la fecha ''01/MM/YYYY''.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_AverageCost';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El costo promedio ponderado se calcula como ((cantidad_previa * valor_previo) + (cantidad_entrada * valor_unitario)) / (cantidad_previa + cantidad_entrada), evitando división por cero usando 1 cuando la suma es 0.; El Id de cada tramo es la concatenación ''ProductId-DateInitial'' garantizando unicidad por producto y fecha de inicio de vigencia.; DateFinal del tramo anterior siempre se ajusta a (fecha del nuevo documento - 1 segundo) para no solapar vigencias.; Solo se procesan entradas cuyo ConfirmationDate cae en el mes y año parametrizados.; Para el cálculo de cantidad existente solo se consideran movimientos de Kardex con AffectInventory = 1 y DocumentDate anterior a la fecha del documento de entrada.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_AverageCost';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Costo promedio ponderado; Kardex de inventario; Comprobante de entrada de mercancía; Movimientos de inventario (entradas/salidas); Cantidad existente en bodega; Vigencia de costo por producto', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_AverageCost';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @ListProduct: Cuando el valor unitario de la entrada actual difiere del valor previo del mismo producto, se inserta un tramo con AverageCostInitial = costo promedio previo y AverageCostFinal = nuevo costo promedio ponderado calculado con la cantidad existente antes de la fecha del documento.; [INSERT] @ListProduct: Cuando el valor unitario coincide con el previo y aún no existe un registro con ese Id (ProductId-DateInitial), se inserta un tramo inicial con AverageCostInitial = AverageCostFinal = UnitValue de la entrada.; [UPDATE] @ListProduct: Cuando ya existe un tramo previo (@IdPrevious distinto de vacío) y aparece una variación de precio, se actualiza DateFinal del tramo anterior a (DocumentDate - 1 segundo) para cerrar su vigencia.; [RETURN_RESULT] @ListProduct: Al finalizar el cursor se devuelve el contenido de @ListProduct ordenado por ProductId y DateInitial.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_AverageCost';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @PreviousProduct = 0 (primera iteración del cursor) → Inicializa el producto previo, el costo previo calculado y la fecha inicial al primer día del mes/año.; si @PreviousProduct <> @ProductIdCostAverageFinal (cambio de producto entre iteraciones) → Reinicia variables de control (valor previo, IdPrevious, fecha inicial) para empezar a calcular el nuevo producto.; si @ValuePreviousAverageFinal = 0 → Asigna como valor previo el UnitValue de la entrada actual.; si @ValuePreviousAverageFinal <> @UnitValueCostAverageFinal (cambio de precio dentro del periodo) → Calcula la cantidad existente previa desde Kardex, recalcula el costo promedio ponderado, inserta un nuevo tramo en @ListProduct y cierra el tramo anterior actualizando su DateFinal. else Si no hay variación de precio y aún no existe registro para ese ProductId+DateInitial, inserta un tramo con costo inicial y final iguales al UnitValue.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_AverageCost';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.Kardex; Inventory.EntranceVoucherDetail; Inventory.EntranceVoucher', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_AverageCost';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_AverageCost';
-- GO
