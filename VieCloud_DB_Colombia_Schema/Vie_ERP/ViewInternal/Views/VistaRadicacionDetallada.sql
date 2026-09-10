

CREATE VIEW [ViewInternal].[VistaRadicacionDetallada]
AS
SELECT        rc.RadicatedConsecutive AS Radicado, c.Nit AS NitCliente, c.Name AS Cliente, rc.DocumentDate AS FechaDocumento, CAST(rc.ConfirmDateSystem AS date) AS FechaConfirmacion, CAST(rc.ConfirmDate AS date) 
                         AS FechaRadicado, rd.InvoiceNumber AS NumeroFactura, CASE rd.Devolution WHEN 1 THEN 'Si' ELSE 'No' END AS Devolucion, 
                         CASE rd.Devolution WHEN 1 THEN gdrc.CreationUser + ' - ' + p.fullname ELSE '' END AS UsuarioDevolucion, ISNULL(ic.Code + ' - ' + ic.Name, '') AS Categoria, rd.InvoiceDate AS FechaFactura, 
                         rd.IngressNumber AS Ingreso, rd.InvoiceValueEntity AS ValorEntidad, rd.InvoiceValuePacient AS ValorPaciente, 
                         CASE MA.Number WHEN '13190101' THEN 'Contributivo' WHEN '13190301' THEN 'Subsidiado' WHEN '13190801' THEN 'Servicio IPS Privada' WHEN '13190501' THEN 'Medicina Prepagada' WHEN '13191201' THEN
                          'Compañias Aseguradoras' WHEN '13191601' THEN 'Particulares' WHEN '13191001' THEN 'Servicio IPS Publicas' WHEN '13191401' THEN 'Regimen Especial' WHEN '13192101' THEN 'Vinculados - Departamentos'
                          WHEN '13192102' THEN 'Vinculados Municipios' WHEN '13192301' THEN 'Arl Riesgos Profesionales' WHEN '13191701' THEN 'Accidentes de Transito' WHEN '13199002' THEN 'Otras Cuentas X Cobrar' END AS
                          Regimen, rc.State AS EstadoFacRadicada
FROM            Portfolio.RadicateInvoiceC AS rc WITH (nolock) INNER JOIN
                         Portfolio.RadicateInvoiceD AS rd WITH (nolock) ON rc.Id = rd.RadicateInvoiceCId INNER JOIN
                         Common.Customer AS c WITH (nolock) ON c.Id = rc.CustomerId INNER JOIN
                         Portfolio.AccountReceivable AS ar WITH (nolock) ON ar.InvoiceNumber = rd.InvoiceNumber AND ar.AccountReceivableType = 2 INNER JOIN
                         GeneralLedger.MainAccounts AS MA WITH (nolock) ON MA.Id = ar.AccountWithoutRadicateId LEFT OUTER JOIN
                         Billing.Invoice AS i WITH (nolock) ON i.InvoiceNumber = ar.InvoiceNumber LEFT OUTER JOIN
                         Billing.InvoiceCategories AS ic WITH (nolock) ON ic.Id = i.InvoiceCategoryId LEFT OUTER JOIN
                             (SELECT        MAX(gdrc.Id) AS id, gdrd.InvoiceNumber
                               FROM            Glosas.GlosaDevolutionsReceptionD AS gdrd WITH (nolock) INNER JOIN
                                                         Glosas.GlosaDevolutionsReceptionC AS gdrc WITH (nolock) ON gdrc.Id = gdrd.GlosaDevolutionsReceptionCId
                               WHERE        (gdrd.State = 2) AND (gdrc.State <> 4)
                               GROUP BY gdrd.InvoiceNumber) AS dev ON ar.InvoiceNumber = dev.InvoiceNumber LEFT OUTER JOIN
                         Glosas.GlosaDevolutionsReceptionC AS gdrc WITH (nolock) ON dev.id = gdrc.Id LEFT OUTER JOIN
                         Security.[User] AS u ON gdrc.CreationUser = u.UserCode LEFT OUTER JOIN
                         Security.Person AS p ON u.IdPerson = p.Id
