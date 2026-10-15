-- =============================================
-- Author:		<Author,Juan David Patiño Cabrea,Name>
-- Create date: <Create Date,17-10-2019,>
-- Description:	<Description,Sp que me lista los pacientes de referencia con solicitud de remisión>
-- =============================================
CREATE PROCEDURE [dbo].[SP_REF_TRASINT_ListarPacientesConSolicitudesSuspendidas]
(
  @CentroAtencion as varchar(MAX)
)

AS
BEGIN
  SET NOCOUNT ON;
  
  declare @Sql as nvarchar(MAX)


  Set @Sql = ' Select A.ID,I.CODTIPPAC as ''Grupo Poblacion'',Case I.CODTIPPAC when 1 then ''Maternas'' when 2 then ''Menor de 5 Años'' when 3 then ''Adulto Mayor'' when 4 then ''Discapacitado'' when 5 then ''Población General'' end As ''Tipo Poblacion'', a.FECHAREGISTRO as ''Fecha Solicitud'', Rtrim(A.IPCODPACI) As ''Paciente'',Rtrim(inpa.IPNOMCOMP) as ''Nombre Paciente'', Rtrim(A.NUMINGRES) as ''Ingreso'', Rtrim(B.NOMCENATE) as ''CA Origen'',  
		Rtrim(NOMENTIDA) As ''Nombre Entidad'',IPFECNACI
		From [dbo].[HCSOLTRASLADOSINT] a 
			--INNER JOIN HCTRASLADOS  x with(nolock) ON A.ID = x.IDHCSOLTRASLADOSINT
			INNER JOIN INPACIENT  inpa with(nolock) ON inpa.IPCODPACI = A.IPCODPACI
			INNER JOIN ADINGRESO I with(nolock) ON  A.NUMINGRES  = I.NUMINGRES 
			INNER JOIN INENTIDAD inen with(nolock) ON inen.CODENTIDA =  I.CODENTIDA  
			INNER JOIN ADCENATEN B  with(nolock)ON  A.CODCENATEOR = B.CODCENATE 
		WHERE  convert(varchar(MAX),A.CODCENATEOR) IN (' + @CentroAtencion + ') and A.ESTADO IN (5) ' --- 5 - Cancelado

		 
--  PRINT @SQL
  exec sp_executesql @Sql 

END