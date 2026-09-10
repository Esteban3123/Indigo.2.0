
CREATE PROCEDURE [dbo].[SPHC_ListarProductosPendientesDevolutivosConsulta]
(
@Ingreso char(10),
@Paciente varchar(25),
@Centro char(10),
@Unidad char(10)
)
AS
BEGIN
	SET NOCOUNT ON;
    SELECT A.IPCODPACI AS CodigoPaciente,RTRIM(IPNOMCOMP) AS Paciente,A.NUMINGRES AS Ingreso, 
	RTRIM(A.CODPRODUC) AS CodigoProducto, RTRIM(DESPRODUC) AS Producto, SUM(CANPRODUCT)  AS CantidadPrestada,
	CASE WHEN D.CANACTPRO IS NULL THEN 0 ELSE D.CANACTPRO END AS CantidadDisponible
	FROM dbo.HCPRESTME A
	INNER JOIN dbo.INPACIENT B ON A.IPCODPACI=B.IPCODPACI
	INNER JOIN dbo.IHLISTPRO C ON A.CODPRODUC=C.CODPRODUC
	LEFT OUTER JOIN dbo.HCFISIPRO D ON A.IPCODPACI=D.IPCODPACI AND A.NUMINGRES=D.NUMINGRES AND A.CODCENATE=D.CODCENATE AND A.UFUCODIGO=D.UFUCODIGO AND A.CODPRODUC=D.CODPRODUC
	WHERE A.IPCODPACP=@Paciente AND A.NUMINGREP=@Ingreso  AND A.CODCENATE=@Centro 
	AND A.UFUCODIGO=@Unidad AND CANPROPEN > 0 AND PREANULAD = 0
	GROUP BY A.IPCODPACI,IPNOMCOMP,A.NUMINGRES,A.CODPRODUC,DESPRODUC,CANACTPRO
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los medicamentos e insumos prestados que aún están pendientes de devolución para un ingreso y paciente específicos. Consulta los movimientos de préstamos (HCPRESTME) filtrando por el paciente prestador, el ingreso, el centro de atención y la unidad funcional, mostrando únicamente los ítems con cantidad pendiente mayor a cero y que no hayan sido anulados. Combina el catálogo de productos (IHLISTPRO) para mostrar el nombre del medicamento o insumo, el maestro de pacientes (INPACIENT) para obtener el nombre del paciente que recibió el préstamo, y el inventario físico del paciente (HCFISIPRO) para informar la cantidad disponible actualmente en su historia clínica. Se utiliza en la gestión de devoluciones de medicamentos prestados entre pacientes durante la hospitalización.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarProductosPendientesDevolutivosConsulta';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarProductosPendientesDevolutivosConsulta';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los productos prestados pendientes de devolución para un paciente en un ingreso, centro y unidad funcional, mostrando la cantidad prestada y la cantidad actualmente disponible para devolver.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarProductosPendientesDevolutivosConsulta';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben existir registros de préstamo (HCPRESTME) para el paciente, ingreso, centro y unidad indicados; El paciente debe existir en INPACIENT y el producto en IHLISTPRO (INNER JOIN obligatorios)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarProductosPendientesDevolutivosConsulta';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran préstamos no anulados (PREANULAD = 0); Solo se consideran préstamos con saldo pendiente por devolver (CANPROPEN > 0); La cantidad prestada se agrega (SUM) por paciente, ingreso, producto y descripción; Si no existe registro en HCFISIPRO para la combinación paciente/ingreso/centro/unidad/producto, la disponibilidad se asume en 0', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarProductosPendientesDevolutivosConsulta';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso hospitalario; Préstamo de productos; Devolución de productos; Centro de atención; Unidad funcional; Producto farmacéutico/dispositivo médico; Cantidad pendiente; Préstamo anulado', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarProductosPendientesDevolutivosConsulta';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve solo préstamos con CANPROPEN > 0 (cantidad pendiente por devolver) y PREANULAD = 0 (préstamo no anulado)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarProductosPendientesDevolutivosConsulta';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si D.CANACTPRO IS NULL (no hay registro asociado en HCFISIPRO) → Se reporta CantidadDisponible = 0 else Se reporta CantidadDisponible = D.CANACTPRO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarProductosPendientesDevolutivosConsulta';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCPRESTME; dbo.INPACIENT; dbo.IHLISTPRO; dbo.HCFISIPRO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarProductosPendientesDevolutivosConsulta';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarProductosPendientesDevolutivosConsulta';
-- GO
