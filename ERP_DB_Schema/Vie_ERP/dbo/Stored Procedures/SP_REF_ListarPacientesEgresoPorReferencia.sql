-- =============================================
-- Author:		<Author,Juan David Patiño Cabrea,Name>
-- Create date: <Create Date,27-10-2018,>
-- Description:	<Description,Sp que me lista los pacientes de referencia con solicitud de remisión>
-- =============================================
CREATE PROCEDURE [dbo].[SP_REF_ListarPacientesEgresoPorReferencia]
(
  @CentroAtencion as varchar(Max)
)

AS
BEGIN
  SET NOCOUNT ON;
  
 declare @Sql as nvarchar(Max)

  Set @Sql = ' SELECT DISTINCT A.NUMINGRES,S.CODTIPPAC,Case S.CODTIPPAC when  1 then  ''Maternas'' when 2 then ''Menores de 5 Años'' when 3 then ''Adultos Mayores''  when 4 then ''Discapacitados'' when 5 then ''Poblacion General'' end as ''TipoPoblacion'',FECSOLICIT,G.IPCODPACI,RTRIM(I.IPNOMCOMP) as IPNOMCOMP,I.IPFECNACI,RTRIM(E.CODENTIDA) +''-''+RTRIM(E.NOMENTIDA) AS ''Entdidad'',RTRIM(UFUEGRHOS) +''-''+RTRIM(UFUDESCRI) AS ''UnidadFuncional'',S.FECREGCRE,S.FECHEGRESO,Rtrim(Z.CODCENATE) as ''Codigo Centro Atencion'',Rtrim(z.NOMCENATE) as ''Nombre Centro Atencion''
				FROM dbo.HCREFCONP  AS G  
						INNER JOIN dbo.INPACIENT AS I ON I.IPCODPACI = G.IPCODPACI 
						INNER JOIN dbo.ADINGRESO  AS S ON  S.NUMINGRES  = G.NUMINGRES 
						INNER JOIN dbo.CHREGEGRE  AS A ON A.NUMINGRES  = G.NUMINGRES  
                        INNER JOIN dbo.ADCENATEN  AS Z ON  Z.CODCENATE = g.CODCENATE  
						LEFT JOIN dbo.INENTIDAD as E ON E.CODENTIDA = S.CODENTIDA 
						INNER JOIN dbo.INUNIFUNC as R ON R.UFUCODIGO = UFUEGRHOS 
  Where  convert(varchar(500),g.CODCENATE) IN (' + @CentroAtencion + ') and  ESTPACEGR = 4 and G.ESTADO = 5 AND VISADO is null '
  
  exec sp_executesql @Sql 

END