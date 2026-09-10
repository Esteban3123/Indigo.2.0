CREATE VIEW [dbo].[ViewOfficeResponse]
AS
SELECT   
	c.Id,
	c.RadicatedConsecutive, 
	c.DocumentDate,
	c.RadicatedDate, 
	cus.Name,
	c.ReceivesTheSettled,
	c.DocumentCommentRadicated, 
	p.PatientCode,
	p.PatientName,
	p.RadicatedNumber,
	p.InvoiceNumber, 
    p.BalanceInvoice,
	Con.Code + ' - ' + con.NameSpecific AS ConceptGlosa, 
	conEva.NameSpecific AS ConceptEvalution, 
	ISNULL(m.JustificationGlosaText, '') AS JustificationGlosaText,
	ISNULL(m.JustificationReiterationText, '')  AS JustificationReiterationText, 
	ISNULL(m.ValueGlosado, 0) AS valueglosado, 
	ISNULL(m.ValueAcceptedFirstInstance, 0) AS ValueAcceptedFirstInstance, 
	ISNULL(m.ValueReiterated, 0) AS valuereiterated, 
    ISNULL(m.ValueAcceptedSecondInstance, 0) AS ValueAcceptedSecondInstance,
	de.servicecode + ' - ' + de.servicename as ServiceName, 
	m.MainGlosa,
	Rg.Code +' - ' + rg.Name  ResponsibleGlosa,
	rr.Code + '-' + rr.Name as ResponsibleReiteration,
	d.DocumentType, 
	grr.Comments
FROM   Glosas.GlosaObjectionsReceptionC AS c 
INNER JOIN Glosas.GlosaObjectionsReceptionD AS d ON c.Id = d.GlosaObjectionsReceptionCId 
INNER JOIN Glosas.GlosaInvoiceDetail AS de ON d.InvoiceNumber = de.InvoiceNumber
INNER JOIN Glosas.GlosaMovementGlosa AS m ON de.Id = m.InvoiceDetailId 
INNER JOIN Glosas.GlosaPortfolioGlosada AS p ON p.InvoiceNumber = d.InvoiceNumber
INNER JOIN Common.Customer AS cus ON cus.Id = c.CustomerId 
INNER JOIN Common.ConceptGlosas AS con ON con.Id = m.CodeGlosaId 
inner join [Glosas].[Responsible] Rg on Rg.id = m.ResponsibleId
LEFT JOIN Common.ConceptGlosas AS conEva ON  conEva.code = m.codeglosaEvaluation 
left join [Glosas].[Responsible] rr on rr.id = m.ResponsibleReiterationId
LEFT join Glosas.RadicateResponse grr on grr.Id = c.IdRadicateResponse
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida el detalle completo del proceso de respuesta a objeciones de glosas entre la IPS y las entidades pagadoras (EPS, aseguradoras). Integra los encabezados y líneas de recepción de objeciones, los ítems de factura glosados, los movimientos de glosa con sus valores glosados, aceptados en primera y segunda instancia y reiterados, junto con los conceptos de glosa aplicados y su evaluación, los responsables de gestión y reiteración, y los datos de cartera de la factura afectada. También incorpora la información del paciente, el número de factura, el nombre del cliente pagador y los comentarios del radicado de respuesta. Sirve como fuente principal para reportes y seguimiento del ciclo de glosas: desde la radicación de la objeción hasta la conciliación final de los valores disputados entre la IPS y el pagador.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewOfficeResponse';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewOfficeResponse';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida la información de oficios de respuesta a glosas, integrando datos de la recepción de objeciones, factura, movimientos de glosa, cartera glosada, cliente pagador, conceptos de glosa y responsables, para su presentación unificada.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewOfficeResponse';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir correspondencia entre la recepción de objeciones (cabecera) y su detalle por GlosaObjectionsReceptionCId.; El número de factura debe coincidir entre el detalle de objeciones, el detalle de factura glosada y la cartera glosada.; Cada movimiento de glosa debe estar asociado a un ítem de detalle de factura existente.; El movimiento de glosa debe tener un concepto de glosa (CodeGlosaId) y un responsable principal (ResponsibleId) válidos.; El cliente referenciado en la recepción debe existir en Common.Customer.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewOfficeResponse';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Los valores monetarios (ValueGlosado, ValueAcceptedFirstInstance, ValueReiterated, ValueAcceptedSecondInstance) nunca se devuelven como NULL: se sustituyen por 0.; Los textos de justificación (glosa y reiteración) nunca se devuelven como NULL: se sustituyen por cadena vacía.; El concepto de glosa principal y el responsable principal siempre están presentes (INNER JOIN), garantizando que toda fila tenga clasificación y responsable.; Los campos ConceptGlosa y ResponsibleGlosa se presentan siempre concatenados como ''Código - Nombre''.; Solo se incluyen recepciones que tengan al menos un ítem en cartera glosada y un movimiento de glosa asociado al detalle de factura.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewOfficeResponse';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Glosa; Objeción de glosa; Radicación; Factura; Cartera glosada; Concepto de glosa; Responsable de glosa; Reiteración de glosa; Justificación de glosa; Valor glosado; Valor aceptado primera instancia; Valor aceptado segunda instancia; Cliente pagador (EPS/aseguradora); Paciente; Servicio facturado', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewOfficeResponse';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Devuelve un conjunto de filas combinando recepción de objeciones, detalle de factura glosada, movimientos de glosa, cartera, cliente, conceptos y responsables.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewOfficeResponse';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe coincidencia entre conEva.code y m.codeglosaEvaluation (LEFT JOIN ConceptGlosas) → Se muestra el nombre específico del concepto de evaluación else ConceptEvalution queda en NULL; si El movimiento tiene ResponsibleReiterationId asociado → Se muestra código y nombre del responsable de reiteración else ResponsibleReiteration queda en NULL; si La recepción tiene IdRadicateResponse asociado → Se incluyen los comentarios de la respuesta de radicación else Comments queda en NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewOfficeResponse';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Glosas.GlosaObjectionsReceptionC; Glosas.GlosaObjectionsReceptionD; Glosas.GlosaInvoiceDetail; Glosas.GlosaMovementGlosa; Glosas.GlosaPortfolioGlosada; Common.Customer; Common.ConceptGlosas; Glosas.Responsible; Glosas.RadicateResponse', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewOfficeResponse';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewOfficeResponse';
GO
