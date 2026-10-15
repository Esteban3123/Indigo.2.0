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