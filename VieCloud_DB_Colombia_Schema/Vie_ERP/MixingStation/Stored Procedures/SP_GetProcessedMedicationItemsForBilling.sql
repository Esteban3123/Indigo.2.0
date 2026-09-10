-- ===============================================================================================================================
-- Author:		Andrea Coqueco
-- Create date: 02/07/2025
-- Description:	Procedimiento que se encarga de listar los productos transformados en la pestaña central de mezclas del control de cuentas hospitalario
-- ===============================================================================================================================

CREATE PROCEDURE [MixingStation].[SP_GetProcessedMedicationItemsForBilling]
    @AdmissionNumber VARCHAR(50) = '',
	@isChild BIT = 0
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        
		-- Materializar DispensingDose: Contiene los datos de productos que SI tuvieron una dispensación.
		-- Para NPT desde dashboard: el batch puede no estar en RequestPackageDetailStatus. Se usa LEFT JOIN
		-- y GroupingCodeDose desde PharmaDose cuando rpds no existe.
		-- Cuando una dispensación consolida varias solicitudes del mismo producto, DetailPhysicalCUM conserva
		-- el detalle real por GroupingCodeDose y evita perder hijos por el fallback TOP 1.
        SELECT
            pd.Id AS PharmaceuticalDispensingId,
            pdd.Id AS PharmaceuticalDispensingDetailId,
            pdd.ProductId,
            ip.Code,
            ip.Name,
            a.Code AS AtcCode,
            SUM(pddb.OutstandingQuantity) AS OutstandingQuantity,
            pd.AdmissionNumber,
            bs.BatchCode,
            pdd.ServiceDate AS DispensingDate,
            pdd.EntityId,
            pdd.EntityName,
            pdd.FunctionalUnitId,
            pdd.SurchargeApply,
            pdd.OrderedHealthProfessionalCode,
            pdd.OrderedHealthProfessionalThirdPartyId,
            pdd.HealthAdministratorId,
            pdd.OrderedProfessionalSpecialty,
            pdd.WarehouseId
        INTO #DispensingBase
        FROM Inventory.PharmaceuticalDispensingDetailBatchSerial AS pddb
        JOIN Inventory.PharmaceuticalDispensingDetail AS pdd ON pdd.Id = pddb.PharmaceuticalDispensingDetailId
        JOIN Inventory.PharmaceuticalDispensing AS pd ON pd.Id = pdd.PharmaceuticalDispensingId
        JOIN Inventory.InventoryProduct AS ip ON pdd.ProductId = ip.Id
        JOIN Inventory.ATC AS a ON a.Id = ip.ATCId
        JOIN Inventory.ProductType AS pt ON ip.ProductTypeId = pt.Id AND pt.Class = 5
        JOIN Inventory.PhysicalInventory AS pi ON pddb.PhysicalInventoryId = pi.Id
        JOIN Inventory.BatchSerial AS bs ON pi.BatchSerialId = bs.Id
        WHERE pddb.OutstandingQuantity > 0
          AND pd.AdmissionNumber = @AdmissionNumber
        GROUP BY
            pd.Id,
            pdd.Id,
            pdd.ProductId,
            ip.Code,
            ip.Name,
            a.Code,
            pd.AdmissionNumber,
            bs.BatchCode,
            pdd.ServiceDate,
            pdd.EntityId,
            pdd.EntityName,
            pdd.FunctionalUnitId,
            pdd.SurchargeApply,
            pdd.OrderedHealthProfessionalCode,
            pdd.OrderedHealthProfessionalThirdPartyId,
            pdd.HealthAdministratorId,
            pdd.OrderedProfessionalSpecialty,
            pdd.WarehouseId;

        CREATE NONCLUSTERED INDEX IX_DispensingBase_Detail
            ON #DispensingBase (PharmaceuticalDispensingDetailId, ProductId, BatchCode);

        CREATE NONCLUSTERED INDEX IX_DispensingBase_Entity
            ON #DispensingBase (EntityName, EntityId, AdmissionNumber);

        SELECT DISTINCT
            db.PharmaceuticalDispensingDetailId,
            ph.GroupingCodeDose
        INTO #DispensingEntityGrouping
        FROM #DispensingBase AS db
        JOIN HCFARMEPD AS hcfd ON hcfd.Id = db.EntityId
        JOIN MedicalHistory.PharmaDose AS ph ON ph.CodeSusceptibleMixingStation = hcfd.CodeSusceptibleMixingStation
            AND ph.IDHCFARMEPC = hcfd.CODCONCEC
        WHERE db.EntityName = 'HCFARMEPD';

        CREATE UNIQUE NONCLUSTERED INDEX IX_DispensingEntityGrouping
            ON #DispensingEntityGrouping (PharmaceuticalDispensingDetailId, GroupingCodeDose);

        SELECT
            db.PharmaceuticalDispensingDetailId,
            db.ProductId,
            db.BatchCode,
            MAX(COALESCE(rpds.PackageId, rmd_meta.PackageId)) AS PackageId,
            MAX(COALESCE(rpds.PackagePersonalizedId, rmd_meta.PackagePersonalizedId)) AS PackagePersonalizedId,
            MAX(rmd_meta.Source) AS Source
        INTO #DispensingPackageMetadata
        FROM #DispensingBase AS db
        JOIN MixingStation.RequestPackageDetailStatus AS rpds ON rpds.BatchCode = db.BatchCode
            AND rpds.ProductId = db.ProductId
        LEFT JOIN MixingStation.RequestMixingStationDetail AS rmd_meta ON rmd_meta.Id = rpds.RequestMixingStationDetailId
        GROUP BY
            db.PharmaceuticalDispensingDetailId,
            db.ProductId,
            db.BatchCode;

        CREATE UNIQUE NONCLUSTERED INDEX IX_DispensingPackageMetadata
            ON #DispensingPackageMetadata (PharmaceuticalDispensingDetailId, ProductId, BatchCode);

        SELECT
            db.PharmaceuticalDispensingDetailId,
            db.ProductId,
            db.BatchCode,
            rpds.GroupingCodeDose,
            MAX(COALESCE(rpds.PackageId, rmd_meta.PackageId)) AS PackageId,
            MAX(COALESCE(rpds.PackagePersonalizedId, rmd_meta.PackagePersonalizedId)) AS PackagePersonalizedId,
            MAX(rmd_meta.Source) AS Source
        INTO #DispensingGroupingMetadata
        FROM #DispensingBase AS db
        JOIN MixingStation.RequestPackageDetailStatus AS rpds ON rpds.BatchCode = db.BatchCode
            AND rpds.ProductId = db.ProductId
            AND rpds.GroupingCodeDose IS NOT NULL
        LEFT JOIN MixingStation.RequestMixingStationDetail AS rmd_meta ON rmd_meta.Id = rpds.RequestMixingStationDetailId
        GROUP BY
            db.PharmaceuticalDispensingDetailId,
            db.ProductId,
            db.BatchCode,
            rpds.GroupingCodeDose;

        CREATE UNIQUE NONCLUSTERED INDEX IX_DispensingGroupingMetadata
            ON #DispensingGroupingMetadata (PharmaceuticalDispensingDetailId, ProductId, BatchCode, GroupingCodeDose);

        SELECT
            db.PharmaceuticalDispensingDetailId,
            db.ProductId,
            db.BatchCode,
            dpc.GroupingCodeDose,
            COALESCE(MAX(dgm.PackageId), MAX(dpm.PackageId)) AS PackageId,
            COALESCE(MAX(dgm.PackagePersonalizedId), MAX(dpm.PackagePersonalizedId)) AS PackagePersonalizedId,
            COALESCE(MAX(dgm.Source), MAX(dpm.Source)) AS Source,
            SUM(CASE
                WHEN dpc.DispensedQuantity > ISNULL(dpc.ReturnedQuantity, 0)
                    THEN dpc.DispensedQuantity - ISNULL(dpc.ReturnedQuantity, 0)
                ELSE dpc.DispensedQuantity
            END) AS QuantityDelivered,
            CAST(1 AS TINYINT) AS SourceKind
        INTO #DispensingGroupingPhysical
        FROM #DispensingBase AS db
        JOIN MedicalHistory.DetailPhysicalCUM AS dpc ON dpc.BatchCode = db.BatchCode
            AND dpc.ProductId = db.ProductId
            AND dpc.GroupingCodeDose IS NOT NULL
        JOIN HCFISIPRO AS fis ON fis.ID = dpc.IDHCFISIPRO
            AND fis.NUMINGRES = db.AdmissionNumber
            AND fis.TIPREGIST = 1
        LEFT JOIN #DispensingEntityGrouping AS deg ON deg.PharmaceuticalDispensingDetailId = db.PharmaceuticalDispensingDetailId
            AND deg.GroupingCodeDose = dpc.GroupingCodeDose
        LEFT JOIN #DispensingGroupingMetadata AS dgm ON dgm.PharmaceuticalDispensingDetailId = db.PharmaceuticalDispensingDetailId
            AND dgm.ProductId = db.ProductId
            AND dgm.BatchCode = db.BatchCode
            AND dgm.GroupingCodeDose = dpc.GroupingCodeDose
        LEFT JOIN #DispensingPackageMetadata AS dpm ON dpm.PharmaceuticalDispensingDetailId = db.PharmaceuticalDispensingDetailId
            AND dpm.ProductId = db.ProductId
            AND dpm.BatchCode = db.BatchCode
        WHERE ISNULL(db.EntityName, '') <> 'HCFARMEPD'
           OR deg.GroupingCodeDose IS NOT NULL
        GROUP BY
            db.PharmaceuticalDispensingDetailId,
            db.ProductId,
            db.BatchCode,
            dpc.GroupingCodeDose;

        CREATE NONCLUSTERED INDEX IX_DispensingGroupingPhysical
            ON #DispensingGroupingPhysical (PharmaceuticalDispensingDetailId, ProductId, BatchCode, GroupingCodeDose);

        SELECT
            db.PharmaceuticalDispensingDetailId,
            db.ProductId,
            db.BatchCode,
            dgm.GroupingCodeDose,
            MAX(dgm.PackageId) AS PackageId,
            MAX(dgm.PackagePersonalizedId) AS PackagePersonalizedId,
            MAX(dgm.Source) AS Source,
            CAST(NULL AS INT) AS QuantityDelivered,
            CAST(2 AS TINYINT) AS SourceKind
        INTO #DispensingGroupingRpds
        FROM #DispensingBase AS db
        JOIN #DispensingGroupingMetadata AS dgm ON dgm.PharmaceuticalDispensingDetailId = db.PharmaceuticalDispensingDetailId
            AND dgm.ProductId = db.ProductId
            AND dgm.BatchCode = db.BatchCode
        WHERE NOT EXISTS (
            SELECT 1
            FROM #DispensingGroupingPhysical AS dgp
            WHERE dgp.PharmaceuticalDispensingDetailId = db.PharmaceuticalDispensingDetailId
              AND dgp.ProductId = db.ProductId
              AND dgp.BatchCode = db.BatchCode
        )
        GROUP BY
            db.PharmaceuticalDispensingDetailId,
            db.ProductId,
            db.BatchCode,
            dgm.GroupingCodeDose;

        CREATE NONCLUSTERED INDEX IX_DispensingGroupingRpds
            ON #DispensingGroupingRpds (PharmaceuticalDispensingDetailId, ProductId, BatchCode, GroupingCodeDose);

        SELECT
            dgs.PharmaceuticalDispensingDetailId,
            dgs.ProductId,
            dgs.BatchCode,
            dgs.GroupingCodeDose,
            MAX(dgs.PackageId) AS PackageId,
            MAX(dgs.PackagePersonalizedId) AS PackagePersonalizedId,
            MAX(dgs.Source) AS Source,
            MAX(dgs.QuantityDelivered) AS QuantityDelivered,
            MIN(dgs.SourceKind) AS SourceKind
        INTO #DispensingGroupingDose
        FROM (
            SELECT *
            FROM #DispensingGroupingPhysical

            UNION ALL

            SELECT *
            FROM #DispensingGroupingRpds
        ) AS dgs
        GROUP BY
            dgs.PharmaceuticalDispensingDetailId,
            dgs.ProductId,
            dgs.BatchCode,
            dgs.GroupingCodeDose;

        CREATE NONCLUSTERED INDEX IX_DispensingGroupingDose
            ON #DispensingGroupingDose (PharmaceuticalDispensingDetailId, GroupingCodeDose);

        SELECT
            ph_src.PharmaceuticalDispensingDetailId,
            ph_src.GroupingCodeDose
        INTO #DispensingPharmaFallback
        FROM (
            SELECT
                db.PharmaceuticalDispensingDetailId,
                ph.GroupingCodeDose,
                ROW_NUMBER() OVER (
                    PARTITION BY db.PharmaceuticalDispensingDetailId
                    ORDER BY CASE WHEN ph.DeliveryStatus IN (0, 1) THEN 0 ELSE 1 END, ph.Id ASC
                ) AS RowNumber
            FROM #DispensingBase AS db
            JOIN HCFARMEPD AS hcfd ON hcfd.Id = db.EntityId
            JOIN HCFARMEPC AS hcfc ON hcfc.CODCONCEC = hcfd.CODCONCEC
            JOIN MedicalHistory.PharmaDose AS ph ON ph.CodeSusceptibleMixingStation = hcfd.CodeSusceptibleMixingStation
                AND ph.IDHCFARMEPC = hcfd.CODCONCEC
            WHERE db.EntityName = 'HCFARMEPD'
              AND hcfc.NUMINGRES = db.AdmissionNumber
        ) AS ph_src
        WHERE ph_src.RowNumber = 1;

        CREATE UNIQUE NONCLUSTERED INDEX IX_DispensingPharmaFallback
            ON #DispensingPharmaFallback (PharmaceuticalDispensingDetailId);

        SELECT
            db.PharmaceuticalDispensingId,
            db.ProductId,
            db.Code,
            db.Name,
            db.AtcCode,
            CASE
                WHEN MAX(CASE WHEN gd.SourceKind = 1 THEN 1 ELSE 0 END) = 1
                    THEN MAX(gd.QuantityDelivered)
                ELSE SUM(db.OutstandingQuantity)
            END AS QuantityDelivered,
            COALESCE(gd.GroupingCodeDose, ph_fallback.GroupingCodeDose) AS GroupingCodeDose,
            db.AdmissionNumber,
            db.BatchCode,
            db.DispensingDate,
            COALESCE(gd.PackageId, dpm.PackageId) AS PackageId,
            COALESCE(gd.PackagePersonalizedId, dpm.PackagePersonalizedId) AS PackagePersonalizedId,
            ISNULL(COALESCE(gd.Source, dpm.Source), 0) AS Source,
            db.FunctionalUnitId,
            db.SurchargeApply,
            db.OrderedHealthProfessionalCode,
            db.OrderedHealthProfessionalThirdPartyId,
            db.HealthAdministratorId,
            db.OrderedProfessionalSpecialty,
            db.WarehouseId
        INTO #DispensingDose
        FROM #DispensingBase AS db
        LEFT JOIN #DispensingGroupingDose AS gd ON gd.PharmaceuticalDispensingDetailId = db.PharmaceuticalDispensingDetailId
            AND gd.ProductId = db.ProductId
            AND gd.BatchCode = db.BatchCode
        LEFT JOIN #DispensingPharmaFallback AS ph_fallback ON ph_fallback.PharmaceuticalDispensingDetailId = db.PharmaceuticalDispensingDetailId
        LEFT JOIN #DispensingPackageMetadata AS dpm ON dpm.PharmaceuticalDispensingDetailId = db.PharmaceuticalDispensingDetailId
            AND dpm.ProductId = db.ProductId
            AND dpm.BatchCode = db.BatchCode
        WHERE gd.GroupingCodeDose IS NOT NULL
           OR ph_fallback.GroupingCodeDose IS NOT NULL
        GROUP BY
            db.ProductId,
            db.FunctionalUnitId,
            db.SurchargeApply,
            db.OrderedHealthProfessionalCode,
            db.OrderedHealthProfessionalThirdPartyId,
            db.HealthAdministratorId,
            db.OrderedProfessionalSpecialty,
            db.WarehouseId,
            db.PharmaceuticalDispensingId,
            db.Code,
            db.Name,
            COALESCE(gd.GroupingCodeDose, ph_fallback.GroupingCodeDose),
            db.AdmissionNumber,
            db.BatchCode,
            db.DispensingDate,
            COALESCE(gd.PackageId, dpm.PackageId),
            COALESCE(gd.PackagePersonalizedId, dpm.PackagePersonalizedId),
            COALESCE(gd.Source, dpm.Source),
            db.AtcCode;

        CREATE NONCLUSTERED INDEX IX_DispensingDose ON #DispensingDose (GroupingCodeDose, AtcCode, AdmissionNumber);

        -- Materializar WithoutDispensation
        SELECT
            rpds.ProductId,
            ip.Code,
            ip.Name,
            SUM(rmd.Quantity) AS QuantityDelivered,
            rpds.GroupingCodeDose,
            phd.NUMINGRES      AS AdmissionNumber,
            bs.BatchCode,
            rpds.PackageId,
            rpds.PackagePersonalizedId,
            phd.WarehouseId,
            phd.FunctionalUnitId,
            phd.HealthAdministratorId,
            phd.OrderedHealthProfessionalCode,
            phd.OrderedHealthProfessionalThirdPartyId,
            phd.OrderedProfessionalSpecialty,
            phd.TypePayment,
            rmd.RequestMixingStationId,
            rmd.Source
        INTO #WithoutDispensation
        FROM MixingStation.RequestPackageDetailStatus AS rpds
        JOIN MixingStation.RequestMixingStationDetail AS rmd ON rmd.Id = rpds.RequestMixingStationDetailId
        JOIN Inventory.PhysicalInventory AS pi ON pi.Id = rpds.PhysicalInventoryId
        JOIN Inventory.BatchSerial AS bs ON pi.BatchSerialId = bs.Id
        JOIN MixingStation.CampaignDetail AS cd ON cd.Id = rmd.CampaignDetailId
        JOIN Inventory.InventoryProduct AS ip ON rpds.ProductId = ip.Id
        JOIN (
            SELECT 
                phd_sub.GroupingCodeDose,
                fpc.NUMINGRES,
                w.Id AS WarehouseId,
                fu.Id AS FunctionalUnitId,
                c.HealthAdministratorId,
                fpc.CODPROSAL AS OrderedHealthProfessionalCode,
                i.CODESPEC1 AS OrderedProfessionalSpecialty,
                t.Id AS OrderedHealthProfessionalThirdPartyId,
                cgm.TypePayment
            FROM MedicalHistory.PharmaDose AS phd_sub
            JOIN HCFARMEPC AS fpc ON fpc.CODCONCEC = phd_sub.IDHCFARMEPC
            JOIN ADINGRESO AS a ON a.NUMINGRES = fpc.NUMINGRES
            JOIN Inventory.Warehouse AS w ON w.Code = fpc.CODBODEGA
            JOIN Payroll.FunctionalUnit AS fu ON fu.Code = a.UFUCODIGO
            JOIN Contract.CareGroup AS cg ON cg.Id = a.GENCAREGROUP
            JOIN Contract.Contract AS c ON c.Id = cg.ContractId
            JOIN INPROFSAL AS i ON i.CODPROSAL = fpc.CODPROSAL
            JOIN Common.ThirdParty AS t ON t.Nit = i.CODIGONIT
            LEFT JOIN Contract.CareGroupMixLiquidation AS cgm ON cgm.CareGroupId = cg.Id
        ) AS phd 
            ON phd.GroupingCodeDose = rpds.GroupingCodeDose
        LEFT JOIN #DispensingDose AS cdd 
            ON cdd.GroupingCodeDose = rpds.GroupingCodeDose
        WHERE 
            cd.CampaignStatus = 6
            AND phd.TypePayment = 2
            AND rmd.Source = 1
            AND cdd.PharmaceuticalDispensingId IS NULL
            AND rpds.Status = 3
            AND (phd.NUMINGRES = @AdmissionNumber)
        GROUP BY 
            rpds.ProductId,
            ip.Code,
            ip.Name,
            rpds.GroupingCodeDose,
            bs.BatchCode,
            rpds.PackageId,
            rpds.PackagePersonalizedId,
            phd.NUMINGRES,
            phd.WarehouseId,
            phd.FunctionalUnitId,
            phd.HealthAdministratorId,
            phd.OrderedHealthProfessionalCode,
            phd.OrderedHealthProfessionalThirdPartyId,
            phd.OrderedProfessionalSpecialty,
            phd.TypePayment,
            rmd.RequestMixingStationId,
            rmd.Source;

        CREATE NONCLUSTERED INDEX IX_WithoutDispensation 
            ON #WithoutDispensation (GroupingCodeDose, AdmissionNumber);

        -- Ensamblar resultado final		
		--------------------------------------------------------------------- PADRES -----------------------------------------------------------------------------------------

		IF ISNULL(@isChild, 0) = 0
		BEGIN
			-- Materializar PharmaDose: Agrupado por GroupingCodeDose para obtener una fila por aplicación/dosis.
			-- AppliedDose y QuantityReceivable: usar MAX (no SUM) porque representan la cantidad del producto terminado.
			SELECT
				ph.CodeSusceptibleMixingStation,
				ph.GroupingCodeDose,
				MAX(ph.AppliedDose) AS AppliedDose,
				MAX(ph.QuantityReceivable) AS QuantityReceivable,
				hcf.NUMINGRES
			INTO #PharmaDose
			FROM MedicalHistory.PharmaDose AS ph
			JOIN HCFARMEPC AS hcf ON hcf.CODCONCEC = ph.IDHCFARMEPC
			WHERE hcf.NUMINGRES = @AdmissionNumber
			GROUP BY 
				ph.GroupingCodeDose, ph.CodeSusceptibleMixingStation, hcf.NUMINGRES;

			CREATE NONCLUSTERED INDEX IX_PharmaDose ON #PharmaDose (GroupingCodeDose, NUMINGRES);

			----------------------------------------------------------------
			-- Padre con dispensación
			-- QuantityDelivered: suma de cantidades dispensadas (todas las dosis).
			-- AppliedDose: suma de cantidades realmente aplicadas (solo las que tienen DeliveryStatus=2).
			-- QuantityReceivable: suma de cantidades a cobrar (solo las aplicadas).
			-- #PharmaDose está agrupado por GroupingCodeDose (una fila por aplicación); aquí agrupamos por dispensación.
			SELECT
				NEWID() AS Id, MAX(psms.FullProductName) AS FullProductName, MAX(psms.ApplicationsNumber) AS DoseNumber, cdd.ProductId, cdd.Code,
				CONCAT(cdd.Code, '-', cdd.Name) AS ProductCodName,
				-- COUNT(DISTINCT GroupingCodeDose): cada GroupingCodeDose representa 1 unidad/dosis del producto terminado.
				-- SUM(QuantityDelivered) inflaba el total cuando RequestPackageDetailStatus tiene múltiples filas por batch
				-- (unidades no dispensadas aún de la campaña con GroupingCodeDose=NULL producen fan-out via ph_fallback).
				COUNT(DISTINCT cp.GroupingCodeDose) AS QuantityDelivered,
				SUM(cp.AppliedDose) AS AppliedDose,
				SUM(cp.QuantityReceivable) AS QuantityReceivable, 
				NULL AS DeliveryStatusName, NULL AS DeliveryStatus, NULL AS Child, cdd.AdmissionNumber,
				NULL AS BatchCode, MIN(cdd.DispensingDate) AS DispensingDate, CAST(0 AS BIT) AS Selected, NULL AS PackageId, NULL AS PackagePersonalizedId, cdd.FunctionalUnitId, NULL AS SurchargeApply,
				NULL AS OrderedHealthProfessionalCode, NULL AS OrderedHealthProfessionalThirdPartyId, NULL AS HealthAdministratorId,
				NULL AS OrderedProfessionalSpecialty, NULL AS WarehouseId, NULL AS AppliedDateDose, cdd.PharmaceuticalDispensingId AS Father,
				NULL AS PaidToEndCampaign, CAST(0 AS TINYINT) AS Source
			FROM #DispensingDose AS cdd
			-- Enlace por GroupingCodeDose; ProductSusceptibleMixingStation valida que el producto corresponde a la fórmula.
			JOIN #PharmaDose AS cp ON cdd.GroupingCodeDose = cp.GroupingCodeDose AND cdd.AdmissionNumber = cp.NUMINGRES
			JOIN MedicalHistory.ProductSusceptibleMixingStation AS psms ON psms.CodeSusceptibleMixingStation = cp.CodeSusceptibleMixingStation 
				AND (psms.MainDrugCode = cdd.AtcCode OR psms.MainDrugCode = cdd.Code)
			GROUP BY 
				cdd.ProductId, cdd.Code, cdd.Name,
				cdd.AdmissionNumber, cdd.FunctionalUnitId, cdd.PharmaceuticalDispensingId

			RETURN
		END
		
		--------------------------------------------------------------------- HIJOS -----------------------------------------------------------------------------------------

		-- Materializar PharmaDoseDetail: Contiene los datos de detalle de PharmaDose agrupados por GroupingCodeDose.
		-- Cada GroupingCodeDose representa una aplicación/dosis; puede tener múltiples ingredientes pero un solo DeliveryStatus.
		-- AppliedDose y QuantityReceivable: usar MAX (no SUM) porque cada ingrediente tiene el mismo valor y representan
		-- la cantidad del producto terminado, no la suma de ingredientes.
        SELECT
            ph.GroupingCodeDose,
            MAX(ph.DeliveryStatus) AS DeliveryStatus,
            MAX(ph.AppliedDose) AS AppliedDose,
            MAX(ph.QuantityReceivable) AS QuantityReceivable,
            ph.CodeSusceptibleMixingStation,
            MAX(ph.AppliedDateDose) AS AppliedDateDose,
            hcf.NUMINGRES
        INTO #PharmaDoseDetail
        FROM MedicalHistory.PharmaDose AS ph
        JOIN HCFARMEPC AS hcf ON hcf.CODCONCEC = ph.IDHCFARMEPC
        WHERE hcf.NUMINGRES = @AdmissionNumber
        GROUP BY 
            ph.GroupingCodeDose,
            ph.CodeSusceptibleMixingStation,
            hcf.NUMINGRES;

        CREATE NONCLUSTERED INDEX IX_PharmaDoseDetail ON #PharmaDoseDetail (GroupingCodeDose, NUMINGRES);

		----------------------------------------------------------------

        -- Hijos con dispensación
        -- Usar siempre los valores reales de cp2 (PharmaDoseDetail) para DeliveryStatus, AppliedDose, QuantityReceivable.
        -- Cada GroupingCodeDose tiene su propio estado de aplicación independiente.
        -- #PharmaDoseDetail ya está agrupado por GroupingCodeDose (una fila por aplicación).
        SELECT 
            cp2.GroupingCodeDose AS Id, psms.FullProductName, psms.ApplicationsNumber AS DoseNumber, cdd.ProductId, NULL AS Code, 
            CONCAT(cdd.Code, '-', cdd.Name) AS ProductCodName, cdd.QuantityDelivered, 
            cp2.AppliedDose, 
            cp2.QuantityReceivable, 
            CASE cp2.DeliveryStatus WHEN 0 THEN 'Sin Entregar' WHEN 1 THEN 'Entregado' WHEN 2 THEN 'Generado' WHEN 3 THEN 'Anulado' END AS DeliveryStatusName,
            CAST(cp2.DeliveryStatus AS INT) AS DeliveryStatus, 
            cdd.PharmaceuticalDispensingId AS Child, cdd.AdmissionNumber, cdd.BatchCode, 
            cdd.DispensingDate,
            CAST(IIF(cp2.DeliveryStatus IN (2, 3), 1, 0) AS BIT) AS Selected, 
            cdd.PackageId, cdd.PackagePersonalizedId, cdd.FunctionalUnitId, cdd.SurchargeApply,
            cdd.OrderedHealthProfessionalCode, cdd.OrderedHealthProfessionalThirdPartyId, cdd.HealthAdministratorId,
            cdd.OrderedProfessionalSpecialty, cdd.WarehouseId, cp2.AppliedDateDose, NULL AS Father, NULL AS PaidToEndCampaign, cdd.Source
        FROM #DispensingDose AS cdd
        -- Enlace por GroupingCodeDose; ProductSusceptibleMixingStation valida que el producto corresponde a la fórmula.
        JOIN #PharmaDoseDetail AS cp2 ON cdd.GroupingCodeDose = cp2.GroupingCodeDose AND cdd.AdmissionNumber = cp2.NUMINGRES
        JOIN MedicalHistory.ProductSusceptibleMixingStation AS psms ON psms.CodeSusceptibleMixingStation = cp2.CodeSusceptibleMixingStation 
            AND (psms.MainDrugCode = cdd.AtcCode OR psms.MainDrugCode = cdd.Code)

        UNION ALL

        -- Hijos sin dispensación (desde solicitud de mezcla)
        -- #PharmaDoseDetail ya está agrupado por GroupingCodeDose (una fila por aplicación).
        SELECT 
            cp2.GroupingCodeDose AS Id, psms.FullProductName, psms.ApplicationsNumber AS DoseNumber, cdd.ProductId, NULL AS Code, 
            CONCAT(cdd.Code, '-', cdd.Name) AS ProductCodName, cdd.QuantityDelivered, cp2.AppliedDose, 
            cp2.QuantityReceivable, 
            CASE cp2.DeliveryStatus WHEN 0 THEN 'Sin Entregar' WHEN 1 THEN 'Entregado' WHEN 2 THEN 'Generado' WHEN 3 THEN 'Anulado' ELSE 'Elaborado' END AS DeliveryStatusName,
            CAST(cp2.DeliveryStatus AS INT) AS DeliveryStatus, 
            cdd.RequestMixingStationId AS Child, cdd.AdmissionNumber, cdd.BatchCode, NULL AS DispensingDate, CAST(IIF(cp2.DeliveryStatus IN (2, 3), 1, 0) AS BIT) AS Selected,
            cdd.PackageId, cdd.PackagePersonalizedId, cdd.FunctionalUnitId, CAST(0 AS BIT) AS SurchargeApply, cdd.OrderedHealthProfessionalCode, cdd.OrderedHealthProfessionalThirdPartyId,
            cdd.HealthAdministratorId, cdd.OrderedProfessionalSpecialty, cdd.WarehouseId, cp2.AppliedDateDose, NULL AS Father,
            IIF(cdd.TypePayment = 2, 1, 0) AS PaidToEndCampaign, cdd.Source
        FROM #WithoutDispensation AS cdd
        JOIN #PharmaDoseDetail AS cp2 ON cdd.GroupingCodeDose = cp2.GroupingCodeDose
        JOIN MedicalHistory.ProductSusceptibleMixingStation AS psms ON psms.CodeSusceptibleMixingStation = cp2.CodeSusceptibleMixingStation;

    END TRY
    BEGIN CATCH
        THROW;
    END CATCH;

    -- Limpieza final
    IF OBJECT_ID('tempdb..#PharmaDose') IS NOT NULL DROP TABLE #PharmaDose;
    IF OBJECT_ID('tempdb..#PharmaDoseDetail') IS NOT NULL DROP TABLE #PharmaDoseDetail;
    IF OBJECT_ID('tempdb..#DispensingPharmaFallback') IS NOT NULL DROP TABLE #DispensingPharmaFallback;
    IF OBJECT_ID('tempdb..#DispensingGroupingDose') IS NOT NULL DROP TABLE #DispensingGroupingDose;
    IF OBJECT_ID('tempdb..#DispensingGroupingRpds') IS NOT NULL DROP TABLE #DispensingGroupingRpds;
    IF OBJECT_ID('tempdb..#DispensingGroupingPhysical') IS NOT NULL DROP TABLE #DispensingGroupingPhysical;
    IF OBJECT_ID('tempdb..#DispensingGroupingMetadata') IS NOT NULL DROP TABLE #DispensingGroupingMetadata;
    IF OBJECT_ID('tempdb..#DispensingPackageMetadata') IS NOT NULL DROP TABLE #DispensingPackageMetadata;
    IF OBJECT_ID('tempdb..#DispensingEntityGrouping') IS NOT NULL DROP TABLE #DispensingEntityGrouping;
    IF OBJECT_ID('tempdb..#DispensingBase') IS NOT NULL DROP TABLE #DispensingBase;
    IF OBJECT_ID('tempdb..#DispensingDose') IS NOT NULL DROP TABLE #DispensingDose;
    IF OBJECT_ID('tempdb..#WithoutDispensation') IS NOT NULL DROP TABLE #WithoutDispensation;
