

CREATE VIEW [Glosas].[ViewCoordinationGlosaExport] 
AS
WITH RE_Radicate AS
(
	SELECT rid.InvoiceNumber, MAX(ri.Id) Id, MAX(rid.RadicatedNumber) RadicatedNumber, MAX(rid.RadicatedDate) RadicatedDate
	FROM Portfolio.RadicateInvoiceC ri WITH (NOLOCK)
	JOIN Portfolio.RadicateInvoiceD rid WITH (NOLOCK) ON ri.Id = rid.RadicateInvoiceCId
	WHERE ri.State = 2 AND rid.State = 2 
	GROUP BY rid.InvoiceNumber
)

SELECT 
	gorc.Id AS GlosaObjectionsReceptionCId, gpg.InvoiceNumber, gmg.Id AS MovementId, gpg.InvoiceDate, ri.RadicatedNumber, 
	ri.RadicatedDate, gorc.RadicatedConsecutive AS ObjectionRadicatedConsecutive, gorc.RadicatedDate AS ObjectionRadicatedDate, 
	gpg.PatientName, gpg.IngressNumber, gpg.IngressDate, gid.ServiceCode,
	gid.ServiceName, gid.ServiceDate, gidQX.ServiceCode AS ServiceCodeQX, gidQX.ServiceName AS ServiceNameQX,
	gid.CostCenterCode, gid.CostCenterName, gmg.CodeGlosa, cg.NameSpecific, gmg.RationaleGlosa, 
	gmg.ValueGlosado, gmg.ValuePayments, r.Name AS ResponsableName
FROM Glosas.GlosaObjectionsReceptionC gorc WITH (NOLOCK)
JOIN Glosas.GlosaObjectionsReceptionD gord WITH (NOLOCK) ON gorc.Id = gord.GlosaObjectionsReceptionCId
JOIN Glosas.GlosaPortfolioGlosada gpg WITH (NOLOCK) ON gpg.Id = gord.PortfolioGlosaId
JOIN Glosas.GlosaMovementGlosa gmg WITH (NOLOCK) ON gpg.InvoiceNumber = gmg.InvoiceNumber 
JOIN Glosas.GlosaInvoiceDetail gid WITH (NOLOCK) ON gid.Id = gmg.InvoiceDetailId
JOIN RE_Radicate ri ON ri.InvoiceNumber = gpg.InvoiceNumber
JOIN Common.ConceptGlosas cg WITH (NOLOCK) ON cg.Id = gmg.CodeGlosaId
JOIN Glosas.Responsible r WITH (NOLOCK) ON r.Id = gmg.ResponsibleId
LEFT JOIN Glosas.GlosaInvoiceDetailQX gidQX  WITH (NOLOCK) ON gid.Id = gidQX.InvoiceDetailId AND gmg.InvoiceDetailIdQX = gidQX.Id
WHERE
(
	(gpg.State IN ('2', '3') AND gord.DocumentType = '1') 
	OR
	(gpg.State IN ('5', '6') AND gord.DocumentType = '2')
)
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de exportación para el proceso de coordinación de glosas. Integra las objeciones de glosas recibidas de entidades pagadoras (EPS, aseguradoras) con el detalle de cada ítem glosado, el movimiento individual de la glosa (valor glosado, justificación, responsable), el servicio o procedimiento facturado y los datos del radicado de cobro original de la factura. Filtra únicamente los registros en estados activos de objeción (primera y segunda instancia) según el tipo de documento, y consolida en una sola fila la información del paciente, el número de ingreso, las fechas de radicación de la factura y de la objeción, el código y nombre del concepto de glosa (catálogo CUPS/auditoría), el responsable de gestión y los valores glosados y pagados. Se utiliza para exportar y reportar el seguimiento del ciclo de glosas en cartera, permitiendo a los equipos de facturación y auditoría médica analizar y responder objeciones de glosas por factura, servicio y pagador.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'VIEW', @level1name = N'ViewCoordinationGlosaExport';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'VIEW', @level1name = N'ViewCoordinationGlosaExport';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida la información de exportación/coordinación de glosas, integrando datos de la factura, su radicación, las objeciones recibidas, los movimientos de glosa y servicios (incluidos quirúrgicos) para reporte.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'VIEW', @level1name=N'ViewCoordinationGlosaExport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben existir radicaciones de factura con estado=2 tanto en el encabezado como en el detalle para que la factura aparezca en el reporte.; Cada movimiento de glosa debe tener concepto de glosa y responsable asociados.; La factura glosada debe estar enlazada a una recepción de objeción mediante su detalle.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'VIEW', @level1name=N'ViewCoordinationGlosaExport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Una factura aparece en el reporte solo si tiene radicación efectiva (State=2 en encabezado y detalle).; Por cada InvoiceNumber se toma una única radicación representativa, mediante MAX(Id), MAX(RadicatedNumber) y MAX(RadicatedDate).; El cruce de estado de la cartera glosada con el tipo de documento de la objeción es excluyente: estados 2/3 corresponden a DocumentType=1 y estados 5/6 a DocumentType=2.; Todas las consultas se hacen con NOLOCK, asumiendo lecturas no bloqueantes del proceso operativo.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'VIEW', @level1name=N'ViewCoordinationGlosaExport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Glosa; Objeción de glosa; Radicación de factura; Factura glosada; Cartera glosada; Concepto de glosa; Responsable de glosa; Servicio quirúrgico (QX); Centro de costo; Ingreso del paciente; Valor glosado; Valor pagado', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'VIEW', @level1name=N'ViewCoordinationGlosaExport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Resultset: Se devuelven solo facturas cuya radicación tenga State=2 en RadicateInvoiceC y RadicateInvoiceD (toma máxima Id, consecutivo y fecha por InvoiceNumber).; [RETURN_RESULT] Resultset: Se incluye la fila cuando GlosaPortfolioGlosada.State IN (''2'',''3'') y GlosaObjectionsReceptionD.DocumentType=''1'', o cuando State IN (''5'',''6'') y DocumentType=''2''.; [RETURN_RESULT] Resultset: El detalle quirúrgico (ServiceCodeQX/ServiceNameQX) se trae mediante LEFT JOIN, por lo que es opcional y aparece solo si el movimiento referencia un InvoiceDetailIdQX existente.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'VIEW', @level1name=N'ViewCoordinationGlosaExport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si gpg.State IN (''2'',''3'') AND gord.DocumentType=''1'' → Incluye la factura glosada como caso de objeción tipo documento 1 (glosa inicial).; si gpg.State IN (''5'',''6'') AND gord.DocumentType=''2'' → Incluye la factura glosada como caso de objeción tipo documento 2 (ratificación/segunda instancia). else Se excluye del resultado cualquier combinación distinta de estado y tipo de documento.; si ri.State=2 AND rid.State=2 en RE_Radicate → Solo radicaciones aprobadas/activas se consideran como fuente de número y fecha de radicado.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'VIEW', @level1name=N'ViewCoordinationGlosaExport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Portfolio.RadicateInvoiceC; Portfolio.RadicateInvoiceD; Glosas.GlosaObjectionsReceptionC; Glosas.GlosaObjectionsReceptionD; Glosas.GlosaPortfolioGlosada; Glosas.GlosaMovementGlosa; Glosas.GlosaInvoiceDetail; Glosas.GlosaInvoiceDetailQX; Common.ConceptGlosas; Glosas.Responsible', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'VIEW', @level1name=N'ViewCoordinationGlosaExport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'VIEW', @level1name=N'ViewCoordinationGlosaExport';
GO
