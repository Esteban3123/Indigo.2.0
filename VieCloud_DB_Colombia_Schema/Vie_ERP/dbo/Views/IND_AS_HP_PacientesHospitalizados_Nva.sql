
CREATE VIEW [dbo].[IND_AS_HP_PacientesHospitalizados_Nva]
AS
SELECT        P.IPFECNACI AS FechaNacimiento, I.NUMINGRES AS NumeroIngreso, E.IPCODPACI AS Identificación, P.IPNOMCOMP AS NombrePaciente, 
                         RTRIM(LTRIM(dbo.Edad(CONVERT(varchar, P.IPFECNACI, 105), CONVERT(varchar, GETDATE(), 105)))) AS Edad, DAY(P.IPFECNACI) AS DíaCumpleaños, 
                         DATENAME(mm, P.IPFECNACI) AS MesCumpleaños, P.IPDIRECCI AS Direccion, P.IPTELEFON AS Telefono, P.IPTELMOVI AS Movil, C.DESCCAMAS AS Cama, 
                         CASE c.CODCLAHAB WHEN '1' THEN 'SALA OBSERVACION' WHEN '2' THEN 'SALA PROCEDIMIENTO' WHEN '3' THEN 'SALA RECUPERACION' WHEN '4' THEN
                          'HABITACION 1 CAMA' WHEN '5' THEN 'HABITACION 2 CAMAS' WHEN '6' THEN 'HABITACION 3 CAMAS' WHEN '7' THEN 'HABITACION 4 CAMAS' WHEN '8' THEN
                          'SUITE' WHEN '9' THEN 'HAB.ESPECIAL' WHEN '10' THEN 'UCI' WHEN '11' THEN 'OTRO' END AS ClaseHabitacion, TE.DESTIPEST AS [Tipo de Estancia], 
                         CASE C.CODAISLAM WHEN '1' THEN 'Aerosol' WHEN '2' THEN 'Contacto' WHEN '3' THEN 'Estandar' WHEN '4' THEN 'Gota' WHEN '1' THEN 'Protector' END AS
                          TipoAislamiento, 
                         CASE c.CODCLACAM WHEN '1' THEN 'ObservacionUrgencias' WHEN '2' THEN 'Recuperacion Post Qx' WHEN '3' THEN 'HOSPITALARIA' END AS ClaseCama, 
                         CONVERT(varchar, E.FECINIEST, 100) AS FechaCambioUnidad, CONVERT(varchar, I.IFECHAING, 100) AS FechaIngreso, I.CODCONTRA AS CodContrato, 
                         A.NOMENTIDA AS Entidad, I.CODPANATE AS PlanBen, UF.UFUCODIGO AS Cod_Servicio, UF.UFUDESCRI AS Servicio, D.CODDIAGNO AS CodDiag, 
                         V.NOMDIAGNO AS DiagnosticoPpal, dbo.Especialidades(dbo.MedicosEsp(dbo.MedicosHosp(E.IPCODPACI, E.NUMINGRES))) AS Especialidad, 
                         CASE WHEN P.IPSEXOPAC = '1' THEN 'Masculino' WHEN P.IPSEXOPAC = '2' THEN 'Femenino' END AS Sexo, I.IAUTORIZA AS NumeroAutorizacion, 
                         I.IOBSERVAC AS Observaciones, W.DISCDESCRI AS Discapacidad
FROM            dbo.CHCAMASHO AS C INNER JOIN
                         dbo.ADCENATEN AS CA ON CA.CODCENATE = C.CODCENATE INNER JOIN
                         dbo.INUNIFUNC AS UF ON UF.UFUCODIGO = C.UFUCODIGO INNER JOIN
                         dbo.CHREGESTA AS E ON E.CODICAMAS = C.CODICAMAS AND E.REGESTADO = '1' INNER JOIN
                         dbo.CHTIPESTA AS TE ON TE.CODTIPEST = E.CODTIPEST INNER JOIN
                         dbo.ADINGRESO AS I ON I.NUMINGRES = E.NUMINGRES INNER JOIN
                         dbo.INPACIENT AS P ON P.IPCODPACI = E.IPCODPACI INNER JOIN
                         dbo.COPLANBEF AS G ON G.CODPLANBE = I.CODPANATE INNER JOIN
                         dbo.INENTIDAD AS A ON A.CODENTIDA = I.CODENTIDA LEFT OUTER JOIN
                         dbo.INDIAGNOP AS D ON D.NUMINGRES = E.NUMINGRES AND D.IPCODPACI = E.IPCODPACI AND D.CODDIAPRI = 'True' LEFT OUTER JOIN
                         dbo.ADDISCAPACI AS W ON W.DISCCODIGO = P.DISCCODIGO LEFT OUTER JOIN
                         dbo.INDIAGNOS AS V ON V.CODDIAGNO = D.CODDIAGNO
