CREATE VIEW [Portfolio].[VReportPortfolioByAge]
AS
     SELECT ar.InvoiceNumber AS DocumentCode, 
            tpar.Nit AS ThirdPartyNit, 
            tpar.Name AS ThirdPartyName, 
            par.IdentificationType, 
            tpar.Nit AS CustomerNit, 
            tps.Nit AS SellerNit,
            CASE ar.AccountReceivableType
                WHEN 1
                THEN ar.AccountReceivableDate
                ELSE ri.DocumentDate
            END DocumentDate, 
            DATEADD(DAY, ar.Term,
                            CASE ar.AccountReceivableType
                                WHEN 1
                                THEN ar.AccountReceivableDate
                                ELSE ri.ConfirmDate
                            END) AS expiredDate, 
            ISNULL(ri.ConfirmDate, ar.AccountReceivableDate) AS DocumentDateCalculated, 
            ISNULL(ri.ConfirmDate,
                      CASE ar.AccountReceivableType
                          WHEN 1
                          THEN ar.AccountReceivableDate
                          ELSE GETDATE()
                      END) AS expiredDateCalculated, 
            DATEDIFF(DAY, ISNULL(ri.ConfirmDate,
                                    CASE ar.AccountReceivableType
                                        WHEN 1
                                        THEN ar.AccountReceivableDate
                                        ELSE GETDATE()
                                    END), GETDATE()) AS Diferencia, 
            ar.Balance, 
            ar.AccountReceivableType AS DocumentType, 
            ar.PortfolioStatus, 
            ar.NumberShares, 
            ar.Term, 
            ar.OpeningBalance, 
            1 AS AdvanceOrAccountReceivable, 
            cg.EntityType AS Regimen, 
            cg.Code AS CodeCareGroup, 
            cg.Name AS NameCareGroup, 
            ma.Number AS MainAccountNumber, 
            ma.Name AS MainAccountName, 
            mar.Number AS NumberAccount, 
            ri.RadicatedConsecutive,
            CASE ar.AccountReceivableType
                WHEN 1
                THEN ar.AccountReceivableDate
                ELSE ri.ConfirmDate
            END AS RadicatedDate, 
            CAST(ri.CreationUser AS VARCHAR(20)) AS RadicatedUser,
            CASE mar.Number
                WHEN '14090103'
                THEN 'Contributivo'
                WHEN '14090304'
                THEN 'Subsidiado'
                WHEN '14090401'
                THEN 'Servicio IPS Privada'
                WHEN '14090501'
                THEN 'Medicina Prepagada'
                WHEN '14090601'
                THEN 'Compañias Aseguradoras'
                WHEN '14090701'
                THEN 'Particulares'
                WHEN '14090901'
                THEN 'Servicio IPS Publicas'
                WHEN '14091004'
                THEN 'Regimen Especial'
                WHEN '14091102'
                THEN 'Vinculados - Departamentos'
                WHEN '14091103'
                THEN 'Vinculados Municipios'
                WHEN '14091201'
                THEN 'Arl Riesgos Profesionales'
                WHEN '14091403'
                THEN 'Accidentes de Transito'
                WHEN '14090201'
                THEN 'Otras Cuentas X Cobrar'
                ELSE ''
            END AS RegimenCalculated, 
            ar.AccountReceivableDate AS InvoiceDate, 
            ri.StateC, 
            ri.StateD, 
            ar.Id AS AccountReceivableId, 
            ar.[Value], 
            shared.CreditValue, 
            shared.DebitValue, 
            shared.PaymentValue, 
            ic.Code + ' - ' + ic.[Name] AS Category, 
            gpg.Id AS GlosaPortfolioGlosadaId, 
            ISNULL(gpg.ValueGlosado, 0) AS ValueGlosado, 
            ISNULL(gpg.BalanceGlosa, 0) AS BalanceGlosa, 
            ISNULL(gpg.BalanceInvoice, 0) AS BalanceInvoice, 
            ar.DeteriorationBalance, 
            ISNULL(pp.Expectative, 0) Expectative, 
            ct.Code ContractCode, 
            ct.ContractName ContractName,
            CASE gpg.state
                WHEN 1
                THEN 'Pendiente Confirmar Glosa'
                WHEN 2
                THEN 'Pendiente Evaluacion Glosa'
                WHEN 3
                THEN 'Pendiente envio de oficio'
                WHEN 4
                THEN 'Pendiente confirmar reiteracion'
                WHEN 5
                THEN 'Pendiente evaluacion reitreacion'
                WHEN 6
                THEN 'Pendiente conciliacion'
                WHEN 7
                THEN 'Pendiente de confirmar Conciliacion'
                WHEN 8
                THEN 'Conciliada'
                WHEN 9
                THEN 'Conciliada Parcialmente'
                WHEN 11
                THEN 'Glosa con Respuesta'
                WHEN 12
                THEN 'Reiteracion con respuesta'
                WHEN 13
                THEN 'Pendiente confirmar pago parcial'
                WHEN 14
                THEN 'Confirmado pago parcial'
                WHEN 15
                THEN 'Cobro juridico'
                ELSE 'No esta Glosada'
            END AS GlosaState, 
            c.NOMCENATE CenterAttention
     FROM Portfolio.AccountReceivable AS ar WITH(NOLOCK)
          INNER JOIN
     (
         SELECT AccountReceivableId, 
                SUM(DebitValue) AS DebitValue, 
                SUM(CreditValue) AS CreditValue, 
                SUM(TransferValue + PaymentValue + CrossingValue) AS PaymentValue
         FROM Portfolio.AccountReceivableShare
         GROUP BY AccountReceivableId
     ) AS shared ON shared.AccountReceivableId = ar.Id
          INNER JOIN Common.ThirdParty AS tpar WITH(NOLOCK) ON tpar.Id = ar.ThirdPartyId
          INNER JOIN Common.Person AS par WITH(NOLOCK) ON par.Id = tpar.PersonId
          LEFT JOIN Glosas.GlosaPortfolioGlosada gpg WITH(NOLOCK) ON ar.InvoiceNumber = gpg.InvoiceNumber
          LEFT JOIN Common.Seller AS s WITH(NOLOCK) ON s.Id = ar.SellerId
          LEFT JOIN Common.ThirdParty AS tps WITH(NOLOCK) ON tps.Id = s.ThirdPartyId
          LEFT JOIN Billing.Invoice AS i WITH(NOLOCK) ON i.Id = ar.InvoiceId
          LEFT JOIN Billing.InvoiceCategories AS ic ON ic.Id = ar.InvoiceCategoryId
          LEFT JOIN Contract.CareGroup AS cg WITH(NOLOCK) ON cg.Id = i.CareGroupId
          LEFT JOIN Contract.Contract ct ON ISNULL(i.ContractId, cg.ContractId) = ct.Id
          LEFT JOIN GeneralLedger.MainAccounts AS ma WITH(NOLOCK) ON ma.Id = CASE ar.PortfolioStatus
                                                                                 WHEN 1
                                                                                 THEN ar.AccountWithoutRadicateId
                                                                                 WHEN 2
                                                                                 THEN ar.AccountWithoutRadicateId
                                                                                 WHEN 15
                                                                                 THEN ar.AccountHardCollectionId
                                                                                 ELSE ISNULL(ar.AccountRadicateId, ar.AccountWithoutRadicateId)
                                                                             END
          LEFT JOIN GeneralLedger.MainAccounts AS mar WITH(NOLOCK) ON mar.Id = ar.AccountWithoutRadicateId
          LEFT JOIN
     (
         SELECT Ric.DocumentDate, 
                Ric.ConfirmDate, 
                Ric.RadicatedConsecutive, 
                Ric.RadicatedDate, 
                RID.InvoiceNumber, 
                Ric.CreationUser, 
                RID.InvoiceDate, 
                Ric.[State] AS StateC, 
                RID.[State] AS StateD
         FROM Portfolio.RadicateInvoiceD AS RID WITH(NOLOCK)
              JOIN Portfolio.RadicateInvoiceC AS RIC WITH(NOLOCK) ON Ric.Id = RID.RadicateInvoiceCId
                                                                     AND ISNULL(RID.State, 0) = 2
                                                                     AND Ric.State = 2
              JOIN
         (
             SELECT RID.InvoiceNumber, 
                    MIN(RIC.Id) Id
             FROM Portfolio.RadicateInvoiceC AS RIC WITH(NOLOCK)
                  JOIN Portfolio.RadicateInvoiceD AS RID WITH(NOLOCK) ON RIC.Id = RID.RadicateInvoiceCId
                                                                         AND ISNULL(RID.State, 0) = 2
                                                                         AND Ric.State = 2
             GROUP BY RID.InvoiceNumber
         ) WithoutDuplicates ON RID.InvoiceNumber = WithoutDuplicates.InvoiceNumber
                                AND RIC.Id = WithoutDuplicates.Id
     ) AS ri ON ri.InvoiceNumber = ar.InvoiceNumber
          LEFT JOIN
     (
         SELECT ppd.AccountReceivableId, 
                ppd.Value, 
                ppd.Expectative
         FROM Portfolio.PortfolioProvision pp
              JOIN Portfolio.PortfolioProvisionDetail ppd ON pp.Id = ppd.PortfolioProvisionId
              JOIN
         (
             SELECT ppd.AccountReceivableId, 
                    MAX(pp.Id) Id
             FROM Portfolio.PortfolioProvision pp
                  JOIN Portfolio.PortfolioProvisionDetail ppd ON pp.Id = ppd.PortfolioProvisionId
             WHERE pp.DocumentType = 2
                   AND pp.STATUS = 2
             GROUP BY ppd.AccountReceivableId
         ) pm ON ppd.AccountReceivableId = pm.AccountReceivableId
                 AND pp.Id = pm.Id
         WHERE pp.DocumentType = 2
               AND pp.STATUS = 2
     ) pp ON ar.Id = pp.AccountReceivableId
          LEFT JOIN dbo.ADINGRESO a ON i.AdmissionNumber = a.NUMINGRES
          LEFT JOIN dbo.ADCENATEN c ON a.CODCENATE = c.CODCENATE
     WHERE ar.Balance <> 0
           AND ar.STATUS = 2
           AND ar.AccountReceivableType IN(1, 2);
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de cartera envejecida que consolida el estado financiero de cada cuenta por cobrar (facturas, cuentas de cobro) cruzando información de terceros pagadores (EPS, aseguradoras, particulares, empresas), vendedor o asesor comercial, cuotas de pago, facturas de facturación electrónica y glosas. Calcula la antigüedad de la deuda en días (diferencia entre la fecha de radicación o confirmación y hoy), la fecha de vencimiento según el plazo pactado, el saldo pendiente, valores débito, crédito y pagos acumulados por cuota, y clasifica cada documento por régimen de afiliación (contributivo, subsidiado, particular, ARL, SOAT, etc.) a partir del número de cuenta contable. Incorpora el estado del proceso de glosa (pendiente, conciliada, cobro jurídico, etc.), el saldo glosado, el saldo de la factura en glosa, la provisión por deterioro y la expectativa de recaudo, junto con el contrato y el centro de atención asociados. Está diseñada para reportes de cartera por edades, seguimiento de cobro, gestión de glosas y análisis financiero de cuentas por cobrar del módulo de cartera de la institución de salud.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'VIEW', @level1name = N'VReportPortfolioByAge';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'VIEW', @level1name = N'VReportPortfolioByAge';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Reporte consolidado de cartera por edades que une cada cuenta por cobrar activa con su radicación, glosa, provisión, contrato y centro de atención para calcular días vencidos, saldos y clasificación de régimen.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'VReportPortfolioByAge';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La cuenta por cobrar debe tener saldo distinto de cero (ar.Balance <> 0); La cuenta por cobrar debe estar en estado 2 (ar.STATUS = 2); Solo se incluyen documentos cuyo AccountReceivableType sea 1 o 2; Para considerar la radicación, tanto el detalle (RID.State) como el encabezado (RIC.State) deben estar en estado 2; Para considerar la provisión, PortfolioProvision debe tener DocumentType = 2 y STATUS = 2', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'VReportPortfolioByAge';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reporta cartera viva: saldo no cero, status activo (2) y tipo de cuenta 1 o 2; Para cada factura se elige una única radicación: la de menor RIC.Id por InvoiceNumber con detalle y encabezado en estado 2; Para cada cuenta por cobrar se toma la provisión más reciente (MAX(pp.Id)) con DocumentType=2 y STATUS=2; AdvanceOrAccountReceivable siempre se entrega con valor 1 (cartera, no anticipo); Los días de mora (Diferencia) se calculan respecto a la fecha actual (GETDATE()); Los valores de pago se totalizan como suma de TransferValue + PaymentValue + CrossingValue de las cuotas; Cuando no hay glosa, expectativa o valores glosados, se devuelven ceros (ISNULL)', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'VReportPortfolioByAge';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cartera por edades; Cuenta por cobrar; Radicación de facturas; Glosa; Provisión de cartera; Régimen (Contributivo, Subsidiado, Especial, Vinculados, ARL, SOAT); Contrato de salud; Centro de atención; Plan único de cuentas (PUC); Cobro jurídico; Conciliación de glosas; Vendedor / asesor comercial; Días de mora / vencimiento; Saldo de deterioro; Expectativa de recuperación', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'VReportPortfolioByAge';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Portfolio.VReportPortfolioByAge: Devuelve una fila por cada cuenta por cobrar activa (Balance<>0, STATUS=2, AccountReceivableType IN (1,2)) con sus datos de tercero, radicación, glosa, provisión, contrato y centro de atención.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'VReportPortfolioByAge';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ar.AccountReceivableType = 1 → Usa ar.AccountReceivableDate como DocumentDate y RadicatedDate; el vencimiento se calcula sumando ar.Term a esa fecha else Usa ri.DocumentDate como DocumentDate y ri.ConfirmDate como base de RadicatedDate y vencimiento; si ar.PortfolioStatus = 1 o 2 → La cuenta contable principal (ma) se toma de ar.AccountWithoutRadicateId else Si PortfolioStatus = 15 toma ar.AccountHardCollectionId; en otros casos usa ISNULL(ar.AccountRadicateId, ar.AccountWithoutRadicateId); si mar.Number coincide con uno de los códigos PUC predefinidos (14090103, 14090304, 14090401, 14090501, 14090601, 14090701, 14090901, 14091004, 14091102, 14091103, 14091201, 14091403, 14090201) → Asigna el régimen correspondiente (Contributivo, Subsidiado, Servicio IPS Privada, Medicina Prepagada, Compañías Aseguradoras, Particulares, Servicio IPS Públicas, Régimen Especial, Vinculados, ARL, Accidentes de Tránsito, Otras Cuentas por Cobrar) else RegimenCalculated queda como cadena vacía; si gpg.state entre 1 y 15 → Traduce el estado numérico a la descripción de glosa correspondiente (Pendiente Confirmar Glosa, Conciliada, Cobro jurídico, etc.) else GlosaState = ''No esta Glosada''; si ri.ConfirmDate IS NULL y ar.AccountReceivableType <> 1 → Usa GETDATE() como fecha base para expiredDateCalculated y para el cálculo de Diferencia (días vencidos) else Usa la fecha de confirmación de radicación o la fecha del documento según corresponda', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'VReportPortfolioByAge';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Portfolio.AccountReceivable; Portfolio.AccountReceivableShare; Common.ThirdParty; Common.Person; Glosas.GlosaPortfolioGlosada; Common.Seller; Billing.Invoice; Billing.InvoiceCategories; Contract.CareGroup; Contract.Contract; GeneralLedger.MainAccounts; Portfolio.RadicateInvoiceD; Portfolio.RadicateInvoiceC; Portfolio.PortfolioProvision; Portfolio.PortfolioProvisionDetail; dbo.ADINGRESO; dbo.ADCENATEN', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'VReportPortfolioByAge';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'VReportPortfolioByAge';
GO
