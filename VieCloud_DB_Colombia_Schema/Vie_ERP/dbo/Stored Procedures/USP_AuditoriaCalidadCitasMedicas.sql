CREATE PROCEDURE [dbo].[USP_AuditoriaCalidadCitasMedicas] @fechaInicial DATETIME, 
                                                         @fechaFinal   DATETIME
AS
     SELECT dbo.TipDocR256(INP.IPTIPODOC) AS TipoDocumento, 
            INP.IPCODPACI AS Identificacion, 
            INP.IPPRINOMB AS 'PrimerNombre', 
            INP.IPSEGNOMB AS 'SegundoNombre', 
            INP.IPPRIAPEL AS 'PrimerApellido', 
            INP.IPSEGAPEL AS 'SegundoApellido', 
            dbo.SexoR256(INP.IPSEXOPAC) AS Sexo, 
            CAST(INP.IPFECNACI AS DATE) AS FNacimiento, 
            INP.IPDIRECCI AS Direccion, 
            INMU.MUNNOMBRE AS Municipio, 
            INE.NOMENTIDA AS Entidad,
            CASE HA.EntityType
                WHEN 1
                THEN 'EPS Contributivo'
                WHEN 2
                THEN 'EPS Subsidiado'
                WHEN 3
                THEN 'ET Vinculados Municipios'
                WHEN 4
                THEN 'ET Vinculados Departamentos'
                WHEN 5
                THEN 'ARL Riesgos Laborales'
                WHEN 6
                THEN 'MP Medicina Prepagada'
                WHEN 7
                THEN 'IPS Privada'
                WHEN 8
                THEN 'IPS Publica'
                WHEN 9
                THEN 'Regimen Especial'
                WHEN 10
                THEN 'Accidentes de transito'
                WHEN 11
                THEN 'Fosyga'
                WHEN 12
                THEN 'Otros'
            END AS 'Tipo de entidad ', 
            HA.HealthEntityCode AS 'CodigoEntidad', 
            INP.IPTELEFON AS Tel1, 
            INP.IPTELMOVI AS Tel2, 
            INES.DESESPECI AS Especialidad, 
            INPR.NOMMEDICO AS Médico,
            CASE
                WHEN AGC.FECREGSIS IS NULL
                THEN AGA.FECREGSIS
                ELSE AGC.FECREGSIS
            END AS 'FechaSolicitud',
     --AGA.FECREGSIS AS '1SOLICITUD',--FECREGSIS
            CASE
                WHEN AGC.FECREGSIS IS NOT NULL
                THEN AGC.FECHACITA
                ELSE AGA.FECITADES
            END AS 'FechaDeseada', --FECITADES
            AGA.FECHORAIN AS 'FechaAsignada', --FECHORAIN
            DATEDIFF(d, (CASE
                             WHEN AGC.FECREGSIS IS NULL
                             THEN AGA.FECREGSIS
                             ELSE AGC.FECREGSIS
                         END), AGA.FECHORAIN) AS DiasSvsA, 
            DATEDIFF(d, (CASE
                             WHEN AGC.FECREGSIS IS NOT NULL
                             THEN AGC.FECHACITA
                             ELSE AGA.FECITADES
                         END), AGA.FECHORAIN) AS DiasDvsA, 
            AGAC.DESACTMED AS Acitvidad, 
            AGA.CODUSUASI AS 'CodigoAsignoCitaMedica', 
            prs.Fullname AS 'NombreAsignoCitaMedica',
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
            CASE
                WHEN AGC.IDCITA IS NULL
                THEN 'NO'
                ELSE 'SI'
            END AS CitaEnEspera, 
            AGA.FECREGSIS AS SolicitudAgend, 
            AGA.FECITADES AS DeseadaAgend, 
            AGA.FECHORAIN AS AsignacionAgend, 
            DATEDIFF(d, AGA.FECREGSIS, AGA.FECHORAIN) AS AgendDiasSvsA, 
            DATEDIFF(d, AGA.FECITADES, AGA.FECHORAIN) AS AgendDiasDvsA, 
            AGC.FECREGSIS AS SolicitudCitaEE, 
            AGC.FECHACITA AS DeseadaCitaEE, 
            AGA.FECHORAIN AS AsignacionAgend, 
            DATEDIFF(d, AGC.FECREGSIS, AGA.FECHORAIN) AS CitasDiasSvsA, 
            DATEDIFF(d, AGC.FECHACITA, AGA.FECHORAIN) AS CitasDiasDvsA
     FROM dbo.INPACIENT AS INP
          INNER JOIN dbo.INENTIDAD AS INE ON INP.CODENTIDA = INE.CODENTIDA
          INNER JOIN dbo.AGASICITA AS AGA ON INP.IPCODPACI = AGA.IPCODPACI
          INNER JOIN dbo.INPROFSAL AS INPR ON AGA.CODPROSAL = INPR.CODPROSAL
          INNER JOIN dbo.INESPECIA AS INES ON AGA.CODESPECI = INES.CODESPECI
          INNER JOIN dbo.INUBICACI AS INU ON INP.AUUBICACI = INU.AUUBICACI
          INNER JOIN dbo.INMUNICIP AS INMU ON INU.DEPMUNCOD = INMU.DEPMUNCOD
          INNER JOIN dbo.AGACTIMED AS AGAC ON AGA.CODACTMED = AGAC.CODACTMED
          LEFT OUTER JOIN dbo.AGCITESPE AS AGCI ON AGA.CODESPECI = AGCI.CODESPECI
                                                   AND AGA.IPCODPACI = AGCI.IPCODPACI
          INNER JOIN [Security].[User] AS usr ON usr.UserCode = AGA.CODUSUASI
          INNER JOIN [Security].[Person] AS prs ON prs.Id = usr.IdPerson
          LEFT JOIN Contract.HealthAdministrator AS HA ON HA.Id = INP.GENCONENTITY
          LEFT JOIN AGCITAESP AS AGC ON AGA.CODAUTONU = AGC.IDCITA
     WHERE AGA.FECHORAIN BETWEEN @fechaInicial AND @fechaFinal
     ORDER BY AGA.FECHORAIN;
     RETURN 0;
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reporte de auditoría y calidad de citas médicas asignadas en un rango de fechas. Consolida información del paciente (identificación, nombre, sexo, fecha de nacimiento, dirección, municipio, teléfonos y entidad aseguradora con su tipo: EPS, ARL, medicina prepagada, etc.), el profesional de salud y la especialidad de la cita, y la actividad médica programada. Calcula los días transcurridos entre la fecha de solicitud, la fecha deseada y la fecha efectivamente asignada de la cita, tanto para citas agendadas directamente como para citas en espera especializada, permitiendo medir oportunidad en la asignación de citas. También indica el estado final de la cita (asignada, cumplida, incumplida, cancelada, entre otros), el usuario que realizó la asignación y si la cita provino de una lista de espera especializada. Es utilizado por áreas de calidad y auditoría médica para evaluar el cumplimiento de tiempos de acceso y oportunidad en la atención ambulatoria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'USP_AuditoriaCalidadCitasMedicas';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'USP_AuditoriaCalidadCitasMedicas';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un reporte de auditoría de calidad sobre las citas médicas asignadas en un rango de fechas, mostrando datos del paciente, entidad, profesional, fechas clave (solicitud, deseada, asignada) y métricas de oportunidad.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_AuditoriaCalidadCitasMedicas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El rango de fechas (inicial y final) debe estar definido para filtrar citas por fecha de asignación.; El paciente debe existir en el maestro de pacientes y estar relacionado a una entidad, ubicación y municipio.; La cita debe tener profesional, especialidad y actividad médica válidos en sus catálogos.; El usuario que asignó la cita debe existir en Security.User y tener una persona asociada en Security.Person.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_AuditoriaCalidadCitasMedicas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan citas cuya fecha de asignación (FECHORAIN) cae dentro del rango parametrizado.; El resultado se ordena cronológicamente por fecha/hora de asignación de la cita.; Las métricas de oportunidad se calculan como diferencias en días entre fecha de solicitud/deseada y fecha asignada.; Si la cita proviene de una lista de espera (AGCITAESP), las fechas de solicitud y deseada se priorizan desde dicha lista sobre las del agendamiento.; Se incluyen únicamente citas con paciente, entidad, profesional, especialidad, ubicación, municipio, actividad médica y usuario asignador existentes (INNER JOINs).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_AuditoriaCalidadCitasMedicas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Cita médica; Especialidad médica; Profesional de la salud; Entidad aseguradora / administradora de salud; Tipo de entidad (EPS, ARL, IPS, Medicina Prepagada, SOAT, Fosyga, Régimen Especial); Estado de la cita (Asignada, Cumplida, Incumplida, PreAsignada, Cancelada); Lista de espera de citas especializadas; Oportunidad de la cita (días entre solicitud, fecha deseada y fecha asignada); Auditoría de calidad de agendamiento; Municipio / ubicación geográfica; Actividad médica', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_AuditoriaCalidadCitasMedicas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Cuando AGA.FECHORAIN está entre @fechaInicial y @fechaFinal, se retorna una fila por cita con datos del paciente, entidad, médico, fechas y diferencias en días.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_AuditoriaCalidadCitasMedicas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si AGC.FECREGSIS IS NULL (no existe cita en espera asociada) → FechaSolicitud toma AGA.FECREGSIS (fecha de registro del agendamiento) else FechaSolicitud toma AGC.FECREGSIS (fecha de registro de la cita en espera); si AGC.FECREGSIS IS NOT NULL (existe cita en espera previa) → FechaDeseada toma AGC.FECHACITA else FechaDeseada toma AGA.FECITADES; si AGC.IDCITA IS NULL → Marca CitaEnEspera = ''NO'' else Marca CitaEnEspera = ''SI''; si HA.EntityType según valor 1..12 → Clasifica el tipo de entidad (EPS Contributivo, EPS Subsidiado, ET Vinculados, ARL, Medicina Prepagada, IPS Privada/Pública, Régimen Especial, SOAT, Fosyga, Otros); si AGA.CODESTCIT en 0..4 → Traduce el estado de la cita a Asignada, Cumplida, Incumplida, PreAsignada o Cancelada', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_AuditoriaCalidadCitasMedicas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.TipDocR256; dbo.SexoR256', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_AuditoriaCalidadCitasMedicas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INPACIENT; dbo.INENTIDAD; dbo.AGASICITA; dbo.INPROFSAL; dbo.INESPECIA; dbo.INUBICACI; dbo.INMUNICIP; dbo.AGACTIMED; dbo.AGCITESPE; Security.User; Security.Person; Contract.HealthAdministrator; dbo.AGCITAESP', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_AuditoriaCalidadCitasMedicas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_AuditoriaCalidadCitasMedicas';
-- GO
