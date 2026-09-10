
CREATE PROCEDURE [dbo].[SPHC_ListarProductosPendientesDevolutivosConsultaPorProducto]
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
	INNER JOIN dbo.INPACIENT B ON A.IPCODPACI=B.IPCODPACI
	INNER JOIN dbo.IHLISTPRO C ON A.CODPRODUC=C.CODPRODUC
	WHERE A.IPCODPACP=@Paciente AND A.NUMINGREP=@Ingreso  AND A.CODCENATE=@Centro 
	AND A.UFUCODIGO=@Unidad AND A.CODPRODUC=@Producto AND CANPROPEN > 0 AND PREANULAD = 0
	GROUP BY IPNOMCOMP,DESPRODUC
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta los préstamos de medicamentos o insumos pendientes de devolución para un paciente, ingreso, centro de atención, unidad funcional y producto específicos. Cruza los registros de préstamos (HCPRESTME) con el catálogo de productos (IHLISTPRO) y los datos del paciente (INPACIENT) para mostrar el nombre del paciente, la descripción del producto prestado y la cantidad total prestada aún no devuelta. Solo considera registros activos (no anulados) con cantidad pendiente mayor a cero. Es útil para el seguimiento y conciliación de préstamos de medicamentos o insumos entre pacientes dentro de una unidad hospitalaria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarProductosPendientesDevolutivosConsultaPorProducto';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarProductosPendientesDevolutivosConsultaPorProducto';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consulta la cantidad pendiente por devolver de un producto específico prestado a un paciente durante un ingreso, centro y unidad funcional dados.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarProductosPendientesDevolutivosConsultaPorProducto';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben existir préstamos registrados en HCPRESTME para la combinación paciente/ingreso/centro/unidad/producto; El paciente debe existir en INPACIENT y el producto en IHLISTPRO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarProductosPendientesDevolutivosConsultaPorProducto';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo considera préstamos no anulados (PREANULAD = 0); Solo considera préstamos con cantidad pendiente mayor a cero (CANPROPEN > 0); Los resultados se agrupan por paciente y producto sumando las cantidades prestadas', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarProductosPendientesDevolutivosConsultaPorProducto';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Préstamo de productos; Producto/medicamento; Devolutivos pendientes; Ingreso del paciente; Centro de atención; Unidad funcional; Anulación de préstamo', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarProductosPendientesDevolutivosConsultaPorProducto';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RESULTSET: Devuelve nombre del paciente, descripción del producto y suma de cantidades prestadas filtrando por CANPROPEN > 0 (cantidad pendiente) y PREANULAD = 0 (préstamo no anulado)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarProductosPendientesDevolutivosConsultaPorProducto';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCPRESTME; dbo.INPACIENT; dbo.IHLISTPRO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarProductosPendientesDevolutivosConsultaPorProducto';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarProductosPendientesDevolutivosConsultaPorProducto';
-- GO
