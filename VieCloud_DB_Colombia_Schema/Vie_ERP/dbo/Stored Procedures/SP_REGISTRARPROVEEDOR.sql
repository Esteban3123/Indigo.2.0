
create proc SP_REGISTRARPROVEEDOR(
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
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Registra un nuevo proveedor garantizando unicidad por número de documento; si ya existe, no inserta y devuelve mensaje informativo.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REGISTRARPROVEEDOR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El documento no debe existir previamente en AP_proveedor para permitir el alta', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REGISTRARPROVEEDOR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'No se permiten dos proveedores con el mismo Documento; Resultado=0 indica que no se realizó inserción (documento duplicado); El resultado distinto de 0 corresponde al ID identidad del proveedor recién creado', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REGISTRARPROVEEDOR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Proveedor; Documento de identificación; Razón social', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REGISTRARPROVEEDOR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] dbo.AP_proveedor: Cuando NOT EXISTS un registro con el mismo Documento, se inserta el proveedor y se retorna el SCOPE_IDENTITY() como resultado; [RETURN_RESULT] dbo.AP_proveedor: Cuando ya existe un proveedor con el mismo Documento, se devuelve el mensaje ''El numero de documento ya existe'' sin insertar', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REGISTRARPROVEEDOR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si NOT EXISTS proveedor con el mismo Documento → Inserta nuevo proveedor y asigna el ID generado al resultado else Asigna mensaje ''El numero de documento ya existe'' y mantiene resultado en 0', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REGISTRARPROVEEDOR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AP_proveedor', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REGISTRARPROVEEDOR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REGISTRARPROVEEDOR';
-- GO
