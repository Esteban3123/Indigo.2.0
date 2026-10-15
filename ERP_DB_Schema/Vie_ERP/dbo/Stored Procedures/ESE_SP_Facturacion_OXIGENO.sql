CREATE PROCEDURE [dbo].[ESE_SP_Facturacion_OXIGENO] @FechaIni DATETIME, 
                                                   @FechaFin DATETIME
AS
     SELECT DISTINCT 
            F.InvoiceNumber AS Factura, 
            F.AdmissionNumber AS Ingreso, 
            F.PatientCode AS Identificacion, 
            P.IPPRIAPEL AS Apellido1, 
            P.IPSEGAPEL AS Apellido2, 
            P.IPPRINOMB AS Nombre1, 
            P.IPSEGNOMB AS Nombre2, 
            ga.Name AS GrupoAtención, 
            F.InvoiceDate AS [Fecha Factura], 
            dos.InvoicedQuantity AS Cantidad,
            CASE
                WHEN dq.TotalSalesPrice IS NULL
                THEN dos.TotalSalesPrice
                ELSE dq.TotalSalesPrice
            END AS ValorUnitario,
            CASE
                WHEN dq.TotalSalesPrice IS NULL
                THEN DF.GrandTotalSalesPrice
                ELSE dq.TotalSalesPrice
            END AS ValorTotal, 
            t.Nit, 
            ea.Code + ' - ' + ea.Name AS [Entidad Administradora],
            CASE dos.RecordType
                WHEN '1'
                THEN 'Servicios'
                WHEN '2'
                THEN 'Medicamentos'
            END AS ServiciosMedicamentos, 
            Cups.Code AS Cups, 
            cups.Description AS NombreCups,
            CASE
                WHEN pr.Code IS NULL
                THEN ServiciosIPS.Code
                ELSE pr.Code
            END AS Código,
            CASE
                WHEN pr.Name IS NULL
                THEN ServiciosIPS.Name
                ELSE pr.Name
            END AS Descripción,
            CASE dos.Presentation
                WHEN '1'
                THEN 'No Quirúrgico'
                WHEN '2'
                THEN 'Quirúrgico'
                WHEN '3'
                THEN 'Paquete'
            END AS PresentacionServicio, 
            ServiciosIPSQ.Code AS Subcodigo, 
            ServiciosIPSQ.Name AS Subnombre,
            CASE
                WHEN dq.PerformsHealthProfessionalCode IS NULL
                THEN dos.PerformsHealthProfessionalCode
                ELSE dq.PerformsHealthProfessionalCode
            END AS CodigoMèdico, 
            IIF(ISNULL(os.EntityName, '') = 'PharmaceuticalDispensing', (STUFF(
     (
         SELECT DISTINCT 
                CHAR(13) + CHAR(10) + ' - ' + w.Code + ': ' + w.Name
         FROM Inventory.PharmaceuticalDispensing pd
              JOIN Inventory.PharmaceuticalDispensingDetail pdd ON pd.Id = pdd.PharmaceuticalDispensingId
                                                                   AND pdd.ProductId = dos.ProductId
              JOIN Inventory.Warehouse w ON pdd.WarehouseId = w.Id
         WHERE pd.Id = os.EntityId FOR XML PATH(N''), TYPE
     ).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')), '') Almacenes
     FROM Billing.Invoice AS F WITH(NOLOCK)
          INNER JOIN Billing.InvoiceDetail AS DF WITH(NOLOCK) ON DF.InvoiceId = F.Id
          INNER JOIN Billing.RevenueControlDetail rcd WITH(NOLOCK) ON rcd.Id = F.RevenueControlDetailId
          INNER JOIN Security.[User] AS u ON u.UserCode = F.InvoicedUser
          INNER JOIN Security.Person AS per ON per.Id = u.IdPerson
          INNER JOIN dbo.ADINGRESO AS ing WITH(NOLOCK) ON CAST(ing.NUMINGRES AS INT) = F.AdmissionNumber
          INNER JOIN DBO.ADCENATEN AS CEN WITH(NOLOCK) ON CEN.CODCENATE = ing.CODCENATE
          INNER JOIN Billing.ServiceOrderDetail AS dos WITH(NOLOCK) ON dos.Id = DF.ServiceOrderDetailId
          INNER JOIN Common.ThirdParty AS t WITH(NOLOCK) ON t.Id = F.ThirdPartyId
          INNER JOIN Contract.CareGroup AS ga WITH(NOLOCK) ON ga.Id = F.CareGroupId
          INNER JOIN Contract.HealthAdministrator AS ea WITH(NOLOCK) ON ea.Id = F.HealthAdministratorId
          INNER JOIN Billing.InvoiceCategories AS cat WITH(NOLOCK) ON cat.Id = F.InvoiceCategoryId
          LEFT OUTER JOIN Contract.IPSService AS ServiciosIPS WITH(NOLOCK) ON ServiciosIPS.Id = dos.IPSServiceId
          LEFT OUTER JOIN Inventory.InventoryProduct AS pr WITH(NOLOCK) ON pr.Id = dos.ProductId
          LEFT OUTER JOIN Contract.CUPSEntity AS Cups WITH(NOLOCK) ON cups.Id = dos.CUPSEntityId
          LEFT OUTER JOIN dbo.INPROFSAL AS med WITH(NOLOCK) ON med.CODPROSAL = dos.PerformsHealthProfessionalCode
          LEFT OUTER JOIN dbo.INESPECIA AS espmed WITH(NOLOCK) ON espmed.CODESPECI = med.CODESPEC1
          LEFT OUTER JOIN dbo.INDIAGNOS AS diag ON diag.CODDIAGNO = ing.CODDIAEGR
          LEFT OUTER JOIN dbo.HCREGEGRE AS salida WITH(NOLOCK) ON CAST(salida.NUMINGRES AS INT) = F.AdmissionNumber
          LEFT OUTER JOIN Billing.ServiceOrderDetailSurgical AS dq WITH(NOLOCK) ON dq.ServiceOrderDetailId = dos.Id
                                                                                   AND dq.OnlyMedicalFees = '0'
          LEFT OUTER JOIN dbo.INPROFSAL AS medqx WITH(NOLOCK) ON medqx.CODPROSAL = dq.PerformsHealthProfessionalCode
          LEFT OUTER JOIN dbo.INESPECIA AS espqx ON espqx.CODESPECI = medqx.CODESPEC1
          LEFT OUTER JOIN dbo.INPACIENT AS P WITH(NOLOCK) ON P.IPCODPACI = ing.IPCODPACI
          LEFT OUTER JOIN dbo.INUBICACI AS Ubi WITH(NOLOCK) ON Ubi.AUUBICACI = P.AUUBICACI
          LEFT OUTER JOIN dbo.ADCOMUNAS AS C WITH(NOLOCK) ON C.Id = Ubi.IDCOMUNA
          LEFT OUTER JOIN Contract.IPSService AS ServiciosIPSQ WITH(NOLOCK) ON ServiciosIPSQ.Id = dq.IPSServiceId
          LEFT OUTER JOIN Payroll.FunctionalUnit AS UF WITH(NOLOCK) ON UF.Id = dos.PerformsFunctionalUnitId
          LEFT OUTER JOIN Billing.ServiceOrder AS os WITH(NOLOCK) ON os.Id = dos.ServiceOrderId
          LEFT OUTER JOIN Billing.BillingGroup bg WITH(NOLOCK) ON bg.Id = IIF(ISNULL(Cups.ApplyRIAS, 0) = 1, Cups.RIASBillingGroupId, Cups.BillingGroupId)
          LEFT OUTER JOIN.RIASCUPS rc WITH(NOLOCK) ON rc.ID = dos.RIASCupsId
          LEFT OUTER JOIN.RIAS r WITH(NOLOCK) ON r.ID = rc.IDRIAS
     WHERE(F.STATUS = '1')
          AND (dos.IsDelete = '0')
          AND (F.InvoiceDate >= @FechaIni)
          AND (F.InvoiceDate <= @FechaFin)
          AND Cups.Code IN('S55201', 'S55202', 'S55203', 'S55204', 'S55205', 'S55206', 'S55207', 'S55208', 'S55209')
     ORDER BY F.InvoiceDate;