
create proc SP_AP_REGISTRARPROVEEDOR(
@Documento varchar(50),
@RazonSocial varchar(500),
@Correo varchar(50),
@Telefono varchar(50),
@Estado int,
@Resultado int output,
@Mensaje varchar(500) output
)as
begin
	set @Resultado = 0
	declare @IDPERSONA INT
	if not exists (select *from AP_proveedor where Documento = @Documento)
	begin
		insert into AP_proveedor(Documento,RazonSocial,Correo,Telefono,Estado)
		values (@Documento,@RazonSocial,@Correo,@Telefono,@Estado)

		set @Resultado = SCOPE_IDENTITY()
	end
	ELSE
		set @Mensaje = 'El numero de documento ya existe'
end

GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Registra un nuevo proveedor validando que su documento no exista previamente, devolviendo el ID generado o un mensaje de duplicidad.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AP_REGISTRARPROVEEDOR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe proporcionarse el documento del proveedor para validar unicidad antes de insertar', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AP_REGISTRARPROVEEDOR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El Documento del proveedor es único: no se permite insertar dos proveedores con el mismo Documento; El resultado se inicializa en 0 y solo toma valor positivo (ID generado) cuando la inserción ocurre exitosamente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AP_REGISTRARPROVEEDOR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'proveedor; documento de identificación; razón social', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AP_REGISTRARPROVEEDOR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] dbo.AP_proveedor: Cuando no existe un proveedor con el mismo Documento, inserta un nuevo registro con Documento, RazonSocial, Correo, Telefono y Estado, retornando el SCOPE_IDENTITY como resultado; [RETURN_RESULT] dbo.AP_proveedor: Cuando ya existe un proveedor con el mismo Documento, no inserta y retorna el mensaje ''El numero de documento ya existe''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AP_REGISTRARPROVEEDOR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si No existe registro en AP_proveedor con el mismo Documento → Inserta el nuevo proveedor y devuelve el ID generado como resultado else No inserta y devuelve mensaje ''El numero de documento ya existe''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AP_REGISTRARPROVEEDOR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AP_proveedor', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AP_REGISTRARPROVEEDOR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AP_REGISTRARPROVEEDOR';
-- GO
