
CREATE VIEW [dbo].[IND_AD_AD_Profesionales]
AS
SELECT        p.CODPROSAL AS Código, p.CODIGONIT AS Identificacion, p.NOMMEDICO AS Médico, p.IMDIRECCI AS Dirección, p.IMTELEFON AS Teléfono, 
                         p.IMTELMOVI AS Celular, p.TARJETAPR AS TarjetaProfesional, e.DESESPECI AS Especialidad1, E2.DESESPECI AS Especialidad2, 
                         e3.DESESPECI AS Especialidad3
FROM            dbo.INPROFSAL AS p INNER JOIN
                         dbo.INESPECIA AS e ON e.CODESPECI = p.CODESPEC1 LEFT OUTER JOIN
                         dbo.INESPECIA AS E2 ON E2.CODESPECI = p.CODESPEC2 LEFT OUTER JOIN
                         dbo.INESPECIA AS e3 ON e3.CODESPECI = p.CODESPEC3
WHERE        (p.ESTADOMED = '1') AND (p.CODPROSAL <> '996') AND (p.CODPROSAL <> '161') AND (p.CODPROSAL <> '998') AND (p.CODPROSAL <> '982') AND 
                         (p.CODPROSAL <> '997')
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que lista todos los profesionales de la salud activos registrados en el sistema (médicos, enfermeros, especialistas y otros prestadores), excluyendo códigos internos de uso sistémico. Combina el maestro de profesionales (INPROFSAL) con el catálogo de especialidades (INESPECIA) para mostrar hasta tres especialidades por profesional. Expone datos de identificación (cédula o NIT), nombre del médico, dirección, teléfono fijo, celular y número de tarjeta profesional. Útil para consultas de directorio médico, asignación de profesionales en agendamiento o admisiones, y validación de datos de contacto y habilitación del prestador.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'IND_AD_AD_Profesionales';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'IND_AD_AD_Profesionales';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone el listado de profesionales de la salud activos con sus datos de contacto, tarjeta profesional y hasta tres especialidades resueltas contra el catálogo.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AD_AD_Profesionales';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El catálogo de especialidades debe contener el código de la especialidad principal del profesional; de lo contrario, el profesional no aparece.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AD_AD_Profesionales';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen profesionales activos (estado = ''1'').; Se excluyen códigos de profesional reservados/genéricos: 996, 997, 998, 982 y 161.; Cada profesional listado debe tener al menos una especialidad principal válida en el catálogo (INNER JOIN sobre la primera especialidad).; La segunda y tercera especialidad son opcionales (LEFT JOIN), permitiendo profesionales con 1, 2 o 3 especialidades.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AD_AD_Profesionales';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Profesional de la salud; Médico; Especialidad médica; Tarjeta profesional', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AD_AD_Profesionales';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.INPROFSAL: Cuando ESTADOMED=''1'' y CODPROSAL no está en {''996'',''161'',''998'',''982'',''997''}, se retorna el profesional con su especialidad principal (INNER JOIN) y, si existen, la segunda y tercera especialidad (LEFT JOIN).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AD_AD_Profesionales';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INPROFSAL; dbo.INESPECIA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AD_AD_Profesionales';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AD_AD_Profesionales';
GO
