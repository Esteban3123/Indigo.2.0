-- =============================================
-- Author:		<Author,Juan David Patiño Cabrea,Name>
-- Create date: <Create Date,27-10-2018,>
-- Description:	<Description,Sp que me lista los pacientes de referencia con solicitud de remisión>
-- =============================================
CREATE PROCEDURE [dbo].[SP_REF_ListarPacientesSolicitudesSuspendidas]
(
  @CentroAtencion as varchar(max)
)

AS
BEGIN
  SET NOCOUNT ON;
  
 declare @Sql as nvarchar(max)

  Set @Sql = ' SELECT 
					S.CODTIPPAC as Tipo , 
					G.AUTO,	
					G.FECSOLICIT as ''Fecha Solicitud'', 
					G.IPCODPACI as Identificacion, 
					RTRIM(I.IPNOMCOMP) as ''Nombre Paciente'', 
					IPFECNACI, 
					RTRIM(E.CODENTIDA) + ''-'' + RTRIM(E.NOMENTIDA) AS Entdidad, 
					A.NUMINGRES as ingreso, 
					RTRIM(UFUDESCRI) AS UnidadFuncional, 
					S.FECREGCRE, 
					S.FECHEGRESO , 
					Rtrim(Z.CODCENATE) as ''Codigo Centro Atencion'', 
					Rtrim(z.NOMCENATE) as ''Nombre Centro Atencion'', 
					G.ESTADO  
			   FROM dbo.HCREFCONP  AS G  
					INNER JOIN dbo.INPACIENT AS I ON I.IPCODPACI = G.IPCODPACI 
					INNER JOIN dbo.ADINGRESO  AS S ON  S.NUMINGRES = G.NUMINGRES
					LEFT JOIN dbo.CHREGEGRE  AS A ON A.NUMINGRES  = S.NUMINGRES 
					INNER JOIN dbo.INENTIDAD as E ON E.CODENTIDA =s.CODENTIDA 
					INNER JOIN dbo.ADCENATEN  AS Z ON  Z.CODCENATE = g.CODCENATE
					LEFT JOIN dbo.INUNIFUNC as R ON R.UFUCODIGO = UFUEGRHOS
			   WHERE  convert(varchar(500),g.CODCENATE) IN (' + @CentroAtencion + ') 
					AND ( A.ESTPACEGR <> 4 OR G.ESTADO = 4  ) 
					and G.VISADOSUSPE is null '
  print @sql
  exec sp_executesql @Sql 

END