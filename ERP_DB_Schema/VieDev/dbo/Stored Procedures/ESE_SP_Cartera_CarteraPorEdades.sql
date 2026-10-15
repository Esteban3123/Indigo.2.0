CREATE PROCEDURE [dbo].[ESE_SP_Cartera_CarteraPorEdades]
AS
     SELECT NroFactura, 
            cuenta, 
            NombreCuenta, 
            TipoCxC, 
            TipoDocumento, 
            CONVERT(DATE, FechaIngreso) FechaIngreso, 
            Nit, 
            GrupoAtencion, 
            GrupAtención, 
            CONVERT(DATE, FechaFactura, 103) FechaFactura, 
            CONVERT(DATE, FechaVencimiento, 103) FechaRadicado, 
            Radicado, 
            CONVERT(DATE, FechaVencimientoR, 103) FechaVenc, 
            ValorFactura, 
            SaldoTotal, 
            COALESCE(SinRadicar1319, 0) SinRadicar1319, 
            COALESCE(Radicada1319, 0) Radicada1319, 
            SaldoInicial, 
            EdadFactura, 
            EdadRadicado, 
            EstadoRadicado, 
            Entidad, 
            TipoPersona, 
            Identificacion, 
            Paciente, 
            FechaEgreso, 
            EstadoFactura, 
            Categoria, 
            FechaModificacion, 
            UsuarioModificacion, 
            Contrato, 
            Ingreso
     FROM
     (
         SELECT C.InvoiceNumber AS 'NroFactura', 
                cuentas.Number AS 'cuenta', 
                cuentas.name AS NombreCuenta,
                CASE C.AccountReceivableType
                    WHEN '1'
                    THEN 'FacturacionBasica'
                    WHEN '2'
                    THEN 'FacturacionLey100'
                    WHEN '3'
                    THEN 'Impuestos'
                    WHEN '4'
                    THEN 'Pagarés'
                    WHEN '5'
                    THEN 'AcuerdosdePago'
                    WHEN '6'
                    THEN 'DocumentoPagoCuotaModeradora'
                    WHEN '7'
                    THEN 'FacturaProducto'
                END AS 'TipoCxC',
                CASE F.DocumentType
                    WHEN '1'
                    THEN 'Factura EAPB con Contrato'
                    WHEN '2'
                    THEN 'Factura EAPB Sin Contrato'
                    WHEN '3'
                    THEN 'Factura Particular'
                    WHEN '4'
                    THEN 'Factura Capita'
                    WHEN '5'
                    THEN 'Control Capitacion'
                    WHEN '6'
                    THEN 'Factura Basica'
                    WHEN '7'
                    THEN 'Factura Venta Productos'
                    ELSE 'SaldoInicial'
                END AS 'TipoDocumento', 
                I.IFECHAING AS 'FechaIngreso', 
                TE.Nit AS 'Nit', 
                G.Code AS 'GrupoAtencion', 
                G.Name AS GrupAtención, 
                (C.AccountReceivableDate) AS 'FechaFactura', 
                C.ExpiredDate AS 'FechaVencimiento', 
                (RC.ConfirmDate) AS 'FechaRadicado', 
                (RD.RadicatedNumber) AS 'Radicado', 
                (DATEADD(m, 2, ((RC.ConfirmDate)))) AS 'FechaVencimientoR', 
                C.Value AS 'ValorFactura', 
                C.Balance AS 'SaldoTotal', 
                T.descri AS 'descri', 
                D.Balance,
                CASE
                    WHEN c.OpeningBalance = 'True'
                    THEN 'SI'
                    ELSE 'NO'
                END AS 'SaldoInicial',
                CASE
                    WHEN CAST([Common].[GETDATE]() - C.ExpiredDate AS INT) < 1
                    THEN '1. Sin Vencer'
                    WHEN CAST([Common].[GETDATE]() - C.ExpiredDate AS INT) > 0
                         AND CAST([Common].[GETDATE]() - C.ExpiredDate AS INT) < 31
                    THEN '2. De 1 a 30 Dias'
                    WHEN CAST([Common].[GETDATE]() - C.ExpiredDate AS INT) > 30
                         AND CAST([Common].[GETDATE]() - C.ExpiredDate AS INT) < 61
                    THEN '3. De 31 a 60 Dias'
                    WHEN CAST([Common].[GETDATE]() - C.ExpiredDate AS INT) > 60
                         AND CAST([Common].[GETDATE]() - C.ExpiredDate AS INT) < 91
                    THEN '4. De 61 a 90 Dias'
                    WHEN CAST([Common].[GETDATE]() - C.ExpiredDate AS INT) > 90
                         AND CAST([Common].[GETDATE]() - C.ExpiredDate AS INT) < 121
                    THEN '5. De 91 a 120 Dias'
                    WHEN CAST([Common].[GETDATE]() - C.ExpiredDate AS INT) > 120
                         AND CAST([Common].[GETDATE]() - C.ExpiredDate AS INT) < 181
                    THEN '6. De 121 a 180 Dias'
                    WHEN CAST([Common].[GETDATE]() - C.ExpiredDate AS INT) > 180
                         AND CAST([Common].[GETDATE]() - C.ExpiredDate AS INT) < 361
                    THEN '7. De 181 a 360 Dias'
                    WHEN CAST([Common].[GETDATE]() - C.ExpiredDate AS INT) > 360
                    THEN 'Mayor a 360 Dias'
                END AS 'EdadFactura',
                CASE
                    WHEN CAST([Common].[GETDATE]() - RC.ConfirmDate AS INT) < 1
                    THEN '1. Sin Vencer'
                    WHEN CAST([Common].[GETDATE]() - RC.ConfirmDate AS INT) > 0
                         AND CAST([Common].[GETDATE]() - RC.ConfirmDate AS INT) < 31
                    THEN '2. De 1 a 30 Dias'
                    WHEN CAST([Common].[GETDATE]() - RC.ConfirmDate AS INT) > 30
                         AND CAST([Common].[GETDATE]() - RC.ConfirmDate AS INT) < 61
                    THEN '3. De 31 a 60 Dias'
                    WHEN CAST([Common].[GETDATE]() - RC.ConfirmDate AS INT) > 60
                         AND CAST([Common].[GETDATE]() - RC.ConfirmDate AS INT) < 91
                    THEN '4. De 61 a 90 Dias'
                    WHEN CAST([Common].[GETDATE]() - RC.ConfirmDate AS INT) > 90
                         AND CAST([Common].[GETDATE]() - RC.ConfirmDate AS INT) < 121
                    THEN '5. De 91 a 120 Dias'
                    WHEN CAST([Common].[GETDATE]() - RC.ConfirmDate AS INT) > 120
                         AND CAST([Common].[GETDATE]() - RC.ConfirmDate AS INT) < 181
                    THEN '6. De 121 a 180 Dias'
                    WHEN CAST([Common].[GETDATE]() - RC.ConfirmDate AS INT) > 180
                         AND CAST([Common].[GETDATE]() - RC.ConfirmDate AS INT) < 361
                    THEN '7. De 181 a 360 Dias'
                    WHEN CAST([Common].[GETDATE]() - RC.ConfirmDate AS INT) > 360
                    THEN 'Mayor a 360 Dias'
                END AS 'EdadRadicado',
                CASE
                    WHEN RC.State = '1'
                    THEN 'SinConfirmar'
                    WHEN RC.State = 2
                    THEN 'Confirmado'
                    WHEN RC.State = ''
                    THEN 'P'
                END AS 'EstadoRadicado', 
                te.Name AS 'Entidad',
                CASE TE.PersonType
                    WHEN '1'
                    THEN 'Naturales'
                    WHEN '2'
                    THEN 'Juridicas'
                END AS 'TipoPersona', 
                F.PatientCode AS 'Identificacion', 
                p.IPNOMCOMP AS 'Paciente', 
                i.FECREGCRE AS 'FechaEgreso',
                CASE
                    WHEN C.PortfolioStatus = '1'
                    THEN 'SINRADICAR'
                    WHEN C.PortfolioStatus = '2'
                    THEN 'RADICADA SIN CONFIRMAR'
                    WHEN C.PortfolioStatus = '3'
                    THEN 'RADICADA ENTIDAD'
                    WHEN C.PortfolioStatus = '7'
                    THEN 'CERTIFICADA_PARCIAL'
                    WHEN C.PortfolioStatus = '8'
                    THEN 'CERTIFICADA_TOTAL'
                    WHEN C.PortfolioStatus = '14'
                    THEN 'DEVOLUCION_FACTURA'
                    WHEN C.PortfolioStatus = '15'
                    THEN 'TRASLADO_COBRO_JURIDICO'
                END AS 'EstadoFactura', 
                categ.Name AS [Categoria], 
                RC.ModificationDate AS 'FechaModificacion', 
                RC.ModificationUser AS 'UsuarioModificacion', 
                RTRIM(cont.Code) + ' - ' + RTRIM(cont.ContractName) AS Contrato, 
                f.admissionnumber AS Ingreso
         FROM Portfolio.AccountReceivable AS C WITH(NOLOCK)
              INNER JOIN Portfolio.AccountReceivableAccounting AS D WITH(NOLOCK) ON C.Id = D.AccountReceivableId
              INNER JOIN dbo.TemCuenta AS T WITH(NOLOCK) ON D.MainAccountId = T.Id
              INNER JOIN Portfolio.AccountReceivableShare AS S WITH(NOLOCK) ON C.Id = S.AccountReceivableId
              INNER JOIN Common.ThirdParty AS TE WITH(NOLOCK) ON C.ThirdPartyId = TE.Id
              LEFT OUTER JOIN Billing.Invoice AS F WITH(NOLOCK) ON C.InvoiceNumber = F.InvoiceNumber
              LEFT OUTER JOIN Portfolio.RadicateInvoiceD AS RD WITH(NOLOCK) ON C.InvoiceNumber = RD.InvoiceNumber
                                                                               AND RD.State <> '4'
              LEFT OUTER JOIN Portfolio.RadicateInvoiceC AS RC WITH(NOLOCK) ON RD.RadicateInvoiceCId = RC.Id
                                                                               AND RC.state <> '4'
              LEFT OUTER JOIN.ADINGRESO AS I WITH(NOLOCK) ON F.AdmissionNumber = I.NUMINGRES
              LEFT OUTER JOIN.INPACIENT AS P WITH(NOLOCK) ON P.ipcodpaci = f.PatientCode
              LEFT OUTER JOIN Contract.CareGroup AS G WITH(NOLOCK) ON F.CareGroupId = G.Id
              LEFT OUTER JOIN contract.contract AS cont WITH(NOLOCK) ON cont.id = g.ContractId
              LEFT OUTER JOIN cONTRACT.ContractAccountingStructure AS SC WITH(NOLOCK) ON sc.id = G.ContractAccountingStructureId
              LEFT OUTER JOIN Common.OperatingUnit AS uo WITH(NOLOCK) ON uo.Id = C.OperatingUnitId
              LEFT OUTER JOIN GeneralLedger.MainAccounts AS cuentas WITH(NOLOCK) ON cuentas.id = sc.AccountWithoutRadicateId
              LEFT OUTER JOIN Billing.InvoiceCategories AS categ WITH(NOLOCK) ON categ.id = F.InvoiceCategoryId
         WHERE C.Balance > 0
               AND C.AccountReceivableType NOT IN('6')
              AND C.STATUS <> 3
     ) source PIVOT(SUM(SOURCE.Balance) FOR source.descri IN(SinRadicar1319, 
                                                             Radicada1319)) AS PIVOTABLE;