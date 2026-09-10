
CREATE VIEW [dbo].[IND_AD_HC_ExtranjerosAtendidosSIRE]
AS
SELECT DISTINCT 
                         i.NUMINGRES AS Ingreso, i.IPCODPACI AS [Documento Identificación], 
                         CASE p.iptipodoc WHEN '1' THEN 'Cédula_Ciudadanía' WHEN '2' THEN 'Cédula_Extranjería' WHEN '3' THEN 'Tarjeta_Identidad' WHEN '4' THEN 'Registro_Civil'
                          WHEN '5' THEN 'Pasporte' WHEN '6' THEN 'AdultoSinIdentificación' WHEN '7' THEN 'MenorSinIdentificación' END AS [Tipo de Documento], 
                         P.IPNOMCOMP AS Paciente, ge.DESCRIPCION AS [Grupo Especial], hc.NUMEFOLIO AS Folio, i.IFECHAING AS [Fecha Ingreso], 
                         i.UFUCODIGO AS [Unidad ó Servicio], u.UFUDESCRI AS [Descripción Servicio de Ingreso], i.FECHOSPIT AS [Fecha Ingreso Hospitalización], 
                         i.FECREGCRE AS [Fecha Egreso], i.CODDIAING AS [CIE-10], d.NOMDIAGNO AS Diagnóstico, 
                         CASE p.ipsexopac WHEN '1' THEN 'Masculino' WHEN '2' THEN 'Femenino' END AS [Sexo Paciente], P.IPFECNACI AS [Fecha Nacimiento], YEAR(GETDATE()) 
                         - YEAR(P.IPFECNACI) - 1 AS EdadEnAnosHoy, P.OBSERVACI AS Observaciones
FROM            dbo.ADINGRESO AS i INNER JOIN
                         dbo.INPACIENT AS P ON P.IPCODPACI = i.IPCODPACI INNER JOIN
                         dbo.INUNIFUNC AS u ON u.UFUCODIGO = i.UFUCODIGO INNER JOIN
                         dbo.INDIAGNOS AS d ON i.CODDIAING = d.CODDIAGNO INNER JOIN
                         dbo.HCHISPACA AS hc ON hc.IPCODPACI = i.IPCODPACI AND hc.NUMINGRES = i.NUMINGRES LEFT OUTER JOIN
                         dbo.ADPOBESPEPAC AS gep ON gep.IPCODPACI = P.IPCODPACI LEFT OUTER JOIN
                         dbo.ADPOBESPE AS ge ON ge.ID = gep.IDADPOBESPE
WHERE        (i.CODCENATE = '001') AND (i.IESTADOIN <> 'A') AND (i.IFECHAING >= '01/01/2015 00:00:00') AND (P.IPTIPODOC = '2') AND (hc.IDETIPHIS = 'HCURGING1')
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida la información de pacientes extranjeros (con cédula de extranjería) atendidos en el centro de atención principal (001) desde enero de 2015, para reportería SIRE. Integra datos de ingresos o admisiones, historia clínica de urgencias, diagnóstico CIE-10, unidad funcional o servicio de atención, y grupo especial de población (como migrantes o víctimas) al que pertenece el paciente. Por cada ingreso activo muestra el número de ingreso, documento de identidad, tipo de documento, nombre completo del paciente, folio de historia clínica, fechas de ingreso y egreso u hospitalización, código y nombre del diagnóstico, sexo, fecha de nacimiento, edad calculada y observaciones. Se usa para cumplimiento de obligaciones de notificación y seguimiento de población extranjera atendida, facilitando auditorías, indicadores y reportes regulatorios sobre extranjeros en el sistema de salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'IND_AD_HC_ExtranjerosAtendidosSIRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'IND_AD_HC_ExtranjerosAtendidosSIRE';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los pacientes extranjeros (identificados con cédula de extranjería) atendidos en urgencias desde 2015 en el centro de atención ''001'', con datos demográficos, diagnóstico y servicio de ingreso, para reporte SIRE.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AD_HC_ExtranjerosAtendidosSIRE';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existencia de ingresos en ADINGRESO con paciente, unidad funcional, diagnóstico e historia clínica relacionados; Catálogo ADPOBESPE para resolver el grupo especial del paciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AD_HC_ExtranjerosAtendidosSIRE';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen ingresos del centro de atención ''001''; Se excluyen ingresos con estado ''A'' (anulados); Solo se consideran ingresos desde el 1 de enero de 2015; Solo pacientes con tipo de documento ''2'' (Cédula de Extranjería); Solo atenciones cuya historia clínica sea de tipo ''HCURGING1'' (urgencias); La edad se calcula como YEAR(GETDATE()) - YEAR(IPFECNACI) - 1; Resultados sin duplicados (DISTINCT)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AD_HC_ExtranjerosAtendidosSIRE';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente extranjero; Cédula de extranjería; Ingreso/Admisión; Urgencias; Historia clínica; Diagnóstico CIE-10; Unidad funcional/Servicio; Grupo poblacional especial; Reporte SIRE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AD_HC_ExtranjerosAtendidosSIRE';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.ADINGRESO: Cuando CODCENATE=''001'' AND IESTADOIN<>''A'' AND IFECHAING>=''2015-01-01'' AND el paciente tiene IPTIPODOC=''2'' (cédula de extranjería) AND la historia clínica es de tipo IDETIPHIS=''HCURGING1'', se retorna el ingreso con sus datos demográficos y clínicos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AD_HC_ExtranjerosAtendidosSIRE';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si p.iptipodoc según valor 1..7 → Traduce el código a etiqueta de tipo de documento (Cédula_Ciudadanía, Cédula_Extranjería, Tarjeta_Identidad, Registro_Civil, Pasporte, AdultoSinIdentificación, MenorSinIdentificación); si p.ipsexopac = ''1'' o ''2'' → Traduce a ''Masculino'' o ''Femenino'' respectivamente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AD_HC_ExtranjerosAtendidosSIRE';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADINGRESO; dbo.INPACIENT; dbo.INUNIFUNC; dbo.INDIAGNOS; dbo.HCHISPACA; dbo.ADPOBESPEPAC; dbo.ADPOBESPE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AD_HC_ExtranjerosAtendidosSIRE';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AD_HC_ExtranjerosAtendidosSIRE';
GO
