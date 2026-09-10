
CREATE VIEW [dbo].[Statistic_DetailGlosaXConcept]
AS
SELECT     CO.Code AS CodigoConceptoGeneral, CO.NameGeneral AS DescripcionConceptoGeneral, CO.NameSpecific AS DescripcionConceptoEspecifico, 
                      DG.RationaleGlosa AS Comentario, DG.InvoiceNumber AS NroFactura, G.InvoiceDate AS FechaFactura, C.Nit, C.Name AS Entidad, DE.CostCenterCode AS CC, 
                      DE.CostCenterName AS NombreCentro, DG.ValueGlosado AS ValorGlosado, COALESCE (ISNULL(DG.ValueAcceptedFirstInstance, 0), 0) AS ValorAceptadoGlosaInicial, 
                      COALESCE (ISNULL(DG.ValueReiterated, 0), 0) AS ValorReiterado, COALESCE (ISNULL(DG.ValueReiterationBalance, 0), 0) AS ValorAceptadoEAPBReiteracion, 
                      COALESCE (ISNULL(DG.ValueAcceptedSecondInstance, 0), 0) AS ValorAceptadoReiteracion, COALESCE (ISNULL(DG.ValueAcceptedIPSconciliation, 0), 0) 
                      AS ValorAceptadoIPSConciliacion, COALESCE (ISNULL(DG.ValueAcceptedEAPBconciliation, 0), 0) AS ValorAceptadoEAPBConciliacion, 
                      COALESCE (ISNULL(DG.ValuePendingConciliation, 0), 0) AS ValorPendienteConciliar, G.InvoiceValueEntity AS ValorFactura, RTRIM(DE.ServiceCode) 
                      + RTRIM(DE.ServiceName) AS Servicio, DE.MedicalCode AS CodMedico, DE.MedicalName AS Medico, G.RadicatedNumber AS RadicadoERP, 
                      G.RadicatedDate AS FechaRadicadoERP, GC.RadicatedDate AS FechaRecepcionObjecion, DE.BillerCode AS Codfacturador, DE.BillerName AS Facturador, 
                      CASE WHEN DE.TypeServiceProduct = 1 THEN 'Servicio' WHEN DE.TypeServiceProduct = 2 THEN 'Medicamento o Insumo' END AS TipoServicio, 
                      CASE WHEN DE.TypeProcedure = 1 THEN 'No quirurgico' WHEN DE.TypeProcedure = 2 THEN 'Quirurgico' WHEN DE.TypeProcedure = 3 THEN 'Paquete' WHEN DE.TypeProcedure
                       = 4 THEN 'NoAplica' END AS TipoProcedimiento, DG.JustificationGlosaText AS Respuesta
FROM         Glosas.GlosaMovementGlosa AS DG INNER JOIN
                      Glosas.GlosaPortfolioGlosada AS G ON DG.InvoiceNumber = G.InvoiceNumber INNER JOIN
                      Glosas.GlosaObjectionsReceptionD AS RG ON G.Id = RG.PortfolioGlosaId AND RG.DocumentType = '1' INNER JOIN
                      Glosas.GlosaObjectionsReceptionC AS GC ON RG.GlosaObjectionsReceptionCId = GC.Id INNER JOIN
                      Common.Customer AS C ON GC.CustomerId = C.Id INNER JOIN
                      Common.ConceptGlosas AS CO ON DG.CodeGlosaId = CO.Id INNER JOIN
                      Glosas.GlosaInvoiceDetail AS DE ON DG.InvoiceDetailId = DE.Id
