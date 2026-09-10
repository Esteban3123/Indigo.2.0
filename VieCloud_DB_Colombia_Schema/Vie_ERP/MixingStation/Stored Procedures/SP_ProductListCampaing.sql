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
        Vehicle BIT,
        NPTItemOrder TINYINT,
        RequestMixingStationDetailId INT,
		VerifiedFor VARCHAR(250),
		ConfirmedFor VARCHAR(250),
		IsPackagePersonalized BIT DEFAULT 0, -- Indica si viene de un paquete personalizado
		HasComplementaryMedicine BIT DEFAULT 0,
		Source TINYINT NOT NULL DEFAULT 0
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
        FROM MixingStation.RequestMixingStationDetail rd
        JOIN MixingStation.UnitDoseType udt ON udt.Id = rd.UnitDoseTypeId
        LEFT JOIN MixingStation.Package p ON rd.PackageId = p.Id
        WHERE rd.CampaignDetailId = @CampaignDetailId 
        AND rd.[STATUS] <> 3
        AND rd.RequestPackageDetailStatusId IS NULL 
        AND udt.MSClass = 5
    )
    BEGIN
        INSERT INTO @RequestMixingStationDetail
		SELECT 
			rd.Id,
			rd.RequestMixingStationId,
			rd.ATCId,
			rd.PackageId,
			rd.PackagePersonalizedId,
			rd.UnitDoseTypeId,
			CASE WHEN ISNULL(p.VehicleOptimization, 0) <> 1 AND rd.Quantity > 1 THEN 1 ELSE rd.Quantity END AS Quantity,
			rd.[STATUS],
			rd.EntityId,
			rd.EntityName,
			rd.CareCenterCode,
			rd.[Source],
			rd.ProductionLineId,
			rd.CampaignDetailId,
			rd.LabelType,
			rd.SendTo,
			rd.ConfirmationUser,
			rd.ConfirmationDate
		FROM MixingStation.RequestMixingStationDetail rd
		LEFT JOIN MixingStation.Package p ON rd.PackageId = p.Id
		CROSS APPLY (
			SELECT TOP (CASE 
			 -- Paquetes personalizados: siempre 1 fila (la cantidad ya está calculada)
				WHEN rd.PackagePersonalizedId IS NOT NULL THEN 1
				WHEN ISNULL(p.VehicleOptimization, 0) <> 1 AND rd.Quantity > 1 THEN rd.Quantity 
				ELSE 1 
			END) 1 AS n
			FROM sys.all_objects so
		) AS tally
		WHERE rd.CampaignDetailId = @CampaignDetailId
		AND rd.[STATUS] <> 3
		AND rd.RequestPackageDetailStatusId IS NULL;
    END
    ELSE
    BEGIN
        INSERT INTO @RequestMixingStationDetail
        SELECT Id, RequestMixingStationId, ATCId, PackageId, PackagePersonalizedId, 
               UnitDoseTypeId, Quantity, [STATUS], EntityId, EntityName, CareCenterCode, 
               [Source], ProductionLineId, CampaignDetailId, LabelType, SendTo, 
               ConfirmationUser, ConfirmationDate
        FROM MixingStation.RequestMixingStationDetail rd
        WHERE rd.CampaignDetailId = @CampaignDetailId
        AND rd.[STATUS] <> 3;
    END

    ;WITH CampaignDetailWithRequests AS (
    SELECT 
        rd.Id,
		-- Determinar el ItemId basándonos en el tipo de producto
        COALESCE(pp.Id, p.Id, a.Id) AS ItemId,
        
		-- Determinar el ItemType basándonos en las relaciones de las tablas
        CASE 
            WHEN pp.Id IS NOT NULL THEN 3 -- PackagePersonalized
            WHEN p.Id IS NOT NULL THEN 1 -- Package
            WHEN a.Id IS NOT NULL THEN 2 -- ATC
        END AS ItemType,

		-- Si la cantidad en RequestMixingStationDetailPatients es nula, se usa la cantidad de la tabla principal
        COALESCE(rdp.Quantity, rd.Quantity) AS RequestQuantity,
        COALESCE(rdp.CampaignDetailId, rd.CampaignDetailId) AS CampaignDetailId,

        p.VehicleOptimization,
        rd.TmpId,
        udt.MSClass,
        rd.PackageId,
        rd.Source,
        CAST(CASE
            WHEN pp.Id IS NOT NULL AND EXISTS (
                SELECT 1
                FROM MixingStation.PackagePersonalizedDetail ppdComp
                WHERE ppdComp.PackagePersonalizedId = pp.Id
                AND ISNULL(ppdComp.ComplementaryMedicine, 0) = 1
            ) THEN 1
            ELSE 0
        END AS BIT) AS HasComplementaryMedicine
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
        GROUP BY RequestMixingStationDetailId, CampaignDetailId
    ) rdp ON rdp.RequestMixingStationDetailId = rd.Id
    WHERE (rdp.CampaignDetailId IS NOT NULL OR rd.CampaignDetailId IS NOT NULL)
    AND rd.[STATUS] <> 3
	),
	-- CTE: Lotes distintos - EXCLUYE NPT (MSClass = 2) del filtro
	DistinctBatchCodes AS (
		SELECT DISTINCT 
			rpds.BatchCode, 
			rpds.RequestMixingStationDetailId
		FROM MixingStation.RequestPackageDetailStatus rpds
		INNER JOIN MixingStation.RequestMixingStationDetail rmsd ON rmsd.id = rpds.RequestMixingStationDetailId
		INNER JOIN MixingStation.UnitDoseType udt ON udt.Id = rmsd.UnitDoseTypeId
		WHERE rmsd.CampaignDetailId = @CampaignDetailId
		AND rmsd.[STATUS] <> 3
		AND rpds.BatchCode IS NOT NULL
		AND rpds.BatchCode <> ''
		AND udt.MSClass <> 2  -- EXCLUIR NPT del filtro de BatchCode
	),
	cc AS (
		SELECT 
		 -- Determinamos el tipo de producto basándonos en las relaciones entre las tablas
			CASE 
				WHEN pd.AtcId IS NOT NULL THEN 1 -- ATC
				WHEN pd.SupplieId IS NOT NULL THEN 2 -- Insumo
				WHEN pd.ProductId IS NOT NULL THEN 3 -- Producto
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
			CAST(IIF(ISNULL(pd.MainMedicine, 0) = 1 OR ISNULL(pd.ComplementaryMedicine, 0) = 1, 1, 3) AS TINYINT) AS ItemType,

			 -- Determina la agrupacion del item
			v.PackageId,
			IIF(ISNULL(pd.MainMedicine, 0) = 1 OR ISNULL(pd.ComplementaryMedicine, 0) = 1, 'Principal', 'Otro') AS GroupName,

			CASE
				WHEN v.MSClass = 2 THEN  -- NPT primero, luego medicamentos principales de paquetes personalizados
					pd.Quantity * v.RequestQuantity

				WHEN @Type = 2 THEN --Unidad de medida en ML
					CASE
						WHEN v.ItemType = 3 THEN --Paquete Personalizado
							CASE
								WHEN (pd.MainMedicine = 1 OR ISNULL(pd.ComplementaryMedicine, 0) = 1) AND a2.FormulationType = 1 THEN --Peso
									(pd.Quantity * (SELECT TOP 1 
														pd2.Quantity 
													 FROM MixingStation.PackageDetail pd2 
													 WHERE pd2.PackageId = v.PackageId AND pd2.Thinner = 1) / a2.Weight)

								WHEN (pd.MainMedicine = 1 OR ISNULL(pd.ComplementaryMedicine, 0) = 1) AND a2.FormulationType = 3 THEN --Peso - Volumen
									((pd.Quantity * a2.Volume) / a2.Weight)

								ELSE
									pd.Quantity * v.RequestQuantity
							END

						WHEN v.ItemType = 1 THEN --Paquete estandar
							CASE
								WHEN (pd.MainMedicine = 1 OR ISNULL(pd.ComplementaryMedicine, 0) = 1) AND a2.FormulationType = 1 THEN --Peso
									(pd.Quantity * (SELECT TOP 1 
													pd2.Quantity 
													 FROM MixingStation.PackageDetail pd2 
													 WHERE pd2.PackageId = v.PackageId AND pd2.Thinner = 1) / a2.Weight)

								WHEN (pd.MainMedicine = 1 OR ISNULL(pd.ComplementaryMedicine, 0) = 1) AND a2.FormulationType = 3 THEN --Peso - Volumen
									(pd.Quantity * a2.Volume) / a2.Weight
						
								ELSE
									pd.Quantity * v.RequestQuantity
							END
						ELSE
							pd.Quantity * v.RequestQuantity
					END
				ELSE --Unidad de medida original
					pd.Quantity * v.RequestQuantity
				END AS Quantity,

			-- Obtener las unidades de medida correspondientes
			IIF(@Type = 2 AND pd.AtcId IS NOT NULL, '019', UnitPeso.Code) AS CodigoUnidadPeso,
			UnitVolumen.Code AS CodigoUnidadVolumen,

			-- Otros detalles del producto
			a2.FormulationType,
			a2.ATCEntityId,
			a2.PharmaceuticalFormId,

			-- Calculo de concentracion
			CASE 
				WHEN v.MSClass = 2 THEN ISNULL(a2.Volume, 0)
				WHEN a2.FormulationType IN (1, 3) THEN a2.Weight
				WHEN a2.FormulationType = 4 then a2.ConcentrationQuantity 
				ELSE a2.Volume
			END AS Concentration,

			-- Agrupar por los productos de vehículos optimizados
			IIF(v.VehicleOptimization = 0 AND pd.Vehicle = 1, v.TmpId, 0) AS ItemGroup,

			-- Obtener BatchCode
			IIF(v.MSClass IN (5, 7),
				(SELECT TOP 1 rpds.BatchCode 
				 FROM MixingStation.RequestPackageDetailStatus rpds 
				 WHERE rpds.RequestMixingStationDetailId = v.Id
				 GROUP BY rpds.BatchCode),
				(SELECT STRING_AGG(rpds.BatchCode, ',')
				 FROM MixingStation.RequestPackageDetailStatus rpds 
				 WHERE rpds.RequestMixingStationDetailId = v.Id)
			) AS BatchCode,

			pd.Quantity AS QuantityPackage,
			v.Id AS RequestMixingStationDetailId,
			pd.Thinner,
			IIF(pd.Thinner = 1, 
				COALESCE(
					(SELECT TOP 1 ppd.Quantity 
					 FROM MixingStation.PackagePersonalizedDetail ppd 
					 WHERE v.ItemType = 3 AND ppd.PackagePersonalizedId = v.ItemId AND ppd.Thinner = 1),
					(SELECT TOP 1 pd2.Quantity 
					 FROM MixingStation.PackageDetail pd2 
					 WHERE pd2.PackageId = v.PackageId AND pd2.Thinner = 1),
					0
				),
				NULL
			) AS QuantityThinner,
			pd.Vehicle,
			pd.NPTItemOrder,
			v.MSClass,
			v.VehicleOptimization,
			CASE 
				WHEN v.MSClass IN (5,7) THEN vcdu.MSAuxName
				ELSE vcdu.QFProductionName
			END AS VerifiedFor,
			vcdu.QFQualityName ConfirmedFor,
			-- Indica si el registro viene de un paquete personalizado (v.ItemType = 3)
			CAST(IIF(v.ItemType = 3, 1, 0) AS BIT) AS IsPackagePersonalized,
			v.HasComplementaryMedicine,
			v.Source
		FROM CampaignDetailWithRequests v
		INNER JOIN (
			SELECT DISTINCT k.*
			FROM (
				SELECT 1 AS ItemType, AtcId, SupplieId, ProductId, PackageId AS Id, ComponentType, MainMedicine, ISNULL(ComplementaryMedicine, 0) AS ComplementaryMedicine, Thinner, 
					   Vehicle, Quantity, MeasurementUnitId, Quantity AS Volume, MeasurementUnitId AS VolumeMeasureUnit, NPTItemOrder
				FROM MixingStation.PackageDetail

				UNION ALL

				SELECT 3 AS ItemType, AtcId, SupplieId, ProductId, PackagePersonalizedId AS Id, ComponentType, MainMedicine, ISNULL(ComplementaryMedicine, 0) AS ComplementaryMedicine,
					   Thinner, Vehicle, Quantity, MeasurementUnitId, Volume, VolumeMeasureUnit, NPTItemOrder
				FROM MixingStation.PackagePersonalizedDetail
			) AS k
		) pd ON pd.Id = v.ItemId AND pd.ItemType = v.ItemType
		LEFT JOIN Inventory.ATC a2 ON a2.Id = pd.AtcId
		LEFT JOIN Inventory.InventorySupplie s ON s.Id = pd.SupplieId
		LEFT JOIN Inventory.InventoryProduct prod ON prod.Id = pd.ProductId
		LEFT JOIN Inventory.InventoryMeasurementUnit UnitPeso ON UnitPeso.id = pd.MeasurementUnitId
		LEFT JOIN Inventory.InventoryMeasurementUnit UnitVolumen ON UnitVolumen.id = pd.VolumeMeasureUnit
		LEFT JOIN MixingStation.ViewCampaignDetailUser vcdu ON vcdu.Id = @CampaignDetailId
		-- FILTRO CONDICIONAL: Solo para NO-NPT, verificar BatchCode
		WHERE (v.MSClass = 2 OR EXISTS (
			SELECT 1 
			FROM DistinctBatchCodes dbc 
			WHERE dbc.RequestMixingStationDetailId = v.Id
		))

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
					WHEN a2.FormulationType = 4 then a2.ConcentrationQuantity
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
				, v.MSClass
				, v.VehicleOptimization
				,CASE 
					WHEN v.MSClass IN (5,7) THEN vcdu.MSAuxName
					ELSE vcdu.QFProductionName
				END AS VerifiedFor,
				vcdu.QFQualityName ConfirmedFor,
				CAST(0 AS BIT) AS IsPackagePersonalized, -- ATCs directos no son paquetes personalizados
				CAST(0 AS BIT) AS HasComplementaryMedicine,
				v.Source
			FROM CampaignDetailWithRequests v
			INNER JOIN Inventory.ATC a2 ON a2.Id = v.ItemId AND v.ItemType = 2
			LEFT JOIN Inventory.InventoryMeasurementUnit UnitPeso ON UnitPeso.id = a2.WeightMeasureUnit				 
			LEFT JOIN Inventory.InventoryMeasurementUnit UnitVolumen ON UnitVolumen.id = a2.VolumeMeasureUnit
			LEFT JOIN MixingStation.ViewCampaignDetailUser vcdu ON vcdu.Id = @CampaignDetailId
			-- FILTRO CONDICIONAL: Solo para NO-NPT, verificar BatchCode
			WHERE (v.MSClass = 2 OR EXISTS (
				SELECT 1 
				FROM DistinctBatchCodes dbc 
				WHERE dbc.RequestMixingStationDetailId = v.Id
			))
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
			-- Los componentes de materia prima deben conservar la cantidad real definida
			-- para cada preparación, tanto en paquetes base como personalizados.
			WHEN tmp.ItemType = 3 THEN
				MixingStation.CalculationQuantityMaterialRaw(
					FormulationType,
					Code,
					SUM(Quantity),
					CASE
						WHEN FormulationType IN (1, 3) THEN CodigoUnidadPeso
						WHEN FormulationType = 4 THEN 1
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
						when FormulationType = 4 then 1
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
		tmp.Vehicle,
		tmp.NPTItemOrder,
		tmp.RequestMixingStationDetailId,
		tmp.VerifiedFor,
		tmp.ConfirmedFor,
		tmp.IsPackagePersonalized,
		tmp.HasComplementaryMedicine,
		tmp.Source
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
		tmp.Vehicle,
		tmp.NPTItemOrder,
		tmp.MSClass,
		tmp.RequestMixingStationDetailId,
		tmp.VehicleOptimization,
		tmp.Quantity,
		tmp.VerifiedFor,
		tmp.ConfirmedFor,
		tmp.IsPackagePersonalized,
		tmp.HasComplementaryMedicine,
		tmp.Source

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
			, td.Vehicle
			, td.NPTItemOrder
            , td.RequestMixingStationDetailId
			, VerifiedFor
			, ConfirmedFor
			, td.IsPackagePersonalized
			, td.HasComplementaryMedicine
			, td.Source
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
			, PackageId
			, td.Vehicle
			, td.NPTItemOrder
            , td.RequestMixingStationDetailId
			, VerifiedFor
			, ConfirmedFor
			, td.IsPackagePersonalized
			, td.HasComplementaryMedicine
			, td.Source

		UNION ALL --Consultamos los productos de la canasta

		SELECT
			pbd.ComponentType TypeProduct, 
			CASE pbd.ComponentType 
				WHEN 1 THEN 'ATC' 
				WHEN 2 THEN 'Insumo' 
				WHEN 3 THEN 'Producto' 
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
            , NULL
			, CAST(0 AS BIT) AS IsPackagePersonalized  -- Canasta no es paquete personalizado
			, CAST(0 AS BIT) AS HasComplementaryMedicine
			, CAST(0 AS TINYINT) AS Source
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
				WHEN a2.Id IS NOT NULL THEN 1
				WHEN pd.Id IS NOT NULL AND pd.AtcId IS NOT NULL THEN 1
				WHEN pd.Id IS NOT NULL AND pd.SupplyId IS NOT NULL THEN 2
				WHEN pd.Id IS NOT NULL AND pd.ProductId IS NOT NULL THEN 3
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
				WHEN pd.Id IS NOT NULL AND pd.AtcId IS NOT NULL THEN a2.NAME
				WHEN pd.Id IS NOT NULL AND pd.SupplyId IS NOT NULL THEN s.SupplieName
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
            , NULL
			, CAST(0 AS BIT) AS IsPackagePersonalized -- Solicitudes manuales no son paquetes personalizados
			, CAST(0 AS BIT) AS HasComplementaryMedicine
			, CAST(0 AS TINYINT) AS Source
		FROM  MixingStation.CampaignDetailItems pd
		LEFT JOIN Inventory.ATC a2 ON a2.Id = pd.AtcId
		LEFT JOIN Inventory.InventorySupplie s ON s.Id = pd.SupplyId
		LEFT JOIN Inventory.InventoryProduct prod ON prod.Id = pd.ProductId
		LEFT JOIN Inventory.InventoryMeasurementUnit f ON f.Id =  prod.MeasurementUnitId
		LEFT JOIN Inventory.InventoryMeasurementUnit UnitPeso ON UnitPeso.id = a2.WeightMeasureUnit 
		LEFT JOIN Inventory.InventoryMeasurementUnit UnitVolumen ON UnitVolumen.id = a2.VolumeMeasureUnit 
		WHERE pd.CampaignDetailId = @CampaignDetailId AND pd.ItemType = 4

	END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los productos o componentes (medicamentos, paquetes estándar, paquetes personalizados y principios activos ATC) que conforman una campaña de preparación en la estación de mezclas (MixingStation), identificada por el detalle de campaña (@CampaignDetailId) y filtrada por tipo (@Type). Consulta los detalles de solicitud de mezcla (RequestMixingStationDetail), el tipo de dosis unitaria (UnitDoseType) y el empaque o paquete asociado (Package), aplicando lógica de expansión de filas según cantidad y optimización de vehículo, para determinar cuántas preparaciones individuales deben producirse. Si no existen registros en estado de readecuación (MSClass=5 sin estado de paquete), reconstruye las filas multiplicando por la cantidad prescrita; de lo contrario, usa directamente los registros existentes. El resultado es utilizado por el proceso de producción farmacéutica para conocer exactamente qué ítems, en qué cantidades y de qué tipo deben elaborarse dentro de una campaña de mezclas.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'PROCEDURE', @level1name = N'SP_ProductListCampaing';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'PROCEDURE', @level1name = N'SP_ProductListCampaing';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida y devuelve los productos, componentes y cantidades a preparar en una campaña de mezclas farmacéuticas, combinando solicitudes de paquetes, paquetes personalizados, ATCs directos, canasta de producción e ítems manuales.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_ProductListCampaing';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'@CampaignDetailId debe corresponder a un detalle de campaña existente en MixingStation.CampaignDetail.; Los detalles de solicitud (RequestMixingStationDetail) asociados a la campaña deben tener UnitDoseType válido; las cantidades dependen del flag VehicleOptimization del Package.; Para clasificar correctamente el ítem, RequestMixingStationDetail debe tener informado al menos uno entre PackagePersonalizedId, PackageId o ATCId.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_ProductListCampaing';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen filas con Quantity > 0 al insertar en @TablaMedicamentosDX.; Los pacientes con STATUS=3 en RequestMixingStationDetailPatients se excluyen del cálculo de RequestQuantity.; Los paquetes personalizados siempre se tratan como una única fila con la cantidad ya calculada (no se expanden).; Las solicitudes con RequestPackageDetailStatusId NO NULL no se reexpanden (se asume ya procesadas).; Cuando @Type=2 y el ítem es ATC, la unidad de peso reportada se normaliza al código ''019''.; TypeProduct codifica: 1=ATC, 2=Insumo/Canasta, 3=Producto; ItemType en la salida codifica: 1=Principal, 2=Canasta, 3=Otro/Personalizado.; Los ítems de CampaignDetailItems solo se incluyen cuando ItemType=4 (Solicitudes Manuales).; Los ítems de la canasta se obtienen vía CampaignDetail.ProductionBasketId → ProductionBasketsDetail.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_ProductListCampaing';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @TablaMedicamentosDX: Inserta los productos/componentes calculados desde el CTE cc, agrupados, solo cuando tmp.Quantity > 0; calcula DosisRequerida vía MixingStation.CalculationQuantityMaterialRaw para ItemType=1 o cuando VehicleOptimization<>0.; [INSERT] @RequestMixingStationDetail: Si no existen filas para la campaña con UnitDoseType.MSClass=5 y RequestPackageDetailStatusId IS NULL, expande las solicitudes vía CROSS APPLY (TOP rd.Quantity) cuando VehicleOptimization<>1 y Quantity>1, generando una fila por unidad; los paquetes personalizados siempre generan 1 fila.; [INSERT] @RequestMixingStationDetail: Si ya existen filas con MSClass=5 y RequestPackageDetailStatusId IS NULL, copia los detalles tal cual sin expansión.; [RETURN_RESULT] RESULT: Retorna la unión de: (1) productos agrupados desde @TablaMedicamentosDX con su unidad de medida; (2) productos de la canasta de producción (ProductionBasketsDetail) marcados con GroupName=''Canasta''; (3) ítems de CampaignDetailItems con ItemType=4 marcados como ''Solicitudes Manuales''.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_ProductListCampaing';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si NOT EXISTS RequestMixingStationDetail con MSClass=5 y RequestPackageDetailStatusId NULL para la campaña → Carga @RequestMixingStationDetail expandiendo cantidades vía CROSS APPLY (una fila por dosis cuando VehicleOptimization<>1 y Quantity>1) else Carga @RequestMixingStationDetail sin expansión, con la cantidad original; si Tipo de ítem según relaciones: PackagePersonalizedId NOT NULL → 3; PackageId NOT NULL → 1; ATCId NOT NULL → 2 → Determina ItemType y aplica la lógica de cálculo de cantidad correspondiente; si MSClass = 2 AND MainMedicine=1 AND @Type=2 AND FormulationType=3 → Quantity = RequestQuantity * pd.Volume else Otras combinaciones aplican fórmulas distintas (Quantity*RequestQuantity, conversión mg→ml usando Weight, uso del diluyente del paquete, etc.); si ItemType=3 (paquete personalizado) AND @Type=2 AND AtcId NOT NULL → Convierte a ml según FormulationType=1 usa (Volume*Quantity)/Weight; FormulationType=3 usa (a2.Volume*Quantity)/Weight; FormulationType=2 (vehículo) usa cantidad directa en ml; si MainMedicine=1 AND @Type=2 AND FormulationType=1 (cálculo por concentración con diluyente) → Quantity = (cantidad del diluyente del paquete o RequestQuantity * pd.Quantity) / a2.Weight; si MSClass IN (5,7) → BatchCode = TOP 1 BatchCode de RequestPackageDetailStatus; VerifiedFor = vcdu.MSAuxName else BatchCode = STRING_AGG de DistinctBatchCodes; VerifiedFor = vcdu.QFProductionName; si ItemType=1 (paquete) en cálculo de DosisRequerida → Llama MixingStation.CalculationQuantityMaterialRaw con unidad de peso/volumen según FormulationType else Si VehicleOptimization=0 mantiene Concentration; si no, llama CalculationQuantityMaterialRaw con regla extendida (FormulationType=4 usa unidad 1); si @Type=2 AND pd.AtcId NOT NULL → CodigoUnidadPeso se forza a ''019'' (mg) else Usa UnitPeso.Code del ítem; si pd.Thinner = 1 → QuantityThinner = cantidad del diluyente desde PackagePersonalizedDetail (si ItemType=3) o PackageDetail; sino NULL; si VehicleOptimization=0 AND pd.Vehicle=1 → ItemGroup = TmpId (agrupa vehículos no optimizados) else ItemGroup = 0', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_ProductListCampaing';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_ProductListCampaing';
-- GO
