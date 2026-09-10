-- =============================================
-- Author:		<Author,Juan David Patiño Cabrea,Name>
-- Create date: <Create Date,09-10-2019,>
-- Description:	<Description,Sp que me lista los pacientes que tienen solicitudes de traslados internos>
-- =============================================
CREATE PROCEDURE [dbo].[SP_REF_TRASINT_ListarPacientesSolicitudTrasladosInternos]
(
  @CentroAtencion as varchar(MAX)
)

AS
BEGIN
  SET NOCOUNT ON;
  
 declare @Sql as nvarchar(MAX)

  Set @Sql = ' Select A.ID,I.CODTIPPAC as ''Grupo Poblacion'',Case I.CODTIPPAC when 1 then ''Maternas'' when 2 then ''Menor de 5 Años'' when 3 then ''Adulto Mayor'' when 4 then ''Discapacitado'' when 5 then ''Población General'' end As ''Tipo Poblacion'', A.FECHAREGISTRO As ''Fecha Solicitud'', Rtrim(A.IPCODPACI) As ''Paciente'',Rtrim(inpa.IPNOMCOMP) as ''Nombre Paciente'', Rtrim(A.NUMINGRES) as ''Ingreso'', Rtrim(B.NOMCENATE) as ''CA Origen'',Rtrim(C.UFUDESCRI) AS ''UF Origen'',Rtrim(D.NOMCENATE)as ''CA Destino'', Rtrim(E.UFUDESCRI) as ''UF Destino'', Rtrim(F.DESMOTANU)  as ''Motivo'', Rtrim(A.OBSERVACION) AS ''Observacion'', CASE A.ESTADO WHEN 1 then ''1. Solicitado'' when 2 then ''2. Ambulancia Asignada'' when 3 then ''3. Traslado Iniciado'' when 4 then ''4. Traslado Completado'' when 5 then ''5. Anulado'' when 6 then ''6. Cancelado'' end as ''Estado'',
		Rtrim(NOMENTIDA) As ''Nombre Entidad'',IPFECNACI,A.NOVEDAD,Rtrim(g.NOMUSUARI) as ''Usuario solicita'',
		Rtrim(A.CODCENATEOR) as ''Codigo Centro Atencion Origen'', Rtrim(A.UFUCODIGOOR) as ''Codigo Unidad Funcional Origen'', Rtrim(A.CODCENATEDE) as ''Codigo Centro Atencion destino'', Rtrim(a.UFUCODIGODE) as ''Codigo Unidad Funcional Destino'', A.ESTADO AS ''Codigo Estado Registro'',
		x.IDRCVEHICTRAS AS ''Id Ambulancia''
		From [dbo].[HCSOLTRASLADOSINT] a 
			LEFT JOIN HCTRASLADOS  x with(nolock) ON A.ID = x.IDHCSOLTRASLADOSINT AND x.ESTADO = 1 --EN PROCESO
			INNER JOIN INPACIENT  inpa with(nolock) ON inpa.IPCODPACI = A.IPCODPACI
			INNER JOIN INENTIDAD inen with(nolock) ON inen.CODENTIDA =  inpa.CODENTIDA  
			INNER JOIN ADCENATEN B  with(nolock)ON  A.CODCENATEOR = B.CODCENATE 
			INNER JOIN INUNIFUNC C with(nolock) ON  A.UFUCODIGOOR = C.UFUCODIGO 
			INNER JOIN ADCENATEN D with(nolock) ON  A.CODCENATEDE = D.CODCENATE 
			INNER JOIN INUNIFUNC E with(nolock) ON  A.UFUCODIGODE = E.UFUCODIGO 
			INNER JOIN HCMOANULB F with(nolock) ON  A.IDHCMOANULB = F.CODMOTANU  
			INNER JOIN SEGusuaru g with(nolock) ON  A.USUARIOSOLICITA = g.CODUSUARI 
			INNER JOIN ADINGRESO I with(nolock) ON  A.NUMINGRES  = I.NUMINGRES 
		WHERE  convert(varchar(MAX),A.CODCENATEOR) IN (' + @CentroAtencion + ') and A.ESTADO IN (1,2) ' --- 1.Solicitados y 2.Ambulancia asignada

		 
