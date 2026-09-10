

CREATE VIEW [dbo].[vwActividadesAgendamientoParamCUPS]
AS
(
SELECT AM.CODACTMED AS CodigoActividad, AM.DESACTMED AS DescripciónActividad, AM.ESTADOACT AS EstadoActividad, 
	AM.CODSERIPS AS CodigoCUPS,	I.DESSERIPS AS DescripcionCUPS, 
	CASE I.SERIPSDASH WHEN 1 THEN 'Laboratorio' WHEN 2 THEN 'Patología' WHEN 3 THEN 'Imágenes Diagnósticas' 
	WHEN 4 THEN 'Consulta Externa' WHEN 5 THEN 'Quimioterapia' WHEN 6 THEN 'Radioterapia' 
	WHEN 7 THEN 'Diálisis' WHEN 8 THEN 'Ninguno' WHEN 9 THEN 'Procedimiento No Qx' WHEN 10 THEN 'Procedimiento Qx' 
	WHEN 11 THEN 'Interconsultas' WHEN 7 THEN 'Otros Procedimientos' END AS Dashboard,
	CASE WHEN I.SIPSESTADO=1 THEN 'Activo' WHEN I.SIPSESTADO=2 THEN 'Inactivo' END AS EstadoCups
FROM dbo.AGACTIMED AS AM LEFT OUTER JOIN
	 dbo.INCUPSIPS AS I ON AM.CODSERIPS = I.CODSERIPS
	);
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que cruza las actividades médicas configuradas para agendamiento de citas con su código CUPS/IPS asociado, mostrando para cada actividad su código interno, descripción, estado (activo/inactivo), el código y nombre del servicio CUPS, la categoría de dashboard (Laboratorio, Imágenes Diagnósticas, Consulta Externa, Procedimiento Quirúrgico, entre otras) y el estado del CUPS. Integra el catálogo de actividades de agendamiento (AGACTIMED) con el maestro de servicios CUPS-IPS (INCUPSIPS) mediante el código de servicio. Sirve para parametrización y reportería de agendamiento, permitiendo identificar qué procedimiento o consulta CUPS está vinculado a cada actividad agendable y en qué módulo de dashboard se clasifica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwActividadesAgendamientoParamCUPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwActividadesAgendamientoParamCUPS';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone el catálogo de actividades médicas de agendamiento junto con su código CUPS asociado, clasificándolas por tipo de servicio (dashboard) y estado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'vwActividadesAgendamientoParamCUPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La relación actividad-CUPS se realiza por CODSERIPS mediante LEFT JOIN, por lo que toda actividad médica aparece aunque carezca de CUPS asociado; El valor 7 en SERIPSDASH siempre se mapea a ''Diálisis'' (la rama duplicada hacia ''Otros Procedimientos'' es código muerto inalcanzable); Solo se reconocen dos estados de CUPS: Activo (1) e Inactivo (2); cualquier otro valor produce NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'vwActividadesAgendamientoParamCUPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Actividad médica; Agendamiento; CUPS; Dashboard de servicios; Laboratorio; Patología; Imágenes Diagnósticas; Consulta Externa; Quimioterapia; Radioterapia; Diálisis; Procedimiento Quirúrgico; Procedimiento No Quirúrgico; Interconsultas', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'vwActividadesAgendamientoParamCUPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve todas las actividades médicas con su CUPS asociado vía LEFT JOIN, conservando las actividades aunque no tengan CUPS válido en INCUPSIPS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'vwActividadesAgendamientoParamCUPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si I.SERIPSDASH entre 1 y 11 → Traduce el código numérico a etiqueta de dashboard: 1=Laboratorio, 2=Patología, 3=Imágenes Diagnósticas, 4=Consulta Externa, 5=Quimioterapia, 6=Radioterapia, 7=Diálisis, 8=Ninguno, 9=Procedimiento No Qx, 10=Procedimiento Qx, 11=Interconsultas else NULL; si I.SIPSESTADO = 1 → EstadoCups = ''Activo'' else Si SIPSESTADO=2 entonces ''Inactivo'', en otro caso NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'vwActividadesAgendamientoParamCUPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AGACTIMED; dbo.INCUPSIPS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'vwActividadesAgendamientoParamCUPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'vwActividadesAgendamientoParamCUPS';
GO
