CREATE PROCEDURE [dbo].[ESE_SP_Asistencial_Examen_fisico]
@FechaIni DateTime,
@FechaFin DateTime
AS
SELECT  EX.IPCODPACI, EX.FECREGSIS, EX.PESOPACIE, EX.TALLAPACI, EX.IDETIPHIS, EX.NUMEFOLIO, EX.NUMINGRES, EX.CODCENATE

FROM HCEXFISIC EX
where FECREGSIS BETWEEN @FechaIni AND @FechaFin
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que consulta los registros de examen físico de pacientes dentro de un rango de fechas determinado. Extrae de la historia clínica información como el peso, la talla, el número de ingreso, el folio y el centro de atención de cada paciente. Se utiliza para reportes asistenciales que requieren revisar los signos antropométricos y datos del examen físico registrados en la atención clínica durante un período específico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_Asistencial_Examen_fisico';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_Asistencial_Examen_fisico';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consulta los registros de examen físico (peso, talla, identificación de paciente, folio, ingreso y centro de atención) realizados en un rango de fechas dado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_Examen_fisico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Se debe proveer un rango de fechas válido (inicio y fin) para filtrar los registros de examen físico.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_Examen_fisico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se devuelven registros de examen físico cuya fecha de registro esté dentro del rango solicitado (inclusive en ambos extremos).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_Examen_fisico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Examen físico; Paciente; Historia clínica; Folio; Ingreso; Centro de atención; Peso; Talla', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_Examen_fisico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCEXFISIC: Cuando FECREGSIS está entre @FechaIni y @FechaFin, se retorna el conjunto de datos del examen físico del paciente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_Examen_fisico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCEXFISIC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_Examen_fisico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_Examen_fisico';
-- GO
