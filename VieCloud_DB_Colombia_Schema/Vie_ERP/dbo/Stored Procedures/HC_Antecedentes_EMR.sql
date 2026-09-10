CREATE PROCEDURE [dbo].[HC_Antecedentes_EMR]
(
    @Paciente varchar(20)
)
WITH RECOMPILE
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT
        IPCODPACI,
        ANTMEDPAC,
        ANTQUIPAC,
        ANTTRAPAC,
        ANTINMPAC,
        ANTALEPAC,
        ANTTRUPAC,
        ANTPSIPAC,
        ANTFARPAC,
        ANTFAMPAC,
        ANTTOXPAC,
        ANTOTRPAC,
        ANTANESTE,
        ANTHABVID,
        ANTESCOLARES,
        ANTLABORALES,
        ANTNUTRICION,
        ANTODONTOLOG,
        ANTSOCIOECON,
        ANTUROLOGIASEXUAL,
        Ophthalmological
    FROM HCANTPACH
    WHERE IPCODPACI = @Paciente;

END;
GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Consulta el historial de antecedentes clínicos de un paciente específico desde la tabla `HCANTPACH`, recuperando múltiples categorías: médicos, quirúrgicos, traumáticos, inmunológicos, alérgicos, psiquiátricos, farmacológicos, familiares, tóxicos, hábitos de vida, escolares, laborales, nutricionales, odontológicos, socioeconómicos, urológicos y oftalmológicos. Se utiliza para poblar la sección de antecedentes en la historia clínica electrónica del paciente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'HC_Antecedentes_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'HC_Antecedentes_EMR';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Recupera el conjunto completo de antecedentes clínicos y no clínicos de un paciente para su consulta en la historia clínica electrónica.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'HC_Antecedentes_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir el registro de antecedentes asociado al paciente en la tabla de antecedentes; de lo contrario el resultado será vacío.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'HC_Antecedentes_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo retorna antecedentes del paciente cuyo identificador coincide exactamente con el solicitado.; No modifica datos: es una operación de solo lectura.; Se recompila el plan en cada ejecución (WITH RECOMPILE).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'HC_Antecedentes_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Antecedentes médicos del paciente; Antecedentes quirúrgicos; Antecedentes traumáticos; Antecedentes inmunológicos; Antecedentes alérgicos; Antecedentes transfusionales; Antecedentes psiquiátricos; Antecedentes farmacológicos; Antecedentes familiares; Antecedentes tóxicos; Antecedentes anestésicos; Hábitos de vida; Antecedentes escolares; Antecedentes laborales; Antecedentes nutricionales; Antecedentes odontológicos; Antecedentes socioeconómicos; Antecedentes urológicos/sexuales; Antecedentes oftalmológicos; Historia clínica electrónica (EMR)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'HC_Antecedentes_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCANTPACH: Cuando el paciente coincide con el identificador recibido, retorna sus antecedentes (médicos, quirúrgicos, traumáticos, inmunológicos, alérgicos, transfusionales, psiquiátricos, farmacológicos, familiares, tóxicos, anestésicos, hábitos de vida, escolares, laborales, nutrición, odontológicos, socioeconómicos, urológicos/sexuales y oftalmológicos).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'HC_Antecedentes_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCANTPACH', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'HC_Antecedentes_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'HC_Antecedentes_EMR';
-- GO
