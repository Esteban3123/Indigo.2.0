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