

CREATE PROCEDURE [dbo].[ESE_SP_Facturacion_FacturacionGeneral]
AS
     SELECT DISTINCT 
            ing.CODCENATE AS CodigoCentro, 
            CEN.NOMCENATE AS CentroAtencion,
            CASE f.DocumentType
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
            END AS [Tipo Documento],
            CASE ing.ICAUSAING
                WHEN '11'
                THEN 'Cirugía programada'
            END AS [Causa Ingreso], 
            ing.UFUCODIGO + ' - ' + uf.UFUDESCRI AS [Unidad funcional], 
            f.InvoiceNumber AS [Nro Documento], 
            f.AdmissionNumber AS Ingreso, 
            ing.IFECHAING AS [Fecha Ingreso], 
            eh.FECALTPAC AS [Fecha alta médica], 
            ea.Code + ' - ' + ea.Name AS [Entidad Administradora], 
            t.Nit, 
            t.DigitVerification AS [Dígito verificación], 
            t.Name AS Entidad, 
            f.PatientCode AS Identificación, 
            p.IPNOMCOMP AS NombrePaciente, 
            ga.Code + ' - ' + ga.Name AS [Grupo Atención], 
            f.InvoiceDate AS [Fecha Factura], 
            f.InvoiceExpirationDate AS [Fecha vencimiento], 
            f.TotalInvoice AS [Vr Factura], 
            f.ThirdPartySalesValue AS [Vr Entidad], 
            f.ThirdPartyDiscountValue AS [Vr. Descuento],
            CASE f.ResponsibleRecoveryFee
                WHEN '1'
                THEN 'Ninguno'
                WHEN '2'
                THEN 'Paciente'
                WHEN '3'
                THEN 'Tercero'
            END AS [Responsable Cuota Recuperacion], 
            f.TotalPatientSalesPrice AS [Vr cuota recuperación], 
            f.PatientDiscount AS [Descuento Cuota recuperación], 
            f.CashReceiptId AS [Recibo Caja Paciente], 
            f.PatientPaidValue AS [Vr pagado Paciente], 
            f.ThirdPartyAccountReceivableValue AS [Vr CxC], 
            f.PatientAccountReceivableValue AS [Vr CxC generada a Paciente],
            CASE f.PatientType
                WHEN '1'
                THEN 'Contributivo'
                WHEN '2'
                THEN 'Subsidiado'
                WHEN '3'
                THEN 'Vinculado'
                WHEN '4'
                THEN 'Particular'
                WHEN '5'
                THEN 'Otros'
                WHEN '6'
                THEN 'Desplazado Contributivo'
                WHEN '7'
                THEN 'Desplazado Subsidiado'
                WHEN '8'
                THEN 'Desplazado no Asegurado'
            END AS [Tipo Paciente], 
            f.Observation AS Observaciones, 
            C.Name AS Categoría,
            CASE f.STATUS
                WHEN '1'
                THEN 'Facturado'
                WHEN '2'
                THEN 'Anulado'
            END AS Estado_Documento, 
            f.InvoicedUser + ' - ' + per.Fullname AS Usuario, 
            f.InvoicedDate AS Fecha
     FROM Billing.Invoice AS f WITH(NOLOCK)
          LEFT OUTER JOIN dbo.INPACIENT AS p WITH(NOLOCK) ON p.IPCODPACI = f.PatientCode
          LEFT OUTER JOIN dbo.ADINGRESO AS ing WITH(NOLOCK) ON CAST(ing.NUMINGRES AS INT) = f.AdmissionNumber
          LEFT OUTER JOIN dbo.ADCENATEN AS cen WITH(NOLOCK) ON cen.CODCENATE = ing.CODCENATE
          LEFT OUTER JOIN Contract.CareGroup AS ga WITH(NOLOCK) ON ga.Id = f.CareGroupId
          LEFT OUTER JOIN Contract.HealthAdministrator AS ea WITH(NOLOCK) ON ea.Id = f.HealthAdministratorId
          LEFT OUTER JOIN Common.ThirdParty AS t WITH(NOLOCK) ON t.Id = f.ThirdPartyId
          LEFT OUTER JOIN Common.OperatingUnit AS uo WITH(NOLOCK) ON uo.Id = f.OperatingUnitId
          LEFT OUTER JOIN Security.[UserInt] AS u ON u.UserCode = f.InvoicedUser
          LEFT OUTER JOIN Security.PersonInt AS per ON per.Id = u.IdPerson
          LEFT OUTER JOIN Security.[UserInt] AS uc ON uc.UserCode = f.AnnulmentUser
          LEFT OUTER JOIN Billing.InvoiceCategories AS C WITH(NOLOCK) ON C.Id = f.InvoiceCategoryId
          LEFT OUTER JOIN dbo.INUNIFUNC AS uf WITH(NOLOCK) ON uf.UFUCODIGO = ing.UFUCODIGO
          LEFT OUTER JOIN dbo.HCREGEGRE AS eh WITH(NOLOCK) ON eh.NUMINGRES = f.AdmissionNumber
                                                              AND eh.IPCODPACI = f.PatientCode
     WHERE(f.STATUS = '1')
          AND (uo.Id = '14')
          AND (f.InvoiceDate BETWEEN '01/01/2019 00:00:00' AND '31/12/2019 23:59:59');
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reporte de facturación general de la institución: consolida todas las facturas emitidas (con estado ''Facturado'') durante el año 2019 para una unidad operativa específica, cruzando datos de admisiones/ingresos de pacientes, entidades administradoras de salud (EPS, ARS), terceros pagadores, grupos de atención y categorías de factura. Para cada documento genera una fila con información completa del episodio: centro de atención, unidad funcional, tipo de factura (EAPB con contrato, particular, capita, entre otros), datos de identificación y nombre del paciente, fechas de ingreso y alta médica, valores cobrados a la entidad y cuota de recuperación del paciente, cuentas por cobrar, recibos de caja, tipo de paciente (contributivo, subsidiado, vinculado, etc.), estado del documento y usuario que facturó. Sirve como fuente principal de reportería financiera y de auditoría de facturación, permitiendo analizar la producción facturada por sede, entidad pagadora, tipo de documento y modalidad de ingreso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_Facturacion_FacturacionGeneral';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_Facturacion_FacturacionGeneral';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las facturas activas de la unidad operativa 14 emitidas durante 2019, enriquecidas con datos del paciente, ingreso, entidad responsable, centro de atención y usuario facturador, para reportes de facturación general.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Facturacion_FacturacionGeneral';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existencia de facturas en Billing.Invoice con estado facturado; La unidad operativa con Id ''14'' debe existir en Common.OperatingUnit; Las facturas deben tener InvoiceDate dentro del año 2019', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Facturacion_FacturacionGeneral';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan facturas con estado ''Facturado'' (STATUS=1), nunca anuladas; El reporte está restringido a la unidad operativa con Id=''14''; El rango temporal está fijo (hardcodeado) al año 2019; El JOIN con ADINGRESO castea NUMINGRES a INT para emparejar con AdmissionNumber, asumiendo numérico', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Facturacion_FacturacionGeneral';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Factura; Tipo de documento (EAPB con/sin contrato, Particular, Cápita, Básica, Venta de productos); Ingreso/Admisión; Causa de ingreso (Cirugía programada); Centro de atención; Unidad funcional; Unidad operativa; Entidad administradora (EAPB); Grupo de atención; Cuota de recuperación; Responsable de cuota (Paciente/Tercero); Recibo de caja; Cuenta por cobrar (CxC); Tipo de paciente / Régimen (Contributivo, Subsidiado, Vinculado, Particular, Desplazado); Alta médica; Anulación de factura; Categoría de factura', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Facturacion_FacturacionGeneral';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Billing.Invoice: Cuando STATUS=''1'' (Facturado), uo.Id=''14'' y InvoiceDate entre 01/01/2019 y 31/12/2019, se retorna el conjunto de facturas con sus datos relacionados', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Facturacion_FacturacionGeneral';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si f.DocumentType en {1..7} → Traduce el código a etiqueta de tipo de documento (Factura EAPB con/Sin Contrato, Particular, Capita, Control Capitación, Básica, Venta Productos); si ing.ICAUSAING=''11'' → Etiqueta la causa de ingreso como ''Cirugía programada'' else Causa de ingreso queda en NULL; si f.ResponsibleRecoveryFee en {1,2,3} → Traduce a ''Ninguno'', ''Paciente'' o ''Tercero'' como responsable de la cuota de recuperación; si f.PatientType en {1..8} → Clasifica al paciente en régimen Contributivo, Subsidiado, Vinculado, Particular, Otros, Desplazado Contributivo/Subsidiado/no Asegurado; si f.STATUS en {1,2} → Traduce a ''Facturado'' o ''Anulado'' como estado del documento (aunque solo se filtran los facturados)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Facturacion_FacturacionGeneral';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.Invoice; dbo.INPACIENT; dbo.ADINGRESO; dbo.ADCENATEN; Contract.CareGroup; Contract.HealthAdministrator; Common.ThirdParty; Common.OperatingUnit; Security.UserInt; Security.PersonInt; Billing.InvoiceCategories; dbo.INUNIFUNC; dbo.HCREGEGRE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Facturacion_FacturacionGeneral';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Facturacion_FacturacionGeneral';
-- GO
