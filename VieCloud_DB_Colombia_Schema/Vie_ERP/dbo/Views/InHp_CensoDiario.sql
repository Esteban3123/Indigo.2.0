
CREATE VIEW [dbo].[InHp_CensoDiario]
AS
SELECT     TOP (100) PERCENT I.NUMINGRES AS NumeroIngreso, E.IPCODPACI AS Identificación, P.NUMCARPET AS [N°HC], P.IPNOMCOMP AS NombrePaciente, 
                      RTRIM(LTRIM(dbo.Edad(CONVERT(varchar, P.IPFECNACI, 105), CONVERT(varchar, GETDATE(), 105)))) AS Edad, P.IPDIRECCI AS Direccion, P.IPTELEFON AS Telefono, 
                      P.IPTELMOVI AS Movil, C.DESCCAMAS AS Cama, 
                      CASE c.CODCLAHAB WHEN '1' THEN 'SALA OBSERVACION' WHEN '2' THEN 'SALA PROCEDIMIENTO' WHEN '3' THEN 'SALA RECUPERACION' WHEN '4' THEN 'HABITACION 1 CAMA'
                       WHEN '5' THEN 'HABITACION 2 CAMAS' WHEN '6' THEN 'HABITACION 3 CAMAS' WHEN '7' THEN 'HABITACION 4 CAMAS' WHEN '8' THEN 'SUITE' WHEN '9' THEN 'HAB.ESPECIAL'
                       WHEN '10' THEN 'UCI' WHEN '11' THEN 'OTRO' END AS ClaseHabitacion, TE.DESTIPEST AS [Tipo de Estancia], 
                      CASE c.CODCLACAM WHEN '1' THEN 'ObservacionUrgencias' WHEN '2' THEN 'Recuperacion Post Qx' WHEN '3' THEN 'HOSPITALARIA' END AS ClaseCama, 
                      CONVERT(varchar, E.FECINIEST, 100) AS FechaIngreso, A.NOMENTIDA AS Entidad, UF.UFUDESCRI AS Servicio, D.CODDIAGNO AS CodDiag, 
                      V.NOMDIAGNO AS DiagnosticoPpal, Et.DESESPECI AS Especialidad, EM.DESESPECI AS EspecialidadMedicaIngreso, 
                      CASE WHEN P.IPSEXOPAC = '1' THEN 'Masculino' WHEN P.IPSEXOPAC = '2' THEN 'Femenino' END AS Sexo, I.IAUTORIZA AS NumeroAutorizacion, 
                      I.IOBSERVAC AS Observaciones, - DATEDIFF(yy, GETDATE(), P.IPFECNACI) AS EdadAños, dbo.DiferenciaDias(E.FECINIEST) AS [DIAS TRANSCURRIDOS], 
                      '1' AS Cantidad
FROM         dbo.CHCAMASHO AS C INNER JOIN
                      dbo.ADCENATEN AS CA ON CA.CODCENATE = C.CODCENATE INNER JOIN
                      dbo.INUNIFUNC AS UF ON UF.UFUCODIGO = C.UFUCODIGO INNER JOIN
                      dbo.CHREGESTA AS E ON E.CODICAMAS = C.CODICAMAS AND E.REGESTADO = '1' INNER JOIN
                      dbo.CHTIPESTA AS TE ON TE.CODTIPEST = E.CODTIPEST INNER JOIN
                      dbo.ADINGRESO AS I ON I.NUMINGRES = E.NUMINGRES INNER JOIN
                      dbo.INPACIENT AS P ON P.IPCODPACI = E.IPCODPACI INNER JOIN
                      dbo.INENTIDAD AS A ON A.CODENTIDA = I.CODENTIDA LEFT OUTER JOIN
                      dbo.INPROFSAL AS PR ON PR.CODPROSAL = I.CODPROING LEFT OUTER JOIN
                      dbo.INESPECIA AS EM ON EM.CODESPECI = PR.CODESPEC1 AND PR.CODPROSAL = I.CODPROING LEFT OUTER JOIN
                      dbo.INESPECIA AS Et ON Et.CODESPECI = I.CODESPTRA LEFT OUTER JOIN
                      dbo.INDIAGNOP AS D ON D.NUMINGRES = E.NUMINGRES AND D.IPCODPACI = E.IPCODPACI AND D.CODDIAPRI = 'True' LEFT OUTER JOIN
                      dbo.INDIAGNOS AS V ON V.CODDIAGNO = D.CODDIAGNO
