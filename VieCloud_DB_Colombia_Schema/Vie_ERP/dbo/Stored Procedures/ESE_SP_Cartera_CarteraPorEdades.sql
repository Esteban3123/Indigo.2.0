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
GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Procedimiento de cartera por edades que consolida las cuentas por cobrar con saldo pendiente, clasificándolas en tramos de vencimiento (sin vencer, 1-30, 31-60, 61-90, 91-120, 121-180, 181-360 y más de 360 días) tanto por fecha de vencimiento de la factura como por fecha de radicado. Cruza facturas, radicaciones, contratos, grupos de atención, datos del paciente y cuentas contables, e incluye un PIVOT para separar saldos sin radicar de saldos radicados por cuenta 1319, orientado a reportes de gestión de cobro a entidades pagadoras.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Cartera_CarteraPorEdades';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Cartera_CarteraPorEdades';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un reporte de cartera por edades clasificando facturas y radicados según los días vencidos, con saldos pivoteados entre cuentas sin radicar y radicadas (1319).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Cartera_CarteraPorEdades';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existencia de cuentas por cobrar con Balance > 0; Las cuentas por cobrar deben tener tipo distinto de ''6'' (Documento Pago Cuota Moderadora); Las cuentas por cobrar deben tener STATUS distinto de 3; La función [Common].[GETDATE]() debe estar disponible para cálculos de antigüedad', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Cartera_CarteraPorEdades';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan cuentas por cobrar con saldo positivo (Balance>0); Se excluyen las cuentas tipo Documento Pago Cuota Moderadora (tipo 6); Se excluyen cuentas con STATUS=3 (anuladas/canceladas); Las radicaciones consideradas excluyen registros con State=4 tanto en cabecera como detalle; La fecha de vencimiento del radicado se calcula sumando 2 meses a la fecha de confirmación del radicado; Los rangos de antigüedad usan tramos: Sin Vencer, 1-30, 31-60, 61-90, 91-120, 121-180, 181-360 y mayor a 360 días', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Cartera_CarteraPorEdades';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cartera por edades; Cuenta por cobrar; Factura; Radicación de facturas; Tercero / Entidad pagadora; Grupo de atención; Contrato; Paciente; Ingreso/Egreso; Saldo inicial; Cuota moderadora; EAPB; Capitación; Acuerdos de pago; Estado de cartera (certificada, devolución, traslado a cobro jurídico)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Cartera_CarteraPorEdades';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve el listado de cartera filtrando solo cuentas con Balance>0, AccountReceivableType<>''6'' y STATUS<>3, con saldos pivoteados en columnas SinRadicar1319 y Radicada1319', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Cartera_CarteraPorEdades';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si AccountReceivableType de la CxC → Traduce el código numérico a etiqueta de tipo de cartera (FacturacionBasica, FacturacionLey100, Impuestos, Pagarés, AcuerdosdePago, DocumentoPagoCuotaModeradora, FacturaProducto); si DocumentType de la factura → Clasifica el documento (Factura EAPB con/sin Contrato, Particular, Capita, Control Capitación, Básica, Venta Productos) else Si no coincide con valores 1-7 se etiqueta como ''SaldoInicial''; si Días entre GETDATE y ExpiredDate de la factura → Asigna rango de antigüedad EdadFactura: <1 Sin Vencer; 1-30; 31-60; 61-90; 91-120; 121-180; 181-360; >360 Mayor a 360 Días; si Días entre GETDATE y ConfirmDate del radicado → Asigna rango de antigüedad EdadRadicado con los mismos cortes que EdadFactura; si RC.State del radicado → Traduce estado: 1=SinConfirmar, 2=Confirmado, vacío=P; si C.PortfolioStatus → Clasifica estado de la factura en cartera: 1=SINRADICAR, 2=RADICADA SIN CONFIRMAR, 3=RADICADA ENTIDAD, 7=CERTIFICADA_PARCIAL, 8=CERTIFICADA_TOTAL, 14=DEVOLUCION_FACTURA, 15=TRASLADO_COBRO_JURIDICO; si TE.PersonType del tercero → Clasifica como Naturales (1) o Jurídicas (2); si C.OpeningBalance → Marca SaldoInicial=''SI'' cuando es True; ''NO'' en caso contrario', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Cartera_CarteraPorEdades';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Portfolio.AccountReceivable; Portfolio.AccountReceivableAccounting; dbo.TemCuenta; Portfolio.AccountReceivableShare; Common.ThirdParty; Billing.Invoice; Portfolio.RadicateInvoiceD; Portfolio.RadicateInvoiceC; ADINGRESO; INPACIENT; Contract.CareGroup; contract.contract; contract.ContractAccountingStructure; Common.OperatingUnit; GeneralLedger.MainAccounts; Billing.InvoiceCategories', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Cartera_CarteraPorEdades';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Cartera_CarteraPorEdades';
-- GO
