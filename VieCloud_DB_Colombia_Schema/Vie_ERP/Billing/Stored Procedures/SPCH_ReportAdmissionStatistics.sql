-- =============================================
-- Author:		<Johan Sebastian Carranza Ramos, Developer Junior>
-- Create date: <20 Agosto de 2019>
-- Description:	<Procedimiento almacenado que lista los datos para el reporte de estadistico de ingresos.
-- =============================================
CREATE PROCEDURE [Billing].[SPCH_ReportAdmissionStatistics]( 
	@Agrupador varchar (20), --Agrupador para determinar porque campo se debe agrupar. (0-Entidad,1-Grupo de atencion,2-Usuario apertura,3-Usuario facturo, 4-centro atencion, 5-unidad funcional, 6-Estado ingreso)
	@FechaInicial As Datetime,
	@FechaFinal As Datetime,
	@TipoReporte As varchar(20),
	@Estado As varchar(max),
	@Tercero As Varchar(max),
	@Entidad As Varchar (max),
	@GrupoAtencion As Varchar(max),
	@UsuarioCrea As varchar (max),
	@CentroAtencion As Varchar (max),
	@UnidadFuncional As Varchar (max)
	)
AS
BEGIN
	--@Agrupador = '0' Entidad
	--@Agrupador = '1' Grupo de atencion
	--@Agrupador = '2' UsuarioCreador
	--@Agrupador = '3' Usuario que facturo
	--@Agrupador = '4' CentroAtención
	--@Agrupador = '5' UnidadFuncional 
	--@Agrupador = '6' Estado

	--Estado = F-Facturado,A-Anulado,''-Abierto,P-parcial,C-Cerrado

	If @TipoReporte = 'Detallado' Begin
		SELECT C.NOMCENATE,
			   B.NOMENTIDA,
			   D.UFUCODIGO,
			   D.UFUDESCRI, 
			   Billing.fnGetDescriptionUnitTypeFunctionalUnit(D.UFUTIPUNI) UnitTypeDescription,
			   UFUACT.UFUCODIGO UFUCODIGOACT,
			   UFUACT.UFUDESCRI UFUDESCRIACT,
			   Billing.fnGetDescriptionUnitTypeFunctionalUnit(UFUACT.UFUTIPUNI) UnitTypeDescriptionUFUACT,
			   UFUACTHOS.UFUCODIGO UFUCODIGOACTHOS,
			   UFUACTHOS.UFUDESCRI UFUDESCRIACTHOS,
			   Billing.fnGetDescriptionUnitTypeFunctionalUnit(UFUACTHOS.UFUTIPUNI) UnitTypeDescriptionUFUACTHOS,
			   A.NUMINGRES, 
			   A.IFECHAING, 
			   A.IPCODPACI,
			   E.IPNOMCOMP,
			   F.CODUSUARI, 
			   F.NOMUSUARI, 
			   CASE A.IESTADOIN 
			   WHEN '' THEN 'Abierto' 
			   WHEN 'P' THEN 'Parcial' 
			   WHEN 'F' THEN 'Facturado' 
			   WHEN 'C' THEN 'Cerrado' 
			   WHEN 'A' THEN 'Anulado' 
			   ELSE 'Otro' END AS ESTADO, 
			   G.Name, 
			   UADCO.CODUSUARI ControlUserCode, 
			   UADCO.NOMUSUARI ControlUserName,
			   IIF(A.TIPOINGRE = 1, 'Ambulatorio', 'Hospitalario') AdmissionTypeDescription, 
			   salida.FECALTPAC
		FROM .ADINGRESO AS A WITH(NOLOCK) 
			INNER JOIN .INENTIDAD AS B WITH(NOLOCK) ON B.CODENTIDA = A.CODENTIDA 
			INNER JOIN .ADCENATEN AS C WITH(NOLOCK) ON C.CODCENATE = A.CODCENATE 
			INNER JOIN .INUNIFUNC AS D WITH(NOLOCK) ON D.UFUCODIGO = A.UFUCODIGO 
			INNER JOIN .INPACIENT AS E WITH(NOLOCK) ON E.IPCODPACI = A.IPCODPACI
			INNER JOIN .SEGusuaru AS F WITH(NOLOCK) ON F.CODUSUARI = A.CODUSUCRE 
			INNER JOIN Contract.CareGroup AS G WITH(NOLOCK) ON G.ID = A.GENCAREGROUP
			left join (
				SELECT MIN(TRIANUMER) TRIANUMER, NUMINGRES, MIN(CODCONCEC) CODCONCEC
				FROM .ADTRIAGEU
				WHERE CODCONCEC IS NOT NULL
				GROUP BY NUMINGRES
			) AS ADT ON ADT.NUMINGRES = A.NUMINGRES
			left join .ADCONTURG AS ADCO ON ADT.CODCONCEC = ADCO.CODCONCEC
			left join .SEGusuaru AS UADCO WITH(NOLOCK) ON UADCO.CODUSUARI = ADCO.CODUSUARI
			left join .INUNIFUNC AS UFUACT WITH(NOLOCK) ON UFUACT.UFUCODIGO = A.UFUACTPAC
			left join .INUNIFUNC AS UFUACTHOS WITH(NOLOCK) ON UFUACTHOS.UFUCODIGO = A.UFUAACTHOS
			left join dbo.HCREGEGRE AS salida WITH (nolock) ON salida.NUMINGRES = a.NUMINGRES
		WHERE IFECHAING BETWEEN @FechaInicial AND @FechaFinal
			AND (@Estado = '' OR IESTADOIN IN (SELECT convert(varchar, rtrim(Data)) FROM dbo.Split(@Estado, ','))) -- Filtro estado 
			AND (@Tercero = '' OR A.IPCODPACI IN (SELECT convert(varchar, rtrim(Data)) FROM dbo.Split(@Tercero, ','))) -- Filtro tercero
			AND (@Entidad = '' OR RTRIM(A.CODENTIDA) IN (SELECT convert(varchar, rtrim(Data)) FROM dbo.Split(@Entidad , ','))) -- Filtro entidad
			AND (@GrupoAtencion = '' OR RTRIM(G.Code) IN (SELECT convert(varchar, rtrim(Data)) FROM dbo.Split(@GrupoAtencion, ','))) -- Filtro Grupo atencion
			AND (@UsuarioCrea = '' OR RTRIM(A.CODUSUCRE) IN (SELECT convert(varchar, rtrim(Data)) FROM dbo.Split(@UsuarioCrea, ','))) -- Filtro usuario que crea
			AND (@CentroAtencion = '' OR RTRIM(A.CODCENATE) IN (SELECT convert(varchar, rtrim(Data)) FROM dbo.Split(@CentroAtencion, ','))) -- Filtro centro de atencion
			AND (@UnidadFuncional = '' OR RTRIM(A.UFUCODIGO) IN (SELECT convert(varchar, rtrim(Data)) FROM dbo.Split(@UnidadFuncional, ','))) -- Filtro unidad funcional
		ORDER BY 
			CASE WHEN @Agrupador = '0' THEN B.NOMENTIDA 
				 WHEN @Agrupador = '1' THEN G.Name
				 WHEN @Agrupador = '2' THEN F.NOMUSUARI
				--WHEN @Agrupador = '3' THEN F.NOMUSUARI
				 WHEN @Agrupador = '4' THEN C.NOMCENATE
				 WHEN @Agrupador = '5' THEN D.UFUDESCRI
				 WHEN @Agrupador = '6' THEN CASE A.IESTADOIN WHEN '' THEN 'Abierto' 
															 WHEN 'P' THEN 'Parcial' 
															 WHEN 'F' THEN 'Facturado' 
															 WHEN 'C' THEN 'Cerrado' 
															 WHEN 'A' THEN 'Anulado' 
															 ELSE 'Otro' END 
			END		
		End
	Else If @TipoReporte = 'Resumido'
	Begin
		SELECT CASE WHEN @Agrupador = '0' THEN B.NOMENTIDA 
					WHEN @Agrupador = '1' THEN G.Name
					WHEN @Agrupador = '2' THEN F.NOMUSUARI
					--WHEN @Agrupador = '3' THEN F.NOMUSUARI
					WHEN @Agrupador = '4' THEN C.NOMCENATE
					WHEN @Agrupador = '5' THEN D.UFUDESCRI
					WHEN @Agrupador = '6' THEN CASE A.IESTADOIN WHEN '' THEN 'Abierto' WHEN 'P' THEN 'Parcial' 
					                            WHEN 'F' THEN 'Facturado' WHEN 'C' THEN 'Cerrado' WHEN 'A' THEN 'Anulado' ELSE 'Otro' END END AS Agrupador , COUNT(1) AS Cantidad

		FROM .ADINGRESO AS A WITH(NOLOCK) 
			INNER JOIN .INENTIDAD AS B WITH(NOLOCK) ON B.CODENTIDA = A.CODENTIDA 
			INNER JOIN .ADCENATEN AS C WITH(NOLOCK) ON C.CODCENATE = A.CODCENATE 
			INNER JOIN .INUNIFUNC AS D WITH(NOLOCK) ON D.UFUCODIGO = A.UFUCODIGO 
			INNER JOIN .INPACIENT AS E WITH(NOLOCK) ON E.IPCODPACI = A.IPCODPACI 
			INNER JOIN .SEGusuaru AS F WITH(NOLOCK) ON F.CODUSUARI = A.CODUSUCRE 
			INNER JOIN Contract.CareGroup AS G WITH(NOLOCK) ON G.ID = A.GENCAREGROUP
		WHERE IFECHAING BETWEEN @FechaInicial AND @FechaFinal
			AND (@Estado = '' OR RTRIM(IESTADOIN) IN (SELECT convert(varchar, rtrim(Data)) FROM dbo.Split(@Estado, ','))) -- Filtro estado 
			AND (@Tercero = '' OR RTRIM(A.IPCODPACI) IN (SELECT convert(varchar, rtrim(Data)) FROM dbo.Split(@Tercero, ','))) -- Filtro tercero
			AND (@Entidad = '' OR RTRIM(A.CODENTIDA) IN (SELECT convert(varchar, rtrim(Data)) FROM dbo.Split(@Entidad , ','))) -- Filtro entidad
			AND (@GrupoAtencion = '' OR RTRIM(G.Code) IN (SELECT convert(varchar, rtrim(Data)) FROM dbo.Split(@GrupoAtencion, ','))) -- Filtro Grupo atencion
			AND (@UsuarioCrea = '' OR RTRIM(A.CODUSUCRE) IN (SELECT convert(varchar, rtrim(Data)) FROM dbo.Split(@UsuarioCrea, ','))) -- Filtro usuario que crea
			AND (@CentroAtencion = '' OR RTRIM(A.CODCENATE) IN (SELECT convert(varchar, rtrim(Data)) FROM dbo.Split(@CentroAtencion, ','))) -- Filtro centro de atencion
			AND (@UnidadFuncional = '' OR RTRIM(A.UFUCODIGO) IN (SELECT convert(varchar, rtrim(Data)) FROM dbo.Split(@UnidadFuncional, ','))) -- Filtro unidad funcional
		GROUP BY CASE WHEN @Agrupador = '0' THEN B.NOMENTIDA 
					  WHEN @Agrupador = '1' THEN G.Name
					  WHEN @Agrupador = '2' THEN F.NOMUSUARI
					  --WHEN @Agrupador = '3' THEN F.NOMUSUARI
					  WHEN @Agrupador = '4' THEN C.NOMCENATE
					  WHEN @Agrupador = '5' THEN D.UFUDESCRI
					  WHEN @Agrupador = '6' THEN CASE A.IESTADOIN WHEN '' THEN 'Abierto' WHEN 'P' THEN 'Parcial' WHEN 'F' THEN 'Facturado' WHEN 'C' THEN 'Cerrado' 
													WHEN 'A' THEN 'Anulado' ELSE 'Otro' END END
		
	End
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reporte estadístico de ingresos y admisiones de pacientes. Genera dos modos de consulta: ''Detallado'', que lista cada ingreso con datos del paciente (cédula, nombre), entidad pagadora (EPS/aseguradora), centro de atención, unidad funcional, estado del ingreso (abierto, parcial, facturado, cerrado, anulado), tipo de admisión (ambulatorio u hospitalario), usuario que creó el ingreso, grupo de atención del contrato, información de triage de urgencias, usuario de control de urgencias y fecha de egreso; y ''Resumido'', que agrupa y cuenta los ingresos según el criterio seleccionado (entidad, grupo de atención, usuario creador, centro de atención, unidad funcional o estado). Permite filtrar por rango de fechas de ingreso, estado, paciente, entidad, grupo de atención, usuario, centro de atención y unidad funcional. Se usa para analizar la productividad y distribución de admisiones en el área de facturación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SPCH_ReportAdmissionStatistics';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SPCH_ReportAdmissionStatistics';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el reporte estadístico de ingresos/admisiones en dos modalidades (Detallado o Resumido) con filtros y agrupador configurables sobre fecha, estado, entidad, grupo de atención, usuario, centro y unidad funcional.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SPCH_ReportAdmissionStatistics';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'@TipoReporte debe ser ''Detallado'' o ''Resumido''; cualquier otro valor no devuelve resultados.; @FechaInicial y @FechaFinal deben definir un rango válido sobre ADINGRESO.IFECHAING.; Los parámetros multivaluados (@Estado, @Tercero, @Entidad, @GrupoAtencion, @UsuarioCrea, @CentroAtencion, @UnidadFuncional) deben venir vacíos ('''') para no filtrar, o como lista separada por comas compatible con dbo.Split.; @Agrupador debe ser uno de ''0'',''1'',''2'',''4'',''5'',''6'' (el valor ''3'' quedó deshabilitado por código comentado).; Los códigos de estado válidos en el filtro son: ''''(Abierto), ''P'', ''F'', ''C'', ''A''.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SPCH_ReportAdmissionStatistics';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El rango obligatorio de filtrado siempre es IFECHAING BETWEEN @FechaInicial AND @FechaFinal; sin importar otros filtros, los ingresos fuera del rango nunca aparecen.; Los filtros multivaluados (@Estado, @Tercero, @Entidad, @GrupoAtencion, @UsuarioCrea, @CentroAtencion, @UnidadFuncional) se interpretan como CSV vía dbo.Split; el valor '''' equivale a ''sin filtro''.; Solo se incluyen ingresos que tengan entidad, centro de atención, unidad funcional, paciente, usuario creador y grupo de atención existentes (INNER JOIN); registros huérfanos en esos catálogos se excluyen.; Para el detalle de triage solo se considera el TRIANUMER mínimo y el CODCONCEC mínimo por NUMINGRES, y únicamente cuando CODCONCEC IS NOT NULL.; El Agrupador ''3'' (Usuario que facturó) está deshabilitado: el código que lo manejaría está comentado, por lo que no produce ordenamiento ni agrupación.; Todas las lecturas se hacen con WITH(NOLOCK), permitiendo lecturas sucias para fines de reporte.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SPCH_ReportAdmissionStatistics';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ingreso/Admisión; Entidad responsable de pago; Grupo de atención; Centro de atención; Unidad funcional; Estado del ingreso (Abierto/Parcial/Facturado/Cerrado/Anulado); Triage de urgencias; Egreso hospitalario; Tipo de admisión (Ambulatorio/Hospitalario); Paciente', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SPCH_ReportAdmissionStatistics';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] ?: Cuando @TipoReporte=''Detallado'', retorna un resultset con datos del ingreso, paciente, entidad, centro y unidades funcionales (actual, hospitalaria), usuario creador, usuario de control de urgencias (vía ADTRIAGEU→ADCONTURG), estado descriptivo, tipo de admisión y fecha de alta (HCREGEGRE.FECALTPAC), ordenado por el campo definido por @Agrupador.; [RETURN_RESULT] ?: Cuando @TipoReporte=''Resumido'', retorna un resultset con dos columnas (Agrupador, Cantidad) que cuenta ingresos agrupados por el campo determinado por @Agrupador.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SPCH_ReportAdmissionStatistics';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @TipoReporte = ''Detallado'' → Devuelve listado fila a fila de cada ingreso con datos del paciente, entidad, centro/unidad funcional (incluida la unidad actual y la hospitalaria), usuario creador, usuario que atendió en triage/urgencias, estado descriptivo, tipo de admisión (Ambulatorio/Hospitalario) y fecha de alta desde HCREGEGRE. else Si @TipoReporte = ''Resumido'', devuelve solo el agrupador y COUNT(1) como Cantidad de ingresos.; si Valor de @Agrupador (0..6) → Determina la columna de ORDER BY (modo Detallado) o de GROUP BY/SELECT (modo Resumido): 0=Entidad, 1=Grupo de atención (CareGroup.Name), 2=Usuario creador, 4=Centro de atención, 5=Unidad funcional, 6=Estado descriptivo. El valor 3 (Usuario que facturó) está comentado y no se aplica.; si A.IESTADOIN → Se mapea a etiqueta: ''''→Abierto, ''P''→Parcial, ''F''→Facturado, ''C''→Cerrado, ''A''→Anulado, cualquier otro→''Otro''.; si IIF(A.TIPOINGRE = 1, ...) → TIPOINGRE=1 se reporta como ''Ambulatorio''; cualquier otro valor como ''Hospitalario''.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SPCH_ReportAdmissionStatistics';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADINGRESO; dbo.INENTIDAD; dbo.ADCENATEN; dbo.INUNIFUNC; dbo.INPACIENT; dbo.SEGusuaru; Contract.CareGroup; dbo.ADTRIAGEU; dbo.ADCONTURG; dbo.HCREGEGRE; dbo.Split; Billing.fnGetDescriptionUnitTypeFunctionalUnit', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SPCH_ReportAdmissionStatistics';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SPCH_ReportAdmissionStatistics';
-- GO