WHERE        (C.CODCENATE = '001') AND (C.ESTADCAMA = '2')
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de consulta orientada a reporting y seguimiento en tiempo real de pacientes actualmente hospitalizados en el centro de atención ''001'', filtrando solo camas en estado ocupado (''2''). Consolida datos demográficos del paciente, información de la cama (clase, tipo de aislamiento, servicio), datos del ingreso (entidad, plan de beneficios, autorización, fechas) y diagnóstico principal CIE-10. Incluye además edad calculada, cumpleaños, discapacidad y especialidad médica tratante, siendo útil para el censo hospitalario y gestión de camas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AS_HP_PacientesHospitalizados_Nva';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AS_HP_PacientesHospitalizados_Nva';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los pacientes actualmente hospitalizados en el centro de atención ''001'' con datos demográficos, ubicación de cama, ingreso, contrato/entidad, diagnóstico principal y especialidad tratante.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AS_HP_PacientesHospitalizados_Nva';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El centro de atención ''001'' debe existir en ADCENATEN; La cama debe estar en estado ''2'' (ocupada) en CHCAMASHO; Debe existir un registro de estancia activo (REGESTADO=''1'') en CHREGESTA para el ingreso; Las funciones dbo.Edad, dbo.Especialidades, dbo.MedicosEsp, dbo.MedicosHosp deben existir y estar accesibles', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AS_HP_PacientesHospitalizados_Nva';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo expone camas del centro de atención ''001''; Solo expone camas marcadas como ocupadas (ESTADCAMA=''2''); Solo considera la estancia vigente del paciente (REGESTADO=''1''); Toma como diagnóstico mostrado únicamente el marcado como principal (CODDIAPRI=''True''); El código de aislamiento ''1'' está duplicado en el CASE, por lo que ''Protector'' nunca se devuelve (siempre gana ''Aerosol''); Edad, fecha de cambio de unidad y fecha de ingreso se entregan formateadas como cadenas', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AS_HP_PacientesHospitalizados_Nva';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente hospitalizado; Cama hospitalaria; Centro de atención; Unidad funcional / servicio; Estancia / tipo de estancia; Ingreso / admisión; Clase de habitación; Tipo de aislamiento; Clase de cama; Contrato y plan de beneficios; Entidad pagadora; Diagnóstico principal (CIE); Especialidad médica tratante; Discapacidad; Autorización; Datos demográficos del paciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AS_HP_PacientesHospitalizados_Nva';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve solo camas con CODCENATE=''001'' y ESTADCAMA=''2'', cruzadas con la estancia vigente (REGESTADO=''1'') y el diagnóstico marcado como principal (CODDIAPRI=''True'').', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AS_HP_PacientesHospitalizados_Nva';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si CHCAMASHO.CODCLAHAB ∈ {''1''..''11''} → Traduce el código a etiqueta legible de clase de habitación (Sala Observación, UCI, Suite, etc.) else ClaseHabitacion = NULL; si CHCAMASHO.CODAISLAM ∈ {''1'',''2'',''3'',''4''} → Traduce a tipo de aislamiento (Aerosol, Contacto, Estandar, Gota, Protector) else TipoAislamiento = NULL; si CHCAMASHO.CODCLACAM ∈ {''1'',''2'',''3''} → Traduce a clase de cama (ObservacionUrgencias, Recuperacion Post Qx, HOSPITALARIA) else ClaseCama = NULL; si INPACIENT.IPSEXOPAC = ''1'' o ''2'' → Mapea a ''Masculino'' o ''Femenino'' else Sexo = NULL; si INDIAGNOP.CODDIAPRI = ''True'' → Se toma como diagnóstico principal del ingreso else El ingreso queda sin diagnóstico (LEFT JOIN devuelve NULL)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AS_HP_PacientesHospitalizados_Nva';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.Edad; dbo.Especialidades; dbo.MedicosEsp; dbo.MedicosHosp', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AS_HP_PacientesHospitalizados_Nva';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.CHCAMASHO; dbo.ADCENATEN; dbo.INUNIFUNC; dbo.CHREGESTA; dbo.CHTIPESTA; dbo.ADINGRESO; dbo.INPACIENT; dbo.COPLANBEF; dbo.INENTIDAD; dbo.INDIAGNOP; dbo.ADDISCAPACI; dbo.INDIAGNOS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AS_HP_PacientesHospitalizados_Nva';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AS_HP_PacientesHospitalizados_Nva';
GO
