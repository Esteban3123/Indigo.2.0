

CREATE VIEW [Billing].[ViewScheduleActivityByEspeciality]
AS
	SELECT	A.CODACTMED AS ActivityCode,
			C.DESACTMED AS ActivityDescription,
			(CASE 
				WHEN ACTIVICON = 1 THEN 'QX' + ' - ' + A.CODACTMED + ' - ' +  DESACTMED 
				ELSE 'CE' + ' - ' + A.CODACTMED + ' - ' +  DESACTMED 
				END) AS Activities,
			C.ACTIVICON as ActivityType ,
			A.CODESPECI AS EspecialityCode
FROM dbo.AGESPACTI A 
  INNER JOIN dbo.INESPECIA B ON A.CODESPECI=B.CODESPECI
  INNER JOIN dbo.AGACTIMED C ON A.CODACTMED=C.CODACTMED
WHERE ESTADOACT=1
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista las actividades médicas habilitadas para agendamiento de citas, organizadas por especialidad. Combina la relación entre especialidades y actividades (AGESPACTI), el catálogo de especialidades (INESPECIA) y el catálogo de actividades médicas (AGACTIMED), mostrando solo las actividades activas. Para cada registro expone el código y descripción de la actividad, la especialidad a la que pertenece, y una etiqueta legible que clasifica la actividad como quirúrgica (QX) o de consulta externa (CE), útil para poblar selectores y filtros en el portal de agendamiento de citas médicas.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewScheduleActivityByEspeciality';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewScheduleActivityByEspeciality';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone las actividades médicas activas habilitadas por especialidad para agendamiento, diferenciando visualmente entre quirúrgicas (QX) y consulta externa (CE).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewScheduleActivityByEspeciality';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La actividad debe estar vinculada a una especialidad existente en INESPECIA; La actividad debe existir en el catálogo AGACTIMED; El registro de la relación especialidad-actividad debe estar activo (ESTADOACT=1)', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewScheduleActivityByEspeciality';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Sólo se exponen actividades cuyo vínculo con la especialidad esté activo (ESTADOACT=1); Toda fila retornada tiene una especialidad válida en INESPECIA y una actividad válida en AGACTIMED (INNER JOIN); El tipo de actividad se clasifica de forma binaria: QX (ACTIVICON=1) o CE (cualquier otro valor)', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewScheduleActivityByEspeciality';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'especialidad médica; actividad médica; consulta externa; procedimiento quirúrgico; agendamiento de citas', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewScheduleActivityByEspeciality';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Devuelve sólo combinaciones especialidad-actividad con ESTADOACT=1 (activas); [RETURN_RESULT] : Construye etiqueta ''Activities'' con prefijo ''QX'' cuando ACTIVICON=1, ''CE'' en caso contrario, concatenado con el código y descripción de la actividad', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewScheduleActivityByEspeciality';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ACTIVICON = 1 → Etiqueta de actividad se prefija con ''QX'' (procedimiento quirúrgico) else Etiqueta de actividad se prefija con ''CE'' (consulta externa)', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewScheduleActivityByEspeciality';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AGESPACTI; dbo.INESPECIA; dbo.AGACTIMED', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewScheduleActivityByEspeciality';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewScheduleActivityByEspeciality';
GO
