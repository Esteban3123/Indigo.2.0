
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

	SELECT 'Completos' as Tipo, A.CODPRODUC, RTRIM(B.DESPRODUC) AS DESPRODUC,A.CANACTPRO ,RTRIM(isnull(DOSPROACU,0)) +' - '+ RTRIM(isnull(C.ABRUNIMED,'')) AS Sobrante,isnull(TOTHORACU,0) AS Horas, RTRIM(isnull(DOSPROACU,0)) As DOSPROACU
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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista el inventario físico de medicamentos e insumos de un paciente hospitalizado, combinando en un solo resultado los productos ''completos'' (con cantidad actual disponible mayor a cero) y los productos ''sobrantes'' (medicamentos de liberación retardada que aún tienen dosis acumuladas pendientes dentro del tiempo estimado de estabilidad). Recibe como parámetros la cédula del paciente, el número de ingreso, el centro de atención y la unidad funcional. Cruza el inventario físico del paciente (HCFISIPRO) con el catálogo maestro de productos farmacéuticos (IHLISTPRO) para obtener el nombre y características del medicamento, y con la tabla de unidades de medida (INUNIMEDI) para mostrar la abreviatura de la unidad correspondiente a la dosis sobrante acumulada. Se usa en el proceso de gestión de medicación hospitalaria para identificar qué productos quedan disponibles o pendientes de administrar al paciente en su ingreso actual.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPCH_ListarInventarioFisicoPacienteCompletosSobrantes';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPCH_ListarInventarioFisicoPacienteCompletosSobrantes';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los productos del inventario físico de un paciente en un ingreso, centro y unidad funcional, considerando completos (con cantidad activa) y sobrantes acumulados de productos con retraso permitido dentro del tiempo estimado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarInventarioFisicoPacienteCompletosSobrantes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente debe tener registros en HCFISIPRO para el ingreso, centro de atención y unidad funcional indicados.; Los productos deben existir en el catálogo IHLISTPRO.; La unidad de medida puede no existir en INUNIMEDI (LEFT JOIN tolera ausencia).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarInventarioFisicoPacienteCompletosSobrantes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Todos los registros devueltos pertenecen al mismo paciente, ingreso, centro de atención y unidad funcional.; La columna Tipo siempre se devuelve con el literal ''Completos''.; Los valores nulos de dosis acumulada, horas acumuladas y abreviatura de unidad de medida se sustituyen por 0 o cadena vacía para evitar nulos en la salida.; Solo se consideran productos con retraso permitido (RETRASOGE=1) cuando aún no se ha excedido el tiempo estimado de medicación.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarInventarioFisicoPacienteCompletosSobrantes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso hospitalario; Centro de atención; Unidad funcional; Inventario físico de productos; Producto/medicamento; Dosis acumulada; Unidad de medida; Retraso permitido en administración; Tiempo estimado de medicación', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarInventarioFisicoPacienteCompletosSobrantes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCFISIPRO: Devuelve filas etiquetadas como ''Completos'' cuando CANACTPRO > 0, o cuando el producto permite retraso (B.RETRASOGE=1) y tiene dosis acumulada (DOSPROACU>0) sin superar el tiempo estimado (TOTHORACU <= B.TIEESTMED), siempre filtrando por paciente, ingreso, centro y unidad funcional.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarInventarioFisicoPacienteCompletosSobrantes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si A.CANACTPRO > 0 (existe cantidad activa pendiente) → Se incluye el producto como ''Completos'' mostrando su cantidad activa y dosis acumulada como sobrante.; si B.RETRASOGE = 1 AND DOSPROACU > 0 AND TOTHORACU <= B.TIEESTMED → Se incluye el producto aunque no tenga cantidad activa, por tener dosis acumulada dentro del tiempo estimado permitido de retraso.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarInventarioFisicoPacienteCompletosSobrantes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFISIPRO; dbo.IHLISTPRO; dbo.INUNIMEDI', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarInventarioFisicoPacienteCompletosSobrantes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarInventarioFisicoPacienteCompletosSobrantes';
-- GO
