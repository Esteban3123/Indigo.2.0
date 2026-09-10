
-- =============================================
-- Author:		<Yezid Garcia Medina>
-- Create date: <08-10-2019>
-- Description:	<Sp que trae la disponibilidad de la sala de apoyo diagnostico >
-- =============================================
CREATE PROCEDURE [dbo].[SP_AGE_DisponibilidadSalaApoyoDX]
@CentroAtencion char(10) ,
@FechaInicio Datetime ,
@FechaFin Datetime , 
@TipoServicio tinyint,
@ActividadMedica char(3),
@salaSeleccionadas varchar(max)
AS
BEGIN
	SET NOCOUNT ON;

			--- Disponibilidad de Salas en Apoyo Diagnostico

	Declare @dia AS DATETIME
	SET @dia = CONVERT(date, @FechaInicio)

		DECLARE @TABLA AS TABLE
		(
			FilaINICIO INT,
			FilaFIN INT,
			CODCONCEC INT,
			DESCRIPSAL VARCHAR(100),
			horainicio1  DATETIME,
			horafin1 DATETIME,
			horainicio2 DATETIME,
			horafin2 DATETIME,
			horainicio3 DATETIME,
			horafin3 DATETIME,
			Hora_Inicio DATETIME,
			Hora_Fin DATETIME
		)

	INSERT INTO @TABLA	
		SELECT
			ROW_NUMBER() OVER( Partition by LAPSO.CODCONCEC ORDER BY horafin2 ASC) AS FilaINICIO,
			ROW_NUMBER() OVER( Partition by LAPSO.CODCONCEC ORDER BY horafin2 DESC) AS FilaFIN,
			LAPSO.CODCONCEC,
			LAPSO.DESCRIPSAL,
			horainicio1,
			horafin1,
			horainicio2,
			horafin2,
			horainicio3,
			horafin3,
			CASE 
				WHEN FECHORAIN IS NULL THEN horainicio1
			END AS Hora_Inicio,
			CASE 
				WHEN FECHORAFI IS NULL THEN horafin1
			END AS Hora_Fin			 
			FROM
			(	
				SELECT 
					-- Trae las salas y la disponibilidad
						SALA.CODCONCEC, 
						SALA.DESCRIPSAL,
						SALA.CODIGSALA,
						CITASHOY.FECHORAIN, 
						CITASHOY.FECHORAFI, 
						SALA.DISPHORINI, 
						SALA.DISPHORFIN, 
						SALA.TIPOSALA,
						SALA.ESTADO,
						SALA.TIPOSERVIC,
						SALA.CODCENATE,
						ACT.CODACTMED,
						CASE  
							WHEN CITASHOY.FECHORAIN IS NULL THEN  CONVERT(DATETIME, @FechaInicio) + CONVERT(DATETIME, CONVERT(time, DISPHORINI)) 			
						END AS horainicio1,
						CASE 
							WHEN CITASHOY.FECHORAFI IS NULL THEN CONVERT(DATETIME, @FechaInicio) +  CONVERT(DATETIME, CONVERT(time, DISPHORFIN)) 
						END AS horafin1 ,

						CASE  			
							WHEN CITASHOY.FECHORAIN IS NOT NULL THEN CONVERT(DATETIME, @FechaInicio) + CONVERT(DATETIME, CONVERT(time, DISPHORINI)) 
						END AS horainicio2,
						CASE 
							WHEN CITASHOY.FECHORAFI IS NOT NULL THEN CONVERT(DATETIME, @FechaInicio) +  CONVERT(DATETIME, CONVERT(time, DATEADD(SECOND, -1, FECHORAIN) ) ) 
						END AS horafin2 ,

						CASE  			
							WHEN CITASHOY.FECHORAIN IS NOT NULL THEN CONVERT(DATETIME, @FechaInicio) + CONVERT(DATETIME, CONVERT(time, DATEADD(SECOND, +1, FECHORAFI) ) ) 
						END AS horainicio3,
						CASE 
							WHEN CITASHOY.FECHORAFI IS NOT NULL THEN CONVERT(DATETIME, @FechaInicio) +  CONVERT(DATETIME, CONVERT(time, DISPHORFIN) ) 
						END AS horafin3 
					FROM AGENSALAC AS SALA
					LEFT JOIN (
							SELECT 
							-- Trae las citas del dia de hoy
								a.CODCONCEC,
								a.DESCRIPSAL,
								a.CODIGSALA,
								b.FECHORAIN, 
								b.FECHORAFI							
							FROM AGENSALAC AS a
						
							LEFT JOIN AGASICITA AS b
							ON a.CODCONCEC = b.IDSALA
							WHERE
								a.TIPOSALA = 'D'				
								AND a.ESTADO = 1
								AND a.TIPOSERVIC = @TipoServicio
								AND a.CODCENATE = @CentroAtencion
								AND a.CODIGSALA IN (SELECT Value FROM dbo.SplitString(@salaSeleccionadas))
								AND b.TIPSOLICITU = 2 
								AND b.CODESTCIT IN ('0','1','2','3')
								AND b.FECHORAIN >= @FechaInicio
								AND b.FECHORAFI <= @FechaFin		
																			
				) AS CITASHOY
				ON SALA.CODCONCEC = CITASHOY.CODCONCEC 
				INNER JOIN AGENSALAACT AS SACT 
				ON SALA.CODCONCEC = SACT.CODCONCEC 
				INNER JOIN AGACTIMED ACT 
				ON ACT.CODACTMED = SACT.CODACTMED
				
				WHERE 
					SALA.TIPOSALA = 'D'
					AND SALA.ESTADO = 1
					AND SALA.TIPOSERVIC  = @TipoServicio
					AND SALA.CODCENATE = @CentroAtencion
					AND SALA.CODIGSALA IN (SELECT Value FROM dbo.SplitString(@salaSeleccionadas))					
					AND ACT.CODACTMED = @ActividadMedica
					AND ACT.ACTIVICON = 2 
					AND ACT.ESTADOACT = 1
					AND	
						CASE
							WHEN @@LANGUAGE = 'Español' THEN 
							CASE
								WHEN  DATEPART(WEEKDAY, @dia ) = 1 THEN LUN
								WHEN  DATEPART(WEEKDAY, @dia ) = 2 THEN MAR
								WHEN  DATEPART(WEEKDAY, @dia ) = 3 THEN MIE
								WHEN  DATEPART(WEEKDAY, @dia ) = 4 THEN JUE
								WHEN  DATEPART(WEEKDAY, @dia ) = 5 THEN VIE
								WHEN  DATEPART(WEEKDAY, @dia ) = 6 THEN SAB
								WHEN  DATEPART(WEEKDAY, @dia ) = 7 THEN DOM		
							END
							WHEN @@LANGUAGE = 'us_english' THEN 
							CASE
								WHEN  DATEPART(WEEKDAY, @dia ) = 1 THEN DOM		
								WHEN  DATEPART(WEEKDAY, @dia ) = 2 THEN LUN
								WHEN  DATEPART(WEEKDAY, @dia ) = 3 THEN MAR
								WHEN  DATEPART(WEEKDAY, @dia ) = 4 THEN MIE
								WHEN  DATEPART(WEEKDAY, @dia ) = 5 THEN JUE
								WHEN  DATEPART(WEEKDAY, @dia ) = 6 THEN VIE
								WHEN  DATEPART(WEEKDAY, @dia ) = 7 THEN SAB	
							END
						END = 1
					AND ( FESTIVOS = 1
						OR 
						NOT EXISTS(SELECT 1 FROM INDIAFEST DF WHERE DF.DIAFESTIV = @dia)  )

					
			) AS LAPSO
					   	
		--- SELECT * FROM @TABLA

		SELECT * FROM
		(
			SELECT 
				ROW_NUMBER() OVER( Partition by CODCONCEC ORDER BY HORAINICIO ASC) AS Number,
				CODCONCEC,
				DESCRIPSAL,
				HORAINICIO, 
				HORAFIN 
			FROM 
			(
				SELECT A =1, CODCONCEC, DESCRIPSAL, HORAINICIO= horainicio1, HORAFIN= horafin1 
				FROM @TABLA
				WHERE FilaINICIO = 1
				AND horainicio1 IS NOT NULL

				UNION ALL

				SELECT A =2, CODCONCEC, DESCRIPSAL,  HORAINICIO= horainicio2, HORAFIN= horafin2 
				FROM @TABLA
				WHERE FilaINICIO = 1
				AND horainicio2 IS NOT NULL

				UNION ALL

				SELECT A =3, b1.CODCONCEC, b1.DESCRIPSAL, HORAINICIO=b1.horainicio3,  HORAFIN= b2.horafin2 
				FROM @TABLA b1
				inner join @TABLA b2
				on b2.CODCONCEC = b1.CODCONCEC
				and b2.FilaINICIO = b1.FilaINICIO + 1

				UNION ALL

				SELECT A =4, CODCONCEC, DESCRIPSAL, HORAINICIO= horainicio3,  HORAFIN= horafin3 
				FROM @TABLA
				WHERE FILAFIN = 1
				AND horainicio3 IS NOT NULL
			) T

			--- SELECT * FROM T

		) DISPONIBILIDAD
		WHERE  Number = 1
		ORDER BY CODCONCEC, HORAINICIO	
	
