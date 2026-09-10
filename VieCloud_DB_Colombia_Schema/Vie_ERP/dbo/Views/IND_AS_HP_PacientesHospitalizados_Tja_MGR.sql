
CREATE VIEW [dbo].[IND_AS_HP_PacientesHospitalizados_Tja_MGR]
AS
SELECT        A.NOMENTIDA AS Entidad, c.UFUCODIGO AS UnidadFun, uf.UFUDESCRI AS Servicio, c.DESCCAMAS AS Cama, 
                         CASE c.CODCLAHAB WHEN '1' THEN 'SALA OBSERVACION' WHEN '2' THEN 'SALA PROCEDIMIENTO' WHEN '3' THEN 'SALA RECUPERACION' WHEN '4' THEN 'HABITACION 1 CAMA'
                          WHEN '5' THEN 'HABITACION 2 CAMAS' WHEN '6' THEN 'HABITACION 3 CAMAS' WHEN '7' THEN 'HABITACION 4 CAMAS' WHEN '8' THEN 'SUITE' WHEN '9' THEN 'HAB.ESPECIAL'
                          WHEN '10' THEN 'UCI' WHEN '11' THEN 'OTRO' END AS ClaseHabitacion, 
                         CASE c.CODCLACAM WHEN '1' THEN 'ObservacionUrgencias' WHEN '2' THEN 'Recuperacion Post Qx' WHEN '3' THEN 'HOSPITALARIA' END AS ClaseCama, 
                         CASE c.ESTADCAMA WHEN '1' THEN 'LIBRE' WHEN '2' THEN 'OCUPADA' END AS EstadoCama, E.NUMINGRES AS NumeroIngreso, E.IPCODPACI AS Identificación, 
                         P.IPNOMCOMP AS NombrePaciente, c.CODAISLAM AS CodAislamiento, E.FECINIEST AS FechaIngreso, 1 AS Cantidad, DATEDIFF(dd, E.FECINIEST, GETDATE()) 
                         AS DiasEstancia, D.CODDIAGNO AS CodDiagnostico, V.NOMDIAGNO AS NombreDiagnostico, D.CODDIAPRI AS [Es Principal], c.ESTADCAMA, 
                         dbo.ADINGRESO.CODENTIDA, dbo.ADINGRESO.IFECHAING AS Fecha_IngresoClinica, - DATEDIFF(yy, GETDATE(), P.IPFECNACI) AS Expr1, 
                         dbo.ADINGRESO.UFUINGMED, dbo.ADINGRESO.CODUSUCRE, dbo.ADINGRESO.CODCONTRA, P.IPTIPOAFI AS afiliacion, P.NIVCODIGO AS estrato, 
                         P.TIPCOBSAL AS cobertura, P.AUUBICACI, dbo.INMUNICIP.MUNNOMBRE AS [municipio ubicacion], RTRIM(LTRIM(dbo.Edad(CONVERT(varchar, P.IPFECNACI, 105), 
                         CONVERT(varchar, GETDATE(), 105)))) AS Edad, dbo.INESPECIA.DESESPECI AS tratante, TE.DESTIPEST AS [Tipo de Estancia]
FROM            dbo.INPACIENT AS P INNER JOIN
                         dbo.INUBICACI ON P.AUUBICACI = dbo.INUBICACI.AUUBICACI INNER JOIN
                         dbo.INMUNICIP ON dbo.INUBICACI.DEPMUNCOD = dbo.INMUNICIP.DEPMUNCOD LEFT OUTER JOIN
                         dbo.CHCAMASHO AS c INNER JOIN
                         dbo.ADCENATEN AS ca ON ca.CODCENATE = c.CODCENATE INNER JOIN
                         dbo.INUNIFUNC AS uf ON uf.UFUCODIGO = c.UFUCODIGO INNER JOIN
                         dbo.CHREGESTA AS E ON E.CODICAMAS = c.CODICAMAS AND E.REGESTADO = '1' INNER JOIN
                         dbo.CHTIPESTA AS TE ON TE.CODTIPEST = E.CODTIPEST INNER JOIN
                         dbo.ADINGRESO ON E.NUMINGRES = dbo.ADINGRESO.NUMINGRES LEFT OUTER JOIN
                         dbo.INESPECIA ON dbo.ADINGRESO.CODESPTRA = dbo.INESPECIA.CODESPECI ON P.IPCODPACI = E.IPCODPACI LEFT OUTER JOIN
                         dbo.INDIAGNOP AS D ON E.NUMINGRES = D.NUMINGRES AND E.IPCODPACI = D.IPCODPACI LEFT OUTER JOIN
                         dbo.INDIAGNOS AS V ON V.CODDIAGNO = D.CODDIAGNO LEFT OUTER JOIN
                         dbo.INENTIDAD AS A ON dbo.ADINGRESO.CODENTIDA = A.CODENTIDA
