-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-03-02
-- Description:	Procedimiento para el reporte de cuenta fiscal
-- =============================================
CREATE PROCEDURE [Inventory].[SP_ReportFiscalAccountSummary]
	@xmlCriterias AS XML
AS
BEGIN
	SET NOCOUNT ON

	DECLARE	@Year INT,
			@Month INT,
			@IncludeWarehouseTransferOrder BIT,
			@GroupBy TINYINT,
			@Warehouses VARCHAR(MAX),
			@Groups VARCHAR(MAX),
			@SubGroups VARCHAR(MAX),
			-------------
			@FilterByWarehouses BIT = 0,
			@FilterByGroups BIT = 0,
			@FilterBySubGroups BIT = 0,
			@IVACost BIT

	DECLARE @Table_Warehouses AS TABLE(Id INT)
	DECLARE @Table_Groups AS TABLE(Id INT)
	DECLARE @Table_SubGroups AS TABLE(Id INT)

	DECLARE @TableInventory TABLE
	(
		ProductId INT,
		WarehouseId INT,
		BatchSerialId INT,
		PreviousBalance DECIMAL(20,4),
		PhysicalInventory INT,
		MovementIn DECIMAL(20,4),
		MovementOut DECIMAL(20,4)
	)

	BEGIN TRY
		
		/********************************** CRITERIOS Y FILTROS **********************************/

		SELECT	@Year = t.x.value('Year[1]','int'),
				@Month = t.x.value('Month[1]','int'),
				@GroupBy = t.x.value('GroupBy[1]','tinyint'),
				@IncludeWarehouseTransferOrder = t.x.value('IncludeWarehouseTransferOrder[1]','bit'),				
				@Warehouses = t.x.value('Warehouses[1]','varchar(max)'),
				@Groups = t.x.value('Groups[1]','varchar(max)'),
				@SubGroups = t.x.value('SubGroups[1]','varchar(max)')
		FROM @xmlCriterias.nodes('/Data') t(x)

		IF ISNULL(@Warehouses, '') <> ''
		BEGIN
			SET @FilterByWarehouses = 1

			INSERT INTO @Table_Warehouses
				SELECT CAST(Data AS INT) Data 
				FROM dbo.Split(@Warehouses, ',')
		END

		IF ISNULL(@Groups, '') <> ''
		BEGIN
			SET @FilterByGroups = 1

			INSERT INTO @Table_Groups
				SELECT CAST(Data AS INT) Data 
				FROM dbo.Split(@Groups, ',')
		END

		IF ISNULL(@SubGroups, '') <> ''
		BEGIN
			SET @FilterBySubGroups = 1

			INSERT INTO @Table_SubGroups
				SELECT CAST(Data AS INT) Data 
				FROM dbo.Split(@SubGroups, ',')
		END

		SELECT @IVACost = IVACost FROM Inventory.SettingInventory

		/******************************************  OBTENCION DE DATOS ******************************************/

		INSERT INTO @TableInventory
			SELECT	ISNULL(cmi.ProductId, k.ProductId) ProductId,
					ISNULL(cmi.WarehouseId, k.WarehouseId) WarehouseId,
					ISNULL(cmi.BatchSerialId, k.BatchSerialId) BatchSerialId,
					ISNULL(cmi.CostTotal, 0) PreviousBalance,
					ISNULL(cmi.Quantity, 0) + ISNULL(k.Quantity, 0) PhysicalInventory,
					ISNULL(k.MovementIn, 0) MovementIn,
					ISNULL(k.MovementOut, 0) MovementOut
			FROM 
			(
				SELECT	k.ProductId, k.WarehouseId, k.BatchSerialId,
						SUM(k.Quantity * IIF(k.MovementType = 1, 1, -1)) Quantity,
						SUM((k.Value - ISNULL(evd.Value, 0)) * IIF(k.MovementType = 1, 1, -1)) CostTotal
				FROM 
				(
					SELECT	k.ProductId, k.WarehouseId, k.BatchSerialId,
							k.AffectInventory, k.MovementType,
							k.EntityName, k.EntityId,
							SUM(IIF(k.AffectInventory = 1, k.Quantity, 0)) Quantity, SUM(IIF(k.Quantity = 0 AND k.EntityName = 'JournalVouchers', 1, k.Quantity) * k.Value) Value
					FROM Inventory.Kardex k WITH (NOLOCK)
					WHERE 
					(
						(YEAR(k.DocumentDate) = @Year AND MONTH(k.DocumentDate) < @Month)
						OR
						(YEAR(k.DocumentDate) < @Year)
					)
					GROUP BY k.ProductId, k.WarehouseId, k.BatchSerialId, 
							k.AffectInventory, k.MovementType, 
							k.EntityName, k.EntityId
				) k
				LEFT JOIN
				(
					SELECT	ev.Id, evd.ProductId, evdbs.BatchSerialId, 
							SUM(evd.Quantity * IIF(@IVACost = 1, ROUND(red.UnitValue * (1 + red.IvaPercentage / 100), 2), ROUND(red.UnitValue, 2))) Value
					FROM Inventory.EntranceVoucher ev WITH (NOLOCK)
					JOIN Inventory.EntranceVoucherDetail evd WITH (NOLOCK) ON ev.Id = evd.EntranceVoucherId
					JOIN Inventory.EntranceVoucherDetailBatchSerial evdbs WITH (NOLOCK) ON evd.Id = evdbs.EntranceVoucherDetailId
					JOIN Inventory.RemissionEntranceDetailBatchSerial redbs WITH (NOLOCK) ON evd.RemissionEntranceDetailBatchSerialId = redbs.Id
					JOIN Inventory.RemissionEntranceDetail red WITH (NOLOCK) ON redbs.RemissionEntranceDetailId = red.Id
					WHERE evd.EntranceSource = 4
					GROUP BY ev.Id, evd.ProductId, evdbs.BatchSerialId
				) evd ON k.AffectInventory = 0 AND k.EntityName = 'EntranceVoucher' AND k.EntityId = evd.Id AND k.ProductId = evd.ProductId AND ISNULL(k.BatchSerialId, 0) = ISNULL(evd.BatchSerialId, 0)
				WHERE 
				(
					@IncludeWarehouseTransferOrder = 1 
					OR
					k.EntityName <> 'TransferOrder'
					OR
					(
						k.EntityName = 'TransferOrder'
						AND
						NOT EXISTS (SELECT 1 FROM Inventory.TransferOrder tro WHERE k.EntityId = tro.Id AND tro.OrderType = 1 AND tro.DispatchTo = 1)
					)
				)
				GROUP BY k.ProductId, k.WarehouseId, k.BatchSerialId
			) cmi
			FULL JOIN
			(
				SELECT	k.ProductId, k.WarehouseId, k.BatchSerialId,
						SUM(k.Quantity * IIF(k.MovementType = 1, 1, -1)) Quantity,
						SUM(IIF(k.MovementType = 1, (k.Value - ISNULL(evd.Value, 0)), 0)) MovementIn,
						SUM(IIF(k.MovementType = 1, 0, (k.Value - ISNULL(evd.Value, 0)))) MovementOut
				FROM 
				(
					SELECT	k.ProductId, k.WarehouseId, k.BatchSerialId,
							k.AffectInventory, k.MovementType,
							k.EntityName, k.EntityId,
							SUM(IIF(k.AffectInventory = 1, k.Quantity, 0)) Quantity, SUM(IIF(k.Quantity = 0 AND k.EntityName = 'JournalVouchers', 1, k.Quantity) * k.Value) Value
					FROM Inventory.Kardex k WITH (NOLOCK)
					WHERE (YEAR(k.DocumentDate) = @Year AND MONTH(k.DocumentDate) = @Month)
					GROUP BY k.ProductId, k.WarehouseId, k.BatchSerialId, 
							k.AffectInventory, k.MovementType, 
							k.EntityName, k.EntityId
				) k
				LEFT JOIN
				(
					SELECT	ev.Id, evd.ProductId, evdbs.BatchSerialId, 
							SUM(evd.Quantity * IIF(@IVACost = 1, ROUND(red.UnitValue * (1 + red.IvaPercentage / 100), 2), ROUND(red.UnitValue, 2))) Value
					FROM Inventory.EntranceVoucher ev WITH (NOLOCK)
					JOIN Inventory.EntranceVoucherDetail evd WITH (NOLOCK) ON ev.Id = evd.EntranceVoucherId
					JOIN Inventory.EntranceVoucherDetailBatchSerial evdbs WITH (NOLOCK) ON evd.Id = evdbs.EntranceVoucherDetailId
					JOIN Inventory.RemissionEntranceDetailBatchSerial redbs WITH (NOLOCK) ON evd.RemissionEntranceDetailBatchSerialId = redbs.Id
					JOIN Inventory.RemissionEntranceDetail red WITH (NOLOCK) ON redbs.RemissionEntranceDetailId = red.Id
					WHERE evd.EntranceSource = 4
					GROUP BY ev.Id, evd.ProductId, evdbs.BatchSerialId
				) evd ON k.AffectInventory = 0 AND k.EntityName = 'EntranceVoucher' AND k.EntityId = evd.Id AND k.ProductId = evd.ProductId AND ISNULL(k.BatchSerialId, 0) = ISNULL(evd.BatchSerialId, 0)
				WHERE
				(
					@IncludeWarehouseTransferOrder = 1 
					OR
					k.EntityName <> 'TransferOrder'
					OR
					(
						k.EntityName = 'TransferOrder'
						AND
						NOT EXISTS (SELECT 1 FROM Inventory.TransferOrder tro WHERE k.EntityId = tro.Id AND tro.OrderType = 1 AND tro.DispatchTo = 1)
					)
				)
				GROUP BY k.ProductId, k.WarehouseId, k.BatchSerialId
			) k ON cmi.ProductId = k.ProductId AND ISNULL(cmi.WarehouseId, 0) = ISNULL(k.WarehouseId, 0) AND ISNULL(cmi.BatchSerialId, 0) = ISNULL(k.BatchSerialId, 0)

		/*********************************************** RESULTADO ***********************************************/

		SELECT	w.Code WarehouseCode,
				w.Name WarehouseName,
				IIF(@GroupBy IN (1, 3), pg.Code, NULL) GroupCode,
				IIF(@GroupBy IN (1, 3), pg.Name, NULL) GroupName,
				IIF(@GroupBy IN (2, 3), psg.Code, NULL) SubGroupCode,
				IIF(@GroupBy IN (2, 3), psg.Name, NULL) SubGroupName,
				SUM(ti.PreviousBalance) PreviousBalance,
				SUM(ti.PhysicalInventory) PhysicalInventory,
				SUM(ti.MovementIn) MovementIn,
				SUM(ti.MovementOut) MovementOut,
				SUM(ti.PreviousBalance + ti.MovementIn - ti.MovementOut) Balance
		FROM @TableInventory ti
		JOIN Inventory.Warehouse w WITH (NOLOCK) ON ti.WarehouseId = w.Id
		JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON ti.ProductId = ip.Id
		JOIN Inventory.ProductGroup pg WITH (NOLOCK) ON ip.ProductGroupId = pg.Id
		JOIN Inventory.ProductSubGroup psg WITH (NOLOCK) ON ip.ProductSubGroupId = psg.Id
		LEFT JOIN @Table_Warehouses tw ON w.Id = tw.Id
		LEFT JOIN @Table_Groups tg ON pg.Id = tg.Id
		LEFT JOIN @Table_SubGroups tsg ON psg.Id = tsg.Id
		WHERE   (@FilterByWarehouses = 0 OR tw.Id IS NOT NULL)
			AND (@FilterByGroups = 0 OR tg.Id IS NOT NULL)
			AND (@FilterBySubGroups = 0 OR tsg.Id IS NOT NULL)
		GROUP BY w.Code, w.Name,
				IIF(@GroupBy IN (1, 3), pg.Code, NULL), IIF(@GroupBy IN (1, 3), pg.Name, NULL),
				IIF(@GroupBy IN (2, 3), psg.Code, NULL), IIF(@GroupBy IN (2, 3), psg.Name, NULL)
		ORDER BY 1, 3, 5
	END TRY
	BEGIN CATCH	
		SELECT '999' CodeResult, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)) MessageResult
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el reporte de resumen de cuenta fiscal de inventario para un año y mes determinados. Consolida los saldos anteriores, el inventario físico y los movimientos de entrada y salida de productos (por bodega, lote o serial) consultando el kardex de inventario, los comprobantes de entrada y sus detalles de lotes, así como las remisiones de entrada. Permite filtrar por bodegas, grupos y subgrupos de productos, opcionalmente incluir órdenes de traslado entre bodegas, y considera la configuración del módulo de inventario para determinar si el IVA se incluye en el costo. Es el soporte principal para la reportería contable y fiscal del inventario en un período cerrado.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_ReportFiscalAccountSummary';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_ReportFiscalAccountSummary';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un resumen de cuenta fiscal de inventario por bodega (y opcionalmente por grupo/subgrupo) con saldo anterior, inventario físico y movimientos de entrada/salida valorizados para un mes y año dados, ajustando el costo según configuración de IVA.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportFiscalAccountSummary';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML @xmlCriterias debe contener nodo /Data con Year, Month, GroupBy, IncludeWarehouseTransferOrder y opcionalmente listas separadas por coma de Warehouses, Groups y SubGroups.; Inventory.SettingInventory debe contener un registro con la bandera IVACost (se lee con SELECT escalar sin filtro).; Las tablas Inventory.Warehouse, InventoryProduct, ProductGroup y ProductSubGroup deben tener registros consistentes con los IDs presentes en Kardex (los JOIN son INNER).; La función dbo.Split debe existir y aceptar (cadena, separador) devolviendo columna Data convertible a INT.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportFiscalAccountSummary';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El saldo anterior (PreviousBalance) se calcula con movimientos de Kardex anteriores al mes/año del criterio (YEAR<@Year o YEAR=@Year AND MONTH<@Month).; Los movimientos del período son los de YEAR=@Year AND MONTH=@Month.; Solo afectan el inventario físico los registros de Kardex con AffectInventory=1.; Para EntranceVoucher con AffectInventory=0, el valor se ajusta restando el costo calculado desde RemissionEntranceDetail (con o sin IVA según SettingInventory.IVACost) sólo cuando EntranceSource=4.; El balance final se calcula como PreviousBalance + MovementIn - MovementOut.; El reporte agrupa siempre por bodega; la inclusión de grupo y subgrupo depende de @GroupBy (1=grupo, 2=subgrupo, 3=ambos).; Las órdenes de traslado tipo 1 con destino tipo 1 se excluyen cuando @IncludeWarehouseTransferOrder=0.; Cualquier excepción se captura y devuelve un resultset con CodeResult=''999'' y mensaje + línea de error en lugar de propagar la excepción.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportFiscalAccountSummary';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cuenta fiscal de inventario; Kardex; Saldo anterior (PreviousBalance); Inventario físico; Movimientos de entrada y salida; Comprobante de entrada (EntranceVoucher); Remisión de entrada; Costo con IVA / sin IVA; Orden de traslado entre bodegas; Comprobante contable (JournalVouchers); Grupos y subgrupos de producto; Bodega/almacén', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportFiscalAccountSummary';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Resultset: Devuelve un resultset agrupado por bodega (y grupo/subgrupo según @GroupBy) con PreviousBalance, PhysicalInventory, MovementIn, MovementOut y Balance = PreviousBalance + MovementIn - MovementOut.; [RETURN_RESULT] Resultset: En caso de error en el TRY, devuelve un único registro con CodeResult=''999'' y MessageResult con ERROR_MESSAGE() + '' - Linea: '' + ERROR_LINE().', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportFiscalAccountSummary';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ISNULL(@Warehouses,'''') <> '''' → Activa filtro por bodegas y carga IDs en tabla temporal vía dbo.Split else No se filtra por bodega; si ISNULL(@Groups,'''') <> '''' → Activa filtro por grupos de producto else No se filtra por grupo; si ISNULL(@SubGroups,'''') <> '''' → Activa filtro por subgrupos de producto else No se filtra por subgrupo; si @IncludeWarehouseTransferOrder = 0 y k.EntityName=''TransferOrder'' con TransferOrder.OrderType=1 y DispatchTo=1 → Excluye esos movimientos de Kardex del cálculo (órdenes de traslado tipo 1 con despacho a tipo 1) else Incluye todos los movimientos de Kardex; si @IVACost = 1 (configuración de Inventory.SettingInventory) → El valor de la entrada se calcula como ROUND(UnitValue * (1 + IvaPercentage/100), 2) (costo con IVA) else Se usa ROUND(UnitValue, 2) sin IVA; si @GroupBy IN (1,3) → El resultado incluye Code/Name del ProductGroup else GroupCode/GroupName se devuelven NULL; si @GroupBy IN (2,3) → El resultado incluye Code/Name del ProductSubGroup else SubGroupCode/SubGroupName se devuelven NULL; si k.MovementType = 1 → El movimiento se considera entrada (suma cantidades y valores en MovementIn) else El movimiento se considera salida (resta cantidades y suma valores en MovementOut); si k.AffectInventory = 0 AND k.EntityName=''EntranceVoucher'' (con coincidencia en EntranceVoucherDetail con EntranceSource=4) → Se descuenta el valor calculado desde el comprobante de entrada (evd.Value) al valor del kardex para evitar doble cómputo else Se usa el valor del kardex tal cual; si k.Quantity = 0 AND k.EntityName=''JournalVouchers'' → Se trata el factor cantidad como 1 al multiplicar por Value (ajustes contables sin cantidad) else Se multiplica Quantity * Value', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportFiscalAccountSummary';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.Split', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportFiscalAccountSummary';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.SettingInventory; Inventory.Kardex; Inventory.EntranceVoucher; Inventory.EntranceVoucherDetail; Inventory.EntranceVoucherDetailBatchSerial; Inventory.RemissionEntranceDetailBatchSerial; Inventory.RemissionEntranceDetail; Inventory.TransferOrder; Inventory.Warehouse; Inventory.InventoryProduct; Inventory.ProductGroup; Inventory.ProductSubGroup', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportFiscalAccountSummary';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportFiscalAccountSummary';
-- GO
