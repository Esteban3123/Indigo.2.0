CREATE VIEW [dbo].[IND_AD_HC_PatologiasUAI]
AS
     SELECT DISTINCT TOP (100) PERCENT C.NUMEFOLIO AS Folio, 
                                       I.NUMINGRES AS NumeroIngreso, 
                                       C.IPCODPACI AS Identificación, 
                                       P.IPNOMCOMP AS NombrePaciente, 
                                       RTRIM(LTRIM(dbo.Edad(CONVERT(VARCHAR, P.IPFECNACI, 105), CONVERT(VARCHAR, GETDATE(), 105)))) AS Edad, 
                                       YEAR(GETDATE()) - YEAR(P.IPFECNACI) - 1 AS AnosPaciente,
                                       CASE
                                           WHEN P.IPSEXOPAC = '1'
                                           THEN 'Masculino'
                                           WHEN P.IPSEXOPAC = '2'
                                           THEN 'Femenino'
                                       END AS Sexo, 
                                       P.IPDIRECCI AS Direccion, 
                                       P.IPTELEFON AS Telefono, 
                                       P.IPTELMOVI AS Movil, 
                                       CONVERT(VARCHAR, I.IFECHAING, 100) AS FechaIngreso, 
                                       CONVERT(VARCHAR, E.FECALTPAC, 100) AS FechaAltaMedica, 
                                       A.NOMENTIDA AS Entidad, 
                                       UF.UFUDESCRI AS Servicio, 
                                       D.CODDIAGNO AS CodDiag1, 
                                       V.NOMDIAGNO AS DiagnosticoPpal, 
                                       D.CODDIAGNO AS CodDiag2, 
                                       V2.NOMDIAGNO AS Diagnostico2, 
                                       D.CODDIAGNO AS CodDiag3, 
                                       dbo.Especialidades(dbo.MedicosEsp(C.CODPROSAL)) AS Especialidad, 
                                       I.IAUTORIZA AS NumeroAutorizacion, 
                                       I.IOBSERVAC AS Observaciones, 
                                       I.IFECHAING AS [Fecha Ingreso Filtro]
     FROM dbo.HCHISPACA AS C WITH(NOLOCK)
          INNER JOIN dbo.ADCENATEN AS CA WITH(NOLOCK) ON CA.CODCENATE = C.CODCENATE
          INNER JOIN dbo.INUNIFUNC AS UF WITH(NOLOCK) ON UF.UFUCODIGO = C.UFUCODIGO
          INNER JOIN dbo.ADINGRESO AS I WITH(NOLOCK) ON I.NUMINGRES = C.NUMINGRES
          INNER JOIN dbo.INPACIENT AS P WITH(NOLOCK) ON P.IPCODPACI = C.IPCODPACI
          INNER JOIN dbo.INENTIDAD AS A WITH(NOLOCK) ON A.CODENTIDA = I.CODENTIDA
          INNER JOIN dbo.INDIAGNOP AS D WITH(NOLOCK) ON D.NUMINGRES = C.NUMINGRES
                                                        AND D.IPCODPACI = C.IPCODPACI
                                                        AND D.CODDIAPRI = 'True'
          INNER JOIN dbo.INDIAGNOS AS V WITH(NOLOCK) ON V.CODDIAGNO = D.CODDIAGNO
          LEFT OUTER JOIN dbo.INDIAGNOP AS D2 WITH(NOLOCK) ON D2.NUMINGRES = C.NUMINGRES
                                                              AND D2.IPCODPACI = C.IPCODPACI
                                                              AND D2.CODDIAPRI = 'False'
          LEFT OUTER JOIN dbo.INDIAGNOS AS V2 WITH(NOLOCK) ON V2.CODDIAGNO = D2.CODDIAGNO
          LEFT OUTER JOIN dbo.HCREGEGRE AS E WITH(NOLOCK) ON E.IPCODPACI = C.IPCODPACI
                                                             AND E.NUMINGRES = C.NUMINGRES
     WHERE(C.CODCENATE IN('001', '00102'))
          AND (C.UFUCODIGO IN('N30', 'N40'))
