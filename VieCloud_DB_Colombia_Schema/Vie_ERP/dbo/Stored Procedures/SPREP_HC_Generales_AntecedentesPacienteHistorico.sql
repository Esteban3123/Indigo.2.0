
CREATE PROCEDURE [dbo].[SPREP_HC_Generales_AntecedentesPacienteHistorico]
(
@CodigoPaciente Varchar(25)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
SELECT ANTMEDPAC AS 'ANTECEDENTES MEDICOS', ANTQUIPAC AS 'ANTECEDENTES QUIRURGICOS', ANTTRAPAC AS 'ANTECEDENTES TRANSFUCIONALES', 
ANTINMPAC AS 'ANTECEDENTES INMUNOLOGICOS', ANTALEPAC AS 'ANTECEDENTES ALERGICOS', ANTTRUPAC AS 'ANTECEDENTES TRAUMATICOS', ANTPSIPAC AS 'ANTECEDENTES PSICOLOGICOS PSIQUIATRICOS', 
ANTFARPAC AS 'ANTECEDENTES FARMACOLOGICOS', ANTFAMPAC AS 'ANTECEDENTES FAMILIARES', ANTTOXPAC AS 'ANTECEDENTES TOXICOS', ANTOTRPAC AS 'ANTECEDENTES OTROS'
    
FROM HCANTPACI WITH(NOLOCK)
	   
WHERE IPCODPACI=@CodigoPaciente AND NUMEFOLIO<='6' 	   

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reporte de antecedentes clínicos históricos de un paciente en la historia clínica. Dado el código (cédula) del paciente, consulta la tabla HCANTPACI y retorna todos los tipos de antecedentes registrados: médicos, quirúrgicos, transfusionales, inmunológicos, alérgicos, traumáticos, psicológicos/psiquiátricos, farmacológicos, familiares, tóxicos y otros, limitado a los primeros folios (folio ≤ 6). Se utiliza para generar el resumen de antecedentes personales y familiares del paciente en reportes de historia clínica general.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Generales_AntecedentesPacienteHistorico';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Generales_AntecedentesPacienteHistorico';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Recupera los antecedentes clínicos históricos de un paciente (médicos, quirúrgicos, transfusionales, alérgicos, familiares, etc.) limitados a los primeros folios de su historia.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_AntecedentesPacienteHistorico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe proporcionarse el código del paciente como parámetro de entrada.; Debe existir información de antecedentes registrada para el paciente en HCANTPACI.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_AntecedentesPacienteHistorico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran registros cuyo NUMEFOLIO sea menor o igual a ''6'' (comparación textual).; La consulta es de solo lectura y usa NOLOCK, por lo que puede leer datos no confirmados.; Los antecedentes retornados se restringen al paciente identificado por IPCODPACI.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_AntecedentesPacienteHistorico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Antecedentes médicos; Antecedentes quirúrgicos; Antecedentes transfusionales; Antecedentes inmunológicos; Antecedentes alérgicos; Antecedentes traumáticos; Antecedentes psicológicos/psiquiátricos; Antecedentes farmacológicos; Antecedentes familiares; Antecedentes tóxicos; Historia clínica; Folio', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_AntecedentesPacienteHistorico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCANTPACI: Cuando IPCODPACI coincide con el paciente y NUMEFOLIO <= ''6'', retorna las columnas de antecedentes con alias descriptivos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_AntecedentesPacienteHistorico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCANTPACI', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_AntecedentesPacienteHistorico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_AntecedentesPacienteHistorico';
-- GO
