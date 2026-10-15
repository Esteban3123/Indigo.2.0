CREATE VIEW [Portfolio].[Onco_FacturacionRadicada]
AS
     SELECT uo.UnitName AS Sede, 
            PC.InvoiceNumber AS Nro_Factura,
            CASE AccountReceivableType
                WHEN '1'
                THEN 'Facturación Básica'
                WHEN '2'
                THEN 'Facturación Ley 100'
                WHEN '3'
                THEN 'Impuestos'
                WHEN '4'
                THEN 'Pagarés'
                WHEN '5'
                THEN 'Acuerdos de Pago'
                WHEN '6'
                THEN 'Documento de Pago a Cuota Moderadora'
                WHEN '7'
                THEN 'Factura de Producto'
            END AS [Tipo CxC], 
            ing.NUMINGRES AS Ingreso, 
            ea.HealthEntityCode AS Entidad_Administradora, 
            t.Nit, 
            t.DigitVerification AS [Dígito verificación], 
            t.Name AS Entidad, 
            f.PatientCode AS Identificación, 
            p.IPNOMCOMP AS NombrePaciente, 
            YEAR(GETDATE()) - YEAR(p.IPFECNACI) AS Edad, 
            ing.IFECHAING AS FechaIngreso, 
            ing.FECHEGRESO AS FechaEgreso, 
            ga.Code + ' - ' + ga.Name AS [Grupo Atención], 
            PC.AccountReceivableDate AS [Fecha Factura], 
            f.InvoiceExpirationDate AS [Fecha vencimiento], 
            PC.Value AS Vr_Factura, 
            sxcc.Balance AS [Saldo sin radicar], 
            PC.Balance AS [Saldo Total],
            CASE PC.PortfolioStatus
                WHEN '1'
                THEN 'SIN RADICAR'
                WHEN '2'
                THEN 'RADICADA SIN CONFIRMAR'
                WHEN '3'
                THEN 'RADICADA ENTIDAD'
                WHEN '7'
                THEN 'CERTIFICADA_PARCIAL'
                WHEN '8'
                THEN 'CERTIFICADA_TOTAL'
                WHEN '14'
                THEN 'DEVOLUCION_FACTURA'
                WHEN '15'
                THEN 'TRASLADO COBRO JURÍDICO CONFIRMADO'
            END AS Estado_Factura_Cartera, 
            cr.RadicatedConsecutive AS [Nro Radicado], 
            cr.ConfirmDate AS [Fecha confirmación], 
            categ.name AS Categoria, 
            PC.Observations AS [Origen Factura], 
            uc.NOMUSUARI AS Usuario
     FROM Portfolio.AccountReceivable AS PC WITH(NOLOCK)
          LEFT OUTER JOIN Billing.Invoice AS f ON f.InvoiceNumber = PC.InvoiceNumber
                                                  AND f.STATUS <> '2'
          LEFT OUTER JOIN Contract.HealthAdministrator AS ea ON ea.Id = f.HealthAdministratorId
          LEFT OUTER JOIN Portfolio.RadicateInvoiceD AS dr ON dr.InvoiceNumber = PC.InvoiceNumber
                                                              AND dr.State <> '4'
          LEFT OUTER JOIN Portfolio.RadicateInvoiceC AS cr ON cr.Id = dr.RadicateInvoiceCId
                                                              AND cr.State <> '4'
          LEFT OUTER JOIN dbo.INPACIENT AS p ON p.IPCODPACI = f.PatientCode
          LEFT OUTER JOIN dbo.ADINGRESO AS ing ON CAST(ing.NUMINGRES AS INT) = f.AdmissionNumber
          LEFT OUTER JOIN Contract.CareGroup AS ga ON ga.Id = f.CareGroupId
          LEFT OUTER JOIN Common.ThirdParty AS t ON t.Id = PC.ThirdPartyId
          LEFT OUTER JOIN Common.OperatingUnit AS uo ON uo.Id = PC.OperatingUnitId
          LEFT OUTER JOIN dbo.SEGusuaru AS uc ON uc.CODUSUARI = PC.CreationUser
          LEFT OUTER JOIN GeneralLedger.MainAccounts AS cuca ON cuca.Id = PC.AccountRadicateId
          LEFT OUTER JOIN Portfolio.AccountReceivableAccounting AS rccsr ON rccsr.AccountReceivableId = PC.MainAccountWithoutFilingId
          LEFT OUTER JOIN Portfolio.AccountReceivableAccounting AS sxcc ON sxcc.AccountReceivableId = PC.Id
          LEFT OUTER JOIN Billing.InvoiceCategories AS categ ON categ.id = F.InvoiceCategoryId
     WHERE(PC.Balance > '0')
          AND (sxcc.Balance > '0')
          AND (PC.SellerId IS NULL)
          AND (PC.STATUS <> '3')
          AND (PC.PortfolioStatus IN('3')); ---AND (PC.AccountReceivableType <> '6')  