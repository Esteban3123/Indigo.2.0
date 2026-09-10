CREATE PROCEDURE [dbo].[SP_CAL_ListarPacientesAccionesInseguras]
(
  @CentroAtencion as varchar(max)   
)

AS
BEGIN
  SET NOCOUNT ON;

  declare @Sql as nvarchar(max)
  
 set @Sql = ' Select A.ID,z.CODTIPPAC as ''GrupoPoblacion'',AT.Name as ''TipoPoblacion'',FECHAOCURRENC AS ''Fecha Ocurrencia'',Rtrim(B.UFUDESCRI) AS ''Unidad Funcional Ocurrencia'',A.IPCODPACI AS ''Identificacion'',Rtrim(A.NOMBRECOMP) As ''Nombre paciente'',Rtrim(C.NOMENTIDA) As ''Nombre Entidad'',
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
			  left join Admissions.TypesPopulationGroups AT WITH(NOLOCK) ON AT.CODE = Z.CODTIPPAC

  Where  convert(varchar(max),A.CODCENATE) IN (' + @CentroAtencion + ') AND A.TIPOESTADO IN (1,3)  AND A.FECHAREPORTE> ''2023-01-08'''
 
  exec sp_executesql @Sql 
  
  
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los pacientes con reportes de acciones inseguras y situaciones clínicas inesperadas registradas en el sistema de calidad, filtrados por centro de atención. Para cada reporte entrega datos del paciente (cédula, nombre, identificación, sexo, edad, dirección, teléfono), información del ingreso (número de ingreso, unidad funcional, cama), clasificación del evento (acción insegura o situación inesperada, nivel de daño, tipo de vigilancia como farmacovigilancia o tecnovigilancia), fechas de ocurrencia y reporte, entidad aseguradora, clase de reporte (voluntario, búsqueda activa, anónimo) y alertas de factores de riesgo y escalas clínicas. Está orientado a los equipos de seguridad del paciente y calidad asistencial para el seguimiento y análisis de eventos adversos e incidentes reportados desde el 8 de enero de 2023 en estado reportado o modificado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_CAL_ListarPacientesAccionesInseguras';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_CAL_ListarPacientesAccionesInseguras';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Listar los reportes de acciones inseguras (eventos adversos e incidentes) de pacientes, filtrados por centros de atención, en estado Reportado o Modificado y posteriores al 8/01/2023, con sus datos demográficos, clínicos y de clasificación.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CAL_ListarPacientesAccionesInseguras';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El parámetro de centros de atención debe entregarse como lista de valores válida y ya saneada para concatenarse en una cláusula IN (riesgo de SQL injection); Deben existir relaciones consistentes entre el reporte y su ingreso, unidad funcional, entidad, centro de atención y tipo/clase con TIPOACCION=1; La función dbo.Edad y Common.getdate deben estar disponibles', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CAL_ListarPacientesAccionesInseguras';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen reportes con TIPOESTADO en (1,3), es decir Reportado o Modificado; nunca Descartados ni Visados; Solo se consideran reportes con FECHAREPORTE posterior al 2023-01-08; Solo se listan registros cuyo tipo/clase pertenezca a TIPOACCION = 1 en CALTIPOCLASE (acciones inseguras); El filtro por centro de atención es obligatorio (lista de códigos inyectada en el IN); La edad del paciente se calcula con dbo.Edad sobre la fecha actual del sistema (Common.getdate)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CAL_ListarPacientesAccionesInseguras';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Acciones inseguras; Eventos adversos; Incidentes; Situaciones clínicas inesperadas; Nivel de daño; Reporte anónimo; Centro de atención; Unidad funcional; Grupo poblacional; Tecnovigilancia; Farmacovigilancia; Hemovigilancia; Reactivovigilancia; IAAS (Infecciones asociadas a la atención en salud); Clasificación del paciente; Cama de hospitalización; Ingreso del paciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CAL_ListarPacientesAccionesInseguras';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.CALREPORTE: Devuelve un resultset dinámico con los reportes cuando A.CODCENATE IN (lista) AND A.TIPOESTADO IN (1,3) AND A.FECHAREPORTE > ''2023-01-08'' y D.TIPOACCION = 1', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CAL_ListarPacientesAccionesInseguras';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si TIPORERPORTE = 1 → Se etiqueta como ''Acciones Inseguras'' else Si =2 se etiqueta ''Situaciones clínicas inesperadas''; si CLASEREPORTE in (1,2,3,4) → Se traduce a Voluntario / Búsqueda activa / Otros / Identificado por terceros respectivamente; si CLASIFICACION = 1 → Se clasifica como ''Evento adverso'' else Si =2 se clasifica como ''Incidente''; si TIPOESTADO = 1/2/3/4 → Se traduce a Reportado / Descartado / Modificado / Visado; si CONTROLVIGILA in (1..7) → Se traduce al programa de vigilancia: Tecnovigilancia, Farmacovigilancia, Hemovigilancia, Reactivovigilancia, IAAS-Dispositivos, IAAS-Infección de localización quirúrgica, Ninguno; si REPORTEANONIMO = 1 → Marca el reporte como anónimo (''Si'') else Si =2 indica ''No''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CAL_ListarPacientesAccionesInseguras';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.CALREPORTE; dbo.ADINGRESO; dbo.INUNIFUNC; dbo.INENTIDAD; dbo.ADCENATEN; dbo.CALTIPOCLASE; dbo.CHCAMASHO; dbo.CALNIVELDANO; dbo.SEGusuaru; Admissions.TypesPopulationGroups', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CAL_ListarPacientesAccionesInseguras';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CAL_ListarPacientesAccionesInseguras';
-- GO
