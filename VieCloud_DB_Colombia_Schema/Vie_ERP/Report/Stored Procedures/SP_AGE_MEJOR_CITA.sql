/**********************************************************************************************************************************
Nombre:[Report].[SP_AGE_MEJOR_CITA] 
Tipo:Procedimiento Almacenado
Observacion:
Profesional: 
Fecha:
---------------------------------------------------------------------------
Modificaciones
_____________________________________________________________________________
Version 2
Persona que modifico: Nilsson Miguel Galindo Lopez
Fecha:26-06-2024
Ovservaciones: Se cambia la logica del procedimiento almacenado en las partes de los cilos while para mejorar la velocidad del mismo, pasando de 1:30 a 4 segundos,
			y se comentarea toda la logica anterior.
--------------------------------------
Version 3
Persona que modifico:
Fecha:
***********************************************************************************************************************************/
CREATE PROCEDURE [Report].[SP_AGE_MEJOR_CITA] 
@codcenate char(10),
@codprosal  as char(60)
AS

--DECLARE @codcenate char(10)='35051';
--DECLARE @codprosal  as char(50)='ONCOLOGIA CLINICA'

----@codcenate char(10),
----@codprosal  as char(60)
----AS
-- --Verifica si la tabla temporal #schedule existe
--IF OBJECT_ID('tempdb..#schedule') IS NOT NULL
--BEGIN
--    -- Si la tabla temporal existe, la elimina
--    DROP TABLE #schedule;
--END

---- Crea la tabla temporal #schedule
--CREATE TABLE #schedule
--(
--    idagenda INT, 
--    idrow INT, 
--    inidatetime DATETIME, 
--    enddatetime DATETIME,
--    [state] BIT
--)

--DECLARE 
--	@codautonu INT,
--	@fechorain DATETIME,
--	@fechorafi DATETIME,
--	@minutes INT

----	_________________________________________________________________________________________________________________________________________________________________________________
--DECLARE schedule_cursor CURSOR FOR 
--	SELECT DISTINCT
--		aga.codautonu, aga.fechorain, aga.fechorafi
--	FROM dbo.agagemedc AS  aga
--	INNER JOIN dbo.agagemedd as agd ON aga.codautonu = agd.codautonu
--	INNER JOIN dbo.inespecia AS esp ON aga.codespeci = esp.codespeci 
--	WHERE aga.codcenate = @codcenate AND esp.desespeci = @codprosal 
--	AND aga.fechorain  >= GETDATE() AND aga.fechorain <= DATEADD(MONTH, 4, GETDATE())

--OPEN schedule_cursor;  
---- Perform the first fetch.  
--FETCH NEXT FROM schedule_cursor    
--INTO @codautonu, @fechorain, @fechorafi
  
---- Check @@FETCH_STATUS to see if there are any more rows to fetch.
--WHILE @@FETCH_STATUS = 0  
--BEGIN  

--	DECLARE 
--		@num_rows INT,
--		@ini_date DATETIME,
--		@end_date DATETIME
--	SET @num_rows =  DATEDIFF(MINUTE, @fechorain, @fechorafi) / 5

--	SET @ini_date = @fechorain

--	WHILE @num_rows > 0
--	BEGIN
		
--		SET @end_date = DATEADD(MI, 5, @ini_date)

--		--SELECT  @codautonu, @fechorain, @fechorafi, @minutes, @num_rows, @ini_date, @end_date,  @minutes / 5 - @num_rows + 1
--		--SELECT @codautonu--,  CAST(DATEPART(HOUR, @ini_date) AS VARCHAR(2)) + '' + CAST(DATEPART(MINUTE, @ini_date) AS VARCHAR(2)), @ini_date,  @end_date, 0
--		INSERT INTO #schedule VALUES (@codautonu,  CAST(DATEPART(HOUR, @ini_date) AS VARCHAR(2)) + '' + CAST(DATEPART(MINUTE, @ini_date) AS VARCHAR(2)), @ini_date,  @end_date, 0)

--		SET @ini_date = @end_date
--		SET @num_rows -= 1
--	END
	
--	-- This is executed as long as the previous fetch succeeds.
--    FETCH NEXT FROM schedule_cursor   
--    INTO @codautonu, @fechorain, @fechorafi
--END;

--CLOSE schedule_cursor;  
--DEALLOCATE schedule_cursor;  

----	_________________________________________________________________________________________________________________________________________________________________________________
--DECLARE appointments_cursor CURSOR FOR 
--	SELECT 
--		cit.idagenda, cit.fechorain, cit.fechorafi
--	FROM dbo.agasicita AS cit 
--	INNER JOIN #schedule aga ON aga.idagenda = cit.idagenda
--	WHERE cit.codestcit IN (0, 3) 

--OPEN appointments_cursor;  

---- Perform the first fetch.  
--FETCH NEXT FROM appointments_cursor    
--INTO @codautonu, @fechorain, @fechorafi
  
---- Check @@FETCH_STATUS to see if there are any more rows to fetch.
--WHILE @@FETCH_STATUS = 0  
--BEGIN  

