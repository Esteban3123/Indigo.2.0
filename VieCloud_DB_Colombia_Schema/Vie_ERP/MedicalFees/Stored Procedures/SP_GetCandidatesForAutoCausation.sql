-- =============================================
-- Author:		Cesar Collazos
-- CREATE date: 2026-02-16
-- Modified:	2026-02-18
-- Description:	Obtiene candidatos CUPS elegibles para auto-causación con batching.
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

	SELECT TOP(@BatchSize)
		*
	FROM (
		-- ========== NO QUIRÚRGICOS ==========
		SELECT
			sod.Id AS ServiceOrderDetailId
			,so.Id AS ServiceOrderId
			,so.AdmissionNumber
			,so.PatientCode
			,sod.CUPSEntityId AS CupsEntityId
			,sod.CareGroupId
			,sod.RateManualId
			,sod.RateManualType
			,sod.TotalSalesPrice
			,sod.InvoicedQuantity
			,sod.IPSServiceId
			,CONCAT(ips.Code, ' - ', ips.Name) AS IPSServiceDescription
			,sod.PerformsHealthProfessionalCode
			,sod.PerformsHealthProfessionalThirdPartyId AS ThirdPartyId
			,tp.Name AS ThirdPartyDescription
			,sod.Presentation
			,sod.ServiceDate
			,sod.ServiceType
			,sod.CostCenterId
			,sod.PerformsFunctionalUnitId
			,CAST(NULL AS INT) AS ServiceOrderDetailSurgicalId
			,i.NOMMEDICO AS HealthProfessionalName
			,i.GENPROVEE AS SupplierThirdPartyId
		FROM Billing.ServiceOrderDetail sod
		INNER JOIN Billing.ServiceOrder so ON so.Id = sod.ServiceOrderId
		INNER JOIN [Contract].IPSService ips ON ips.Id = sod.IPSServiceId
		LEFT JOIN INPROFSAL i ON i.CODPROSAL = sod.PerformsHealthProfessionalCode
		LEFT JOIN Common.ThirdParty tp ON tp.Id = sod.PerformsHealthProfessionalThirdPartyId
		WHERE sod.CUPSEntityId IS NOT NULL
			AND sod.IsAnnulled = 0
			AND sod.IsDelete = 0
			AND so.Status = 1
			AND sod.PerformsHealthProfessionalCode IS NOT NULL
			AND (ips.ServiceClass IS NULL OR ips.ServiceClass NOT IN (5, 6, 7))
			AND NOT EXISTS (
				SELECT 1 FROM MedicalFees.MedicalFeesCausation mfc
				WHERE mfc.ServiceOrderDetailId = sod.Id
					AND mfc.ServiceOrderDetailSurgicalId IS NULL
					AND mfc.Status <> 4
			)
			AND sod.ServiceDate >= ISNULL(i.FECULTLIQ, @FirstDayOfMonth)

		UNION ALL

		-- ========== QUIRÚRGICOS ==========
		SELECT
			sod.Id AS ServiceOrderDetailId
			,so.Id AS ServiceOrderId
			,so.AdmissionNumber
			,so.PatientCode
			,sod.CUPSEntityId AS CupsEntityId
			,sod.CareGroupId
			,sod.RateManualId
			,sod.RateManualType
			,sods.TotalSalesPrice
			,sods.InvoicedQuantity
			,sods.IPSServiceId
			,CONCAT(ipsQx.Code, ' - ', ipsQx.Name) AS IPSServiceDescription
			,sods.PerformsHealthProfessionalCode
			,sods.PerformsHealthProfessionalThirdPartyId AS ThirdPartyId
			,tp.Name AS ThirdPartyDescription
			,sod.Presentation
			,sod.ServiceDate
			,sod.ServiceType
			,sods.CostCenterId
			,sod.PerformsFunctionalUnitId
			,sods.Id AS ServiceOrderDetailSurgicalId
			,i.NOMMEDICO AS HealthProfessionalName
			,i.GENPROVEE AS SupplierThirdPartyId
		FROM Billing.ServiceOrderDetailSurgical sods
		INNER JOIN Billing.ServiceOrderDetail sod ON sod.Id = sods.ServiceOrderDetailId
		INNER JOIN Billing.ServiceOrder so ON so.Id = sod.ServiceOrderId
		INNER JOIN [Contract].IPSService ipsQx ON ipsQx.Id = sods.IPSServiceId
		LEFT JOIN INPROFSAL i ON i.CODPROSAL = sods.PerformsHealthProfessionalCode
		LEFT JOIN Common.ThirdParty tp ON tp.Id = sods.PerformsHealthProfessionalThirdPartyId
		WHERE sod.CUPSEntityId IS NOT NULL
			AND sod.IsAnnulled = 0
			AND sod.IsDelete = 0
			AND so.Status = 1
			AND sods.PerformsHealthProfessionalCode IS NOT NULL
			AND (ipsQx.ServiceClass IS NULL OR ipsQx.ServiceClass NOT IN (5, 6, 7))
			AND NOT EXISTS (
				SELECT 1 FROM MedicalFees.MedicalFeesCausation mfc
				WHERE mfc.ServiceOrderDetailId = sod.Id
					AND mfc.ServiceOrderDetailSurgicalId = sods.Id
					AND mfc.Status <> 4
			)
			AND sod.ServiceDate >= ISNULL(i.FECULTLIQ, @FirstDayOfMonth)
	) AS Candidates
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Obtiene un lote de ítems de órdenes de servicio (procedimientos quirúrgicos y no quirúrgicos) que son candidatos elegibles para el proceso de auto-causación de honorarios médicos. Filtra únicamente ítems con código CUPS asignado, que no estén anulados ni eliminados, que tengan una distribución financiera activa (control de ingresos en estado aprobado), que no posean ya una causación vigente, y que excluyen clases de servicio especiales como Derecho de Sala, Materiales de Sutura e Instrumentación Quirúrgica. Para cada candidato retorna datos de la orden de servicio, el número de admisión, el código del paciente, el servicio IPS con descripción CUPS, el profesional de salud ejecutante con su nombre y su tercero proveedor asociado, y el tercero pagador responsable; procesando como máximo el número de registros indicado por el parámetro de tamaño de lote (@BatchSize, por defecto 1000) para garantizar un procesamiento eficiente y controlado.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'PROCEDURE', @level1name = N'SP_GetCandidatesForAutoCausation';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'PROCEDURE', @level1name = N'SP_GetCandidatesForAutoCausation';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve un lote de ítems CUPS (no quirúrgicos y quirúrgicos) elegibles para causación automática de honorarios médicos, filtrando órdenes vigentes sin causación activa.', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'PROCEDURE', @level1name=N'SP_GetCandidatesForAutoCausation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Los ítems deben tener CUPSEntityId no nulo.; Los ítems no deben estar anulados ni eliminados (IsAnnulled=0, IsDelete=0).; La orden de servicio debe estar en estado 1 (Registrado/sin factura).; El ítem debe tener profesional de salud asignado (PerformsHealthProfessionalCode no nulo).; La fecha de servicio debe ser mayor o igual a la última fecha de liquidación del profesional (FECULTLIQ) o al primer día del mes actual si no existe.; No debe existir una causación activa para el ítem (Status distinto de 4).', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'PROCEDURE', @level1name=N'SP_GetCandidatesForAutoCausation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Nunca retorna ítems con ServiceClass 5, 6 o 7 (Derecho Sala, Materiales Sutura, Instrumentación Quirúrgica).; Nunca retorna ítems sin profesional de salud asignado.; Nunca retorna ítems de órdenes facturadas o en estado distinto de 1.; Nunca retorna ítems anulados o eliminados lógicamente.; Nunca retorna ítems que ya tengan una causación con Status distinto de 4 (entendido como 4 = anulada/cancelada).; El tamaño del resultado está limitado por @BatchSize (default 1000).; Para no quirúrgicos, la causación se identifica con ServiceOrderDetailSurgicalId NULL; para quirúrgicos, con el Id quirúrgico específico.', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'PROCEDURE', @level1name=N'SP_GetCandidatesForAutoCausation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Causación de honorarios médicos; Auto-causación; Orden de Servicio (OS); CUPS; Procedimientos quirúrgicos; Profesional de salud; Última fecha de liquidación; Derecho de sala; Materiales de sutura; Instrumentación quirúrgica; IPS; Lote de procesamiento (batching)', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'PROCEDURE', @level1name=N'SP_GetCandidatesForAutoCausation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Retorna hasta @BatchSize candidatos combinando ítems no quirúrgicos (ServiceOrderDetail) y quirúrgicos (ServiceOrderDetailSurgical) que cumplen los filtros de elegibilidad.', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'PROCEDURE', @level1name=N'SP_GetCandidatesForAutoCausation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ips.ServiceClass IS NULL OR ServiceClass NOT IN (5,6,7) → Se incluye el ítem como candidato (excluye Derecho Sala, Materiales Sutura, Instrumentación Qx).; si Existe MedicalFeesCausation con mismo ServiceOrderDetailId (y mismo ServiceOrderDetailSurgicalId para quirúrgicos, o NULL para no quirúrgicos) con Status <> 4 → Se excluye el ítem del resultado (ya tiene causación activa).; si INPROFSAL.FECULTLIQ existe para el profesional → Se filtra por ServiceDate >= FECULTLIQ. else Se filtra por ServiceDate >= primer día del mes actual.', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'PROCEDURE', @level1name=N'SP_GetCandidatesForAutoCausation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.ServiceOrderDetail; Billing.ServiceOrder; Contract.IPSService; Common.ThirdParty; MedicalFees.MedicalFeesCausation; Billing.ServiceOrderDetailSurgical; INPROFSAL', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'PROCEDURE', @level1name=N'SP_GetCandidatesForAutoCausation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MedicalFees', @level1type=N'PROCEDURE', @level1name=N'SP_GetCandidatesForAutoCausation';
-- GO
