-- =============================================
-- Author:      <Author, , Name>
-- Create Date: <Create Date, , >
-- Description: <Description, , >
-- =============================================
CREATE PROCEDURE [dbo].[sp_calcularValorDispensar]
(
@codigoProducto as varchar(20)
)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON

	declare @cantidadSolicitada as int = 10150
	declare @cantidadmaximausar as int

	declare @tablevalores as table(codigo varchar(20),cantidadDisponible int, cantidadusar int, presentacion int, totalusar int)
	insert into @tablevalores
	select top 1 CODIGO,CANTIDAD,cantidadusar,PRESENTACION,Totalusar  from (
		select *,@cantidadSolicitada / PRESENTACION as cantidadusar, 
		(@cantidadSolicitada / PRESENTACION) * PRESENTACION as Totalusar
		from Table_Inventario where CANTIDAD > 0 
	) as tmp where Totalusar <=@cantidadSolicitada  order by cantidadusar asc

	declare @totalusado int = (select SUM(totalusar ) from @tablevalores )

	declare @saldopendiente as int = @cantidadSolicitada - @totalusado

	select @saldopendiente 

	

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Calcula la cantidad de unidades a dispensar de un producto del inventario dado un código de producto, determinando cuántas unidades completas por presentación se pueden usar para cubrir una cantidad solicitada fija. Consulta el inventario disponible (Table_Inventario) buscando lotes con existencias mayores a cero, calcula cuántas presentaciones enteras caben dentro de la cantidad requerida y determina el saldo pendiente que no pudo ser cubierto. Está orientado al proceso de dispensación de medicamentos o insumos, apoyando la lógica de abastecimiento y control de stock en farmacia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'sp_calcularValorDispensar';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'sp_calcularValorDispensar';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Calcula, a partir del inventario disponible y la presentación del producto, cuánto puede dispensarse en múltiplos exactos de la presentación y devuelve el saldo pendiente que no alcanza a cubrirse.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'sp_calcularValorDispensar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir al menos un registro en el inventario con CANTIDAD > 0 cuya PRESENTACION permita calcular un Totalusar menor o igual a la cantidad solicitada; de lo contrario el saldo pendiente equivaldrá al total solicitado.; La columna PRESENTACION no puede ser cero (se usa como divisor).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'sp_calcularValorDispensar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran lotes/registros de inventario con cantidad disponible mayor a cero (CANTIDAD > 0).; La cantidad a usar se ajusta a múltiplos exactos de la presentación (Totalusar = (solicitado / presentación) * presentación), nunca excede lo solicitado (Totalusar <= cantidadSolicitada).; Se selecciona un único registro de inventario (TOP 1) ordenado ascendentemente por cantidad a usar, priorizando la menor cantidad de unidades de presentación a consumir.; El saldo pendiente reportado es la diferencia entre lo solicitado y lo cubierto por la presentación seleccionada.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'sp_calcularValorDispensar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'inventario; dispensación; presentación de producto; saldo pendiente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'sp_calcularValorDispensar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] ?: Devuelve como resultset el saldo pendiente (cantidadSolicitada - suma de Totalusar) calculado tras seleccionar el registro de inventario óptimo con CANTIDAD > 0 y Totalusar <= cantidadSolicitada.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'sp_calcularValorDispensar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.Table_Inventario', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'sp_calcularValorDispensar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'sp_calcularValorDispensar';
-- GO
