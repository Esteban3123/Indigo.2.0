CREATE PROCEDURE [dbo].[SP_HC_ListarIndicacionesPaciente]
(
@Paciente varchar(25),
@Ingreso char(10)
)
AS
BEGIN
	SET NOCOUNT ON;
	  
select NUMINGRES as ingreso, IPCODPACI as Paciente,INDICAMED as IndicacionesMedicas, FECHISPAC as FechaHistoria,Rtrim(B.NOMMEDICO) as NameMedico from HCHISPACA A
	INNER JOIN INPROFSAL B ON A.CODPROSAL = B.CODPROSAL 
WHERE IPCODPACI = @Paciente and NUMINGRES = @Ingreso AND  (INDICAMED <> '' and INDICAMED IS not NULL)
order by FECHISPAC asc
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista todas las indicaciones médicas registradas en la historia clínica de un paciente para un ingreso u hospitalización específico. Consulta las notas clínicas (HCHISPACA) filtrando por cédula del paciente y número de ingreso, excluyendo registros sin indicaciones, y enriquece el resultado con el nombre del médico tratante obtenido del maestro de profesionales de la salud (INPROFSAL). Se usa para visualizar en orden cronológico las instrucciones o prescripciones médicas anotadas durante la atención, útil en módulos de historia clínica, seguimiento de tratamientos e informes de indicaciones por ingreso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarIndicacionesPaciente';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarIndicacionesPaciente';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las indicaciones médicas registradas en la historia clínica de un paciente para un ingreso específico, junto con el médico que las emitió, ordenadas cronológicamente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarIndicacionesPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente debe tener registros de historia clínica asociados al ingreso indicado; Debe existir correspondencia entre el profesional de la historia y el catálogo de profesionales de salud', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarIndicacionesPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Nunca retorna historias clínicas sin indicación médica (excluye NULL y cadenas vacías); Solo retorna historias del paciente y del ingreso especificados; El nombre del médico se entrega sin espacios en blanco a la derecha; El resultado siempre se ordena por fecha de historia en orden ascendente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarIndicacionesPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'paciente; ingreso; indicaciones médicas; historia clínica; médico; profesional de salud', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarIndicacionesPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCHISPACA: Devuelve registros de historia clínica del paciente e ingreso solicitados, filtrando únicamente aquellos cuya indicación médica no sea nula ni cadena vacía, ordenados ascendentemente por fecha de historia', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarIndicacionesPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCHISPACA; dbo.INPROFSAL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarIndicacionesPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarIndicacionesPaciente';
-- GO
