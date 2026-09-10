
CREATE proc SP_REGISTRARCLIENTE(
@Documento varchar(50),
@NombreCompleto varchar(50),
@Correo varchar(50),
@Telefono varchar(50),
@Estado bit,
@Resultado int output,
@Mensaje varchar(500) output
)
as
begin

	set @Resultado = 0
	declare @IDPERSONA INT
	IF NOT EXISTS (SELECT * FROM AP_cliente where Documento = @Documento)
	 begin

		insert into AP_cliente(Documento,NombreCompleto,Correo,Telefono,Estado)
		values (@Documento,@NombreCompleto,@Correo,@Telefono,@Estado)

		set @Resultado = SCOPE_IDENTITY()

	 end
	 else
		set @Mensaje = 'El numero de documento ya existe'

end

GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Registra un nuevo cliente validando que su número de documento no exista previamente, devolviendo el ID generado o un mensaje de duplicado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REGISTRARCLIENTE';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe proveerse un número de documento para validar unicidad antes del registro', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REGISTRARCLIENTE';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'No se permiten clientes duplicados por número de documento; El resultado inicia en 0 y solo cambia al ID generado si la inserción ocurre', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REGISTRARCLIENTE';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'cliente; documento de identidad', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REGISTRARCLIENTE';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] dbo.AP_cliente: Cuando NOT EXISTS un registro con el mismo Documento, se inserta el cliente con sus datos y estado, y se retorna SCOPE_IDENTITY() como resultado; [RETURN_RESULT] dbo.AP_cliente: Cuando ya existe un cliente con el mismo Documento, se asigna el mensaje ''El numero de documento ya existe'' y el resultado permanece en 0', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REGISTRARCLIENTE';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si No existe un cliente con el mismo Documento en AP_cliente → Inserta el nuevo cliente y devuelve el ID generado en el resultado else Devuelve mensaje ''El numero de documento ya existe'' sin insertar', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REGISTRARCLIENTE';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AP_cliente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REGISTRARCLIENTE';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REGISTRARCLIENTE';
-- GO
