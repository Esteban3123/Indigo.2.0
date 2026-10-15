-- =============================================
-- Author:		<Author,Juan David Patiño Cabrea,Name>
-- Create date: <Create Date,27-10-2018,>
-- Description:	<Description,Sp que me lista los pacientes de referencia con solicitud de remisión>
-- =============================================
CREATE PROCEDURE [dbo].[SP_REF_ListarPacientesSolicitudRemision]
(
  @CentroAtencion as varchar(MAX)
)

AS
BEGIN
  SET NOCOUNT ON;
  
 declare @Sql as nvarchar(MAX)

  Set @Sql = ' Select RequestType, NOVEDAD,FECSOLICIT,HCREF.AUTO,HCREF.IPCODPACI,RTRIM(IPNOMCOMP)AS IPNOMCOMP,IPFECNACI,HCREF.UFUCODIGO,RTRIM(UFUDESCRI) AS CodDesUnidadFuncio,NUMINGRES,inen.CODENTIDA,RTRIM(NOMENTIDA) AS CodNomEntida,HCREF.CODPROSAL,RTRIM(NOMMEDICO) AS CodNomProf,espec.CODESPECI,RTRIM(DESESPECI) AS CodDesEspeci,SERVIDOREM, CASE WHEN HCREF.ESTADO = 1 THEN ''1. Solicitado sin definir pertinencia''	  WHEN HCREF.ESTADO = 2 THEN ''3. Gestionando''	  WHEN HCREF.ESTADO = 3 THEN ''4. Aceptado con pendiente de salida''	  WHEN HCREF.ESTADO = 6 THEN ''2. Solicitado con pertinencia médica'' END AS CASE_ESTADO,HCREF.ESTADO,FECHCONFIR,Rtrim(Z.CODCENATE) as ''Codigo Centro Atencion'',Rtrim(z.NOMCENATE) as ''Nombre Centro Atencion'', dbo.[RiskFactorAlert](HCREF.IPCODPACI, HCREF.NUMINGRES, 1) AS ''AlertaFactoresRiesgo'',
		  dbo.[RiskFactorAlert](HCREF.IPCODPACI, HCREF.NUMINGRES, 2) AS ''AlertaEscalas''
			  FROM dbo.HCREFCONP AS HCREF  
					 INNER JOIN dbo.INUNIFUNC AS unif ON unif.UFUCODIGO = HCREF.UFUCODIGO and unif.UFUTIPUNI <> 15 
					 INNER JOIN dbo.INPACIENT AS inpa  ON inpa.IPCODPACI = HCREF.IPCODPACI 
					 INNER JOIN dbo.INENTIDAD AS inen ON inen.CODENTIDA =  inpa.CODENTIDA 
					 INNER JOIN dbo.INPROFSAL  AS inprof ON  inprof.CODPROSAL = HCREF.CODPROSAL 
					 INNER JOIN dbo.ADCENATEN  AS Z ON  Z.CODCENATE = HCREF.CODCENATE  
					 LEFT JOIN  dbo.INESPECIA  AS espec ON espec.CODESPECI =  HCREF.CODESPECI
					 LEFT JOIN  dbo.HCREFCONTD as d ON d.ID = (select top 1 ID from dbo.HCREFCONTD where HCREFCONPID = HCREF.AUTO AND FECHCONFIR IS NOT NULL)
  Where  convert(varchar(500),HCREF.CODCENATE) IN (' + @CentroAtencion + ')  AND HCREF.ESTADO <> 4 AND HCREF.ESTADO <> 5 AND (HCREF.EXTRAMURAL IS NULL OR  HCREF.EXTRAMURAL = 0 )'
--  PRINT @SQL
  exec sp_executesql @Sql 

END