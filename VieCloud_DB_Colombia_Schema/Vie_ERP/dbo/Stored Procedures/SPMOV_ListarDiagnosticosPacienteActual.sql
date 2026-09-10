CREATE Procedure [dbo].[SPMOV_ListarDiagnosticosPacienteActual]
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
  FROM [INDIAGNOP] as A
  inner join INDIAGNOS B on B.CODDIAGNO = A.CODDIAGNO
  where A.IPCODPACI	= @Paciente AND A.NUMINGRES = @Ingreso
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los diagnósticos clínicos registrados para un paciente en un ingreso o atención específica. Cruza los diagnósticos del paciente (INDIAGNOP) con el catálogo maestro CIE-10 (INDIAGNOS) para devolver el código y nombre del diagnóstico, si aplica al ingreso, al egreso o a ambos momentos, si requiere notificación obligatoria a entidades de salud, y las observaciones clínicas asociadas. Se usa en la historia clínica para consultar el listado de diagnósticos activos o previos de un paciente durante una hospitalización, urgencia o atención ambulatoria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPMOV_ListarDiagnosticosPacienteActual';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPMOV_ListarDiagnosticosPacienteActual';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los diagnósticos registrados para un paciente en un ingreso específico, indicando descripción, tipo (ingreso/egreso/ambos), si exige notificación y observaciones.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarDiagnosticosPacienteActual';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir relación paciente-ingreso registrada en la tabla de diagnósticos del paciente; Los códigos de diagnóstico registrados al paciente deben existir en el catálogo maestro de diagnósticos para ser retornados', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarDiagnosticosPacienteActual';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se devuelven diagnósticos asociados al paciente y número de ingreso indicados; Cada diagnóstico listado debe existir en el catálogo maestro de diagnósticos (INNER JOIN con INDIAGNOS); El tipo de diagnóstico se normaliza al dominio {INGRESO, EGRESO, AMBOS}; La exigencia de notificación se normaliza a {Si, No}, tratando NULL como ''No''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarDiagnosticosPacienteActual';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'paciente; ingreso/episodio; diagnóstico; diagnóstico de ingreso; diagnóstico de egreso; notificación obligatoria de diagnóstico', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarDiagnosticosPacienteActual';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.INDIAGNOP: Cuando existen diagnósticos del paciente con su ingreso y el código está en el catálogo, se retorna el conjunto con código, descripción, tipo, notificación y observaciones', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarDiagnosticosPacienteActual';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si DIAINGEGR = ''I'' → Se etiqueta el diagnóstico como ''INGRESO''; si DIAINGEGR = ''E'' → Se etiqueta el diagnóstico como ''EGRESO''; si DIAINGEGR = ''A'' → Se etiqueta el diagnóstico como ''AMBOS''; si EXIGENOTI es NULL o 0 → Se marca como ''No'' la exigencia de notificación else Si EXIGENOTI = 1 se marca como ''Si''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarDiagnosticosPacienteActual';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INDIAGNOP; dbo.INDIAGNOS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarDiagnosticosPacienteActual';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarDiagnosticosPacienteActual';
-- GO
