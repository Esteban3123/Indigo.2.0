
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