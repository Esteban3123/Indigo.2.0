CREATE PROCEDURE [dbo].[USP_AgendamientoHCfacturaV3] @fechaInicial DATETIME, 
                                                    @fechaFinal   DATETIME
AS
     WITH F
          AS (SELECT DISTINCT 
                     BI.PatientCode, 
                     CAST(BI.InvoiceDate AS DATE) AS FechaFactura
              FROM [Billing].[Invoice] AS BI
              WHERE BI.InvoiceDate >= @fechaInicial
                    AND BI.InvoiceDate < @fechaFinal
                    AND BI.PatientCode IS NOT NULL
              UNION ALL
              SELECT DISTINCT 
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
                 RTRIM(P.IPNOMCOMP) + CONCAT(' (', P.IPTELEFON + ' - ', P.IPTELMOVI, ')') AS NombrePaciente, 
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
                 H.FechaHC, 
                 H.NumeroAnotacionesHC,
                 CASE
                     WHEN F.FechaFactura IS NOT NULL
                     THEN 'SI'
                     ELSE 'NO'
                 END AS Factura, 
                 F.FechaFactura, 
                 PM.FechaProcMenor, 
                 PM.CodProcMenor, 
                 PM.DescripcionProcMenor, 
                 PM.NombreProfSaludProcMenor, 
                 P.IPFECNACI AS FechaNacimiento, 
                 dbo.Edad(CAST(P.IPFECNACI AS DATE), CAST(AGA.FECHORAIN AS DATE)) AS Edad2, 
                 --dbo.UnidadesEdad(CAST(P.IPFECNACI AS DATE), CAST(AGA.FECHORAIN AS DATE)) 
				 ''AS UnidadesEdad2
          FROM dbo.AGASICITA AS AGA
               INNER JOIN dbo.INPACIENT AS P ON AGA.IPCODPACI = P.IPCODPACI
               INNER JOIN dbo.INESPECIA AS ES ON AGA.CODESPECI = ES.CODESPECI
               INNER JOIN dbo.INPROFSAL AS PS ON AGA.CODPROSAL = PS.CODPROSAL
               INNER JOIN dbo.AGACTIMED AS AGAC ON AGA.CODACTMED = AGAC.CODACTMED
               LEFT OUTER JOIN F ON AGA.IPCODPACI = F.PatientCode
                                    AND DATEDIFF(DAY, F.FechaFactura, AGA.FECHORAIN) = 0
               LEFT OUTER JOIN PM ON AGAC.CODSERIPS = PM.CodProcMenor
                                     AND CAST(AGA.FECHORAIN AS DATE) = CAST(PM.FechaProcMenor AS DATE)
                                     AND AGA.IPCODPACI = PM.IdPcteProcMenor
               OUTER APPLY
          (
              SELECT CAST(HC.FECHISPAC AS DATE) AS FechaHC, 
                     COUNT(*) AS NumeroAnotacionesHC
              FROM dbo.HCHISPACA AS HC
              WHERE AGA.IPCODPACI = HC.IPCODPACI
                    AND DATEDIFF(DAY, HC.FECHISPAC, AGA.FECHORAIN) = 0
              GROUP BY HC.IPCODPACI, 
                       CAST(HC.FECHISPAC AS DATE)
          ) AS H
          WHERE AGA.FECHORAIN >= @fechaInicial
                AND AGA.FECHORAIN < @fechaFinal --AND HC.FECHISPAC IS NULL  
          ORDER BY DAY(AGA.FECHORAIN), 
                   ES.DESESPECI;
     RETURN 0;
GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Procedimiento de reporte que, dado un rango de fechas, cruza las citas médicas agendadas con facturas de facturación, notas de historia clínica y procedimientos menores (CUPS) registrados el mismo día, para cada paciente. Devuelve por cita: estado, especialidad, profesional, datos del paciente con teléfono y edad calculada, e indicadores de si existió factura y/o procedimiento menor asociado, facilitando auditoría de productividad asistencial y facturación.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_AgendamientoHCfacturaV3';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_AgendamientoHCfacturaV3';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un reporte consolidado de citas agendadas en un rango de fechas, cruzando cada cita con su historia clínica del día, factura emitida y procedimientos menores realizados al paciente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_AgendamientoHCfacturaV3';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El rango de fechas (inicial y final) debe estar definido para acotar citas, facturas, procedimientos menores e historia clínica.; Deben existir relaciones íntegras entre citas, pacientes, especialidades, profesionales de salud y actividades médicas (INNER JOIN obliga su existencia).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_AgendamientoHCfacturaV3';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen citas cuya fecha/hora de inicio esté dentro del rango [fechaInicial, fechaFinal).; El cruce con factura exige coincidencia de paciente y mismo día calendario entre la fecha de la factura y la fecha de la cita.; El cruce con procedimiento menor exige coincidencia de paciente, mismo día y que el código de servicio de la actividad médica de la cita sea igual al código del procedimiento.; Las anotaciones de historia clínica contadas son las del mismo paciente y mismo día calendario que la cita.; Solo se consideran facturas con PatientCode no nulo.; Los procedimientos menores provienen de dos fuentes: notas clínicas (HCINFPROM) y documentos adjuntos con archivo (HCDOCUMAD).; La edad se calcula a la fecha de la cita, no a la fecha actual.; El campo UnidadesEdad2 siempre se devuelve vacío (lógica comentada).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_AgendamientoHCfacturaV3';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cita médica; Agendamiento; Estado de cita (Asignada/Cumplida/Incumplida/PreAsignada/Cancelada); Especialidad médica; Profesional de salud; Actividad médica; Historia clínica; Anotaciones de historia clínica; Factura; Procedimiento menor; Documento adjunto a historia clínica; Paciente; Edad del paciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_AgendamientoHCfacturaV3';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Retorna una fila por cita cuya FECHORAIN esté en [fechaInicial, fechaFinal), enriquecida con HC del mismo día, factura del mismo día y procedimiento menor del mismo día/paciente/servicio.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_AgendamientoHCfacturaV3';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si AGA.CODESTCIT = 0/1/2/3/4 → Traduce el código de estado de la cita a ''Asignada''/''Cumplida''/''Incumplida''/''PreAsignada''/''Cancelada'' respectivamente.; si F.FechaFactura IS NOT NULL (existe factura del paciente el mismo día de la cita) → Marca la columna Factura como ''SI''. else Marca la columna Factura como ''NO''.; si En el CTE PM, fuente HCDOCUMAD requiere D.NOMARCADJ IS NOT NULL → Solo se consideran procedimientos menores documentados en HCDOCUMAD si tienen archivo adjunto asociado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_AgendamientoHCfacturaV3';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.Edad', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_AgendamientoHCfacturaV3';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.Invoice; dbo.HCINFPROM; dbo.INCUPSIPS; dbo.INPROFSAL; dbo.HCDOCUMAD; dbo.AGASICITA; dbo.INPACIENT; dbo.INESPECIA; dbo.AGACTIMED; dbo.HCHISPACA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_AgendamientoHCfacturaV3';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_AgendamientoHCfacturaV3';
-- GO
