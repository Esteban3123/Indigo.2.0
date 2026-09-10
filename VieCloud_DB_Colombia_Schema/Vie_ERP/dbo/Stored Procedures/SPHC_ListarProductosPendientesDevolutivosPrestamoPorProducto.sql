
CREATE PROCEDURE [dbo].[SPHC_ListarProductosPendientesDevolutivosPrestamoPorProducto]
(
@Ingreso char(10),
@Paciente varchar(25),
@Centro char(10),
@Unidad char(10),
@Producto char(20)
)
AS
BEGIN
	SET NOCOUNT ON;
    SELECT RTRIM(IPNOMCOMP) AS Paciente, RTRIM(DESPRODUC) AS Producto, SUM(CANPRODUCT)  AS CantidadPrestada
	FROM dbo.HCPRESTME A
	INNER JOIN dbo.INPACIENT B ON A.IPCODPACP=B.IPCODPACI
	INNER JOIN dbo.IHLISTPRO C ON A.CODPRODUC=C.CODPRODUC
	WHERE A.IPCODPACI=@Paciente AND A.NUMINGRES=@Ingreso AND A.CODCENATE=@Centro 
	AND A.UFUCODIGO=@Unidad AND A.CODPRODUC=@Producto AND CANPROPEN > 0 AND PREANULAD = 0
	GROUP BY IPNOMCOMP,DESPRODUC
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los productos (medicamentos o insumos) prestados a un paciente en un ingreso específico que aún tienen devoluciones pendientes, filtrando por centro de atención, unidad funcional y un producto en particular. Consulta los préstamos registrados en la historia clínica (HCPRESTME), cruza con el catálogo de productos (IHLISTPRO) para obtener el nombre del medicamento o insumo, y con el maestro de pacientes (INPACIENT) para mostrar el nombre del paciente prestamista. Solo considera registros no anulados y con cantidad pendiente por devolver mayor a cero, agrupando el total prestado por paciente y producto. Se usa para gestionar y hacer seguimiento de devoluciones de préstamos de medicamentos o insumos entre pacientes dentro del proceso de farmacia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarProductosPendientesDevolutivosPrestamoPorProducto';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarProductosPendientesDevolutivosPrestamoPorProducto';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Listar la cantidad pendiente por devolver de un producto específico prestado a un paciente durante una atención, agrupada por paciente y producto.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarProductosPendientesDevolutivosPrestamoPorProducto';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un préstamo registrado en HCPRESTME para la combinación paciente, ingreso, centro, unidad y producto indicados.; El paciente debe existir en INPACIENT y el producto en IHLISTPRO.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarProductosPendientesDevolutivosPrestamoPorProducto';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Excluye préstamos anulados (PREANULAD = 0).; Excluye préstamos sin saldo pendiente (CANPROPEN > 0).; Resultado agrupado por nombre del paciente y descripción del producto.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarProductosPendientesDevolutivosPrestamoPorProducto';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Producto (medicamento/insumo); Préstamo devolutivo; Ingreso/Atención; Centro de atención; Unidad funcional; Cantidad pendiente de devolución; Anulación de préstamo', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarProductosPendientesDevolutivosPrestamoPorProducto';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCPRESTME: Devuelve únicamente préstamos con CANPROPEN > 0 y PREANULAD = 0 (pendientes y no anulados), sumando CANPRODUCT como cantidad prestada.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarProductosPendientesDevolutivosPrestamoPorProducto';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCPRESTME; dbo.INPACIENT; dbo.IHLISTPRO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarProductosPendientesDevolutivosPrestamoPorProducto';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarProductosPendientesDevolutivosPrestamoPorProducto';
-- GO
