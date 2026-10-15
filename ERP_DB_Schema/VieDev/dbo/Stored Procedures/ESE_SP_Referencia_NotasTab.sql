CREATE PROCEDURE [dbo].[ESE_SP_Referencia_NotasTab] @FechaIni DATETIME, 
                                                   @FechaFin DATETIME
AS
     SELECT ENT.NOMENTIDA AS 'Entidad',
            CASE
                WHEN CGR.EntityType = 1
                THEN 'Contributivo'
                WHEN CGR.EntityType = 2
                THEN 'Subsidiado'
                WHEN CGR.EntityType = 3
                THEN 'ET Vinculados Municipios - N'
                WHEN CGR.EntityType = 4
                THEN 'ET Vinculados Departamentos - N'
                WHEN CGR.EntityType = 5
                THEN 'ARL Riesgos Laborales - P'
                WHEN CGR.EntityType = 6
                THEN 'MP Medicina Prepagada - P'
                WHEN CGR.EntityType = 7
                THEN 'IPS Privada - P'
                WHEN CGR.EntityType = 8
                THEN 'IPS Publica - P'
                WHEN CGR.EntityType = 9
                THEN 'E'
                WHEN CGR.EntityType = 10
                THEN 'Accidentes de transito -P'
                WHEN CGR.EntityType = 11
                THEN 'Fosyga - P'
                WHEN CGR.EntityType = 12
                THEN 'Otros - N'
                WHEN CGR.EntityType = 13
                THEN 'Aseguradoras - P'
                WHEN CGR.EntityType = 99
                THEN 'Particulares - P'
            END AS 'Regimen', 
            ING.IFECHAING AS FechaIngreso, 
            T.FECREGIST AS FechaHistoria, 
            ING.NUMINGRES AS Ingreso, 
            ING.CODCENATE AS CodCentro, 
            RTRIM(cen.NOMCENATE) AS CentroAtencion, 
            RTRIM(PAC.IPPRINOMB) AS PrimerNombre, 
            RTRIM(PAC.IPSEGNOMB) AS SegundoNombre, 
            RTRIM(PAC.IPPRIAPEL) AS PrimerApellido, 
            RTRIM(PAC.IPSEGAPEL) AS SegundoApellido,
            CASE PAC.IPTIPODOC
                WHEN 1
                THEN 'CC'
                WHEN 2
                THEN 'CE'
                WHEN 3
                THEN 'TI'
                WHEN 4
                THEN 'RC '
                WHEN 5
                THEN 'PA'
                WHEN 6
                THEN 'AS'
                WHEN 7
                THEN 'MS'
                WHEN 8
                THEN 'NUIP'
                WHEN 12
                THEN 'PEP'
            END AS TipoIdentificacion, 
            RTRIM(ING.IPCODPACI) AS Identificacion, 
            CAST(PAC.IPFECNACI AS DATE) AS FechaNacimiento, 
            YEAR([Common].[GETDATE]()) - YEAR(PAC.IPFECNACI) AS Edad,
            CASE PAC.IPSEXOPAC
                WHEN 1
                THEN 'M'
                WHEN 2
                THEN 'F'
            END AS Sexo, 
            PAC.IPTELMOVI AS Celular, 
            PAC.IPTELEFON AS TelefonoFijo,
            CASE PAC.IPESTADOC
                WHEN 1
                THEN 'Soltero'
                WHEN 2
                THEN 'Casado'
                WHEN 3
                THEN 'Viudo'
                WHEN 4
                THEN 'UnionLibre'
                WHEN 5
                THEN 'Separado'
            END AS EstadoCivil, 
            TITNOTENF AS Titulo, 
            NOTENFSUB AS Subjetivo, 
            NOTENFOBJ AS Objetivo, 
            NOTENFANA AS Analisis, 
            DIAG.Diagnostico1
     FROM.HCCTRNOTE AS T
         INNER JOIN.ADINGRESO AS ING ON ING.NUMINGRES = T.NUMINGRES
         INNER JOIN.INPACIENT AS PAC WITH(NOLOCK) ON T.IPCODPACI = PAC.IPCODPACI
         INNER JOIN.INENTIDAD AS ENT WITH(NOLOCK) ON ING.CODENTIDA = ENT.CODENTIDA
         INNER JOIN.ADCENATEN AS Cen WITH(NOLOCK) ON Cen.CODCENATE = ING.CODCENATE
         INNER JOIN.INPROFSAL AS S WITH(NOLOCK) ON S.CODPROSAL = T.CODPROSAL
         INNER JOIN Contract.CareGroup AS CGR WITH(NOLOCK) ON ING.GENCAREGROUP = CGR.Id
         INNER JOIN.DWHDiagnosticosTotal AS DIAG WITH(NOLOCK) ON DIAG.NUMINGRES = ING.NUMINGRES
     WHERE(CODNIVIMP = 'C')
          AND CAST(T.FECREGIST AS DATE) BETWEEN @FechaIni AND @FechaFin;