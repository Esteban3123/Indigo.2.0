CREATE PROCEDURE [dbo].[SP_CAL_ListarPacientes_ReportesVisados]
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
		AT.Name as ''Tipo Poblacion'', 
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
		A.FECHAMODIFICACION AS ''Fecha Visado'',
		A.USUARIOMODIFICACION AS ''Usuario Visado'',
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
		left join Admissions.TypesPopulationGroups AT WITH(NOLOCK) ON AT.CODE = A.CODTIPPAC
	Where
		A.TIPOESTADO = 4   
		AND convert(varchar(max),A.CODCENATE) IN (' + @CentroAtencion + ')
		AND convert(varchar(max),A.UFUCODIGO) IN (' + @UnidadFuncional + ')
		AND convert(varchar(max),A.FECHAOCURRENC) BETWEEN  '''+ @FechaInicial +'''  AND  '''+ @FechaFinal +''''
		
	  exec sp_executesql @Sql 
  
  
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los reportes de eventos de calidad y seguridad del paciente que han sido visados (estado 4), incluyendo datos del paciente (cédula, nombre, sexo, edad, dirección, teléfono), datos del evento (fecha de ocurrencia, tipo de reporte, clasificación como evento adverso o incidente, nivel de daño, descripción, acciones correctivas) y datos administrativos (centro de atención, unidad funcional, entidad, ingreso). Filtra por rango de fechas de ocurrencia, centro de atención, unidad funcional y tipo de acción, permitiendo consultas multiselección mediante SQL dinámico. Se usa para reportería y auditoría de eventos adversos, incidentes y acciones inseguras visados por el área de calidad asistencial.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_CAL_ListarPacientes_ReportesVisados';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_CAL_ListarPacientes_ReportesVisados';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Listar los reportes de seguridad del paciente (acciones inseguras / situaciones clínicas inesperadas) que se encuentran en estado Visado, filtrados por centro de atención, unidad funcional, tipo de acción y rango de fecha de ocurrencia.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CAL_ListarPacientes_ReportesVisados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las fechas recibidas deben ser convertibles a DATE.; Los parámetros de centro de atención y unidad funcional deben venir como lista válida para cláusula IN (valores separados por coma y comillas según corresponda).; El parámetro de tipo debe contener valores válidos para CALTIPOCLASE.TIPOACCION.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CAL_ListarPacientes_ReportesVisados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se listan reportes con estado Visado (TIPOESTADO=4).; Solo se listan reportes cuyo tipo/clase pertenece al TIPOACCION solicitado.; El historial de modificación se toma exclusivamente del seguimiento con ESTADO=3 (Modificado).; El filtro de fechas aplica sobre la fecha de ocurrencia del evento.; Filtros de centro de atención y unidad funcional admiten múltiples valores (lista IN dinámica).; La edad del paciente se calcula con la función dbo.Edad usando la fecha actual de Common.getdate().', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CAL_ListarPacientes_ReportesVisados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Reporte de eventos adversos; Acciones inseguras; Situaciones clínicas inesperadas; Incidente; Evento adverso; Visado de reportes; Nivel de daño; Reporte anónimo; Grupo poblacional; Centro de atención; Unidad funcional; Paciente; Tipo de identificación', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CAL_ListarPacientes_ReportesVisados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Resultset: Cuando A.TIPOESTADO=4 y se cumplen filtros de centro, unidad funcional, tipo y rango de fecha de ocurrencia, retorna el detalle del reporte con datos del paciente, entidad, clasificación, nivel de daño y trazabilidad de modificación/visado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CAL_ListarPacientes_ReportesVisados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si CALREPORTE.TIPORERPORTE = 1 → Se etiqueta como ''Acciones Inseguras'' else Si =2 se etiqueta ''Situaciones clínicas inesperadas''; si CALREPORTE.CLASEREPORTE in (1..4) → Mapea a Voluntario/Búsqueda activa/Otros/Identificado por terceros; si CALREPORTE.CLASIFICACION = 1 → Clasifica como ''Evento adverso'' else Si =2 ''Incidente''; si CALREPORTE.TIPOESTADO in (1..4) → Mapea a Reportado/Descartado/Modificado/Visado; si CALREPORTE.REPORTEANONIMO = 1 → Reporte marcado como anónimo (Sí) else Si =2, No anónimo; si CALSEGEST.FECHAREGISTRO IS NOT NULL (con ESTADO=3) → Se expone fecha y usuario de modificación desde el seguimiento de estado modificado else NULL en fecha/usuario de modificación; si CALTIPOCLASE.TIPOACCION IN (@Tipo) → Solo se incluyen reportes cuyo tipo/clase coincide con el filtro de tipo de acción', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CAL_ListarPacientes_ReportesVisados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.CALREPORTE; dbo.INUNIFUNC; dbo.INENTIDAD; dbo.ADCENATEN; dbo.CALTIPOCLASE; dbo.CALNIVELDANO; dbo.SEGusuaru; dbo.CALSEGEST; Admissions.TypesPopulationGroups', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CAL_ListarPacientes_ReportesVisados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CAL_ListarPacientes_ReportesVisados';
-- GO
