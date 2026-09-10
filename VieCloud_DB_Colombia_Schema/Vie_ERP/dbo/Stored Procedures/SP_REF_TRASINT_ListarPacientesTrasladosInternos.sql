-- =============================================
-- Author:		<Author,Juan David Patiño Cabrea,Name>
-- Create date: <Create Date,09-10-2019,>
-- Description:	<Description,Sp que me lista los pacientes de referencia con solicitud de remisión>
-- =============================================
CREATE PROCEDURE [dbo].[SP_REF_TRASINT_ListarPacientesTrasladosInternos]
(
  @CentroAtencion as varchar(MAX)
)

AS
BEGIN
  SET NOCOUNT ON;
  
  declare @Sql as nvarchar(MAX)

  Set @Sql = ' Select A.ID,I.CODTIPPAC as ''Grupo Poblacion'',Case I.CODTIPPAC when 1 then ''Maternas'' when 2 then ''Menor de 5 Años'' when 3 then ''Adulto Mayor'' when 4 then ''Discapacitado'' when 5 then ''Población General'' end As ''Tipo Poblacion'', x.FECHATRASLADO as ''Fecha Traslado'', Rtrim(A.IPCODPACI) As ''Paciente'',Rtrim(inpa.IPNOMCOMP) as ''Nombre Paciente'', Rtrim(A.NUMINGRES) as ''Ingreso'', Rtrim(B.NOMCENATE) as ''CA Origen'',Rtrim(C.UFUDESCRI) AS ''UF Origen'',Rtrim(D.NOMCENATE)as ''CA Destino'', Rtrim(E.UFUDESCRI) as ''UF Destino'', Rtrim(F.DESMOTANU)  as ''Motivo'', Rtrim(x.OBSERVACIONTRASLADO) AS ''Observacion'', CASE A.ESTADO WHEN 1 then ''1. Solicitado'' when 2 then ''2. Ambulancia Asignada'' when 3 then ''3. Traslado Iniciado'' when 4 then ''4. Traslado Completado'' when 5 then ''5. Anulado'' when 6 then ''6. Cancelado'' end as ''Estado'',
		Rtrim(NOMENTIDA) As ''Nombre Entidad'',IPFECNACI,A.NOVEDAD,Rtrim(g.NOMUSUARI) as ''Usuario solicita'',
		Rtrim(A.CODCENATEOR) as ''Codigo Centro Atencion Origen'', Rtrim(A.UFUCODIGOOR) as ''Codigo Unidad Funcional Origen'', Rtrim(A.CODCENATEDE) as ''Codigo Centro Atencion destino'', Rtrim(a.UFUCODIGODE) as ''Codigo Unidad Funcional Destino'', A.ESTADO AS ''Codigo Estado Registro''
		From [dbo].[HCSOLTRASLADOSINT] a 
			INNER JOIN HCTRASLADOS  x with(nolock) ON A.ID = x.IDHCSOLTRASLADOSINT AND x.ESTADO = 1 --EN PROCESO
			INNER JOIN INPACIENT  inpa with(nolock) ON inpa.IPCODPACI = A.IPCODPACI
			INNER JOIN INENTIDAD inen with(nolock) ON inen.CODENTIDA =  inpa.CODENTIDA  
			INNER JOIN ADCENATEN B  with(nolock)ON  A.CODCENATEOR = B.CODCENATE 
			INNER JOIN INUNIFUNC C with(nolock) ON  x.UFUCODIGOORTRASLADO = C.UFUCODIGO 
			INNER JOIN ADCENATEN D with(nolock) ON  x.CODCENATEDETRASLADO = D.CODCENATE 
			INNER JOIN INUNIFUNC E with(nolock) ON  x.UFUCODIGODETRASLADO = E.UFUCODIGO 
			INNER JOIN HCMOANULB F with(nolock) ON  A.IDHCMOANULB = F.CODMOTANU  
			INNER JOIN SEGusuaru g with(nolock) ON  A.USUARIOSOLICITA = g.CODUSUARI 
			INNER JOIN ADINGRESO I with(nolock) ON  A.NUMINGRES  = I.NUMINGRES  
		WHERE  convert(varchar(MAX),A.CODCENATEOR) IN (' + @CentroAtencion + ') and A.ESTADO IN (3,4) ' --- 3. Trslado Iniciado y 4.Traslado Completado

		 
