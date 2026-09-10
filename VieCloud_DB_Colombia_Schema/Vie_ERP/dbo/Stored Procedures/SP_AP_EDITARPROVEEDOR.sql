create proc SP_AP_EDITARPROVEEDOR(
@IdProveedor int,
@Documento varchar(50),
@RazonSocial varchar(500),
@Correo varchar(50),
@Telefono varchar(50),
@Estado int,
@Resultado bit output,
@Mensaje varchar(500) output
)as
begin
	set @Resultado = 1
	declare @IDPERSONA INT
	if not exists (select *from AP_proveedor where Documento = @Documento and IdProveedor != @IDProveedor)
	begin
		update AP_proveedor set
		Documento = @Documento,
		RazonSocial = @RazonSocial,
		Correo = @Correo,
		Telefono = @Telefono,
		Estado = @Estado
		where IdProveedor = @IdProveedor
	end
	ELSE
	begin
		set @Resultado = 0
		set @Mensaje = 'El numero de documento ya existe'
	end

end

GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Actualiza los datos de un proveedor existente validando previamente que el número de documento no esté asignado a otro proveedor.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AP_EDITARPROVEEDOR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir el proveedor a editar identificado por su Id; El número de documento no debe estar registrado en otro proveedor distinto', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AP_EDITARPROVEEDOR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'No se permite que dos proveedores distintos compartan el mismo número de documento; El resultado por defecto es exitoso (1) salvo que se detecte duplicidad de documento', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AP_EDITARPROVEEDOR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'proveedor; documento de identificación; razón social', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AP_EDITARPROVEEDOR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] dbo.AP_proveedor: Cuando no existe otro proveedor con el mismo Documento y distinto IdProveedor, se actualizan Documento, RazonSocial, Correo, Telefono y Estado del proveedor indicado; [RETURN_RESULT] dbo.AP_proveedor: Cuando ya existe otro proveedor con el mismo Documento, se retorna Resultado=0 y Mensaje=''El numero de documento ya existe'' sin actualizar', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AP_EDITARPROVEEDOR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si No existe otro proveedor distinto con el mismo número de documento → Actualiza los datos del proveedor (documento, razón social, correo, teléfono y estado) else Marca resultado en 0 y devuelve mensaje ''El numero de documento ya existe''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AP_EDITARPROVEEDOR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AP_proveedor', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AP_EDITARPROVEEDOR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AP_EDITARPROVEEDOR';
-- GO
