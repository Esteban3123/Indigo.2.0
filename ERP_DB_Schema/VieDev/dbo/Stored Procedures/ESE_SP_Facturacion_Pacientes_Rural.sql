CREATE PROCEDURE [dbo].[ESE_SP_Facturacion_Pacientes_Rural] @InitialDate DATETIME, 
                                                           @EndDate     DATETIME
AS
     SELECT p.IPCODPACI Identificacion, 
            i.InvoiceNumber Numero_Factura, 
            p.IPPRIAPEL Apellido_Uno, 
            p.IPSEGAPEL Apellido_Dos, 
            p.IPPRINOMB Nombre_Uno, 
            p.IPSEGNOMB Nombre_Dos, 
            i.InvoiceDate Fecha_Factura, 
            id.InvoicedQuantity Cantidad, 
            ce.Code Codigo_CUPS, 
            ce.Description Nombre_CUPS, 
            IIF(ISNULL(sod.ApplyRIAS, 0) = 0, 'No', 'Si') Aplica_RIAS, 
            r.CODPRO Codigo_RIAS, 
            r.NOMBRE Nombre_RIAS, 
            p.IPFECNACI Fecha_Nacimiento, 
            P.IPSEXO, 
            ing.CODCENATE, 
            cg.Code Codigo_Grupo_Atencion, 
            cg.Name Nombre_Grupo_Atencion, 
            ha.Code Codigo_Entidad, 
            ha.Name Nombre_Entidad
     FROM Billing.ServiceOrder so WITH(NOLOCK)
          INNER JOIN Billing.ServiceOrderDetail sod WITH(NOLOCK) ON sod.ServiceOrderId = so.Id
          INNER JOIN Billing.InvoiceDetail id WITH(NOLOCK) ON id.ServiceOrderDetailId = sod.Id
          INNER JOIN Billing.Invoice i WITH(NOLOCK) ON i.Id = id.InvoiceId
          INNER JOIN Billing.RevenueControlDetail rcd WITH(NOLOCK) ON rcd.Id = i.RevenueControlDetailId
          INNER JOIN Contract.CareGroup cg WITH(NOLOCK) ON cg.Id = rcd.CareGroupId
          INNER JOIN.INPACIENT p WITH(NOLOCK) ON p.IPCODPACI = so.PatientCode
          INNER JOIN.ADINGRESO ing WITH(NOLOCK) ON ing.NUMINGRES = so.AdmissionNumber
          LEFT JOIN Contract.CUPSEntity ce WITH(NOLOCK) ON ce.Id = sod.CUPSEntityId
          LEFT JOIN.RIASCUPS rc WITH(NOLOCK) ON rc.ID = sod.RIASCupsId
          LEFT JOIN.RIAS r WITH(NOLOCK) ON r.ID = rc.IDRIAS
          LEFT JOIN Contract.HealthAdministrator ha WITH(NOLOCK) ON ha.Id = rcd.HealthAdministratorId
     WHERE i.STATUS = 1
           AND ing.IESTADOIN = 'F'
           AND sod.IsDelete = 0
           AND i.InvoiceDate >= @InitialDate
           AND i.InvoiceDate <= @EndDate;