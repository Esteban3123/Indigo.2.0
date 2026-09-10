-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-03-02
-- Description:	Procedimiento para el reporte de cuenta fiscal detallado
-- =============================================
CREATE PROCEDURE [Inventory].[SP_ReportFiscalAccountDetailed]
	@xmlCriterias AS XML
AS
BEGIN
	SET NOCOUNT ON

	DECLARE	@Year INT,
			@Month INT,
			@MovementType TINYINT,
			@GroupBy TINYINT,
			@IncludeWarehouseTransferOrder BIT,
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
		EntityCode VARCHAR(20),
		EntityName VARCHAR(250),		
		AffectInventory INT,
		Value DECIMAL(20,4)
	)

	BEGIN TRY
		
		/********************************** CRITERIOS Y FILTROS **********************************/

		SELECT	@Year = t.x.value('Year[1]','int'),
				@Month = t.x.value('Month[1]','int'),
				@MovementType = t.x.value('MovementType[1]','tinyint'),
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
			SELECT	k.ProductId, k.WarehouseId, k.BatchSerialId,
					k.EntityCode, k.EntityName, k.AffectInventory,
					SUM(k.Value - ISNULL(evd.Value, 0)) Value					
			FROM 
			(
				SELECT	k.ProductId, k.WarehouseId, k.BatchSerialId,
						k.AffectInventory,
						k.EntityId, k.EntityCode, k.EntityName, 
						SUM(IIF(k.Quantity = 0 AND k.EntityName = 'JournalVouchers', 1, k.Quantity) * k.Value) Value
				FROM Inventory.Kardex k WITH (NOLOCK)
				WHERE (YEAR(k.DocumentDate) = @Year AND MONTH(k.DocumentDate) = @Month) AND k.MovementType = @MovementType
				GROUP BY k.ProductId, k.WarehouseId, k.BatchSerialId, 
						k.AffectInventory,
						k.EntityName, k.EntityId, k.EntityCode
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
			GROUP BY k.ProductId, k.WarehouseId, k.BatchSerialId,
					k.EntityCode, k.EntityName, k.AffectInventory

		/*********************************************** RESULTADO ***********************************************/

		SELECT	w.Code WarehouseCode,
				w.Name WarehouseName,
				IIF(@GroupBy IN (1, 3), pg.Code, NULL) GroupCode,
				IIF(@GroupBy IN (1, 3), pg.Name, NULL) GroupName,
				IIF(@GroupBy IN (2, 3), psg.Code, NULL) SubGroupCode,
				IIF(@GroupBy IN (2, 3), psg.Name, NULL) SubGroupName,
				ti.EntityCode, 
				gend.Description EntityName,
				CONCAT(gend.Description, ' - Nro.: ', ti.EntityCode, IIF(MIN(ti.AffectInventory) = 0, ' - Con cruce de remisiones', '')) Description,
				SUM(ti.Value) Value
		FROM @TableInventory ti
		JOIN Inventory.Warehouse w WITH (NOLOCK) ON ti.WarehouseId = w.Id
		JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON ti.ProductId = ip.Id
		JOIN Inventory.ProductGroup pg WITH (NOLOCK) ON ip.ProductGroupId = pg.Id
		JOIN Inventory.ProductSubGroup psg WITH (NOLOCK) ON ip.ProductSubGroupId = psg.Id
		LEFT JOIN Common.GetEntityNameDescriptions() gend ON ti.EntityName = gend.EntityName
		LEFT JOIN @Table_Warehouses tw ON w.Id = tw.Id
		LEFT JOIN @Table_Groups tg ON pg.Id = tg.Id
		LEFT JOIN @Table_SubGroups tsg ON psg.Id = tsg.Id
		WHERE   (@FilterByWarehouses = 0 OR tw.Id IS NOT NULL)
			AND (@FilterByGroups = 0 OR tg.Id IS NOT NULL)
			AND (@FilterBySubGroups = 0 OR tsg.Id IS NOT NULL)
		GROUP BY w.Code, w.Name,
				IIF(@GroupBy IN (1, 3), pg.Code, NULL), IIF(@GroupBy IN (1, 3), pg.Name, NULL),
				IIF(@GroupBy IN (2, 3), psg.Code, NULL), IIF(@GroupBy IN (2, 3), psg.Name, NULL),
				ti.EntityCode, ti.EntityName, gend.Description
		ORDER BY 1, 3, 5	
	END TRY
	BEGIN CATCH	
		SELECT '999' CodeResult, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)) MessageResult
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el reporte de cuenta fiscal detallado del módulo de inventario para un período (año y mes) y tipo de movimiento específicos. Consulta el kardex de movimientos para obtener los valores de entradas y salidas de productos, cruzando con los comprobantes de entrada (vales de ingreso) y remisiones cuando aplica, para ajustar el costo unitario según la configuración del IVA en costo definida en los parámetros del módulo. Permite filtrar por bodegas, grupos y subgrupos de productos, y agrupar los resultados por grupo o subgrupo, mostrando para cada documento (factura, orden, remisión, etc.) el código del documento, la bodega, la clasificación del producto y el valor total afectado al inventario. Se usa en la gestión contable y fiscal del inventario para conciliar movimientos y valores por comprobante en un período contable.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_ReportFiscalAccountDetailed';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_ReportFiscalAccountDetailed';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el reporte detallado de cuenta fiscal de inventario para un mes/año y tipo de movimiento, valorando los movimientos del kardex y descontando entradas legalizadas por remisión, con agrupación opcional por grupo/subgrupo y filtros por bodega.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportFiscalAccountDetailed';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML @xmlCriterias debe contener los nodos Year, Month, MovementType, GroupBy, IncludeWarehouseTransferOrder y opcionalmente Warehouses/Groups/SubGroups (listas separadas por coma de IDs enteros).; Debe existir al menos un registro en Inventory.SettingInventory para leer el flag IVACost.; Las listas de filtros (Warehouses, Groups, SubGroups) deben ser convertibles a INT por dbo.Split.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportFiscalAccountDetailed';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran movimientos del kardex cuyo YEAR(DocumentDate)=@Year, MONTH(DocumentDate)=@Month y MovementType=@MovementType.; El cruce contra EntranceVoucher para descontar valor legalizado solo aplica cuando EntranceSource=4 (origen remisión).; Las órdenes de traslado con OrderType=1 y DispatchTo=1 se excluyen siempre que @IncludeWarehouseTransferOrder=0.; Los filtros por bodega/grupo/subgrupo se aplican únicamente si el respectivo flag de filtrado está activo; en caso contrario no restringen.; Todas las consultas se ejecutan con WITH (NOLOCK), aceptando lecturas sucias.; Los errores no se relanzan: se capturan en CATCH y se devuelven como fila con CodeResult=''999''.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportFiscalAccountDetailed';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cuenta fiscal de inventario; Kardex; Comprobante de entrada (EntranceVoucher); Remisión de entrada; Orden de traslado entre bodegas; Costo con/sin IVA; Grupo y subgrupo de productos; Bodega/almacén; Cruce de remisiones; Lote y serial; Tipo de movimiento de inventario', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportFiscalAccountDetailed';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Resultset: Devuelve un conjunto con WarehouseCode/Name, GroupCode/Name (si @GroupBy IN (1,3)), SubGroupCode/Name (si @GroupBy IN (2,3)), EntityCode, EntityName, Description y Value (suma de valores del kardex menos valor legalizado de la remisión cuando aplica).; [RETURN_RESULT] Resultset: Si ocurre una excepción, devuelve un único registro con CodeResult=''999'' y MessageResult=ERROR_MESSAGE()+'' - Linea: ''+ERROR_LINE() en lugar del reporte.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportFiscalAccountDetailed';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ISNULL(@Warehouses,'''') <> '''' → Activa @FilterByWarehouses=1 y carga IDs en @Table_Warehouses para restringir el resultado solo a esas bodegas. else No se filtra por bodegas.; si ISNULL(@Groups,'''') <> '''' → Activa @FilterByGroups=1 y carga IDs en @Table_Groups para restringir el resultado a esos grupos de producto. else No se filtra por grupos.; si ISNULL(@SubGroups,'''') <> '''' → Activa @FilterBySubGroups=1 y carga IDs en @Table_SubGroups para restringir el resultado a esos subgrupos. else No se filtra por subgrupos.; si @IVACost = 1 (leído de Inventory.SettingInventory) → Para el cruce con remisiones, valora cada unidad como ROUND(UnitValue*(1+IvaPercentage/100),2), incluyendo IVA en el costo. else Valora cada unidad como ROUND(UnitValue,2), sin IVA.; si k.Quantity = 0 AND k.EntityName = ''JournalVouchers'' → Trata la cantidad como 1 al multiplicar por el valor (los comprobantes contables con cantidad cero igualmente aportan su Value). else Multiplica Quantity * Value normalmente.; si @IncludeWarehouseTransferOrder = 1 → Incluye todos los movimientos del kardex sin excluir órdenes de traslado. else Excluye movimientos cuya EntityName=''TransferOrder'' y correspondan a una TransferOrder con OrderType=1 y DispatchTo=1.; si k.AffectInventory = 0 AND k.EntityName = ''EntranceVoucher'' → Cruza el movimiento con el detalle de EntranceVoucher cuyo EntranceSource=4 para restar el valor ya legalizado por remisión (evd.Value). else No se aplica el descuento por cruce de remisiones.; si @GroupBy IN (1,3) → Incluye en el resultado y la agrupación el código y nombre del ProductGroup. else GroupCode y GroupName se devuelven como NULL.; si @GroupBy IN (2,3) → Incluye en el resultado y la agrupación el código y nombre del ProductSubGroup. else SubGroupCode y SubGroupName se devuelven como NULL.; si MIN(ti.AffectInventory) = 0 dentro del grupo final → Concatena el sufijo '' - Con cruce de remisiones'' a la descripción del registro. else La descripción no incluye ese sufijo.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportFiscalAccountDetailed';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.Split; Common.GetEntityNameDescriptions', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportFiscalAccountDetailed';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.SettingInventory; Inventory.Kardex; Inventory.EntranceVoucher; Inventory.EntranceVoucherDetail; Inventory.EntranceVoucherDetailBatchSerial; Inventory.RemissionEntranceDetailBatchSerial; Inventory.RemissionEntranceDetail; Inventory.TransferOrder; Inventory.Warehouse; Inventory.InventoryProduct; Inventory.ProductGroup; Inventory.ProductSubGroup', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportFiscalAccountDetailed';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportFiscalAccountDetailed';
-- GO
