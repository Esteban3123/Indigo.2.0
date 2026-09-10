
CREATE PROCEDURE [dbo].[ProductosPacienteATrasladar]
(
@Paciente varchar(25),
@Ingreso char(20),
@CentroAtencion char(10),
@UnidadFuncional char(10)
)
AS
BEGIN

declare @numProductos int

exec [dbo].[SPCH_ListarInventarioFisicoPacienteCompletosSobrantes] @Paciente, @Ingreso, @CentroAtencion, @UnidadFuncional

SELECT @numProductos = @@ROWCOUNT

RETURN @numProductos

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta los medicamentos o productos de farmacia asignados a un paciente que tienen unidades sobrantes o completas pendientes de traslado, dado su número de ingreso, centro de atención y unidad funcional. Internamente invoca el procedimiento SPCH_ListarInventarioFisicoPacienteCompletosSobrantes para obtener el inventario físico del paciente, y devuelve como valor de retorno la cantidad total de productos encontrados. Se utiliza en el proceso de traslado de pacientes para identificar qué medicamentos o insumos deben moverse junto con el paciente a otra unidad o servicio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ProductosPacienteATrasladar';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ProductosPacienteATrasladar';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los productos sobrantes del inventario físico del paciente que pueden ser trasladados, devolviendo la cantidad de productos encontrados.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ProductosPacienteATrasladar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente, ingreso, centro de atención y unidad funcional deben existir para que el procedimiento dependiente retorne información', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ProductosPacienteATrasladar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El valor retornado siempre corresponde al conteo de filas del listado de inventario físico sobrante del paciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ProductosPacienteATrasladar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso; Centro de atención; Unidad funcional; Inventario físico; Productos sobrantes; Traslado de productos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ProductosPacienteATrasladar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve el resultset producido por SPCH_ListarInventarioFisicoPacienteCompletosSobrantes y retorna como código de salida el número de filas (@@ROWCOUNT) generadas', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ProductosPacienteATrasladar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.SPCH_ListarInventarioFisicoPacienteCompletosSobrantes', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ProductosPacienteATrasladar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ProductosPacienteATrasladar';
-- GO
