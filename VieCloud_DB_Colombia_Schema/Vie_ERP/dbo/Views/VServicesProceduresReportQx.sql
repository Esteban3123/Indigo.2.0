
CREATE VIEW [dbo].[VServicesProceduresReportQx]
AS
	SELECT	CONCAT('HCQXINFOR', '-', A.AUTO) Id,
			'HCQXINFOR' EntityName,
			A.AUTO AS Row,
			A.CODSERIPS,
			case when A.GENSERVICEORDER is null then CAST(0 as BIT) else CAST(1 as BIT) end AS Seleccione,
			CONCAT(RTRIM(D.UFUCODIGO), ' - ', RTRIM(D.UFUDESCRI)) AS UnidadFuncional,
			CONCAT(RTRIM(E.CODIGONIT), ' - ', RTRIM(E.NOMMEDICO)) AS Medico,
			RTRIM(DESPROCED) AS Observacion,
			FECHORINI AS Fecha,
			A.NUMEFOLIO AS Folio,
			A.CODPROSAL,
			CODESPEC1,
			CANTIDAQX AS CANSERIPS,
			A.NUMINGRES,
			A.IPCODPACI,
			D.UFUCODIGO,
			E.CODIGONIT as NitMedico,
			A.GENSERVICEORDER
	FROM dbo.HCQXINFOR A 
	JOIN dbo.INCUPSIPS B ON A.CODSERIPS=B.CODSERIPS 
	JOIN dbo.HCQXREALI C ON A.IPCODPACI=C.IPCODPACI AND A.NUMINGRES=C.NUMINGRES AND A.NUMEFOLIO=C.NUMEFOLIO AND A.CODSERIPS=C.CODSERIPS AND C.QXPRINCIP = 1
	JOIN dbo.INUNIFUNC D ON A.UFUCODIGO=D.UFUCODIGO 
	JOIN dbo.INPROFSAL E ON A.CODPROSAL=E.CODPROSAL
