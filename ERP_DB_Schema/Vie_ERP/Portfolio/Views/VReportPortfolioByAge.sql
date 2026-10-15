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