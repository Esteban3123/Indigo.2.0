CREATE PROCEDURE [dbo].[SPU_Agendamiento_Facturacion] @FechaInicial DATE, 
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
          SELECT BI.Id, 
                 BI.InvoiceNumber, 
                 BI.AdmissionNumber, 
                 BI.InvoiceDate, 
                 BI.PatientCode, 
                 BI.TotalInvoice, 
                 BI.[Status], 
                 BI.DescriptionReversal, 
                 AG.FECHORAIN AS FechaAgenda, 
                 AG.CODESPECI AS EspecialidadAgenda
          FROM Billing.Invoice AS BI
               INNER JOIN AG ON AG.IPCODPACI = BI.PatientCode
                                AND CAST(AG.FECHORAIN AS DATE) = CAST(BI.InvoiceDate AS DATE)
          ORDER BY BI.InvoiceDate;
     RETURN 0;
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reporte de agendamiento con facturación: dado un rango de fechas y una especialidad médica, recupera todas las citas no canceladas (asignadas, cumplidas, incumplidas o pre-asignadas) junto con sus facturas de cobro asociadas. Cruza la información de citas (AGASICITA) con la especialidad médica (INESPECIA), la actividad médica programada (AGACTIMED) y el profesional de salud (INPROFSAL), y luego vincula cada cita con su factura de venta (Billing.Invoice) usando el código del paciente y la fecha. Sirve para auditar y conciliar si las citas agendadas en una especialidad generaron efectivamente una factura, apoyando procesos de control de ingresos, glosas y productividad médica por especialidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPU_Agendamiento_Facturacion';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPU_Agendamiento_Facturacion';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Cruza las citas agendadas vigentes de una especialidad y rango de fechas con las facturas emitidas el mismo día al mismo paciente, para conciliar agendamiento vs. facturación.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_Agendamiento_Facturacion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El rango de fechas y el código de especialidad deben estar definidos para filtrar la agenda.; Deben existir maestros consistentes de especialidad, actividad médica y profesional de la salud para resolver los INNER JOIN.; Las facturas deben tener PatientCode coincidente con el paciente de la cita y la fecha de factura debe coincidir (a nivel día) con la fecha de la cita.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_Agendamiento_Facturacion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Nunca incluye citas canceladas (CODESTCIT=4) en el cruce con facturación.; El emparejamiento agenda-factura siempre se hace por paciente y por igualdad de fecha truncada a día (sin hora).; El rango de fechas se evalúa como semiabierto: incluye @FechaInicial y excluye @FechaFinal.; Sólo se reportan facturas que tienen al menos una cita coincidente (INNER JOIN), no se listan facturas sin agenda.; Los resultados se entregan ordenados por fecha de factura ascendente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_Agendamiento_Facturacion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Agendamiento de citas; Especialidad médica; Profesional de la salud / médico; Actividad médica; Estado de la cita (Asignada, Cumplida, Incumplida, PreAsignada, Cancelada); Facturación / factura de venta; Paciente; Admisión', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_Agendamiento_Facturacion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Billing.Invoice: Devuelve facturas cruzadas con citas cuya FECHORAIN está en [@FechaInicial, @FechaFinal), CODESPECI=@Especialidad y CODESTCIT<>4 (no canceladas), emparejando por paciente y misma fecha (CAST a DATE).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_Agendamiento_Facturacion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si CI.CODESTCIT = 0 → El estado de agendamiento se reporta como ''Asignada''; si CI.CODESTCIT = 1 → El estado de agendamiento se reporta como ''Cumplida''; si CI.CODESTCIT = 2 → El estado de agendamiento se reporta como ''Incumplida''; si CI.CODESTCIT = 3 → El estado de agendamiento se reporta como ''PreAsignada''; si CI.CODESTCIT = 4 → La cita se excluye del resultado (citas canceladas no participan en la conciliación)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_Agendamiento_Facturacion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'DBO.AGASICITA; DBO.INESPECIA; DBO.AGACTIMED; dbo.INPROFSAL; Billing.Invoice', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_Agendamiento_Facturacion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_Agendamiento_Facturacion';
-- GO
