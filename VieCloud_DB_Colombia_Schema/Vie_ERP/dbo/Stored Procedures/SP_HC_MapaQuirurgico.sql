
-- =============================================
-- Author:        <Andres Felipe Bonilla Tocora>
-- Create date:   <14/12/2017>
-- Description:   <Mapa quirurgico>
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_MapaQuirurgico]
(
    @fechaInicial DATETIME,
    @fechaFinal DATETIME,
	@JornadaMananaInicial DATETIME,
	@JornadaMananafinal DATETIME,
	@JornadaTardeInicial DATETIME,
	@JornadaTardeFinal DATETIME,
	@JornadaNocheInicial DATETIME,
	@JornadaNocheFinal DATETIME
	--,@DesdeRegistro INT
)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;

	DECLARE @TopRegistros INT = 200;
	DECLARE @fechaWhile DATE = @fechaInicial;  --fecha para el ciclo
    DECLARE @columnas VARCHAR(MAX);  --nombre de columnas para generar la estructura
    DECLARE @query AS NVARCHAR(MAX); --querys dinamicos
	SET @fechaFinal=  dateadd(day,1,@fechaFinal)

    --creacion de la tabla que tendra la estructura que se devolvera.
    CREATE TABLE dbo.#tmpMapaQx
    (
		REGISTRO INT IDENTITY(1,1) NOT NULL,
		FECHAX TIME NOT NULL,
        JORNADA NVARCHAR(25) NOT NULL,
        SALA NVARCHAR(120) NOT NULL,
		CONSTRAINT [PK_Primaria] PRIMARY KEY CLUSTERED
		([REGISTRO] ASC)
    );
	 
	-- ciclo para recorrer las echas
    WHILE @fechaWhile <= @fechaFinal
    BEGIN
        SELECT @columnas
            = CONCAT(
                        @columnas,
                        QUOTENAME(CONCAT(DATENAME(dw, @fechaWhile), ' (', CONVERT(VARCHAR(20), @fechaWhile, 103), ')')),
                        ' NVARCHAR(80) NULL, '
                    );
        SET @fechaWhile = DATEADD(DAY, 1, @fechaWhile);
    END;
    SET @columnas = LEFT(@columnas, LEN(@columnas) - 1);

	--print @columnas

	--alteracion de la tabla creada para agregar las columnas de fecha dinamica
    SELECT @query = 'ALTER TABLE #tmpMapaQx ADD ' + @columnas;
    EXEC sp_executesql @SQL = @query;

	

    DECLARE @RowCount INTEGER;
    DECLARE @Jornada AS NVARCHAR(25);
    DECLARE @Sala AS NVARCHAR(30);
	DECLARE @FECHAX AS TIME;
    DECLARE @columnaGrabar AS NVARCHAR(30);
    DECLARE @ValorGrabar AS NVARCHAR(130);
    SET @query = '';

	--creacion de tabla temporal donde tengo los datos que debo guardar en la tabla de estructura
    CREATE TABLE dbo.#TblParams
    (
		ID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
		JORNADA NVARCHAR(25) NOT NULL,
        SALA NVARCHAR(30) NOT NULL,
		FECHAX TIME NOT NULL,
        FECHA NVARCHAR(30) NOT NULL,
        PROFESIONAL NVARCHAR(130) NOT NULL
	);
	    INSERT INTO dbo.#TblParams
    SELECT dbo.JornadaMapaQuirurgico(
                                        CONCAT(
                                                  FORMAT(DATEPART(HOUR, A.FECHORAIN), '00'),
                                                  ':',
                                                  FORMAT(DATEPART(MINUTE, A.FECHORAIN), '00')
                                              ),
										 CONCAT(
                                            FORMAT(DATEPART(HOUR, @JornadaMananaInicial), '00'),
                                            ':',
                                            FORMAT(DATEPART(MINUTE, @JornadaMananaInicial), '00')
												),
										CONCAT(
                                            FORMAT(DATEPART(HOUR, @JornadaMananaFinal), '00'),
                                            ':',
                                            FORMAT(DATEPART(MINUTE, @JornadaMananaFinal), '00')
												),
										 CONCAT(
                                            FORMAT(DATEPART(HOUR, @JornadaTardeInicial), '00'),
                                            ':',
                                            FORMAT(DATEPART(MINUTE, @JornadaTardeInicial), '00')
												),
										 CONCAT(
                                            FORMAT(DATEPART(HOUR, @JornadaTardeFinal), '00'),
                                            ':',
                                            FORMAT(DATEPART(MINUTE, @JornadaTardeFinal), '00')
												),
										CONCAT(
                                            FORMAT(DATEPART(HOUR, @JornadaNocheInicial), '00'),
                                            ':',
                                            FORMAT(DATEPART(MINUTE, @JornadaNocheInicial), '00')
												),
										CONCAT(
                                            FORMAT(DATEPART(HOUR, @JornadaNocheFinal), '00'),
                                            ':',
                                            FORMAT(DATEPART(MINUTE, @JornadaNocheFinal), '00')
												)
                                    ) AS Jornada,
           CONCAT(B.CODIGSALA, ' - ', B.DESCRIPSAL) AS Sala,
		   CONCAT(FORMAT(DATEPART(HOUR, A.FECHORAIN), '00'), ':', FORMAT(DATEPART(MINUTE, A.FECHORAIN), '00')) AS FECHAX,
           CONCAT(DATENAME(dw, A.FECHORAIN), ' (', CONVERT(VARCHAR(20), A.FECHORAIN, 103), ')') AS FECHA,
           CONCAT(
                     RTRIM(C.NOMMEDICO),
                     ' | ',
                     CONCAT(FORMAT(DATEPART(HOUR, A.FECHORAIN), '00'), ':', FORMAT(DATEPART(MINUTE, A.FECHORAIN), '00')),
                     ' - ',
                     CONCAT(FORMAT(DATEPART(HOUR, A.FECHORAFI), '00'), ':', FORMAT(DATEPART(MINUTE, A.FECHORAFI), '00'))
                 ) AS Profesional
    FROM dbo.AGAGEMEDC A
        INNER JOIN dbo.AGENSALAC B
            ON A.AGENSALAC = B.CODCONCEC
        INNER JOIN dbo.INPROFSAL C
            ON A.CODPROSAL = C.CODPROSAL
    WHERE A.TIPAGEMED = '0'
          AND A.AGENSALAC IS NOT NULL
          AND A.FECHORAIN >= @fechaInicial
          AND A.FECHORAFI <= DATEADD(DAY,1,@fechaFinal)
    ORDER BY A.FECHORAIN
	--OFFSET @DesdeRegistro ROWS
	--FETCH NEXT @DesdeRegistro+@TopRegistros ROWS ONLY;

	DECLARE @contador INT = (SELECT COUNT(ID) FROM #TblParams )
	WHILE @contador <> 0
    BEGIN
				
	--consulta para traer el rowcount de la tabla que se recorrerra
		    SELECT 	@Jornada = JORNADA,
					@Sala = SALA,
					@FECHAX= FECHAX,
					@columnaGrabar = FECHA,
					@ValorGrabar = PROFESIONAL
				FROM #TblParams where ID = @contador;
		-- agregando los datos a la tabla con estructura
		
		if (SELECT @columnaGrabar FROM #tmpMapaQx WHERE JORNADA=@Jornada AND SALA=@Sala and FECHAX =@FECHAX) is null
			begin
				SET @query
	            = CONCAT('INSERT INTO #tmpMapaQx(FECHAX,jornada,sala,[' , @columnaGrabar , ']) VALUES (''' , @FECHAX , ''',''' , @Jornada , ''',''' , @Sala + ''',''' , @ValorGrabar , ''')');
				EXEC sp_executesql @SQL = @query;				
			end
			else
			begin
				SET @query
				= CONCAT('UPDATE #tmpMapaQx SET [', @columnaGrabar ,']=''',@ValorGrabar ,''' WHERE jornada=''',@Jornada ,''' AND SALA=''' , @Sala ,''' AND FECHAX=''',  @FECHAX ,''';')
				EXEC sp_executesql @SQL = @query;				
			end

		SET @contador =  @contador - 1 

    END;
	

	--eliminacion de las columnas que no son necesarias para el resultado
	DECLARE @UltimaColumna NVARCHAR(30)
	SELECT @UltimaColumna = SUBSTRING(@COLUMNAS,(LEN(@COLUMNAS) - CHARINDEX('[',REVERSE(@COLUMNAS))), (LEN(@COLUMNAS) - CHARINDEX(']',REVERSE(@COLUMNAS))) - (LEN(@COLUMNAS) - CHARINDEX('[',REVERSE(@COLUMNAS))) + 2)
		
	ALTER TABLE #tmpMapaQx drop constraint PK_Primaria
	SET @query	= 'ALTER TABLE #tmpMapaQx DROP COLUMN REGISTRO, COLUMN FECHAX, COLUMN ' + @UltimaColumna
	EXEC sp_executesql @SQL = @query;

	
		SELECT * FROM #tmpMapaQx
		ORDER BY JORNADA--registro
		--		ORDER BY JORNADA
	--)
	--AS PaginacioN
	--WHERE REGISTRO > @DesdeRegistro

END;
-------------------------------------------------------------
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el mapa quirúrgico del bloque de salas de cirugía para un rango de fechas determinado, mostrando en formato de tabla dinámica (columna por cada día) qué profesional de la salud ocupa cada sala quirúrgica en cada franja horaria (mañana, tarde o noche). Cruza la agenda médica (AGAGEMEDC) con las salas de atención (AGENSALAC) y el maestro de profesionales (INPROFSAL) para armar una vista tipo grilla donde las filas son sala, jornada y hora de inicio, y las columnas son los días del período consultado con el nombre del médico asignado. Se usa para planificar y visualizar la ocupación del quirófano, identificar disponibilidad de salas y gestionar la programación quirúrgica diaria por turno.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_MapaQuirurgico';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_MapaQuirurgico';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Construye dinámicamente un mapa quirúrgico pivotado por día (columnas), sala y jornada (mañana/tarde/noche), mostrando en cada celda el profesional y franja horaria de los bloques de agenda médica con sala asignada.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_MapaQuirurgico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las fechas inicial y final deben definir un rango válido para construir las columnas dinámicas; Deben suministrarse los rangos horarios de las tres jornadas (mañana, tarde, noche) para que la función de clasificación de jornada opere correctamente; Debe existir la función escalar dbo.JornadaMapaQuirurgico; Los registros de AGAGEMEDC deben tener relación válida con AGENSALAC (por CODCONCEC) e INPROFSAL (por CODPROSAL)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_MapaQuirurgico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran registros de agenda con TIPAGEMED=''0'' y con sala asignada (AGENSALAC IS NOT NULL); El rango de fechas se amplía un día: la fecha final efectiva se incrementa en 1 día antes de filtrar y construir columnas; Cada celda del pivote se identifica unívocamente por la combinación (JORNADA, SALA, FECHAX); no se generan duplicados, se actualiza la celda existente; La clasificación de jornada (mañana/tarde/noche) se delega a la función dbo.JornadaMapaQuirurgico según los rangos horarios parametrizados; El resultado final omite las columnas auxiliares REGISTRO y FECHAX, así como la última columna de fecha generada (día extra agregado al rango); Las columnas dinámicas del pivote se nombran con el patrón ''NombreDíaSemana (dd/mm/yyyy)''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_MapaQuirurgico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Mapa quirúrgico; Agenda médica; Sala quirúrgica/consultorio; Profesional de la salud; Jornada (mañana/tarde/noche); Programación de citas/cirugías', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_MapaQuirurgico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] #tmpMapaQx: Cuando para una combinación (JORNADA, SALA, FECHAX) la columna del día está NULL, se inserta una nueva fila con el profesional en la columna de la fecha correspondiente; [UPDATE] #tmpMapaQx: Cuando ya existe la fila para (JORNADA, SALA, FECHAX), se actualiza la columna del día con el valor del profesional y su franja horaria; [INSERT] #TblParams: Se carga con los bloques de AGAGEMEDC donde TIPAGEMED=''0'', AGENSALAC IS NOT NULL y FECHORAIN/FECHORAFI dentro del rango (con +1 día al final), enriquecidos con sala, jornada calculada y profesional; [RETURN_RESULT] #tmpMapaQx: Al finalizar se devuelve el contenido de la tabla pivote (sin REGISTRO, FECHAX ni la última columna de fecha) ordenado por JORNADA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_MapaQuirurgico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si La celda (jornada, sala, hora) en la columna del día correspondiente está NULL en la tabla pivote temporal → Se INSERTA una nueva fila con el valor del profesional en esa columna de fecha else Se hace UPDATE de la columna de fecha sobre la fila existente que coincide en jornada, sala y hora', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_MapaQuirurgico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.JornadaMapaQuirurgico', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_MapaQuirurgico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AGAGEMEDC; dbo.AGENSALAC; dbo.INPROFSAL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_MapaQuirurgico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_MapaQuirurgico';
-- GO
