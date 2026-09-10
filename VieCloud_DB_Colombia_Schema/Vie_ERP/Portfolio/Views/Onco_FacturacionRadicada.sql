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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista del módulo de oncología que consolida la facturación radicada ante entidades pagadoras (EPS, aseguradoras) con saldo pendiente de cobro y estado ''RADICADA ENTIDAD''. Integra información de cuentas por cobrar, facturas de venta, datos del paciente (cédula, nombre, edad), número de ingreso o admisión, grupo de atención, entidad administradora de salud, tercero pagador, sede, radicado de cobro con fecha de confirmación, categoría de factura y usuario que generó el documento. Permite al área de cartera y facturación hacer seguimiento a las facturas ya radicadas ante el pagador que aún tienen saldo sin recaudar, facilitando la gestión de cobro, conciliación y control de glosas en el servicio de oncología.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'VIEW', @level1name = N'Onco_FacturacionRadicada';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'VIEW', @level1name = N'Onco_FacturacionRadicada';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las facturas de cartera oncológica que ya fueron radicadas ante la entidad pagadora y aún tienen saldo pendiente de cobro, enriquecidas con datos del paciente, ingreso, entidad, sede y radicado.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'Onco_FacturacionRadicada';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La cuenta por cobrar debe tener saldo > 0 (PC.Balance > ''0''); El registro contable asociado (AccountReceivableAccounting por Id) debe tener saldo > 0 (sxcc.Balance > ''0''); La cuenta por cobrar no debe tener vendedor asignado (PC.SellerId IS NULL); El estado general de la cuenta por cobrar no puede ser ''3'' (PC.STATUS <> ''3''); El estado de cartera debe ser exactamente ''3'' = RADICADA ENTIDAD (PC.PortfolioStatus IN (''3''))', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'Onco_FacturacionRadicada';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo aparecen facturas cuyo estado de cartera es ''RADICADA ENTIDAD''; Solo se exponen documentos con saldo total y saldo sin radicar mayores a cero; Las facturas anuladas en Billing.Invoice (STATUS=''2'') no aportan datos al resultado; Los radicados con State=''4'' (anulados/inactivos) no se consideran para Nro Radicado ni Fecha confirmación; La edad se calcula como diferencia simple de años entre el año actual y el año de nacimiento del paciente; El campo ''Saldo sin radicar'' proviene del registro contable de la cuenta por cobrar, no del campo de la cabecera', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'Onco_FacturacionRadicada';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cartera; Factura radicada; Radicación ante entidad pagadora; Cuenta por cobrar; Entidad administradora de salud (EPS); Paciente; Ingreso/Admisión; Grupo de atención; Saldo de factura; Categoría de factura; Tipo de cuenta por cobrar (Ley 100, pagarés, acuerdos de pago, cuota moderadora); Sede / Unidad operativa', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'Onco_FacturacionRadicada';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Portfolio.AccountReceivable: Devuelve únicamente cuentas por cobrar con saldo positivo, sin vendedor, no anuladas (STATUS<>''3'') y en estado de cartera ''RADICADA ENTIDAD'' (PortfolioStatus=''3'')', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'Onco_FacturacionRadicada';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si AccountReceivableType en 1..7 → Traduce el código a etiqueta legible: 1=Facturación Básica, 2=Facturación Ley 100, 3=Impuestos, 4=Pagarés, 5=Acuerdos de Pago, 6=Documento de Pago a Cuota Moderadora, 7=Factura de Producto else NULL; si PortfolioStatus en {1,2,3,7,8,14,15} → Traduce a etiqueta de estado de cartera (SIN RADICAR, RADICADA SIN CONFIRMAR, RADICADA ENTIDAD, CERTIFICADA_PARCIAL, CERTIFICADA_TOTAL, DEVOLUCION_FACTURA, TRASLADO COBRO JURÍDICO CONFIRMADO) else NULL; si Billing.Invoice.STATUS = ''2'' → Excluye la factura del join (no se trae datos de paciente, entidad ni categoría) else Se enlaza la factura; si Portfolio.RadicateInvoiceD.State = ''4'' o RadicateInvoiceC.State = ''4'' → Se ignoran esos registros de radicación (no aportan consecutivo ni fecha de confirmación) else Se toman los datos del radicado', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'Onco_FacturacionRadicada';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Portfolio.AccountReceivable; Billing.Invoice; Contract.HealthAdministrator; Portfolio.RadicateInvoiceD; Portfolio.RadicateInvoiceC; dbo.INPACIENT; dbo.ADINGRESO; Contract.CareGroup; Common.ThirdParty; Common.OperatingUnit; dbo.SEGusuaru; GeneralLedger.MainAccounts; Portfolio.AccountReceivableAccounting; Billing.InvoiceCategories', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'Onco_FacturacionRadicada';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'Onco_FacturacionRadicada';
GO