WHERE        (rd.State = '2') AND (rc.State = '2')
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporting que consolida el detalle de facturas radicadas ante entidades pagadoras en estado confirmado (estado 2 en encabezado y detalle). Cruza cada radicado con su cliente, factura, cuenta contable de cartera y categoría de facturación, clasifica el régimen de cobro según el código PUC (contributivo, subsidiado, particular, ARL, entre otros) e identifica si la factura fue devuelta por glosa, exponiendo el usuario responsable de la devolución.', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaRadicacionDetallada';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaRadicacionDetallada';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone facturas radicadas confirmadas con su detalle de cliente, valores, régimen contable, categoría y trazabilidad de devolución de glosa asociada.', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaRadicacionDetallada';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las facturas radicadas (encabezado y detalle) deben estar en State = 2 (confirmadas) para aparecer en la vista; Debe existir una cuenta por cobrar (AccountReceivable) con AccountReceivableType = 2 cuyo InvoiceNumber coincida con la factura radicada; La cuenta contable referenciada en AccountWithoutRadicateId debe existir en MainAccounts', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaRadicacionDetallada';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se listan radicaciones cuyo encabezado y detalle están en estado 2 (confirmado); El régimen se determina exclusivamente por el código de la cuenta contable contra la cual se registró la cuenta por cobrar sin radicar; Las devoluciones de glosa anuladas (gdrc.State = 4) se ignoran en la trazabilidad; Por cada factura solo se asocia la devolución de glosa más reciente (MAX(Id)); La vista usa NOLOCK en todas las tablas, por lo que admite lecturas sucias', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaRadicacionDetallada';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Radicación de facturas; Cliente / entidad pagadora; Factura de venta; Cuenta por cobrar; Cuenta contable PUC; Régimen de salud (Contributivo, Subsidiado, Prepagada, ARL, SOAT, Particulares, Régimen Especial, Vinculados); Devolución de glosa; Categoría de facturación; Valor entidad / valor paciente', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaRadicacionDetallada';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] VistaRadicacionDetallada: Devuelve solo registros donde rd.State = 2 y rc.State = 2 (radicaciones confirmadas); [RETURN_RESULT] VistaRadicacionDetallada: Solo se cruza con cuentas por cobrar de tipo 2 (AccountReceivableType = 2); [RETURN_RESULT] VistaRadicacionDetallada: Para devoluciones de glosa, solo considera la última recepción (MAX(Id)) por factura cuyo detalle esté en State = 2 y cuyo encabezado no esté en State = 4 (anulado)', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaRadicacionDetallada';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si rd.Devolution = 1 → Marca Devolucion=''Si'' y expone el usuario y nombre de persona que registró la devolución de glosa else Marca Devolucion=''No'' y deja el usuario de devolución vacío; si MA.Number según código PUC (13190101, 13190301, 13190801, 13190501, 13191201, 13191601, 13191001, 13191401, 13192101, 13192102, 13192301, 13191701, 13199002) → Clasifica el régimen del pagador (Contributivo, Subsidiado, IPS Privada/Pública, Medicina Prepagada, Aseguradoras, Particulares, Régimen Especial, Vinculados Departamentos/Municipios, ARL, SOAT, Otras Cuentas por Cobrar) else Régimen queda en NULL si la cuenta contable no coincide con ninguno de los códigos esperados; si Existe categoría de factura asociada (ic) → Muestra ''Code - Name'' de la categoría else Devuelve cadena vacía', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaRadicacionDetallada';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Portfolio.RadicateInvoiceC; Portfolio.RadicateInvoiceD; Common.Customer; Portfolio.AccountReceivable; GeneralLedger.MainAccounts; Billing.Invoice; Billing.InvoiceCategories; Glosas.GlosaDevolutionsReceptionD; Glosas.GlosaDevolutionsReceptionC; Security.User; Security.Person', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaRadicacionDetallada';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaRadicacionDetallada';
GO
