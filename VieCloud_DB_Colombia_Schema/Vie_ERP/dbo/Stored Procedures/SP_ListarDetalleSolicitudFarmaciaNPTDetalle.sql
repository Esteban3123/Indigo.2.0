	CREATE PROCEDURE [dbo].[SP_ListarDetalleSolicitudFarmaciaNPTDetalle]
(
  @IdNPT as Int
)
WITH RECOMPILE
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
				A.VOLUMENCAL AS 'CANTIDAD COMPONENTE', RTRIM(A.CODPRODUC) AS 'COD PRODUCTO COMPONENTE',RTRIM(F.Name) AS 'PRODUCTO COMPONENTE',A.CODUNIMED AS 'COD UNIDAD MEDIDA COMPONENTE',
				H.DESUNIMED AS 'UNIDAD MEDIDA COMPONENTE'
			FROM HCNUTPAREND A
				INNER JOIN Inventory.ATC F ON A.CODPRODUC = F.Code
				INNER JOIN INUNIMEDI H ON A.CODUNIMED = H.CODUNIMED
			WHERE A.IDHCNUTPAREC = @IdNPT
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los componentes detallados de una solicitud de Nutrición Parenteral Total (NPT) a farmacia, dado el identificador de la pauta de nutrición parenteral. Para cada componente (aminoácidos, lípidos, glucosa, electrolitos, vitaminas, oligoelementos, etc.) retorna la cantidad o volumen, el código del producto, su nombre comercial o técnico obtenido del catálogo ATC, y la unidad de medida correspondiente. Relaciona la prescripción de nutrición parenteral (HCNUTPAREND) con el catálogo de medicamentos e insumos (ATC) y la tabla de unidades de medida (INUNIMEDI). Se utiliza en el proceso de dispensación y preparación de NPT en farmacia hospitalaria, permitiendo visualizar la fórmula completa de la bolsa de nutrición parenteral prescrita.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ListarDetalleSolicitudFarmaciaNPTDetalle';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ListarDetalleSolicitudFarmaciaNPTDetalle';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los componentes (productos, cantidades y unidades de medida) que conforman el detalle de una solicitud de Nutrición Parenteral Total (NPT) de farmacia.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarDetalleSolicitudFarmaciaNPTDetalle';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un identificador de cabecera de NPT (IDHCNUTPAREC) válido para retornar filas.; Cada componente debe tener su producto registrado en Inventory.ATC y su unidad de medida en INUNIMEDI; de lo contrario el INNER JOIN lo excluye.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarDetalleSolicitudFarmaciaNPTDetalle';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retornan componentes cuyo producto está catalogado en Inventory.ATC y cuya unidad de medida existe en INUNIMEDI (INNER JOIN).; Los códigos de producto se devuelven sin espacios a la derecha (RTRIM).; Resultados restringidos a un único encabezado de NPT por ejecución.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarDetalleSolicitudFarmaciaNPTDetalle';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Nutrición Parenteral Total (NPT); Solicitud de farmacia; Componente de fórmula; Producto/medicamento (ATC); Unidad de medida', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarDetalleSolicitudFarmaciaNPTDetalle';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCNUTPAREND: Cuando A.IDHCNUTPAREC = @IdNPT, devuelve los componentes asociados con sus volúmenes, códigos y descripciones de producto y unidad de medida.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarDetalleSolicitudFarmaciaNPTDetalle';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'HCNUTPAREND; Inventory.ATC; INUNIMEDI', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarDetalleSolicitudFarmaciaNPTDetalle';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarDetalleSolicitudFarmaciaNPTDetalle';
-- GO
