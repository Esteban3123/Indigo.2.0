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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento almacenado que genera el reporte de facturación de pacientes rurales para un rango de fechas dado. Consolida en un único resultado los datos del paciente (cédula, nombre, apellidos, fecha de nacimiento, sexo), la factura emitida (número, fecha, cantidades facturadas), el servicio o procedimiento CUPS cobrado, y la clasificación RIAS (Ruta Integral de Atención en Salud) cuando aplica. Cruza las órdenes de servicio y su detalle con las facturas activas (estado 1) y los ingresos cerrados (estado F), incorporando además el grupo de atención del contrato y la entidad pagadora (EPS o aseguradora) responsable. Se utiliza para reportería de gestión y control de cobros en la modalidad de atención rural, permitiendo identificar qué servicios fueron facturados a cada paciente de ese ámbito, bajo qué contrato y entidad, y si los procedimientos corresponden a rutas RIAS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_Facturacion_Pacientes_Rural';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_Facturacion_Pacientes_Rural';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las facturas activas de pacientes con ingreso finalizado en un rango de fechas, incluyendo datos del paciente, CUPS facturado, RIAS aplicable, grupo de atención y entidad pagadora, para reporte de facturación rural.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Facturacion_Pacientes_Rural';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Los rangos @InitialDate y @EndDate deben estar definidos para acotar la fecha de factura.; Deben existir relaciones consistentes entre ServiceOrder, ServiceOrderDetail, InvoiceDetail, Invoice y RevenueControlDetail.; El paciente (INPACIENT) y el ingreso (ADINGRESO) deben existir para los códigos referenciados en la orden.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Facturacion_Pacientes_Rural';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen facturas con estado 1 (activas/válidas).; Solo se consideran ingresos en estado ''F'' (finalizado).; Se excluyen detalles de orden marcados como eliminados (IsDelete=0).; El filtrado temporal se realiza sobre la fecha de la factura, no sobre la orden ni el ingreso.; CUPS, RIAS y entidad de salud son opcionales (LEFT JOIN); su ausencia no excluye la factura.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Facturacion_Pacientes_Rural';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Factura; Ingreso hospitalario; Orden de servicio; CUPS; RIAS (Rutas Integrales de Atención en Salud); Grupo de atención; Entidad administradora de salud (EPS/ARS); Centro de atención; Facturación rural', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Facturacion_Pacientes_Rural';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Cuando i.STATUS=1 AND ing.IESTADOIN=''F'' AND sod.IsDelete=0 AND InvoiceDate entre @InitialDate y @EndDate, retorna fila con datos de paciente, factura, CUPS, RIAS, grupo de atención y entidad.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Facturacion_Pacientes_Rural';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ISNULL(sod.ApplyRIAS,0) = 0 → Marca Aplica_RIAS = ''No'' else Marca Aplica_RIAS = ''Si''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Facturacion_Pacientes_Rural';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.ServiceOrder; Billing.ServiceOrderDetail; Billing.InvoiceDetail; Billing.Invoice; Billing.RevenueControlDetail; Contract.CareGroup; INPACIENT; ADINGRESO; Contract.CUPSEntity; RIASCUPS; RIAS; Contract.HealthAdministrator', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Facturacion_Pacientes_Rural';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Facturacion_Pacientes_Rural';
-- GO
