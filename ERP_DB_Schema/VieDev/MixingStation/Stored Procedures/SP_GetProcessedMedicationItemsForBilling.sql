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
        SELECT
            pd.Id AS PharmaceuticalDispensingId,
            pdd.ProductId,
            ip.Code,
            ip.Name,
            a.Code AS AtcCode,
            SUM(pddb.OutstandingQuantity) AS QuantityDelivered,
            rpds.GroupingCodeDose,
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
        FROM Inventory.PharmaceuticalDispensingDetailBatchSerial AS pddb WITH(NOLOCK)
        JOIN Inventory.PharmaceuticalDispensingDetail AS pdd WITH(NOLOCK) ON pdd.Id = pddb.PharmaceuticalDispensingDetailId
        JOIN Inventory.PharmaceuticalDispensing AS pd WITH(NOLOCK) ON pd.Id = pdd.PharmaceuticalDispensingId
        JOIN Inventory.InventoryProduct AS ip WITH(NOLOCK) ON pdd.ProductId = ip.Id
        JOIN Inventory.ATC AS a WITH(NOLOCK) ON a.Id = ip.ATCId
        JOIN Inventory.ProductType AS pt WITH(NOLOCK) ON ip.ProductTypeId = pt.Id AND pt.Class = 5
        JOIN Inventory.PhysicalInventory AS pi WITH(NOLOCK) ON pddb.PhysicalInventoryId = pi.Id
        JOIN Inventory.BatchSerial AS bs WITH(NOLOCK) ON pi.BatchSerialId = bs.Id
        JOIN MixingStation.RequestPackageDetailStatus AS rpds WITH(NOLOCK) ON rpds.BatchCode = bs.BatchCode
        WHERE pddb.OutstandingQuantity > 0
          AND (pd.AdmissionNumber = @AdmissionNumber)
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
            rpds.GroupingCodeDose,
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
        FROM MixingStation.RequestPackageDetailStatus AS rpds WITH(NOLOCK)
        JOIN MixingStation.RequestMixingStationDetail AS rmd WITH(NOLOCK) ON rmd.Id = rpds.RequestMixingStationDetailId
        JOIN Inventory.PhysicalInventory AS pi WITH(NOLOCK) ON pi.Id = rpds.PhysicalInventoryId
        JOIN Inventory.BatchSerial AS bs WITH(NOLOCK) ON pi.BatchSerialId = bs.Id
        JOIN MixingStation.CampaignDetail AS cd WITH(NOLOCK) ON cd.Id = rmd.CampaignDetailId
        JOIN Inventory.InventoryProduct AS ip WITH(NOLOCK) ON rpds.ProductId = ip.Id
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
            FROM MedicalHistory.PharmaDose AS phd_sub WITH(NOLOCK)
            JOIN HCFARMEPC AS fpc WITH(NOLOCK) ON fpc.CODCONCEC = phd_sub.IDHCFARMEPC
            JOIN ADINGRESO AS a WITH(NOLOCK) ON a.NUMINGRES = fpc.NUMINGRES
            JOIN Inventory.Warehouse AS w WITH(NOLOCK) ON w.Code = fpc.CODBODEGA
            JOIN Payroll.FunctionalUnit AS fu WITH(NOLOCK) ON fu.Code = a.UFUCODIGO
            JOIN Contract.CareGroup AS cg WITH(NOLOCK) ON cg.Id = a.GENCAREGROUP
            JOIN Contract.Contract AS c WITH(NOLOCK) ON c.Id = cg.ContractId
            JOIN INPROFSAL AS i WITH(NOLOCK) ON i.CODPROSAL = fpc.CODPROSAL
            JOIN Common.ThirdParty AS t WITH(NOLOCK) ON t.Nit = i.CODIGONIT
            LEFT JOIN Contract.CareGroupMixLiquidation AS cgm WITH(NOLOCK) ON cgm.CareGroupId = cg.Id
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
			-- Materializar PharmaDose
			SELECT
				ph.CodeSusceptibleMixingStation,
				ph.GroupingCodeDose,
				ph.AppliedDose,
				ph.QuantityReceivable,
				ph.ProductCode,
				hcf.NUMINGRES
			INTO #PharmaDose
			FROM MedicalHistory.PharmaDose AS ph WITH(NOLOCK)
			JOIN HCFARMEPC AS hcf WITH(NOLOCK) ON hcf.CODCONCEC = ph.IDHCFARMEPC
			WHERE hcf.NUMINGRES = @AdmissionNumber
			GROUP BY 
				ph.GroupingCodeDose, ph.CodeSusceptibleMixingStation, ph.AppliedDose, 
				ph.QuantityReceivable, ph.ProductCode, hcf.NUMINGRES;

			CREATE NONCLUSTERED INDEX IX_PharmaDose ON #PharmaDose (GroupingCodeDose, ProductCode, NUMINGRES);

			----------------------------------------------------------------
			--SELECT *
			--FROM #DispensingDose AS cdd
			--JOIN #PharmaDose AS cp ON cdd.GroupingCodeDose = cp.GroupingCodeDose AND cdd.AtcCode = cp.ProductCode AND cdd.AdmissionNumber = cp.NUMINGRES
			--JOIN MedicalHistory.ProductSusceptibleMixingStation AS psms WITH(NOLOCK) ON psms.CodeSusceptibleMixingStation = cp.CodeSusceptibleMixingStation AND psms.MainDrugCode = cp.ProductCode

			-- Padre con dispensación
			SELECT 
				NEWID() AS Id, psms.FullProductName, psms.ApplicationsNumber AS DoseNumber, cdd.ProductId, cdd.Code, 
				CONCAT(cdd.Code, '-', cdd.Name) AS ProductCodName, SUM(cdd.QuantityDelivered) AS QuantityDelivered, SUM(cp.AppliedDose) AS AppliedDose,
				SUM(cp.QuantityReceivable) AS QuantityReceivable, NULL AS DeliveryStatusName, NULL AS DeliveryStatus, NULL AS Child, cdd.AdmissionNumber,
				NULL AS BatchCode, CAST(0 AS BIT) AS Selected, NULL AS PackageId, cdd.FunctionalUnitId, NULL AS SurchargeApply,
				NULL AS OrderedHealthProfessionalCode, NULL AS OrderedHealthProfessionalThirdPartyId, NULL AS HealthAdministratorId, 
				NULL AS OrderedProfessionalSpecialty, NULL AS WarehouseId, NULL AS AppliedDateDose, cdd.PharmaceuticalDispensingId AS Father, 
				NULL AS PaidToEndCampaign
			FROM #DispensingDose AS cdd
			JOIN #PharmaDose AS cp ON cdd.GroupingCodeDose = cp.GroupingCodeDose AND cdd.AtcCode = cp.ProductCode AND cdd.AdmissionNumber = cp.NUMINGRES
			JOIN MedicalHistory.ProductSusceptibleMixingStation AS psms WITH(NOLOCK) ON psms.CodeSusceptibleMixingStation = cp.CodeSusceptibleMixingStation AND psms.MainDrugCode = cp.ProductCode
			GROUP BY 
				psms.CodeSusceptibleMixingStation, psms.FullProductName, psms.ApplicationsNumber, cdd.ProductId, cdd.Code, cdd.Name, 
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
			--JOIN MedicalHistory.ProductSusceptibleMixingStation AS psms WITH(NOLOCK) ON psms.CodeSusceptibleMixingStation = cp.CodeSusceptibleMixingStation
			--GROUP BY 
			--	cdd.ProductId, cdd.Code, cdd.Name, cdd.AdmissionNumber, psms.FullProductName, psms.ApplicationsNumber, 
			--	psms.CodeSusceptibleMixingStation, cdd.RequestMixingStationId

			RETURN
		END
		
		--------------------------------------------------------------------- HIJOS -----------------------------------------------------------------------------------------

		-- Materializar PharmaDoseDetail: Contiene los datos de detalle de PharmaDose.
        SELECT
            ph.GroupingCodeDose,
            ph.DeliveryStatus,
            ph.AppliedDose,
            ph.QuantityReceivable,
            ph.CodeSusceptibleMixingStation,
            ph.AppliedDateDose,
            ph.ProductCode,
            hcf.NUMINGRES
        INTO #PharmaDoseDetail
        FROM MedicalHistory.PharmaDose AS ph WITH(NOLOCK)
        JOIN HCFARMEPC AS hcf WITH(NOLOCK) ON hcf.CODCONCEC = ph.IDHCFARMEPC
        WHERE hcf.NUMINGRES = @AdmissionNumber
        GROUP BY 
            ph.GroupingCodeDose,
            ph.DeliveryStatus,
            ph.AppliedDose,
            ph.QuantityReceivable,
            ph.CodeSusceptibleMixingStation,
            ph.AppliedDateDose,
            ph.ProductCode,
            hcf.NUMINGRES;

        CREATE NONCLUSTERED INDEX IX_PharmaDoseDetail ON #PharmaDoseDetail (GroupingCodeDose, ProductCode, NUMINGRES);

		----------------------------------------------------------------

        -- Hijos con dispensación
        SELECT 
            cp2.GroupingCodeDose AS Id, psms.FullProductName, psms.ApplicationsNumber AS DoseNumber, cdd.ProductId, NULL AS Code, 
            CONCAT(cdd.Code, '-', cdd.Name) AS ProductCodName, cdd.QuantityDelivered, SUM(cp2.AppliedDose) AS AppliedDose, 
            SUM(cp2.QuantityReceivable) AS QuantityReceivable, 
            CASE cp2.DeliveryStatus WHEN 0 THEN 'Sin Entregar' WHEN 1 THEN 'Entregado' WHEN 2 THEN 'Generado' END AS DeliveryStatusName,
            cp2.DeliveryStatus, cdd.PharmaceuticalDispensingId AS Child, cdd.AdmissionNumber, cdd.BatchCode, 
            CAST(IIF(cp2.DeliveryStatus = 2, 1, 0) AS BIT) AS Selected, cdd.PackageId, cdd.FunctionalUnitId, cdd.SurchargeApply, 
            cdd.OrderedHealthProfessionalCode, cdd.OrderedHealthProfessionalThirdPartyId, cdd.HealthAdministratorId, 
            cdd.OrderedProfessionalSpecialty, cdd.WarehouseId, cp2.AppliedDateDose, NULL AS Father, NULL AS PaidToEndCampaign
        FROM #DispensingDose AS cdd
        JOIN #PharmaDoseDetail AS cp2 ON cdd.GroupingCodeDose = cp2.GroupingCodeDose AND cdd.AtcCode = cp2.ProductCode AND cdd.AdmissionNumber = cp2.NUMINGRES
        JOIN MedicalHistory.ProductSusceptibleMixingStation AS psms WITH(NOLOCK) ON psms.CodeSusceptibleMixingStation = cp2.CodeSusceptibleMixingStation AND psms.MainDrugCode = cp2.ProductCode
        GROUP BY 
            cp2.GroupingCodeDose, psms.FullProductName, psms.ApplicationsNumber, cdd.ProductId, cdd.Code, cdd.Name, cdd.QuantityDelivered, 
            cp2.DeliveryStatus, cdd.PharmaceuticalDispensingId, cdd.AdmissionNumber, cdd.BatchCode, cdd.PackageId, cdd.FunctionalUnitId, 
            cdd.SurchargeApply, cdd.OrderedHealthProfessionalCode, cdd.OrderedHealthProfessionalThirdPartyId, cdd.HealthAdministratorId, 
            cdd.OrderedProfessionalSpecialty, cdd.WarehouseId, cp2.AppliedDateDose

        UNION ALL

        -- Hijos sin dispensación (desde solicitud de mezcla)
        SELECT 
            cp2.GroupingCodeDose AS Id, psms.FullProductName, psms.ApplicationsNumber AS DoseNumber, cdd.ProductId, NULL AS Code, 
            CONCAT(cdd.Code, '-', cdd.Name) AS ProductCodName, cdd.QuantityDelivered, SUM(cp2.AppliedDose) AS AppliedDose, 
            SUM(cp2.QuantityReceivable) AS QuantityReceivable, 'Elaborado' AS DeliveryStatusName, 3 AS DeliveryStatus, 
            cdd.RequestMixingStationId AS Child, cdd.AdmissionNumber, cdd.BatchCode, CAST(IIF(cp2.DeliveryStatus = 2, 1, 0) AS BIT) AS Selected, 
            cdd.PackageId, cdd.FunctionalUnitId, CAST(0 AS BIT) AS SurchargeApply, cdd.OrderedHealthProfessionalCode, cdd.OrderedHealthProfessionalThirdPartyId,
            cdd.HealthAdministratorId, cdd.OrderedProfessionalSpecialty, cdd.WarehouseId, cp2.AppliedDateDose, NULL AS Father, 
            IIF(cdd.TypePayment = 2, 1, 0) AS PaidToEndCampaign
        FROM #WithoutDispensation AS cdd
        JOIN #PharmaDoseDetail AS cp2 ON cdd.GroupingCodeDose = cp2.GroupingCodeDose
        JOIN MedicalHistory.ProductSusceptibleMixingStation AS psms WITH(NOLOCK) ON psms.CodeSusceptibleMixingStation = cp2.CodeSusceptibleMixingStation
        GROUP BY 
            cdd.ProductId, cdd.Code, cdd.Name, cdd.AdmissionNumber, psms.FullProductName, psms.ApplicationsNumber, cdd.QuantityDelivered,
            cp2.DeliveryStatus, cdd.BatchCode, cp2.GroupingCodeDose, cdd.PackageId, cdd.WarehouseId, cdd.FunctionalUnitId, 
            cp2.AppliedDateDose, cdd.HealthAdministratorId, cdd.OrderedHealthProfessionalCode, cdd.OrderedHealthProfessionalThirdPartyId, 
            cdd.OrderedProfessionalSpecialty, cdd.TypePayment, cdd.RequestMixingStationId;

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