WHERE     (DG.MainGlosa = 1)
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista estadística que muestra el detalle de glosas clasificadas por concepto de glosa, integrando los movimientos principales de glosa (primera instancia, reiteración y conciliación) con los datos de la factura glosada, la entidad pagadora (EPS/EAPB), el centro de costos y el servicio o medicamento facturado. Combina los catálogos de conceptos de glosa, el portafolio de cartera glosada, el detalle de ítems de factura y las recepciones de objeciones para exponer valores glosados, aceptados, reiterados, conciliados y pendientes de conciliar por cada ítem. Sirve como base para reportes de auditoría de glosas, análisis financiero de cartera glosada y seguimiento del ciclo de glosa por concepto, entidad pagadora, factura, profesional y tipo de servicio o procedimiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'Statistic_DetailGlosaXConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'Statistic_DetailGlosaXConcept';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista estadística que consolida el detalle de glosas principales por concepto, mostrando valores glosados, aceptados, reiterados y conciliados junto con datos de factura, entidad pagadora, servicio y médico responsable.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Statistic_DetailGlosaXConcept';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada movimiento de glosa debe estar asociado a una factura existente en la cartera glosada (GlosaPortfolioGlosada); Debe existir una recepción de objeción tipo documento ''1'' que vincule la factura con la cabecera de objeción; El movimiento de glosa debe tener un concepto de glosa válido en el catálogo Common.ConceptGlosas; El movimiento debe referenciar un detalle de factura existente en GlosaInvoiceDetail; La cabecera de objeción debe estar asociada a un cliente registrado en Common.Customer', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Statistic_DetailGlosaXConcept';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La vista solo expone glosas marcadas como principales (MainGlosa=1); Los valores monetarios derivados (aceptado inicial, reiterado, aceptado reiteración EAPB, aceptado segunda instancia, aceptado IPS/EAPB conciliación, pendiente conciliar) nunca se exponen como NULL: siempre se devuelve 0 cuando faltan; Cada fila representa la combinación única movimiento-glosa + recepción de objeción documental tipo ''1''; El servicio se concatena como código + nombre (RTRIM aplicado para evitar espacios sobrantes)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Statistic_DetailGlosaXConcept';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Glosa; Glosa principal; Concepto de glosa; Factura; Cartera glosada; Objeción; Radicado ERP; Recepción de objeción; Entidad pagadora (EPS/EAPB); Conciliación de glosa; Reiteración de glosa; Centro de costo; Facturador; Médico tratante; Servicio / Medicamento o Insumo; Tipo de procedimiento (quirúrgico, no quirúrgico, paquete); Justificación / Respuesta de glosa; Valor aceptado IPS; Valor aceptado EAPB; Valor pendiente por conciliar', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Statistic_DetailGlosaXConcept';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Solo se incluyen movimientos donde MainGlosa = 1, es decir, únicamente las glosas principales (excluye glosas secundarias o derivadas); [RETURN_RESULT] resultset: Filtra recepciones de objeción con DocumentType = ''1'' (un único tipo documental se considera para la vista); [RETURN_RESULT] resultset: Los valores monetarios de aceptación, reiteración y conciliación se normalizan a 0 cuando son NULL mediante COALESCE/ISNULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Statistic_DetailGlosaXConcept';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si DE.TypeServiceProduct = 1 → Se etiqueta el ítem como ''Servicio'' else Si TypeServiceProduct = 2 se etiqueta como ''Medicamento o Insumo''; otros valores devuelven NULL; si DE.TypeProcedure = 1 → Se etiqueta como ''No quirurgico'' else TypeProcedure=2 → ''Quirurgico''; =3 → ''Paquete''; =4 → ''NoAplica''; otros valores devuelven NULL; si DG.MainGlosa = 1 → El movimiento de glosa se incluye en la vista else Se excluye del resultado', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Statistic_DetailGlosaXConcept';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Glosas.GlosaMovementGlosa; Glosas.GlosaPortfolioGlosada; Glosas.GlosaObjectionsReceptionD; Glosas.GlosaObjectionsReceptionC; Common.Customer; Common.ConceptGlosas; Glosas.GlosaInvoiceDetail', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Statistic_DetailGlosaXConcept';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Statistic_DetailGlosaXConcept';
GO
