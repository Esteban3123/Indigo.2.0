CREATE VIEW [Portfolio].[Onco_FacturacionSinRadicar]
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
          AND (PC.SellerId IS NULL)
          AND (PC.STATUS <> '3')
          AND (PC.PortfolioStatus IN('1', '2'))
          AND (PC.AccountReceivableType <> '6');
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de oncología que muestra las facturas pendientes de radicar o radicadas sin confirmar ante entidades pagadoras (EPS, aseguradoras), con saldo mayor a cero y que no han sido cedidas a terceros cobradores. Integra información de cuentas por cobrar, facturas de venta, datos del paciente (nombre, edad, cédula/identificación), número de ingreso hospitalario, entidad administradora de salud, grupo de atención del contrato, sede de atención y el usuario que creó la factura. Incluye el saldo sin radicar, saldo total, fechas de factura y vencimiento, tipo de cuenta por cobrar, estado de cartera (sin radicar o radicada sin confirmar) y el número de radicado cuando existe. Se usa para el seguimiento y gestión de cobro de cartera de oncología que aún no ha sido completamente radicada o confirmada ante el pagador.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'VIEW', @level1name = N'Onco_FacturacionSinRadicar';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'VIEW', @level1name = N'Onco_FacturacionSinRadicar';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las cuentas por cobrar de oncología pendientes de radicación o por confirmar (sin vendedor, con saldo y estado 1 o 2), enriquecidas con datos de factura, paciente, ingreso, entidad pagadora, sede y radicado.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'Onco_FacturacionSinRadicar';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las cuentas por cobrar deben existir en Portfolio.AccountReceivable con Balance > 0, sin vendedor (SellerId IS NULL), STATUS distinto de 3 y PortfolioStatus en (1,2).; El tipo de CxC no puede ser 6 (cuota moderadora).; Para enriquecer datos de paciente, ingreso, entidad y categoría, debe existir la factura asociada en Billing.Invoice con STATUS distinto de 2.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'Onco_FacturacionSinRadicar';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo expone cuentas por cobrar con saldo positivo (Balance > 0).; Solo incluye CxC sin vendedor asignado (SellerId IS NULL).; Excluye cuentas por cobrar con STATUS = ''3'' (anuladas/canceladas).; Únicamente muestra CxC en estado 1 (SIN RADICAR) o 2 (RADICADA SIN CONFIRMAR), por lo que el resultado siempre representa facturación pendiente de radicación o confirmación.; Excluye documentos de pago a cuota moderadora (AccountReceivableType <> 6).; Ignora facturas anuladas en Billing.Invoice (STATUS <> ''2'').; Ignora radicados o detalles de radicación con State = 4 (anulados).', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'Onco_FacturacionSinRadicar';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cartera; Cuentas por cobrar; Facturación; Radicación de facturas; Entidad administradora de salud (EPS); Paciente; Ingreso/Admisión; Grupo de atención; Ley 100; Cuota moderadora; Categoría de factura; Saldo sin radicar', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'Onco_FacturacionSinRadicar';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Portfolio.AccountReceivable: Retorna una fila por cuenta por cobrar que cumpla: Balance>0, SellerId IS NULL, STATUS<>3, PortfolioStatus IN (1,2) y AccountReceivableType<>6.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'Onco_FacturacionSinRadicar';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si AccountReceivableType (1..7) → Mapea a etiqueta de Tipo CxC: 1=Facturación Básica, 2=Facturación Ley 100, 3=Impuestos, 4=Pagarés, 5=Acuerdos de Pago, 6=Documento de Pago a Cuota Moderadora, 7=Factura de Producto; si PortfolioStatus (1,2,3,7,8,14,15) → Traduce a estado legible: 1=SIN RADICAR, 2=RADICADA SIN CONFIRMAR, 3=RADICADA ENTIDAD, 7=CERTIFICADA_PARCIAL, 8=CERTIFICADA_TOTAL, 14=DEVOLUCION_FACTURA, 15=TRASLADO COBRO JURÍDICO CONFIRMADO', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'Onco_FacturacionSinRadicar';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Portfolio.AccountReceivable; Billing.Invoice; Contract.HealthAdministrator; Portfolio.RadicateInvoiceD; Portfolio.RadicateInvoiceC; dbo.INPACIENT; dbo.ADINGRESO; Contract.CareGroup; Common.ThirdParty; Common.OperatingUnit; dbo.SEGusuaru; GeneralLedger.MainAccounts; Portfolio.AccountReceivableAccounting; Billing.InvoiceCategories', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'Onco_FacturacionSinRadicar';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'Onco_FacturacionSinRadicar';
GO
