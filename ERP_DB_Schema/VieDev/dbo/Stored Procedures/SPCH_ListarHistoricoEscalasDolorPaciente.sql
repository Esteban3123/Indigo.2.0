
CREATE PROCEDURE [dbo].[SPCH_ListarHistoricoEscalasDolorPaciente]
(
@Paciente varchar(25),
@Ingreso Char(10)
)
AS
BEGIN
    SET NOCOUNT ON

	DECLARE @sql NVARCHAR(MAX)

	SET @sql = N'	
	CREATE TABLE #HistoricoEscalasDolor (
		FECHA datetime null,
		TIPOESCALA int null,
		ESCALA varchar(500) null,
		SELECCION bit DEFAULT 0 null,
		RESULTADO varchar(500) null,
		CODPROSAL char(20) null
	)

	INSERT INTO #HistoricoEscalasDolor
	exec [dbo].[SPCH_ListarHistoricoEscalasPaciente] @NumPaciente, @NumIngreso;
	
	WITH CTE_ESCALAS AS (
	SELECT 
		FECHA, 
		IIF(ESCALA LIKE ''%Escala%'', ESCALA, CONCAT(''Escala '', ESCALA)) AS ESCALA, 
		A.RESULTADO AS Resultado, 
		RTRIM(B.NOMMEDICO) AS PROFESIONAL, 
		dbo.TipoProfesionMedico(B.TIPPROFES) AS PROFESION 
	FROM #HistoricoEscalasDolor A
	INNER JOIN INPROFSAL B ON A.CODPROSAL = B. CODPROSAL 
	WHERE TIPOESCALA IN (''94'',''49'',''90'',''91'',''93'')
	),

	CTE_DOLOR AS (
	SELECT 
		A.FECREGITE AS FECHA, 
		''Dolor'' AS ESCALA, 
		CASE 
			WHEN CAST(A.DOLOR as int) = 0 THEN CONCAT(A.DOLOR,'' puntos - Sin dolor'')
			WHEN CAST(A.DOLOR as int)  BETWEEN 1 and 4 THEN CONCAT(A.DOLOR,'' puntos - Dolor suave'')
			WHEN CAST(A.DOLOR as int)  BETWEEN 4 and 6 THEN CONCAT(A.DOLOR,'' puntos - Dolor moderado'')
			WHEN CAST(A.DOLOR as int)  BETWEEN 7 and 10 THEN CONCAT(A.DOLOR,'' puntos - Dolor intenso'') 			
			ELSE ''Sin valoración'' 
		END AS Resultado, 
		RTRIM(B.NOMMEDICO) AS PROFESIONAL, 
		dbo.TipoProfesionMedico(B.TIPPROFES) AS PROFESION
	FROM HCEXFISIC A
	INNER JOIN INPROFSAL B ON A.CODPROSAL = B. CODPROSAL 
	WHERE IPCODPACI = @NumPaciente AND LEN(DOLOR) > 0--AND NUMINGRES = @NumIngreso
	)

	SELECT * FROM (SELECT * FROM CTE_ESCALAS UNION SELECT * FROM CTE_DOLOR) As Resultado ORDER BY FECHA DESC
	
	DROP TABLE #HistoricoEscalasDolor'

	EXEC sp_executesql @sql, N'@NumPaciente varchar(25), @NumIngreso Char(10)', @Paciente, @Ingreso


END