END;;
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que obtiene el listado de medicamentos procesados en la estación de mezclas farmacéuticas (NPT, mezclas oncológicas, etc.) asociados a un número de ingreso hospitalario, para ser presentados en la pestaña de control de cuentas hospitalario. Combina dos fuentes principales: productos que ya tuvieron una dispensación física registrada (cruzando dispensaciones farmacéuticas, lotes/seriales e inventario físico con los estados de paquetes de mezclas) y productos preparados en mezclas que aún no tienen dispensación formal en inventario. Integra datos del catálogo de productos (ATC, tipo de producto clase 5 correspondiente a mezclas), órdenes médicas de farmacia de historia clínica (HCFARMEPD/HCFARMEPC) y dosis programadas (PharmaDose) para resolver el código de agrupación de dosis cuando no existe en el estado de paquete. El resultado sirve para la facturación y control de cuentas de pacientes hospitalizados, identificando qué mezclas fueron entregadas, en qué cantidad, por qué profesional fueron ordenadas y bajo qué convenio o administradora de salud.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'PROCEDURE', @level1name = N'SP_GetProcessedMedicationItemsForBilling';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'PROCEDURE', @level1name = N'SP_GetProcessedMedicationItemsForBilling';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los productos transformados (mezclas farmacéuticas) asociados a una admisión hospitalaria, presentándolos como filas padre (resumen del producto terminado) o hijo (detalle por aplicación/dosis) para el control de cuentas y facturación.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_GetProcessedMedicationItemsForBilling';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Se debe proveer un número de admisión (@AdmissionNumber) existente en HCFARMEPC/PharmaceuticalDispensing.; Los productos deben estar clasificados con ProductType.Class = 5 (productos transformados/mezcla).; Para que aparezca un ítem dispensado: PharmaceuticalDispensingDetailBatchSerial.OutstandingQuantity > 0.; Debe existir un GroupingCodeDose, ya sea en MixingStation.RequestPackageDetailStatus o en MedicalHistory.PharmaDose vía HCFARMEPD/HCFARMEPC.; Para ítems sin dispensación: la campaña asociada debe estar en estado 6 (CampaignStatus=6), el paquete en estado rpds.Status=3, la solicitud rmd.Source=1 y el TypePayment del CareGroupMixLiquidation = 2.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_GetProcessedMedicationItemsForBilling';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se procesan productos con ProductType.Class = 5 (productos de mezcla/transformados).; La unión entre dispensación y aplicación clínica siempre se hace por GroupingCodeDose (+ AdmissionNumber).; En la vista padre, AppliedDose y QuantityReceivable se SUMAN, mientras en la vista hijo se toman directamente del MAX por GroupingCodeDose, evitando duplicar por ingredientes.; Los ítems sin dispensación efectiva (PharmaceuticalDispensingId IS NULL) solo aparecen si la campaña terminó (CampaignStatus=6) y el pago es diferido (TypePayment=2).; DeliveryStatusName se mapea estrictamente desde PharmaDose: 0=''Sin Entregar'', 1=''Entregado'', 2=''Generado'', 3=''Anulado''.; Todas las consultas usan WITH(NOLOCK) — lecturas sin bloqueo, tolerantes a lecturas sucias.; Las tablas temporales se eliminan al final aunque haya error (limpieza fuera del TRY/CATCH).', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_GetProcessedMedicationItemsForBilling';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Resultado tabular (cliente): Si @isChild = 0: devuelve filas padre por dispensación (NEWID como Id, ApplicationsNumber como DoseNumber, sumas de QuantityDelivered/AppliedDose/QuantityReceivable, PharmaceuticalDispensingId como Father) y termina con RETURN.; [RETURN_RESULT] Resultado tabular (cliente): Si @isChild = 1: devuelve filas hijo uniendo dispensados y no-dispensados con DeliveryStatus real de PharmaDose y etiquetas ''Sin Entregar''/''Entregado''/''Generado''/''Anulado'' para 0/1/2/3; Selected=1 cuando DeliveryStatus IN (2,3); SurchargeApply=0 y PaidToEndCampaign=1 si TypePayment=2 para no-dispensados.; [INSERT] tempdb.#DispensingDose: Materializa productos clase 5 con OutstandingQuantity>0 para la admisión, tomando GroupingCodeDose desde RequestPackageDetailStatus o, si no existe, desde PharmaDose vía HCFARMEPD priorizando registros con DeliveryStatus IN (0,1) y menor Id.; [INSERT] tempdb.#WithoutDispensation: Inserta paquetes sin dispensación efectiva (cdd.PharmaceuticalDispensingId IS NULL) cuando CampaignStatus=6, rpds.Status=3, rmd.Source=1 y TypePayment=2.; [INSERT] tempdb.#PharmaDose / #PharmaDoseDetail: Agrupa MedicalHistory.PharmaDose por GroupingCodeDose para la admisión, usando MAX en AppliedDose, QuantityReceivable y DeliveryStatus (una fila por aplicación).; [RAISERROR] Errores: TRY/CATCH con THROW: cualquier error en el bloque se relanza al llamador.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_GetProcessedMedicationItemsForBilling';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ISNULL(@isChild,0) = 0 → Construye y devuelve la vista PADRE: una fila por dispensación con sumas de cantidades y FullProductName/ApplicationsNumber desde ProductSusceptibleMixingStation, luego RETURN. else Construye la vista HIJO: une dispensados y no-dispensados con DeliveryStatus real de PharmaDose uno por GroupingCodeDose.; si GroupingCodeDose desde RequestPackageDetailStatus IS NULL → Usa fallback pre-materializado contra HCFARMEPD/HCFARMEPC/PharmaDose (donde EntityName=''HCFARMEPD'' y NUMINGRES coincide) priorizando DeliveryStatus IN (0,1) y menor Id.; si cp2.DeliveryStatus IN (2,3) → Marca la fila como Selected=1; si es 2 etiqueta ''Generado'' y si es 3 etiqueta ''Anulado''. else Selected=0; etiqueta ''Sin Entregar'' (0), ''Entregado'' (1) o ''Elaborado'' para estados no contemplados.; si TypePayment = 2 en hijos sin dispensación → Marca PaidToEndCampaign=1 (se paga al cierre de campaña). else PaidToEndCampaign=0.; si Match por psms.MainDrugCode = cdd.AtcCode OR psms.MainDrugCode = cdd.Code → Permite enlazar el producto susceptible de mezcla por código ATC o por código de producto (alternativos).', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_GetProcessedMedicationItemsForBilling';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_GetProcessedMedicationItemsForBilling';
GO
