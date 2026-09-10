CREATE VIEW [dbo].[Resolucion_256_Triage2]
AS
SELECT        CASE WHEN U.IPTIPODOC = '1' THEN 'CC' WHEN U.IPTIPODOC = '2' THEN 'CE' WHEN U.IPTIPODOC = '3' THEN 'TI' WHEN U.IPTIPODOC = '4' THEN 'RC' WHEN U.IPTIPODOC = '5' THEN 'PA' WHEN U.IPTIPODOC = '6' THEN ' AS'
                          WHEN U.IPTIPODOC = '7' THEN 'MS' END AS [Tipo de identificación del usuario ], C.IPCODPACI AS Número_de_identificación_del_usuario, CONVERT(VARCHAR(10), U.IPFECNACI, 103) AS Fecha_Nacimiento_Pac, 
                         CASE WHEN U.IPSEXOPAC = '1' THEN 'Masculino' WHEN U.IPSEXOPAC = '2' THEN 'Femenino' END AS Sexo_del_Usuario, U.IPPRIAPEL AS Primer_apellido, U.IPSEGAPEL AS Segundo_apellido_del_Usuario, 
                         U.IPPRINOMB AS Primer_Nombre_del_Usuario, U.IPSEGNOMB AS Segundo_Nombre, F.HealthEntityCode AS Código_EAPB, CONVERT(VARCHAR(10), A.TRIAFECHA, 103) AS Fecha_TRIAGE, RIGHT(A.TRIAFECHA, 7) 
                         AS Hora_TRIAGE, CONVERT(VARCHAR(10), k.FECHINIHI, 103) AS Fecha_CONSULTA_URGENCIAS, RIGHT(k.FECHINIHI, 7) AS Hora_CONSULTA_URGENCIAS, A.TRIAGECLA AS ClasificacionTriage
FROM            dbo.ADCONTURG AS C WITH (nolock) LEFT OUTER JOIN
                         dbo.INPACIENT AS U WITH (nolock) ON U.IPCODPACI = C.IPCODPACI LEFT OUTER JOIN
                         Contract.CareGroup AS E WITH (nolock) ON E.Id = U.GENCAREGROUP LEFT OUTER JOIN
                         dbo.ADTRIAGEU AS A WITH (nolock) ON A.TRIANUMER = C.CODCONCEC LEFT OUTER JOIN
                         dbo.INPROFSAL AS P WITH (nolock) ON P.CODPROSAL = A.CODPROSAL LEFT OUTER JOIN
                         dbo.INPROFSAL AS Pa WITH (nolock) ON Pa.CODPROSAL = C.PROAUSENT LEFT OUTER JOIN
                         dbo.INESPECIA AS ES WITH (nolock) ON ES.CODESPECI = P.CODESPEC1 LEFT OUTER JOIN
                         dbo.ADCATTRIU AS ds WITH (nolock) ON ds.TRIACATEG = A.TRIACATEG LEFT OUTER JOIN
                         dbo.HCURGING1 AS k WITH (nolock) ON k.IPCODPACI = A.IPCODPACI AND k.NUMINGRES = A.NUMINGRES AND k.UFUCODIGO = C.UFUCODIGO LEFT OUTER JOIN
                         dbo.INPROFSAL AS PAIU WITH (NOLOCK) ON PAIU.CODPROSAL = k.CODPROSAL LEFT OUTER JOIN
                         dbo.ADINGRESO AS I WITH (NOLOCK) ON I.NUMINGRES = A.NUMINGRES LEFT OUTER JOIN
                         dbo.INDIAGNOS AS DX WITH (NOLOCK) ON DX.CODDIAGNO = I.CODDIAING LEFT OUTER JOIN
                         dbo.INDIAGNOS AS DXA WITH (NOLOCK) ON DXA.CODDIAGNO = I.CODDIAEGR LEFT OUTER JOIN
                         Contract.HealthAdministrator AS F WITH (nolock) ON F.Id = I.GENCONENTITY
WHERE        (C.IPFECLLEGA >= '01/09/2020 00:00:00')
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporte para cumplimiento de la Resolución 256 del sistema de urgencias, que aplana en una sola fila los datos demográficos del paciente, la clasificación de triage (fecha, hora y nivel), la fecha y hora de inicio de la consulta de urgencias, y el código de la entidad administradora de salud (EAPB). Consolida información desde el registro de llegada a urgencias, la valoración de triage, la historia clínica inicial y el ingreso, filtrando atenciones desde septiembre de 2020.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Resolucion_256_Triage2';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Resolucion_256_Triage2';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone los registros de triage de urgencias junto con datos del paciente, EAPB y consulta de urgencias para reporte regulatorio (Resolución 256).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Resolucion_256_Triage2';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existir registros en ADCONTURG con IPFECLLEGA >= 01/09/2020; Disponer de catálogos relacionados (pacientes, profesionales, diagnósticos, EAPB)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Resolucion_256_Triage2';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan contactos de urgencias a partir del 1 de septiembre de 2020; Las fechas se entregan en formato dd/MM/yyyy (estilo 103); La hora se obtiene tomando los últimos 7 caracteres del campo de fecha/hora; Los joins son LEFT OUTER, garantizando que un triage sin paciente/EAPB/diagnóstico aún se incluye', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Resolucion_256_Triage2';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Triage de urgencias; Clasificación de triage; Tipo de identificación del paciente; EAPB (administradora de planes de beneficios); Consulta de urgencias; Resolución 256; Ingreso/admisión; Diagnóstico de ingreso y egreso', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Resolucion_256_Triage2';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Resolucion_256_Triage2: Devuelve filas de triage solo cuando C.IPFECLLEGA >= ''01/09/2020 00:00:00''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Resolucion_256_Triage2';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si U.IPTIPODOC en 1..7 → Mapea a etiqueta de tipo de documento: 1=CC, 2=CE, 3=TI, 4=RC, 5=PA, 6=AS, 7=MS else NULL; si U.IPSEXOPAC = ''1'' o ''2'' → Traduce a ''Masculino'' o ''Femenino'' respectivamente else NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Resolucion_256_Triage2';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADCONTURG; dbo.INPACIENT; Contract.CareGroup; dbo.ADTRIAGEU; dbo.INPROFSAL; dbo.INESPECIA; dbo.ADCATTRIU; dbo.HCURGING1; dbo.ADINGRESO; dbo.INDIAGNOS; Contract.HealthAdministrator', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Resolucion_256_Triage2';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Resolucion_256_Triage2';
GO
