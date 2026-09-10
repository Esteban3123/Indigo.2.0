

CREATE VIEW [dbo].[Statistic_GeneralGlosa]
AS
SELECT        T.Nit, T.Name AS Entidad, RC.RadicatedConsecutive AS NroRadicadoGlosa, RC.RadicatedDate AS FechaRadicadoGlosas, 
                         RC.ConfirmDate AS FechaConfirmacionRadicadoGlosas, 
                         CASE WHEN RC.State = 1 THEN 'Sin Confirmar' WHEN RC.State = 2 THEN 'ConfirmadoRadicado' WHEN RC.State = 3 THEN 'OficioConRespuesta' WHEN RC.State = 4
                          THEN 'Anulada' END AS EstadoRadicadoGlosa, CG.InvoiceNumber AS Factura, CG.InvoiceDate AS FechaFactura, CG.InvoiceValueEntity AS ValorEntidad, 
                         CG.BalanceInvoice AS ValorFactura, CG.ValueGlosado AS ValorGlosado, CG.ValueAcceptedFirstInstance AS ValorAceptadoPrimeraInstancia, 
                         CG.ValueReiterated AS ValorReiterado, CG.ValueReiterationBalance AS ValorAceptadoEAPBReiteracion, 
                         CG.ValueAcceptedSecondInstance AS ValorAceptadoSegundaInstancia, CG.ValueAcceptedIPSconciliation AS ValorAceptadoIPSConciliacion, 
                         CG.ValueAcceptedEAPBconciliation AS ValorAceptadoEAPBConciliacion, CG.BalanceGlosa AS SaldoPendienteConciliar, CG.RadicatedNumber AS NroRadicadoERP, 
                         CG.RadicatedDate AS FechaRadicadoERP, CG.AccountantAccountCustomers AS Cuenta
FROM            Glosas.GlosaPortfolioGlosada AS CG INNER JOIN
                         Glosas.GlosaObjectionsReceptionD AS RD ON RD.InvoiceNumber = CG.InvoiceNumber AND RD.DocumentType = '1' INNER JOIN
                         Glosas.GlosaObjectionsReceptionC AS RC ON RC.Id = RD.GlosaObjectionsReceptionCId INNER JOIN
                         Common.Customer AS T ON RC.CustomerId = T.Id
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista estadística de glosas generales que consolida el estado financiero y de seguimiento de cada factura glosada por entidades pagadoras (EPS/EAPB/aseguradoras). Integra la cartera de facturas glosadas con los radicados de objeción recibidos y los datos del cliente o aseguradora, mostrando por cada factura: la entidad pagadora (NIT y nombre), el consecutivo y fechas del radicado de glosa, el estado del radicado (sin confirmar, confirmado, con respuesta o anulado), y el detalle de valores cobrados, glosados, aceptados en primera y segunda instancia, reiterados, conciliados por IPS y EAPB, y el saldo pendiente por conciliar. Sirve para reportería financiera y de cartera de glosas, permitiendo hacer seguimiento al proceso de objeción y conciliación de glosas entre la IPS y las entidades pagadoras.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'Statistic_GeneralGlosa';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'Statistic_GeneralGlosa';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida información estadística de facturas glosadas con su radicado de objeción, valores financieros del proceso de glosa (aceptados, reiterados, conciliados, saldo) y datos de la entidad pagadora.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Statistic_GeneralGlosa';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir correspondencia entre la factura en cartera glosada y un detalle de recepción de objeciones con DocumentType = ''1''; El detalle de recepción debe estar vinculado a un encabezado de recepción de objeciones existente; El encabezado de recepción debe tener un cliente registrado en Common.Customer', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Statistic_GeneralGlosa';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen registros de recepción de objeciones cuyo detalle tenga DocumentType = ''1'' (tipo factura); Cada fila combina obligatoriamente una factura en cartera glosada con un detalle de recepción de objeciones y su encabezado; Solo se incluyen radicados de glosa asociados a un cliente existente en Common.Customer (INNER JOIN); El estado del radicado solo admite las traducciones 1..4; otros valores quedan en NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Statistic_GeneralGlosa';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Glosa; Radicado de glosa; Factura; Entidad pagadora (cliente/EPS/EAPB); Conciliación de glosa (IPS y EAPB); Reiteración de glosa; Cartera glosada; Cuenta contable de clientes; NIT', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Statistic_GeneralGlosa';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve únicamente las filas donde existe coincidencia entre GlosaPortfolioGlosada.InvoiceNumber y GlosaObjectionsReceptionD.InvoiceNumber con DocumentType=''1'', enlazadas con su encabezado de recepción y su cliente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Statistic_GeneralGlosa';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si RC.State = 1 → Estado del radicado de glosa se reporta como ''Sin Confirmar''; si RC.State = 2 → Estado del radicado de glosa se reporta como ''ConfirmadoRadicado''; si RC.State = 3 → Estado del radicado de glosa se reporta como ''OficioConRespuesta''; si RC.State = 4 → Estado del radicado de glosa se reporta como ''Anulada'' else NULL (cualquier otro valor de State no se traduce a etiqueta)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Statistic_GeneralGlosa';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Glosas.GlosaPortfolioGlosada; Glosas.GlosaObjectionsReceptionD; Glosas.GlosaObjectionsReceptionC; Common.Customer', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Statistic_GeneralGlosa';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Statistic_GeneralGlosa';
GO
