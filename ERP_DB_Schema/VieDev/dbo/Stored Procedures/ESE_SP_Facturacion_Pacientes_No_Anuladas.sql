CREATE PROCEDURE [dbo].[ESE_SP_Facturacion_Pacientes_No_Anuladas] @PatientCode VARCHAR(25)
AS
     SELECT i.InvoiceNumber Numero_Factura, 
            i.InvoiceDate Fecha_Factura, 
            so.AdmissionNumber Numero_Ingreso, 
            ce.Code Codigo_CUPS, 
            ce.Description Nombre_CUPS, 
            IIF(ISNULL(sod.ApplyRIAS, 0) = 0, 'No', 'Si') Aplica_RIAS, 
            r.CODPRO Codigo_RIAS, 
            r.NOMBRE Nombre_RIAS, 
            id.InvoicedQuantity Cantidad, 
            p.IPCODPACI Codigo_Paciente, 
            p.IPNOMCOMP Nombre_Paciente, 
            p.IPFECNACI Fecha_Nacimiento
     FROM Billing.ServiceOrder so WITH(NOLOCK)
          INNER JOIN Billing.ServiceOrderDetail sod WITH(NOLOCK) ON sod.ServiceOrderId = so.Id
          INNER JOIN Billing.InvoiceDetail id WITH(NOLOCK) ON id.ServiceOrderDetailId = sod.Id
          INNER JOIN Billing.Invoice i WITH(NOLOCK) ON i.Id = id.InvoiceId
          INNER JOIN.INPACIENT p WITH(NOLOCK) ON p.IPCODPACI = so.PatientCode
          INNER JOIN.ADINGRESO ing WITH(NOLOCK) ON ing.NUMINGRES = so.AdmissionNumber
          LEFT JOIN Contract.CUPSEntity ce WITH(NOLOCK) ON ce.Id = sod.CUPSEntityId
          LEFT JOIN.RIASCUPS rc WITH(NOLOCK) ON rc.ID = sod.RIASCupsId
          LEFT JOIN.RIAS r WITH(NOLOCK) ON r.ID = rc.IDRIAS
     WHERE i.STATUS = 1
           AND ing.IESTADOIN = 'F'
           AND sod.IsDelete = 0
           AND so.PatientCode = @PatientCode
           AND sod.CUPSEntityId IS NOT NULL
     ORDER BY i.InvoiceDate;