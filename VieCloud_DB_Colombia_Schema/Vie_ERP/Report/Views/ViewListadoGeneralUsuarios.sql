
CREATE VIEW [Report].[ViewListadoGeneralUsuarios] as
SELECT 
 CAST(DB_NAME() AS VARCHAR(11)) AS ID_COMPANY,
 vs.TENAN_ID,
 vs.[Persona_Id],
 vs.[Codigo_Usuario],
 vs.[Identification],
 vs.[Nombre_Completo],
 vs.[Email],
 vs.[Tipo Rol],
 vs.[Rol_Codigo],
 vs.[Rol_Nombre],
 vs.[Tipo Usuario],
 vs.[Grupo_Codigo],
 vs.[Grupo_Nombre],
 vs.[Cargo],
 vs.[Estado],
 ips.CODUSUARI [Identificador],
 ips.CODESPEC1 [Nombre_Especif],
 ES.DESESPECI [Especif],
 CASE ips.TIPPROFES WHEN '1' THEN 'Medico General'
                    WHEN '2' THEN 'Medico Especialista'
                    WHEN '3' THEN 'Enfermera'
                    WHEN '4' THEN 'Auxiliar Enfermeria'
                    WHEN '5' THEN 'Odontologo General'
                    WHEN '6' THEN 'Odontologo Especialista'
                    WHEN '7' THEN 'Nutricionista'
                    WHEN '8' THEN 'Higienista'
                    WHEN '9' THEN 'Psicologo'
                    WHEN '10' THEN 'Trabajadora Social'
                    WHEN '11' THEN 'Promotor de Saneamiento'
                    WHEN '12' THEN 'Ingeniero Sanitario'
                    WHEN '13' THEN 'Medico Veterinario'
                    WHEN '14' THEN 'Ingeniero Alimento'
                    WHEN '15' THEN 'Auxiliar Bacteriologo'
                    WHEN '16' THEN 'Terapeuta'
                    WHEN '17' THEN 'Optometra'
                    WHEN '18' THEN 'Quimico Farmaceutico'
                    WHEN '19' THEN 'Radiologo'
                    WHEN '21' THEN 'Instrumentador Qx'
                    WHEN '22' THEN 'Auxiliar Patologia'
                    WHEN '23' THEN 'Otros'
                    WHEN '24' THEN 'Medico Interno'
                    WHEN '25' THEN 'Bacteriologo(a)'
                    WHEN '26' THEN 'Patólogo(a)'
                    WHEN '27' THEN 'Médico residente'
 END [Tipo_Profesional],
 CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
FROM 
 [Report].[ViewListadoUsuarios] vs
 LEFT JOIN dbo.INPROFSAL ips ON ips.CODPROSAL = vs.[Identification]
 LEFT JOIN dbo.INESPECIA ES ON ES.CODESPECI = ips.CODESPEC1
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporting que consolida el listado general de usuarios del sistema cruzando sus datos de acceso, roles y grupos con su perfil como profesional de salud. Enriquece cada usuario con su tipo de profesional (médico, enfermero, odontólogo, etc.), especialidad y descripción de especialidad, obtenidos del maestro de profesionales y el catálogo de especialidades. Está orientada a reportes multiempresa, incorporando el nombre de la base de datos como identificador de compañía y la fecha/hora actual en zona horaria Pakistan Standard Time.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewListadoGeneralUsuarios';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewListadoGeneralUsuarios';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en una sola vista el listado general de usuarios del sistema enriquecido con datos del profesional de la salud (especialidad y tipo de profesional) para reportería multi-tenant.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewListadoGeneralUsuarios';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La identificación del usuario en ViewListadoUsuarios debe coincidir con CODPROSAL de INPROFSAL para obtener datos profesionales (de lo contrario quedan en NULL por LEFT JOIN).; La zona horaria ''Pakistan Standard Time'' debe estar disponible en SQL Server para calcular ULT_ACTUAL.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewListadoGeneralUsuarios';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'ID_COMPANY siempre se obtiene del nombre de la base de datos actual (DB_NAME) truncado a 11 caracteres.; ULT_ACTUAL siempre refleja la fecha/hora actual convertida a la zona horaria ''Pakistan Standard Time''.; El catálogo de tipos de profesional está hardcodeado en la vista (no se lee de tabla), con códigos del 1 al 27 excluyendo el 20.; Los usuarios sin coincidencia en INPROFSAL aparecen igualmente, con campos profesionales en NULL (LEFT JOIN).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewListadoGeneralUsuarios';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Usuario del sistema; Profesional de la salud; Especialidad médica; Tipo de profesional (médico, enfermera, odontólogo, etc.); Rol; Tenant/Compañía', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewListadoGeneralUsuarios';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.ViewListadoGeneralUsuarios: Devuelve un registro por usuario de ViewListadoUsuarios, agregando especialidad (INESPECIA.DESESPECI) y descripción del tipo de profesional traducida desde INPROFSAL.TIPPROFES mediante CASE.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewListadoGeneralUsuarios';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ips.TIPPROFES (código numérico del tipo de profesional) → Se mapea a una etiqueta legible (1=Medico General, 2=Medico Especialista, 3=Enfermera, ... 27=Médico residente) else NULL si TIPPROFES no está entre los valores 1-27 listados (se omite el 20)', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewListadoGeneralUsuarios';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Report.ViewListadoUsuarios; dbo.INPROFSAL; dbo.INESPECIA', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewListadoGeneralUsuarios';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewListadoGeneralUsuarios';
GO
