-- =============================================
-- Author:      Diego A. Roldán
-- Create Date: <Create Date, , >
-- Description: sp para consultar los productos o componentes que tiene una campaña
-- =============================================
CREATE PROCEDURE [MixingStation].[SP_ProductListCampaing]
(
    @CampaignDetailId INT,
	@Type INT
)
AS
BEGIN
    SET NOCOUNT ON;

    -- Tabla temporal para almacenar medicamentos
    DECLARE @TablaMedicamentosDX TABLE(
        Id INT IDENTITY(1,1),
        TypeProduct INTEGER,
        NameTypeProduct VARCHAR(MAX),
        ItemId INTEGER,
        code VARCHAR(MAX),
        NAME VARCHAR(MAX),
        ItemType INTEGER,
        PackageId INTEGER,
        GroupName VARCHAR(MAX),
        CantidadDosis DECIMAL(18,2),
        CodigoUnidadPeso VARCHAR(MAX),
        CodigoUnidadVolumen VARCHAR(MAX),
        FormulationType INTEGER,
        ATCEntityId INTEGER,
        PharmaceuticalFormId INTEGER,
        Concentration DECIMAL(18,2),
        DosisRequerida DECIMAL(18,5),
        ItemGroup INT,
        BatchCode NVARCHAR(MAX) NULL,
        QuantityPackage DECIMAL(18,4),
        Thinner BIT,
        QuantityThinner DECIMAL(18,2),
        QuantityBlister DECIMAL(18,2),
        Vehicle BIT,
        NPTItemOrder TINYINT,
        RequestMixingStationDetailId INT
    );

    -- Tabla temporal para RequestMixingStationDetail
    DECLARE @RequestMixingStationDetail TABLE(
        TmpId INT IDENTITY,
        Id INT,
        RequestMixingStationId INT NOT NULL,
        ATCId INT,
        PackageId INT,
        PackagePersonalizedId INT,
        UnitDoseTypeId INT NOT NULL,
        Quantity INT,
        STATUS INT,
        EntityId INT NOT NULL,
        EntityName VARCHAR(300) NOT NULL,
        CareCenterCode VARCHAR(20) NOT NULL,
        Source TINYINT NOT NULL,
        ProductionLineId INT,
        CampaignDetailId INT,
        LabelType TINYINT,
        SendTo TINYINT NOT NULL,
        ConfirmationUser VARCHAR(20),
        ConfirmationDate DATETIME
    );

    -- Si no existen registros, hacemos una readecuación
    IF NOT EXISTS (
        SELECT 1
        FROM MixingStation.RequestMixingStationDetail rd WITH(NOLOCK)
        JOIN MixingStation.UnitDoseType udt ON udt.Id = rd.UnitDoseTypeId
        LEFT JOIN MixingStation.Package p WITH(NOLOCK) ON rd.PackageId = p.Id
        WHERE rd.CampaignDetailId = @CampaignDetailId 
        AND rd.RequestPackageDetailStatusId IS NULL 
        AND udt.MSClass = 5
    )
    BEGIN
        DECLARE @Quantity INT, @VehicleOptimization BIT, @Id INT;

        DECLARE cursor_request_detail CURSOR FOR
        SELECT rd.Quantity, ISNULL(p.VehicleOptimization, 0), rd.Id
        FROM MixingStation.RequestMixingStationDetail rd WITH(NOLOCK)
        LEFT JOIN MixingStation.Package p WITH(NOLOCK) ON rd.PackageId = p.Id
        WHERE rd.CampaignDetailId = @CampaignDetailId
        AND rd.RequestPackageDetailStatusId IS NULL;

        OPEN cursor_request_detail;
        FETCH NEXT FROM cursor_request_detail INTO @Quantity, @VehicleOptimization, @Id;
        WHILE @@FETCH_STATUS = 0
        BEGIN
            IF @VehicleOptimization <> 1 AND @Quantity > 1
            BEGIN
                DECLARE @i INT = 0;
                WHILE @i <= @Quantity - 1
                BEGIN
                    INSERT INTO @RequestMixingStationDetail
                    SELECT Id, RequestMixingStationId, ATCId, PackageId, PackagePersonalizedId, 
                           UnitDoseTypeId, 1, [STATUS], EntityId, EntityName, CareCenterCode, 
                           [Source], ProductionLineId, CampaignDetailId, LabelType, SendTo, 
                           ConfirmationUser, ConfirmationDate
                    FROM MixingStation.RequestMixingStationDetail rd
                    WHERE rd.Id = @Id;

                    SET @i = @i + 1;
                END
            END
            ELSE
            BEGIN
                INSERT INTO @RequestMixingStationDetail
                SELECT Id, RequestMixingStationId, ATCId, PackageId, PackagePersonalizedId, 
                       UnitDoseTypeId, Quantity, [STATUS], EntityId, EntityName, CareCenterCode, 
                       [Source], ProductionLineId, CampaignDetailId, LabelType, SendTo, 
                       ConfirmationUser, ConfirmationDate
                FROM MixingStation.RequestMixingStationDetail rd
                WHERE rd.Id = @Id;
            END
            FETCH NEXT FROM cursor_request_detail INTO @Quantity, @VehicleOptimization, @Id;
        END
        CLOSE cursor_request_detail;
        DEALLOCATE cursor_request_detail;
    END
    ELSE
    BEGIN
        INSERT INTO @RequestMixingStationDetail
        SELECT Id, RequestMixingStationId, ATCId, PackageId, PackagePersonalizedId, 
               UnitDoseTypeId, Quantity, [STATUS], EntityId, EntityName, CareCenterCode, 
               [Source], ProductionLineId, CampaignDetailId, LabelType, SendTo, 
               ConfirmationUser, ConfirmationDate
        FROM MixingStation.RequestMixingStationDetail rd
        WHERE rd.CampaignDetailId = @CampaignDetailId;
    END

    ;WITH CampaignDetailWithRequests AS (
    SELECT 
        rd.Id,
        -- Determinar el ItemId basándonos en el tipo de producto
        COALESCE(pp.Id, p.Id, a.Id) AS ItemId,
        
        -- Determinar el ItemType basándonos en las relaciones de las tablas
        CASE 
            WHEN pp.Id IS NOT NULL THEN 3  -- PackagePersonalized
            WHEN p.Id IS NOT NULL THEN 1   -- Package
            WHEN a.Id IS NOT NULL THEN 2   -- ATC
        END AS ItemType,

        -- Si la cantidad en RequestMixingStationDetailPatients es nula, se usa la cantidad de la tabla principal
        COALESCE(rdp.Quantity, rd.Quantity) AS RequestQuantity,
        COALESCE(rdp.CampaignDetailId, rd.CampaignDetailId) AS CampaignDetailId,

        p.VehicleOptimization,
        rd.TmpId,
        udt.MSClass,
        rd.PackageId
    FROM @RequestMixingStationDetail rd
    INNER JOIN MixingStation.UnitDoseType udt ON udt.Id = rd.UnitDoseTypeId
    LEFT JOIN MixingStation.Package p ON p.Id = rd.PackageId
    LEFT JOIN Inventory.ATC a ON a.Id = rd.ATCId
    LEFT JOIN MixingStation.PackagePersonalized pp ON pp.Id = rd.PackagePersonalizedId
    LEFT JOIN (
        SELECT 
            RequestMixingStationDetailId, 
            SUM(Quantity) AS Quantity, 
            CampaignDetailId
        FROM MixingStation.RequestMixingStationDetailPatients
        WHERE CampaignDetailId IS NOT NULL 
        AND STATUS <> 3
        GROUP BY RequestMixingStationDetailId, CampaignDetailId
    ) rdp ON rdp.RequestMixingStationDetailId = rd.Id
    WHERE rdp.CampaignDetailId IS NOT NULL OR rd.CampaignDetailId IS NOT NULL
	),
	DistinctBatchCodes AS (
		SELECT DISTINCT 
			rpds.BatchCode, 
			rpds.RequestMixingStationDetailId
		FROM MixingStation.RequestPackageDetailStatus rpds
		INNER JOIN MixingStation.RequestMixingStationDetail rmsd ON rmsd.id = rpds.RequestMixingStationDetailId
		WHERE rmsd.CampaignDetailId = @CampaignDetailId
	),
	cc AS (
		SELECT 
			-- Determinamos el tipo de producto basándonos en las relaciones entre las tablas
			CASE 
				WHEN pd.AtcId IS NOT NULL THEN 1   -- ATC
				WHEN pd.SupplieId IS NOT NULL THEN 2  -- Insumo
				WHEN pd.ProductId IS NOT NULL THEN 3  -- Producto
			END AS TypeProduct,

			-- Nombre de los productos según el tipo
			CASE 
				WHEN pd.AtcId IS NOT NULL THEN 'ATC'
				WHEN pd.SupplieId IS NOT NULL THEN 'Insumo'
				WHEN pd.ProductId IS NOT NULL THEN 'Producto'
			END AS NameTypeProduct,

			-- Determinar el ItemId basado en la prioridad
			COALESCE(pd.AtcId, pd.SupplieId, pd.ProductId) AS ItemId,

			-- Código correspondiente basado en el tipo de producto
			COALESCE(a2.Code, s.Code, prod.Code) AS Code,

			-- Nombre correspondiente basado en el tipo de producto
			COALESCE(a2.NAME, s.SupplieName, prod.NAME) AS [NAME],

			-- Determinar si es un medicamento principal o no
			CAST(IIF(ISNULL(pd.MainMedicine, 0) = 1, 1, 3) AS TINYINT) AS ItemType,

			-- Determina la agrupacion del item
			v.PackageId,
			IIF(ISNULL(pd.MainMedicine, 0) = 1, 'Principal', 'Otro') AS GroupName,

			-- Calcular la cantidad según los casos definidos en el procedimiento
			CASE 
				WHEN v.MSClass = 2 THEN
					CASE
						WHEN pd.MainMedicine = 1 AND @Type = 2 AND a2.FormulationType = 3 THEN v.RequestQuantity * pd.Volume
						ELSE pd.Quantity * v.RequestQuantity
					END
				WHEN pd.MainMedicine = 1 AND @Type = 2 AND a2.FormulationType = 1 THEN 
					(ISNULL((SELECT TOP 1 p.Quantity 
							 FROM MixingStation.PackageDetail p 
							 WHERE p.PackageId = v.PackageId AND p.Thinner = 1), v.RequestQuantity) * pd.Quantity) / a2.Weight
				WHEN pd.MainMedicine = 1 AND @Type = 2 AND a2.FormulationType = 3 THEN 
					(a2.Volume * pd.Quantity) / a2.Weight
				ELSE 
					pd.Quantity * v.RequestQuantity
			END AS Quantity,

			-- Obtener las unidades de medida correspondientes
			IIF(@Type = 2 AND pd.AtcId IS NOT NULL, '019', UnitPeso.Code) AS CodigoUnidadPeso,
			UnitVolumen.Code AS CodigoUnidadVolumen,

			-- Otros detalles del producto
			a2.FormulationType,
			a2.ATCEntityId,
			a2.PharmaceuticalFormId,

			-- Cálculo de concentración
			CASE 
				WHEN v.MSClass = 2 THEN ISNULL(a2.Volume, 0)
				WHEN a2.FormulationType IN (1, 3) THEN a2.Weight
				ELSE a2.Volume
			END AS Concentration,

			-- Agrupar por los productos de vehículos optimizados
			IIF(v.VehicleOptimization = 0 AND pd.Vehicle = 1, v.TmpId, 0) AS ItemGroup,

			-- Obtener los códigos de lote
			IIF(v.MSClass IN (5, 7),
				(SELECT TOP 1 rpds.BatchCode 
				 FROM MixingStation.RequestPackageDetailStatus rpds 
				 WHERE rpds.RequestMixingStationDetailId = v.Id
				 GROUP BY rpds.BatchCode),
				(SELECT STRING_AGG(rpds.BatchCode, ',')
				 FROM DistinctBatchCodes rpds
				 WHERE rpds.RequestMixingStationDetailId = v.Id)
			) AS BatchCode,

			pd.Quantity AS QuantityPackage,
			v.Id AS RequestMixingStationDetailId,
			IIF(pd.Thinner = 1, (ISNULL((SELECT TOP 1 pd.Thinner 
										FROM MixingStation.PackageDetail pd 
										WHERE pd.PackageId = v.PackageId AND pd.Thinner = 1), 0)), NULL) AS Thinner,
			IIF(pd.Thinner = 1, (ISNULL((SELECT TOP 1 pd.Quantity 
										 FROM MixingStation.PackageDetail pd 
										 WHERE pd.PackageId = v.PackageId AND pd.Thinner = 1), 0)), NULL) AS QuantityThinner,
			IIF(pd.MainMedicine = 1, ISNULL(a2.Volume, 0), NULL) AS QuantityBlister,
			pd.Vehicle,
			pd.NPTItemOrder,
			v.MSClass,
			v.VehicleOptimization
		FROM CampaignDetailWithRequests v
		INNER JOIN (
			SELECT DISTINCT k.*
			FROM (
				SELECT 1 AS ItemType, AtcId, SupplieId, ProductId, PackageId AS Id, ComponentType, MainMedicine, Thinner, 
					   Vehicle, Quantity, MeasurementUnitId, Quantity AS Volume, MeasurementUnitId AS VolumeMeasureUnit, NPTItemOrder
				FROM MixingStation.PackageDetail
				UNION ALL
				SELECT 3 AS ItemType, AtcId, SupplieId, ProductId, PackagePersonalizedId AS Id, ComponentType, MainMedicine, 
					   Thinner, Vehicle, Quantity, MeasurementUnitId, Volume, VolumeMeasureUnit, NPTItemOrder
				FROM MixingStation.PackagePersonalizedDetail
			) AS k
		) pd ON pd.Id = v.ItemId AND pd.ItemType = v.ItemType
		LEFT JOIN Inventory.ATC a2 ON a2.Id = pd.AtcId
		LEFT JOIN Inventory.InventorySupplie s ON s.Id = pd.SupplieId
		LEFT JOIN Inventory.InventoryProduct prod ON prod.Id = pd.ProductId
		LEFT JOIN Inventory.InventoryMeasurementUnit UnitPeso ON UnitPeso.id = pd.MeasurementUnitId
		LEFT JOIN Inventory.InventoryMeasurementUnit UnitVolumen ON UnitVolumen.id = pd.VolumeMeasureUnit
		WHERE v.CampaignDetailId = @CampaignDetailId

		UNION ALL

			SELECT 
				1 TypeProduct,
				'ATC' NameTypeProduct,
				v.ItemId,
				a2.Code,
				a2.NAME,
				CAST(1 AS TINYINT) ItemType
				, NULL PackageId
				, 'Principal' GroupName
				, ((CASE WHEN a2.FormulationType = 1 OR a2.FormulationType = 3 THEN a2.Weight ELSE a2.Volume END) * v.RequestQuantity) AS Quantity
				, IIF( @Type = 2 And a2.FormulationType = 3, '019' , UnitPeso.Code) CodigoUnidadPeso
				, UnitVolumen.Code AS CodigoUnidadVolumen
				, a2.FormulationType
				, a2.ATCEntityId
				,  a2.PharmaceuticalFormId
				, CASE 
					WHEN v.MSClass = 2 THEN ISNULL(a2.Volume, 0)
					WHEN a2.FormulationType IN (1, 3) THEN a2.Weight
					ELSE a2.Volume
				  END AS Concentration
				, 0 ItemGroup
				, IIF(v.MSClass IN (5, 7),
					(
						SELECT TOP 1 rpds.BatchCode 
						FROM MixingStation.RequestPackageDetailStatus rpds 
						WHERE rpds.RequestMixingStationDetailId = v.Id
						GROUP BY rpds.BatchCode
					),(
						SELECT  STRING_AGG(CAST(rpds.BatchCode AS NVARCHAR(MAX)), ', ')
						FROM MixingStation.RequestPackageDetailStatus rpds 
						WHERE rpds.RequestMixingStationDetailId = v.Id
					)
				) AS BatchCode
				, 1 AS QuantityPackage
				, v.Id AS RequestMixingStationDetailId
				, NULL
				, NULL
				, NULL
				, NULL
				, NULL
				, v.MSClass
				, v.VehicleOptimization
			FROM CampaignDetailWithRequests v
			INNER JOIN Inventory.ATC a2 ON a2.Id = v.ItemId AND v.ItemType = 2
			LEFT JOIN Inventory.InventoryMeasurementUnit UnitPeso ON UnitPeso.id = a2.WeightMeasureUnit				 
			LEFT JOIN Inventory.InventoryMeasurementUnit UnitVolumen ON UnitVolumen.id = a2.VolumeMeasureUnit
			WHERE CampaignDetailId = @CampaignDetailId
	)
				  
		INSERT INTO @TablaMedicamentosDX
		SELECT 
		TypeProduct,
		NameTypeProduct,
		ItemId,
		Code,
		NAME,
		ItemType,
		PackageId,
		GroupName,
		SUM(Quantity) AS CantidadDosis,
		CodigoUnidadPeso,
		CodigoUnidadVolumen,
		FormulationType,
		ATCEntityId,
		PharmaceuticalFormId,
		Concentration,
		CASE 
			WHEN tmp.ItemType = 1 THEN 
				MixingStation.CalculationQuantityMaterialRaw(
					FormulationType, 
					Code, 
					SUM(Quantity), 
					CASE 
						WHEN FormulationType IN (1, 3) THEN CodigoUnidadPeso 
						ELSE CodigoUnidadVolumen 
					END)
			WHEN tmp.VehicleOptimization = 0 THEN tmp.Concentration
			ELSE 
				MixingStation.CalculationQuantityMaterialRaw(
					FormulationType, 
					Code, 
					SUM(Quantity), 
					CASE 
						WHEN FormulationType IN (1, 3) THEN CodigoUnidadPeso 
						ELSE CodigoUnidadVolumen 
					END)
		END AS DosisRequerida,
		ItemGroup,
		tmp.BatchCode,
		CASE 
			WHEN tmp.ItemType = 1 THEN tmp.Quantity 
			ELSE tmp.QuantityPackage 
		END AS QuantityPackage,
		tmp.Thinner,
		tmp.QuantityThinner,
		tmp.QuantityBlister,
		tmp.Vehicle,
		tmp.NPTItemOrder,
		tmp.RequestMixingStationDetailId
	FROM cc AS tmp
	WHERE tmp.Quantity > 0
	GROUP BY 
		TypeProduct,
		NameTypeProduct,
		ItemId,
		Code,
		NAME,
		ItemType,
		PackageId,
		GroupName,
		CodigoUnidadPeso,
		CodigoUnidadVolumen,
		FormulationType,
		ATCEntityId,
		PharmaceuticalFormId,
		Concentration,
		ItemGroup,
		tmp.QuantityPackage,
		tmp.BatchCode,
		tmp.Thinner,
		tmp.QuantityThinner,
		tmp.QuantityBlister,
		tmp.Vehicle,
		tmp.NPTItemOrder,
		tmp.MSClass,
		tmp.RequestMixingStationDetailId,
		tmp.VehicleOptimization,
		tmp.Quantity

		SELECT TypeProduct
			, NameTypeProduct
			, ItemId
			, td.Code
			, td.NAME
			, ItemType
			, PackageId
			, GroupName
			, SUM(CantidadDosis) CantidadDosis
			, CodigoUnidadPeso
			, CodigoUnidadVolumen
			, FormulationType
			, ATCEntityId
			, PharmaceuticalFormId
			, Concentration
			, SUM(DosisRequerida) DosisRequerida
			, BatchCode
			, QuantityPackage
			, mu.Abbreviation AS MeasureUnitAbbreviation
			, td.Thinner
			, td.QuantityThinner
			, td.QuantityBlister
			, td.Vehicle
			, td.NPTItemOrder
            , td.RequestMixingStationDetailId
		FROM @TablaMedicamentosDX td
		LEFT JOIN Inventory.InventoryMeasurementUnit mu ON td.CodigoUnidadPeso = mu.Code
		GROUP BY TypeProduct, NameTypeProduct, ItemId, td.Code, GroupName,td.NAME,ItemType,GroupName,CodigoUnidadPeso, CodigoUnidadVolumen
			, FormulationType
			, ATCEntityId
			, PharmaceuticalFormId
			, Concentration
			, ItemGroup
			, BatchCode
			, QuantityPackage
			, mu.Abbreviation
			, td.Thinner
			, td.QuantityThinner
			, td.QuantityBlister
			, PackageId
			, td.Vehicle
			, td.NPTItemOrder
            , td.RequestMixingStationDetailId

		UNION ALL --Consultamos los productos de la canasta

		SELECT
			pbd.ComponentType TypeProduct, 
			CASE pbd.ComponentType 
				WHEN 1 THEN 'ATC' 
				WHEN 2 THEN 'Insumo' 
				WHEN 3 THEN 'Pruducto' 
			END AS NameTypeProduct,
			ISNULL(pbd.productId, isnull(pbd.AtcId, pbd.SupplieId)) ItemId,
			ISNULL(b.Code, isnull(c.Code, e.Code)) Code,
			ISNULL(b.NAME, isnull(c.NAME, e.SupplieName)) NAME,
			CAST(2 AS TINYINT) AS ItemType,
			NULL PackageId,
			'Canasta'  GroupName,
			pbd.Quantity  CantidadDosis,
			F.Code  CodigoUnidadPeso,
			NULL CodigoUnidadVolumen,
			B.FormulationType,
			B.ATCEntityId,
			B.PharmaceuticalFormId,
			1 AS Concentration,
			pbd.Quantity AS DosisRequerida
			, '' AS BatchCode
			, 1 AS QuantityPackage
			, '' AS MeasureUnitAbbreviation
			, NULL
			, NULL
			, NULL
			, NULL
			, NULL
            , NULL
		FROM MixingStation.CampaignDetail cd
		INNER JOIN MixingStation.ProductionBasketsDetail pbd ON cd.ProductionBasketId = pbd.ProductionBasketsId
		LEFT JOIN Inventory.InventoryMeasurementUnit f ON f.Id =  pbd.MeasurementUnitId 
		LEFT JOIN Inventory.ATC b ON b.Id = pbd.AtcId
		LEFT JOIN Inventory.InventoryProduct c ON c.Id =  pbd.ProductId
		LEFT JOIN Inventory.InventorySupplie e ON e.Id =  pbd.SupplieId
		WHERE cd.id = @CampaignDetailId

		UNION ALL --Consultamos los productos agregados desde el Boton de Productos

		SELECT
			CASE
				WHEN a2.Id IS NOT NULL THEN 1 -- ATC
				WHEN pd.Id IS NOT NULL AND pd.AtcId IS NOT NULL THEN 1 -- ATC
				WHEN pd.Id IS NOT NULL AND pd.SupplyId IS NOT NULL THEN 2 --Insumo
				WHEN pd.Id IS NOT NULL AND pd.ProductId IS NOT NULL THEN 3 --Producto
			END TypeProduct,
			CASE 
				WHEN a2.Id IS NOT NULL THEN 'ATC' 
				WHEN pd.Id IS NOT NULL AND pd.AtcId IS NOT NULL THEN 'ATC' 
				WHEN pd.Id IS NOT NULL AND pd.SupplyId IS NOT NULL THEN 'Insumo' 
				WHEN pd.Id IS NOT NULL AND pd.ProductId IS NOT NULL THEN 'Producto' 
			END NameTypeProduct,
			CASE 
				WHEN a2.Id IS NOT NULL THEN a2.Id
				WHEN pd.Id IS NOT NULL AND pd.AtcId IS NOT NULL THEN pd.AtcId
				WHEN pd.Id IS NOT NULL AND pd.SupplyId IS NOT NULL THEN pd.SupplyId
				WHEN pd.Id IS NOT NULL AND pd.ProductId IS NOT NULL THEN pd.ProductId
			END ItemId,
			CASE 
				WHEN a2.Id IS NOT NULL THEN a2.Code 
				WHEN pd.Id IS NOT NULL AND pd.AtcId IS NOT NULL THEN a2.Code 
				WHEN pd.Id IS NOT NULL AND pd.SupplyId IS NOT NULL THEN s.Code 
				WHEN pd.Id IS NOT NULL AND pd.ProductId IS NOT NULL THEN prod.Code 
			END Code,
			CASE 
				WHEN a2.Id IS NOT NULL THEN  a2.NAME 
				WHEN pd.Id IS NOT NULL AND pd.AtcId IS NOT NULL THEN  + a2.NAME
				WHEN pd.Id IS NOT NULL AND pd.SupplyId IS NOT NULL THEN  + s.SupplieName
				WHEN pd.Id IS NOT NULL AND pd.ProductId IS NOT NULL THEN prod.NAME
			END NAME, 
			pd.ItemType,
			NULL PackageId,
			'Solicitudes Manuales' GroupName,
			(pd.RequestQuantity) AS CantidadDosis,
			UnitPeso.Code AS CodigoUnidadPeso,
			UnitVolumen.Code AS CodigoUnidadVolumen,	
			a2.FormulationType,
			a2.ATCEntityId,
			a2.PharmaceuticalFormId,		
			1 AS Concentration,
			(pd.RequestQuantity) AS DosisRequerida
			, '' AS BatchCode
			, 1 AS QuantityPackage
			, '' AS MeasureUnitAbbreviation
			, NULL
			, NULL
			, NULL
			, NULL
			, NULL
            , NULL
		FROM  MixingStation.CampaignDetailItems pd
		LEFT JOIN Inventory.ATC a2 ON a2.Id = pd.AtcId
		LEFT JOIN Inventory.InventorySupplie s ON s.Id = pd.SupplyId
		LEFT JOIN Inventory.InventoryProduct prod ON prod.Id = pd.ProductId
		LEFT JOIN Inventory.InventoryMeasurementUnit f ON f.Id =  prod.MeasurementUnitId
		LEFT JOIN Inventory.InventoryMeasurementUnit UnitPeso ON UnitPeso.id = a2.WeightMeasureUnit 
		LEFT JOIN Inventory.InventoryMeasurementUnit UnitVolumen ON UnitVolumen.id = a2.VolumeMeasureUnit 
		WHERE pd.CampaignDetailId = @CampaignDetailId AND pd.ItemType = 4

	END