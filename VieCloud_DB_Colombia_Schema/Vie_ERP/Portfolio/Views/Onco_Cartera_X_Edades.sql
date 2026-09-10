

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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cartera por edades del módulo de Oncología: consolida el estado de cobro de las facturas emitidas a entidades pagadoras (EPS, aseguradoras, particulares) clasificando cada documento según el tiempo transcurrido desde su vencimiento en rangos de antigüedad (sin vencer, 1-30 días, 31-60 días, hasta más de 360 días), tanto por fecha de vencimiento de la factura como por fecha de radicación ante la entidad. Integra información de cuentas por cobrar, cuotas de pago, facturas, radicados de cobro, terceros pagadores, ingresos de pacientes y datos del paciente para ofrecer una visión completa del recaudo pendiente. Permite a los gestores de cartera y dirección financiera identificar facturas sin radicar, radicadas, glosadas, en proceso jurídico o conciliadas, con datos del paciente, sede, entidad, NIT, tipo de documento y estado del radicado, facilitando el seguimiento y gestión de la cartera oncológica por antigüedad y estado.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'VIEW', @level1name = N'Onco_Cartera_X_Edades';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'VIEW', @level1name = N'Onco_Cartera_X_Edades';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Reporta la cartera oncológica vigente clasificada por edades de vencimiento de la factura y del radicado, pivotando saldos contables según el estado del proceso de radicación (sin radicar, radicada, glosada, jurídico, conciliada).', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'Onco_Cartera_X_Edades';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La cuenta por cobrar debe tener saldo pendiente (C.Balance > 0).; Se excluyen cuentas por cobrar tipo ''6'' (DocumentoPagoCuotaModeradora).; Se excluyen cuentas por cobrar con status = 3 (anuladas/canceladas).; Los detalles de radicación (RadicateInvoiceD) y su encabezado (RadicateInvoiceC) deben tener State distinto de ''4'' para ser considerados.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'Onco_Cartera_X_Edades';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La fecha de vencimiento del radicado se calcula siempre como ConfirmDate + 2 meses (DATEADD(m,2,RC.ConfirmDate)).; Solo se incluyen cuentas por cobrar activas con saldo positivo y excluyendo cuotas moderadoras y status 3.; Los saldos por estado de radicación se obtienen pivotando D.Balance contra el nombre de la cuenta contable principal (T.Name) en las cinco categorías predefinidas (1301-1305).; Las edades de factura y de radicado usan exactamente los mismos cortes de días (1, 30, 60, 90, 120, 180, 360).', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'Onco_Cartera_X_Edades';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cartera; Cuenta por cobrar; Factura; Radicación de factura; Glosa; Cobro jurídico; Conciliación; Edad de cartera; Saldo inicial; EAPB; Capitación; Grupo de atención; Paciente; Ingreso/Admisión; Sede/Unidad operativa; Tercero (Natural/Jurídico)', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'Onco_Cartera_X_Edades';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Portfolio.Onco_Cartera_X_Edades: Devuelve un conjunto pivotado donde cada saldo contable (D.Balance) se asigna a una columna según el nombre de la cuenta principal: SinRadicar1301, Radicada1302, Glosada1303, PJuridico1304 o Conciliada1305; los nulos resultantes se reemplazan por 0 vía COALESCE.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'Onco_Cartera_X_Edades';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si C.AccountReceivableType ∈ {1..7} → Mapea a etiquetas de TipoCxC: 1=FacturacionBasica, 2=FacturacionLey100, 3=Impuestos, 4=Pagarés, 5=AcuerdosdePago, 6=DocumentoPagoCuotaModeradora, 7=FacturaProducto; si F.DocumentType ∈ {1..7} → Mapea TipoDocumento: 1=Factura EAPB con Contrato, 2=Factura EAPB Sin Contrato, 3=Factura Particular, 4=Factura Capita, 5=Control Capitacion, 6=Factura Basica, 7=Factura Venta Productos else ''SaldoInicial'' cuando DocumentType es NULL u otro valor; si Diferencia en días entre GETDATE() y C.ExpiredDate → Clasifica EdadFactura en rangos: <1=''1. Sin Vencer''; 1-30; 31-60; 61-90; 91-120; 121-180; 181-360; >360=''Mayor a 360 Dias''; si Diferencia en días entre GETDATE() y RC.ConfirmDate → Clasifica EdadRadicado con los mismos rangos que EdadFactura; si RC.State → 1=SinConfirmar, 2=Confirmado, ''''=P; si C.PortfolioStatus → 1=SINRADICAR, 2=RADICADA SIN CONFIRMAR, 3=RADICADA ENTIDAD, 7=CERTIFICADA_PARCIAL, 8=CERTIFICADA_TOTAL, 14=DEVOLUCION_FACTURA, 15=TRASLADO_COBRO_JURIDICO; si TE.PersonType → 1=Naturales, 2=Juridicas; si c.OpeningBalance = ''True'' → Marca SaldoInicial=''SI'' else ''NO''', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'Onco_Cartera_X_Edades';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Portfolio.AccountReceivable; Portfolio.AccountReceivableAccounting; GeneralLedger.MainAccounts; Portfolio.AccountReceivableShare; Common.ThirdParty; Billing.Invoice; Portfolio.RadicateInvoiceD; Portfolio.RadicateInvoiceC; dbo.ADINGRESO; dbo.INPACIENT; Contract.CareGroup; Contract.ContractAccountingStructure; Common.OperatingUnit; Billing.InvoiceCategories; Security.User; Security.Person', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'Onco_Cartera_X_Edades';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'Onco_Cartera_X_Edades';
GO
