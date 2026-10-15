
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SPCH_ListarInventarioFisicoPacienteCompletosSobrantes]
@Paciente varchar(25),
@Ingreso char(20),
@CentroAtencion char(10),
@UnidadFuncional char(10)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;


	SELECT 'Completos' as Tipo, A.CODPRODUC, RTRIM(B.DESPRODUC) AS DESPRODUC,A.CANACTPRO ,RTRIM(isnull(DOSPROACU,0)) +' - '+ RTRIM(isnull(C.ABRUNIMED,'')) AS Sobrante,isnull(TOTHORACU,0) AS Horas
FROM  dbo.HCFISIPRO A INNER JOIN  dbo.IHLISTPRO B ON A.CODPRODUC=B.CODPRODUC
LEFT JOIN  dbo.INUNIMEDI C ON A.CODUNIMED=c.CODUNIMED
WHERE ( A.IPCODPACI=@Paciente AND A.NUMINGRES=@Ingreso AND CODCENATE=@CentroAtencion AND UFUCODIGO=@UnidadFuncional AND A.CANACTPRO >0)
or (A.IPCODPACI=@Paciente AND A.NUMINGRES=@Ingreso AND CODCENATE=@CentroAtencion AND UFUCODIGO=@UnidadFuncional AND 
		B.RETRASOGE=1 AND DOSPROACU>0 AND TOTHORACU<=B.TIEESTMED)

--SELECT 'Completos' as Tipo, A.CODPRODUC, RTRIM(B.DESPRODUC) AS DESPRODUC,A.CANACTPRO ,'0' AS Sobrante,0 AS Horas
--FROM  dbo.HCFISIPRO A INNER JOIN  dbo.IHLISTPRO B ON A.CODPRODUC=B.CODPRODUC 
--WHERE A.IPCODPACI=@Paciente AND A.NUMINGRES=@Ingreso AND CODCENATE=@CentroAtencion AND UFUCODIGO=@UnidadFuncional AND A.CANACTPRO >'0'
--UNION 
--SELECT 'Sobrantes' as Tipo, A.CODPRODUC, RTRIM(B.DESPRODUC) AS DESPRODUC,0,RTRIM(DOSPROACU) +' - '+ RTRIM(C.ABRUNIMED)  AS Sobrante,TOTHORACU As Horas
--FROM  dbo.HCFISIPRO A INNER JOIN  dbo.IHLISTPRO B ON A.CODPRODUC=B.CODPRODUC INNER JOIN  dbo.INUNIMEDI C ON A.CODUNIMED=c.CODUNIMED
--WHERE B.RETRASOGE=1 AND DOSPROACU>0 AND TOTHORACU<=B.TIEESTMED AND A.IPCODPACI=@Paciente AND A.NUMINGRES=@ingreso AND CODCENATE=@CentroAtencion  AND UFUCODIGO=@UnidadFuncional 

END