
--DROP PROCEDURE [dbo].[USP_AgendamientoHCfacturaV2]

CREATE PROCEDURE [dbo].[USP_AgendamientoHCfacturaV2] @fechaInicial DATETIME, 
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
-- EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Procedimiento que consolida, para un rango de fechas, información de citas agendadas cruzándola con historia clínica del mismo día, facturación emitida y procedimientos menores (registrados en evoluciones clínicas o documentos adjuntos). Permite identificar si cada cita tuvo factura generada, nota clínica asociada y procedimientos CUPS realizados, junto con el estado de la cita (Asignada, Cumplida, Incumplida, etc.), la especialidad y el profesional de salud. Sirve como reporte de seguimiento y conciliación entre agendamiento, atención clínica y facturación.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_AgendamientoHCfacturaV2';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_AgendamientoHCfacturaV2';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un reporte consolidado de citas agendadas en un rango de fechas, cruzándolas con historia clínica, facturación y procedimientos menores ejecutados el mismo día por el mismo paciente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_AgendamientoHCfacturaV2';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El rango de fechas (inicial y final) debe estar definido; se aplica como [inicial, final).; Las citas deben tener paciente, especialidad, profesional y actividad médica válidos en sus catálogos para aparecer en el resultado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_AgendamientoHCfacturaV2';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran citas cuya fecha/hora de inicio cae dentro del rango [fechaInicial, fechaFinal).; Solo se consideran facturas con PatientCode no nulo dentro del rango.; El cruce con HC, factura y procedimiento menor se realiza exigiendo coincidencia del mismo día calendario (DATEDIFF DAY = 0) y el mismo paciente.; El cruce con procedimiento menor exige además que el código de servicio (CODSERIPS) de la actividad médica de la cita coincida con el del procedimiento.; Los procedimientos menores se obtienen tanto de HCINFPROM como de HCDOCUMAD (este último solo con adjunto).; No se modifican datos: la operación es de solo lectura.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_AgendamientoHCfacturaV2';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cita médica / Agendamiento; Estado de cita (Asignada, Cumplida, Incumplida, PreAsignada, Cancelada); Historia clínica; Especialidad médica; Profesional de la salud; Paciente; Factura; Procedimiento menor; Actividad médica; Documento adjunto de historia clínica', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_AgendamientoHCfacturaV2';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RESULTSET: Devuelve un conjunto de citas del rango con datos de paciente, especialidad, profesional, estado, HC y marcas de factura/procedimiento menor el mismo día; ordenado por día y especialidad.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_AgendamientoHCfacturaV2';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si AGA.CODESTCIT en {0,1,2,3,4} → Mapea a etiquetas Asignada/Cumplida/Incumplida/PreAsignada/Cancelada respectivamente; si Existe factura del paciente el mismo día de la cita (F.FechaFactura no nula) → Marca Factura=''SI'' else Marca Factura=''NO''; si Procedimiento menor proviene de HCDOCUMAD → Solo se incluye si NOMARCADJ no es nulo (existe archivo adjunto)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_AgendamientoHCfacturaV2';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.Invoice; dbo.HCINFPROM; dbo.INCUPSIPS; dbo.INPROFSAL; dbo.HCDOCUMAD; dbo.AGASICITA; dbo.INPACIENT; dbo.INESPECIA; dbo.AGACTIMED; dbo.HCHISPACA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_AgendamientoHCfacturaV2';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_AgendamientoHCfacturaV2';
-- GO
