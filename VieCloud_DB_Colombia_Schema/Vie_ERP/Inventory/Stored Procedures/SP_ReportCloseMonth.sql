
-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-05-10
-- Description:	Genera el informe valorizado de inventario al cierre de mes
-- =============================================

CREATE PROCEDURE [Inventory].[SP_ReportCloseMonth]
	@xmlFilters AS XML
AS
BEGIN
	SET NOCOUNT ON

	DECLARE	@YearReport INT,
			@MonthReport INT,
			@ReportType TINYINT,
			@InitialWarehouse VARCHAR(50),
			@FinalWarehouse VARCHAR(50),
			@InitialGroup VARCHAR(50),
			@FinalGroup VARCHAR(50),
			@IVACost BIT,
			@OfficialCurrency int,
			@ToCurrency INT

	--Se obtienen los datos de los filtros
	SELECT	@YearReport = t.x.value('Year[1]','INT'),
			@MonthReport = t.x.value('Month[1]','INT'),
			@ReportType = t.x.value('ReportType[1]','INT'),
			@InitialWarehouse = t.x.value('InitialWarehouse[1]','INT'),
			@FinalWarehouse = t.x.value('FinalWarehouse[1]','INT'),
			@InitialGroup = t.x.value('InitialGroup[1]','INT'),
			@FinalGroup = t.x.value('FinalGroup[1]','INT'),
			@ToCurrency = t.x.value('ToCurrency[1]','INT')
	FROM @xmlFilters.nodes('/Data') t(x)

	DECLARE @TableInventory TABLE
	(
		ProductId INT,
		WarehouseId INT,
		BatchSerialId INT,
		Quantity INT,
		CostTotal DECIMAL(20,4)
	)

	DECLARE @TableProductCost TABLE
	(
		ProductId INT,
		Quantity INT,
		CostTotal DECIMAL(20,4)
	)

	DECLARE @currentDate datetime, -- fecha actual al dia que se realiza el reporte
			@ReportDate DATETIME, --feha estima del reporte si es de una fecha diferente a la fecha actual
			@ConvertDate DATETIME, --fecha final <toma el dia actual o el ultimo dia del mes segun las validaciones
			@currentMonth DATETIME,
			@currentYear DATETIME
	BEGIN TRY
	/******************************************  OBTENCION DE LA FECHA PARA CREAR EL REPORTE BASADO EN EL TRM  ******************************************/

		set @currentDate = Common.GETDATE()
		
		--Validamos si el mes y el año ingresado para generar el reporte se encuentran cerrados o todavia esta abierto
		if EXISTS (select * from inventory.ClosedMonth cm WHERE cm.Year = @YearReport and cm.Month = @MonthReport)
		begin 
			--creamos la fecha estima de la creacion del reporte tomando en cuenta el año y mes ingresados; se deja el dia como 1 ya que todos lo meses tienen este dia
			set @ReportDate = DATEFROMPARTS(@YearReport, @MonthReport, 1);
			
			--el mes al estar cerrado tomamos el ultimo dia de dicho mes en el año seleccionado
			set @ConvertDate  = EOMONTH ( @ReportDate )	

			--validamos que exista un trm para la fecha del reportw
			if NOT EXISTS(select * from Common.TRM where MeasurementDate = @ConvertDate) and exists(select 1
																									from (select OfficialCurrencyId from GeneralLedger.LegalBook group by OfficialCurrencyId) a
																									having count(*) > 1)
				BEGIN
					SELECT '999' CodeResult, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)) MessageResult
				end
		END 
		ELSE
		BEGIN

			--obtenesmos el mes de la fecha actual
			set @currentMonth = MONTH(@currentDate)
			set @currentYear =  YEAR(@currentDate )

			--si la fecha del reporte tiene el mismo mes y años al actual tomamos la fehca del dia 
			if (@currentMonth =  @MonthReport) and ( @currentYear = @YearReport)
			begin
				set @ConvertDate= @currentDate
			end
			ELSE
			BEGIN
				SELECT '999' CodeResult,'No existe datos para la fecha seleccionada'MessageResult
			END
		END
	/***********************************************************************************************************************************************/	
		SELECT @IVACost = IVACost FROM Inventory.SettingInventory
		--se obtiene la moneda oficial 
		select @OfficialCurrency = OfficialCurrencyId from GeneralLedger.CompanySettings

			IF @ToCurrency is null 
		BEGIN
			select @ToCurrency =  cs.Id
			FROM GeneralLedger.CompanySettings cs
		end
		
		/******************************************  OBTENCION DE DATOS ******************************************/

		INSERT INTO @TableInventory
			SELECT	ISNULL(cmi.ProductId, k.ProductId) ProductId,
					ISNULL(cmi.WarehouseId, k.WarehouseId) WarehouseId,
					ISNULL(cmi.BatchSerialId, k.BatchSerialId) BatchSerialId,
					ISNULL(cmi.Quantity, 0) + ISNULL(k.Quantity, 0) Quantity,
					CASE 
						WHEN ISNULL(cmi.CostTotal, 0) + ISNULL(k.CostTotal, 0) < 0 THEN 0
						ELSE ISNULL(cmi.CostTotal, 0) + ISNULL(k.CostTotal, 0)
					END AS CostTotal
			FROM 
			(
				SELECT	k.ProductId, 
						IIF(@ReportType = 1, NULL, k.WarehouseId) WarehouseId,
						k.BatchSerialId,
						SUM(k.Quantity * IIF(k.MovementType = 1, 1, -1)) Quantity,
						SUM((k.Value - ISNULL(evd.Value, 0)) * IIF(k.MovementType = 1, 1, -1)) CostTotal
				FROM 
				(
					SELECT	k.ProductId,
							k.WarehouseId,
							k.BatchSerialId,
							k.AffectInventory,
							k.MovementType,
							k.EntityName,
							k.EntityId,
							SUM(IIF(k.AffectInventory = 1, k.Quantity, 0)) Quantity,
							SUM(IIF(k.Quantity = 0 AND k.EntityName = 'JournalVouchers', 1, k.Quantity) * ip.ProductCost) Value
					FROM Inventory.Kardex k WITH (NOLOCK)
					JOIN Inventory.InventoryProduct ip WITH(NOLOCK) on k.ProductId =ip.Id
					WHERE 
					(
						(YEAR(k.DocumentDate) = @YearReport AND MONTH(k.DocumentDate) < @MonthReport)
						OR
						(YEAR(k.DocumentDate) < @YearReport)
					)

					AND k.Quantity > 0 -- evita acumulaciones falsas
					AND k.AffectInventory = 1 -- solo movimientos reales
					GROUP BY k.ProductId, k.WarehouseId, k.BatchSerialId, 
							k.AffectInventory, k.MovementType, 
							k.EntityName, k.EntityId
				) k
				LEFT JOIN
				(
					SELECT	ev.Id,
							evd.ProductId,
							evdbs.BatchSerialId, 
							SUM(evd.Quantity * IIF(@IVACost = 1, ROUND(red.UnitValue * (1 + red.IvaPercentage / 100), 2), ROUND(red.UnitValue, 2))) Value
					FROM Inventory.EntranceVoucher ev WITH (NOLOCK)
					JOIN Inventory.EntranceVoucherDetail evd WITH (NOLOCK) ON ev.Id = evd.EntranceVoucherId
					JOIN Inventory.EntranceVoucherDetailBatchSerial evdbs WITH (NOLOCK) ON evd.Id = evdbs.EntranceVoucherDetailId
					JOIN Inventory.RemissionEntranceDetailBatchSerial redbs WITH (NOLOCK) ON evd.RemissionEntranceDetailBatchSerialId = redbs.Id
					JOIN Inventory.RemissionEntranceDetail red WITH (NOLOCK) ON redbs.RemissionEntranceDetailId = red.Id
					WHERE evd.EntranceSource = 4
					GROUP BY ev.Id, evd.ProductId, evdbs.BatchSerialId
				) evd ON k.AffectInventory = 0 AND k.EntityName = 'EntranceVoucher' AND k.EntityId = evd.Id AND k.ProductId = evd.ProductId AND ISNULL(k.BatchSerialId, 0) = ISNULL(evd.BatchSerialId, 0)
				GROUP BY k.ProductId, IIF(@ReportType = 1, NULL, k.WarehouseId), k.BatchSerialId
			) cmi

			FULL JOIN
			(
					SELECT	k.ProductId,
							IIF(@ReportType = 1, NULL, k.WarehouseId) WarehouseId,
							k.BatchSerialId,
						SUM(k.Quantity * IIF(k.MovementType = 1, 1, -1)) Quantity,
						SUM((k.Value - ISNULL(evd.Value, 0)) * IIF(k.MovementType = 1, 1, -1)) CostTotal
				FROM 
				(
					SELECT	k.ProductId, 
							k.WarehouseId,
							k.BatchSerialId,
							k.AffectInventory,
							k.MovementType,
							k.EntityName, k.EntityId,
							SUM(IIF(k.AffectInventory = 1, k.Quantity, 0)) Quantity,
							SUM(IIF(k.Quantity = 0 AND k.EntityName = 'JournalVouchers', 1, k.Quantity) * ip.ProductCost) Value
					FROM Inventory.Kardex k WITH (NOLOCK)
					JOIN Inventory.InventoryProduct ip WITH(NOLOCK) on k.ProductId =ip.Id
					WHERE (YEAR(k.DocumentDate) = @YearReport AND MONTH(k.DocumentDate) = @MonthReport)
					AND k.Quantity > 0 -- evita acumulaciones falsas
					AND k.AffectInventory = 1 -- solo movimientos reales
					GROUP BY k.ProductId, k.WarehouseId, k.BatchSerialId, 
							k.AffectInventory, k.MovementType, 
							k.EntityName, k.EntityId
				) k
				LEFT JOIN
				(
					SELECT	ev.Id,
							evd.ProductId,
							evdbs.BatchSerialId, 
							SUM(evd.Quantity * IIF(@IVACost = 1, ROUND(red.UnitValue * (1 + red.IvaPercentage / 100), 2), ROUND(red.UnitValue, 2))) Value
					FROM Inventory.EntranceVoucher ev WITH (NOLOCK)
					JOIN Inventory.EntranceVoucherDetail evd WITH (NOLOCK) ON ev.Id = evd.EntranceVoucherId
					JOIN Inventory.EntranceVoucherDetailBatchSerial evdbs WITH (NOLOCK) ON evd.Id = evdbs.EntranceVoucherDetailId
					JOIN Inventory.RemissionEntranceDetailBatchSerial redbs WITH (NOLOCK) ON evd.RemissionEntranceDetailBatchSerialId = redbs.Id
					JOIN Inventory.RemissionEntranceDetail red WITH (NOLOCK) ON redbs.RemissionEntranceDetailId = red.Id
					WHERE evd.EntranceSource = 4
					GROUP BY ev.Id, evd.ProductId, evdbs.BatchSerialId
				) evd ON k.AffectInventory = 0 AND k.EntityName = 'EntranceVoucher' AND k.EntityId = evd.Id AND k.ProductId = evd.ProductId AND ISNULL(k.BatchSerialId, 0) = ISNULL(evd.BatchSerialId, 0)
				GROUP BY k.ProductId, IIF(@ReportType = 1, NULL, k.WarehouseId), k.BatchSerialId
			) k ON cmi.ProductId = k.ProductId AND ISNULL(cmi.WarehouseId, 0) = ISNULL(k.WarehouseId, 0) AND ISNULL(cmi.BatchSerialId, 0) = ISNULL(k.BatchSerialId, 0)

			/***************************************** MAYORIZACION PRODUCTO *****************************************/
			
			INSERT INTO @TableProductCost
				SELECT ProductId, SUM(Quantity) Quantity, SUM(CostTotal) CostTotal
				FROM @TableInventory
				GROUP BY ProductId

			/*********************************************** RESULTADO ***********************************************/

			SELECT	w.Code WarehouseCode,
					w.Name WarehouseName,
					pg.Code ProductGroupCode,
					pg.Name ProductGroupName,
					ip.Code ProductCode,
					ip.Name ProductName,
					bs.BatchCode,
					bs.ExpirationDate,
					imu.Code MeasurementUnitCode,
					imu.Name MeasurementUnit,
					atc.Concentration,
					------------ DATA ------------
					ti.Quantity,
					c.Abbreviation as CurrencyAbbreviation,
					ROUND(Common.CurrencyConverterWithDate(IIF(tpc.Quantity = 0, 0, tpc.CostTotal / tpc.Quantity),@OfficialCurrency,@ToCurrency,@ConvertDate ),2) ProductCost,
					ROUND(Common.CurrencyConverterWithDate(IIF(tpc.Quantity = 0, ti.CostTotal, ti.Quantity * IIF(tpc.Quantity = 0, 0, tpc.CostTotal / tpc.Quantity)),@OfficialCurrency,@ToCurrency,@ConvertDate ),2) CostTotal
			FROM @TableProductCost tpc
			JOIN @TableInventory ti	ON tpc.ProductId = ti.ProductId
			JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON ti.ProductId = ip.Id
			JOIN Inventory.ProductGroup pg WITH (NOLOCK) ON ip.ProductGroupId = pg.Id
			LEFT JOIN Inventory.Warehouse w WITH (NOLOCK) ON ti.WarehouseId = w.Id
			LEFT JOIN Inventory.BatchSerial bs WITH (NOLOCK) ON ti.BatchSerialId = bs.Id
			LEFT JOIN Inventory.InventoryMeasurementUnit imu WITH (NOLOCK) ON ip.MeasurementUnitId = imu.Id
			LEFT JOIN Inventory.ATC atc WITH (NOLOCK) ON ip.ATCId = atc.Id
			LEFT JOIN Common.Currency c on c.Id = @ToCurrency
			WHERE
				(@ReportType = 1 OR (w.Code BETWEEN ISNULL(@InitialWarehouse,'0') AND ISNULL(@FinalWarehouse,'99999999999999999999')))
				AND pg.Code BETWEEN ISNULL(@InitialGroup,'0') AND ISNULL(@FinalGroup,'99999999999999999999')
				AND 
				(
					ti.Quantity <> 0 
					OR 
					(tpc.Quantity = 0 AND ABS(CAST(tpc.CostTotal AS DECIMAL(18,2))) > 0.01 AND ti.CostTotal <> 0)
				)
			ORDER BY 1, 3, 5
	END TRY
	BEGIN CATCH	
		SELECT '999' CodeResult, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)) MessageResult
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el informe valorizado de inventario al cierre de mes para un período (año/mes) seleccionado. Consolida saldos de productos por bodega, lote y serie combinando los cierres de período registrados en ClosedMonth con los movimientos del kardex (entradas y salidas de mercancía), aplicando costos promedio de productos y el valor real de compra desde los comprobantes de entrada (EntranceVoucher/EntranceVoucherDetail), con opción de incluir el IVA en el costo según la configuración del módulo (SettingInventory). Convierte los valores a la moneda deseada usando la TRM vigente al último día del mes cerrado o a la fecha actual si el período está abierto, y filtra por rango de bodegas y grupos de productos. Se usa para reportería contable y de control de inventarios al cierre mensual.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_ReportCloseMonth';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_ReportCloseMonth';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el informe valorizado de inventario al cierre de mes, calculando saldos en cantidad y costo por producto/bodega/lote, convertidos a la moneda solicitada según la TRM de la fecha de cierre.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportCloseMonth';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Los filtros (Year, Month, ReportType, InitialWarehouse, FinalWarehouse, InitialGroup, FinalGroup, ToCurrency) deben venir en el XML de entrada bajo el nodo /Data.; Si el mes/año solicitado está cerrado en Inventory.ClosedMonth, debe existir una TRM en Common.TRM para el último día de ese mes cuando exista más de una OfficialCurrencyId en GeneralLedger.LegalBook.; Si el mes/año solicitado NO está cerrado, debe coincidir con el mes y año actuales para poder generar el reporte.; Debe existir configuración en Inventory.SettingInventory (IVACost) y en GeneralLedger.CompanySettings (OfficialCurrencyId).', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportCloseMonth';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran movimientos del Kardex con Quantity > 0 y AffectInventory = 1 (movimientos reales) para el cálculo de saldos.; El saldo inicial proviene de movimientos con DocumentDate anterior al mes/año del reporte; el movimiento del periodo proviene de DocumentDate dentro del mes/año del reporte.; Los movimientos con MovementType = 1 suman cantidad/valor; el resto resta (signo -1).; Para entradas de Inventory.EntranceVoucher con EntranceSource = 4 se descuenta el valor calculado desde RemissionEntranceDetail al valor del Kardex (k.Value - evd.Value).; El CostTotal acumulado nunca queda negativo (se fuerza a 0 cuando la suma daría negativo).; Solo se incluyen filas con cantidad distinta de 0, o con cantidad total del producto = 0 pero costo absoluto > 0.01 y CostTotal del registro distinto de 0.; Todos los valores monetarios del resultado se convierten desde la moneda oficial (@OfficialCurrency) a @ToCurrency usando la TRM de @ConvertDate.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportCloseMonth';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Resultado tabular: Devuelve el inventario valorizado por bodega/grupo/producto/lote con cantidad, costo unitario y costo total convertidos a la moneda destino mediante Common.CurrencyConverterWithDate, ordenado por bodega, grupo y producto.; [RETURN_RESULT] Resultado tabular: Cuando el mes está cerrado y no existe TRM para el último día del mes y hay múltiples OfficialCurrencyId en LegalBook, retorna CodeResult=''999'' con el ERROR_MESSAGE.; [RETURN_RESULT] Resultado tabular: Cuando el mes/año no está cerrado y no coincide con el mes/año actual, retorna CodeResult=''999'' con MessageResult=''No existe datos para la fecha seleccionada''.; [RAISERROR] Resultado tabular: Cualquier excepción capturada en el TRY/CATCH retorna CodeResult=''999'' con ERROR_MESSAGE() y la línea del error.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportCloseMonth';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si EXISTS en Inventory.ClosedMonth para (Year=@YearReport, Month=@MonthReport) → Toma @ConvertDate = EOMONTH(primer día del mes/año solicitado) y valida existencia de TRM para esa fecha. else Si el mes/año coincide con el actual usa @ConvertDate = fecha actual; si no, retorna error 999 ''No existe datos para la fecha seleccionada''.; si @ReportType = 1 → Agrupa el inventario ignorando la bodega (WarehouseId se fuerza a NULL) y omite el filtro de rango de bodegas en el resultado final. else Conserva la WarehouseId y filtra resultados por w.Code BETWEEN @InitialWarehouse y @FinalWarehouse.; si @IVACost = 1 (configuración Inventory.SettingInventory) → El valor de las entradas de Inventory.EntranceVoucher se calcula con UnitValue * (1 + IvaPercentage/100) redondeado a 2 decimales. else El valor de las entradas se calcula con UnitValue redondeado a 2 decimales sin IVA.; si @ToCurrency es NULL → Se asigna @ToCurrency = Id de GeneralLedger.CompanySettings (moneda por defecto de la empresa).; si ISNULL(cmi.CostTotal,0) + ISNULL(k.CostTotal,0) < 0 → El CostTotal acumulado se fija a 0 (no se permiten costos totales negativos en el saldo).; si tpc.Quantity = 0 en la mayorización por producto → El costo unitario del producto (ProductCost) se reporta como 0 y el CostTotal de salida usa ti.CostTotal directo en lugar de Quantity * costo unitario. else ProductCost = tpc.CostTotal / tpc.Quantity y CostTotal = ti.Quantity * ProductCost.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportCloseMonth';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE; Common.CurrencyConverterWithDate', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportCloseMonth';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.ClosedMonth; Common.TRM; GeneralLedger.LegalBook; Inventory.SettingInventory; GeneralLedger.CompanySettings; Inventory.Kardex; Inventory.InventoryProduct; Inventory.EntranceVoucher; Inventory.EntranceVoucherDetail; Inventory.EntranceVoucherDetailBatchSerial; Inventory.RemissionEntranceDetailBatchSerial; Inventory.RemissionEntranceDetail; Inventory.ProductGroup; Inventory.Warehouse; Inventory.BatchSerial; Inventory.InventoryMeasurementUnit; Inventory.ATC; Common.Currency', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportCloseMonth';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportCloseMonth';
-- GO
