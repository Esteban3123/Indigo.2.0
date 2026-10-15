

CREATE   VIEW [Portfolio].[Onco_Cartera_X_Edades]
AS
SELECT        Sede, NroFactura, cuenta, TipoCxC, TipoDocumento, FechaIngreso, Nit, GrupoAtencion, GrupAtención, FechaFactura, FechaVencimiento, FechaRadicado, 
                         Radicado, FechaVencimientoR, ValorFactura, SaldoTotal, COALESCE (SinRadicar1301, 0) SinRadicar1301, COALESCE (Radicada1302, 0) Radicada1302, 
                         COALESCE (Glosada1303, 0) Glosada1303, COALESCE (PJuridico1304, 0) PJuridico1304, COALESCE (Conciliada1305, 0) Conciliada1305, SaldoInicial, 
                         EdadFactura, EdadRadicado, EstadoRadicado, Entidad, TipoPersona, Identificacion, Paciente, FechaEgreso, EstadoFactura, Categoria, Usuario
FROM            (SELECT        uo.UnitName AS 'Sede', C.InvoiceNumber AS 'NroFactura', cuentas.Number AS 'cuenta', 
                                                    CASE C.AccountReceivableType WHEN '1' THEN 'FacturacionBasica' WHEN '2' THEN 'FacturacionLey100' WHEN '3' THEN 'Impuestos' WHEN '4'
                                                     THEN 'Pagarés' WHEN '5' THEN 'AcuerdosdePago' WHEN '6' THEN 'DocumentoPagoCuotaModeradora' WHEN '7' THEN 'FacturaProducto' END AS
                                                     'TipoCxC', 
                                                    CASE F.DocumentType WHEN '1' THEN 'Factura EAPB con Contrato' WHEN '2' THEN 'Factura EAPB Sin Contrato' WHEN '3' THEN 'Factura Particular'
                                                     WHEN '4' THEN 'Factura Capita' WHEN '5' THEN 'Control Capitacion' WHEN '6' THEN 'Factura Basica' WHEN '7' THEN 'Factura Venta Productos'
                                                     ELSE 'SaldoInicial' END AS 'TipoDocumento', I.IFECHAING AS 'FechaIngreso', TE.Nit AS 'Nit', G.Code AS 'GrupoAtencion', 
                                                    G.Name AS GrupAtención, (C.AccountReceivableDate) AS 'FechaFactura', C.ExpiredDate AS 'FechaVencimiento', (RC.ConfirmDate) 
                                                    AS 'FechaRadicado', (RD.RadicatedNumber) AS 'Radicado', (DATEADD(m, 2, ((RC.ConfirmDate)))) AS 'FechaVencimientoR', 
                                                    C.Value AS 'ValorFactura', C.Balance AS 'SaldoTotal', T .Name AS 'descri', D .Balance, 
                                                    CASE WHEN c.OpeningBalance = 'True' THEN 'SI' ELSE 'NO' END AS 'SaldoInicial', CASE WHEN CAST(GETDATE() - C.ExpiredDate AS INT) 
                                                    < 1 THEN '1. Sin Vencer' WHEN CAST(GETDATE() - C.ExpiredDate AS INT) > 0 AND CAST(GETDATE() - C.ExpiredDate AS INT) 
                                                    < 31 THEN '2. De 1 a 30 Dias' WHEN CAST(GETDATE() - C.ExpiredDate AS INT) > 30 AND CAST(GETDATE() - C.ExpiredDate AS INT) 
                                                    < 61 THEN '3. De 31 a 60 Dias' WHEN CAST(GETDATE() - C.ExpiredDate AS INT) > 60 AND CAST(GETDATE() - C.ExpiredDate AS INT) 
                                                    < 91 THEN '4. De 61 a 90 Dias' WHEN CAST(GETDATE() - C.ExpiredDate AS INT) > 90 AND CAST(GETDATE() - C.ExpiredDate AS INT) 
                                                    < 121 THEN '5. De 91 a 120 Dias' WHEN CAST(GETDATE() - C.ExpiredDate AS INT) > 120 AND CAST(GETDATE() - C.ExpiredDate AS INT) 
                                                    < 181 THEN '6. De 121 a 180 Dias' WHEN CAST(GETDATE() - C.ExpiredDate AS INT) > 180 AND CAST(GETDATE() - C.ExpiredDate AS INT) 
                                                    < 361 THEN '7. De 181 a 360 Dias' WHEN CAST(GETDATE() - C.ExpiredDate AS INT) > 360 THEN 'Mayor a 360 Dias' END AS 'EdadFactura', 
                                                    CASE WHEN CAST(GETDATE() - RC.ConfirmDate AS INT) < 1 THEN '1. Sin Vencer' WHEN CAST(GETDATE() - RC.ConfirmDate AS INT) > 0 AND 
                                                    CAST(GETDATE() - RC.ConfirmDate AS INT) < 31 THEN '2. De 1 a 30 Dias' WHEN CAST(GETDATE() - RC.ConfirmDate AS INT) > 30 AND 
                                                    CAST(GETDATE() - RC.ConfirmDate AS INT) < 61 THEN '3. De 31 a 60 Dias' WHEN CAST(GETDATE() - RC.ConfirmDate AS INT) > 60 AND 
                                                    CAST(GETDATE() - RC.ConfirmDate AS INT) < 91 THEN '4. De 61 a 90 Dias' WHEN CAST(GETDATE() - RC.ConfirmDate AS INT) > 90 AND 
                                                    CAST(GETDATE() - RC.ConfirmDate AS INT) < 121 THEN '5. De 91 a 120 Dias' WHEN CAST(GETDATE() - RC.ConfirmDate AS INT) > 120 AND 
                                                    CAST(GETDATE() - RC.ConfirmDate AS INT) < 181 THEN '6. De 121 a 180 Dias' WHEN CAST(GETDATE() - RC.ConfirmDate AS INT) > 180 AND 
                                                    CAST(GETDATE() - RC.ConfirmDate AS INT) < 361 THEN '7. De 181 a 360 Dias' WHEN CAST(GETDATE() - RC.ConfirmDate AS INT) 
                                                    > 360 THEN 'Mayor a 360 Dias' END AS 'EdadRadicado', 
                                                    CASE WHEN RC.State = '1' THEN 'SinConfirmar' WHEN RC.State = 2 THEN 'Confirmado' WHEN RC.State = '' THEN 'P' END AS 'EstadoRadicado', 
                                                    te.Name AS 'Entidad', CASE TE.PersonType WHEN '1' THEN 'Naturales' WHEN '2' THEN 'Juridicas' END AS 'TipoPersona', 
                                                    F.PatientCode AS 'Identificacion', p.IPNOMCOMP AS 'Paciente', i.FECREGCRE AS 'FechaEgreso', 
                                                    CASE WHEN C.PortfolioStatus = '1' THEN 'SINRADICAR' WHEN C.PortfolioStatus = '2' THEN 'RADICADA SIN CONFIRMAR' WHEN C.PortfolioStatus
                                                     = '3' THEN 'RADICADA ENTIDAD' WHEN C.PortfolioStatus = '7' THEN 'CERTIFICADA_PARCIAL' WHEN C.PortfolioStatus = '8' THEN 'CERTIFICADA_TOTAL'
                                                     WHEN C.PortfolioStatus = '14' THEN 'DEVOLUCION_FACTURA' WHEN C.PortfolioStatus = '15' THEN 'TRASLADO_COBRO_JURIDICO' END AS 'EstadoFactura',
                                                     categ.Name AS [Categoria],     per.Fullname AS Usuario
                          FROM            Portfolio.AccountReceivable AS C WITH (nolock) INNER JOIN
 
 
                                                    Portfolio.AccountReceivableAccounting AS D WITH (nolock) ON C.Id = D .AccountReceivableId INNER JOIN
                                                    GeneralLedger.MainAccounts AS T WITH (nolock) ON D .MainAccountId = T .Id INNER JOIN
                                                    Portfolio.AccountReceivableShare AS S WITH (nolock) ON C.Id = S.AccountReceivableId INNER JOIN
                                                    Common.ThirdParty AS TE WITH (nolock) ON C.ThirdPartyId = TE.Id LEFT OUTER JOIN
    Billing.Invoice AS F WITH (nolock) ON C.InvoiceNumber = F.InvoiceNumber left outer join

                                               
                                                    Portfolio.RadicateInvoiceD AS RD WITH (nolock) ON C.InvoiceNumber = RD.InvoiceNumber AND RD.State <> '4' LEFT OUTER JOIN
                                                    Portfolio.RadicateInvoiceC AS RC WITH (nolock) ON RD.RadicateInvoiceCId = RC.Id AND RC.state <> '4' LEFT OUTER JOIN
                                                    dbo.ADINGRESO AS I WITH (nolock) ON F.AdmissionNumber = I.NUMINGRES LEFT OUTER JOIN
                                                    dbo.INPACIENT AS P WITH (nolock) ON P.ipcodpaci = f.PatientCode LEFT OUTER JOIN
                                                    Contract.CareGroup AS G WITH (nolock) ON F.CareGroupId = G.Id LEFT OUTER JOIN
                                                    cONTRACT.ContractAccountingStructure AS SC WITH (nolock) ON sc.id = G.ContractAccountingStructureId LEFT OUTER JOIN
                                                    Common.OperatingUnit AS uo WITH (nolock) ON uo.Id = C.OperatingUnitId LEFT OUTER JOIN
                                                    GeneralLedger.MainAccounts AS cuentas ON cuentas.id = sc.AccountWithoutRadicateId LEFT OUTER JOIN
                                                    Billing.InvoiceCategories AS categ ON categ.id = F.InvoiceCategoryId left outer join
Security.[User] AS u ON u.UserCode = F.InvoicedUser left outer join
                         Security.Person AS per ON per.Id = u.IdPerson 



                          WHERE        C.Balance > 0 AND C.AccountReceivableType NOT IN ('6') AND C.status <> 3) source PIVOT (SUM(SOURCE.Balance) FOR 
                         source.descri IN (SinRadicar1301, Radicada1302, Glosada1303, PJuridico1304, Conciliada1305)) AS PIVOTABLE