
-- =============================================
-- Author:		Johan Sebastian Carranza Ramos
-- Create date: 15 Octubre de 2019
-- Description:	Procedimiento almacenado que lista los datos para el reporte de estadistico de ingresos
-- =============================================
CREATE PROCEDURE [Glosas].[SP_ReceptionObjectionExport]
	@GlosaObjectionsReceptionCId AS INT --Id de la recepcion de objeciones.
AS
BEGIN
	SET NOCOUNT ON

;WITH ObjectReception_CTE AS (

	SELECT
	gord.Id AS GordId,
	gord.GlosaObjectionsReceptionCId,
	gord.DocumentType,
	gid.Id AS GidId,
	gidqx.Id AS GidqxId,
	gid.InvoiceNumber, 
	gid.CostCenterCode, 
	ISNULL(gidqx.ServiceCode, gid.ServiceCode) ServiceCode, 
	ISNULL(gidqx.ServiceName, gid.ServiceName) ServiceName, 
	ISNULL(gidqx.ValueServiceManual, gid.ValueServiceManual) ValueServiceManual, 
	ISNULL(gidqx.UnitValue, gid.UnitValue) UnitValue, 
	ISNULL(gidqx.Ammount, gid.Ammount) Ammount, 
	ISNULL(gidqx.InvoicedValue, gid.InvoicedValue) AS InvoicedValue

	FROM Glosas.GlosaObjectionsReceptionD gord
	LEFT JOIN Glosas.GlosaInvoiceDetail gid WITH (NOLOCK) ON gid.ObjectionsReceptionDId = gord.Id
	LEFT JOIN  Glosas.GlosaInvoiceDetailQX gidqx WITH (NOLOCK) ON gidqx.InvoiceDetailId = gid.Id
	WHERE gord.GlosaObjectionsReceptionCId = @GlosaObjectionsReceptionCId AND ISNULL(gidqx.InvoicedValue, gid.InvoicedValue) > 0
),

GlosaMovementGlosa_CTE AS (

	SELECT 
		gmg.InvoiceDetailId,
		gmg.InvoiceDetailIdQX,
		gmg.CodeGlosaId,
		gmg.valuereiterated,
		gmg.JustificationGlosaText,
		gmg.JustificationReiterationText,
		gmg.MainGlosa,
		gmg.ValueGlosado,
		gmg.ValueAcceptedFirstInstance,
		gmg.ValueAcceptedSecondInstance,
		gmg.ResponsibleId,
		gmg.ResponsibleReiterationId,
		gmg.CodeGlosaEvaluation

	FROM Glosas.GlosaMovementGlosa gmg WITH (NOLOCK)
	WHERE gmg.InvoiceDetailId IS NOT NULL OR gmg.InvoiceDetailIdQX IS NOT NULL
		AND gmg.Id IS NOT NULL
),

ConceptGlosas_CTE AS (

	SELECT Id, Code, NameSpecific
	FROM Common.ConceptGlosas
),

