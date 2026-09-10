CREATE PROCEDURE [dbo].[SPHC_AtencionUrgencias_1]
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
							  LEFT JOIN Tmp_ADCONTURG AS B ON A.CODCONCEC = B.CODCONCEC
                              LEFT JOIN Tmp_ADINGRESO AS K ON A.NUMINGRES = K.NUMINGRES
							  LEFT JOIN Tmp_INPACIENT AS F ON B.IPCODPACI = F.IPCODPACI
                              left JOIN dbo.ADCATTRIU AS E WITH(NOLOCK) ON E.TRIACATEG = A.TRIACATEG
                              LEFT JOIN DBO.INESPECIA AS G WITH(NOLOCK) ON A.CODESPECI = G.CODESPECI
                              LEFT JOIN Tmp_PobEspecial AS PE ON K.IPCODPACI = PE.IPCODPACI
                              LEFT JOIN Tmp_Acompanantes AS AC ON A.NUMINGRES = AC.NUMINGRES
                         --ORDER BY A.TRIAGECLA
                         )

						 --SELECT DISTINCT 
					  --          A.TRIORIGEN, 
       --                         A.NUMINGRES, 
       --                         A.CODPROSAL, 
       --                         A.CODCONCEC, 
       --                         A.CODESPECI, 
       --                         A.TRIANUMER, 
       --                         A.TRIAFECHA, 
       --                         A.TRIAGECLA, 
       --                         A.TRIACATEG,
							--	A.FECHINITR
       --                  FROM dbo.ADTRIAGEU AS A WITH(NOLOCK)
       --                       --LEFT JOIN Tmp_Bloqueo AS B ON A.NUMINGRES = B.NUMDOCUME
       --                       INNER JOIN (SELECT  TRIANUMER, MAX(FECHINITR) fecha_max FROM ADTRIAGEU GROUP BY TRIANUMER) c ON a.TRIANUMER = c.TRIANUMER AND a.FECHINITR = c.fecha_max
       --                  WHERE A.CODCENATE = @CentroAtencion
       --                        AND A.NUMINGRES IS NOT NULL
       --                        AND A.TRIAFECHA >= @Fecha
							--   AND A.NUMINGRES = '574'

							--   SELECT B.CODCONCEC, 
       --                         B.UFUCODIGO, 
       --                         B.IPCODPACI
       --                  FROM dbo.ADCONTURG AS B WITH(NOLOCK)
       --                  WHERE B.CONESTADO = @Estado_4
       --                        AND B.IPFECLLEGA >= @Fecha
							--   AND B.IPCODPACI = '1141368794'
                     SELECT *
                     FROM Tmp_Respuesta
                     ORDER BY Triage;
			END
    

    END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que genera el tablero de atención de urgencias y atención prioritaria para un centro de atención y unidad funcional específicos. Consolida información de triage (ADTRIAGEU), ingresos (ADINGRESO), contactos de urgencias (ADCONTURG), datos del paciente (INPACIENT) y acompañantes (ADACOMPAN) para mostrar el estado actual de los pacientes en sala de urgencias de los últimos 8 días. Determina si la unidad es de atención prioritaria o urgencias (según tipo de unidad funcional en INUNIFUNC), e incluye clasificación de triage, diagnóstico sindrómico, tiempo de espera, población especial y riesgo de agresividad del paciente. Se usa para el monitoreo en tiempo real de la sala de urgencias por parte del personal asistencial y administrativo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_AtencionUrgencias_1';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_AtencionUrgencias_1';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los pacientes en sala de urgencias con su triage vigente, datos demográficos, riesgos y acompañantes para tableros de seguimiento, excluyendo unidades funcionales catalogadas como atención prioritaria.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_AtencionUrgencias_1';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La unidad funcional debe existir en INUNIFUNC para determinar si es prioritaria (UFUTIPUNI=31) o de urgencias.; Solo se ejecuta la consulta cuando la unidad funcional NO es prioritaria (UFUTIPUNI<>31).; Solo se consideran triages con fecha (TRIAFECHA) dentro de los últimos 8 días respecto a Common.GetDate().; Solo se consideran contactos de urgencia (ADCONTURG) en estado 4 con IPFECLLEGA dentro de los últimos 8 días.; Los registros de triage deben tener NUMINGRES no nulo y pertenecer al centro de atención indicado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_AtencionUrgencias_1';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Por cada TRIANUMER se toma únicamente el registro de triage con la fecha FECHINITR más reciente (último triage vigente).; Solo se incluyen pacientes cuyo contacto de urgencia esté en estado 4 (espera/activo).; Solo se contabilizan poblaciones especiales con TIPOPOESPERIES=1 (riesgo).; La ventana temporal de búsqueda es fija de 8 días previos a la fecha actual.; El procedimiento es de solo lectura: usa WITH(NOLOCK) en todas las tablas y no modifica datos.; El CTE Tmp_Bloqueo se define pero no se usa (filtro de ingresos bloqueados está comentado), por lo que no se excluyen ingresos con documentos en estado 3.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_AtencionUrgencias_1';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Triage de urgencias; Clasificación de triage (Reanimación/Emergencia/Urgencia médica/Urgencia diferida/No urgente); Atención prioritaria; Ingreso/Admisión; Acompañantes del paciente; Población especial / riesgo; Zona apartada; Riesgo de agresión; Entidad pagadora del paciente; Especialidad médica; Medicina general vs medicina especializada', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_AtencionUrgencias_1';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Tmp_Respuesta: Cuando la unidad funcional no es prioritaria, retorna el listado de pacientes en urgencias ordenado por nivel de triage (Triage).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_AtencionUrgencias_1';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si INUNIFUNC.UFUTIPUNI = 31 (unidad de atención prioritaria) → No se ejecuta la consulta y no se retorna ningún resultado. else Se construye y retorna el listado de pacientes de urgencias del centro de atención.; si ADTRIAGEU.TRIORIGEN = ''1'' o ''2'' → Se etiqueta el origen como ''Pacientes Medicina General'' o ''Pacientes Medicina Especializada'' respectivamente.; si ADTRIAGEU.TRIAGECLA en {1..5} → Se traduce a categoría textual: 1-REANIMACIÓN, 2-EMERGENCIA, 3-URGENCIA MÉDICA, 4-URGENCIA DIFERIDA, 5-NO URGENTE.; si INPACIENT.IPTIPODOC IN (6,7) → Se marca el paciente con bandera ASMS=1 (menor de edad sin documento de adulto). else ASMS=0', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_AtencionUrgencias_1';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INUNIFUNC; dbo.INDOCUMEN; dbo.ADTRIAGEU; dbo.ADACOMPAN; dbo.ADINGRESO; dbo.ADPOBESPEPAC; dbo.ADPOBESPE; dbo.ADCONTURG; dbo.INPACIENT; dbo.ADACTIVID; dbo.INENTIDAD; dbo.ADCATTRIU; dbo.INESPECIA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_AtencionUrgencias_1';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_AtencionUrgencias_1';
-- GO