--  PRINT @SQL
  exec sp_executesql @Sql 

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los pacientes con traslados internos en curso o completados dentro de la institución, filtrando por uno o varios centros de atención de origen. Combina información de la solicitud de traslado, el traslado efectivo, datos del paciente (cédula, nombre, fecha de nacimiento, grupo poblacional), entidad aseguradora, unidades funcionales y centros de atención de origen y destino, el motivo del traslado y el usuario que realizó la solicitud. Se utiliza para seguimiento y control operativo de traslados internos entre unidades funcionales o sedes, mostrando únicamente los registros en estado ''Traslado Iniciado'' (3) o ''Traslado Completado'' (4). El filtro de centros de atención se construye dinámicamente en tiempo de ejecución mediante SQL dinámico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_REF_TRASINT_ListarPacientesTrasladosInternos';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_REF_TRASINT_ListarPacientesTrasladosInternos';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las solicitudes de traslado interno de pacientes (referencia/remisión) cuyo traslado está en proceso y cuyo estado es Iniciado o Completado, filtradas por uno o varios centros de atención de origen.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REF_TRASINT_ListarPacientesTrasladosInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Se debe proveer una lista de códigos de centro de atención formateada para insertarse en una cláusula IN (valores separados por coma y entrecomillados según corresponda); Deben existir registros en HCTRASLADOS asociados a la solicitud con estado 1 (en proceso); El paciente, ingreso, entidad, centros y unidades funcionales referenciados deben existir en sus tablas maestras', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REF_TRASINT_ListarPacientesTrasladosInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo retorna solicitudes cuyo traslado asociado en HCTRASLADOS esté en estado 1 (en proceso); Solo retorna solicitudes con estado 3 (Traslado Iniciado) o 4 (Traslado Completado); Restringe los resultados a los centros de atención de origen recibidos en el filtro; Requiere existencia de paciente, entidad, centros origen/destino, unidades funcionales origen/destino, motivo de anulación, usuario solicitante e ingreso (INNER JOIN); No realiza modificaciones de datos; es solo de consulta', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REF_TRASINT_ListarPacientesTrasladosInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'paciente; traslado interno; referencia/remisión; centro de atención; unidad funcional; ingreso hospitalario; grupo poblacional (Maternas, Menor de 5 Años, Adulto Mayor, Discapacitado, Población General); entidad responsable de pago; motivo de anulación; ambulancia', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REF_TRASINT_ListarPacientesTrasladosInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Cuando HCTRASLADOS.ESTADO=1 y HCSOLTRASLADOSINT.ESTADO IN (3,4) y CODCENATEOR está en la lista recibida, retorna una fila por solicitud con datos de paciente, ingreso, centros y unidades funcionales origen/destino, motivo, observación, usuario solicitante y etiquetas de grupo poblacional y estado', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REF_TRASINT_ListarPacientesTrasladosInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si CODTIPPAC del ingreso (1..5) → Etiqueta el grupo poblacional como Maternas, Menor de 5 Años, Adulto Mayor, Discapacitado o Población General; si ESTADO de la solicitud de traslado (1..6) → Traduce a etiqueta: 1.Solicitado, 2.Ambulancia Asignada, 3.Traslado Iniciado, 4.Traslado Completado, 5.Anulado, 6.Cancelado; si A.ESTADO IN (3,4) → Solo se incluyen solicitudes con traslado iniciado o completado; si x.ESTADO = 1 en HCTRASLADOS → Solo se considera el traslado que esté ''EN PROCESO''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REF_TRASINT_ListarPacientesTrasladosInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCSOLTRASLADOSINT; dbo.HCTRASLADOS; dbo.INPACIENT; dbo.INENTIDAD; dbo.ADCENATEN; dbo.INUNIFUNC; dbo.HCMOANULB; dbo.SEGusuaru; dbo.ADINGRESO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REF_TRASINT_ListarPacientesTrasladosInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REF_TRASINT_ListarPacientesTrasladosInternos';
-- GO
