CREATE PROCEDURE [dbo].[ESE_SP_Facturacion_ProductividadPorMedico] @UserCode VARCHAR(20), 
                                                                  @FechaIni DATETIME, 
                                                                  @FechaFin DATETIME
AS
     SELECT CEN.NOMCENATE AS CentroAtencion,
            CASE
                WHEN dq.PerformsHealthProfessionalCode IS NULL
                THEN dos.PerformsHealthProfessionalCode
                ELSE dq.PerformsHealthProfessionalCode
            END AS CodigoMedico,
            CASE
                WHEN RTRIM(medqx.NOMMEDICO) IS NULL
                THEN RTRIM(med.NOMMEDICO)
                ELSE RTRIM(medqx.NOMMEDICO)
            END AS NombreMedico,
            CASE dos.RecordType
                WHEN '1'
                THEN 'Servicios'
                WHEN '2'
                THEN 'Medi/Insumo'
            END AS ServiciosMedicamentos,
            CASE
                WHEN pr.Code IS NULL
                THEN Cups.Code
                ELSE pr.Code
            END AS Codigo,
            CASE
                WHEN pr.Name IS NULL
                THEN cups.Description
                ELSE pr.Name
            END AS Descripción, 
            SUM(dos.InvoicedQuantity) AS Cantidad,
            CASE
                WHEN MIN(dq.TotalSalesPrice) IS NULL
                THEN MIN(dos.TotalSalesPrice)
                ELSE MIN(dq.TotalSalesPrice)
            END AS ValorUnitario, 
            (SUM(dos.InvoicedQuantity) * CASE
                                             WHEN MIN(dq.TotalSalesPrice) IS NULL
                                             THEN MIN(dos.TotalSalesPrice)
                                             ELSE MIN(dq.TotalSalesPrice)
                                         END) AS Total
     FROM Billing.Invoice AS F
          INNER JOIN Billing.InvoiceDetail AS DF WITH(NOLOCK) ON DF.InvoiceId = F.Id
          INNER JOIN Security.[User] AS u ON u.UserCode = F.InvoicedUser
          INNER JOIN Security.Person AS per ON per.Id = u.IdPerson
          INNER JOIN dbo.ADINGRESO AS ing WITH(NOLOCK) ON CAST(ing.NUMINGRES AS INT) = F.AdmissionNumber
          INNER JOIN DBO.ADCENATEN AS CEN WITH(NOLOCK) ON CEN.CODCENATE = ing.CODCENATE
          INNER JOIN Billing.ServiceOrderDetail AS dos WITH(NOLOCK) ON dos.Id = DF.ServiceOrderDetailId
          LEFT OUTER JOIN Billing.ServiceOrderDetailSurgical AS dq WITH(NOLOCK) ON dq.ServiceOrderDetailId = dos.Id
                                                                                   AND dq.OnlyMedicalFees = '0'
          LEFT OUTER JOIN Contract.IPSService AS ServiciosIPS WITH(NOLOCK) ON ServiciosIPS.Id = dos.IPSServiceId
          LEFT OUTER JOIN Inventory.InventoryProduct AS pr WITH(NOLOCK) ON pr.Id = dos.ProductId
          LEFT OUTER JOIN Contract.CUPSEntity AS Cups WITH(NOLOCK) ON cups.Id = dos.CUPSEntityId
          LEFT OUTER JOIN dbo.INPROFSAL AS med WITH(NOLOCK) ON med.CODPROSAL = dos.PerformsHealthProfessionalCode
          LEFT OUTER JOIN dbo.INPROFSAL AS medqx WITH(NOLOCK) ON medqx.CODPROSAL = dq.PerformsHealthProfessionalCode
     WHERE((dq.TotalSalesPrice IS NULL
            AND dos.TotalSalesPrice > 0)
           OR ((dos.TotalSalesPrice IS NULL
                AND dq.TotalSalesPrice > 0)))
          AND (F.STATUS = '1')
          AND (dos.IsDelete = '0')
          AND CONVERT(VARCHAR(10), F.InvoiceDate, 105) >= @FechaIni
          AND CONVERT(VARCHAR(10), F.InvoiceDate, 105) <= @FechaFin
          AND ((dq.PerformsHealthProfessionalCode IS NULL
                AND dos.PerformsHealthProfessionalCode = @UserCode)
               OR ((dos.PerformsHealthProfessionalCode IS NULL
                    AND dq.PerformsHealthProfessionalCode = @UserCode)))
     GROUP BY CEN.NOMCENATE,
              CASE
                  WHEN dq.PerformsHealthProfessionalCode IS NULL
                  THEN dos.PerformsHealthProfessionalCode
                  ELSE dq.PerformsHealthProfessionalCode
              END,
              CASE
                  WHEN RTRIM(medqx.NOMMEDICO) IS NULL
                  THEN RTRIM(med.NOMMEDICO)
                  ELSE RTRIM(medqx.NOMMEDICO)
              END,
              CASE dos.RecordType
                  WHEN '1'
                  THEN 'Servicios'
                  WHEN '2'
                  THEN 'Medi/Insumo'
              END,
              CASE
                  WHEN pr.Code IS NULL
                  THEN Cups.Code
                  ELSE pr.Code
              END,
              CASE
                  WHEN pr.Name IS NULL
                  THEN cups.Description
                  ELSE pr.Name
              END;