Responsible_CTE AS (

	SELECT Id, Code, Name
	FROM Glosas.Responsible
)

	SELECT 
		gor.RadicatedConsecutive, 
		gor.RadicatedDate, 
		gor.DocumentDate AS FechaOficio,
		gor.DocumentNumber AS Oficio,
		c.Nit AS CustomerNit, 
		c.Name CustomerName, 
		gord.InvoiceNumber, 
		gord.CostCenterCode, 
		gord.ServiceCode, 
		gord.ServiceName, 
		gord.ValueServiceManual, 
		gord.UnitValue, 
		gord.Ammount, 
		gord.InvoicedValue,
		cg.Code AS CodeGlosa, 
		cg.namespecific AS ConceptGlosa, 
		conEva.Code AS CodeEvaluation,
		conEva.namespecific AS ConceptEvaluation,
		CASE 
			WHEN gmg.valuereiterated IS NOT NULL 
			AND gmg.valuereiterated > 0 THEN rr.Code + '-' + rr.Name
			ELSE Rg.Code + ' - ' + rg.Name 
		END AS Responsable,
		IIF(gord.DocumentType = 1, gmg.JustificationGlosaText, gmg.JustificationReiterationText) Justification,
		CASE 
			WHEN gmg.MainGlosa = 0 THEN 0 
			ELSE gmg.ValueGlosado
		END AS ObjetadoEAPB,
		ISNULL(gmg.ValueAcceptedFirstInstance, 0) AS ValueAcceptedFirstInstance,
		ISNULL(gmg.valuereiterated, 0) AS valuereiterated,
		ISNULL(gmg.ValueAcceptedSecondInstance, 0) AS ValueAcceptedSecondInstance, 
		ISNULL(gmg.ValueAcceptedFirstInstance,0) + ISNULL(gmg.ValueAcceptedSecondInstance,0) AS ValorAceptado,
		CASE
			WHEN gmg.valuereiterated IS NULL THEN 0 
			WHEN gmg.valuereiterated > 0 THEN gmg.valuereiterated - ISNULL(gmg.ValueAcceptedSecondInstance,0)
			ELSE gmg.ValueGlosado - ISNULL(gmg.ValueAcceptedFirstInstance,0) 
		END AS BalanceEAPB		
	FROM Glosas.GlosaObjectionsReceptionC gor 
	JOIN Common.Customer c ON c.Id = gor.CustomerId
	JOIN ObjectReception_CTE gord on gord.GlosaObjectionsReceptionCId = gor.Id
	LEFT JOIN GlosaMovementGlosa_CTE gmg ON gmg.InvoiceDetailId = gord.GidId OR gmg.InvoiceDetailIdQX = gord.GidqxId
	---------------------------------------------------------------------------------------------------------------
	LEFT JOIN ConceptGlosas_CTE cg on cg.Id = gmg.CodeGlosaId
	LEFT JOIN Responsible_CTE Rg on Rg.Id = gmg.ResponsibleId
	LEFT JOIN Responsible_CTE rr on rr.Id = gmg.ResponsibleReiterationId
	LEFT JOIN ConceptGlosas_CTE conEva on conEva.Code = gmg.CodeGlosaEvaluation
	WHERE gor.Id = @GlosaObjectionsReceptionCId
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Exporta el detalle completo de una recepción de objeciones de glosas para un pagador (EPS/aseguradora), identificada por su ID de recepción. Consolida, por cada ítem objetado, la información de la factura (número, centro de costo, servicio, valores facturados), los movimientos de glosa asociados (valor glosado por el pagador, justificaciones, valores aceptados en primera y segunda instancia, valor reiterado y balance pendiente ante la EAPB), el código y concepto de glosa según la clasificación interna, y el responsable asignado en cada instancia. Integra datos de servicios generales y quirúrgicos (GlosaInvoiceDetail y GlosaInvoiceDetailQX), priorizando la información quirúrgica cuando existe, junto con el radicado de respuesta y los datos del cliente pagador. Sirve como base para el reporte estadístico y de seguimiento del proceso de objeciones de glosas, permitiendo auditar el ciclo completo de cada glosa desde la recepción hasta la conciliación.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_ReceptionObjectionExport';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_ReceptionObjectionExport';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el dataset para el reporte/exportación de una recepción de objeciones de glosas, consolidando datos de la factura, ítems glosados (incluyendo quirúrgicos), conceptos, responsables y valores aceptados/reiterados por instancia.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_ReceptionObjectionExport';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir una recepción de objeciones (GlosaObjectionsReceptionC) con el Id recibido; La recepción debe tener un Customer asociado válido en Common.Customer; Los detalles deben tener InvoicedValue > 0 (o el de su contraparte QX) para ser incluidos', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_ReceptionObjectionExport';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Cuando existe detalle quirúrgico (QX) sus valores prevalecen sobre los del detalle de factura estándar para servicio, valores y cantidades; Solo se reportan ítems con valor facturado positivo; El valor aceptado total es la suma de aceptado en primera y segunda instancia; Los movimientos de glosa considerados deben estar asociados a un detalle de factura (estándar o QX); El responsable de reiteración prevalece sobre el original cuando hay valor reiterado positivo', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_ReceptionObjectionExport';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Glosa; Recepción de objeciones; Reiteración de glosa; Objeción EAPB; Detalle de factura; Servicios quirúrgicos (QX); Concepto de glosa; Responsable de glosa; Primera instancia; Segunda instancia; Justificación de glosa; Cliente/entidad pagadora (NIT); Radicado; Saldo EAPB', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_ReceptionObjectionExport';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve un conjunto de filas con datos de la recepción, factura, ítems, conceptos de glosa, evaluación, responsables, valores objetados, aceptados (1ra/2da instancia), reiterados y saldo EAPB', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_ReceptionObjectionExport';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ISNULL(gidqx.InvoicedValue, gid.InvoicedValue) > 0 → Se incluye el detalle (con preferencia a los datos del detalle quirúrgico QX sobre el detalle estándar) else Se excluye el ítem del reporte; si gmg.valuereiterated IS NOT NULL AND gmg.valuereiterated > 0 → El responsable mostrado es el de la reiteración (ResponsibleReiterationId) else El responsable mostrado es el original (ResponsibleId); si gord.DocumentType = 1 → La justificación mostrada es JustificationGlosaText (glosa inicial) else La justificación mostrada es JustificationReiterationText (reiteración); si gmg.MainGlosa = 0 → El valor objetado por EAPB se reporta como 0 else Se reporta el ValueGlosado real; si gmg.valuereiterated IS NULL → Saldo EAPB = 0 else Si valuereiterated > 0: saldo = valuereiterated - ValueAcceptedSecondInstance; en otro caso: saldo = ValueGlosado - ValueAcceptedFirstInstance', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_ReceptionObjectionExport';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Glosas.GlosaObjectionsReceptionD; Glosas.GlosaInvoiceDetail; Glosas.GlosaInvoiceDetailQX; Glosas.GlosaMovementGlosa; Common.ConceptGlosas; Glosas.Responsible; Glosas.GlosaObjectionsReceptionC; Common.Customer', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_ReceptionObjectionExport';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_ReceptionObjectionExport';
-- GO
