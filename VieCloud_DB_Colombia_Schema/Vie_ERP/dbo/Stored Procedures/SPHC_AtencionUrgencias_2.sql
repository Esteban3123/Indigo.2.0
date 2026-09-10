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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que genera el tablero de atención de urgencias y atención prioritaria para un centro de atención y unidad funcional dados. Consolida en una sola consulta los registros de triage (ADTRIAGEU), los ingresos del paciente (ADINGRESO), los controles de urgencias (ADCONTURG), los datos demográficos del paciente (INPACIENT), los acompañantes (ADACOMPAN) y las poblaciones especiales (ADPOBESPEPAC), mostrando información como clasificación de triage, diagnóstico sindrómico, nombre e identificación del paciente, entidad aseguradora, tiempo de espera y estado de atención. Distingue automáticamente si la unidad funcional corresponde a urgencias o atención prioritaria (tipo 31 en INUNIFUNC) para adaptar la lógica de presentación, y filtra los registros de los últimos 8 días. Es utilizado por el personal asistencial y de urgencias para monitorear en tiempo real los pacientes que están en sala de espera o en atención, con sus prioridades, riesgos y datos relevantes.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_AtencionUrgencias_2';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_AtencionUrgencias_2';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los pacientes en espera de atención de urgencias (no prioritaria) con su clasificación de triage, datos demográficos, riesgos y acompañantes, para tablero/seguimiento del centro de atención.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_AtencionUrgencias_2';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La unidad funcional debe existir en INUNIFUNC para determinar si es atención prioritaria (UFUTIPUNI=31) o urgencias.; Solo se ejecuta el flujo principal cuando la unidad NO es prioritaria (UFUTIPUNI<>31).; Existencia de triage en ADTRIAGEU con NUMINGRES no nulo y TRIAFECHA dentro de los últimos 8 días.; Debe existir contacto en ADCONTURG con CONESTADO=4 (en estado activo/llamado) y IPFECLLEGA dentro de los últimos 8 días.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_AtencionUrgencias_2';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran triages de los últimos 8 días respecto a Common.GetDate().; Por cada TRIANUMER se toma únicamente el registro de ADTRIAGEU con la máxima FECHINITR (último triage vigente).; Solo se incluyen pacientes cuyo contacto de urgencias ADCONTURG tenga CONESTADO=4.; Solo se listan triages del centro de atención solicitado (CODCENATE).; Los campos Minutos, Barra, Confirmado, Ausente y Riesgo siempre se inicializan en 0/false en la salida.; Cuando la unidad es prioritaria (UFUTIPUNI=31) el procedimiento no devuelve filas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_AtencionUrgencias_2';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Triage de urgencias; Atención prioritaria; Ingreso/admisión de paciente; Acompañantes; Población especial con riesgo; Riesgo de agresión; Zona apartada; Entidad/aseguradora del paciente; Especialidad médica; Clasificación de triage (Reanimación, Emergencia, Urgencia médica, Urgencia diferida, No urgente); Origen de paciente (Medicina general/especializada); Vive solo', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_AtencionUrgencias_2';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Resultset: Devuelve el listado de pacientes en urgencias ordenado por nivel de Triage cuando la unidad funcional no es prioritaria (UFUTIPUNI<>31).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_AtencionUrgencias_2';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si INUNIFUNC.UFUTIPUNI = 31 para la unidad funcional indicada → Se marca @AtencionPrioritaria=1 y NO se ejecuta el bloque de listado (no retorna filas). else Se ejecuta el armado del listado de pacientes de urgencias y se retorna ordenado por Triage.; si ADTRIAGEU.TRIORIGEN = ''1'' / ''2'' → Se etiqueta el origen como ''Pacientes Medicina General'' o ''Pacientes Medicina Especializada'' respectivamente.; si ADTRIAGEU.TRIAGECLA entre 1 y 5 → Se traduce a etiqueta de triage: 1-REANIMACIÓN, 2-EMERGENCIA, 3-URGENCIA MÉDICA, 4-URGENCIA DIFERIDA, 5-NO URGENTE.; si INPACIENT.IPTIPODOC IN (6,7) → Se marca el paciente como ASMS=1 (afiliado al sistema mediante esos tipos documentales). else ASMS=0; si ADPOBESPE.TIPOPOESPERIES = 1 → Se cuenta al paciente dentro de POBESPECIAL (población especial con riesgo).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_AtencionUrgencias_2';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INUNIFUNC; dbo.INDOCUMEN; dbo.ADTRIAGEU; dbo.ADACOMPAN; dbo.ADINGRESO; dbo.ADPOBESPEPAC; dbo.ADPOBESPE; dbo.ADCONTURG; dbo.INPACIENT; dbo.ADACTIVID; dbo.INENTIDAD; dbo.ADCATTRIU; dbo.INESPECIA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_AtencionUrgencias_2';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_AtencionUrgencias_2';
-- GO