AND (I.IFECHAING > '01/01/2019 00:00:00');
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida las patologías y diagnósticos de pacientes atendidos en las Unidades de Atención Inmediata (UAI) de los centros de atención ''001'' y ''00102'', específicamente en las unidades funcionales N30 y N40, para ingresos registrados a partir del año 2019. Integra datos demográficos del paciente (cédula, nombre, edad, sexo, dirección, teléfono, celular), información del episodio de atención (número de ingreso, folio de historia clínica, fecha de ingreso, fecha de alta médica, entidad aseguradora, servicio o unidad funcional, número de autorización) y los diagnósticos clínicos CIE-10 asociados al ingreso, diferenciando el diagnóstico principal del secundario. Combina las historias clínicas (HCHISPACA), los ingresos (ADINGRESO), el maestro de pacientes (INPACIENT), las entidades pagadoras (INENTIDAD), las unidades funcionales (INUNIFUNC) y el catálogo de diagnósticos (INDIAGNOS y INDIAGNOP) para ofrecer un informe operativo y epidemiológico de las patologías atendidas en urgencias o UAI, útil para seguimiento clínico, indicadores de salud y reportería gerencial.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'IND_AD_HC_PatologiasUAI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'IND_AD_HC_PatologiasUAI';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista pacientes con sus diagnósticos, datos demográficos, ingreso y alta para las unidades funcionales de patologías UAI (N30, N40) en centros de atención específicos, a partir de 2019.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AD_HC_PatologiasUAI';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente debe tener historia clínica (HCHISPACA) asociada a un ingreso (ADINGRESO) y a un paciente (INPACIENT).; Debe existir al menos un diagnóstico marcado como principal (CODDIAPRI = ''True'') en INDIAGNOP para el ingreso.; El centro de atención debe ser ''001'' o ''00102''.; La unidad funcional debe ser ''N30'' o ''N40''.; La fecha de ingreso debe ser posterior al 01/01/2019.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AD_HC_PatologiasUAI';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen atenciones de los centros ''001'' y ''00102''.; Solo se incluyen unidades funcionales ''N30'' y ''N40'' (patologías UAI).; Solo ingresos posteriores al 01/01/2019.; Cada fila tiene siempre un diagnóstico principal asociado; el diagnóstico secundario y el egreso son opcionales.; La edad se calcula a la fecha actual (GETDATE) y no a la fecha de ingreso.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AD_HC_PatologiasUAI';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso/Admisión; Historia clínica; Diagnóstico principal y secundario (CIE-10); Unidad funcional/Servicio; Centro de atención; Entidad pagadora; Egreso/Alta médica; Especialidad médica; Autorización; Patologías UAI', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AD_HC_PatologiasUAI';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve filas distintas (TOP 100 PERCENT con DISTINCT) con datos de paciente, ingreso, diagnósticos y egreso filtradas por centro (''001'',''00102''), unidad funcional (''N30'',''N40'') y fecha de ingreso > 01/01/2019.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AD_HC_PatologiasUAI';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si P.IPSEXOPAC = ''1'' → Sexo se reporta como ''Masculino'' else Si IPSEXOPAC = ''2'' se reporta ''Femenino''; cualquier otro valor queda NULL.; si D.CODDIAPRI = ''True'' → Se considera el diagnóstico principal (INNER JOIN obligatorio). else D2.CODDIAPRI = ''False'' se considera diagnóstico secundario en LEFT JOIN (opcional).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AD_HC_PatologiasUAI';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.Edad; dbo.Especialidades; dbo.MedicosEsp', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AD_HC_PatologiasUAI';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCHISPACA; dbo.ADCENATEN; dbo.INUNIFUNC; dbo.ADINGRESO; dbo.INPACIENT; dbo.INENTIDAD; dbo.INDIAGNOP; dbo.INDIAGNOS; dbo.HCREGEGRE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AD_HC_PatologiasUAI';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AD_HC_PatologiasUAI';
GO
