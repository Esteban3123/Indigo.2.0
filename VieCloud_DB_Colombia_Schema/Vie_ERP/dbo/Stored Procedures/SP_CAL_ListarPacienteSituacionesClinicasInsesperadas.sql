CREATE PROCEDURE [dbo].[SP_CAL_ListarPacienteSituacionesClinicasInsesperadas]
(
  @CentroAtencion as varchar(max) 
)

AS
BEGIN
  SET NOCOUNT ON;
  
 declare @Sql as nvarchar(max)

  Set @Sql = ' Select A.ID,A.CODTIPPAC as ''Grupo Poblacion'',AT.Name as ''Tipo Poblacion'',FECHAOCURRENC AS ''Fecha Ocurrencia'',Rtrim(B.UFUDESCRI) AS ''Unidad Funcional Ocurrencia'',IPCODPACI AS ''Identificacion'',Rtrim(A.NOMBRECOMP) As ''Nombre paciente'',Rtrim(C.NOMENTIDA) As ''Nombre Entidad'',
		  A.NUMINGRES AS ''Ingreso'',A.FECNACIMIENTO,CASE A.TIPORERPORTE WHEN 1 THEN ''Acciones Inseguras'' When 2 then ''Situaciones clínicas inesperadas'' End As ''Tipo Reporte'',CASE A.CLASEREPORTE When 1 Then ''Voluntario'' When 2 Then ''Busqueda activa'' When 3 Then ''Otros'' When 4 Then ''Identificado por terceros'' end as ''Clase'',
		  A.FECHAREPORTE As ''Fecha Reporte'',Rtrim(NOMCENATE) As ''Centro Atencion'', Case A.TIPOIDENTIFICA When 1 THEN ''Cédula ciudadanía'' when 2 then ''Cédula extranjera'' When 3 then ''Tarjeta Indetidad'' When 4 then ''Registro civil'' when 5 then ''Pasaporte'' when  6 then ''Adulto sin ID'' when 7 then ''Menor sin ID'' When 8  Then ''NU'' end as ''Tipo Identificacion'',
		  Case A.SEXO when 1 then ''Masculino'' when 2 then ''Femenino'' end as ''Sexo'',RTRIM(A.DIRECCION) as ''Direccion'',RTRIM(A.TELEFONO) As ''Telefono'',
		  RTRIM(A.DESCRIPCION) As ''Descripcion Evento'',RTRIM(A.ACCIONCORREC) As ''Accion Correctiva'',A.REPORTEANONIMO,Case A.REPORTEANONIMO when 1 then ''Si'' when 2 then ''No'' end as ''Reporte Anonimo'',Rtrim(G.UFUDESCRI) as ''Unidad Funcional Anonimo'',RTRIM(H.NOMUSUARI) As ''Nombre Usuari Anonimo'',A.IDTIPO , 
		  case A.TIPOESTADO when 1 then ''Reportado'' when 2 then ''Descartado'' when 3 then ''Modificado'' when 4 then ''Visado'' end as ''TIPOESTADO''   
		  ,Rtrim(D.DESCRIPCION) As ''CLASEE'', Rtrim(D.DESCRIPCION) As ''TIPO'', [dbo].[Edad] (A.FECNACIMIENTO, Common.getdate()) as ''Edad''
		  from CALREPORTE A
		  Inner Join INUNIFUNC B ON A.UFUCODIGO = B.UFUCODIGO
		  Inner Join INENTIDAD C ON A.CODENTIDA = C.CODENTIDA
	      Inner Join ADCENATEN E on A.CODCENATE = E.CODCENATE
		  Inner Join CALTIPOCLASE D on A.IDTIPO  = D.ID AND D.TIPOACCION = 2
		  left Join INUNIFUNC G ON A.UFUCODIGORE = G.UFUCODIGO
		  left Join SEGusuaru H ON A.USUARIORE = H.CODUSUARI
		  left join Admissions.TypesPopulationGroups AT WITH(NOLOCK) ON AT.CODE = A.CODTIPPAC
  Where  convert(varchar(max),A.CODCENATE) IN (' + @CentroAtencion + ')  AND A.TIPOESTADO IN (1,3)
  AND  A.FECHAREPORTE>''2023-08-01'''

  exec sp_executesql @Sql 

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los pacientes con reportes de situaciones clínicas inesperadas o acciones inseguras registradas en el módulo de calidad (CALREPORTE), filtrando por uno o varios centros de atención y mostrando solo reportes activos o modificados desde agosto de 2023. Para cada paciente incluye su identificación (cédula/documento), nombre, grupo y tipo de población (maternas, menor de 5 años, adulto mayor, discapacitado, población general), fecha y descripción del evento, unidad funcional donde ocurrió, entidad aseguradora, número de ingreso, sexo, edad, tipo de reporte (acciones inseguras o situaciones clínicas inesperadas), clase del reporte (voluntario, búsqueda activa, identificado por terceros), acciones correctivas, y alertas de factores de riesgo y escalas clínicas del último ingreso activo. Se utiliza para la gestión y auditoría de eventos adversos y seguridad del paciente en los centros de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_CAL_ListarPacienteSituacionesClinicasInsesperadas';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_CAL_ListarPacienteSituacionesClinicasInsesperadas';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los reportes de situaciones clínicas inesperadas (acciones inseguras) de pacientes en estado Reportado o Modificado, posteriores al 2023-08-01, filtrados por centros de atención indicados.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CAL_ListarPacienteSituacionesClinicasInsesperadas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Se debe recibir una lista de códigos de centro de atención válida y formateada para cláusula IN (valores separados por coma); Las tablas maestras (INUNIFUNC, INENTIDAD, ADCENATEN, CALTIPOCLASE) deben tener los registros referenciados por los reportes; Debe existir la función dbo.Edad y Common.getdate disponibles', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CAL_ListarPacienteSituacionesClinicasInsesperadas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se listan reportes cuyo TIPOESTADO sea 1 (Reportado) o 3 (Modificado), excluyendo descartados y visados; Solo se listan reportes con FECHAREPORTE posterior al 2023-08-01; Solo considera tipos/clases con TIPOACCION = 2 en CALTIPOCLASE (situaciones clínicas inesperadas); El filtro por centros de atención se aplica obligatoriamente vía lista IN dinámica; La edad se calcula al momento actual usando Common.getdate() y dbo.Edad sobre la fecha de nacimiento; Las uniones a unidad funcional de reporte anónimo y usuario reportador son opcionales (LEFT JOIN), permitiendo registros sin estos datos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CAL_ListarPacienteSituacionesClinicasInsesperadas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Reporte de seguridad del paciente; Acciones inseguras; Situaciones clínicas inesperadas; Reporte anónimo; Centro de atención; Unidad funcional; Entidad (asegurador); Grupo poblacional; Tipo de identificación; Estado del reporte (Reportado/Descartado/Modificado/Visado); Clase de reporte (Voluntario/Búsqueda activa/Identificado por terceros); Edad del paciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CAL_ListarPacienteSituacionesClinicasInsesperadas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.CALREPORTE: Devuelve el conjunto de reportes cuando A.CODCENATE está en la lista de centros recibida, A.TIPOESTADO IN (1,3), A.FECHAREPORTE > ''2023-08-01'' y D.TIPOACCION = 2', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CAL_ListarPacienteSituacionesClinicasInsesperadas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si TIPORERPORTE = 1 → Se etiqueta como ''Acciones Inseguras'' else Si TIPORERPORTE = 2 se etiqueta como ''Situaciones clínicas inesperadas''; si CLASEREPORTE in (1,2,3,4) → Se traduce a ''Voluntario'', ''Busqueda activa'', ''Otros'' o ''Identificado por terceros'' respectivamente; si TIPOIDENTIFICA entre 1 y 8 → Se traduce al tipo de documento (Cédula ciudadanía, Cédula extranjera, Tarjeta Identidad, Registro civil, Pasaporte, Adulto sin ID, Menor sin ID, NU); si SEXO = 1 o 2 → Se traduce a ''Masculino'' o ''Femenino''; si REPORTEANONIMO = 1 o 2 → Se traduce a ''Si'' o ''No''; si TIPOESTADO in (1,2,3,4) → Se traduce a ''Reportado'', ''Descartado'', ''Modificado'' o ''Visado''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CAL_ListarPacienteSituacionesClinicasInsesperadas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.Edad; Common.getdate; sys.sp_executesql', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CAL_ListarPacienteSituacionesClinicasInsesperadas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.CALREPORTE; dbo.INUNIFUNC; dbo.INENTIDAD; dbo.ADCENATEN; dbo.CALTIPOCLASE; dbo.SEGusuaru; Admissions.TypesPopulationGroups', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CAL_ListarPacienteSituacionesClinicasInsesperadas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CAL_ListarPacienteSituacionesClinicasInsesperadas';
-- GO
