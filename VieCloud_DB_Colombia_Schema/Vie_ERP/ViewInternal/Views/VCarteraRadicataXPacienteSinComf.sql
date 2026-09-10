

CREATE VIEW [ViewInternal].[VCarteraRadicataXPacienteSinComf]
AS
SELECT        rc.RadicatedConsecutive AS Radicado, tp.Nit, tp.Name AS Entidad, ar.InvoiceNumber AS Factura, 
                         CASE paci.IPTIPODOC WHEN '1' THEN 'Cédula de Ciudadanía' WHEN '2' THEN 'Cédula de Extranjería' WHEN '3' THEN 'Tarjeta de Identidad' WHEN '4' THEN 'Registro Civil' WHEN '5' THEN 'Pasaporte' WHEN '6' THEN 'Adulto Sin Identificación'
                          WHEN '7' THEN 'Menor Sin Identificación' ELSE '' END AS TipoIdentificacionPaciente, rd.PatientCode AS IdentificacionPaciente, rd.PatientName AS NombrePaciente, rd.IngressDate AS FechaIngreso, 
                         CAST(rd.InvoiceDate AS date) AS FechaFactura, rc.ConfirmDateSystem AS FechaRadicado, ISNULL(FUR.NUMSOA, '') AS NumeroPoliza, CAST(rd.InvoiceValueEntity + rd.InvoiceValuePacient AS decimal(18, 0)) 
                         AS ValorBrutoFactura, CAST(rd.InvoiceValuePacient AS decimal(18, 0)) AS ValorCuotaRecuperacion, CAST(ISNULL(rd.CreditNoteValue, 0) AS decimal(18, 0)) AS ValorNotaCredito, CAST(ISNULL(rd.DebitNoteValue, 0) AS decimal(18, 
                         0)) AS ValorNotaDebito, CAST(rd.BalanceInvoice AS decimal(18, 0)) AS ValorNetoFactura, ISNULL(notas.Value, 0) AS TotalNotas, ISNULL(pagos.Valor, 0) AS TotalPagos, ar.Balance AS SaldoActual, 
                         rc.DocumentDate AS FechaDocumento
FROM            Portfolio.AccountReceivable AS ar INNER JOIN
                         Common.ThirdParty AS tp ON ar.ThirdPartyId = tp.Id INNER JOIN
                         Portfolio.RadicateInvoiceD AS rd ON rd.InvoiceNumber = ar.InvoiceNumber AND rd.State = 1 INNER JOIN
                         Portfolio.RadicateInvoiceC AS rc ON rc.Id = rd.RadicateInvoiceCId AND rc.State = 1 LEFT OUTER JOIN
                             (SELECT        ar.InvoiceNumber, SUM(ptd.Value) AS Valor
                               FROM            Portfolio.PortfolioTransfer AS pt INNER JOIN
                                                         Portfolio.PortfolioTransferDetail AS ptd ON pt.Id = ptd.PortfolioTrasferId INNER JOIN
                                                         Portfolio.AccountReceivable AS ar ON ar.Id = ptd.AccountReceivableId
                               WHERE        (pt.Status = 2) AND (ar.AccountReceivableType = 2)
                               GROUP BY ar.InvoiceNumber) AS pagos ON pagos.InvoiceNumber = ar.InvoiceNumber LEFT OUTER JOIN
                             (SELECT        ar.InvoiceNumber, SUM(pnd.AdjusmentValue) AS Value
                               FROM            Portfolio.PortfolioNote AS pn INNER JOIN
                                                         Portfolio.PortfolioNoteAccountReceivableAdvance AS pnd ON pnd.PortfolioNoteId = pn.Id INNER JOIN
                                                         Portfolio.AccountReceivable AS ar ON ar.Id = pnd.AccountReceivableId
                               WHERE        (ar.AccountReceivableType = 2)
                               GROUP BY ar.InvoiceNumber) AS notas ON notas.InvoiceNumber = ar.InvoiceNumber LEFT OUTER JOIN
                         dbo.INPACIENT AS paci ON paci.IPCODPACI = rd.PatientCode LEFT OUTER JOIN
                         dbo.ADFURIPSU AS FUR ON FUR.NUMINGRES = rd.IngressNumber
