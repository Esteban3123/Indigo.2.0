
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