-- =============================================
-- Author:		Rafael Patino
-- Create date: 21/09/2023
-- Description:	se crea sp para listar actividades enfermeria con sql dinamico por la particion de tabla hechas en clinica medilisar
-- HCKARPAC_OLD y HCKARPAC_OLD2
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarHojaProcedimientoEnfermeria] 
	-- Add the parameters for the stored procedure here
	@condiciones varchar(250)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	declare @SQl as nvarchar(max)

	if exists(select 1 from INEMPRESU where INDNITEMP = '813001952-0')
	begin
				set @SQl = N'WITH CTE_GASTOPRODUCTO AS
						(
						 SELECT A.NUMCONSEC,RTRIM(B.DESPRODUC) AS ''DESCRIPCION DEL PRODUCTO'', CANPRODUCT AS ''CANTIDAD UTILIZADA DEL PRODUCTO'', RTRIM(A.CODPRODUC) AS ''CODIGO DEL PRODUCTO'',  CAST(1 AS INT) AS NUMERO, CAST(CAST(DAY(A.FECREGKAR) AS CHAR) + ''/'' + CAST(MONTH(A.FECREGKAR) AS CHAR) + ''/'' + CAST(YEAR(A.FECREGKAR) AS CHAR) AS DATETIME) AS DIAS,  A.FECREGKAR AS DIAS2, RTRIM(UFUDESCRI) AS ''DESCRIPCION UNIDAD FUNCIONAL'', A.DESMOVPRO AS ''ACTIVIDAD ENFERMERIA'', DOC.MEDIFIRMA AS ''FIRMA PROFESIONAL'',  A.FECHAUTIL AS FECHA, A.OBSERVACI AS OBSERVACION 
						 FROM (
								select A.ipcodpaci, A.numingres,A.TIPORIREG,A.CODPROSAL,A.UFUCODIGO, A.NUMCONSEC,RTRIM(A.CODPRODUC) as CODPRODUC,  A.FECREGKAR,  A.DESMOVPRO,  A.FECHAUTIL , A.OBSERVACI, A.CANPRODUCT  from dbo.HCKARDPAC A INNER JOIN dbo.IHLISTPRO B with (nolock) ON A.CODPRODUC = B.CODPRODUC where ' + @condiciones +'
								UNION 
								select A.ipcodpaci, A.numingres,A.TIPORIREG,A.CODPROSAL,A.UFUCODIGO,A.NUMCONSEC,RTRIM(A.CODPRODUC) as CODPRODUC,  A.FECREGKAR,  A.DESMOVPRO,  A.FECHAUTIL , A.OBSERVACI, A.CANPRODUCT  from dbo.HCKARDPAC_OLD A INNER JOIN dbo.IHLISTPRO B with (nolock) ON A.CODPRODUC = B.CODPRODUC where ' + @condiciones +'
								UNION 
								select A.ipcodpaci, A.numingres,A.TIPORIREG,A.CODPROSAL,A.UFUCODIGO,A.NUMCONSEC,RTRIM(A.CODPRODUC) as CODPRODUC,  A.FECREGKAR,  A.DESMOVPRO,  A.FECHAUTIL , A.OBSERVACI , A.CANPRODUCT from dbo.HCKARDPAC_OLD2 A INNER JOIN dbo.IHLISTPRO B with (nolock) ON A.CODPRODUC = B.CODPRODUC where ' + @condiciones +'
								) AS A
						 INNER JOIN dbo.IHLISTPRO B with (nolock) ON A.CODPRODUC = B.CODPRODUC  AND ' + @condiciones +'
						 INNER JOIN dbo.INPROFSAL DOC  with (nolock) ON A.CODPROSAL = DOC.CODPROSAL 
						 INNER JOIN dbo.INUNIFUNC C  with (nolock) ON A.UFUCODIGO = C.UFUCODIGO
						), CTE_GASTOINSUMOTMP AS
						(
						select MAX(G.CONSECUTI) AS CONSECUTI, A.NUMCONSEC, G.CANACTENF,  G.VALACTENF  
							 From dbo.HCKARDPAC_OLD A   with (nolock)
							 INNER Join dbo.IHLISTPRO B  with (nolock) ON B.CODPRODUC = A.CODPRODUC  AND ' + @condiciones +'
							 INNER Join dbo.HCHOGASIN G  with (nolock) On A.IPCODPACI = G.IPCODPACI  And A.NUMINGRES = G.NUMINGRES 
							 And A.CODPRODUC = G.CODPRODUC  
							 AND G.TIPORIGEN IN (2,3) 
							 AND A.FECREGKAR > G.FECHAUTIL 
							 AND (G.CANACTENF > 0 OR G.VALACTENF IS NOT NULL) 
							 AND (DATEDIFF (MINUTE, A.FECREGKAR, G.FECHAUTIL ) BETWEEN -3 And 3)  
							 GROUP BY A.NUMCONSEC,  G.CANACTENF,  G.VALACTENF
						),CTE_GENERAL as
						(
						select A.*,  H.CANACTENF as ''CANTIDAD ACTIVIDAD ENFERMERIA'', H.VALACTENF as ''RESULTADO ACTIVIDAD ENFERMERIA''  from CTE_GASTOPRODUCTO A with (nolock) left JOIN  CTE_GASTOINSUMOTMP H with (nolock) ON H.NUMCONSEC = A.NUMCONSEC 
						)
						select  *  from CTE_GENERAL ORDER BY DIAS,[DESCRIPCION DEL PRODUCTO], DIAS2  ASC;
					'
		end else begin
							set @SQl = N'WITH CTE_GASTOPRODUCTO AS
						(
						 SELECT A.NUMCONSEC,RTRIM(B.DESPRODUC) AS ''DESCRIPCION DEL PRODUCTO'', CANPRODUCT AS ''CANTIDAD UTILIZADA DEL PRODUCTO'', RTRIM(A.CODPRODUC) AS ''CODIGO DEL PRODUCTO'',  CAST(1 AS INT) AS NUMERO, CAST(CAST(DAY(A.FECREGKAR) AS CHAR) + ''/'' + CAST(MONTH(A.FECREGKAR) AS CHAR) + ''/'' + CAST(YEAR(A.FECREGKAR) AS CHAR) AS DATETIME) AS DIAS,  A.FECREGKAR AS DIAS2, RTRIM(UFUDESCRI) AS ''DESCRIPCION UNIDAD FUNCIONAL'', A.DESMOVPRO AS ''ACTIVIDAD ENFERMERIA'', DOC.MEDIFIRMA AS ''FIRMA PROFESIONAL'',  A.FECHAUTIL AS FECHA, A.OBSERVACI AS OBSERVACION 
						 FROM dbo.HCKARDPAC A
						 INNER JOIN dbo.IHLISTPRO B with (nolock) ON A.CODPRODUC = B.CODPRODUC  AND ' + @condiciones +'
						 INNER JOIN dbo.INPROFSAL DOC  with (nolock) ON A.CODPROSAL = DOC.CODPROSAL 
						 INNER JOIN dbo.INUNIFUNC C  with (nolock) ON A.UFUCODIGO = C.UFUCODIGO
						), CTE_GASTOINSUMOTMP AS
						(
						select MAX(G.CONSECUTI) AS CONSECUTI, A.NUMCONSEC, G.CANACTENF,  G.VALACTENF  
							 From dbo.HCKARDPAC A   with (nolock)
							 INNER Join dbo.IHLISTPRO B  with (nolock) ON B.CODPRODUC = A.CODPRODUC  AND ' + @condiciones +'
							 INNER Join dbo.HCHOGASIN G  with (nolock) On A.IPCODPACI = G.IPCODPACI  And A.NUMINGRES = G.NUMINGRES 
							 And A.CODPRODUC = G.CODPRODUC  
							 AND G.TIPORIGEN IN (2,3) 
							 AND A.FECREGKAR > G.FECHAUTIL 
							 AND (G.CANACTENF > 0 OR G.VALACTENF IS NOT NULL) 
							 AND (DATEDIFF (MINUTE, A.FECREGKAR, G.FECHAUTIL ) BETWEEN -3 And 3)  
							 GROUP BY A.NUMCONSEC,  G.CANACTENF,  G.VALACTENF
						),CTE_GENERAL as
						(
						select A.*,  H.CANACTENF as ''CANTIDAD ACTIVIDAD ENFERMERIA'', H.VALACTENF as ''RESULTADO ACTIVIDAD ENFERMERIA''  from CTE_GASTOPRODUCTO A with (nolock) left JOIN  CTE_GASTOINSUMOTMP H with (nolock) ON H.NUMCONSEC = A.NUMCONSEC 
						)
						select  *  from CTE_GENERAL ORDER BY DIAS,[DESCRIPCION DEL PRODUCTO], DIAS2  ASC;
					'
		end
		--print @sql
		exec sp_executesql  @SQl 
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista las actividades de enfermería registradas en la hoja de procedimientos de un paciente durante su ingreso hospitalario, incluyendo los productos o insumos utilizados, cantidades, fechas, unidad funcional, actividad realizada, firma del profesional, observaciones y resultados de actividad de enfermería. Utiliza SQL dinámico para consultar la tabla principal de kardex de paciente (HCKARDPAC) y sus particiones históricas (HCKARDPAC_OLD, HCKARDPAC_OLD2), aplicando un filtro variable recibido como parámetro de condiciones; para instituciones con NIT 813001952-0 (Clínica Medilisar) combina las tres tablas mediante UNION para cubrir datos históricos particionados. Cruza con el maestro de productos o insumos (IHLISTPRO), el maestro de profesionales de salud (INPROFSAL) y las unidades funcionales (INUNIFUNC), además de consultar los gastos de insumos de enfermería (HCHOGASIN) para obtener cantidades y resultados de las actividades. Se usa para generar el reporte o impresión de la hoja de procedimientos de enfermería en la historia clínica del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarHojaProcedimientoEnfermeria';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarHojaProcedimientoEnfermeria';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las actividades y productos registrados en la hoja de procedimientos de enfermería de un paciente, consolidando datos actuales e históricos según la institución, para impresión o reporte en la historia clínica.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarHojaProcedimientoEnfermeria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El parámetro de condiciones debe ser una cláusula WHERE válida y segura (riesgo de SQL injection, se concatena directamente).; Debe existir el registro de la institución en INEMPRESU para evaluar el NIT.; Las tablas históricas HCKARDPAC_OLD y HCKARDPAC_OLD2 deben existir cuando la institución es Clínica Medilisar.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarHojaProcedimientoEnfermeria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran gastos de insumos con TIPORIGEN IN (2,3) en HCHOGASIN.; La correlación entre kárdex y gasto de insumo exige que FECREGKAR > FECHAUTIL y que la diferencia en minutos entre ambas fechas esté entre -3 y 3.; Solo se incluyen registros de gasto cuya CANACTENF > 0 o VALACTENF no sea nulo.; Se toma el máximo CONSECUTI por agrupación de NUMCONSEC, CANACTENF y VALACTENF para evitar duplicidad de gastos.; El cruce con el catálogo de productos (IHLISTPRO), profesional (INPROFSAL) y unidad funcional (INUNIFUNC) es obligatorio (INNER JOIN), por lo que solo se listan registros con maestros completos.; El resultado se ordena cronológicamente por día y alfabéticamente por descripción del producto.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarHojaProcedimientoEnfermeria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Hoja de procedimientos de enfermería; Actividades de enfermería; Kárdex del paciente; Profesional de salud / firma profesional; Unidad funcional; Gasto de insumos; Productos / medicamentos; Historia clínica; Particionamiento histórico de datos clínicos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarHojaProcedimientoEnfermeria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve el listado de actividades de enfermería con producto, cantidad, unidad funcional, profesional firmante, fecha y resultado de la actividad, ordenado por día y descripción del producto.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarHojaProcedimientoEnfermeria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe en INEMPRESU una empresa con INDNITEMP = ''813001952-0'' (Clínica Medilisar) → Construye el SQL uniendo (UNION) HCKARDPAC, HCKARDPAC_OLD y HCKARDPAC_OLD2 para cubrir datos particionados históricos. else Construye el SQL consultando únicamente la tabla actual HCKARDPAC.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarHojaProcedimientoEnfermeria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INEMPRESU; dbo.HCKARDPAC; dbo.HCKARDPAC_OLD; dbo.HCKARDPAC_OLD2; dbo.IHLISTPRO; dbo.INPROFSAL; dbo.INUNIFUNC; dbo.HCHOGASIN', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarHojaProcedimientoEnfermeria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarHojaProcedimientoEnfermeria';
-- GO
