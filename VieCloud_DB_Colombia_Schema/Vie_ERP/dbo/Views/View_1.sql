
CREATE VIEW [dbo].[View_1]
AS
SELECT        R.IPCODPACI AS DOCUMENTO, P.IPNOMCOMP AS NOMBRE, R.NUMINGRES AS INGRESO, U.UFUDESCRI AS UNIDAD_FUNCIONAL, R.FECHANACIM, 
                         CASE WHEN R.SEXRECNAC = '1' THEN 'Masculino' WHEN R.SEXRECNAC = '2' THEN 'Femenino' END AS SEXO, R.TALLARECI AS TALLA, R.PESORECNA AS PESO, D.NOMDIAGNO AS DIAGNOSTICO
FROM            dbo.HCRECINAC AS R INNER JOIN
                         dbo.HCRECNADI AS N ON R.NUMCONSEC = N.CONSECREC INNER JOIN
                         dbo.INDIAGNOS AS D ON N.CODDIAGNO = D.CODDIAGNO INNER JOIN
                         dbo.INPACIENT AS P ON R.IPCODPACI = P.IPCODPACI INNER JOIN
                         dbo.INUNIFUNC AS U ON R.UFUCODIGO = U.UFUCODIGO
WHERE        (R.CODCENATE = '002') AND (R.FECHANACIM >= '01-01-2015')
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Consolida información de recién nacidos registrados en el centro de atención con código ''002'' a partir del 1 de enero de 2015. Cruza el registro clínico neonatal con datos demográficos del paciente, la unidad funcional de atención, y el diagnóstico CIE-10 asociado, incluyendo medidas antropométricas (peso, talla) y sexo decodificado. Está orientada a reportes o consultas de seguimiento de neonatos con su diagnóstico principal.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'View_1';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'View_1';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone el listado de recién nacidos atendidos en el centro de atención ''002'' desde 2015, con sus datos demográficos, antropométricos, unidad funcional y diagnóstico asociado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'View_1';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existencia de relaciones entre el registro de recién nacido (HCRECINAC) y sus diagnósticos (HCRECNADI), el catálogo de diagnósticos (INDIAGNOS), el maestro de pacientes (INPACIENT) y el catálogo de unidades funcionales (INUNIFUNC).; El centro de atención con código ''002'' debe existir y tener registros de recién nacidos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'View_1';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen recién nacidos del centro de atención ''002''.; Solo se exponen registros con fecha de nacimiento desde el 01-01-2015 en adelante.; Cada registro debe tener diagnóstico, paciente y unidad funcional asociados (INNER JOIN obligatorio).; El sexo solo se traduce para los códigos ''1'' y ''2''; cualquier otro valor produce NULL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'View_1';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'recién nacido; paciente; diagnóstico; unidad funcional; centro de atención; sexo; talla; peso; ingreso', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'View_1';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCRECINAC: Cuando CODCENATE = ''002'' y FECHANACIM >= ''01-01-2015'', se retorna el set con documento, nombre, ingreso, unidad funcional, fecha de nacimiento, sexo traducido, talla, peso y diagnóstico del recién nacido.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'View_1';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si SEXRECNAC = ''1'' → Se reporta sexo como ''Masculino'' else Si SEXRECNAC = ''2'' se reporta ''Femenino''; otros valores quedan en NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'View_1';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCRECINAC; dbo.HCRECNADI; dbo.INDIAGNOS; dbo.INPACIENT; dbo.INUNIFUNC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'View_1';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'View_1';
GO