--	DECLARE 
--		@app_num_rows INT,
--		@app_ini_date DATETIME,
--		@app_end_date DATETIME

--	SET @app_num_rows = DATEDIFF(MINUTE, @fechorain, @fechorafi) / 5
--	SET @app_ini_date = @fechorain

--	WHILE @app_num_rows > 0
--	BEGIN
		
--		SET @app_end_date = DATEADD(MI, 5, @app_ini_date)

--		UPDATE #schedule SET [state] = 1 WHERE idagenda = @codautonu AND idrow = CAST(DATEPART(HOUR, @app_ini_date) AS VARCHAR(2)) + '' + CAST(DATEPART(MINUTE, @app_ini_date) AS VARCHAR(2))
		
--		SET @app_ini_date = @app_end_date
--		SET @app_num_rows -= 1
--	END
	
--	-- This is executed as long as the previous fetch succeeds.
--    FETCH NEXT FROM appointments_cursor   
--    INTO @codautonu, @fechorain, @fechorafi
--END;

--CLOSE appointments_cursor;  
--DEALLOCATE appointments_cursor;  

----	_________________________________________________________________________________________________________________________________________________________________________________
--SELECT  'ODO' 'EMPRESA', SCH.idagenda,
--	RTRIM(adc.nomcenate) AS 'CENTRO ATENCION',
--	RTRIM(med.codprosal) + ' - ' + RTRIM(med.nommedico) AS 'PROFESIONAL', 
--	RTRIM(esp.desespeci) AS 'ESPECIALIDAD',
--	CONVERT(CHAR(10), sch.inidatetime, 103) AS 'FECHA',
--	CONVERT(CHAR(5), sch.inidatetime, 108) AS 'HORARIOS'
--FROM #schedule sch
--INNER JOIN dbo.agagemedc AS aga ON sch.idagenda = aga.codautonu 
--INNER JOIN dbo.adcenaten adc ON aga.codcenate = adc.codcenate 
--INNER JOIN dbo.inespecia AS esp ON aga.codespeci = esp.codespeci 
--INNER JOIN dbo.inprofsal AS med ON aga.codprosal = med.codprosal
--WHERE state = 0 
----AND med.codprosal='2000020458'
--AND AGA.CODAUTONU IN ('51695','51696','51526')
--ORDER BY inidatetime 
--GO

-- secrean variables tabla para las diferentes comparaciones
DECLARE @AGENDA AS TABLE (id int identity(1,1),
						  codautonu INT,
						  fechorain DATETIME,
						  fechorafi DATETIME);
DECLARE @SCHEDULE AS TABLE (idagenda INT, 
							idrow INT, 
							inidatetime DATETIME, 
							enddatetime DATETIME,
							[state] BIT);
DECLARE @CITA AS TABLE(id int identity(1,1),
					   idagenda INT, 
					   fechorain DATETIME, 
					   fechorafi DATETIME);

INSERT INTO @AGENDA
SELECT DISTINCT
AGA.codautonu, AGA.fechorain, AGA.fechorafi
FROM 
dbo.agagemedc AS AGA
INNER JOIN dbo.agagemedd AS AGD ON AGA.codautonu = AGD.codautonu
INNER JOIN dbo.inespecia AS ESP ON AGA.codespeci = ESP.codespeci 
WHERE 
AGA.codcenate=@CODCENATE 
AND ESP.desespeci=@CODPROSAL 
AND AGA.fechorain>=GETDATE() 
AND AGA.fechorain <= DATEADD(MONTH, 4, GETDATE()) 
--AND AGA.CODAUTONU IN ('51695','51696','51526')

DECLARE @I INT,@CONTAR INT,@ini_date DATETIME,@end_date DATETIME,@num_rows INT,@fechorain DATETIME,@fechorafi DATETIME,@codautonu INT;
SET @CONTAR=(SELECT COUNT(*) FROM @AGENDA)
SET @I=1
WHILE @I<=@CONTAR
BEGIN
	SET @fechorain=(SELECT fechorain FROM @AGENDA WHERE ID=@I)
	SET @fechorafi=(SELECT fechorafi FROM @AGENDA WHERE ID=@I)
	SET @codautonu=(SELECT codautonu FROM @AGENDA WHERE ID=@I)
	SET @num_rows =  DATEDIFF(MINUTE, @fechorain, @fechorafi) / 5
	SET @ini_date=@fechorain
	WHILE @num_rows > 0
	BEGIN
		SET @end_date = DATEADD(MI, 5, @ini_date)
		INSERT INTO @SCHEDULE VALUES (@codautonu,  CAST(DATEPART(HOUR, @ini_date) AS VARCHAR(2)) + '' + CAST(DATEPART(MINUTE, @ini_date) AS VARCHAR(2)), @ini_date,  @end_date, 0)
		SET @ini_date = @end_date
		SET @num_rows -= 1
	END
	SET @I +=1
END

INSERT INTO @CITA
SELECT distinct
cit.idagenda, cit.fechorain, cit.fechorafi
FROM dbo.agasicita AS cit 
INNER JOIN @SCHEDULE aga ON aga.idagenda = cit.idagenda
WHERE cit.codestcit IN (0, 3) 

