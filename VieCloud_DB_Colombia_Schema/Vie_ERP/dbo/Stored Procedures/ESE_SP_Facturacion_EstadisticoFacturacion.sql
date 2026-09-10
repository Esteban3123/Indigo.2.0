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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el estadístico o reporte de facturación para un rango de fechas, mostrando cada factura emitida con su número de ingreso, datos del paciente (cédula y nombre completo), valores cobrados (total factura, valor a cargo de la entidad, cuota de recuperación, valor pagado por el paciente y cuentas por cobrar), nombre del facturador responsable, centro de atención, entidad pagadora (EPS o aseguradora) y grupo de atención del contrato. Cruza las facturas activas (estado=1) con los ingresos del paciente, la información maestra del paciente, los centros de atención, las administradoras de salud y los grupos de atención contractuales, además del usuario facturador registrado en seguridad. Se usa principalmente para cuadres de producción, auditoría de facturación y seguimiento financiero por período.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_Facturacion_EstadisticoFacturacion';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_Facturacion_EstadisticoFacturacion';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un reporte estadístico de facturación activa en un rango de fechas, mostrando datos de la factura, paciente, entidad, grupo de atención, centro de atención y facturador.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Facturacion_EstadisticoFacturacion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las facturas deben tener estado activo (STATUS = 1); Las facturas deben estar dentro del rango de fechas indicado por los parámetros de fecha inicial y final; Cada factura debe tener ingreso, paciente, centro de atención, administradora de salud, grupo de atención y usuario facturador asociados (uso de INNER JOIN)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Facturacion_EstadisticoFacturacion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan facturas con STATUS = 1 (facturas vigentes/activas); Solo se incluyen facturas que tengan correspondencia completa con ingreso, paciente, centro de atención, administradora, grupo de atención y usuario facturador; La fecha de factura se entrega formateada en estilo 105 (dd-mm-yyyy); El nombre de la entidad se trunca a 50 caracteres y el del grupo de atención a 60 caracteres', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Facturacion_EstadisticoFacturacion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Factura; Ingreso; Paciente; Centro de atención; Entidad/Administradora de salud; Grupo de atención; Cuota de recuperación; Valor pagado por paciente; Cuentas por cobrar; Facturador', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Facturacion_EstadisticoFacturacion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Billing.Invoice: Cuando F.STATUS = 1 y InvoiceDate está en el rango [@FechaIni,@FechaFin], se retorna el detalle de la factura con datos del ingreso, paciente, entidad y facturador', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Facturacion_EstadisticoFacturacion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.Invoice; ADINGRESO; INPACIENT; ADCENATEN; Contract.HealthAdministrator; Contract.CareGroup; Security.User; Security.Person', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Facturacion_EstadisticoFacturacion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Facturacion_EstadisticoFacturacion';
-- GO
