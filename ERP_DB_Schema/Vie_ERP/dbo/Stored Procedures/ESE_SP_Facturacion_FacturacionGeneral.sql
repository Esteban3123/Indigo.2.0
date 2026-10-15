


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