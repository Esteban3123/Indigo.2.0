
-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-11-06
-- Description:	Procedimiento para el reporte de listado de recepción de objeciones
-- =============================================
CREATE PROCEDURE [Glosas].[SP_ReportListObjectionsReception]
	@xmlCriterias AS XML
AS
BEGIN
	SET NOCOUNT ON;
	SET DATEFORMAT DMY
	DECLARE -- CRITERIOS --
			@DateStart DATE,
			@DateEnd DATE

	BEGIN TRY
		
		/********************************** CRITERIOS Y FILTROS **********************************/

		--Se obtienen los datos de los criterios
		SELECT 
			@DateStart = t.x.value('DateStart[1]','date'),
			@DateEnd = t.x.value('DateEnd[1]','date')
		FROM @xmlCriterias.nodes('/Data') t(x);

		/********************************** OBTENCION DE DATOS **********************************/

		WITH cte_Portfolio as (
								SELECT pnara.AccountReceivableId, MIN(pn.Code) Code, MIN(pn.NoteDate) NoteDate
								FROM Portfolio.PortfolioNote pn WITH (NOLOCK)
								JOIN Portfolio.PortfolioNoteAccountReceivableAdvance pnara WITH (NOLOCK) ON pn.Id = pnara.PortfolioNoteId
								WHERE pn.Status = 2 AND pn.Observations IN
								(
									'Aceptacion IPS glosa Subsanable - Glosa',
									'Aceptacion IPS glosa Subsanable - Reiteracion',
									'Aceptacion IPS glosa conciliacion'
								) 
								GROUP BY pnara.AccountReceivableId	),

			cte_ConceptGlosas as (	SELECT cg.Code,cg.NameSpecific,cg.Id
									from Common.ConceptGlosas cg WITH(NOLOCK)),

			cte_Responsible as (SELECT r.Code, r.Name,r.Id
								FROM Glosas.Responsible r WITH(NOLOCK)),
								
			cte_GlosaMov as	  (
								SELECT		gmg.valuereiterated,
											gmg.MainGlosa,
											gmg.ValueGlosado,
											gmg.JustificationGlosaText,
											gmg.JustificationReiterationText,
											gmg.ValueAcceptedFirstInstance,
											gmg.ValueAcceptedSecondInstance,
											gmg.Id ,
											gmg.CodeGlosaId,
											gmg.IdGlosaEvaluation,
											gmg.ResponsibleId,
											gmg.ResponsibleReiterationId,
											gmg.InvoiceDetailIdQX,
											gmg.InvoiceDetailId
								FROM  Glosas.GlosaMovementGlosa gmg WITH (NOLOCK)								
								),

			cte_Glosa as (			SELECT	gid.Id GlosaInvoiceDetailId,
											gid.ObjectionsReceptionDId,
											gid.InvoiceNumber,
											gid.CostCenterCode,
											ISNULL(gid.ServiceCode, gid.ServiceCode) ServiceCode,
											ISNULL(gid.ServiceName, gid.ServiceName) ServiceName,
											ISNULL(gid.ValueServiceManual, gid.ValueServiceManual) ValueServiceManual,
											ISNULL(gid.UnitValue, gid.UnitValue) UnitValue,
											ISNULL(gid.Ammount, gid.Ammount) Ammount,
											ISNULL(gid.InvoicedValue, gid.InvoicedValue) InvoicedValue,
											gmg.valuereiterated,
											gmg.MainGlosa,
											gmg.ValueGlosado,
											gmg.JustificationGlosaText,
											gmg.JustificationReiterationText,
											gmg.ValueAcceptedFirstInstance,
											gmg.ValueAcceptedSecondInstance,
											gmg.Id GlosaMovementGlosaId,
											cg.Code CodeGlosa,
											cg.NameSpecific ConceptGlosa,
											cge.Code CodeEvaluation,
											cge.namespecific  ConceptEvaluation,
											CONCAT(r.Code,' - ',r.Name) rCodeName,
											CONCAT(rr.Code,' - ',rr.Name) rrCodeName
									FROM Glosas.GlosaInvoiceDetail gid WITH (NOLOCK)
																									
									JOIN cte_GlosaMov gmg WITH (NOLOCK) ON gid.Id = gmg.InvoiceDetailId
									-------------------------------------------------------------------------------------------------------------
									LEFT JOIN cte_ConceptGlosas cg WITH (NOLOCK) ON gmg.CodeGlosaId = cg.Id
									LEFT JOIN cte_ConceptGlosas cge WITH (NOLOCK) ON gmg.IdGlosaEvaluation = cge.Id
									LEFT JOIN cte_Responsible r WITH (NOLOCK) ON gmg.ResponsibleId = r.Id
									LEFT JOIN cte_Responsible rr WITH (NOLOCK) ON gmg.ResponsibleReiterationId = rr.Id
									WHERE gmg.InvoiceDetailIdQX IS NULL -- Solo servicios No QX
									
									UNION
									
									SELECT	gid.Id GlosaInvoiceDetailId,
											gid.ObjectionsReceptionDId,
											gid.InvoiceNumber,
											gid.CostCenterCode,
											ISNULL(gidqx.ServiceCode, gid.ServiceCode) ServiceCode,
											ISNULL(gidqx.ServiceName, gid.ServiceName) ServiceName,
											ISNULL(gidqx.ValueServiceManual, gid.ValueServiceManual) ValueServiceManual,
											ISNULL(gidqx.UnitValue, gid.UnitValue) UnitValue,
											ISNULL(gidqx.Ammount, gid.Ammount) Ammount,
											ISNULL(gidqx.InvoicedValue, gid.InvoicedValue) InvoicedValue,
											gmg.valuereiterated,
											gmg.MainGlosa,
											gmg.ValueGlosado,
											gmg.JustificationGlosaText,
											gmg.JustificationReiterationText,
											gmg.ValueAcceptedFirstInstance,
											gmg.ValueAcceptedSecondInstance,
											gmg.Id GlosaMovementGlosaId,
											cg.Code CodeGlosa,
											cg.NameSpecific ConceptGlosa,
											cge.Code CodeEvaluation,
											cge.namespecific  ConceptEvaluation,
											CONCAT(r.Code,' - ',r.Name) rCodeName,
											CONCAT(rr.Code,' - ',rr.Name) rrCodeName
									FROM Glosas.GlosaInvoiceDetail gid WITH (NOLOCK)
									JOIN Glosas.GlosaInvoiceDetailQX gidqx WITH (NOLOCK) ON gid.Id = gidqx.InvoiceDetailId
									JOIN cte_GlosaMov gmg WITH (NOLOCK) ON gidqx.Id = gmg.InvoiceDetailIdQX 
									-------------------------------------------------------------------------------------------------------------
									LEFT JOIN cte_ConceptGlosas cg WITH (NOLOCK) ON gmg.CodeGlosaId = cg.Id
									LEFT JOIN cte_ConceptGlosas cge WITH (NOLOCK) ON gmg.IdGlosaEvaluation = cge.Id
									LEFT JOIN cte_Responsible r WITH (NOLOCK) ON gmg.ResponsibleId = r.Id
									LEFT JOIN cte_Responsible rr WITH (NOLOCK) ON gmg.ResponsibleReiterationId = rr.Id
									WHERE gmg.InvoiceDetailIdQX IS NOT NULL --Solo servicios QX
								)

		
		SELECT
			gor.RadicatedConsecutive,
			gor.RadicatedDate,
			gor.DocumentDate,
			gor.DocumentNumber,
			CASE gor.State
				WHEN '1' THEN 'Registrado'
				WHEN '2' THEN 'Confirmado'
				WHEN '3' THEN 'Anulado'
				ELSE 'N/A'
			END StatusName,
			c.Nit CustomerNit,
			c.Name CustomerName,
			ar.InvoiceNumber,
			ar.AccountReceivableDate,
			gid.CostCenterCode, 
			gr.RegimenName,
			ar.Value,
			ar.Balance,
			gid.ServiceCode, 
			gid.ServiceName, 
			gid.ValueServiceManual, 
			gid.UnitValue, 
			gid.Ammount, 
			gid.InvoicedValue,
			gid.CodeGlosa,
			gid.ConceptGlosa,
			gid.CodeEvaluation,
			gid.ConceptEvaluation,
			CASE	WHEN gid.valuereiterated IS NULL THEN IIF(IIF(gid.MainGlosa = 0,0,gid.ValueGlosado) > 0, gid.rCodeName, '') 
					WHEN gid.valuereiterated > 0 THEN gid.rrCodeName
					ELSE gid.rCodeName
			END AS Responsable,
			IIF(gord.DocumentType = 1, gid.JustificationGlosaText, gid.JustificationReiterationText) Justification,
			CASE WHEN gid.MainGlosa = 0 THEN 0 ELSE gid.ValueGlosado END AS ValueGlosado,
			ISNULL(gid.ValueReiterated, 0) AS ValueReiterated,
			ISNULL(gid.ValueAcceptedFirstInstance,0) + ISNULL(gid.ValueAcceptedSecondInstance,0) AS ValueAcceptedIPS,
			CASE	WHEN gid.valuereiterated IS NULL THEN 0 
					WHEN gid.valuereiterated > 0 THEN gid.valuereiterated - ISNULL(gid.ValueAcceptedSecondInstance,0)
					ELSE gid.ValueGlosado - ISNULL(gid.ValueAcceptedFirstInstance,0) 
			END AS BalanceEAPB,
			pn.Code NoteCode,
			pn.NoteDate
		FROM Glosas.GlosaObjectionsReceptionC gor WITH (NOLOCK)
		JOIN Common.Customer c WITH (NOLOCK) ON gor.CustomerId = c.Id
		JOIN Glosas.GlosaObjectionsReceptionD gord WITH (NOLOCK) ON gor.Id = gord.GlosaObjectionsReceptionCId
		JOIN Portfolio.AccountReceivable ar WITH (NOLOCK) ON gord.InvoiceNumber = ar.InvoiceNumber AND ar.AccountReceivableType = 2
		LEFT JOIN cte_Glosa gid WITH(NOLOCK) on gord.Id = gid.ObjectionsReceptionDId OR gord.InvoiceNumber = gid.InvoiceNumber																							   
		LEFT JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON ar.AccountWithoutRadicateId = ma.Id
		LEFT JOIN Portfolio.GetRegimes() gr ON ma.Number = gr.AccountNumber
		LEFT JOIN cte_Portfolio pn ON ar.Id = pn.AccountReceivableId
		WHERE CAST(gor.RadicatedDate AS DATE) BETWEEN @DateStart AND @DateEnd
			AND gid.GlosaMovementGlosaId IS NOT NULL 
			AND gid.InvoicedValue > 0

		OPTION (RECOMPILE)
	END TRY
	BEGIN CATCH	
		SELECT '999' CodeResult, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)) MessageResult
	END CATCH