End
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Calcula y retorna los tramos horarios disponibles (franjas libres) de las salas de apoyo diagnóstico —como salas de imagenología, laboratorio u otros servicios de diagnóstico— para un centro de atención, un rango de fechas, un tipo de servicio y una actividad médica específicos. Cruza la configuración de cada sala (AGENSALAC) con las citas ya agendadas en ese período (AGASICITA) y con las actividades médicas habilitadas para la sala (AGENSALAACT / AGACTIMED), descontando los bloques ya ocupados y respetando los días hábiles configurados y los festivos. El resultado es una lista ordenada de franjas inicio-fin libres por sala, usada por el módulo de agendamiento para mostrar al agendador o al paciente cuándo puede programarse un procedimiento o examen de apoyo diagnóstico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_AGE_DisponibilidadSalaApoyoDX';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_AGE_DisponibilidadSalaApoyoDX';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Calcula y devuelve las franjas horarias disponibles de salas de apoyo diagnóstico para un centro, tipo de servicio, actividad médica y día determinados, descontando el tiempo ocupado por citas ya agendadas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_DisponibilidadSalaApoyoDX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'@salaSeleccionadas debe ser una lista parseable por dbo.SplitString; @FechaInicio y @FechaFin deben corresponder al mismo día para que el cálculo de franjas sea consistente; Deben existir registros activos en AGENSALAC para el centro, tipo de servicio y salas indicadas; La actividad médica indicada debe existir activa (ESTADOACT=1) y con ACTIVICON=2 en AGACTIMED, y estar asociada a las salas en AGENSALAACT; El horario semanal de la actividad debe tener marcado en 1 el día de la semana correspondiente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_DisponibilidadSalaApoyoDX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran salas con TIPOSALA=''D'' (apoyo diagnóstico) y ESTADO=1 (activas); Solo se consideran citas con TIPSOLICITU=2 y CODESTCIT en (''0'',''1'',''2'',''3'') para descontar disponibilidad; Las citas consideradas deben estar dentro del rango [@FechaInicio, @FechaFin]; Solo se evalúan actividades médicas con ACTIVICON=2 y ESTADOACT=1; Las salas filtradas deben pertenecer al centro de atención y tipo de servicio indicados, y estar en la lista de salas seleccionadas; Si el día es festivo y el horario semanal no permite festivos (FESTIVOS<>1), la sala no se incluye; El procedimiento solo entrega franjas con HORAINICIO no nula (Number=1 por sala/lapso); El cálculo se realiza para un único día derivado de @FechaInicio', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_DisponibilidadSalaApoyoDX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Sala de apoyo diagnóstico; Disponibilidad de sala; Cita médica; Actividad médica; Centro de atención; Tipo de servicio; Días festivos; Horario semanal por día', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_DisponibilidadSalaApoyoDX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Resultset: Devuelve, por sala (CODCONCEC), la primera franja disponible (Number=1) ordenada por HORAINICIO, generada uniendo: (1) horario completo cuando no hay citas, (2) franja antes de la primera cita, (3) franjas entre citas consecutivas, y (4) franja después de la última cita', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_DisponibilidadSalaApoyoDX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Cita existente en la sala (FECHORAIN/FECHORAFI no nulos) → Se parten los lapsos de disponibilidad en bloques antes y después de la cita, restando 1 segundo al inicio y sumando 1 segundo al fin de la cita else Se utiliza un único bloque desde DISPHORINI hasta DISPHORFIN como disponibilidad de la sala; si @@LANGUAGE = ''Español'' → Se mapea DATEPART(WEEKDAY)=1..7 a LUN..DOM respectivamente else Si @@LANGUAGE = ''us_english'', se mapea 1=DOM y 2..7 = LUN..SAB; si El día consultado no es festivo (FESTIVOS=1 o no existe en INDIAFEST) → La sala se considera disponible para ese día else Se excluye la sala si el día es festivo y la actividad no permite festivos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_DisponibilidadSalaApoyoDX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.SplitString', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_DisponibilidadSalaApoyoDX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AGENSALAC; dbo.AGASICITA; dbo.AGENSALAACT; dbo.AGACTIMED; dbo.INDIAFEST', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_DisponibilidadSalaApoyoDX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_DisponibilidadSalaApoyoDX';
-- GO
