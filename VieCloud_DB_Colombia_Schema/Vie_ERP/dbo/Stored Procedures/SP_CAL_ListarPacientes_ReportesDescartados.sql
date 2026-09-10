
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
		left join Admissions.TypesPopulationGroups AT WITH(NOLOCK) ON AT.CODE = A.CODTIPPAC

	Where
		A.TIPOESTADO = 2
		AND convert(varchar(max),A.CODCENATE) IN (' + @CentroAtencion + ')
		AND convert(varchar(max),A.UFUCODIGO) IN (' + @UnidadFuncional + ')
		AND convert(varchar(max),A.FECHAOCURRENC) BETWEEN  '''+ @FechaInicial +'''  AND  '''+ @FechaFinal +''''
		
	  exec sp_executesql @Sql 
		
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los pacientes cuyos reportes de seguridad clínica han sido descartados dentro de un rango de fechas, filtrando por centro de atención, unidad funcional y tipo de reporte (acciones inseguras o situaciones clínicas inesperadas). Consolida información del paciente (cédula, nombre, sexo, edad, dirección, teléfono, tipo de identificación, grupo poblacional), del evento reportado (clasificación como evento adverso o incidente, nivel de daño, descripción, acción correctiva, clase y tipo de reporte, anonimato) y de la trazabilidad del descarte (fecha y usuario que descartó, fecha y usuario de modificación previa). Sirve para auditoría y seguimiento del módulo de calidad y seguridad del paciente (CALREPORTE), permitiendo revisar qué reportes fueron desestimados y por quién.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_CAL_ListarPacientes_ReportesDescartados';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_CAL_ListarPacientes_ReportesDescartados';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los reportes de seguridad del paciente que han sido descartados, con datos demográficos, clínicos y de auditoría, filtrados por centro de atención, unidad funcional, tipo de acción y rango de fechas de ocurrencia.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CAL_ListarPacientes_ReportesDescartados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'FechaInicial y FechaFinal deben ser convertibles a DATE.; Los parámetros de centro de atención y unidad funcional deben venir como lista CSV válida para inyectar en cláusula IN.; El parámetro de tipo debe ser un valor o lista compatible con TIPOACCION del catálogo.; Debe existir catálogo CALTIPOCLASE con TIPOACCION coincidente para devolver filas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CAL_ListarPacientes_ReportesDescartados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se listan reportes con TIPOESTADO = 2 (Descartado).; Se filtra por tipo de acción del catálogo CALTIPOCLASE (D.TIPOACCION IN (@Tipo)).; Los reportes deben tener centro de atención, unidad funcional y entidad válidos (INNER JOIN obligatorio).; El historial de modificación se toma exclusivamente de CALSEGEST con ESTADO = 3.; Los datos de nivel de daño, unidad funcional anónima y usuario anónimo son opcionales (LEFT JOIN).; La edad se calcula dinámicamente con dbo.Edad sobre la fecha actual del sistema (Common.getdate).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CAL_ListarPacientes_ReportesDescartados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Reporte de seguridad del paciente; Eventos adversos; Incidentes; Acciones inseguras; Situaciones clínicas inesperadas; Reporte anónimo; Nivel de daño; Centro de atención; Unidad funcional; Grupo poblacional; Clasificación de reportes; Estados de reporte (Reportado/Descartado/Modificado/Visado)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CAL_ListarPacientes_ReportesDescartados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.CALREPORTE: Cuando TIPOESTADO = 2 y la ocurrencia está en el rango [FechaInicial, FechaFinal] y el centro/unidad funcional están en las listas y el tipo de acción coincide, devuelve el detalle del reporte descartado con descripciones decodificadas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CAL_ListarPacientes_ReportesDescartados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si TIPORERPORTE = 1 → Se etiqueta como ''Acciones Inseguras'' else Si = 2 se etiqueta ''Situaciones clínicas inesperadas''; si CLASEREPORTE 1..4 → Mapea a Voluntario/Búsqueda activa/Otros/Identificado por terceros; si CLASIFICACION = 1 → Evento adverso else Si = 2: Incidente; si TIPOESTADO 1..4 → Mapea Reportado/Descartado/Modificado/Visado; si K.FECHAREGISTRO IS NOT NULL (registro en CALSEGEST con ESTADO=3) → Expone fecha y usuario de modificación else Devuelve NULL en esos campos; si REPORTEANONIMO = 1 → Reporte marcado como anónimo (Sí) else Si = 2: No anónimo', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CAL_ListarPacientes_ReportesDescartados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'sys.sp_executesql', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CAL_ListarPacientes_ReportesDescartados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.CALREPORTE; dbo.INUNIFUNC; dbo.INENTIDAD; dbo.ADCENATEN; dbo.CALTIPOCLASE; dbo.CALNIVELDANO; dbo.SEGusuaru; dbo.CALSEGEST; Admissions.TypesPopulationGroups; dbo.Edad; Common.getdate', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CAL_ListarPacientes_ReportesDescartados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CAL_ListarPacientes_ReportesDescartados';
-- GO
