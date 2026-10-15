-- =============================================
-- Author:		Cesar Collazos
-- CREATE date: 2026-02-16
-- Modified:	2026-02-26
-- Description:	Obtiene candidatos CUPS elegibles para auto-causación con batching.
--              Filtra por fecha >= FECULTLIQ (última liquidación del profesional).
--              ALCANCE: Solo OS en estado Registrado (sin factura), sin causación activa.
--              Incluye No Quirúrgicos y Quirúrgicos (ServiceOrderDetailSurgical).
--              Excluye ServiceClass 5 (Derecho Sala), 6 (Materiales Sutura), 7 (Instrumentación Qx).
-- =============================================
CREATE PROCEDURE [MedicalFees].[SP_GetCandidatesForAutoCausation]
	@BatchSize INT = 1000
AS
BEGIN
	SET NOCOUNT ON;

DECLARE @FirstDayOfMonth DATE = DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 1);

		;WITH Candidates AS (
		SELECT TOP(@BatchSize)
			CandidatePool.ServiceOrderDetailId
			,CandidatePool.ServiceOrderId
			,CandidatePool.CupsEntityId
			,CandidatePool.CareGroupId
			,CandidatePool.RateManualId
			,CandidatePool.RateManualType
			,CandidatePool.TotalSalesPrice
			,CandidatePool.InvoicedQuantity
			,CandidatePool.IPSServiceId
			,CandidatePool.PerformsHealthProfessionalCode
			,CandidatePool.ThirdPartyId
			,CandidatePool.Presentation
			,CandidatePool.ServiceDate
			,CandidatePool.ServiceType
			,CandidatePool.CostCenterId
			,CandidatePool.PerformsFunctionalUnitId
			,CandidatePool.ServiceOrderDetailSurgicalId
		FROM (
			-- ========== NO QUIRURGICOS ==========
			SELECT
				sod.Id AS ServiceOrderDetailId
				,sod.ServiceOrderId
				,sod.CUPSEntityId AS CupsEntityId
				,sod.CareGroupId
				,sod.RateManualId
				,sod.RateManualType
				,sod.TotalSalesPrice
				,sod.InvoicedQuantity
				,sod.IPSServiceId
				,sod.PerformsHealthProfessionalCode
				,sod.PerformsHealthProfessionalThirdPartyId AS ThirdPartyId
				,sod.Presentation
				,sod.ServiceDate
				,sod.ServiceType
				,sod.CostCenterId
				,sod.PerformsFunctionalUnitId
				,CAST(NULL AS INT) AS ServiceOrderDetailSurgicalId
			FROM Billing.ServiceOrderDetail sod
			LEFT JOIN dbo.INPROFSAL i ON i.CODPROSAL = sod.PerformsHealthProfessionalCode
			WHERE sod.CUPSEntityId IS NOT NULL
				AND sod.IsAnnulled = 0
				AND sod.IsDelete = 0
				AND sod.PerformsHealthProfessionalCode IS NOT NULL
				AND EXISTS (
					SELECT 1
					FROM Billing.ServiceOrderDetailDistribution sodd
					INNER JOIN Billing.RevenueControlDetail rcd ON rcd.Id = sodd.RevenueControlDetailId
					WHERE sodd.ServiceOrderDetailId = sod.Id
						AND rcd.Status = 1
				)
				AND EXISTS (
					SELECT 1
					FROM [Contract].IPSService ips
					WHERE ips.Id = sod.IPSServiceId
						AND (ips.ServiceClass IS NULL OR ips.ServiceClass NOT IN (5, 6, 7))
				)
				AND NOT EXISTS (
					SELECT 1
					FROM MedicalFees.MedicalFeesCausation mfc
					WHERE mfc.ServiceOrderDetailId = sod.Id
						AND mfc.ServiceOrderDetailSurgicalId IS NULL
						AND mfc.Status <> 4
				)
				AND sod.ServiceDate >= ISNULL(i.FECULTLIQ, @FirstDayOfMonth)

			UNION ALL

			-- ========== QUIRURGICOS ==========
			SELECT
				sod.Id AS ServiceOrderDetailId
				,sod.ServiceOrderId
				,sod.CUPSEntityId AS CupsEntityId
				,sod.CareGroupId
				,sod.RateManualId
				,sod.RateManualType
				,sods.TotalSalesPrice
				,sods.InvoicedQuantity
				,sods.IPSServiceId
				,sods.PerformsHealthProfessionalCode
				,sods.PerformsHealthProfessionalThirdPartyId AS ThirdPartyId
				,sod.Presentation
				,sod.ServiceDate
				,sod.ServiceType
				,sods.CostCenterId
				,sod.PerformsFunctionalUnitId
				,sods.Id AS ServiceOrderDetailSurgicalId
			FROM Billing.ServiceOrderDetailSurgical sods
			INNER JOIN Billing.ServiceOrderDetail sod ON sod.Id = sods.ServiceOrderDetailId
			LEFT JOIN dbo.INPROFSAL i ON i.CODPROSAL = sods.PerformsHealthProfessionalCode
			WHERE sod.CUPSEntityId IS NOT NULL
				AND sod.IsAnnulled = 0
				AND sod.IsDelete = 0
				AND sods.PerformsHealthProfessionalCode IS NOT NULL
				AND EXISTS (
					SELECT 1
					FROM Billing.ServiceOrderDetailDistribution sodd
					INNER JOIN Billing.RevenueControlDetail rcd ON rcd.Id = sodd.RevenueControlDetailId
					WHERE sodd.ServiceOrderDetailId = sod.Id
						AND rcd.Status = 1
				)
				AND EXISTS (
					SELECT 1
					FROM [Contract].IPSService ipsQx
					WHERE ipsQx.Id = sods.IPSServiceId
						AND (ipsQx.ServiceClass IS NULL OR ipsQx.ServiceClass NOT IN (5, 6, 7))
				)
				AND NOT EXISTS (
					SELECT 1
					FROM MedicalFees.MedicalFeesCausation mfc
					WHERE mfc.ServiceOrderDetailId = sod.Id
						AND mfc.ServiceOrderDetailSurgicalId = sods.Id
						AND mfc.Status <> 4
				)
				AND sod.ServiceDate >= ISNULL(i.FECULTLIQ, @FirstDayOfMonth)
		) AS CandidatePool
	)
	SELECT
		c.ServiceOrderDetailId
		,c.ServiceOrderId
		,so.AdmissionNumber
		,so.PatientCode
		,c.CupsEntityId
		,c.CareGroupId
		,c.RateManualId
		,c.RateManualType
		,c.TotalSalesPrice
		,c.InvoicedQuantity
		,c.IPSServiceId
		,CONCAT(ips.Code, ' - ', ips.Name) AS IPSServiceDescription
		,c.PerformsHealthProfessionalCode
		,c.ThirdPartyId
		,tp.Name AS ThirdPartyDescription
		,c.Presentation
		,c.ServiceDate
		,c.ServiceType
		,c.CostCenterId
		,c.PerformsFunctionalUnitId
		,c.ServiceOrderDetailSurgicalId
		,i.NOMMEDICO AS HealthProfessionalName
		,i.GENPROVEE AS SupplierThirdPartyId
	FROM Candidates c
	INNER JOIN Billing.ServiceOrder so ON so.Id = c.ServiceOrderId
	INNER JOIN [Contract].IPSService ips ON ips.Id = c.IPSServiceId
	LEFT JOIN dbo.INPROFSAL i ON i.CODPROSAL = c.PerformsHealthProfessionalCode
	LEFT JOIN Common.ThirdParty tp ON tp.Id = c.ThirdPartyId
	OPTION (RECOMPILE);
END
GO
