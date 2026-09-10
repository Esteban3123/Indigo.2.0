
--DROP PROCEDURE [dbo].[USP_AgendamientoHCfactura] 

CREATE PROCEDURE [dbo].[USP_AgendamientoHCfactura] @fechaInicial DATETIME, 
                                                  @fechaFinal   DATETIME
AS
     WITH F
          AS (SELECT DISTINCT 
                     BI.PatientCode, 
                     CAST(BI.InvoiceDate AS DATE) AS FechaFactura
              FROM [Billing].[Invoice] AS BI
              WHERE BI.InvoiceDate >= @fechaInicial
                    AND BI.InvoiceDate < @fechaFinal
                    AND BI.PatientCode IS NOT NULL),
          PM
          AS (SELECT PM.IPCODPACI AS IdPcteProcMenor, 
                     CAST(PM.FECREAPRO AS DATE) AS FechaProcMenor, 
                     PM.CODSERIPS AS CodProcMenor, 
                     I.DESSERIPS AS DescripcionProcMenor, 
                     PM.CODPROSAL AS CodProfSaludProcMenor, 
                     PS.NOMMEDICO AS NombreProfSaludProcMenor
              FROM dbo.HCINFPROM AS PM
                   INNER JOIN dbo.INCUPSIPS AS I ON PM.CODSERIPS = I.CODSERIPS
                   INNER JOIN dbo.INPROFSAL AS PS ON PM.CODPROSAL = PS.CODPROSAL
              WHERE PM.FECREAPRO >= @fechaInicial
                    AND PM.FECREAPRO < @fechaFinal
              UNION ALL
              SELECT D.IPCODPACI AS IdPcteProcMenor, 
                     CAST(D.FECPROCES AS DATE) AS FechaProcMenor, 
                     D.CODSERIPS AS CodProcMenor, 
                     I.DESSERIPS AS DescripcionProcMenor, 
                     D.CODPROSAL AS CodProfSaludProcMenor, 
                     PS.NOMMEDICO AS NombreProfSaludProcMenor
              FROM dbo.HCDOCUMAD AS D
                   INNER JOIN dbo.INCUPSIPS AS I ON D.CODSERIPS = I.CODSERIPS
                   INNER JOIN dbo.INPROFSAL AS PS ON D.CODPROSAL = PS.CODPROSAL
              WHERE D.FECPROCES >= @fechaInicial
                    AND D.FECPROCES < @fechaFinal
                    AND D.NOMARCADJ IS NOT NULL)
          SELECT AGA.CODAUTONU AS CodAgenda, 
                 AGA.FECHORAIN AS FechaCita, 
                 AGA.CODESPECI AS CodEspecialidadAgenda, 
                 ES.DESESPECI AS EspecialidadAgenda, 
                 AGA.IPCODPACI AS IdPaciente, 
                 P.IPNOMCOMP AS NombrePaciente, 
                 AGA.CODPROSAL AS CodProfesionalAgenda, 
                 PS.NOMMEDICO AS ProfesionalSaludAgenda, 
                 AGA.FECHORAIN AS HoraInicioConsulta, 
                 AGAC.DESACTMED AS ActividadMedica, 
                 AGA.FECREGSIS AS FechaRegistroAgenda, 
                 AGA.FECITADES AS FechaDeseada,
                 CASE AGA.CODESTCIT
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
                 END AS 'EstadoCita', 
                 HC.FECHISPAC AS FechaHC, 
                 HC.CODESPTRA AS CodEspecialidadHC, 
                 ES2.DESESPECI AS EspecialidadHC,
                 CASE
                     WHEN F.FechaFactura IS NOT NULL
                     THEN 'SI'
                     ELSE 'NO'
                 END AS Factura, 
                 F.FechaFactura, 
                 PM.FechaProcMenor, 
                 PM.CodProcMenor, 
                 PM.DescripcionProcMenor, 
                 PM.NombreProfSaludProcMenor
          FROM dbo.AGASICITA AS AGA
               INNER JOIN dbo.INPACIENT AS P ON AGA.IPCODPACI = P.IPCODPACI
               INNER JOIN dbo.INESPECIA AS ES ON AGA.CODESPECI = ES.CODESPECI
               INNER JOIN dbo.INPROFSAL AS PS ON AGA.CODPROSAL = PS.CODPROSAL
               INNER JOIN dbo.AGACTIMED AS AGAC ON AGA.CODACTMED = AGAC.CODACTMED
               LEFT OUTER JOIN dbo.HCHISPACA AS HC ON AGA.IPCODPACI = HC.IPCODPACI
                                                      AND DATEDIFF(DAY, HC.FECHISPAC, AGA.FECHORAIN) = 0
               LEFT OUTER JOIN dbo.INESPECIA AS ES2 ON HC.CODESPTRA = ES2.CODESPECI
               LEFT OUTER JOIN F ON AGA.IPCODPACI = F.PatientCode
                                    AND DATEDIFF(DAY, F.FechaFactura, AGA.FECHORAIN) = 0
               LEFT OUTER JOIN PM ON AGAC.CODSERIPS = PM.CodProcMenor
                                     AND CAST(AGA.FECHORAIN AS DATE) = CAST(PM.FechaProcMenor AS DATE)
                                     AND AGA.IPCODPACI = PM.IdPcteProcMenor
          WHERE AGA.FECHORAIN >= @fechaInicial
                AND AGA.FECHORAIN < @fechaFinal --AND HC.FECHISPAC IS NULL
          ORDER BY DAY(AGA.FECHORAIN), 
                   ES.DESESPECI;
     RETURN 0;
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de reportería que cruza citas médicas agendadas con historia clínica, facturación y procedimientos menores (CUPS) para un rango de fechas dado. Por cada cita del período, muestra si el paciente tiene historia clínica registrada el mismo día, si se generó una factura de cobro en esa fecha, y si existen procedimientos menores asociados provenientes de hallazgos clínicos (HCINFPROM) o documentos adjuntos (HCDOCUMAD). Permite auditar la trazabilidad asistencial y de facturación de una consulta: desde que el paciente es agendado hasta que se atiende, se documenta en historia clínica y se cobra. Se utiliza para conciliación entre agendamiento, prestación del servicio y generación de factura, identificando citas cumplidas sin factura o sin historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'USP_AgendamientoHCfactura';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'USP_AgendamientoHCfactura';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un reporte consolidado de citas agendadas en un rango de fechas, cruzándolas con historia clínica, facturación y procedimientos menores realizados el mismo día.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_AgendamientoHCfactura';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El rango de fechas (inicial y final) debe estar definido; se filtra con InvoiceDate/FECREAPRO/FECPROCES/FECHORAIN >= inicial y < final.; Las citas deben tener paciente, especialidad, profesional y actividad médica registrados (INNER JOIN obligatorios).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_AgendamientoHCfactura';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran facturas con PatientCode no nulo.; El cruce con HC, factura y procedimiento menor se hace exigiendo coincidencia exacta de día (DATEDIFF DAY = 0) con la fecha/hora de inicio de la cita.; La coincidencia de procedimiento menor exige igualdad simultánea de paciente, código de servicio (CUPS) y fecha.; Los procedimientos menores se obtienen tanto desde notas clínicas (HCINFPROM) como desde documentos adjuntos con archivo (HCDOCUMAD), unificados vía UNION ALL.; El reporte siempre incluye todas las citas del rango aunque no tengan HC, factura ni procedimiento menor (LEFT JOIN).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_AgendamientoHCfactura';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cita médica; Estado de cita (Asignada, Cumplida, Incumplida, PreAsignada, Cancelada); Especialidad médica; Profesional de la salud; Paciente; Historia clínica; Factura; Procedimiento menor; Actividad médica; Documento adjunto de HC; Código CUPS / servicio IPS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_AgendamientoHCfactura';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve una fila por cita en el rango con su estado, datos de HC del mismo día, indicador de factura del mismo día y procedimiento menor coincidente por código/fecha/paciente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_AgendamientoHCfactura';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si AGA.CODESTCIT IN (0,1,2,3,4) → Traduce el código de estado de cita a etiqueta: 0=Asignada, 1=Cumplida, 2=Incumplida, 3=PreAsignada, 4=Cancelada.; si F.FechaFactura IS NOT NULL (existe factura del paciente el mismo día de la cita) → Marca la columna Factura como ''SI'' else Marca la columna Factura como ''NO''; si En HCDOCUMAD: D.NOMARCADJ IS NOT NULL → Solo se consideran documentos adjuntos de HC que tengan archivo adjunto asociado para el cruce de procedimientos menores.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_AgendamientoHCfactura';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.Invoice; dbo.HCINFPROM; dbo.INCUPSIPS; dbo.INPROFSAL; dbo.HCDOCUMAD; dbo.AGASICITA; dbo.INPACIENT; dbo.INESPECIA; dbo.AGACTIMED; dbo.HCHISPACA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_AgendamientoHCfactura';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_AgendamientoHCfactura';
-- GO
