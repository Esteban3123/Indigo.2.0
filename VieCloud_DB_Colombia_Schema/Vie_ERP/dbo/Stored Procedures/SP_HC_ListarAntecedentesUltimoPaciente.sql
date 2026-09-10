
-- =============================================
-- Author:		Danney Gonzalez
-- Create date: 07/10/2016
-- Description:	SP que lista los antecedentes del paciente.
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarAntecedentesUltimoPaciente] 
(
@PACIENTE varchar(25), 
@FECHALIMITE datetime  
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

SELECT (SELECT TOP 1 ANTMEDPAC FROM HCANTPACI WHERE IPCODPACI=@PACIENTE AND ANTMEDPAC IS NOT NULL AND FECHISPAC <= @FECHALIMITE ORDER BY FECHISPAC DESC) AS 'ANTECEDENTES MEDICOS', 
(SELECT TOP 1 ANTQUIPAC FROM HCANTPACI WHERE IPCODPACI=@PACIENTE AND ANTQUIPAC IS NOT NULL AND FECHISPAC <= @FECHALIMITE ORDER BY FECHISPAC DESC) AS 'ANTECEDENTES QUIRURGICOS',
(SELECT TOP 1 ANTTRAPAC FROM HCANTPACI WHERE IPCODPACI=@PACIENTE AND ANTTRAPAC IS NOT NULL AND FECHISPAC <= @FECHALIMITE ORDER BY FECHISPAC DESC) AS 'ANTECEDENTES TRANSFUCIONALES',
(SELECT TOP 1 ANTINMPAC FROM HCANTPACI WHERE IPCODPACI=@PACIENTE AND ANTINMPAC IS NOT NULL AND FECHISPAC <= @FECHALIMITE ORDER BY FECHISPAC DESC) AS 'ANTECEDENTES INMUNOLOGICOS',
(SELECT TOP 1 ANTALEPAC FROM HCANTPACI WHERE IPCODPACI=@PACIENTE AND ANTALEPAC IS NOT NULL AND FECHISPAC <= @FECHALIMITE ORDER BY FECHISPAC DESC) AS 'ANTECEDENTES ALERGICOS',
(SELECT TOP 1 ANTTRUPAC FROM HCANTPACI WHERE IPCODPACI=@PACIENTE AND ANTTRUPAC IS NOT NULL AND FECHISPAC <= @FECHALIMITE ORDER BY FECHISPAC DESC) AS 'ANTECEDENTES TRAUMATICOS',
(SELECT TOP 1 ANTPSIPAC FROM HCANTPACI WHERE IPCODPACI=@PACIENTE AND ANTPSIPAC IS NOT NULL AND FECHISPAC <= @FECHALIMITE ORDER BY FECHISPAC DESC) AS 'ANTECEDENTES PSICOLOGICOS PSIQUIATRICOS',
(SELECT TOP 1 ANTFARPAC FROM HCANTPACI WHERE IPCODPACI=@PACIENTE AND ANTFARPAC IS NOT NULL AND FECHISPAC <= @FECHALIMITE ORDER BY FECHISPAC DESC) AS 'ANTECEDENTES FARMACOLOGICOS',
(SELECT TOP 1 ANTFAMPAC FROM HCANTPACI WHERE IPCODPACI=@PACIENTE AND ANTFAMPAC IS NOT NULL AND FECHISPAC <= @FECHALIMITE ORDER BY FECHISPAC DESC) AS 'ANTECEDENTES FAMILIARES',
(SELECT TOP 1 ANTTOXPAC FROM HCANTPACI WHERE IPCODPACI=@PACIENTE AND ANTTOXPAC IS NOT NULL AND FECHISPAC <= @FECHALIMITE ORDER BY FECHISPAC DESC) AS 'ANTECEDENTES TOXICOS',
(SELECT TOP 1 ANTOTRPAC FROM HCANTPACI WHERE IPCODPACI=@PACIENTE AND ANTOTRPAC IS NOT NULL AND FECHISPAC <= @FECHALIMITE ORDER BY FECHISPAC DESC) AS 'ANTECEDENTES OTROS'
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Recupera el resumen más reciente de antecedentes clínicos de un paciente registrados hasta una fecha límite indicada. Consulta la tabla de antecedentes del paciente (HCANTPACI) y, para cada categoría de antecedente, trae el último registro no nulo anterior o igual a la fecha límite: antecedentes médicos, quirúrgicos, transfusionales, inmunológicos, alérgicos, traumáticos, psicológicos/psiquiátricos, farmacológicos, familiares, tóxicos y otros. Se usa en la historia clínica para mostrar al profesional de salud el perfil de antecedentes vigente del paciente al momento de una atención o ingreso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarAntecedentesUltimoPaciente';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarAntecedentesUltimoPaciente';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve, para un paciente dado y hasta una fecha límite, el último valor no nulo registrado de cada categoría de antecedentes clínicos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarAntecedentesUltimoPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir el paciente en HCANTPACI con registros previos a la fecha límite para obtener datos; de lo contrario las columnas retornan NULL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarAntecedentesUltimoPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Cada columna retornada se evalúa de forma independiente, por lo que los valores pueden provenir de registros (fechas) distintos.; Se ignoran registros con fecha posterior al límite indicado.; Solo se consideran valores no nulos del campo específico de cada antecedente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarAntecedentesUltimoPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Antecedentes médicos; Antecedentes quirúrgicos; Antecedentes transfusionales; Antecedentes inmunológicos; Antecedentes alérgicos; Antecedentes traumáticos; Antecedentes psicológicos/psiquiátricos; Antecedentes farmacológicos; Antecedentes familiares; Antecedentes tóxicos; Historia clínica', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarAntecedentesUltimoPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCANTPACI: Por cada tipo de antecedente, devuelve el valor más reciente (TOP 1 ORDER BY FECHISPAC DESC) cuyo campo correspondiente no sea NULL y cuya FECHISPAC sea menor o igual a la fecha límite, filtrado por el paciente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarAntecedentesUltimoPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCANTPACI', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarAntecedentesUltimoPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarAntecedentesUltimoPaciente';
-- GO
