
CREATE PROCEDURE [dbo].[SPCH_ListarExamenFisicoPacienteSeleccionado]
(
@Paciente Varchar(25),
@NumeroFolio char(10)
)
AS
BEGIN
	SET NOCOUNT ON;

SELECT RTRIM(TENARTSIS) AS TENARTSIS, RTRIM(TENARTDIA) AS TENARTDIA, RTRIM(TEMPERPAC) AS TEMPERPAC, RTRIM(FRECARPAC) AS FRECARPAC, RTRIM(FRERESPAC) AS FRERESPAC, RTRIM(REGSO2PAC) AS REGSO2PAC, TALLAPACI AS TALLAPACI, PESOPACIE/1000 AS PESOPACIE 
,FECREGITE as FechaRegistro
FROM dbo.HCEXFISIC 
WHERE NUMEFOLIO=@NumeroFolio AND IPCODPACI=@Paciente
end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que recupera los signos vitales y medidas antropométricas registradas en el examen físico de un paciente específico dentro de la historia clínica. Dado el código o cédula del paciente y el número de folio de la atención, consulta la tabla HCEXFISIC para obtener tensión arterial sistólica y diastólica, temperatura, frecuencia cardíaca, frecuencia respiratoria, saturación de oxígeno (SpO2), talla y peso del paciente, junto con la fecha de registro. Se utiliza para visualizar o consultar los controles de signos vitales de un encuentro clínico concreto, típicamente desde la historia clínica electrónica en hospitalización, urgencias o consulta externa.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPCH_ListarExamenFisicoPacienteSeleccionado';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPCH_ListarExamenFisicoPacienteSeleccionado';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Recupera los signos vitales y medidas antropométricas del examen físico de un paciente para un folio de atención específico.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarExamenFisicoPacienteSeleccionado';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro de examen físico cuyo folio y código de paciente coincidan con los parámetros recibidos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarExamenFisicoPacienteSeleccionado';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El peso almacenado se asume en gramos y se expone dividido entre 1000 (kilogramos).; Los campos de texto se entregan sin espacios finales (RTRIM).; El examen físico se identifica unívocamente por la combinación de folio y paciente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarExamenFisicoPacienteSeleccionado';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Examen físico; Paciente; Signos vitales (presión arterial sistólica/diastólica, temperatura, frecuencia cardiaca, frecuencia respiratoria, saturación O2); Talla; Peso; Folio de atención', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarExamenFisicoPacienteSeleccionado';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCEXFISIC: Cuando NUMEFOLIO=@NumeroFolio AND IPCODPACI=@Paciente, retorna signos vitales, talla, peso convertido a kilogramos (PESOPACIE/1000) y fecha de registro del examen físico.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarExamenFisicoPacienteSeleccionado';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCEXFISIC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarExamenFisicoPacienteSeleccionado';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarExamenFisicoPacienteSeleccionado';
-- GO
