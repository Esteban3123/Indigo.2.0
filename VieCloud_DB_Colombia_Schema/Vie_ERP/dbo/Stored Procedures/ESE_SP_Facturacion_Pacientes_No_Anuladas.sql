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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que consulta todas las facturas vigentes (no anuladas) emitidas a un paciente específico, identificado por su código o cédula. Combina órdenes de servicio, detalles de facturación e información de facturas para mostrar cada procedimiento o servicio CUPS facturado durante ingresos en estado finalizado. Incluye datos del paciente (nombre, fecha de nacimiento, código), número de factura, fecha, número de ingreso, código y nombre del servicio CUPS, cantidad facturada, y si el servicio aplica a una Ruta Integral de Atención en Salud (RIAS) con su respectivo código y nombre. Se usa para consultar el historial de facturación no anulada de un paciente en el módulo de facturación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_Facturacion_Pacientes_No_Anuladas';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_Facturacion_Pacientes_No_Anuladas';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los servicios facturados (no anulados) de un paciente, mostrando datos de la factura, el ingreso, el CUPS y la posible asociación a una RIAS.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Facturacion_Pacientes_No_Anuladas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir el paciente en INPACIENT y el ingreso en ADINGRESO referenciado por la orden de servicio.; La factura debe tener STATUS = 1 (no anulada).; El ingreso debe estar en estado ''F'' (IESTADOIN = ''F'').; El detalle de la orden de servicio no debe estar eliminado (IsDelete = 0).; El detalle de la orden de servicio debe tener un CUPSEntityId asignado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Facturacion_Pacientes_No_Anuladas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen servicios pertenecientes a facturas activas (STATUS=1), excluyendo facturas anuladas.; Solo se incluyen ingresos finalizados/facturados (IESTADOIN=''F'').; Se excluyen detalles de orden lógicamente eliminados (IsDelete=0).; Se excluyen detalles sin CUPS asociado (CUPSEntityId IS NOT NULL).; La asociación a RIAS es opcional: si no hay RIASCupsId el servicio aparece sin código/nombre RIAS pero igualmente se lista.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Facturacion_Pacientes_No_Anuladas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Factura; Ingreso hospitalario; Orden de servicio; CUPS; RIAS; Anulación de factura', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Facturacion_Pacientes_No_Anuladas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve filas de facturación del paciente cuando i.STATUS=1, ing.IESTADOIN=''F'', sod.IsDelete=0, sod.CUPSEntityId IS NOT NULL y so.PatientCode coincide con el parámetro, ordenadas por fecha de factura ascendente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Facturacion_Pacientes_No_Anuladas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ISNULL(sod.ApplyRIAS,0) = 0 → Marca el servicio como Aplica_RIAS = ''No'' else Marca el servicio como Aplica_RIAS = ''Si''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Facturacion_Pacientes_No_Anuladas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.ServiceOrder; Billing.ServiceOrderDetail; Billing.InvoiceDetail; Billing.Invoice; INPACIENT; ADINGRESO; Contract.CUPSEntity; RIASCUPS; RIAS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Facturacion_Pacientes_No_Anuladas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Facturacion_Pacientes_No_Anuladas';
-- GO
