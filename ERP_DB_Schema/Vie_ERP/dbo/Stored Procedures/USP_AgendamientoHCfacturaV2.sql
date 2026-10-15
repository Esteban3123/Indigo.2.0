
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