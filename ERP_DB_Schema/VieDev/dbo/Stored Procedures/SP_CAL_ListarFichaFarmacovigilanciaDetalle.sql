-- =============================================
-- Author:		<Author,Juan David Patiño Cabrea,Name>
-- Create date: <Create Date,09-07-2018,>
-- Description:	<Description,Sp que me lista la Información de las Fichas de tecnovigilancia>
-- =============================================
CREATE PROCEDURE [dbo].[SP_CAL_ListarFichaFarmacovigilanciaDetalle]
(
  @IdFicha as Int
)

AS
BEGIN
  SET NOCOUNT ON;

select 
CASE SCI WHEN 1 THEN 'S' WHEN 2 THEN 'C' WHEN 3 THEN 'I' END AS SCI
,Rtrim(M.CODPRODUC) +'-'+ Rtrim(MED.DESPRODUC) AS CODPRODUC
,INDICACIONES 
,DOSIS 
,rtrim(UM.CODUNIMED) + ' - ' + rtrim(UM.DESUNIMED) as 'UnidadMedida' 
,rtrim(VIA.CODVIAADM) + ' - ' + rtrim(VIA.DESVIAADM) as 'ViaAdministracion'
,FRECUENCIA 
,convert(varchar(20),FECHAINICIO,103) as FECHAINICIO
,convert(varchar(20),FECHAFIN,103) as FECHAFIN 
from dbo.CALREPORTE C inner join
dbo.CALFARMACOVIGILANCIA  A on A.IDCALREPORTE = C.ID inner join
dbo.CALFARMAMEDICAMEN  M on M.IDFARMACOVIGILANCIA  = A.ID inner join
dbo.INUNIMEDI UM on UM.CODUNIMED = M.CODUNIMED inner join 
dbo.HCVIAADMI VIA on VIA.CODVIAADM = M.CODVIAADM inner join 
dbo.IHLISTPRO MED on MED.CODPRODUC = M.CODPRODUC
where C.ID = @IdFicha 
	
--select * from [dbo].[CALFARMACOVIGILANCIA]
--select * from [dbo].[CALREPORTE] where id =32
--select * from [dbo].[CALFARMAMEDICAMEN] 
--SELECT * FROM IHLISTPRO 

END