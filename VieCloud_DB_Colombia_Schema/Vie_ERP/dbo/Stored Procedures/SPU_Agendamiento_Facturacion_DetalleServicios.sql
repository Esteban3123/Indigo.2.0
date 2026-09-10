CREATE PROCEDURE [dbo].[SPU_Agendamiento_Facturacion_DetalleServicios] @FechaInicial DATE, 
                                                                      @FechaFinal   DATE, 
                                                                      @Especialidad CHAR(3)
AS
     WITH AG
          AS (SELECT CI.CODAUTONU, 
                     CI.CODESPECI, 
                     ES.DESESPECI AS EspecialidadAgendada, 
                     CI.IPCODPACI, 
                     CI.CODPROSAL, 
                     PS.NOMMEDICO AS NombreMedico, 
                     CI.FECHORAIN, 
                     AM.DESACTMED AS ActividadAgendada,
                     CASE CI.CODESTCIT
                         WHEN 0
                         THEN 'Asignada'
                         WHEN 1
                         THEN 'Cumplida'
                         WHEN 2
                         THEN 'Incumplida'
                         WHEN 3
                         THEN 'PreAsignada'
                         WHEN 4
                         THEN 'Cancelada'
                     END AS EstadoAgendamiento
              FROM DBO.AGASICITA AS CI
                   INNER JOIN DBO.INESPECIA AS ES ON CI.CODESPECI = ES.CODESPECI
                   INNER JOIN DBO.AGACTIMED AS AM ON CI.CODACTMED = AM.CODACTMED
                   INNER JOIN dbo.INPROFSAL AS PS ON PS.CODPROSAL = CI.CODPROSAL
              WHERE CI.FECHORAIN >= @FechaInicial
                    AND CI.FECHORAIN < @FechaFinal
                    AND CI.CODESPECI = @Especialidad
                    AND CI.CODESTCIT <> 4)
          SELECT SOD.Id, 
                 SOD.ServiceOrderId, 
                 ce.Code, 
                 ce.Description, 
                 SOD.PerformsHealthProfessionalCode, 
                 SOD.PerformsProfessionalSpecialty, 
                 SOD.SubTotalSalesPrice, 
                 SOD.TotalSalesPrice, 
                 SOD.GrandTotalSalesPrice
          FROM Billing.Invoice AS BI
               INNER JOIN AG ON AG.IPCODPACI = BI.PatientCode
                                AND CAST(AG.FECHORAIN AS DATE) = CAST(BI.InvoiceDate AS DATE)
               INNER JOIN Billing.InvoiceDetail AS BID ON BID.InvoiceId = BI.Id
               INNER JOIN Billing.ServiceOrderDetail AS SOD ON BID.ServiceOrderDetailId = SOD.Id
               INNER JOIN Contract.CUPSEntity AS CE ON CE.Id = SOD.CUPSEntityId
          ORDER BY BI.InvoiceDate;
     RETURN 0;
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que genera el detalle de servicios facturados para citas de agendamiento en un rango de fechas y una especialidad médica específica. Cruza las citas agendadas (excluyendo las canceladas) con las facturas de venta del módulo de facturación, obteniendo los ítems de cada orden de servicio junto con sus códigos CUPS, valores y profesional ejecutante. Se usa para conciliar lo que fue agendado y atendido con lo efectivamente facturado, útil en reportes de producción médica, auditoría de facturación y control de ingresos por especialidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPU_Agendamiento_Facturacion_DetalleServicios';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPU_Agendamiento_Facturacion_DetalleServicios';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los servicios facturados (CUPS, valores y profesional ejecutor) asociados a citas agendadas no canceladas de una especialidad en un rango de fechas, cruzando agenda con facturación por paciente y fecha.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_Agendamiento_Facturacion_DetalleServicios';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El rango de fechas y el código de especialidad deben estar provistos; Deben existir citas en AGASICITA con FECHORAIN dentro del rango y para la especialidad indicada; Las citas deben estar relacionadas con maestros de especialidad, actividad médica y profesional de la salud; Debe existir factura en Billing.Invoice cuyo PatientCode coincida con el paciente de la cita y cuya InvoiceDate (a nivel de día) coincida con la fecha de la cita', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_Agendamiento_Facturacion_DetalleServicios';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Las citas canceladas (CODESTCIT=4) nunca aparecen en el resultado; El cruce agenda-factura se realiza por paciente y por fecha truncada a día (sin componente de hora); Solo se incluyen servicios cuya orden de servicio esté ligada a una factura existente y a un CUPS del catálogo; El resultado se ordena cronológicamente por fecha de factura', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_Agendamiento_Facturacion_DetalleServicios';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Agendamiento de citas; Estado de cita (Asignada, Cumplida, Incumplida, PreAsignada, Cancelada); Especialidad médica; Actividad médica; Profesional de la salud; Paciente; Factura; Orden de servicio; Procedimiento CUPS; Valores de venta (subtotal, total, gran total)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_Agendamiento_Facturacion_DetalleServicios';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Billing.ServiceOrderDetail: Devuelve detalle de servicios facturados (Id, código y descripción CUPS, profesional ejecutor, especialidad ejecutora y valores subtotal/total/grand total) cuando la cita no está cancelada (CODESTCIT<>4) y la fecha de la cita coincide con la fecha de la factura del mismo paciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_Agendamiento_Facturacion_DetalleServicios';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si CODESTCIT = 4 (Cancelada) → Se excluye la cita del resultado else Se traduce el código de estado a etiqueta (0=Asignada,1=Cumplida,2=Incumplida,3=PreAsignada) y se considera para el cruce con facturación', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_Agendamiento_Facturacion_DetalleServicios';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'DBO.AGASICITA; DBO.INESPECIA; DBO.AGACTIMED; dbo.INPROFSAL; Billing.Invoice; Billing.InvoiceDetail; Billing.ServiceOrderDetail; Contract.CUPSEntity', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_Agendamiento_Facturacion_DetalleServicios';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_Agendamiento_Facturacion_DetalleServicios';
-- GO
