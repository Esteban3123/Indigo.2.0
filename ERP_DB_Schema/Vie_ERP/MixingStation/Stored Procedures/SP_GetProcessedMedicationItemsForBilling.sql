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
        SELECT
            pd.Id AS PharmaceuticalDispensingId,
            pdd.ProductId,
            ip.Code,
            ip.Name,
            a.Code AS AtcCode,
            SUM(pddb.OutstandingQuantity) AS QuantityDelivered,
            COALESCE(rpds.GroupingCodeDose, ph_fallback.GroupingCodeDose) AS GroupingCodeDose,
            pd.AdmissionNumber,
            bs.BatchCode,
            rpds.PackageId,
            pdd.FunctionalUnitId,
            pdd.SurchargeApply,
            pdd.OrderedHealthProfessionalCode,
            pdd.OrderedHealthProfessionalThirdPartyId,
            pdd.HealthAdministratorId,
            pdd.OrderedProfessionalSpecialty,
            pdd.WarehouseId
        INTO #DispensingDose
        FROM Inventory.PharmaceuticalDispensingDetailBatchSerial AS pddb
        JOIN Inventory.PharmaceuticalDispensingDetail AS pdd ON pdd.Id = pddb.PharmaceuticalDispensingDetailId
        JOIN Inventory.PharmaceuticalDispensing AS pd ON pd.Id = pdd.PharmaceuticalDispensingId
        JOIN Inventory.InventoryProduct AS ip ON pdd.ProductId = ip.Id
        JOIN Inventory.ATC AS a ON a.Id = ip.ATCId
        JOIN Inventory.ProductType AS pt ON ip.ProductTypeId = pt.Id AND pt.Class = 5
        JOIN Inventory.PhysicalInventory AS pi ON pddb.PhysicalInventoryId = pi.Id
        JOIN Inventory.BatchSerial AS bs ON pi.BatchSerialId = bs.Id
        LEFT JOIN MixingStation.RequestPackageDetailStatus AS rpds ON rpds.BatchCode = bs.BatchCode AND rpds.ProductId = pdd.ProductId
        -- Para NPT: PharmaDose tiene los ingredientes, no el producto terminado. GroupingCodeDose se obtiene
        -- desde HCFARMEPD (EntityId) -> CodeSusceptibleMixingStation -> PharmaDose.
        -- Con múltiples aplicaciones (ApplicationsNumber>1): priorizar pendientes (DeliveryStatus 0,1), luego por Id.
        OUTER APPLY (
            SELECT TOP 1 ph.GroupingCodeDose
            FROM HCFARMEPD AS hcfd
            JOIN HCFARMEPC AS hcfc ON hcfc.CODCONCEC = hcfd.CODCONCEC
            JOIN MedicalHistory.PharmaDose AS ph ON ph.CodeSusceptibleMixingStation = hcfd.CodeSusceptibleMixingStation
                AND ph.IDHCFARMEPC = hcfd.CODCONCEC
            WHERE hcfd.Id = pdd.EntityId
              AND pdd.EntityName = 'HCFARMEPD'
              AND hcfc.NUMINGRES = pd.AdmissionNumber
            ORDER BY CASE WHEN ph.DeliveryStatus IN (0, 1) THEN 0 ELSE 1 END, ph.Id ASC
        ) AS ph_fallback
        WHERE pddb.OutstandingQuantity > 0
          AND (pd.AdmissionNumber = @AdmissionNumber)
          AND (rpds.GroupingCodeDose IS NOT NULL OR ph_fallback.GroupingCodeDose IS NOT NULL)
        GROUP BY 
            pddb.Id,
            pdd.ProductId,
            pdd.FunctionalUnitId,
            pdd.SurchargeApply,
            pdd.OrderedHealthProfessionalCode,
            pdd.OrderedHealthProfessionalThirdPartyId,
            pdd.HealthAdministratorId,
            pdd.OrderedProfessionalSpecialty,
            pdd.WarehouseId,
            pd.Id,
            ip.Code,
            ip.Name,
            COALESCE(rpds.GroupingCodeDose, ph_fallback.GroupingCodeDose),
            pd.AdmissionNumber,
            bs.BatchCode,
            rpds.PackageId,
            a.Code;

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
            phd.WarehouseId,
            phd.FunctionalUnitId,
            phd.HealthAdministratorId,
            phd.OrderedHealthProfessionalCode,
            phd.OrderedHealthProfessionalThirdPartyId,
            phd.OrderedProfessionalSpecialty,
            phd.TypePayment,
            rmd.RequestMixingStationId
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
            phd.NUMINGRES,
            phd.WarehouseId,
            phd.FunctionalUnitId,
            phd.HealthAdministratorId,
            phd.OrderedHealthProfessionalCode,
            phd.OrderedHealthProfessionalThirdPartyId,
            phd.OrderedProfessionalSpecialty,
            phd.TypePayment,
            rmd.RequestMixingStationId;

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
			--SELECT *
			--FROM #DispensingDose AS cdd
			--JOIN #PharmaDose AS cp ON cdd.GroupingCodeDose = cp.GroupingCodeDose AND cdd.AtcCode = cp.ProductCode AND cdd.AdmissionNumber = cp.NUMINGRES
			--JOIN MedicalHistory.ProductSusceptibleMixingStation AS psms ON psms.CodeSusceptibleMixingStation = cp.CodeSusceptibleMixingStation AND psms.MainDrugCode = cp.ProductCode

			-- Padre con dispensación
			-- QuantityDelivered: suma de cantidades dispensadas (todas las dosis).
			-- AppliedDose: suma de cantidades realmente aplicadas (solo las que tienen DeliveryStatus=2).
			-- QuantityReceivable: suma de cantidades a cobrar (solo las aplicadas).
			-- #PharmaDose está agrupado por GroupingCodeDose (una fila por aplicación); aquí agrupamos por dispensación.
			SELECT
				NEWID() AS Id, psms.FullProductName, psms.ApplicationsNumber AS DoseNumber, cdd.ProductId, cdd.Code,
				CONCAT(cdd.Code, '-', cdd.Name) AS ProductCodName,
				-- COUNT(DISTINCT GroupingCodeDose): cada GroupingCodeDose representa 1 unidad/dosis del producto terminado.
				-- SUM(QuantityDelivered) inflaba el total cuando RequestPackageDetailStatus tiene múltiples filas por batch
				-- (unidades no dispensadas aún de la campaña con GroupingCodeDose=NULL producen fan-out via ph_fallback).
				COUNT(DISTINCT cp.GroupingCodeDose) AS QuantityDelivered,
				SUM(cp.AppliedDose) AS AppliedDose,
				SUM(cp.QuantityReceivable) AS QuantityReceivable, 
				NULL AS DeliveryStatusName, NULL AS DeliveryStatus, NULL AS Child, cdd.AdmissionNumber,
				NULL AS BatchCode, CAST(0 AS BIT) AS Selected, NULL AS PackageId, cdd.FunctionalUnitId, NULL AS SurchargeApply,
				NULL AS OrderedHealthProfessionalCode, NULL AS OrderedHealthProfessionalThirdPartyId, NULL AS HealthAdministratorId, 
				NULL AS OrderedProfessionalSpecialty, NULL AS WarehouseId, NULL AS AppliedDateDose, cdd.PharmaceuticalDispensingId AS Father, 
				NULL AS PaidToEndCampaign
			FROM #DispensingDose AS cdd
			-- Enlace por GroupingCodeDose; ProductSusceptibleMixingStation valida que el producto corresponde a la fórmula.
			JOIN #PharmaDose AS cp ON cdd.GroupingCodeDose = cp.GroupingCodeDose AND cdd.AdmissionNumber = cp.NUMINGRES
			JOIN MedicalHistory.ProductSusceptibleMixingStation AS psms ON psms.CodeSusceptibleMixingStation = cp.CodeSusceptibleMixingStation 
				AND (psms.MainDrugCode = cdd.AtcCode OR psms.MainDrugCode = cdd.Code)
			GROUP BY 
				psms.FullProductName, psms.ApplicationsNumber, cdd.ProductId, cdd.Code, cdd.Name, 
				cdd.AdmissionNumber, cdd.FunctionalUnitId, cdd.PharmaceuticalDispensingId

			--UNION ALL

			---- Padre sin dispensación (desde solicitud de mezcla)
			--SELECT 
			--	NEWID() AS Id, psms.FullProductName, psms.ApplicationsNumber AS DoseNumber, cdd.ProductId, cdd.Code, 
			--	CONCAT(cdd.Code, '-', cdd.Name) AS ProductCodName, SUM(cdd.QuantityDelivered) AS QuantityDelivered, SUM(cp.AppliedDose) AS AppliedDose,
			--	SUM(cp.QuantityReceivable) AS QuantityReceivable, NULL AS DeliveryStatusName, NULL AS DeliveryStatus, NULL AS Child, cdd.AdmissionNumber,
			--	NULL AS BatchCode, CAST(0 AS BIT) AS Selected, NULL AS PackageId, NULL AS FunctionalUnitId, NULL AS SurchargeApply,
			--	NULL AS OrderedHealthProfessionalCode, NULL AS OrderedHealthProfessionalThirdPartyId, NULL AS HealthAdministratorId, 
			--	NULL AS OrderedProfessionalSpecialty, NULL AS WarehouseId, NULL AS AppliedDateDose, cdd.RequestMixingStationId AS Father, 
			--	NULL AS PaidToEndCampaign
			--FROM #WithoutDispensation AS cdd
			--JOIN #PharmaDose AS cp ON cdd.GroupingCodeDose = cp.GroupingCodeDose
			--JOIN MedicalHistory.ProductSusceptibleMixingStation AS psms ON psms.CodeSusceptibleMixingStation = cp.CodeSusceptibleMixingStation
			--GROUP BY 
			--	cdd.ProductId, cdd.Code, cdd.Name, cdd.AdmissionNumber, psms.FullProductName, psms.ApplicationsNumber, 
			--	psms.CodeSusceptibleMixingStation, cdd.RequestMixingStationId

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
            CASE cp2.DeliveryStatus WHEN 0 THEN 'Sin Entregar' WHEN 1 THEN 'Entregado' WHEN 2 THEN 'Generado' END AS DeliveryStatusName,
            cp2.DeliveryStatus, 
            cdd.PharmaceuticalDispensingId AS Child, cdd.AdmissionNumber, cdd.BatchCode, 
            CAST(IIF(cp2.DeliveryStatus = 2, 1, 0) AS BIT) AS Selected, 
            cdd.PackageId, cdd.FunctionalUnitId, cdd.SurchargeApply, 
            cdd.OrderedHealthProfessionalCode, cdd.OrderedHealthProfessionalThirdPartyId, cdd.HealthAdministratorId, 
            cdd.OrderedProfessionalSpecialty, cdd.WarehouseId, cp2.AppliedDateDose, NULL AS Father, NULL AS PaidToEndCampaign
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
            cp2.QuantityReceivable, 'Elaborado' AS DeliveryStatusName, 3 AS DeliveryStatus, 
            cdd.RequestMixingStationId AS Child, cdd.AdmissionNumber, cdd.BatchCode, CAST(IIF(cp2.DeliveryStatus = 2, 1, 0) AS BIT) AS Selected, 
            cdd.PackageId, cdd.FunctionalUnitId, CAST(0 AS BIT) AS SurchargeApply, cdd.OrderedHealthProfessionalCode, cdd.OrderedHealthProfessionalThirdPartyId,
            cdd.HealthAdministratorId, cdd.OrderedProfessionalSpecialty, cdd.WarehouseId, cp2.AppliedDateDose, NULL AS Father, 
            IIF(cdd.TypePayment = 2, 1, 0) AS PaidToEndCampaign
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
    IF OBJECT_ID('tempdb..#DispensingDose') IS NOT NULL DROP TABLE #DispensingDose;
    IF OBJECT_ID('tempdb..#WithoutDispensation') IS NOT NULL DROP TABLE #WithoutDispensation;
END;