--  PRINT @SQL
  exec sp_executesql @Sql 

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los pacientes que tienen solicitudes de traslados internos pendientes (en estado ''Solicitado'' o ''Ambulancia Asignada'') para uno o varios centros de atención de origen. Consolida información del paciente (cédula, nombre, fecha de nacimiento, grupo poblacional, entidad aseguradora), del ingreso hospitalario, de las unidades funcionales y centros de atención de origen y destino, del motivo del traslado, del usuario que generó la solicitud y de la ambulancia asignada si aplica. Se usa en el módulo de referencia y traslados internos para monitorear y gestionar en tiempo real los traslados entre servicios o sedes dentro de la institución. Construye la consulta de forma dinámica para filtrar por uno o múltiples centros de atención recibidos como parámetro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_REF_TRASINT_ListarPacientesSolicitudTrasladosInternos';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_REF_TRASINT_ListarPacientesSolicitudTrasladosInternos';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Listar las solicitudes de traslado interno vigentes (solicitadas o con ambulancia asignada) de los centros de atención indicados, con datos del paciente, origen, destino, motivo, estado y ambulancia asociada.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REF_TRASINT_ListarPacientesSolicitudTrasladosInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El parámetro de centros de atención debe venir como lista válida de valores SQL separados por coma para concatenarse en el IN (...); Deben existir maestros consistentes (paciente, entidad, centros, unidades funcionales, motivo de anulación, usuario, ingreso) referenciados por la solicitud', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REF_TRASINT_ListarPacientesSolicitudTrasladosInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se listan solicitudes cuyo centro de atención de origen pertenezca a la lista recibida; Solo se listan solicitudes activas en estado 1 (Solicitado) o 2 (Ambulancia Asignada); Cada solicitud requiere paciente, entidad, centros de atención origen/destino, unidades funcionales origen/destino, motivo, usuario solicitante e ingreso existentes (INNER JOIN); La ambulancia asociada solo se muestra si el traslado está en estado 1 (en proceso)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REF_TRASINT_ListarPacientesSolicitudTrasladosInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Solicitud de traslado interno; Paciente; Ingreso hospitalario; Centro de atención origen y destino; Unidad funcional origen y destino; Grupo poblacional (materna, menor de 5, adulto mayor, discapacitado, población general); Ambulancia / traslado en proceso; Motivo de anulación; Entidad del paciente; Usuario que solicita', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REF_TRASINT_ListarPacientesSolicitudTrasladosInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCSOLTRASLADOSINT: Cuando A.CODCENATEOR está en la lista de centros recibida y A.ESTADO IN (1,2), se devuelve la solicitud enriquecida con paciente, entidad, centros y unidades funcionales origen/destino, motivo, usuario y ambulancia (si HCTRASLADOS.ESTADO=1)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REF_TRASINT_ListarPacientesSolicitudTrasladosInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si CODTIPPAC del ingreso (1..5) → Etiqueta el grupo poblacional como Maternas, Menor de 5 Años, Adulto Mayor, Discapacitado o Población General; si ESTADO de la solicitud (1..6) → Traduce a etiqueta: 1.Solicitado, 2.Ambulancia Asignada, 3.Traslado Iniciado, 4.Traslado Completado, 5.Anulado, 6.Cancelado; si A.ESTADO IN (1,2) → Solo se incluyen solicitudes en estado Solicitado o con Ambulancia Asignada else Se excluyen del resultado; si HCTRASLADOS.ESTADO = 1 (en proceso) → Se vincula la ambulancia/traslado en proceso a la solicitud (LEFT JOIN) else Se devuelve sin ambulancia asociada', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REF_TRASINT_ListarPacientesSolicitudTrasladosInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCSOLTRASLADOSINT; dbo.HCTRASLADOS; dbo.INPACIENT; dbo.INENTIDAD; dbo.ADCENATEN; dbo.INUNIFUNC; dbo.HCMOANULB; dbo.SEGusuaru; dbo.ADINGRESO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REF_TRASINT_ListarPacientesSolicitudTrasladosInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REF_TRASINT_ListarPacientesSolicitudTrasladosInternos';
-- GO
