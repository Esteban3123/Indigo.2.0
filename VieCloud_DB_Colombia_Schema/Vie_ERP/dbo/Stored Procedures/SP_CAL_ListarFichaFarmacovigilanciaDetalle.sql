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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que recupera el detalle de medicamentos asociados a una ficha de farmacovigilancia, dado su identificador. Compone información del reporte de evento adverso (CALREPORTE), el registro de farmacovigilancia (CALFARMACOVIGILANCIA) y el listado de medicamentos involucrados (CALFARMAMEDICAMEN), enriqueciendo cada medicamento con su nombre del catálogo de productos (IHLISTPRO), la unidad de medida (INUNIMEDI) y la vía de administración (HCVIAADMI). Devuelve para cada medicamento del reporte: clasificación SCI (Sospechoso/Concomitante/Interacción), nombre y código del producto, indicaciones, dosis, unidad de medida, vía de administración, frecuencia, y fechas de inicio y fin del tratamiento. Se usa para visualizar el detalle farmacológico completo de una ficha de farmacovigilancia o tecnovigilancia en el módulo de seguridad del paciente y gestión de eventos adversos a medicamentos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_CAL_ListarFichaFarmacovigilanciaDetalle';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_CAL_ListarFichaFarmacovigilanciaDetalle';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista el detalle de medicamentos asociados a una ficha de farmacovigilancia, incluyendo clasificación SCI, producto, dosis, unidad de medida, vía de administración y fechas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CAL_ListarFichaFarmacovigilanciaDetalle';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un reporte en CALREPORTE identificado por el id recibido; Debe existir registro en CALFARMACOVIGILANCIA vinculado al reporte; Los medicamentos en CALFARMAMEDICAMEN deben tener códigos válidos de unidad de medida (INUNIMEDI), vía de administración (HCVIAADMI) y producto (IHLISTPRO)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CAL_ListarFichaFarmacovigilanciaDetalle';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen medicamentos cuyo producto, unidad de medida y vía de administración existan en los catálogos (INNER JOIN); Las fechas de inicio y fin se entregan en formato dd/mm/yyyy (estilo 103); Los códigos se concatenan con su descripción separados por guion', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CAL_ListarFichaFarmacovigilanciaDetalle';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'farmacovigilancia; medicamento; unidad de medida; vía de administración; dosis; frecuencia; indicaciones; clasificación SCI', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CAL_ListarFichaFarmacovigilanciaDetalle';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Cuando C.ID = @IdFicha, retorna las filas de medicamentos de la ficha con catálogos resueltos (producto, unidad de medida, vía de administración)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CAL_ListarFichaFarmacovigilanciaDetalle';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si SCI = 1 → Se muestra como ''S'' (Serio); si SCI = 2 → Se muestra como ''C''; si SCI = 3 → Se muestra como ''I''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CAL_ListarFichaFarmacovigilanciaDetalle';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.CALREPORTE; dbo.CALFARMACOVIGILANCIA; dbo.CALFARMAMEDICAMEN; dbo.INUNIMEDI; dbo.HCVIAADMI; dbo.IHLISTPRO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CAL_ListarFichaFarmacovigilanciaDetalle';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CAL_ListarFichaFarmacovigilanciaDetalle';
-- GO
