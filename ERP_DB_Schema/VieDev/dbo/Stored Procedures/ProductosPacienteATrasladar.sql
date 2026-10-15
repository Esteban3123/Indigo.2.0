
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