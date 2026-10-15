CREATE PROCEDURE [dbo].[SP_ReferenciaGestionPaciente]
AS
     SELECT R.NUMINGRES AS Ingreso, 
            R.IPCODPACI AS Identificacion, 
            p.IPNOMCOMP AS Paciente, 
            [dbo].[Edad](P.IPFECNACI, [Common].[GETDATE]()) AS Edad,
            CASE
                WHEN RD2.FECSEGUIMIENTO IS NULL
                THEN RD.FECSEGUIMIENTO
                ELSE RD2.FECSEGUIMIENTO
            END AS FechaGestion,
            CASE
                WHEN RD.TIPENTIDAD = 1
                THEN IPS.DSCRIPIPS
                WHEN RD.TIPENTIDAD = 2
                THEN ENT2.NOMENTIDA
                WHEN RD.TIPENTIDAD = 3
                THEN 'CRUE'
                WHEN RD.TIPENTIDAD = 4
                THEN RD.OTRAENTIDAD
            END AS EntidadGestion,
            CASE
                WHEN RD2.NOMCONTACT IS NULL
                THEN RD.NOMCONTACT
                ELSE RD2.NOMCONTACT
            END AS ContactoGestion,
            CASE
                WHEN RD.REGESTADO = '1'
                THEN 'SI acepta'
                WHEN RD.REGESTADO = '2'
                THEN 'No Acepta'
                WHEN RD.REGESTADO = '3'
                THEN 'Pendiente'
                WHEN RD.REGESTADO = '4'
                THEN 'No Aplica'
            END AS EstadoGestion, 
            RD.OBSERVACI AS Observaciones, 
            RD.CODUSUAREG + ' - ' + U.NOMUSUARI AS UsuarioGestion, 
            RD.FECSEGUIMIENTO AS FechaSeguimiento, 
            RD.NOMCONTACT AS ContactoSeguimiento
     FROM.HCREFCONP AS R WITH(NOLOCK)
         INNER JOIN.ADINGRESO AS ING WITH(NOLOCK) ON R.NUMINGRES = ING.NUMINGRES
         INNER JOIN.INPACIENT AS P WITH(NOLOCK) ON R.IPCODPACI = P.IPCODPACI
         INNER JOIN.INUNIFUNC AS UNI WITH(NOLOCK) ON UNI.UFUCODIGO = R.UFUCODIGO
         INNER JOIN.ADCENATEN AS Cen WITH(NOLOCK) ON Cen.CODCENATE = R.CODCENATE
         INNER JOIN.INENTIDAD AS ENT ON ING.CODENTIDA = ENT.CODENTIDA
         INNER JOIN.INDIAGNOS AS D WITH(NOLOCK) ON ING.CODDIAING = D.CODDIAGNO
         INNER JOIN.HCREFCONTD AS RD WITH(NOLOCK) ON R.AUTO = RD.HCREFCONPID
         LEFT OUTER JOIN.HCREFCONTD AS RD2 WITH(NOLOCK) ON RD2.ID = RD.HCREFCONTDID
         LEFT OUTER JOIN.INENTIDAD ENT2 WITH(NOLOCK) ON RD.CODENTIDA = ENT2.CODENTIDA
         LEFT JOIN.ADCONTIPS AS IPS WITH(NOLOCK) ON IPS.CODIGOIPS = RD.IPSSELECCION
         LEFT OUTER JOIN.SEGusuaru AS U ON RD.CODUSUAREG = U.CODUSUARI

/*LEFT JOIN
.SEGusuaru AS U2 ON RD2.CODUSUAREG =U2.CODUSUARI*/

     WHERE R.ESTADO = 2 --and R.IPCODPACI in ('26555868')-- AND RD.ID=27062
     ORDER BY CASE
                  WHEN RD2.FECSEGUIMIENTO IS NULL
                  THEN RD.FECSEGUIMIENTO
                  ELSE RD2.FECSEGUIMIENTO
              END, 
              RD.FECHCREREG;