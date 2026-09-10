
CREATE PROCEDURE [dbo].[SPHC_AtencionUrgencias]
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
            FROM INUNIFUNC 
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
                         FROM dbo.INDOCUMEN 
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
                         FROM dbo.ADTRIAGEU AS A 
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
                         FROM dbo.ADACOMPAN AS AC 
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
                         FROM dbo.ADINGRESO AS K 
                              INNER JOIN Tmp_ADTRIAGEU AS A ON A.NUMINGRES = K.NUMINGRES
                         -- WHERE K.IFECHAING >= @Fecha;
                         )
                     --Cargue poblacion espacial
                     ,
                     Tmp_PobEspecial
                     AS (SELECT Z.IPCODPACI, 
                                COUNT(1) AS POBESPECIAL
                         FROM dbo.ADPOBESPEPAC AS Z 
                              INNER JOIN ADPOBESPE AS X ON X.ID = Z.IDADPOBESPE
                              INNER JOIN Tmp_ADINGRESO AS A ON A.IPCODPACI = Z.IPCODPACI
                         WHERE X.TIPOPOESPERIES = 1
                         GROUP BY Z.IPCODPACI)
                     --Cargue ADCONTURG
                     ,
                     Tmp_ADCONTURG
                     AS (SELECT B.CODCONCEC, 
                                B.UFUCODIGO, 
                                B.IPCODPACI
                         FROM dbo.ADCONTURG AS B 
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
                         FROM dbo.INPACIENT AS F 
                              INNER JOIN Tmp_ADCONTURG AS B ON B.IPCODPACI = F.IPCODPACI
                              LEFT JOIN dbo.ADACTIVID AS L ON F.CODACTIVI = L.codactivi
                              LEFT JOIN dbo.INENTIDAD AS M ON M.CODENTIDA = F.CODENTIDA)
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
                                    THEN 'TRIAGE I'
                                    WHEN 2
                                    THEN 'TRIAGE II'
                                    WHEN 3
                                    THEN 'TRIAGE III'
                                    WHEN 4
                                    THEN 'TRIAGE IV'
                                    WHEN 5
                                    THEN 'TRIAGE V'
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
                              left JOIN dbo.ADCATTRIU AS E ON E.TRIACATEG = A.TRIACATEG
                              LEFT JOIN DBO.INESPECIA AS G ON A.CODESPECI = G.CODESPECI
                              LEFT JOIN Tmp_PobEspecial AS PE ON K.IPCODPACI = PE.IPCODPACI
                              LEFT JOIN Tmp_Acompanantes AS AC ON A.NUMINGRES = AC.NUMINGRES
                         --ORDER BY A.TRIAGECLA
                         )
                     SELECT *
                     FROM Tmp_Respuesta
                     ORDER BY Triage;
			END
        ELSE
            BEGIN						
               
				SELECT  * FROM
				(
					SELECT 
					A.TRIAFECHA as FECHALLEGADA, K.UFUCODIGO,A.TRIORIGEN,CASE A.TRIORIGEN WHEN 1  THEN '1 - Pacientes Medicina General' WHEN 2 THEN '2 - Pacientes Medicina Especializada' END AS Origen,K.CODTIPPAC AS Tipo, 
					A.TRIANUMER AS Consecutivo,RTRIM(TRIANOMCA) AS [Dx Sindromatico],RTRIM(K.IPCODPACI) AS Identificacion,RTRIM(F.IPNOMCOMP) AS [Nombre Paciente], TRIAFECHA AS [Fecha Triage],0 AS Minutos,0 AS Barra,CAST(0 AS BIT) AS Confirmado,
					CAST(0 AS BIT) AS Ausente,A.NUMINGRES AS Ingreso,IPFECNACI AS 'Fecha Nacimiento' ,CAST('' AS CHAR(50)) AS Edad,RTRIM(G.DESESPECI) AS Especialidad
					, CASE WHEN F.IPTIPODOC IN(6,7) THEN 1 ELSE 0 END AS ASMS
					, F.ZONAPARTADA,
					L.RIESGOAGRE,
					(SELECT count(*) FROM dbo.ADPOBESPEPAC Z INNER JOIN ADPOBESPE X ON X.ID = Z.IDADPOBESPE WHERE IPCODPACI = F.IPCODPACI AND TIPOPOESPERIES = 1) AS POBESPECIAL,
					K.VIVESOLO,
					(SELECT count(*) FROM ADACOMPAN WHERE NUMINGRES = K.NUMINGRES) AS ACOMPANANTES,
					CONVERT(BIT,0) AS Riesgo, RTRIM(F.CODENTIDA) + '-'+ RTRIM(M.NOMENTIDA) as EntidadPaciente
					FROM 
					dbo.ADTRIAGEU A LEFT JOIN 
					dbo.ADINGRESO K ON A.NUMINGRES = K.NUMINGRES INNER JOIN
					dbo.INPROFSAL D ON D.CODPROSAL = A.CODPROSAL INNER JOIN 
					dbo.ADCATTRIU E ON E.TRIACATEG = A.TRIACATEG INNER JOIN 
					dbo.INPacient F ON K.IPCODPACI = F.IPCODPACI LEFT JOIN
					dbo.INESPECIA G ON A.CODESPECI = G.CODESPECI LEFT JOIN 
					dbo.ADACTIVID L ON F.CODACTIVI = L.codactivi LEFT JOIN
					dbo.INENTIDAD M ON M.CODENTIDA = F.CODENTIDA
					INNER JOIN (SELECT  TRIANUMER, MAX(FECHINITR) fecha_max FROM ADTRIAGEU GROUP BY TRIANUMER) c ON a.TRIANUMER = c.TRIANUMER AND a.FECHINITR = c.fecha_max
					WHERE 
					K.UFUEGRMED IS NULL 
					AND K.IESTADOIN <> 'A'
					AND K.NUMINGRES NOT IN (SELECT DISTINCT A.NUMINGRES FROM  ADINGRESO AS A INNER JOIN AMBORDIMA AS B on B.IPCODPACI = A.IPCODPACI WHERE  Convert(varchar(10),CONVERT(date,A.IFECHAING,106),103) = Convert(varchar(10),CONVERT(date,B.FECORDMED,106),103)) 
					AND (select count(ID) from HCHISPACA where NUMINGRES = K.NUMINGRES) = 0 
					AND K.CODCENATE = @CentroAtencion 
					AND K.UFUCODIGO = @UFuncional
					AND K.NUMINGRES IS NOT NULL 
					AND K.NUMINGRES NOT IN (SELECT NUMDOCUME FROM dbo.INDOCUMEN  WHERE CODDOCUME=@Estado_3 AND CODUSUARI<>@Usuario) 

					UNION ALL
	
					SELECT 
					K.IFECHAING as FECHALLEGADA, K.UFUCODIGO,0 as TRIORIGEN,	'3 - Pacientes sin Clasificación de Triage' AS Origen,K.CODTIPPAC AS Tipo, 0 AS Consecutivo,
					'' AS [Dx Sindromatico],RTRIM(K.IPCODPACI) AS Identificacion,RTRIM(F.IPNOMCOMP) AS [Nombre Paciente], 
					K.IFECHAING AS [Fecha Triage],0 AS Minutos,0 AS Barra,CAST(0 AS BIT) AS Confirmado,CAST(0 AS BIT) AS Ausente,K.NUMINGRES AS Ingreso,
					IPFECNACI AS 'Fecha Nacimiento' ,CAST('' AS CHAR(50)) AS Edad,'' AS Especialidad
					, CASE WHEN F.IPTIPODOC IN(6,7) THEN 1 ELSE 0 END AS ASMS
					, F.ZONAPARTADA,
					L.RIESGOAGRE,
					(SELECT count(*) FROM dbo.ADPOBESPEPAC Z INNER JOIN ADPOBESPE X ON X.ID = Z.IDADPOBESPE WHERE IPCODPACI = F.IPCODPACI AND TIPOPOESPERIES = 1) AS POBESPECIAL,
					K.VIVESOLO,
					(SELECT count(*) FROM ADACOMPAN WHERE NUMINGRES = K.NUMINGRES) AS ACOMPANANTES,
					CONVERT(BIT,0) AS Riesgo, RTRIM(F.CODENTIDA) + '-'+ RTRIM(M.NOMENTIDA) as EntidadPaciente
					FROM 
					dbo.ADINGRESO K INNER JOIN
					dbo.INPacient F ON K.IPCODPACI = F.IPCODPACI LEFT JOIN
					dbo.ADACTIVID L ON F.CODACTIVI = L.codactivi LEFT JOIN
					dbo.INENTIDAD M ON M.CODENTIDA = F.CODENTIDA
					WHERE  
					K.UFUEGRMED IS NULL 
					AND K.IESTADOIN = ' '
					AND K.NUMINGRES NOT IN (SELECT DISTINCT A.NUMINGRES FROM  ADINGRESO AS A INNER JOIN AMBORDIMA AS B on B.IPCODPACI = A.IPCODPACI WHERE  Convert(varchar(10),CONVERT(date,A.IFECHAING,106),103) = Convert(varchar(10),CONVERT(date,B.FECORDMED,106),103))
					AND (K.REQTRIAGE = 0 or K.REQTRIAGE is null) 
					AND (select count(ID) from HCHISPACA where NUMINGRES = K.NUMINGRES) = 0 
					AND K.CODCENATE = @CentroAtencion
					AND K.UFUCODIGO = @UFuncional 
					AND K.NUMINGRES NOT IN (SELECT NUMDOCUME FROM dbo.INDOCUMEN WHERE CODDOCUME=@Estado_3 AND CODUSUARI<>@Usuario) 
					) AS TMP order by [Fecha Triage] 
                    
        END

    END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que genera el panel de atención de urgencias y atención prioritaria para un centro de atención y unidad funcional específicos. Consulta los registros de triage (ADTRIAGEU) de los últimos 8 días, los combina con datos del ingreso del paciente (ADINGRESO), información demográfica y de entidad (INPACIENT), acompañantes (ADACOMPAN), población especial (ADPOBESPEPAC) y la consulta de urgencias activa (ADCONTURG), para construir una vista consolidada del estado actual de los pacientes en sala de urgencias. Diferencia automáticamente si la unidad funcional corresponde a urgencias general o atención prioritaria (tipo 31 en INUNIFUNC), y expone datos clave como clasificación de triage, diagnóstico sindrómico, nombre e identificación del paciente, entidad aseguradora, tiempo de espera y presencia de acompañantes, sirviendo como fuente de datos para el tablero o monitor de urgencias en tiempo real del sistema Indigo Vie Cloud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_AtencionUrgencias';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_AtencionUrgencias';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los pacientes en espera de atención en urgencias (con o sin triage) según si la unidad funcional es de atención prioritaria o de urgencias estándar, excluyendo ingresos bloqueados por otros usuarios.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_AtencionUrgencias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La unidad funcional debe existir en INUNIFUNC para determinar si es prioritaria (UFUTIPUNI=31) o de urgencias; Los ingresos deben pertenecer al centro de atención indicado; Solo se consideran triages/ingresos con fecha dentro de los últimos 8 días (DATEADD(DD,-8,GetDate()))', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_AtencionUrgencias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Se excluyen siempre los ingresos bloqueados (INDOCUMEN con CODDOCUME=3) por usuarios distintos al que consulta; Solo se consideran atenciones de los últimos 8 días; Solo se toma el triage más reciente por TRIANUMER (MAX FECHINITR); En modo prioritario, se excluyen ingresos que ya tienen orden médica ambulatoria (AMBORDIMA) del mismo día de ingreso; En modo prioritario, se excluyen ingresos que ya tienen historia clínica registrada en HCHISPACA; El campo Riesgo siempre se devuelve como 0 (BIT); Los campos Minutos, Barra, Confirmado, Ausente, Edad se inicializan vacíos/cero para que la aplicación los calcule', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_AtencionUrgencias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Triage de urgencias; Atención prioritaria; Ingreso/Admisión; Acompañantes del paciente; Población especial / riesgo; Diagnóstico sindromático; Especialidad médica; Entidad/aseguradora del paciente; Zona apartada; Riesgo de agresión; Bloqueo de documento por usuario; Clasificación de Triage I-V; Origen del paciente (Medicina General/Especializada); Historia clínica del paciente; Orden médica ambulatoria', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_AtencionUrgencias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Resultset: Cuando UFUTIPUNI<>31 (urgencias estándar), retorna pacientes con triage de los últimos 8 días en el centro de atención, con contactos en estado 4, excluyendo ingresos bloqueados (INDOCUMEN.CODDOCUME=3 por otro usuario), ordenado por nivel de Triage; [RETURN_RESULT] Resultset: Cuando UFUTIPUNI=31 (atención prioritaria), retorna unión de: pacientes con triage (UFUEGRMED IS NULL, IESTADOIN<>''A'') y pacientes sin clasificación de triage (IESTADOIN='' '', REQTRIAGE=0 o null), excluyendo ingresos con orden médica ambulatoria del mismo día, sin historia clínica HCHISPACA y no bloqueados por otro usuario, ordenado por Fecha Triage', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_AtencionUrgencias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si UFUTIPUNI de la unidad funcional = 31 (atención prioritaria) → Ejecuta consulta UNION ALL con dos bloques: pacientes con triage y pacientes sin clasificación de triage, filtrando por unidad funcional específica else Ejecuta consulta basada en CTEs sobre ADTRIAGEU + ADCONTURG (contactos en estado 4) sin filtrar por unidad funcional, ordenado por nivel de triage; si IPTIPODOC IN (6,7) → Marca el paciente como ASMS=1 (menor de edad / documento especial) else ASMS=0; si Para cada TRIANUMER se toma el registro con MAX(FECHINITR) → Solo se considera el último triage registrado por consecutivo', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_AtencionUrgencias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INUNIFUNC; dbo.INDOCUMEN; dbo.ADTRIAGEU; dbo.ADACOMPAN; dbo.ADINGRESO; dbo.ADPOBESPEPAC; dbo.ADPOBESPE; dbo.ADCONTURG; dbo.INPACIENT; dbo.ADACTIVID; dbo.INENTIDAD; dbo.ADCATTRIU; dbo.INESPECIA; dbo.INPROFSAL; dbo.AMBORDIMA; dbo.HCHISPACA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_AtencionUrgencias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_AtencionUrgencias';
-- GO