END
-- QA
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el reporte de listado de recepción de objeciones (glosas) dentro de un rango de fechas, integrando información de facturas glosadas, movimientos de glosa (valores glosados, aceptados en primera y segunda instancia, reiterados), conceptos de glosa y evaluación, responsables de la glosa y de la reiteración, y notas de cartera asociadas a anticipos de cuentas por cobrar. Consolida tanto servicios ordinarios como servicios quirúrgicos (QX), presentando por cada ítem glosado el número de factura, centro de costo, servicio, valores facturados y glosados, justificaciones y estado de cada glosa en el ciclo de auditoría. Se utiliza por el área de cartera y auditoría para hacer seguimiento y control del proceso de recepción y gestión de objeciones ante entidades aseguradoras (EAPB).', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_ReportListObjectionsReception';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_ReportListObjectionsReception';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un reporte consolidado de recepción de objeciones de glosas en un rango de fechas, mostrando radicados, facturas, ítems glosados, valores aceptados/reiterados, responsables, saldos a cargo de la EAPB y notas de cartera asociadas.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListObjectionsReception';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de criterios debe contener los nodos /Data/DateStart y /Data/DateEnd con fechas válidas en formato DMY.; Deben existir cuentas por cobrar con AccountReceivableType = 2 ligadas a las facturas objetadas.; Las recepciones de objeciones deben tener RadicatedDate dentro del rango solicitado.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListObjectionsReception';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan filas cuyo InvoicedValue sea mayor a cero y que tengan un movimiento de glosa asociado (GlosaMovementGlosaId no nulo).; Las notas de cartera consideradas son únicamente las confirmadas (Status=2) y con observaciones de aceptación IPS (glosa subsanable, reiteración o conciliación).; Solo se consideran cuentas por cobrar con AccountReceivableType = 2.; El ValueAcceptedIPS siempre suma aceptaciones de primera y segunda instancia tratando NULL como 0.; Para cada AccountReceivableId solo se toma una nota de cartera (la de menor Code/NoteDate).; El reporte distingue ítems QX y No QX evitando duplicarlos mediante UNION según el indicador InvoiceDetailIdQX.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListObjectionsReception';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Glosa; Recepción de objeciones; Reiteración de glosa; Conciliación de glosa; Aceptación IPS; EAPB; Factura; Cuenta por cobrar; Nota de cartera; Servicios quirúrgicos (QX); Régimen; Cliente/Entidad pagadora; Responsable de glosa; Justificación de glosa', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListObjectionsReception';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Resultset: Devuelve el listado de recepción de objeciones filtrado por gor.RadicatedDate BETWEEN @DateStart AND @DateEnd, gid.GlosaMovementGlosaId IS NOT NULL y gid.InvoicedValue > 0.; [RETURN_RESULT] Resultset: Si ocurre cualquier error en TRY, retorna una fila con CodeResult=''999'' y MessageResult con ERROR_MESSAGE() y ERROR_LINE().', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListObjectionsReception';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si gor.State = ''1'' / ''2'' / ''3'' → Mapea el estado a ''Registrado'', ''Confirmado'' o ''Anulado'' respectivamente. else Cualquier otro valor se reporta como ''N/A''.; si gmg.InvoiceDetailIdQX IS NULL en el detalle de glosa → Toma el ítem desde Glosas.GlosaInvoiceDetail (servicios No QX). else Cuando InvoiceDetailIdQX IS NOT NULL, hace JOIN con Glosas.GlosaInvoiceDetailQX para tomar datos de servicios quirúrgicos (QX).; si gid.valuereiterated IS NULL → Responsable = rCodeName solo si MainGlosa<>0 y ValueGlosado>0; en caso contrario cadena vacía. else Si valuereiterated > 0 usa rrCodeName (responsable de reiteración); si no, usa rCodeName.; si gord.DocumentType = 1 → Justification = JustificationGlosaText. else Justification = JustificationReiterationText.; si gid.MainGlosa = 0 → ValueGlosado se reporta como 0. else Se reporta el ValueGlosado real del movimiento.; si gid.valuereiterated IS NULL → BalanceEAPB = 0. else Si valuereiterated > 0, BalanceEAPB = valuereiterated - ValueAcceptedSecondInstance; si valuereiterated <= 0, BalanceEAPB = ValueGlosado - ValueAcceptedFirstInstance.; si PortfolioNote.Status = 2 y Observations en (''Aceptacion IPS glosa Subsanable - Glosa'',''Aceptacion IPS glosa Subsanable - Reiteracion'',''Aceptacion IPS glosa conciliacion'') → Incluye la nota de cartera (mínimo Code y NoteDate por AccountReceivableId) como NoteCode/NoteDate del reporte. else No se asocia nota de cartera al renglón.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListObjectionsReception';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Portfolio.PortfolioNote; Portfolio.PortfolioNoteAccountReceivableAdvance; Common.ConceptGlosas; Glosas.Responsible; Glosas.GlosaMovementGlosa; Glosas.GlosaInvoiceDetail; Glosas.GlosaInvoiceDetailQX; Glosas.GlosaObjectionsReceptionC; Glosas.GlosaObjectionsReceptionD; Common.Customer; Portfolio.AccountReceivable; GeneralLedger.MainAccounts; Portfolio.GetRegimes', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListObjectionsReception';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListObjectionsReception';
-- GO
