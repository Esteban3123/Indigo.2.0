
create function [dbo].[anka_altasmedicas]( @datefrom datetime, @dateto datetime ) 
returns table 
	return SELECT     dbo.HCREGEGRE.NUMEFOLIO AS FOLIO, dbo.HCREGEGRE.NUMINGRES AS INGRESO, dbo.HCREGEGRE.FECALTPAC AS [ALTA MEDICA], 
                      dbo.HCREGEGRE.CODPROSAL AS [COD PROFESIONAL], dbo.INPROFSAL.NOMMEDICO AS [NOMBRE MEDICO], dbo.INPROFSAL.CODESPEC1 AS [COD ESPECIALIDAD],
                       dbo.INESPECIA.DESESPECI AS ESPECIALIDAD, dbo.INPROFSAL.CODIGONIT AS [NIT MEDICO]
			FROM         dbo.HCREGEGRE INNER JOIN
                      dbo.INPROFSAL ON dbo.HCREGEGRE.CODPROSAL = dbo.INPROFSAL.CODPROSAL INNER JOIN
                      dbo.INESPECIA ON dbo.INPROFSAL.CODESPEC1 = dbo.INESPECIA.CODESPECI
            where dbo.HCREGEGRE.FECALTPAC between @datefrom and @dateto;
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función de tabla que retorna el listado de altas médicas hospitalarias ocurridas en un rango de fechas indicado. Para cada egreso, combina el registro de alta del paciente (folio, número de ingreso y fecha de alta) con los datos del médico tratante que firmó el egreso (nombre, NIT/documento del médico) y su especialidad principal. Consulta el registro de egresos HCREGEGRE, el maestro de profesionales de salud INPROFSAL y el catálogo de especialidades INESPECIA. Se utiliza para reportería de productividad médica, control de egresos hospitalarios y auditoría de altas por especialidad o por médico en un período determinado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'anka_altasmedicas';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'anka_altasmedicas';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las altas médicas hospitalarias ocurridas en un rango de fechas, enriquecidas con el médico tratante, su especialidad y NIT.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'anka_altasmedicas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El profesional registrado en el egreso debe existir en el maestro de profesionales; El profesional debe tener una especialidad principal registrada y existente en el catálogo de especialidades; El rango de fechas de alta debe ser válido (desde ≤ hasta)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'anka_altasmedicas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen egresos cuya fecha de alta está dentro del rango solicitado (BETWEEN inclusivo); Solo se retornan egresos cuyo profesional existe en INPROFSAL y cuya especialidad principal existe en INESPECIA (INNER JOIN excluye huérfanos); Se reporta únicamente la especialidad principal (CODESPEC1) del profesional, no especialidades secundarias', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'anka_altasmedicas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Alta médica; Egreso hospitalario; Profesional de la salud; Médico tratante; Especialidad médica; NIT del médico; Folio de atención; Ingreso hospitalario', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'anka_altasmedicas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCREGEGRE: Cuando FECALTPAC está entre @datefrom y @dateto, se retorna folio, ingreso, fecha de alta, código y nombre del médico, especialidad y NIT', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'anka_altasmedicas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCREGEGRE; dbo.INPROFSAL; dbo.INESPECIA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'anka_altasmedicas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'anka_altasmedicas';
GO