WHERE        (ar.AccountReceivableType = 2)
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporting que consolida la cartera radicada ante entidades pagadoras para facturas de tipo cuentas por cobrar (AccountReceivableType = 2), mostrando datos de identificación del paciente, entidad pagadora (NIT/nombre), valores brutos, cuota de recuperación, notas crédito/débito, pagos aplicados y saldo actual. Incluye información de póliza SOAT (FURIPS) cuando la atención corresponde a accidente de tránsito. Excluye registros con confirmación (SinComf), orientada a seguimiento y gestión de cobro de cartera radicada pendiente.', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VCarteraRadicataXPacienteSinComf';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VCarteraRadicataXPacienteSinComf';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolidar la cartera de facturas radicadas a entidades pagadoras a nivel de paciente, con valores brutos, cuotas de recuperación, notas crédito/débito, pagos, saldos y datos de póliza FURIPS asociados.', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VCarteraRadicataXPacienteSinComf';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las facturas deben estar registradas como cuenta por cobrar tipo 2 en Portfolio.AccountReceivable.; Debe existir radicación activa (State=1) tanto en cabecera (RadicateInvoiceC) como en detalle (RadicateInvoiceD) para la factura.; El tercero (entidad pagadora) de la cuenta por cobrar debe existir en Common.ThirdParty.', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VCarteraRadicataXPacienteSinComf';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo expone cuentas por cobrar cuyo AccountReceivableType = 2 (facturas de cartera por radicar a entidad).; Solo considera radicados activos: RadicateInvoiceC.State = 1 y RadicateInvoiceD.State = 1.; Los pagos agregados provienen únicamente de PortfolioTransfer con Status = 2 (transferencias confirmadas) y sobre AccountReceivableType = 2.; Las notas agregadas (TotalNotas) suman AdjusmentValue de PortfolioNoteAccountReceivableAdvance solo para AccountReceivableType = 2.; ValorBrutoFactura se calcula como InvoiceValueEntity + InvoiceValuePacient (parte entidad + parte paciente).; Si no hay notas, pagos o póliza FUR asociados, se devuelven 0 o cadena vacía en lugar de NULL.; El cruce paciente/póliza es opcional (LEFT JOIN), por lo que la factura aparece aunque no exista paciente en INPACIENT ni FURIPS asociado al ingreso.; La vista excluye comprobantes/comfirmaciones (sufijo ''SinComf'' del nombre) al no aplicar filtro de confirmación de comprobante adicional sobre el radicado.', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VCarteraRadicataXPacienteSinComf';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cartera radicada por paciente; Factura radicada; Cuenta por cobrar; Tercero/Entidad pagadora; Notas de cartera (crédito/débito y ajustes); Pagos / Traslados de cartera; Cuota de recuperación (copago paciente); Póliza FURIPS (accidente de tránsito); Tipo de identificación del paciente; Saldo de factura', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VCarteraRadicataXPacienteSinComf';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (resultset): Devuelve una fila por factura radicada con AccountReceivableType=2 y radicado activo (State=1 en cabecera y detalle), agregando pagos confirmados (PortfolioTransfer.Status=2) y notas de cartera del mismo tipo.', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VCarteraRadicataXPacienteSinComf';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si IPTIPODOC del paciente (''1''..''7'') → traduce el código a etiqueta legible (Cédula de Ciudadanía, Cédula de Extranjería, Tarjeta de Identidad, Registro Civil, Pasaporte, Adulto Sin Identificación, Menor Sin Identificación) else cadena vacía', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VCarteraRadicataXPacienteSinComf';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Portfolio.AccountReceivable; Common.ThirdParty; Portfolio.RadicateInvoiceD; Portfolio.RadicateInvoiceC; Portfolio.PortfolioTransfer; Portfolio.PortfolioTransferDetail; Portfolio.PortfolioNote; Portfolio.PortfolioNoteAccountReceivableAdvance; dbo.INPACIENT; dbo.ADFURIPSU', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VCarteraRadicataXPacienteSinComf';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VCarteraRadicataXPacienteSinComf';
GO
