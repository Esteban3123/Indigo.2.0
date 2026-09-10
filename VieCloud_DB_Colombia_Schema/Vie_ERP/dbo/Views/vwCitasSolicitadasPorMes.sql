

CREATE VIEW [dbo].[vwCitasSolicitadasPorMes]
AS
    SELECT MONTH(CE.FECREGSIS) AS Mes, DATENAME(MONTH,CE.FECREGSIS) AS MesDeRegistro, EN.NOMENTIDA AS Entidad, ES.DESESPECI AS Especialidad, 
					CASE CE.ESTADO WHEN 1 THEN 'En Espera' WHEN 2 THEN 'Asignada' WHEN 3 THEN 'Cancelada' END AS EstadoDeLaCita,
					RTrim(US.NOMUSUARI) AS Usuario, COUNT(*) AS Cantidad
FROM   AGCITAESP AS CE INNER JOIN 
                    INPACIENT AS PA ON CE.IPCODPACI = PA.IPCODPACI INNER JOIN
					SEGusuaru AS US ON CE.CODUSUASI=US.CODUSUARI INNER JOIN 
                    INESPECIA AS ES ON CE.CODESPECI = ES.CODESPECI INNER JOIN 
                    INENTIDAD AS EN ON PA.CODENTIDA = EN.CODENTIDA INNER JOIN 
                    AGACTIMED AS AGAC ON CE.CODACTMED = AGAC.CODACTMED LEFT OUTER JOIN 
                    INPROFSAL AS PS ON CE.CODPROSAL = PS.CODPROSAL 
                WHERE CE.FECREGSIS>='20170101'
				GROUP BY DATENAME(MONTH,CE.FECREGSIS), MONTH(CE.FECREGSIS), EN.NOMENTIDA, ES.DESESPECI, CE.ESTADO, US.NOMUSUARI;
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resumen mensual de citas solicitadas por especialidad y entidad aseguradora (EPS/ARS). Agrupa las citas agendadas desde enero de 2017 mostrando el mes de registro, la entidad pagadora del paciente, la especialidad médica solicitada, el estado de la cita (En Espera, Asignada o Cancelada), el usuario que registró la solicitud y la cantidad de citas. Combina datos de la tabla de citas (AGCITAESP) con el paciente, su entidad, la especialidad, la actividad médica, el profesional asignado y el usuario del sistema, permitiendo reportes de demanda de consultas por mes para análisis operativo y de gestión de agendamiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwCitasSolicitadasPorMes';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwCitasSolicitadasPorMes';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Reporta el conteo mensual de citas médicas especializadas solicitadas desde 2017, agrupadas por entidad, especialidad, estado y usuario asignador.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'vwCitasSolicitadasPorMes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las citas deben tener paciente, usuario asignador, especialidad, entidad y actividad médica válidos (INNER JOIN); profesional de salud es opcional; Solo se consideran citas con FECREGSIS >= 01/01/2017', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'vwCitasSolicitadasPorMes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen citas registradas a partir del 01/01/2017; Una cita sin paciente, especialidad, entidad de paciente, usuario asignador o actividad médica se excluye del reporte; El estado de cita se muestra solo para los valores 1, 2 y 3; otros valores aparecen como NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'vwCitasSolicitadasPorMes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cita médica especializada; Paciente; Especialidad; Entidad (aseguradora/convenio); Estado de cita (En Espera, Asignada, Cancelada); Usuario asignador; Profesional de salud; Actividad médica', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'vwCitasSolicitadasPorMes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] AGCITAESP: Devuelve agregación COUNT(*) de citas agrupadas por mes, entidad, especialidad, estado y usuario para registros desde 2017-01-01', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'vwCitasSolicitadasPorMes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si CE.ESTADO = 1 → Etiqueta cita como ''En Espera''; si CE.ESTADO = 2 → Etiqueta cita como ''Asignada''; si CE.ESTADO = 3 → Etiqueta cita como ''Cancelada'' else NULL (otros estados no se traducen)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'vwCitasSolicitadasPorMes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AGCITAESP; dbo.INPACIENT; dbo.SEGusuaru; dbo.INESPECIA; dbo.INENTIDAD; dbo.AGACTIMED; dbo.INPROFSAL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'vwCitasSolicitadasPorMes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'vwCitasSolicitadasPorMes';
GO
