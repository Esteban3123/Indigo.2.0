
CREATE Procedure [dbo].[SPMOV_ListarMedicosHC]
(
@Paciente Varchar(25),
@Ingreso Char(10)
)
AS
SELECT DISTINCT 
		RTRIM(B.CODPROSAL) as CodigoMedico,
		RTRIM(B.MEDICFOTO) as Foto
  FROM HCHISPACA AS A
  Inner Join INPROFSAL B on B.CODPROSAL = A.CODPROSAL
  Inner Join INESPECIA C on C.CODESPECI = A.CODESPTRA
  where A.IPCODPACI	= @Paciente AND A.NUMINGRES = @Ingreso
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los médicos (profesionales de la salud) que generaron notas o folios en la historia clínica de un paciente durante un ingreso específico. Recibe como parámetros la cédula del paciente y el número de ingreso, y devuelve el código del médico junto con su foto de perfil. Combina las historias clínicas (HCHISPACA), el maestro de profesionales (INPROFSAL) y el catálogo de especialidades (INESPECIA) para obtener un listado único de los profesionales que atendieron al paciente en ese ingreso. Se usa para mostrar al equipo tratante o al historial de atenciones médicas de un episodio clínico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPMOV_ListarMedicosHC';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPMOV_ListarMedicosHC';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los médicos (con su foto) que han atendido a un paciente durante un ingreso específico, según los registros de la historia clínica.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarMedicosHC';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente y el número de ingreso deben existir en la historia clínica de pacientes; Los médicos referenciados deben existir en el maestro de profesionales de la salud; Las especialidades tratantes referenciadas deben existir en el maestro de especialidades', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarMedicosHC';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retornan médicos cuya especialidad tratante registrada en la historia clínica esté vigente en el catálogo de especialidades; El resultado nunca contiene duplicados (DISTINCT); Solo se incluyen médicos asociados al paciente e ingreso especificados', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarMedicosHC';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso hospitalario; Historia clínica; Médico tratante; Especialidad médica; Profesional de la salud', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarMedicosHC';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve códigos de médico y foto distintos cuando coinciden paciente e ingreso en la historia clínica y existen los respectivos profesionales y especialidades', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarMedicosHC';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCHISPACA; dbo.INPROFSAL; dbo.INESPECIA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarMedicosHC';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarMedicosHC';
-- GO
