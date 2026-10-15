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