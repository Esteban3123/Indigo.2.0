CREATE PROCEDURE [dbo].[SPU_Agendamiento_Facturacion_Detalle] @FechaInicial DATE, 
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
          SELECT BID.InvoiceId, 
                 BID.ServiceOrderDetailId, 
                 BID.ServiceDate, 
                 BID.GrandTotalSalesPrice, 
                 BID.ThirdPartySalesPrice, 
                 BID.SubTotalPatientSalesPrice, 
                 BI.Id, 
                 BI.InvoiceNumber, 
                 BI.AdmissionNumber
          FROM Billing.Invoice AS BI
               INNER JOIN AG ON AG.IPCODPACI = BI.PatientCode
                                AND CAST(AG.FECHORAIN AS DATE) = CAST(BI.InvoiceDate AS DATE)
               INNER JOIN Billing.InvoiceDetail AS BID ON BID.InvoiceId = BI.Id
          ORDER BY BI.InvoiceDate;
     RETURN 0;
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reporte de facturación detallada por agendamiento: dado un rango de fechas y una especialidad médica, obtiene todas las citas programadas (no canceladas) junto con las facturas y líneas de facturación asociadas al mismo paciente y fecha de atención. Cruza los datos de agendamiento (cita, especialidad, médico, actividad médica y estado de la cita) con las facturas emitidas (Billing.Invoice) y su detalle de servicios facturados (Billing.InvoiceDetail), permitiendo identificar qué citas de una especialidad se convirtieron en factura y cuánto se cobró al tercero pagador y al paciente. Se utiliza para conciliación y control de gestión entre lo agendado y lo efectivamente facturado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPU_Agendamiento_Facturacion_Detalle';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPU_Agendamiento_Facturacion_Detalle';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Reporta el detalle de facturación cruzando las citas agendadas (no canceladas) de una especialidad y rango de fechas con las facturas y sus detalles emitidos al paciente en la misma fecha de atención.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_Agendamiento_Facturacion_Detalle';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El rango de fechas debe ser válido (inicial < final) ya que se filtra con >= inicial y < final; La especialidad solicitada debe existir en INESPECIA; Las citas deben tener profesional, especialidad y actividad médica relacionados en sus maestros (INPROFSAL, INESPECIA, AGACTIMED); Para que aparezca información de facturación, debe existir una factura en Billing.Invoice cuya fecha coincida con la fecha de la cita y cuyo PatientCode coincida con el paciente de la cita', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_Agendamiento_Facturacion_Detalle';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Nunca se incluyen citas canceladas (CODESTCIT = 4) en el reporte; El cruce factura-cita exige coincidencia exacta de paciente y de fecha (a nivel DATE) entre la cita y la factura; Solo se reportan citas cuya especialidad coincide exactamente con la solicitada; La codificación de estados de cita está limitada al rango 0..4; otros valores quedarían como NULL en EstadoAgendamiento; No realiza modificaciones de datos (operación de solo lectura)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_Agendamiento_Facturacion_Detalle';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cita / Agendamiento; Especialidad médica; Profesional de la salud / Médico; Actividad médica; Estado de cita (Asignada, Cumplida, Incumplida, PreAsignada, Cancelada); Paciente; Factura; Detalle de factura; Número de admisión; Valor facturado a paciente y a terceros', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_Agendamiento_Facturacion_Detalle';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Billing.Invoice: Devuelve un resultset combinando datos de la factura y sus detalles cruzados con citas agendadas no canceladas de la especialidad y rango indicado, ordenado por InvoiceDate', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_Agendamiento_Facturacion_Detalle';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si CI.CODESTCIT = 0 → Estado de agendamiento se etiqueta como ''Asignada''; si CI.CODESTCIT = 1 → Estado de agendamiento se etiqueta como ''Cumplida''; si CI.CODESTCIT = 2 → Estado de agendamiento se etiqueta como ''Incumplida''; si CI.CODESTCIT = 3 → Estado de agendamiento se etiqueta como ''PreAsignada''; si CI.CODESTCIT = 4 → Cita se excluye del resultado (filtro CODESTCIT <> 4 = Cancelada)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_Agendamiento_Facturacion_Detalle';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'DBO.AGASICITA; DBO.INESPECIA; DBO.AGACTIMED; dbo.INPROFSAL; Billing.Invoice; Billing.InvoiceDetail', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_Agendamiento_Facturacion_Detalle';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_Agendamiento_Facturacion_Detalle';
-- GO
