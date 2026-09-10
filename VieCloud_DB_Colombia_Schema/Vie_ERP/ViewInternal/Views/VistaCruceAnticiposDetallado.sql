

CREATE VIEW [ViewInternal].[VistaCruceAnticiposDetallado]
AS
SELECT        cu.Nit, cu.Name AS Cliente, ar.InvoiceNumber AS Factura, tr.Code AS CodigoCruceAnticipo, tr.DocumentDate AS FechaDocumento, pa.Code AS CodigoAnticipo,
                         trd.Value AS ValorPagado, '33' as Empresa
FROM            Portfolio.PortfolioTransfer AS tr INNER JOIN
                         Portfolio.PortfolioTransferDetail AS trd ON tr.Id = trd.PortfolioTrasferId INNER JOIN
                         Portfolio.AccountReceivable AS ar ON trd.AccountReceivableId = ar.Id INNER JOIN
                         Common.Customer AS cu ON ar.CustomerId = cu.Id INNER JOIN
                         Portfolio.PortfolioAdvance AS pa ON tr.PortfolioAdvanceId = pa.Id
WHERE        (tr.Status = 2) AND (ar.PortfolioStatus = 3)
union
SELECT        cu.Nit, cu.Name AS Cliente, ar.InvoiceNumber AS Factura, tr.Code AS CodigoCruceAnticipo, tr.DocumentDate AS FechaDocumento, pa.Code AS CodigoAnticipo,
                         trd.Value AS ValorPagado, '09' as Empresa
FROM            Portfolio.PortfolioTransfer AS tr INNER JOIN
                         Portfolio.PortfolioTransferDetail AS trd ON tr.Id = trd.PortfolioTrasferId INNER JOIN
                         Portfolio.AccountReceivable AS ar ON trd.AccountReceivableId = ar.Id INNER JOIN
                         Common.Customer AS cu ON ar.CustomerId = cu.Id INNER JOIN
                         Portfolio.PortfolioAdvance AS pa ON tr.PortfolioAdvanceId = pa.Id
						 WHERE        (tr.Status = 2) AND (ar.PortfolioStatus = 3)
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista consolidada para reporting del cruce de anticipos contra facturas en cartera, uniendo dos empresas (códigos ''33'' y ''09''). Muestra, por cliente, las facturas cuyas cuentas por cobrar tienen estado de cartera 3 y cuyo traslado está en estado 2 (completado/aprobado), junto con el código del cruce, la fecha del documento, el código del anticipo aplicado y el valor pagado.', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaCruceAnticiposDetallado';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaCruceAnticiposDetallado';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone el detalle de los cruces de anticipos aplicados a facturas de clientes, mostrando el anticipo origen, la factura cruzada y el valor aplicado, replicado para dos empresas (''33'' y ''09'').', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaCruceAnticiposDetallado';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El traslado de cartera debe estar en Status = 2 (estado considerado válido/aplicado); La cuenta por cobrar debe tener PortfolioStatus = 3; Debe existir relación entre PortfolioTransfer y un PortfolioAdvance (cruce contra anticipo)', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaCruceAnticiposDetallado';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen cruces cuyo traslado de cartera esté en Status = 2; Solo se incluyen cuentas por cobrar con PortfolioStatus = 3; Cada cruce real se reporta dos veces, una por cada empresa fija (''33'' y ''09''); el UNION elimina duplicados exactos pero al diferenciarse por la columna Empresa siempre genera ambas filas; Solo se consideran traslados que tienen un anticipo asociado (INNER JOIN con PortfolioAdvance); El valor pagado proviene del detalle de la transferencia (no del encabezado)', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaCruceAnticiposDetallado';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cruce de anticipos; Anticipo; Factura / Cuenta por cobrar; Cliente (NIT); Traslado de cartera; Empresa', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaCruceAnticiposDetallado';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] ViewInternal.VistaCruceAnticiposDetallado: Cuando tr.Status = 2 y ar.PortfolioStatus = 3 → retorna fila con datos del cruce (cliente, factura, código de cruce, código anticipo, valor pagado) etiquetada como Empresa ''33'' y duplicada vía UNION como Empresa ''09''', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaCruceAnticiposDetallado';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Portfolio.PortfolioTransfer; Portfolio.PortfolioTransferDetail; Portfolio.AccountReceivable; Common.Customer; Portfolio.PortfolioAdvance', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaCruceAnticiposDetallado';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaCruceAnticiposDetallado';
GO
