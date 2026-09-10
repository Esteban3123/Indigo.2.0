CREATE VIEW [dbo].[ViewNursingProcedures]
AS
	SELECT	C.CODSERIPS,
			CAST(0 AS tinyint) AS Seleccione, 
			A.IPCODPACI, 
			A.NUMINGRES,
			A.CODCENATE,
			CASE B.ACTFACTUR WHEN '1' THEN 'Facturable' WHEN '0' THEN 'No Facturable' END AS Tipo,
			C.DESSERIPS,
			COUNT(*) as TOTAL,
			B.CODACTENF AS CodigoActividad,
			RTRIM(B.DESACTENF) AS Actividad,
			i.IPNOMCOMP  as PersonName
	FROM dbo.HCHOGASIN A 
	JOIN dbo.HCACTENFE B ON A.CODACTENF=B.CODACTENF 
	JOIN dbo.INPACIENT i on a.IPCODPACI = i.IPCODPACI 
	LEFT JOIN INCUPSIPS C ON B.CODSERIPS=C.CODSERIPS 
	where C.CODSERIPS is not null
	GROUP BY RTRIM(DESACTENF),ACTFACTUR,C.CODSERIPS,TIPSERIPS,C.DESSERIPS,a.IPCODPACI,NUMINGRES,CODCENATE,i.IPNOMCOMP,B.CODACTENF
UNION ALL
	SELECT	C.CODSERIPS,
			CAST(0 AS tinyint) AS Seleccione,
			A.IPCODPACI,
			INGMH.NUMINGRES,
			A.CODCENATE,
			CASE B.ACTFACTUR WHEN '1' THEN 'Facturable' WHEN '0' THEN 'No Facturable' END AS Tipo,
			C.DESSERIPS,
			COUNT(*) as TOTAL,
			B.CODACTENF AS CodigoActividad,
			RTRIM(B.DESACTENF) AS Actividad,
			'Hijo '+ cast(rn.NUMHIJREG as varchar(20)) as PersonName
	FROM dbo.HCHOGASIN A 
	JOIN dbo.HCACTENFE B ON A.CODACTENF=B.CODACTENF 
	JOIN dbo.HCINGRESORECNAC INGMH  on a.NUMINGRES = INGMH .NUMINGRESHIJO
	JOIN dbo.HCRECINAC RN  on INGMH.NUMINGRESHIJO  = rn.NUMINGRESHIJO  
	JOIN  dbo.ADINGRESO AS ING  on ING.NUMINGRES = INGMH.NUMINGRESHIJO 
	LEFT JOIN INCUPSIPS C ON B.CODSERIPS=C.CODSERIPS 
	where C.CODSERIPS is not null
	GROUP BY RTRIM(DESACTENF),ACTFACTUR,C.CODSERIPS,TIPSERIPS,C.DESSERIPS,a.IPCODPACI,INGMH.NUMINGRES,a.CODCENATE,rn.NUMHIJREG,B.CODACTENF
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consolida los procedimientos y actividades de enfermería registrados durante la atención hospitalaria, indicando para cada actividad si es facturable o no facturable, el código y descripción del servicio CUPS/IPS asociado, la cantidad de veces que se realizó y el paciente al que se le aplicó. Integra dos escenarios: pacientes regulares (con su cédula y nombre completo) y recién nacidos (identificados como ''Hijo'' con su número de registro), vinculando los insumos y actividades de enfermería con el catálogo de servicios y la información del paciente. Sirve como base para la facturación de enfermería, la auditoría clínica y la revisión de procedimientos ejecutados por turno o ingreso, tanto para pacientes adultos como para neonatos asociados al ingreso de la madre.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewNursingProcedures';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewNursingProcedures';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en una sola lista las actividades/procedimientos de enfermería ejecutados, totalizando ocurrencias por servicio CUPS y diferenciando si fueron prestadas al paciente titular o a un recién nacido asociado al ingreso de la madre.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewNursingProcedures';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las actividades de enfermería deben estar mapeadas a un servicio CUPS (CODSERIPS no nulo en HCACTENFE/INCUPSIPS).; Para el bloque de recién nacidos, el ingreso del paciente debe corresponder a un NUMINGRESHIJO existente en HCINGRESORECNAC y HCRECINAC.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewNursingProcedures';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen actividades de enfermería cuyo código tenga un servicio CUPS (CODSERIPS) asociado.; Cada fila representa un agregado (conteo) por combinación de paciente/ingreso/centro/actividad/servicio CUPS.; Las actividades del paciente titular y las atribuidas a recién nacidos se presentan unificadas mediante UNION ALL, pudiendo repetirse si aplica a ambos contextos.; El campo Seleccione siempre se inicializa en 0 (bandera de selección por defecto).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewNursingProcedures';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Actividades de enfermería; Procedimientos CUPS; Facturable / No facturable; Paciente; Ingreso hospitalario; Centro de atención; Recién nacido (hijo de paciente); Ingreso de la madre vs ingreso del hijo', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewNursingProcedures';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Devuelve un conteo (COUNT(*)) de ejecuciones de cada actividad de enfermería agrupado por paciente, ingreso, centro de atención, código de actividad y servicio CUPS.; [RETURN_RESULT] : Clasifica cada actividad como ''Facturable'' cuando ACTFACTUR=''1'' y ''No Facturable'' cuando ACTFACTUR=''0''.; [RETURN_RESULT] : Para actividades del paciente titular muestra como PersonName el nombre completo del paciente (IPNOMCOMP); para actividades asociadas a un recién nacido muestra ''Hijo ''+NUMHIJREG.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewNursingProcedures';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ACTFACTUR = ''1'' → Marca la actividad como ''Facturable'' else Si ACTFACTUR = ''0'' la marca como ''No Facturable''; si El NUMINGRES de HCHOGASIN coincide con un NUMINGRESHIJO en HCINGRESORECNAC → La actividad se atribuye al recién nacido (PersonName=''Hijo N'') y se reporta el NUMINGRES de la madre desde HCINGRESORECNAC else La actividad se atribuye al paciente titular usando su nombre desde INPACIENT; si C.CODSERIPS IS NOT NULL → Incluye la actividad en el resultado else La excluye (filtra actividades sin servicio CUPS asociado)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewNursingProcedures';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCHOGASIN; dbo.HCACTENFE; dbo.INPACIENT; dbo.INCUPSIPS; dbo.HCINGRESORECNAC; dbo.HCRECINAC; dbo.ADINGRESO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewNursingProcedures';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewNursingProcedures';
GO
