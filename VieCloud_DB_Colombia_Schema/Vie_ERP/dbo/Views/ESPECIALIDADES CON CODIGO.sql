
CREATE VIEW [dbo].[ESPECIALIDADES CON CODIGO]
AS
SELECT     dbo.HCESPSERU.CODSERINT, dbo.INCUPSIPS.DESSERIPS, dbo.INESPECIA.DESESPECI
FROM         dbo.HCESPSERU INNER JOIN
                      dbo.INCUPSIPS ON dbo.HCESPSERU.CODSERINT = dbo.INCUPSIPS.CODSERIPS INNER JOIN
                      dbo.INESPECIA ON dbo.HCESPSERU.CODESPECI = dbo.INESPECIA.CODESPECI
WHERE     (dbo.INCUPSIPS.DESSERIPS LIKE '%INTERCONSULTA POR MEDICINA ESPECIALIZADA%')
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de apoyo para la gestión de interconsultas por medicina especializada. Cruza el catálogo CUPS con las especialidades médicas, filtrando únicamente los servicios cuya descripción corresponde a "INTERCONSULTA POR MEDICINA ESPECIALIZADA". Devuelve el código interno de servicio junto con la descripción CUPS y el nombre de la especialidad, permitiendo identificar qué especialidades tienen habilitado este tipo de servicio en el sistema.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ESPECIALIDADES CON CODIGO';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ESPECIALIDADES CON CODIGO';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Listar las especialidades médicas y sus códigos de servicio asociados que corresponden a interconsultas por medicina especializada.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ESPECIALIDADES CON CODIGO';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las tablas de catálogo de especialidades, CUPS/IPS y la relación especialidad-servicio deben estar pobladas y consistentes en sus códigos.; Debe existir al menos un servicio cuya descripción contenga el texto ''INTERCONSULTA POR MEDICINA ESPECIALIZADA'' para producir resultados.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ESPECIALIDADES CON CODIGO';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen servicios cuya descripción contiene ''INTERCONSULTA POR MEDICINA ESPECIALIZADA''.; Solo se incluyen combinaciones que tengan correspondencia simultánea entre el código de servicio interno (CUPS) y la especialidad asociada (INNER JOIN).; El código de servicio interno se equipara con el código de servicio IPS (CODSERINT = CODSERIPS).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ESPECIALIDADES CON CODIGO';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Especialidad médica; Servicio CUPS/IPS; Interconsulta por medicina especializada', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ESPECIALIDADES CON CODIGO';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCESPSERU: Cuando la descripción del servicio IPS contiene ''INTERCONSULTA POR MEDICINA ESPECIALIZADA'', se retorna el código de servicio interno junto con la descripción del servicio y la descripción de la especialidad.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ESPECIALIDADES CON CODIGO';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCESPSERU; dbo.INCUPSIPS; dbo.INESPECIA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ESPECIALIDADES CON CODIGO';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ESPECIALIDADES CON CODIGO';
GO
