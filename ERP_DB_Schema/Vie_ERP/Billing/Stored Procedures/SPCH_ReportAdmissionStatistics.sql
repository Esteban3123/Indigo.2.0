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