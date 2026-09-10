
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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista el inventario físico disponible en bodega para un conjunto de productos identificados por sus códigos, filtrado por el usuario y las bodegas a las que tiene acceso. Recibe dos parámetros XML: uno con el identificador del grupo de atención, el usuario y la dosis total esperada, y otro con la lista de códigos de productos a consultar (códigos ATC, códigos de insumos o códigos directos de producto). Cruza el inventario físico real contado en bodega (PhysicalInventory) con el catálogo de productos (InventoryProduct), su clasificación (ProductType), la clasificación ATC farmacológica (ATC), los insumos (InventorySupplie) y las unidades de medida (InventoryMeasurementUnit), devolviendo para cada ítem con existencia positiva: el código, la bodega, el producto, el lote o serie, la cantidad, la clase del producto (medicamento, insumo, producción, etc.), la dosis y su unidad. Para productos de tipo ''Producción'' (mezclas de estación de mezclas), consulta adicionalmente los detalles del paquete personalizado para obtener la dosis y unidad correspondientes.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_ListPhysicalInventoryByCode';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_ListPhysicalInventoryByCode';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista el inventario físico disponible para un usuario, filtrando por una lista de códigos (ATC, insumo o producto) y marcando coberturas, dosis y bodegas virtuales para apoyar la dispensación o preparación de mezclas.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ListPhysicalInventoryByCode';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El usuario debe tener bodegas asignadas en Inventory.WarehouseUser.; El XML @XmlParameters debe contener nodo /Data con CareGroupId, UserId y opcionalmente TotalDose.; El XML @XmlATCs debe contener nodos /Data/Code con los códigos a buscar.; Los productos deben tener cantidad física mayor a cero (phy.Quantity > 0) para ser considerados en bodegas reales.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ListPhysicalInventoryByCode';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran productos con existencia física positiva (Quantity>0) en bodegas reales del usuario.; Las bodegas virtuales (VirtualStore=1) siempre presentan stock ficticio de 9999999 unidades.; Un mismo phy.Id no se duplica entre las inserciones por ATC, Insumo y Producto (segunda y tercera inserción excluyen tr.Id existente).; Los lotes ya asignados a pacientes en mezclas no aparecen nunca en el resultado.; Los items de Class=5 (Producción) sin dosis o unidad de medida son excluidos del resultado final.; La cobertura (Covered) requiere vigencia de la tarifa del CareGroup en la fecha actual.; Los errores son capturados y solo se imprimen (PRINT), no se relanzan ni se hace rollback.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ListPhysicalInventoryByCode';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @TableResult: Cuando el código del XML coincide con ATC.Code y el usuario tiene la bodega asignada con Quantity>0, se inserta el inventario físico con dosis tomada de ATC.Weight/Volume (o desde MixingStation si Class=5).; [INSERT] @TableResult: Cuando el código coincide con InventorySupplie.Code y el registro aún no existe en @TableResult, se inserta sin dosis (Dose y DoseMeasurement en NULL).; [INSERT] @TableResult: Cuando el código coincide con InventoryProduct.Code y el registro aún no existe en @TableResult, se inserta con dosis calculada según Class del ProductType.; [INSERT] @TableResult: Para bodegas con VirtualStore=1 asignadas al usuario, se inserta una fila con Id=0 y Quantity=9999999 (stock infinito virtual) para cada coincidencia por ATC o por código de insumo.; [UPDATE] @TableResult: Se completa WarehouseCodeName concatenando Code y Name de Inventory.Warehouse para cada fila.; [UPDATE] @TableResult: Se completa BatchSerialCode y BatchSerialExpirationDate desde Inventory.BatchSerial cuando hay BatchSerialId.; [UPDATE] @TableResult: Se marca Covered=1 cuando el producto está en ProductRateDetail asociado al ProductRate del CareGroup recibido y la fecha actual está entre InitialDate y EndDate.; [DELETE] @TableResult: Se eliminan filas cuyo BatchSerialCode corresponde a lotes ya asignados a pacientes en MixingStation.RequestMixingStationDetailPatients (no disponibles).; [RETURN_RESULT] RESULT: Si @TotalDose>0 se retornan filas no-Class5 más las Class5 cuya Dose<=TotalDose y con Dose y DoseMeasurement no nulos; en caso contrario se retornan no-Class5 más Class5 con Dose y DoseMeasurement no nulos.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ListPhysicalInventoryByCode';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ProductType.Class = 5 (Item Producción) → Dose y DoseMeasurement se calculan desde MixingStation (PackagePersonalizedDetail/PackageDetail con MainMedicine=1) usando el BatchCode del lote. else Dose toma ISNULL(ATC.Weight, ATC.Volume) y DoseMeasurement la abreviatura de la unidad asociada.; si Warehouse.VirtualStore = 1 → Se inserta una fila virtual con Quantity=9999999 simulando stock ilimitado para esa bodega.; si @TotalDose > 0 → Se filtran items Class=5 cuya Dose sea <= @TotalDose y tengan dosis/unidad no nulos. else Se devuelven items Class=5 únicamente si tienen Dose y DoseMeasurement no nulos.; si BatchSerialCode pertenece a lotes con pacientes asignados en RequestMixingStationDetailPatients → Se elimina la fila del resultado (lote ya comprometido a un paciente).; si CareGroup tiene ProductRateDetail vigente para el producto en la fecha actual → Marca Covered=1 (producto cubierto por la tarifa del grupo de atención). else Covered permanece en 0 (default).', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ListPhysicalInventoryByCode';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ListPhysicalInventoryByCode';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.WarehouseUser; Inventory.Warehouse; Inventory.PhysicalInventory; Inventory.InventoryProduct; Inventory.ProductType; Inventory.ATC; Inventory.InventorySupplie; Inventory.InventoryMeasurementUnit; Inventory.BatchSerial; Inventory.ProductRateDetail; Contract.CareGroup; MixingStation.RequestPackageDetailStatus; MixingStation.PackagePersonalizedDetail; MixingStation.PackageDetail; MixingStation.RequestMixingStationDetailPatients', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ListPhysicalInventoryByCode';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ListPhysicalInventoryByCode';
-- GO
