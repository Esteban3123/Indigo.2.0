CREATE PROCEDURE [dbo].[SPMOV_ListarLaboratoriosPacientesRealizados]
(
@Paciente Varchar(25),
@Ingreso Char(10)
)
AS
BEGIN
	SET NOCOUNT ON;
		
		WITH HCORDLABOCte (IPCODPACI, NUMINGRES, ESTSERIPS, CODSERIPS,[AUTO], [TABLE]) AS (
			SELECT IPCODPACI, NUMINGRES, ESTSERIPS, CODSERIPS,AUTO,'HCORDLABO' AS 'TABLE'
			FROM HCORDLABO
			WHERE IPCODPACI = @Paciente AND NUMINGRES = @Ingreso AND ESTALELAB IN (1,3)
		),
		AMBORDLABCte (IPCODPACI, NUMINGRES, ESTSERIPS, CODSERIPS,[AUTO], [TABLE]) AS (
			SELECT IPCODPACI, NUMINGRES, ESTSERIPS, CODSERIPS, AUTO, 'AMBORDLAB' AS 'TABLE'
			FROM AMBORDLAB
			WHERE IPCODPACI = @Paciente AND NUMINGRES = @Ingreso AND ESTALELAB IN (1,3)
		)

		SELECT DISTINCT RTRIM([CUPS].CODSERIPS) as Codigo,
			RTRIM([CUPS].DESSERIPS) AS Servicio, [ORDER].[TABLE],[ORDER].[AUTO],
			CASE ESTSERIPS WHEN 1 THEN 'Pendiente' WHEN 3 THEN 'Realizado' END As 'Estado',
			I.ANALITO As 'Analito', I.Valor AS 'Resultado', I.UNIDAD As 'Unidad', I.VALORMINIMO As 'ValorMinimo', I.VALORMAXIMO As 'ValorMaximo', I.CLASIFICACION As 'Clasificacion'
			FROM HCORDLABOCte [ORDER]
			INNER JOIN INCUPSIPS [CUPS] ON [ORDER].CODSERIPS = [CUPS].CODSERIPS 
			LEFT JOIN INTERCABE A ON A.IPCODPACI = [ORDER].IPCODPACI AND A.NUMINGRES = [ORDER].NUMINGRES
			LEFT JOIN INTERDETA AS B ON A.AUTO = B.CODCONCEC AND B.ORDTIP ='INT'
			LEFT JOIN INTERLABC AS C ON B.CODCONCEC = C.ORDEN_INDIGO
			LEFT JOIN INTERCTRL AS G ON [ORDER].[AUTO] = G.AUTOLABOR AND G.ORDEN_INDIGO = C.ORDEN_INDIGO
			LEFT JOIN INTERLABD AS I ON C.AUTO = I.CODCONCEC AND [ORDER].[AUTO] = I.AUTOLABOR
		WHERE A.IPCODPACI= @Paciente AND A.NUMINGRES = @Ingreso AND ESTSERIPS IN (1,3)

		UNION ALL

		SELECT DISTINCT RTRIM([CUPS].CODSERIPS) as Codigo,
			RTRIM([CUPS].DESSERIPS) AS Servicio, [ORDER].[TABLE],[ORDER].[AUTO],
			CASE ESTSERIPS WHEN 1 THEN 'Pendiente' WHEN 3 THEN 'Realizado' END As 'Estado',
			I.ANALITO As 'Analito', I.Valor AS 'Resultado', I.UNIDAD As 'Unidad', I.VALORMINIMO As 'ValorMinimo', I.VALORMAXIMO As 'ValorMaximo', I.CLASIFICACION As 'Clasificacion'
			FROM AMBORDLABCte [ORDER]
			INNER JOIN INCUPSIPS [CUPS] ON [ORDER].CODSERIPS = [CUPS].CODSERIPS 
			LEFT JOIN INTERCABE A ON A.IPCODPACI = [ORDER].IPCODPACI AND A.NUMINGRES = [ORDER].NUMINGRES
			LEFT JOIN INTERDETA AS B ON A.AUTO = B.CODCONCEC AND B.ORDTIP ='INT'
			LEFT JOIN INTERLABC AS C ON B.CODCONCEC = C.ORDEN_INDIGO
			LEFT JOIN INTERCTRL AS G ON [ORDER].[AUTO] = G.AUTOLABOR AND G.ORDEN_INDIGO = C.ORDEN_INDIGO
			LEFT JOIN INTERLABD AS I ON C.AUTO = I.CODCONCEC AND [ORDER].[AUTO] = I.AUTOLABOR
		WHERE A.IPCODPACI= @Paciente AND A.NUMINGRES = @Ingreso AND ESTSERIPS IN (1,3)

