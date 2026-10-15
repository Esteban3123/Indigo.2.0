CREATE PROCEDURE [dbo].[SPHC_AtencionUrgencias_2]
(@CentroAtencion CHAR(10), 
 @Usuario        CHAR(20), 
 @UFuncional     CHAR(20)
)
AS
    BEGIN
        SET NOCOUNT ON;

        /*Se identifica si la unidad de la atención es prioritaria o urgencias*/

        DECLARE @AtencionPrioritaria BIT=
        (
            SELECT CASE UFUTIPUNI
                       WHEN 31
                       THEN 1
                       ELSE 0
                   END
            FROM INUNIFUNC WITH(NOLOCK)
            WHERE UFUCODIGO = @UFuncional
        );
        DECLARE @Estado_3 AS INT= 3;
        DECLARE @Estado_4 AS INT= 4;
        DECLARE @Fecha DATE= DATEADD(DD, -8, Common.GetDate());

        IF @AtencionPrioritaria <> 1
            BEGIN
                --Ingresos bloqueados
                WITH Tmp_Bloqueo
                     AS (SELECT NUMDOCUME
                         FROM dbo.INDOCUMEN WITH(NOLOCK)
                         WHERE CODDOCUME = @Estado_3
                               AND CODUSUARI <> @Usuario)
                --Cargue ADTRIAGEU
                ,
                     Tmp_ADTRIAGEU
                     AS (SELECT DISTINCT 
					            A.TRIORIGEN, 
                                A.NUMINGRES, 
                                A.CODPROSAL, 
                                A.CODCONCEC, 
                                A.CODESPECI, 
                                A.TRIANUMER, 
                                A.TRIAFECHA, 
                                A.TRIAGECLA, 
                                A.TRIACATEG,
								A.FECHINITR
                         FROM dbo.ADTRIAGEU AS A WITH(NOLOCK)
                              --LEFT JOIN Tmp_Bloqueo AS B ON A.NUMINGRES = B.NUMDOCUME
                              INNER JOIN (SELECT  TRIANUMER, MAX(FECHINITR) fecha_max FROM ADTRIAGEU GROUP BY TRIANUMER) c ON a.TRIANUMER = c.TRIANUMER AND a.FECHINITR = c.fecha_max
                         WHERE A.CODCENATE = @CentroAtencion
                               AND A.NUMINGRES IS NOT NULL
                               AND A.TRIAFECHA >= @Fecha
                               --AND B.NUMDOCUME IS NULL
							   )
                     --Acompañantes
                     ,
                     Tmp_Acompanantes
                     AS (SELECT AC.NUMINGRES, 
                                AC.IPCODPACI, 
                                COUNT(1) AS ACOMPANANTES
                         FROM dbo.ADACOMPAN AS AC WITH(NOLOCK)
                              INNER JOIN Tmp_ADTRIAGEU AS A ON A.NUMINGRES = AC.NUMINGRES
                         GROUP BY AC.NUMINGRES, 
                                  AC.IPCODPACI)
                     --Cargue Ingresos
                     ,
                     Tmp_ADINGRESO
                     AS (SELECT K.NUMINGRES, 
                                K.CODTIPPAC AS Tipo, 
                                K.VIVESOLO, 
                                K.IPCODPACI
                         FROM dbo.ADINGRESO AS K WITH(NOLOCK)
                              INNER JOIN Tmp_ADTRIAGEU AS A ON A.NUMINGRES = K.NUMINGRES
                         -- WHERE K.IFECHAING >= @Fecha;
                         )
                     --Cargue poblacion espacial
                     ,
                     Tmp_PobEspecial
                     AS (SELECT Z.IPCODPACI, 
                                COUNT(1) AS POBESPECIAL
                         FROM dbo.ADPOBESPEPAC AS Z WITH(NOLOCK)
                              INNER JOIN ADPOBESPE AS X WITH(NOLOCK) ON X.ID = Z.IDADPOBESPE
                              INNER JOIN Tmp_ADINGRESO AS A ON A.IPCODPACI = Z.IPCODPACI
                         WHERE X.TIPOPOESPERIES = 1
                         GROUP BY Z.IPCODPACI)
                     --Cargue ADCONTURG
                     ,
                     Tmp_ADCONTURG
                     AS (SELECT B.CODCONCEC, 
                                B.UFUCODIGO, 
                                B.IPCODPACI
                         FROM dbo.ADCONTURG AS B WITH(NOLOCK)
                         WHERE B.CONESTADO = @Estado_4
                               AND B.IPFECLLEGA >= @Fecha)
                     --Cargue info Paciente
                     ,
                     Tmp_INPACIENT
                     AS (SELECT F.IPNOMCOMP, 
                                F.IPTIPODOC, 
                                F.ZONAPARTADA, 
                                L.RIESGOAGRE, 
                                RTRIM(F.CODENTIDA) + '-' + RTRIM(M.NOMENTIDA) AS EntidadPaciente, 
                                F.IPCODPACI, 
                                F.IPFECNACI
                         FROM dbo.INPACIENT AS F WITH(NOLOCK)
                              INNER JOIN Tmp_ADCONTURG AS B ON B.IPCODPACI = F.IPCODPACI
                              LEFT JOIN dbo.ADACTIVID AS L WITH(NOLOCK) ON F.CODACTIVI = L.codactivi
                              LEFT JOIN dbo.INENTIDAD AS M WITH(NOLOCK) ON M.CODENTIDA = F.CODENTIDA)
                     --Select respuesta
                     ,
                     Tmp_Respuesta
                     AS (SELECT B.UFUCODIGO, 
                                A.TRIORIGEN,
                                CASE A.TRIORIGEN
                                    WHEN '1'
                                    THEN '1 - Pacientes Medicina General'
                                    WHEN '2'
                                    THEN '2 - Pacientes Medicina Especializada'
                                END AS Origen, 
                                K.Tipo, 
                                A.TRIANUMER AS Consecutivo, 
                                RTRIM(E.TRIANOMCA) AS [Dx Sindromatico], 
                                RTRIM(B.IPCODPACI) AS Identificacion, 
                                RTRIM(F.IPNOMCOMP) AS [Nombre Paciente], 
                                A.TRIAFECHA AS [Fecha Triage], 
                                0 AS Minutos, 
                                0 AS Barra,
                                CASE A.TRIAGECLA
                                    WHEN 1
                                    THEN '1 - REANIMACIÓN'
                                    WHEN 2
                                    THEN '2 - EMERGENCIA'
                                    WHEN 3
                                    THEN '3 - URGENCIA MÉDICA'
                                    WHEN 4
                                    THEN '4 - URGENCIA DIFERIDA'
                                    WHEN 5
                                    THEN '5 - NO URGENTE'
                                END AS Triage, 
                                CAST(0 AS BIT) AS Confirmado, 
                                CAST(0 AS BIT) AS Ausente, 
                                A.NUMINGRES AS Ingreso, 
                                F.IPFECNACI AS 'Fecha Nacimiento', 
                                CAST('' AS CHAR(50)) AS Edad, 
                                RTRIM(G.DESESPECI) AS Especialidad,
                                CASE
                                    WHEN F.IPTIPODOC IN(6, 7)
                                    THEN 1
                                    ELSE 0
                                END AS ASMS, 
                                F.ZONAPARTADA, 
                                F.RIESGOAGRE, 
                                PE.POBESPECIAL AS POBESPECIAL, 
                                K.VIVESOLO, 
                                AC.ACOMPANANTES AS ACOMPANANTES, 
                                CONVERT(BIT, 0) AS Riesgo, 
                                F.EntidadPaciente
                         FROM Tmp_ADTRIAGEU AS A
							  INNER JOIN Tmp_ADCONTURG AS B ON A.CODCONCEC = B.CODCONCEC
                              INNER JOIN Tmp_ADINGRESO AS K ON A.NUMINGRES = K.NUMINGRES
							  INNER JOIN Tmp_INPACIENT AS F ON B.IPCODPACI = F.IPCODPACI
                              left JOIN dbo.ADCATTRIU AS E WITH(NOLOCK) ON E.TRIACATEG = A.TRIACATEG
                              LEFT JOIN DBO.INESPECIA AS G WITH(NOLOCK) ON A.CODESPECI = G.CODESPECI
                              LEFT JOIN Tmp_PobEspecial AS PE ON K.IPCODPACI = PE.IPCODPACI
                              LEFT JOIN Tmp_Acompanantes AS AC ON A.NUMINGRES = AC.NUMINGRES
                         --ORDER BY A.TRIAGECLA
                         )
                     SELECT *
                     FROM Tmp_Respuesta
                     ORDER BY Triage;
			END
    

    END