DECLARE @app_num_rows INT,@app_ini_date DATETIME,@app_end_date DATETIME
SET @CONTAR=(SELECT COUNT(*) FROM @CITA)
SET @I=1
WHILE @I<=@CONTAR
BEGIN
	SET @fechorain=(SELECT fechorain FROM @CITA WHERE ID=@I)
	SET @fechorafi=(SELECT fechorafi FROM @CITA WHERE ID=@I)
	SET @codautonu=(SELECT idagenda FROM @CITA WHERE ID=@I)
	SET @app_num_rows = DATEDIFF(MINUTE, @fechorain, @fechorafi) / 5
	SET @app_ini_date = @fechorain

	WHILE @app_num_rows > 0
	BEGIN
		
		SET @app_end_date = DATEADD(MI, 5, @app_ini_date)

		UPDATE @schedule SET [state] = 1 WHERE idagenda = @codautonu AND idrow = CAST(DATEPART(HOUR, @app_ini_date) AS VARCHAR(2)) + '' + CAST(DATEPART(MINUTE, @app_ini_date) AS VARCHAR(2))
		
		SET @app_ini_date = @app_end_date
		SET @app_num_rows -= 1
	END
	SET @I +=1
END

SELECT  'ODO' 'EMPRESA',
	RTRIM(adc.nomcenate) AS 'CENTRO ATENCION',
	RTRIM(med.codprosal) + ' - ' + RTRIM(med.nommedico) AS 'PROFESIONAL', 
	RTRIM(esp.desespeci) AS 'ESPECIALIDAD',
	CONVERT(CHAR(10), sch.inidatetime, 103) AS 'FECHA',
	CONVERT(CHAR(5), sch.inidatetime, 108) AS 'HORARIOS'
FROM @SCHEDULE sch
INNER JOIN dbo.agagemedc AS aga ON sch.idagenda = aga.codautonu 
INNER JOIN dbo.adcenaten adc ON aga.codcenate = adc.codcenate 
INNER JOIN dbo.inespecia AS esp ON aga.codespeci = esp.codespeci 
INNER JOIN dbo.inprofsal AS med ON aga.codprosal = med.codprosal
WHERE state = 0 
--AND med.codprosal='2000020458'
--AND AGA.CODAUTONU IN ('51695','51696','51526')
ORDER BY inidatetime
GO
GRANT EXECUTE
    ON OBJECT::[Report].[SP_AGE_MEJOR_CITA] TO [odopbi]
    AS [dbo];
GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Retorna los slots de agenda disponibles (sin cita asignada) para los próximos 4 meses, dado un centro de atención y una especialidad. Genera una grilla de intervalos de 5 minutos a partir de las agendas médicas activas y marca como ocupados los slots con citas en estado 0 o 3, devolviendo solo los libres. El resultado incluye centro, profesional, especialidad, fecha y horario, y es consumido por el rol `odopbi` para reportes de disponibilidad.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_MEJOR_CITA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_MEJOR_CITA';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve los espacios libres (slots de 5 minutos) disponibles en las agendas médicas de un centro de atención y especialidad, dentro de los próximos 4 meses, descontando las citas ya ocupadas.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_MEJOR_CITA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El centro de atención debe existir en agagemedc.codcenate; La especialidad indicada debe coincidir exactamente con inespecia.desespeci; Existir agendas (agagemedc/agagemedd) cuyo inicio sea >= GETDATE() y <= GETDATE()+4 meses', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_MEJOR_CITA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Los slots de tiempo se generan en bloques fijos de 5 minutos (DATEDIFF(MINUTE,...)/5); El identificador de fila idrow se construye como concatenación de hora y minuto del inicio del slot (DATEPART HOUR + DATEPART MINUTE); Solo se reportan slots cuyo state=0, es decir, no cubiertos por una cita en estado 0 o 3; La ventana de búsqueda siempre es desde la fecha/hora actual hasta 4 meses adelante; El campo EMPRESA siempre es la constante ''ODO''', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_MEJOR_CITA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'agenda médica; cita; centro de atención; especialidad; profesional de la salud; disponibilidad horaria; slot de atención', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_MEJOR_CITA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RESULTSET: Devuelve filas con EMPRESA=''ODO'', centro, profesional, especialidad, fecha (formato 103) y hora (formato 108) de slots de 5 minutos cuyo state=0 (no ocupados)', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_MEJOR_CITA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si agasicita.codestcit IN (0, 3) → La cita se considera ocupante del slot y marca state=1 en @SCHEDULE para los intervalos de 5 minutos cubiertos por la cita else Citas con otros códigos de estado se ignoran y los slots quedan como disponibles; si AGA.fechorain >= GETDATE() AND AGA.fechorain <= DATEADD(MONTH, 4, GETDATE()) → La agenda entra al cálculo de slots disponibles else Agendas fuera de la ventana de 4 meses no se consideran', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_MEJOR_CITA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.agagemedc; dbo.agagemedd; dbo.inespecia; dbo.agasicita; dbo.adcenaten; dbo.inprofsal', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_MEJOR_CITA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_MEJOR_CITA';
-- GO
