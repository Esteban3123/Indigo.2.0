CREATE VIEW [dbo].[VServicesProceduresQx]
AS
	SELECT	CONCAT('HCORDPROQ', '-', A.AUTO) Id,
			'HCORDPROQ' EntityName,
			A.[AUTO] as Row,
			A.CODSERIPS,
			case when A.GENSERVICEORDER is null then CAST(0 as BIT) else CAST(1 as BIT) end AS Seleccione,
			CONCAT(RTRIM(D.UFUCODIGO), ' - ', RTRIM(D.UFUDESCRI)) AS UnidadFuncional,
			CONCAT(RTRIM(C.CODIGONIT), ' - ', RTRIM(C.NOMMEDICO)) AS Medico,
			A.OBSSERIPS AS Observacion,
			A.FECORDMED AS Fecha,
			A.NUMEFOLIO AS Folio,
			A.CODPROSAL,
			CODESPEC1,
			CANSERIPS,
			A.NUMINGRES,
			A.IPCODPACI,
			ESTSERIPS,
			D.UFUCODIGO,
			C.CODIGONIT as NitMedico,
			A.GENSERVICEORDER
	FROM dbo.ADINGRESO ing
	JOIN dbo.HCORDPROQ A ON ing.NUMINGRES = a.NUMINGRES
	JOIN dbo.INCUPSIPS B ON A.CODSERIPS=B.CODSERIPS 
	JOIN dbo.INPROFSAL C ON A.CODPROSAL=C.CODPROSAL 
	JOIN dbo.INUNIFUNC D ON A.UFUCODIGO=D.UFUCODIGO
	WHERE (A.MANEXTPRO = 0 OR ISNULL(ing.TRATAESPECIA, 0) = 3) AND A.ESTSERIPS IN ('2','3','4')
UNION ALL
	SELECT	CONCAT('HCORDPROQ', '-', A.AUTO) Id,
			'HCORDPROQ' EntityName,
			A.[AUTO] as Row,
			A.CODSERIPS,
			case when A.GENSERVICEORDER is null then CAST(0 as BIT) else CAST(1 as BIT) end AS Seleccione,
			CONCAT(RTRIM(D.UFUCODIGO), ' - ', RTRIM(D.UFUDESCRI)) AS UnidadFuncional,
			CONCAT(RTRIM(C.CODIGONIT), ' - ', RTRIM(C.NOMMEDICO)) AS Medico,
			A.OBSSERIPS AS Observacion,
			A.FECORDMED AS Fecha,
			A.NUMEFOLIO AS Folio,
			A.CODPROSAL,
			CODESPEC1,
			CANSERIPS,
			INGMH.NUMINGRES,
			a.IPCODPACI,
			ESTSERIPS,
			D.UFUCODIGO,
			C.CODIGONIT as NitMedico,
			A.GENSERVICEORDER
	FROM dbo.HCORDPROQ A 
	JOIN dbo.INCUPSIPS B ON A.CODSERIPS=B.CODSERIPS 
	JOIN dbo.INPROFSAL C ON A.CODPROSAL=C.CODPROSAL 
	JOIN dbo.INUNIFUNC D ON A.UFUCODIGO=D.UFUCODIGO
	JOIN dbo.HCINGRESORECNAC INGMH  on a.NUMINGRES = INGMH .NUMINGRESHIJO
	JOIN dbo.HCRECINAC RN  on INGMH.NUMINGRESHIJO  = rn.NUMINGRESHIJO  
	JOIN  dbo.ADINGRESO AS ING  on ING.NUMINGRES = INGMH.NUMINGRESHIJO  
	WHERE (A.MANEXTPRO = 0 OR ISNULL(ing.TRATAESPECIA, 0) = 3) AND A.ESTSERIPS IN ('2','3','4')
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida las órdenes de procedimientos quirúrgicos y servicios de salud activos (autorizados, en proceso o ejecutados) generadas en la historia clínica, tanto para pacientes adultos ingresados como para recién nacidos vinculados a un ingreso madre-hijo. Integra información del ingreso hospitalario o de urgencias (ADINGRESO), las órdenes de procedimientos (HCORDPROQ), el catálogo de servicios CUPS/IPS (INCUPSIPS), el maestro de profesionales de la salud (INPROFSAL) y las unidades funcionales (INUNIFUNC) para presentar en una sola consulta: el código y folio de la orden, el servicio o procedimiento ordenado, la unidad funcional donde se originó, el médico ordenador con su NIT, la fecha de la orden médica, observaciones, cantidad, estado del servicio, el número de ingreso y la cédula del paciente. Se utiliza para la gestión y seguimiento de procedimientos quirúrgicos pendientes o en curso, reportería operativa de quirófano y verificación de órdenes médicas activas por paciente o ingreso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'VServicesProceduresQx';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'VServicesProceduresQx';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone las órdenes de procedimientos quirúrgicos pendientes/activas de pacientes (incluido recién nacidos asociados al ingreso de la madre) para su gestión y generación de orden de servicio.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VServicesProceduresQx';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las órdenes deben tener relación válida con servicio CUPS, profesional de salud y unidad funcional; Para la rama de recién nacido debe existir vínculo madre-hijo en HCINGRESORECNAC y registro clínico en HCRECINAC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VServicesProceduresQx';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo expone órdenes con estado IPS en {''2'',''3'',''4''}; Excluye procedimientos marcados como manejo extra-procedimiento salvo cuando el ingreso tiene tratamiento especial = 3; El identificador lógico se construye como ''HCORDPROQ-'' + AUTO; La rama de recién nacido siempre asocia el ingreso del hijo (NUMINGRESHIJO) como NUMINGRES expuesto', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VServicesProceduresQx';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Orden de procedimiento quirúrgico; Ingreso/admisión; Profesional de salud (médico); Unidad funcional; Servicio CUPS; Recién nacido; Tratamiento especial; Generación de orden de servicio', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VServicesProceduresQx';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCORDPROQ: Devuelve solo órdenes con ESTSERIPS IN (''2'',''3'',''4'') y (MANEXTPRO=0 OR TRATAESPECIA del ingreso = 3); [RETURN_RESULT] dbo.HCORDPROQ: Marca Seleccione=1 cuando GENSERVICEORDER no es NULL; en caso contrario Seleccione=0', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VServicesProceduresQx';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si A.GENSERVICEORDER IS NULL → Seleccione = 0 (orden no generada) else Seleccione = 1 (orden de servicio ya generada); si A.MANEXTPRO = 0 OR ISNULL(ing.TRATAESPECIA,0) = 3 → La orden es incluida en el resultado else Se excluye del resultado; si Existe vínculo en HCINGRESORECNAC entre ingreso de la orden y NUMINGRESHIJO → Se incluye la orden bajo el contexto del recién nacido (segunda rama UNION ALL) else Se evalúa por la rama estándar de ingreso del paciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VServicesProceduresQx';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADINGRESO; dbo.HCORDPROQ; dbo.INCUPSIPS; dbo.INPROFSAL; dbo.INUNIFUNC; dbo.HCINGRESORECNAC; dbo.HCRECINAC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VServicesProceduresQx';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VServicesProceduresQx';
GO
