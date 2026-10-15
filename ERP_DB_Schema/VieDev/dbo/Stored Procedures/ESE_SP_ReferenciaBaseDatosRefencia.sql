CREATE PROCEDURE [dbo].[ESE_SP_ReferenciaBaseDatosRefencia] @FechaIni DATETIME, 
                                                           @FechaFin DATETIME
AS
     SELECT ING.NUMINGRES AS Ingreso, 
            RTRIM(R.CODCENATE) + ' - ' + RTRIM(CEN.NOMCENATE) AS Centro, 
            RTRIM(R.UFUCODIGO) + ' - ' + RTRIM(UNI.UFUDESCRI) AS UnidadRemision,
            CASE
                WHEN UNI.UFUTIPUNI = 1
                THEN 'Urgencias'
                WHEN UNI.UFUTIPUNI = 2
                THEN 'Observacion/Hospitalizacion'
                WHEN UNI.UFUTIPUNI = 15
                THEN 'Consulta Externa'
                ELSE 'Otros'
            END AS TipoUnidad,
            CASE
                WHEN R.ESTADO = 1
                THEN 'Solicitado'
                WHEN R.ESTADO = 2
                THEN 'Pendinte o Gestionando'
                WHEN R.ESTADO = 3
                THEN 'Aceptado con pediente de salida'
                WHEN R.ESTADO = 4
                THEN 'Suspendido'
                WHEN R.ESTADO = 5
                THEN 'Salio'
                WHEN R.ESTADO = 6
                THEN 'Solicitud con Pertinencia'
            END EstadoRemision, 
            NR.Nombre AS MotivoSuspension, 
            R.JUSTSUSPEN AS Justificacion, 
            R.IPCODPACI AS Identificacion,
            CASE P.IPTIPODOC
                WHEN 1
                THEN 'Cédula de Ciudadanía'
                WHEN 2
                THEN 'Cédula de Extranjería '
                WHEN 3
                THEN 'Tarjeta de Identidad '
                WHEN 4
                THEN 'Registro Civil '
                WHEN 5
                THEN 'Pasporte '
                WHEN 6
                THEN 'Adulto Sin Identificación '
                WHEN 7
                THEN 'Menor Sin Identificación '
                WHEN 8
                THEN 'Número único de identificación personal'
            END AS 'TipoIdentificacion', 
            p.IPNOMCOMP AS Paciente, 
            CONVERT(VARCHAR(10), P.IPFECNACI, 105) AS FechaNacimiento, 
            [dbo].[Edad](P.IPFECNACI, [Common].[GETDATE]()) AS Edad,
            CASE P.IPSEXOPAC
                WHEN 1
                THEN 'Masculino'
                WHEN 2
                THEN 'Femenino'
            END AS 'Genero', 
            RTRIM(ING.CODENTIDA) + ' - ' + RTRIM(ENT.NOMENTIDA) AS Entidad,
            CASE
                WHEN CGR.EntityType = 1
                THEN 'EPS Contributivo'
                WHEN CGR.EntityType = 2
                THEN 'EPS Subsidiado'
                WHEN CGR.EntityType = 3
                THEN 'ET Vinculados Municipios'
                WHEN CGR.EntityType = 4
                THEN 'ET Vinculados Departamentos'
                WHEN CGR.EntityType = 5
                THEN 'ARL Riesgos Laborales'
                WHEN CGR.EntityType = 6
                THEN 'MP Medicina Prepagada'
                WHEN CGR.EntityType = 7
                THEN 'IPS Privada'
                WHEN CGR.EntityType = 8
                THEN 'IPS Publica'
                WHEN CGR.EntityType = 9
                THEN 'Regimen Especial'
                WHEN CGR.EntityType = 10
                THEN 'Accidentes de transito'
                WHEN CGR.EntityType = 11
                THEN 'Fosyga'
                WHEN CGR.EntityType = 12
                THEN 'Otros'
                WHEN CGR.EntityType = 13
                THEN 'Aseguradoras'
                WHEN CGR.EntityType = 99
                THEN 'Particulares'
            END AS Regimen, 
            SR.Nombre AS ServicioAlQueSeRemite, 
            DIAG.CODDIAGNO + ' - ' + RTRIM(D.NOMDIAGNO) AS DiagnosticoPrincipal, 
            E.DESESPECI AS EspecialidadARemitir, 
            I.DSCRIPIPS AS EntidadDondeRemite, 
            T.FECSOLICIT AS FechaSolicitudRemision, 
            IPS.FECHCONFIR AS FechaAceptacion, 
            DATEDIFF(HOUR, T.FECSOLICIT, IPS.FECHCONFIR) AS HorasEspera, 
            PRO.NOMMEDICO AS Medico
     FROM.HCREFCONP AS R WITH(NOLOCK)
         INNER JOIN.ADINGRESO AS ING WITH(NOLOCK) ON R.NUMINGRES = ING.NUMINGRES
         INNER JOIN Contract.CareGroup AS CGR WITH(NOLOCK) ON ING.GENCAREGROUP = CGR.Id
         INNER JOIN.INPACIENT AS P WITH(NOLOCK) ON R.IPCODPACI = P.IPCODPACI
         INNER JOIN.INUNIFUNC AS UNI WITH(NOLOCK) ON UNI.UFUCODIGO = R.UFUCODIGO
         INNER JOIN.ADCENATEN AS Cen WITH(NOLOCK) ON Cen.CODCENATE = R.CODCENATE
         INNER JOIN.INENTIDAD AS ENT WITH(NOLOCK) ON ING.CODENTIDA = ENT.CODENTIDA
         LEFT OUTER JOIN
     (
         SELECT T1.IPCODPACI, 
                T1.SERVIDOREM, 
                T1.FECSOLICIT, 
                T1.NUMINGRES, 
                T1.AUTO
         FROM dbo.HCREFCONT AS T1
              INNER JOIN
         (
             SELECT MIN(FECSOLICIT) AS FS, 
                    NUMINGRES, 
                    MIN(AUTO) AS AUTO
             FROM dbo.HCREFCONT
             GROUP BY NUMINGRES
         ) AS T2 ON T2.NUMINGRES = T1.NUMINGRES
                    AND T2.FS = T1.FECSOLICIT
                    AND T2.AUTO = T1.AUTO
     ) AS T ON T.IPCODPACI = R.IPCODPACI
               AND T.NUMINGRES = R.NUMINGRES
         INNER JOIN dbo.RCSERVICIOS AS SR WITH(NOLOCK) ON SR.Codigo = T.SERVIDOREM
         INNER JOIN.INDIAGNOP AS DIAG WITH(NOLOCK) ON ING.NUMINGRES = DIAG.NUMINGRES
                                                      AND ING.CODDIAEGR = DIAG.CODDIAGNO
         INNER JOIN.INDIAGNOS D WITH(NOLOCK) ON DIAG.CODDIAGNO = D.CODDIAGNO
         INNER JOIN dbo.INESPECIA AS E WITH(NOLOCK) ON E.CODESPECI = R.CODESPECI
         LEFT OUTER JOIN
     (
         SELECT DISTINCT 
                HCREFCONPID, 
                IPSACEPTADO, 
                FECHCONFIR
         FROM dbo.HCREFCONTD AS DT WITH(NOLOCK)
              INNER JOIN.HCREFCONP AS R WITH(NOLOCK) ON DT.HCREFCONPID = R.AUTO
         WHERE R.ESTADO = 5
               AND DT.IPSACEPTADO IS NOT NULL
               AND DT.FECHCONFIR IS NOT NULL
     ) AS IPS ON IPS.HCREFCONPID = R.AUTO
         LEFT OUTER JOIN dbo.ADCONTIPS AS I WITH(NOLOCK) ON I.CODIGOIPS = IPS.IPSACEPTADO
         INNER JOIN.INPROFSAL AS PRO WITH(NOLOCK) ON PRO.CODPROSAL = R.CODPROSAL
         LEFT OUTER JOIN.RCMOTNOREF AS NR WITH(NOLOCK) ON NR.Id = R.RCMOTNOREFID
     WHERE DIAG.CODDIAPRI = 1
           AND T.FECSOLICIT >= @FechaIni
           AND T.FECSOLICIT <= @FechaFin;