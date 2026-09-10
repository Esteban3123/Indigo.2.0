

-- sp para listar las solicitudes autorizadas del usuario administrador logueado

CREATE PROCEDURE [dbo].[SP_SOL_ListarCotizaciones] 
AS BEGIN
SET NOCOUNT ON;

SELECT AUTO,FECHCOTI, FECHREGIS,VIGENCOTI,PLAZOCOTI,FORMACOTI,OBSERCOTI,PROVECOTI,B.PROVNOMBRE,C.Descripcion,ESTADCOTI,CODUSUARI,OBSEANULA
FROM SOLCOTI A 
INNER JOIN SOLPROVEE B ON A.PROVECOTI=B.PROVAUTO
INNER JOIN SOLTIPAGO C ON A.FORMACOTI=C.Autonumerico
LEFT OUTER JOIN SOLCOTANU D ON A.AUTO=D.COTIANULA

ORDER BY A.ESTADCOTI, VIGENCOTI

end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista todas las cotizaciones solicitadas a proveedores registradas en el sistema, combinando los datos de cada cotización (fecha, vigencia, plazo, forma de pago, observaciones y estado) con el nombre del proveedor correspondiente, la descripción del tipo de pago y, cuando aplica, el motivo de anulación. Compone información de la tabla de cotizaciones (SOLCOTI), el catálogo de proveedores (SOLPROVEE), los tipos de pago (SOLTIPAGO) y el historial de anulaciones (SOLCOTANU). Sirve para que el administrador tenga una vista consolidada del estado de todas las cotizaciones, ordenadas por su estado y vigencia, facilitando el seguimiento y gestión del proceso de compras y suministros.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_SOL_ListarCotizaciones';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_SOL_ListarCotizaciones';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las cotizaciones registradas con datos del proveedor, forma de pago y posible motivo de anulación, ordenadas por estado y vigencia.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SOL_ListarCotizaciones';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen cotizaciones cuyo proveedor exista en SOLPROVEE y cuya forma de pago exista en SOLTIPAGO (INNER JOIN obligatorio).; El resultado se ordena primero por estado de la cotización y luego por vigencia.; La información de anulación es opcional: una cotización no anulada igualmente aparece en el listado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SOL_ListarCotizaciones';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cotización; Proveedor; Forma de pago; Anulación de cotización; Vigencia de cotización; Plazo de cotización', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SOL_ListarCotizaciones';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] SOLCOTI: Devuelve todas las cotizaciones uniendo proveedor y tipo de pago; incluye observación de anulación si existe (LEFT JOIN con SOLCOTANU).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SOL_ListarCotizaciones';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.SOLCOTI; dbo.SOLPROVEE; dbo.SOLTIPAGO; dbo.SOLCOTANU', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SOL_ListarCotizaciones';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SOL_ListarCotizaciones';
-- GO
