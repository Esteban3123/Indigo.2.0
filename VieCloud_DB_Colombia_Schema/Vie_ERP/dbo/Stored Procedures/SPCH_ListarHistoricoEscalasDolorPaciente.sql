
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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista el historial completo de evaluaciones de dolor de un paciente para un ingreso específico, combinando dos fuentes: las escalas de dolor clínicas registradas en historia clínica (tipos 49, 90, 91, 93, 94) obtenidas mediante el procedimiento SPCH_ListarHistoricoEscalasPaciente, y las valoraciones de dolor del examen físico (tabla HCEXFISIC) donde el nivel de dolor se categoriza en sin dolor, suave, moderado o intenso según puntaje numérico del 0 al 10. Para cada registro devuelve la fecha, el nombre de la escala, el resultado interpretado, el nombre del profesional de salud y su profesión. El resultado se ordena cronológicamente de más reciente a más antiguo y se usa para visualizar la evolución del dolor del paciente en su historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPCH_ListarHistoricoEscalasDolorPaciente';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPCH_ListarHistoricoEscalasDolorPaciente';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve el histórico consolidado de escalas de valoración del dolor de un paciente, combinando escalas registradas y mediciones directas de dolor, ordenadas cronológicamente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarHistoricoEscalasDolorPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir el procedimiento SPCH_ListarHistoricoEscalasPaciente que provea los registros base de escalas; El paciente debe tener registros vinculados en INPROFSAL para resolver el profesional; Debe existir la función escalar dbo.TipoProfesionMedico', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarHistoricoEscalasDolorPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran escalas con TIPOESCALA en el conjunto fijo {94,49,90,91,93}; Solo se incluyen mediciones de dolor cuyo campo DOLOR tenga longitud mayor a 0; Los resultados siempre se entregan ordenados por fecha descendente; El filtro por número de ingreso en la sección de dolor está deshabilitado (comentado), por lo que devuelve dolor de todos los ingresos del paciente; La clasificación del dolor solapa el valor 4 (cae en ''suave'' por evaluación secuencial del CASE)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarHistoricoEscalasDolorPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'paciente; ingreso hospitalario; escala de dolor; valoración del dolor; profesional médico; tipo de profesión médica; examen físico', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarHistoricoEscalasDolorPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Resultset: Retorna unión de escalas (TIPOESCALA en 94,49,90,91,93) y registros de dolor de HCEXFISIC con LEN(DOLOR)>0, ordenados por FECHA DESC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarHistoricoEscalasDolorPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si TIPOESCALA IN (94,49,90,91,93) → Se incluye el registro como escala, anteponiendo ''Escala '' al nombre si no lo contiene; si CAST(DOLOR as int) = 0 → Clasifica como ''Sin dolor'' else Si 1-4 ''Dolor suave''; 4-6 ''Dolor moderado''; 7-10 ''Dolor intenso''; otro ''Sin valoración''; si ESCALA LIKE ''%Escala%'' → Se mantiene el nombre original else Se concatena el prefijo ''Escala ''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarHistoricoEscalasDolorPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.SPCH_ListarHistoricoEscalasPaciente; dbo.TipoProfesionMedico', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarHistoricoEscalasDolorPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INPROFSAL; dbo.HCEXFISIC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarHistoricoEscalasDolorPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarHistoricoEscalasDolorPaciente';
-- GO
