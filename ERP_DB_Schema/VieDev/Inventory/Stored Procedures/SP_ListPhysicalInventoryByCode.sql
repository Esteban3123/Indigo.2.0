
-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2021-01-31
-- Description:	Procedimiento que se encarga de listar el inventario físico de acuerdo a una lista de codigos
-- =============================================
CREATE PROCEDURE [Inventory].[SP_ListPhysicalInventoryByCode] 
	@XmlParameters AS XML,
	@XmlATCs AS XML
AS
BEGIN
	SET NOCOUNT ON;
	
	/************************************* VARIABLES *************************************/

	DECLARE @CareGroupId INT,
			@UserId INT,
			@TotalDose DECIMAL(18,2),
			@CurrentDate DATE

	--Tabla para almacenar los items del listado que viene en el xml
	DECLARE @ATCs TABLE
	(
		Code VARCHAR(20)
	)

	--Tabla para devolver los resultados
	DECLARE @TableResult TABLE
	(
		Id INT NOT NULL,
		Code VARCHAR(20) NOT NULL,
		WarehouseId INT NOT NULL, 
		WarehouseCodeName VARCHAR(500),
		ProductId INT NOT NULL,
		ProductCodeName VARCHAR(500),
		BatchSerialId INT, 
		BatchSerialCode VARCHAR(500),
		BatchSerialExpirationDate DATE,
		Quantity INT NOT NULL,
		ClassType TINYINT NOT NULL,
		ClassName VARCHAR(20) NOT NULL,
		Dose DECIMAL(18,2),
		DoseMeasurement VARCHAR(10),
		Covered BIT DEFAULT(0) NOT NULL
	)

	BEGIN TRY

		SELECT	@CareGroupId = t.x.value('CareGroupId[1]','int'),
				@UserId = t.x.value('UserId[1]','int'),
				@TotalDose = IIF(t.x.value('TotalDose[1]','VARCHAR(20)') = '', null,REPLACE(t.x.value('TotalDose[1]','VARCHAR(20)'), ',', '.')),
				@CurrentDate = Common.GETDATE()
		FROM @XmlParameters.nodes('/Data') t(x)

		INSERT INTO @ATCs
			SELECT DISTINCT
				t.x.value('Code[1]','varchar(20)') Code
			FROM @XmlATCs.nodes('/Data') t(x)

		/*******************************************************************************************/
		INSERT INTO @TableResult 
		(
			Id, Code, WarehouseId, ProductId, ProductCodeName, BatchSerialId, Quantity, ClassType, ClassName, Dose, DoseMeasurement
		)
			 SELECT	phy.Id,
					atc.Code,
					phy.WarehouseId,
					phy.ProductId,
					CONCAT(ip.Code, ' - ', ip.Name) ProductCodeName,
					phy.BatchSerialId,
					phy.Quantity,
					pt.Class,
					CASE pt.Class
						WHEN 1 THEN 'Grupo'
						WHEN 2 THEN 'Item Medicamento'
						WHEN 3 THEN 'Item Insumo'
						WHEN 4 THEN 'Item Otro'  
						WHEN 5 THEN 'Item Producción'
					END AS ClassName,
					CASE pt.Class
						WHEN 5 THEN (SELECT TOP 1 ISNULL(ppd.Quantity, pd.Quantity) 
									FROM MixingStation.RequestPackageDetailStatus rpds
									LEFT JOIN MixingStation.PackagePersonalizedDetail ppd ON ppd.PackagePersonalizedId = rpds.PackagePersonalizedId AND ppd.MainMedicine = 1
									LEFT JOIN MixingStation.PackageDetail pd ON pd.PackageId = rpds.PackageId AND pd.MainMedicine = 1
									INNER JOIN Inventory.BatchSerial bs ON bs.BatchCode = rpds.BatchCode
									WHERE bs.Id = phy.BatchSerialId
										)
						ELSE ISNULL(atc.Weight, atc.volume)
					END AS Dose,
					CASE pt.Class
						WHEN 5 THEN (SELECT TOP 1 imu.Abbreviation 
									FROM MixingStation.RequestPackageDetailStatus rpds
									LEFT JOIN MixingStation.PackagePersonalizedDetail ppd ON ppd.PackagePersonalizedId = rpds.PackagePersonalizedId AND ppd.MainMedicine = 1
									LEFT JOIN MixingStation.PackageDetail pd ON pd.PackageId = rpds.PackageId AND pd.MainMedicine = 1
									INNER JOIN Inventory.BatchSerial bs ON bs.BatchCode = rpds.BatchCode
									INNER JOIN Inventory.InventoryMeasurementUnit imu ON imu.Id = ISNULL(ppd.MeasurementUnitId, pd.MeasurementUnitId)
									WHERE bs.Id = phy.BatchSerialId)
						ELSE imu.Abbreviation
					END AS DoseMeasurement
			 FROM Inventory.WarehouseUser wu WITH (NOLOCK)
			 JOIN Inventory.PhysicalInventory phy WITH (NOLOCK) ON wu.WarehouseId = phy.WarehouseId
			 JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON phy.ProductId = ip.Id
			 JOIN Inventory.ProductType pt ON pt.Id = ip.ProductTypeId
			 JOIN Inventory.ATC atc WITH (NOLOCK) ON ip.ATCId = atc.Id
			 JOIN @ATCs t ON atc.Code = t.Code
			 LEFT JOIN Inventory.InventoryMeasurementUnit imu ON imu.Id = ISNULL(atc.WeightMeasureUnit, atc.VolumeMeasureUnit)
			 WHERE @UserId = wu.UserId AND phy.Quantity > 0

		INSERT INTO @TableResult 
		(
			Id, Code, WarehouseId, ProductId, ProductCodeName, BatchSerialId, Quantity, ClassType, ClassName, Dose, DoseMeasurement
		)
			 SELECT	phy.Id,
					ins.Code,
					phy.WarehouseId,
					phy.ProductId,
					CONCAT(ip.Code, ' - ', ip.Name) ProductCodeName,
					phy.BatchSerialId,
					phy.Quantity,
					pt.Class,
					CASE pt.Class
						WHEN 1 THEN 'Grupo'
						WHEN 2 THEN 'Item Medicamento'
						WHEN 3 THEN 'Item Insumo'
						WHEN 4 THEN 'Item Otro'  
						WHEN 5 THEN 'Item Producción'
					END AS ClassName,
					NULL Dose,
					NULL DoseMeasurement
			 FROM Inventory.WarehouseUser wu WITH (NOLOCK)
			 JOIN Inventory.PhysicalInventory phy WITH (NOLOCK) ON wu.WarehouseId = phy.WarehouseId
			 JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON phy.ProductId = ip.Id
			 JOIN Inventory.ProductType pt ON pt.Id = ip.ProductTypeId
			 JOIN Inventory.InventorySupplie ins WITH (NOLOCK) ON ip.SupplieId = ins.Id
			 JOIN @ATCs t ON ins.Code = t.Code
			 LEFT JOIN @TableResult tr ON phy.Id = tr.Id
			 WHERE @UserId = wu.UserId AND phy.Quantity > 0 AND tr.Id IS NULL

		INSERT INTO @TableResult 
		(
			Id, Code, WarehouseId, ProductId, ProductCodeName, BatchSerialId, Quantity, ClassType, ClassName, Dose, DoseMeasurement
		)
			 SELECT	phy.Id,
					ip.Code,
					phy.WarehouseId,
					phy.ProductId,
					CONCAT(ip.Code, ' - ', ip.Name) ProductCodeName,
					phy.BatchSerialId,
					phy.Quantity,
					pt.Class,
					CASE pt.Class
						WHEN 1 THEN 'Grupo'
						WHEN 2 THEN 'Item Medicamento'
						WHEN 3 THEN 'Item Insumo'
						WHEN 4 THEN 'Item Otro'  
						WHEN 5 THEN 'Item Producción'
					END AS ClassName,
					CASE pt.Class
						WHEN 5 THEN (SELECT TOP 1 ISNULL(ppd.Quantity, pd.Quantity) 
									FROM MixingStation.RequestPackageDetailStatus rpds
									LEFT JOIN MixingStation.PackagePersonalizedDetail ppd ON ppd.PackagePersonalizedId = rpds.PackagePersonalizedId AND ppd.MainMedicine = 1
									LEFT JOIN MixingStation.PackageDetail pd ON pd.PackageId = rpds.PackageId AND pd.MainMedicine = 1
									INNER JOIN Inventory.BatchSerial bs ON bs.BatchCode = rpds.BatchCode
									WHERE bs.Id = phy.BatchSerialId
										)
						ELSE ISNULL(a.Weight, a.Volume)
					END AS Dose,
					CASE pt.Class
						WHEN 5 THEN (SELECT TOP 1 imu.Abbreviation 
									FROM MixingStation.RequestPackageDetailStatus rpds
									LEFT JOIN MixingStation.PackagePersonalizedDetail ppd ON ppd.PackagePersonalizedId = rpds.PackagePersonalizedId AND ppd.MainMedicine = 1
									LEFT JOIN MixingStation.PackageDetail pd ON pd.PackageId = rpds.PackageId AND pd.MainMedicine = 1
									INNER JOIN Inventory.BatchSerial bs ON bs.BatchCode = rpds.BatchCode
									INNER JOIN Inventory.InventoryMeasurementUnit imu ON imu.Id = ISNULL(ppd.MeasurementUnitId, pd.MeasurementUnitId)
									WHERE bs.Id = phy.BatchSerialId)
						ELSE imu.Abbreviation
					END AS DoseMeasurement
			 FROM Inventory.WarehouseUser wu WITH (NOLOCK)
			 JOIN Inventory.PhysicalInventory phy WITH (NOLOCK) ON wu.WarehouseId = phy.WarehouseId
			 JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON phy.ProductId = ip.Id
			 JOIN Inventory.ATC a ON a.Id = ip.ATCId
			 JOIN Inventory.ProductType pt ON pt.Id = ip.ProductTypeId
			 JOIN @ATCs t ON ip.Code = t.Code
			 LEFT JOIN @TableResult tr ON phy.Id = tr.Id
			 LEFT JOIN Inventory.InventoryMeasurementUnit imu ON imu.Id = ISNULL(a.WeightMeasureUnit, a.VolumeMeasureUnit)
			 WHERE @UserId = wu.UserId AND phy.Quantity > 0 AND tr.Id IS NULL

		INSERT INTO @TableResult 
		(
			Id, Code, WarehouseId,WarehouseCodeName, ProductId, ProductCodeName, Quantity, ClassType, ClassName, Dose, DoseMeasurement
		)
		SELECT VS.Id,
			   VS.Code,
			   VS.WarehouseId,
			   VS.WarehouseCodeName,
			   VS.ProductId,
			   VS.ProductCodeName,
			   VS.Quantity,
			   VS.Class,
			   vs.ClassName,
			   vs.Dose,
			   vs.DoseMeasurement
		FROM
		(	
			SELECT	0 Id,
				a.Code,
				phy.WarehouseId,
				CONCAT(w.Code, ' - ', w.Name) WarehouseCodeName,
				phy.ProductId,
				CONCAT(ip.Code, ' - ', ip.Name) ProductCodeName,
				9999999 Quantity,
				pt.Class,
				CASE pt.Class
					WHEN 1 THEN 'Grupo'
					WHEN 2 THEN 'Item Medicamento'
					WHEN 3 THEN 'Item Insumo'
					WHEN 4 THEN 'Item Otro'  
					WHEN 5 THEN 'Item Producción'
				END AS ClassName,
				CASE pt.Class
						WHEN 5 THEN (SELECT TOP 1 ISNULL(ppd.Quantity, pd.Quantity) 
									FROM MixingStation.RequestPackageDetailStatus rpds
									LEFT JOIN MixingStation.PackagePersonalizedDetail ppd ON ppd.PackagePersonalizedId = rpds.PackagePersonalizedId AND ppd.MainMedicine = 1
									LEFT JOIN MixingStation.PackageDetail pd ON pd.PackageId = rpds.PackageId AND pd.MainMedicine = 1
									INNER JOIN Inventory.BatchSerial bs ON bs.BatchCode = rpds.BatchCode
									WHERE bs.Id = phy.BatchSerialId
										)
						ELSE ISNULL(a.Weight, a.Volume)
					END AS Dose,
					CASE pt.Class
						WHEN 5 THEN (SELECT TOP 1 imu.Abbreviation 
									FROM MixingStation.RequestPackageDetailStatus rpds
									LEFT JOIN MixingStation.PackagePersonalizedDetail ppd ON ppd.PackagePersonalizedId = rpds.PackagePersonalizedId AND ppd.MainMedicine = 1
									LEFT JOIN MixingStation.PackageDetail pd ON pd.PackageId = rpds.PackageId AND pd.MainMedicine = 1
									INNER JOIN Inventory.BatchSerial bs ON bs.BatchCode = rpds.BatchCode
									INNER JOIN Inventory.InventoryMeasurementUnit imu ON imu.Id = ISNULL(ppd.MeasurementUnitId, pd.MeasurementUnitId)
									WHERE bs.Id = phy.BatchSerialId)
						ELSE imu.Abbreviation
					END AS DoseMeasurement
			FROM Inventory.WarehouseUser wu WITH (NOLOCK)
			JOIN Inventory.Warehouse w WITH (NOLOCK) ON wu.WarehouseId = w.Id
			JOIN Inventory.PhysicalInventory phy WITH (NOLOCK) ON wu.WarehouseId = phy.WarehouseId
			JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON phy.ProductId = ip.Id 
			JOIN Inventory.ProductType pt ON pt.Id = ip.ProductTypeId
			JOIN Inventory.ATC a WITH (NOLOCK) ON ip.ATCId = a.Id
			LEFT JOIN Inventory.InventoryMeasurementUnit imu ON	 imu.Id = ISNULL(a.WeightMeasureUnit, a.VolumeMeasureUnit)
			JOIN @ATCs t ON a.Code = t.Code
			WHERE wu.UserId = @UserId AND w.VirtualStore  = 1

			UNION 

			SELECT 0 Id,
				s.Code,
				phy.WarehouseId,
				CONCAT(w.Code, ' - ', w.Name) WarehouseCodeName,
				phy.ProductId,
				CONCAT(ip.Code, ' - ', ip.Name) ProductCodeName,		
				9999999 Quantity,
				pt.Class,
				CASE pt.Class
					WHEN 1 THEN 'Grupo'
					WHEN 2 THEN 'Item Medicamento'
					WHEN 3 THEN 'Item Insumo'
					WHEN 4 THEN 'Item Otro'  
					WHEN 5 THEN 'Item Producción'
				END AS ClassName,
				NULL Dose,
				NULL DoseMeasurement
			FROM Inventory.WarehouseUser wu WITH (NOLOCK)
			JOIN Inventory.Warehouse w WITH (NOLOCK) ON wu.WarehouseId = w.Id
			JOIN Inventory.PhysicalInventory phy WITH (NOLOCK) ON wu.WarehouseId = phy.WarehouseId
			JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON phy.ProductId = ip.Id 
			JOIN Inventory.ProductType pt ON pt.Id = ip.ProductTypeId
			JOIN Inventory.InventorySupplie s WITH (NOLOCK) ON ip.SupplieId = s.Id
			JOIN @ATCs t ON s.Code = t.Code
			WHERE wu.UserId = @UserId AND w.VirtualStore  = 1 
		) vs 
		
		/*******************************************************************************************/

		UPDATE tr
			SET tr.WarehouseCodeName = CONCAT(w.Code, ' - ', w.Name)
		FROM @TableResult tr
		JOIN Inventory.Warehouse w WITH (NOLOCK) ON tr.WarehouseId = w.Id

		UPDATE tr
			SET tr.BatchSerialCode = bs.BatchCode,
				tr.BatchSerialExpirationDate = bs.ExpirationDate
		FROM @TableResult tr
		JOIN Inventory.BatchSerial bs WITH (NOLOCK) ON tr.BatchSerialId = bs.Id

		UPDATE tr
			SET tr.Covered = 1
		FROM @TableResult tr
		JOIN Inventory.ProductRateDetail prd WITH (NOLOCK) ON tr.ProductId = prd.ProductId
		JOIN Contract.CareGroup cg WITH (NOLOCK) ON prd.ProductRateId = cg.ProductRateId
		WHERE cg.Id = @CareGroupId AND @CurrentDate BETWEEN prd.InitialDate AND prd.EndDate
	END TRY
	BEGIN CATCH
		PRINT ERROR_MESSAGE() + ' Linea: ' + cast(ERROR_LINE() AS VARCHAR(20))
	END CATCH

	DELETE FROM @TableResult
	WHERE BatchSerialCode IN (
		SELECT rpds.BatchCode
		FROM MixingStation.RequestPackageDetailStatus rpds
		LEFT JOIN MixingStation.RequestMixingStationDetailPatients rmsdp
			ON rmsdp.RequestMixingStationDetailId = rpds.RequestMixingStationDetailId
		WHERE rmsdp.Id IS NOT NULL
	)

	IF @TotalDose > 0
	BEGIN
		SELECT *
		FROM @TableResult
		WHERE (ClassType <> 5)
		   OR (ClassType = 5 AND Dose <= @TotalDose AND Dose IS NOT NULL AND DoseMeasurement IS NOT NULL)
	END
	ELSE
	BEGIN
		SELECT *
		FROM @TableResult
		WHERE (ClassType <> 5) OR (ClassType = 5 AND Dose IS NOT NULL AND DoseMeasurement IS NOT NULL)
	END
END