end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los exámenes de laboratorio realizados o pendientes de un paciente para un ingreso específico, consultando tanto las órdenes de laboratorio clínico hospitalario (HCORDLABO) como las órdenes ambulatorias (AMBORDLAB). Además de devolver el código CUPS y el nombre del servicio de laboratorio, integra los resultados del laboratorio externo (analito, valor, unidad, rango mínimo/máximo y clasificación) a través de las tablas de interfaz INTERCABE, INTERDETA, INTERLABC e INTERLABD. Se utiliza para mostrar en la historia clínica del paciente el estado y los resultados de sus exámenes de laboratorio, tanto de atención hospitalaria como ambulatoria, filtrando únicamente los que están en estado pendiente o realizado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPMOV_ListarLaboratoriosPacientesRealizados';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPMOV_ListarLaboratoriosPacientesRealizados';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los exámenes de laboratorio (hospitalarios y ambulatorios) ordenados a un paciente en un ingreso específico, mostrando estado pendiente/realizado y los resultados detallados por analito.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarLaboratoriosPacientesRealizados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir el paciente y el ingreso indicados con órdenes de laboratorio en HCORDLABO o AMBORDLAB; Las órdenes deben tener ESTALELAB en (1,3) para ser consideradas; Las órdenes deben tener estado de servicio ESTSERIPS en (1,3)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarLaboratoriosPacientesRealizados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan órdenes con ESTALELAB en (1,3) y ESTSERIPS en (1,3); Se identifica el origen de cada orden con la columna TABLE (''HCORDLABO'' o ''AMBORDLAB''); El cruce con INTERDETA exige ORDTIP=''INT'' para vincular la orden con la interfaz de laboratorio; Solo se mapean los estados 1 y 3 a etiquetas legibles; otros estados quedarían en NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarLaboratoriosPacientesRealizados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso/Hospitalización; Orden de laboratorio; Laboratorio ambulatorio; Laboratorio hospitalario; CUPS (codificación de servicios de salud); Analito; Resultado de laboratorio; Estado de servicio (Pendiente/Realizado); Valor de referencia (mínimo/máximo)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarLaboratoriosPacientesRealizados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve un único resultset unificando órdenes de HCORDLABO y AMBORDLAB con sus resultados de laboratorio asociados desde INTERLABD', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarLaboratoriosPacientesRealizados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ESTSERIPS = 1 → Se etiqueta el estado del servicio como ''Pendiente''; si ESTSERIPS = 3 → Se etiqueta el estado del servicio como ''Realizado''; si ESTALELAB IN (1,3) sobre HCORDLABO/AMBORDLAB → La orden se incluye en el CTE correspondiente else Se excluye de los resultados', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarLaboratoriosPacientesRealizados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORDLABO; dbo.AMBORDLAB; dbo.INCUPSIPS; dbo.INTERCABE; dbo.INTERDETA; dbo.INTERLABC; dbo.INTERCTRL; dbo.INTERLABD', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarLaboratoriosPacientesRealizados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarLaboratoriosPacientesRealizados';
-- GO
