CREATE PROCEDURE [dbo].[SP_CAL_ListarPacientes_ReportesDescartados]
(

  @CentroAtencion as varchar(max) ,
  @Tipo as Char(1),
  @UnidadFuncional as varchar(max), 
  @FechaInicial as varchar(max) ,
  @FechaFinal as varchar(max) 

)

AS
BEGIN
  SET NOCOUNT ON;
  
      declare @Sql as nvarchar(max)
	  SET @FechaInicial = CAST(@FechaInicial AS DATE)
	  SET @FechaFinal = CAST(@FechaFinal AS DATE)
 set @Sql = '

	Select 
		A.ID, 
		A.CODTIPPAC as ''Grupo Poblacion'', 
		Case A.CODTIPPAC 
			when 1 then ''Maternas'' 
			when 2 then ''Menor de 5 Años'' 
			when 3 then ''Adulto Mayor'' 
			when 4 then ''Discapacitado'' 
			when 5 then ''Población General'' 
		end As ''Tipo Poblacion'', 
		FECHAOCURRENC AS ''Fecha Ocurrencia'', 
		Rtrim(B.UFUDESCRI) AS ''Unidad Funcional Ocurrencia'', 
		IPCODPACI AS ''Identificacion'', 
		Rtrim(A.NOMBRECOMP) As ''Nombre paciente'', 
		Rtrim(C.NOMENTIDA) As ''Nombre Entidad'', 
		A.NUMINGRES AS ''Ingreso'', 
		A.FECNACIMIENTO, 
		CASE A.TIPORERPORTE 
			WHEN 1 THEN ''Acciones Inseguras'' 
			When 2 then ''Situaciones clínicas inesperadas''
		End As ''Tipo Reporte'', 
		CASE A.CLASEREPORTE 
			When 1 Then ''Voluntario'' 
			When 2 Then ''Busqueda activa'' 
			When 3 Then ''Otros''
			When 4 Then ''Identificado por terceros'' 
		end as ''Clase'', 
		A.FECHAREPORTE As ''Fecha Reporte'', 
		Rtrim(NOMCENATE) As ''Centro Atencion'', 
		Case A.TIPOIDENTIFICA 
			When 1 THEN ''Cédula ciudadanía'' 
			when 2 then ''Cédula extranjera'' 
			When 3 then ''Tarjeta Indetidad''
			When 4 then ''Registro civil'' 
			when 5 then ''Pasaporte'' 
			when 6 then ''Adulto sin ID'' 
			when 7 then ''Menor sin ID'' 
			When 8  Then ''NU'' 
		end as ''Tipo Identificacion'', 
		Case A.SEXO 
			when 1 then ''Masculino'' 
			when 2 then ''Femenino'' 
		end as ''Sexo'', 
		RTRIM(A.DIRECCION) as ''Direccion'', 
		RTRIM(A.TELEFONO) As ''Telefono'', 
		Case A.CLASIFICACION 
			When 1 then ''Evento adverso'' 
			when 2 then ''Incidente'' 
		end as ''Clasificacion'', 
		Rtrim(F.NOMBRE) As ''NivelDano'',
		RTRIM(A.DESCRIPCION) As ''Descripcion Evento'', 
		RTRIM(A.ACCIONCORREC) As ''Accion Correctiva'', 
		A.REPORTEANONIMO, 
		Case A.REPORTEANONIMO 
			when 1 then ''Si'' 
			when 2 then ''No'' 
		end as ''Reporte Anonimo'', 
		Rtrim(G.UFUDESCRI) as ''Unidad Funcional Anonimo'', 
		RTRIM(H.NOMUSUARI) As ''Nombre Usuari Anonimo'', 
		A.IDTIPO, 
		Case 
			When K.FECHAREGISTRO IS NOT NULL Then K.FECHAREGISTRO 
			else NULL 
		End As ''Fecha Modificacion'',
		Case 
			When K.USUARIOREGISTRO IS NOT NULL Then K.USUARIOREGISTRO
			else NULL 
		End	As ''Usuario Modificacion'',
		A.FECHAMODIFICACION AS ''Fecha Descartado'',
		A.USUARIOMODIFICACION AS ''Usuario Descarto'',
		case A.TIPOESTADO 
			when 1 then ''Reportado'' 
			when 2 then ''Descartado'' 
			when 3 then ''Modificado'' 
			when 4 then ''Visado''
		end as ''TIPOESTADO''
		,Rtrim(D.DESCRIPCION) As ''CLASEE'', Rtrim(D.DESCRIPCION) As ''TIPO'', [dbo].[Edad] (A.FECNACIMIENTO, Common.getdate()) as ''Edad''

	From 
		CALREPORTE A 
		Inner Join INUNIFUNC B ON A.UFUCODIGO = B.UFUCODIGO 
		Inner Join INENTIDAD C ON A.CODENTIDA = C.CODENTIDA 
		Inner Join ADCENATEN E on A.CODCENATE = E.CODCENATE 
		Inner Join CALTIPOCLASE D on A.IDTIPO  = D.ID AND D.TIPOACCION IN ( ' + @Tipo + ' )
		left Join CALNIVELDANO F on A.IDNIVELDANO = F.ID
		left Join INUNIFUNC G ON A.UFUCODIGORE = G.UFUCODIGO 
		left Join SEGusuaru H ON A.USUARIORE = H.CODUSUARI 
		left join CALSEGEST K ON A.ID = K.IDCALREPORTE AND K.ESTADO = 3

	Where
		A.TIPOESTADO = 2
		AND convert(varchar(max),A.CODCENATE) IN (' + @CentroAtencion + ')
		AND convert(varchar(max),A.UFUCODIGO) IN (' + @UnidadFuncional + ')
		AND convert(varchar(max),A.FECHAOCURRENC) BETWEEN  '''+ @FechaInicial +'''  AND  '''+ @FechaFinal +''''
		
	  exec sp_executesql @Sql 
		
END