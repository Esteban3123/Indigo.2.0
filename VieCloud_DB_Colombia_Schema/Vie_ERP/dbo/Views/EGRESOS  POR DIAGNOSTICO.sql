
CREATE VIEW [dbo].[EGRESOS  POR DIAGNOSTICO]
AS
SELECT        dbo.ADINGRESO.TIPOINGRE, dbo.ADINGRESO.CODDIAEGR, dbo.ADINGRESO.IFECHAING, dbo.ADINGRESO.FECREGCRE, dbo.ADINGRESO.IPCODPACI, 
                         dbo.ADINGRESO.NUMINGRES, dbo.INPACIENT.IPNOMCOMP
FROM            dbo.ADINGRESO INNER JOIN
                         dbo.INPACIENT ON dbo.ADINGRESO.IPCODPACI = dbo.INPACIENT.IPCODPACI
WHERE        (dbo.ADINGRESO.TIPOINGRE = 2) AND (dbo.ADINGRESO.IFECHAING >= CONVERT(DATETIME, '2014-01-01 00:00:00', 102) AND 
                         dbo.ADINGRESO.IFECHAING <= CONVERT(DATETIME, '2014-12-31 00:00:00', 102)) AND (dbo.ADINGRESO.CODDIAEGR = 'I110')
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista para reporting puntual que filtra egresos hospitalarios (tipo de ingreso = 2) del año 2014 con diagnóstico de egreso codificado como I110 (hipertensión con enfermedad cardíaca, según CIE-10), uniendo admisiones con el maestro de pacientes para obtener el nombre completo. Está orientada a análisis o auditoría de egresos por ese diagnóstico específico en ese período fijo.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'EGRESOS  POR DIAGNOSTICO';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'EGRESOS  POR DIAGNOSTICO';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los egresos hospitalarios del año 2014 cuyo diagnóstico de egreso corresponde a hipertensión (CIE-10 I110), junto con datos básicos del paciente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'EGRESOS  POR DIAGNOSTICO';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existencia de correspondencia entre el paciente del ingreso y el maestro de pacientes (INNER JOIN obligatorio).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'EGRESOS  POR DIAGNOSTICO';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo expone registros cuyo tipo de ingreso es 2 (interpretado como egresos/hospitalización).; Solo expone registros con diagnóstico de egreso ''I110'' (hipertensión esencial con compromiso cardíaco).; Restringe la ventana temporal a ingresos del año 2014 (01/01/2014 a 31/12/2014).; Cada fila de ingreso se enriquece con el nombre completo del paciente vía INNER JOIN, por lo que excluye ingresos sin paciente maestro.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'EGRESOS  POR DIAGNOSTICO';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'egreso hospitalario; diagnóstico de egreso; paciente; ingreso/admisión; tipo de ingreso', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'EGRESOS  POR DIAGNOSTICO';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.ADINGRESO: Cuando TIPOINGRE=2, CODDIAEGR=''I110'' e IFECHAING está entre 2014-01-01 y 2014-12-31, retorna los datos del ingreso junto con el nombre del paciente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'EGRESOS  POR DIAGNOSTICO';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADINGRESO; dbo.INPACIENT', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'EGRESOS  POR DIAGNOSTICO';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'EGRESOS  POR DIAGNOSTICO';
GO
