

/*WHERE        (rd.State = '2') AND (rc.State = '2')*/
CREATE VIEW [ViewInternal].[VistaRadicacionDetalladaAll]
AS
SELECT        rc.RadicatedConsecutive AS Radicado, c.Nit AS NitCliente, c.Name AS Cliente, rc.DocumentDate AS FechaDocumento, CAST(rc.ConfirmDateSystem AS date) AS FechaConfirmacion, CAST(rc.ConfirmDate AS date) 
                         AS FechaRadicado, rd.InvoiceNumber AS NumeroFactura, CASE rd.Devolution WHEN 1 THEN 'Si' ELSE 'No' END AS Devolucion, 
                         CASE rd.Devolution WHEN 1 THEN gdrc.CreationUser + ' - ' + p.fullname ELSE '' END AS UsuarioDevolucion, ISNULL(ic.Code + ' - ' + ic.Name, '') AS Categoria, rd.InvoiceDate AS FechaFactura, 
                         rd.IngressNumber AS Ingreso, rd.InvoiceValueEntity AS ValorEntidad, rd.InvoiceValuePacient AS ValorPaciente, 
                         CASE MA.Number WHEN '14090103' THEN 'Contributivo' WHEN '14090304' THEN 'Subsidiado' WHEN '14090401' THEN 'Servicio IPS Privada' WHEN '14090501' THEN 'Medicina Prepagada' WHEN '14090601' THEN
                          'Compañias Aseguradoras' WHEN '14090701' THEN 'Particulares' WHEN '14090901' THEN 'Servicio IPS Publicas' WHEN '14091004' THEN 'Regimen Especial' WHEN '14091102' THEN 'Vinculados - Departamentos'
                          WHEN '14091103' THEN 'Vinculados Municipios' WHEN '14091201' THEN 'Arl Riesgos Profesionales' WHEN '14091403' THEN 'Accidentes de Transito' WHEN '14090201' THEN 'Otras Cuentas X Cobrar' END AS
                          Regimen, rd.State AS EstadoFacRadicada
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
                               GROUP BY gdrd.InvoiceNumber) AS dev ON ar.InvoiceNumber = dev.InvoiceNumber LEFT OUTER JOIN
                         Glosas.GlosaDevolutionsReceptionC AS gdrc WITH (nolock) ON dev.id = gdrc.Id LEFT OUTER JOIN
                         Security.[User] AS u ON gdrc.CreationUser = u.UserCode LEFT OUTER JOIN
                         Security.Person AS p ON u.IdPerson = p.Id
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporting que consolida el detalle completo de facturas radicadas ante entidades pagadoras, sin filtrar por estado (a diferencia de versiones restringidas). Cruza encabezados y líneas de radicación con cuentas por cobrar, datos del cliente facturado, cuenta contable PUC para derivar el régimen de afiliación, categoría de factura y, cuando aplica, información de devoluciones por glosa incluyendo el usuario responsable. Está orientada a auditoría de cartera y seguimiento del ciclo de cobro.', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaRadicacionDetalladaAll';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaRadicacionDetalladaAll';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone el listado detallado de todas las facturas radicadas ante entidades pagadoras con su cliente, valores, categoría, régimen contable y estado de devolución por glosa, sin filtrar por estado.', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaRadicacionDetalladaAll';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las facturas deben tener una cuenta por cobrar con AccountReceivableType = 2 para aparecer en la vista; La cuenta por cobrar debe estar asociada a una cuenta contable principal (AccountWithoutRadicateId) existente en GeneralLedger.MainAccounts; Cada radicado debe tener cliente válido en Common.Customer', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaRadicacionDetalladaAll';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen facturas radicadas que tengan una cuenta por cobrar de tipo 2 (AccountReceivableType = 2); El régimen se deriva exclusivamente de la cuenta contable sin radicar (AccountWithoutRadicateId) asociada a la cuenta por cobrar; Para cada factura, la devolución reportada corresponde a la última recepción de devolución de glosa registrada (MAX(Id) por InvoiceNumber); El cliente, el detalle de radicación y el encabezado de radicación son obligatorios (INNER JOIN); categoría, factura de billing y datos de devolución/usuario son opcionales (LEFT JOIN); El nombre del régimen solo se muestra para las cuentas contables del catálogo enumerado; cualquier otra cuenta produce NULL en Régimen', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaRadicacionDetalladaAll';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Radicación de facturas; Cliente/Entidad pagadora; Factura; Cuenta por cobrar; Categoría de facturación; Devolución de glosa; Régimen de afiliación (contributivo, subsidiado, prepagada, ARL, SOAT, etc.); Plan de cuentas contables (PUC); Valor entidad / Valor paciente; Ingreso del paciente', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaRadicacionDetalladaAll';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] ViewInternal.VistaRadicacionDetalladaAll: Devuelve una fila por cada factura del detalle de radicación cruzada con su cuenta por cobrar tipo 2, incluyendo el último evento de devolución de glosa si existe', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaRadicacionDetalladaAll';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Devolution = 1 → Marca la factura radicada como devuelta (''Si'') y expone el usuario que registró la devolución concatenado con su nombre completo else Devolución = ''No'' y usuario de devolución vacío; si Cuenta contable principal (MA.Number) según catálogo PUC → Clasifica el régimen de la factura: 14090103=Contributivo, 14090304=Subsidiado, 14090401=Servicio IPS Privada, 14090501=Medicina Prepagada, 14090601=Compañías Aseguradoras, 14090701=Particulares, 14090901=Servicio IPS Públicas, 14091004=Régimen Especial, 14091102=Vinculados Departamentos, 14091103=Vinculados Municipios, 14091201=ARL Riesgos Profesionales, 14091403=Accidentes de Tránsito, 14090201=Otras Cuentas X Cobrar', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaRadicacionDetalladaAll';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Portfolio.RadicateInvoiceC; Portfolio.RadicateInvoiceD; Common.Customer; Portfolio.AccountReceivable; GeneralLedger.MainAccounts; Billing.Invoice; Billing.InvoiceCategories; Glosas.GlosaDevolutionsReceptionD; Glosas.GlosaDevolutionsReceptionC; Security.User; Security.Person', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaRadicacionDetalladaAll';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaRadicacionDetalladaAll';
GO
