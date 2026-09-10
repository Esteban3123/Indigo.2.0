CREATE PROC SP_ELIMINARUSUARIO(
@IdUsuario int,
@Respuesta bit output,
@Mensaje varchar(500) output
)
as
begin

	set @Respuesta = 0
	set @Mensaje = ''
	declare @pasoreglas bit = 1

	IF EXISTS (SELECT * FROM AP_compra C
	INNER JOIN AP_usuario U ON U.IdUsuario = C.IdUsuario
	WHERE U.IDUSUARIO = @IdUsuario
	)
	BEGIN
		set @pasoreglas = 0
		set @Respuesta = 0
		set @Mensaje = @Mensaje + 'No se puede eliminar porque el usuario se encuentra relacionado a una COMPRA\n'
	END

		IF EXISTS (SELECT * FROM AP_venta V
	INNER JOIN AP_usuario U ON U.IdUsuario = V.IdUsuario
	WHERE U.IDUSUARIO = @IdUsuario
	)
	BEGIN
		set @pasoreglas = 0
		set @Respuesta = 0
		set @Mensaje = @Mensaje + 'No se puede eliminar porque el usuario se encuentra relacionado a una VENTA\n'
	END


	if (@pasoreglas = 1)
	begin
		delete from AP_usuario where IdUsuario = @IdUsuario
		set @Respuesta = 1
	end

end

GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Elimina un usuario solo si no tiene compras ni ventas asociadas, devolviendo respuesta y mensaje según el resultado de las validaciones.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ELIMINARUSUARIO';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El usuario no debe estar referenciado en AP_compra; El usuario no debe estar referenciado en AP_venta', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ELIMINARUSUARIO';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Nunca elimina un usuario si tiene al menos una compra asociada; Nunca elimina un usuario si tiene al menos una venta asociada; Los mensajes de error se acumulan (compra y venta pueden reportarse en una misma ejecución); Respuesta=1 solo cuando se efectúa la eliminación', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ELIMINARUSUARIO';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'usuario; compra; venta', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ELIMINARUSUARIO';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[DELETE] dbo.AP_usuario: Cuando el usuario no está relacionado con compras ni ventas (pasoreglas=1), se elimina el registro de AP_usuario y se retorna Respuesta=1', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ELIMINARUSUARIO';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe registro en AP_compra asociado al usuario → Marca pasoreglas=0, Respuesta=0 y agrega mensaje de bloqueo por COMPRA; si Existe registro en AP_venta asociado al usuario → Marca pasoreglas=0, Respuesta=0 y agrega mensaje de bloqueo por VENTA; si pasoreglas = 1 (sin compras ni ventas asociadas) → Ejecuta DELETE sobre AP_usuario y asigna Respuesta=1 else No realiza eliminación', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ELIMINARUSUARIO';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AP_compra; dbo.AP_usuario; dbo.AP_venta', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ELIMINARUSUARIO';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ELIMINARUSUARIO';
-- GO
