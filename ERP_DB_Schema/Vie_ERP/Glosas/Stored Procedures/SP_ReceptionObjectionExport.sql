
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