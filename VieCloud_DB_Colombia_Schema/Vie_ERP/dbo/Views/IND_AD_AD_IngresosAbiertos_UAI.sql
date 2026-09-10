
CREATE VIEW [dbo].[IND_AD_AD_IngresosAbiertos_UAI]
AS
SELECT        K.IFECHAING AS FechaLlegada, 
                         CASE K.CODTIPPAC WHEN '1' THEN 'Maternas' WHEN '2' THEN 'Menores de 5 Años' WHEN '3' THEN 'Adultos Mayores' WHEN '4' THEN 'Discapacitados' WHEN
                          '5' THEN 'Poblacion General' END AS Tipo, RTRIM(K.IPCODPACI) AS Identificacion, RTRIM(F.IPNOMCOMP) AS Paciente, CAST(0 AS BIT) AS Confirmado, 
                         CAST(0 AS BIT) AS Ausente, K.NUMINGRES AS Ingreso, F.IPFECNACI AS [Fecha Nacimiento], RTRIM(LTRIM(dbo.Edad(CONVERT(varchar, F.IPFECNACI, 105), 
                         CONVERT(varchar, GETDATE(), 105)))) AS Edad, L.RIESGOAGRE AS RiesgoAgresión, K.VIVESOLO,
                             (SELECT        COUNT(*) AS Expr1
                               FROM            dbo.ADACOMPAN
                               WHERE        (NUMINGRES = K.NUMINGRES)) AS ACOMPANANTES, CONVERT(BIT, 0) AS Riesgo, RTRIM(F.CODENTIDA) + '-' + RTRIM(M.NOMENTIDA) 
                         AS EntidadPaciente
FROM            dbo.ADINGRESO AS K WITH (nolock) INNER JOIN
                         dbo.INPACIENT AS F WITH (nolock) ON K.IPCODPACI = F.IPCODPACI LEFT OUTER JOIN
                         dbo.ADACTIVID AS L WITH (nolock) ON F.CODACTIVI = L.codactivi LEFT OUTER JOIN
                         dbo.INENTIDAD AS M WITH (nolock) ON M.CODENTIDA = F.CODENTIDA
WHERE        (K.IFECHAING > '01/06/2017') AND (K.IESTADOIN = ' ') AND (K.NUMINGRES NOT IN
                             (SELECT DISTINCT A.NUMINGRES
                               FROM            dbo.ADINGRESO AS A WITH (nolock) INNER JOIN
                                                         dbo.AMBORDIMA AS B WITH (nolock) ON B.IPCODPACI = A.IPCODPACI AND CONVERT(varchar(10), CONVERT(date, A.IFECHAING, 106), 103) 
                                                         = CONVERT(varchar(10), CONVERT(date, B.FECORDMED, 106), 103))) AND (K.REQTRIAGE = 0 OR
                         K.REQTRIAGE IS NULL) AND (K.CODCENATE = '001') AND (K.UFUCODIGO = 'N30') AND (K.NUMINGRES NOT IN
                             (SELECT        NUMDOCUME
                               FROM            dbo.INDOCUMEN WITH (nolock)
                               WHERE        (CODDOCUME = '3'))) AND
                             ((SELECT        COUNT(*) AS Expr1
                                 FROM            dbo.HCHISPACA WITH (nolock)
                                 WHERE        (NUMINGRES = K.NUMINGRES)) = 0)
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que lista los ingresos actualmente abiertos (sin egreso) en la Unidad de Atención Inmediata (UAI) del centro de atención 001, unidad funcional N30, que aún no tienen historia clínica iniciada ni orden médica registrada en el día de ingreso. Combina datos del episodio de ingreso (ADINGRESO), información demográfica y de entidad aseguradora del paciente (INPACIENT, INENTIDAD), clasificación de riesgo de agresión por actividad (ADACTIVID) y conteo de acompañantes registrados (ADACOMPAN). Sirve como panel de control o lista de espera para que el personal asistencial identifique pacientes pendientes de atención: muestra la fecha de llegada, tipo de paciente (maternas, menores de 5 años, adultos mayores, discapacitados, población general), cédula o identificación, nombre completo, edad calculada, riesgo de agresión, si vive solo, número de acompañantes y la entidad o EPS a la que pertenece el paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'IND_AD_AD_IngresosAbiertos_UAI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'IND_AD_AD_IngresosAbiertos_UAI';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los ingresos abiertos en la unidad N30 del centro 001 que aún no tienen triage, orden de imágenes, documento tipo 3 ni historia clínica registrada, para seguimiento en la UAI.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AD_AD_IngresosAbiertos_UAI';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El ingreso debe pertenecer al centro de atención ''001'' y a la unidad funcional ''N30''.; La fecha de ingreso debe ser posterior al 01/06/2017.; El estado del ingreso (IESTADOIN) debe estar vacío ('' ''), es decir ingreso activo/abierto.; El ingreso no debe requerir triage (REQTRIAGE = 0 o NULL).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AD_AD_IngresosAbiertos_UAI';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Los campos Confirmado, Ausente y Riesgo siempre se devuelven como BIT 0 (valores fijos no calculados).; Solo se incluyen ingresos del centro ''001'' y unidad ''N30''.; Se excluyen ingresos con historia clínica (HCHISPACA) ya iniciada.; Se excluyen ingresos con documento adjunto de tipo ''3'' en INDOCUMEN.; Se excluyen ingresos cuyo paciente tenga orden de imagen ambulatoria en la misma fecha del ingreso.; El conteo de acompañantes proviene de ADACOMPAN para el ingreso.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AD_AD_IngresosAbiertos_UAI';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ingreso/Admisión de paciente; Tipo de paciente (Maternas, Menores de 5 años, Adultos Mayores, Discapacitados, Población General); Triage; Riesgo de agresión; Acompañantes; Historia clínica; Órdenes de imágenes diagnósticas; Entidad/Aseguradora del paciente; Edad del paciente; Vive solo', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AD_AD_IngresosAbiertos_UAI';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve ingresos abiertos que cumplen los filtros y excluye aquellos con orden de imagen ambulatoria en la misma fecha, con documento tipo ''3'' o con historia clínica creada.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AD_AD_IngresosAbiertos_UAI';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si CODTIPPAC = ''1'' → Clasifica al paciente como ''Maternas''; si CODTIPPAC = ''2'' → Clasifica al paciente como ''Menores de 5 Años''; si CODTIPPAC = ''3'' → Clasifica al paciente como ''Adultos Mayores''; si CODTIPPAC = ''4'' → Clasifica al paciente como ''Discapacitados''; si CODTIPPAC = ''5'' → Clasifica al paciente como ''Poblacion General''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AD_AD_IngresosAbiertos_UAI';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADINGRESO; dbo.INPACIENT; dbo.ADACTIVID; dbo.INENTIDAD; dbo.ADACOMPAN; dbo.AMBORDIMA; dbo.INDOCUMEN; dbo.HCHISPACA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AD_AD_IngresosAbiertos_UAI';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AD_AD_IngresosAbiertos_UAI';
GO
