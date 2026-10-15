CREATE PROCEDURE [dbo].[ESE_SP_Facturacion_EstadisticoFacturacion] @FechaIni DATETIME, 
                                                                  @FechaFin DATETIME
AS
     SELECT I.NUMINGRES AS Ingreso, 
            F.InvoiceNumber AS Factura, 
            CONVERT(VARCHAR(10), F.InvoiceDate, 105) AS FechaFactura, 
            RTRIM(F.PatientCode) AS Cedula, 
            RTRIM(P.IPNOMCOMP) AS NombrePaciente, 
            f.TotalInvoice AS VrFactura, 
            RTRIM(f.ThirdPartySalesValue) AS VrEntidad, 
            f.TotalPatientSalesPrice AS VrcuotaRecuperación, 
            f.PatientPaidValue AS VrPagadoPaciente, 
            f.ThirdPartyAccountReceivableValue AS VrCxC, 
            RTRIM(per.Fullname) AS Facturador, 
            RTRIM(CEN.NOMCENATE) AS CentroAtencion, 
            SUBSTRING(RTRIM(RTRIM(ea.Name)), 1, 50) AS Entidad, 
            SUBSTRING(RTRIM(ga.Name), 1, 60) AS GrupoAtencion
     FROM Billing.Invoice AS F WITH(NOLOCK)
          INNER JOIN.ADINGRESO AS I WITH(NOLOCK) ON I.NUMINGRES = F.AdmissionNumber
          INNER JOIN.INPACIENT AS P WITH(NOLOCK) ON P.IPCODPACI = f.PatientCode
          INNER JOIN.ADCENATEN AS CEN WITH(NOLOCK) ON CEN.CODCENATE = I.CODCENATE
          INNER JOIN Contract.HealthAdministrator AS ea WITH(NOLOCK) ON ea.Id = f.HealthAdministratorId
          INNER JOIN Contract.CareGroup AS ga WITH(NOLOCK) ON ga.Id = f.CareGroupId
          INNER JOIN Security.[User] AS u ON u.UserCode = f.InvoicedUser
          INNER JOIN Security.Person AS per ON per.Id = u.IdPerson
     WHERE F.STATUS = 1
           AND f.InvoiceDate BETWEEN @FechaIni AND @FechaFin;