UNION ALL
	SELECT	CONCAT('HCQXINFOR', '-', A.AUTO) Id,
			'HCQXINFOR' EntityName,
			A.AUTO AS Row,
			A.CODSERIPS,
			case when A.GENSERVICEORDER is null then CAST(0 as BIT) else CAST(1 as BIT) end AS Seleccione,
			CONCAT(RTRIM(D.UFUCODIGO), ' - ', RTRIM(D.UFUDESCRI)) AS UnidadFuncional,
			CONCAT(RTRIM(E.CODIGONIT), ' - ', RTRIM(E.NOMMEDICO)) AS Medico,
			RTRIM(DESPROCED) AS Observacion,
			FECHORINI AS Fecha,
			A.NUMEFOLIO AS Folio,
			A.CODPROSAL,
			CODESPEC1,
			CANTIDAQX AS CANSERIPS,
			INGMH.NUMINGRES,
			A.IPCODPACI,
			D.UFUCODIGO,
			E.CODIGONIT as NitMedico,
			A.GENSERVICEORDER
	FROM dbo.HCQXINFOR A 
	JOIN dbo.INCUPSIPS B ON A.CODSERIPS=B.CODSERIPS 
	JOIN dbo.HCQXREALI C ON A.IPCODPACI=C.IPCODPACI AND A.NUMINGRES=C.NUMINGRES AND A.NUMEFOLIO=C.NUMEFOLIO AND A.CODSERIPS=C.CODSERIPS AND C.QXPRINCIP = 1
	JOIN dbo.INUNIFUNC D ON A.UFUCODIGO=D.UFUCODIGO 
	JOIN dbo.INPROFSAL E ON A.CODPROSAL=E.CODPROSAL
	JOIN dbo.HCINGRESORECNAC INGMH  on a.NUMINGRES = INGMH .NUMINGRESHIJO
	JOIN dbo.HCRECINAC RN  on INGMH.NUMINGRESHIJO  = rn.NUMINGRESHIJO  
	JOIN  dbo.ADINGRESO AS ING  on ING.NUMINGRES = INGMH.NUMINGRESHIJO
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida los procedimientos quirúrgicos principales realizados en quirófano, combinando el informe quirúrgico (HCQXINFOR), los procedimientos ejecutados (HCQXREALI), el catálogo de servicios CUPS (INCUPSIPS), las unidades funcionales o salas (INUNIFUNC) y el profesional cirujano (INPROFSAL). Expone por cada acto quirúrgico el código de servicio CUPS, la unidad funcional o sala donde se realizó, el médico responsable con su NIT, la fecha y hora de inicio, el folio del informe, la cantidad de procedimientos y el indicador de si ya se generó una orden de servicio. Incluye un segundo bloque que incorpora ingresos de recién nacidos vinculados al ingreso de la madre, asegurando que los procedimientos neonatales también queden reportados. Se usa para reportería quirúrgica, facturación de servicios CUPS, generación de órdenes y auditoría de procedimientos en sala de cirugía.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'VServicesProceduresReportQx';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'VServicesProceduresReportQx';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone los procedimientos quirúrgicos principales registrados en informes de cirugía, incluyendo médico, unidad funcional e indicador de orden generada, considerando además los casos asociados a ingresos de recién nacidos vinculados a la madre.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VServicesProceduresReportQx';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existencia del informe quirúrgico en HCQXINFOR con un procedimiento principal (QXPRINCIP=1) en HCQXREALI para la misma combinación paciente/ingreso/folio/servicio.; El código de servicio debe existir en el catálogo INCUPSIPS.; La unidad funcional referenciada debe existir en INUNIFUNC y el profesional en INPROFSAL.; Para el segundo bloque (UNION ALL), el ingreso del informe quirúrgico debe corresponder al ingreso del recién nacido (NUMINGRESHIJO) y existir el vínculo madre-hijo en HCINGRESORECNAC, el registro clínico en HCRECINAC y la admisión en ADINGRESO.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VServicesProceduresReportQx';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen procedimientos marcados como principales (QXPRINCIP = 1) en el registro de cirugías realizadas.; Cada fila se identifica de forma única con el prefijo ''HCQXINFOR'' concatenado al consecutivo AUTO.; El indicador Seleccione es booleano y depende exclusivamente de si existe o no una orden de servicio generada.; El procedimiento debe estar catalogado en INCUPSIPS, en una unidad funcional válida (INUNIFUNC) y asignado a un profesional vigente (INPROFSAL).; En el segundo bloque, el ingreso reportado corresponde al ingreso del recién nacido (NUMINGRESHIJO) y no al ingreso original del informe quirúrgico.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VServicesProceduresReportQx';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Informe quirúrgico; Procedimiento quirúrgico principal (CUPS); Unidad funcional; Profesional de salud / Médico; Ingreso hospitalario; Recién nacido (vínculo madre-hijo); Orden de servicio', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VServicesProceduresReportQx';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve registros de HCQXINFOR cuyo procedimiento esté marcado como principal en HCQXREALI (QXPRINCIP = 1), uniendo catálogos de servicios, unidad funcional y profesional.; [RETURN_RESULT] resultset: Mediante UNION ALL agrega una segunda proyección donde el NUMINGRES corresponde al ingreso del recién nacido (INGMH.NUMINGRES = NUMINGRESHIJO), exigiendo vínculo en HCINGRESORECNAC, HCRECINAC y ADINGRESO.; [RETURN_RESULT] resultset: Calcula el flag Seleccione = 1 cuando GENSERVICEORDER no es nulo, y 0 en caso contrario.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VServicesProceduresReportQx';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si GENSERVICEORDER IS NULL → Seleccione = 0 (no se ha generado orden de servicio) else Seleccione = 1 (ya existe orden de servicio asociada)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VServicesProceduresReportQx';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCQXINFOR; dbo.INCUPSIPS; dbo.HCQXREALI; dbo.INUNIFUNC; dbo.INPROFSAL; dbo.HCINGRESORECNAC; dbo.HCRECINAC; dbo.ADINGRESO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VServicesProceduresReportQx';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VServicesProceduresReportQx';
GO
