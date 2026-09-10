CREATE Procedure [dbo].[SPMOV_ListarDiagnosticosPaciente]
(
@Paciente Varchar(25),
@Ingreso Char(10)
)
AS
SELECT  A.CODDIAGNO as CodigoDX,
		RTRIM(B.NOMDIAGNO) as DescripcionDX,
	    Case 
		when [DIAINGEGR] = 'I' then 'INGRESO'
		when [DIAINGEGR] = 'E' then 'EGRESO'
		when [DIAINGEGR] = 'A' then 'AMBOS'
		END as Tipo,
		CASE
		when B.EXIGENOTI is null then 'No'
		when B.EXIGENOTI = 0 then 'No'
		when B. EXIGENOTI = 1 then 'Si'
		END as Notificación,
		RTRIM(OBSDIAGNO) as Observaciones
  FROM [INDIAGNOH] as A
  inner join INDIAGNOS B on B.CODDIAGNO = A.CODDIAGNO
  where A.IPCODPACI	= @Paciente AND A.NUMINGRES = @Ingreso
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista todos los diagnósticos clínicos (CIE-10) registrados para un paciente en un ingreso específico, combinando los diagnósticos de la historia clínica con el catálogo maestro de diagnósticos. Para cada diagnóstico devuelve el código CIE-10, su nombre o descripción, si fue asignado al ingreso, al egreso o a ambos momentos de la atención, si requiere notificación obligatoria a salud pública, y las observaciones del profesional. Se usa en la consulta de historia clínica para visualizar el listado completo de diagnósticos asociados a una hospitalización, urgencia o atención ambulatoria de un paciente identificado por su cédula y número de ingreso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPMOV_ListarDiagnosticosPaciente';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPMOV_ListarDiagnosticosPaciente';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los diagnósticos registrados para un paciente en un ingreso específico, junto con su descripción, tipo (ingreso/egreso/ambos), si exige notificación y observaciones.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarDiagnosticosPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir el paciente y el número de ingreso indicados con registros en la tabla histórica de diagnósticos; Los códigos de diagnóstico registrados en el paciente deben existir en el maestro de diagnósticos para que se devuelvan', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarDiagnosticosPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se listan diagnósticos asociados a un paciente y a un ingreso específicos; Cada diagnóstico del paciente debe estar catalogado en el maestro de diagnósticos (inner join obliga existencia); El tipo de diagnóstico siempre se traduce a una etiqueta legible (INGRESO/EGRESO/AMBOS); La marca de notificación se normaliza a ''Si''/''No'', tratando NULL como ''No''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarDiagnosticosPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'paciente; ingreso hospitalario; diagnóstico; diagnóstico de ingreso; diagnóstico de egreso; notificación obligatoria de diagnóstico', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarDiagnosticosPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Cuando IPCODPACI y NUMINGRES coinciden con los parámetros recibidos, se devuelve el conjunto de diagnósticos del paciente con su descripción, tipo traducido y bandera de notificación normalizada', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarDiagnosticosPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si DIAINGEGR = ''I'' → Se reporta el diagnóstico como tipo ''INGRESO'' else Si ''E'' → ''EGRESO''; si ''A'' → ''AMBOS''; si EXIGENOTI es NULL o = 0 → Notificación = ''No'' else Si EXIGENOTI = 1 → Notificación = ''Si''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarDiagnosticosPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INDIAGNOH; dbo.INDIAGNOS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarDiagnosticosPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarDiagnosticosPaciente';
-- GO