WHERE     (C.CODCENATE = '01') AND (C.ESTADCAMA = '2')
ORDER BY SERVICIO, Cama
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Censo diario de camas ocupadas en el centro de atención principal (código ''01''). Muestra en tiempo real qué pacientes están hospitalizados, en qué cama y servicio se encuentran, cuántos días llevan internados y cuál es su diagnóstico principal, entidad aseguradora, especialidad tratante y profesional de ingreso. Integra información de camas (CHCAMASHO), estados de estancia activos (CHREGESTA con estado ''1'' = ocupada), datos del paciente (INPACIENT), ingreso/admisión (ADINGRESO), unidad funcional/servicio (INUNIFUNC), tipo de estancia (CHTIPESTA), entidad pagadora (INENTIDAD), especialidad médica y diagnóstico CIE-10 principal (INDIAGNOP/INDIAGNOS). Se utiliza para reportes operativos de ocupación hospitalaria, seguimiento de pacientes internados, control de camas disponibles y gestión de estancias.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'InHp_CensoDiario';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'InHp_CensoDiario';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista el censo diario de pacientes hospitalizados con cama ocupada en el centro de atención ''01'', exponiendo datos del paciente, ingreso, ubicación, entidad, diagnóstico principal y especialidades.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'InHp_CensoDiario';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir el centro de atención con código ''01''.; Las camas deben tener estado ''2'' (ocupada) para aparecer.; El registro de estancia debe estar marcado como activo (REGESTADO = ''1'').; El paciente debe tener un ingreso vigente asociado a la cama y al estado activo.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'InHp_CensoDiario';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen estancias activas (REGESTADO=''1'') y camas en estado ocupado (''2'').; El censo se limita exclusivamente al centro de atención ''01''.; Solo se vincula un diagnóstico por ingreso: el marcado como principal (CODDIAPRI=''True'').; La especialidad de ingreso proviene de la primera especialidad (CODESPEC1) del profesional que admitió al paciente.; Los días transcurridos se calculan desde la fecha de inicio de la estancia mediante dbo.DiferenciaDias.; Cada fila representa una cama ocupada y aporta Cantidad = 1 para conteos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'InHp_CensoDiario';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Censo diario hospitalario; Cama hospitalaria; Clase de habitación (UCI, suite, observación, recuperación); Clase de cama (observación urgencias, recuperación post-quirúrgica, hospitalaria); Ingreso/admisión del paciente; Estancia hospitalaria; Paciente; Entidad pagadora/aseguradora; Servicio/unidad funcional; Diagnóstico principal; Especialidad médica; Profesional de la salud que ingresa; Número de autorización; Días transcurridos de estancia', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'InHp_CensoDiario';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.InHp_CensoDiario: Devuelve filas solo cuando C.CODCENATE = ''01'' y C.ESTADCAMA = ''2'', restringiendo a camas ocupadas en el centro de atención principal.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'InHp_CensoDiario';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si c.CODCLAHAB ∈ {''1''..''11''} → Traduce el código de clase de habitación a su descripción (''SALA OBSERVACION'', ''UCI'', ''SUITE'', etc.).; si c.CODCLACAM ∈ {''1'',''2'',''3''} → Mapea la clase de cama a ''ObservacionUrgencias'', ''Recuperacion Post Qx'' u ''HOSPITALARIA''.; si P.IPSEXOPAC = ''1'' o ''2'' → Traduce el sexo del paciente a ''Masculino'' o ''Femenino''.; si D.CODDIAPRI = ''True'' → Solo se toma el diagnóstico marcado como principal del ingreso. else No se reporta diagnóstico', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'InHp_CensoDiario';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.Edad; dbo.DiferenciaDias', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'InHp_CensoDiario';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.CHCAMASHO; dbo.ADCENATEN; dbo.INUNIFUNC; dbo.CHREGESTA; dbo.CHTIPESTA; dbo.ADINGRESO; dbo.INPACIENT; dbo.INENTIDAD; dbo.INPROFSAL; dbo.INESPECIA; dbo.INDIAGNOP; dbo.INDIAGNOS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'InHp_CensoDiario';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'InHp_CensoDiario';
GO
