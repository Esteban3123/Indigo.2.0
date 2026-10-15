CREATE PROCEDURE [dbo].[SP_CAL_ListarPacientesAccionesInseguras]
(
  @CentroAtencion as varchar(max)   
)

AS
BEGIN
  SET NOCOUNT ON;

  declare @Sql as nvarchar(max)
  
 set @Sql = ' Select A.ID,A.CODTIPPAC as ''Grupo Poblacion'',Case A.CODTIPPAC when 1 then ''Maternas'' when 2 then ''Menor de 5 Años'' when 3 then ''Adulto Mayor'' when 4 then ''Discapacitado'' when 5 then ''Población General'' end As ''Tipo Poblacion'',FECHAOCURRENC AS ''Fecha Ocurrencia'',Rtrim(B.UFUDESCRI) AS ''Unidad Funcional Ocurrencia'',A.IPCODPACI AS ''Identificacion'',Rtrim(A.NOMBRECOMP) As ''Nombre paciente'',Rtrim(C.NOMENTIDA) As ''Nombre Entidad'',
		  dbo.[RiskFactorAlert](A.IPCODPACI, A.NUMINGRES, 1) AS ''AlertaFactoresRiesgo'',
		  dbo.[RiskFactorAlert](A.IPCODPACI, A.NUMINGRES, 2) AS ''AlertaEscalas'',
		  A.NUMINGRES AS ''Ingreso'',A.FECNACIMIENTO,CASE A.TIPORERPORTE WHEN 1 THEN ''Acciones Inseguras'' When 2 then ''Situaciones clínicas inesperadas'' End As ''Tipo Reporte'',CASE A.CLASEREPORTE When 1 Then ''Voluntario'' When 2 Then ''Busqueda activa'' When 3 Then ''Otros'' When 4 Then ''Identificado por terceros'' end as ''Clase'',
		  A.FECHAREPORTE As ''Fecha Reporte'',Rtrim(NOMCENATE) As ''Centro Atencion'', A.TIPOIDENTIFICA  as ''Codigo Tipo Identificacion'',Case A.TIPOIDENTIFICA When 1 THEN ''Cédula ciudadanía'' when 2 then ''Cédula extranjera'' When 3 then ''Tarjeta Indetidad'' When 4 then ''Registro civil'' when 5 then ''Pasaporte'' when  6 then ''Adulto sin ID'' when 7 then ''Menor sin ID'' When 8  Then ''NU'' end as ''Tipo Identificacion'',
		  Case A.SEXO when 1 then ''Masculino'' when 2 then ''Femenino'' end as ''Sexo'',RTRIM(A.DIRECCION) as ''Direccion'',RTRIM(A.TELEFONO) As ''Telefono'',Case A.CLASIFICACION When 1 then ''Evento adverso'' when 2 then ''Incidente'' end as ''Clasificacion'',
		  Rtrim(F.NOMBRE) As ''NivelDano'',RTRIM(A.DESCRIPCION) As ''Descripcion Evento'',RTRIM(A.ACCIONCORREC) As ''Accion Correctiva'',A.REPORTEANONIMO,Case A.REPORTEANONIMO when 1 then ''Si'' when 2 then ''No'' end as ''Reporte Anonimo'',Rtrim(G.UFUDESCRI) as ''Unidad Funcional Anonimo'',RTRIM(H.NOMUSUARI) As ''Nombre Usuari Anonimo'',A.IDTIPO ,
		  case D.CONTROLVIGILA when 1 then ''Tecnovigilancia'' when 2 then ''Farmacovigilancia'' when 3 then ''Hemovigilancia'' when 4 then ''Reactivovigilancia'' when 5 then ''IAAS-Dispositivos'' when 6 then ''IAAS-Infección de localización quirúrgica'' when 7 then ''Ninguno'' end as ''Nombre Control'',Rtrim(X.DESCCAMAS) as ''Cama'',UFUACTPAC , 
		  case A.TIPOESTADO when 1 then ''Reportado'' when 2 then ''Descartado'' when 3 then ''Modificado'' when 4 then ''Visado'' end as ''TIPOESTADO''  
		  ,Rtrim(D.DESCRIPCION) As ''CLASEE'', Rtrim(D.DESCRIPCION) As ''TIPO'', [dbo].[Edad] (A.FECNACIMIENTO, Common.getdate()) as ''Edad''
		  from CALREPORTE A WITH(NOLOCK)
			  Inner Join ADINGRESO z WITH(NOLOCK) ON z.NUMINGRES = A.NUMINGRES
			  Inner Join INUNIFUNC B WITH(NOLOCK) ON A.UFUCODIGO = B.UFUCODIGO
			  Inner Join INENTIDAD C WITH(NOLOCK) ON A.CODENTIDA = C.CODENTIDA
			  Inner Join ADCENATEN E WITH(NOLOCK) on A.CODCENATE = E.CODCENATE
			  Inner Join CALTIPOCLASE D WITH(NOLOCK) on A.IDTIPO  = D.ID AND D.TIPOACCION = 1
			  Left Join CHCAMASHO X WITH(NOLOCK) on X.codicamas = z.CODCAMACT
			  left Join CALNIVELDANO F WITH(NOLOCK) on A.IDNIVELDANO = F.ID
			  left Join INUNIFUNC G WITH(NOLOCK) ON A.UFUCODIGORE = G.UFUCODIGO
			  left Join SEGusuaru H WITH(NOLOCK) ON A.USUARIORE = H.CODUSUARI
  Where  convert(varchar(max),A.CODCENATE) IN (' + @CentroAtencion + ') AND A.TIPOESTADO IN (1,3)  AND A.FECHAREPORTE> ''2023-01-08'''
 
  exec sp_executesql @Sql 
  
  
END