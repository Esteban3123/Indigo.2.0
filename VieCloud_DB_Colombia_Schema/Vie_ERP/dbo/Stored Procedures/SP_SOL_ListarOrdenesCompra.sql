
CREATE PROCEDURE [dbo].[SP_SOL_ListarOrdenesCompra]
(
@EstadoOrden int,
@EstadoOrden2  int
)
AS BEGIN
SET NOCOUNT ON;

SELECT ORDCONSEC,PROVNOMBRE,ORDENFECH,ORDDESCUE,PLAZOCOTI,OBSERCOTI,CASE ORDESTADO WHEN 1 THEN '1. Sin Recibir' WHEN 2 THEN '2. Recibido Parcial' END AS ORDESTADO,Descripcion, DATEADD(day, PLAZOCOTI, ORDENFECH)as FECHAENTREGA,  DATEDIFF(day, [Common].[GETDATE](),(DATEADD(day, PLAZOCOTI, ORDENFECH)))AS DIASRESTANTES

FROM SOLORDCOM A 
INNER JOIN SOLCOTI B ON A.AUTOCOTI=B.AUTO
INNER JOIN SOLPROVEE C ON B.PROVECOTI=C.PROVAUTO
INNER JOIN SOLTIPAGO D ON B.FORMACOTI=D.Autonumerico

WHERE ORDESTADO=@EstadoOrden OR ORDESTADO=@EstadoOrden2
ORDER BY FECHAENTREGA
end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista las órdenes de compra activas (sin recibir o recibidas parcialmente) generadas a proveedores, filtrando por uno o dos estados de orden definidos por el usuario. Para cada orden muestra el número consecutivo, el nombre del proveedor, la fecha de emisión, el descuento aplicado, el plazo de entrega en días, las observaciones de la cotización de origen, la forma de pago y calcula automáticamente la fecha estimada de entrega y los días restantes hasta esa fecha. Integra información de las tablas de órdenes de compra (SOLORDCOM), cotizaciones (SOLCOTI), proveedores (SOLPROVEE) y tipos de pago (SOLTIPAGO), ordenando los resultados por proximidad de fecha de entrega para facilitar el seguimiento y control del proceso de compras y suministros de la institución.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_SOL_ListarOrdenesCompra';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_SOL_ListarOrdenesCompra';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista órdenes de compra filtradas por dos posibles estados, mostrando proveedor, forma de pago, fecha y días restantes para la entrega según el plazo de cotización.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SOL_ListarOrdenesCompra';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las órdenes deben tener cotización asociada con proveedor y forma de pago válidos (joins internos lo exigen).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SOL_ListarOrdenesCompra';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La fecha de entrega siempre se calcula como ORDENFECH + PLAZOCOTI días.; Los días restantes se calculan respecto a la fecha actual de [Common].[GETDATE]() contra la fecha de entrega.; Solo se exponen órdenes con estado 1 o 2 etiquetado; otros estados se filtran o aparecen sin etiqueta.; El resultado siempre se ordena ascendente por fecha de entrega.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SOL_ListarOrdenesCompra';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Orden de compra; Cotización; Proveedor; Forma de pago; Plazo de entrega; Estado de orden (Sin Recibir / Recibido Parcial)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SOL_ListarOrdenesCompra';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Retorna las órdenes cuyo ORDESTADO coincide con cualquiera de los dos estados recibidos, ordenadas por fecha de entrega calculada.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SOL_ListarOrdenesCompra';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ORDESTADO = 1 → Etiqueta el estado como ''1. Sin Recibir''; si ORDESTADO = 2 → Etiqueta el estado como ''2. Recibido Parcial'' else Estado se muestra como NULL (no hay otra rama definida)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SOL_ListarOrdenesCompra';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SOL_ListarOrdenesCompra';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.SOLORDCOM; dbo.SOLCOTI; dbo.SOLPROVEE; dbo.SOLTIPAGO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SOL_ListarOrdenesCompra';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SOL_ListarOrdenesCompra';
-- GO
