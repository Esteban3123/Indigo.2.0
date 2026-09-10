
CREATE PROCEDURE [dbo].[SPHC_ListarProductosPendientesDevolutivosPrestamo]
(
@Ingreso char(10),
@Paciente varchar(25),
@Centro char(10),
@Unidad char(10)
)
AS
BEGIN
	SET NOCOUNT ON;
    SELECT CAST(0 AS BIT) AS Sel,IPCODPACP AS CodigoPaciente,RTRIM(IPNOMCOMP) AS Paciente,A.NUMINGREP AS Ingreso, 
	RTRIM(A.CODPRODUC) AS CodigoProducto, RTRIM(DESPRODUC) AS Producto, SUM(CANPRODUCT)  AS CantidadPrestada,
	CASE WHEN D.CANACTPRO IS NULL THEN 0 ELSE D.CANACTPRO END AS CantidadDisponible ,CAST(0 AS BIT) AS Anulado
	FROM dbo.HCPRESTME A
	INNER JOIN dbo.INPACIENT B ON A.IPCODPACP=B.IPCODPACI
	INNER JOIN dbo.IHLISTPRO C ON A.CODPRODUC=C.CODPRODUC
	LEFT OUTER JOIN dbo.HCFISIPRO D ON A.IPCODPACI=D.IPCODPACI AND A.NUMINGRES=D.NUMINGRES AND A.CODCENATE=D.CODCENATE AND A.UFUCODIGO=D.UFUCODIGO AND A.CODPRODUC=D.CODPRODUC
	WHERE A.IPCODPACI=@Paciente AND A.NUMINGRES=@Ingreso AND A.CODCENATE=@Centro 
	AND A.UFUCODIGO=@Unidad AND CANPROPEN > 0 AND PREANULAD = 0
	GROUP BY IPCODPACP,IPNOMCOMP,NUMINGREP,A.CODPRODUC,DESPRODUC,CANACTPRO
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los medicamentos e insumos prestados desde el ingreso de un paciente a otro que aún tienen cantidad pendiente de devolución (saldo por devolver mayor a cero y no anulados). Recibe como parámetros el número de ingreso, la cédula del paciente prestamista, el centro de atención y la unidad funcional, y retorna por cada producto: el paciente prestatario (quien recibió el préstamo) con su nombre, el número de ingreso prestatario, el código y descripción del producto, la cantidad total prestada y la cantidad actualmente disponible en el stock físico del paciente prestamista según HCFISIPRO. Se usa para gestionar la recuperación o devolución de medicamentos e insumos prestados entre pacientes durante la hospitalización, permitiendo identificar deudas de préstamo pendientes antes de dar de alta o realizar ajustes de inventario clínico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarProductosPendientesDevolutivosPrestamo';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarProductosPendientesDevolutivosPrestamo';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los productos prestados a un paciente durante un ingreso que aún tienen cantidades pendientes por devolver, junto con la cantidad disponible registrada en su historia clínica.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarProductosPendientesDevolutivosPrestamo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben existir préstamos registrados para la combinación paciente, ingreso, centro y unidad funcional; El producto debe existir en el catálogo maestro de productos; El paciente debe existir en el maestro de pacientes', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarProductosPendientesDevolutivosPrestamo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran préstamos no anulados (PREANULAD = 0); Solo se consideran préstamos con cantidad pendiente positiva (CANPROPEN > 0); Las cantidades prestadas se totalizan por producto mediante SUM agrupado; Los campos Sel y Anulado siempre se inicializan en 0 (false) en el resultado', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarProductosPendientesDevolutivosPrestamo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso hospitalario; Centro de atención; Unidad funcional; Préstamo de productos; Productos devolutivos; Cantidad pendiente de devolución; Cantidad disponible; Anulación de préstamo', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarProductosPendientesDevolutivosPrestamo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve filas agrupadas por producto sumando CANPRODUCT solo cuando CANPROPEN > 0 y PREANULAD = 0, marcando Sel=0 y Anulado=0 por defecto', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarProductosPendientesDevolutivosPrestamo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si D.CANACTPRO IS NULL (no hay registro en HCFISIPRO para el producto/ingreso) → CantidadDisponible se reporta como 0 else Se reporta el valor de CANACTPRO como CantidadDisponible', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarProductosPendientesDevolutivosPrestamo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCPRESTME; dbo.INPACIENT; dbo.IHLISTPRO; dbo.HCFISIPRO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarProductosPendientesDevolutivosPrestamo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarProductosPendientesDevolutivosPrestamo';
-- GO