WHERE        (c.CODCENATE = '004') AND (c.ESTADCAMA = 2)
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de pacientes actualmente hospitalizados en el centro de atención 004 (camas con estado OCUPADA). Integra información del paciente (nombre, cédula, edad, municipio de residencia, tipo de afiliación, estrato, cobertura), el ingreso hospitalario activo (número de ingreso, fecha de ingreso clínico, contrato, unidad médica, especialidad tratante), la cama asignada (servicio, unidad funcional, clase de habitación, clase de cama, código de aislamiento) y el diagnóstico principal CIE-10 con su nombre. Calcula en tiempo real los días de estancia transcurridos desde el ingreso. Está orientada a reportes de ocupación hospitalaria, control de camas, gestión de hospitalización y monitoreo asistencial en tiempo real para el área de enfermería, admisiones y gerencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'IND_AS_HP_PacientesHospitalizados_Tja_MGR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'IND_AS_HP_PacientesHospitalizados_Tja_MGR';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los pacientes actualmente hospitalizados en un centro de atención específico, mostrando datos del paciente, cama, ingreso, diagnóstico, días de estancia y entidad responsable.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AS_HP_PacientesHospitalizados_Tja_MGR';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existencia de camas registradas para el centro de atención ''004''.; Las estancias activas se identifican con REGESTADO = ''1'' en CHREGESTA.; El paciente debe tener ubicación asociada en INUBICACI y municipio en INMUNICIP (INNER JOIN obligatorio).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AS_HP_PacientesHospitalizados_Tja_MGR';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan camas en estado OCUPADA (ESTADCAMA = 2).; Solo se reportan estancias vigentes (REGESTADO = ''1'').; El alcance está restringido al centro de atención ''004''.; Cada fila representa una cama ocupada con su paciente actual (Cantidad = 1).; Los días de estancia se calculan como diferencia entre la fecha de inicio del estado y la fecha actual.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AS_HP_PacientesHospitalizados_Tja_MGR';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente hospitalizado; Cama hospitalaria; Clase de habitación; Clase de cama; Estado de cama; Ingreso/Admisión; Días de estancia; Diagnóstico principal; Aislamiento; Unidad funcional/Servicio; Especialidad tratante; Entidad responsable de pago; Tipo de afiliación; Estrato; Cobertura en salud; Tipo de estancia', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AS_HP_PacientesHospitalizados_Tja_MGR';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve filas solo cuando c.CODCENATE = ''004'' y c.ESTADCAMA = 2 (cama ocupada), con la estancia vigente (E.REGESTADO = ''1'').', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AS_HP_PacientesHospitalizados_Tja_MGR';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si c.CODCLAHAB entre ''1''..''11'' → Traduce el código a etiqueta textual de clase de habitación (SALA OBSERVACION, UCI, SUITE, etc.) else NULL; si c.CODCLACAM en ''1'',''2'',''3'' → Traduce a clase de cama (ObservacionUrgencias, Recuperacion Post Qx, HOSPITALARIA) else NULL; si c.ESTADCAMA = ''1'' o ''2'' → Traduce estado de cama a LIBRE u OCUPADA else NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AS_HP_PacientesHospitalizados_Tja_MGR';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INPACIENT; dbo.INUBICACI; dbo.INMUNICIP; dbo.CHCAMASHO; dbo.ADCENATEN; dbo.INUNIFUNC; dbo.CHREGESTA; dbo.CHTIPESTA; dbo.ADINGRESO; dbo.INESPECIA; dbo.INDIAGNOP; dbo.INDIAGNOS; dbo.INENTIDAD; dbo.Edad', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AS_HP_PacientesHospitalizados_Tja_MGR';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AS_HP_PacientesHospitalizados_Tja_MGR';
GO
