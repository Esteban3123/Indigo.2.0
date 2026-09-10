

create VIEW [dbo].[Aplicacion]
AS
SELECT     dbo.HCHOJMEZC.CONSECUTI, dbo.HCHOJMEZC.IPCODPACI, dbo.HCHOJMEZC.NUMINGRES, dbo.HCHOJMEZC.CODCENATE, dbo.HCHOJMEZC.FECAPLMED, 
                      dbo.CHREGESTA.FECINIEST, dbo.CHREGESTA.FECFINEST, dbo.HCHOJMEZC.CODPROAPL, dbo.CHCAMASHO.UFUCODIGO, 
                      dbo.HCHOJMEZC.UFUCODIGO AS aplicacion, dbo.HCHOJMEZC.IPCODPACI AS Expr1
FROM         dbo.CHREGESTA INNER JOIN
                      dbo.CHCAMASHO ON dbo.CHREGESTA.CODICAMAS = dbo.CHCAMASHO.CODICAMAS RIGHT OUTER JOIN
                      dbo.HCHOJMEZC ON dbo.CHREGESTA.NUMINGRES = dbo.HCHOJMEZC.NUMINGRES AND dbo.CHCAMASHO.UFUCODIGO <> dbo.HCHOJMEZC.UFUCODIGO
WHERE     (dbo.HCHOJMEZC.CODCENATE = '004') AND (dbo.HCHOJMEZC.FECAPLMED > CONVERT(DATETIME, '2011-03-30 00:00:00', 102)) AND 
                      (dbo.HCHOJMEZC.FECAPLMED BETWEEN dbo.CHREGESTA.FECINIEST AND dbo.CHREGESTA.FECFINEST)
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de auditoría que detecta inconsistencias entre la unidad funcional donde se aplicó una mezcla medicamentosa intravenosa y la unidad funcional de la cama asignada al paciente durante ese ingreso hospitalario. Cruza las mezclas aplicadas (HCHOJMEZC) con el historial de estancias y camas del paciente (CHREGESTA, CHCAMASHO) para identificar casos donde la unidad de aplicación del medicamento no coincide con la unidad donde el paciente estaba físicamente hospitalizado. Está filtrada al centro de atención ''004'' y a aplicaciones realizadas a partir del 30 de marzo de 2011, dentro del rango de fechas de cada estancia. Sirve para control de calidad, conciliación de enfermería y detección de errores de registro en la administración de mezclas a pacientes hospitalizados.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'Aplicacion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'Aplicacion';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone las aplicaciones de mezclas/medicamentos del centro ''004'' posteriores al 30/03/2011 cuya unidad funcional difiere de la unidad funcional de la cama asignada en la estancia vigente al momento de la aplicación.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Aplicacion';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro de aplicación de mezcla/medicamento con centro de atención ''004''.; La fecha de aplicación debe ser posterior al 30/03/2011.; Para cruzar con la estancia, debe existir un ingreso (NUMINGRES) coincidente y una cama asociada en el periodo de la aplicación.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Aplicacion';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran aplicaciones del centro de atención ''004''.; Solo se consideran aplicaciones con fecha posterior al 30/03/2011.; La fecha de aplicación del medicamento debe estar dentro del rango de la estancia (FECINIEST – FECFINEST).; Únicamente se exponen aplicaciones donde la unidad funcional de la cama de la estancia difiere de la unidad funcional registrada en la aplicación (traslado/discrepancia de UF).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Aplicacion';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Aplicación de medicamentos/mezclas; Ingreso hospitalario; Cama hospitalaria; Unidad funcional; Centro de atención; Estancia del paciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Aplicacion';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Devuelve aplicaciones de HCHOJMEZC con CODCENATE=''004'' y FECAPLMED>''2011-03-30'', cruzadas con la estancia (CHREGESTA) cuyo rango FECINIEST..FECFINEST contiene la fecha de aplicación, y donde la UFU de la cama (CHCAMASHO) es distinta de la UFU registrada en la aplicación.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Aplicacion';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCHOJMEZC; dbo.CHREGESTA; dbo.CHCAMASHO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Aplicacion';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Aplicacion';
GO
