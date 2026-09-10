



CREATE PROC SP_EDITARCLIENTE(
@IdCliente int,
@Documento varchar(50),
@NombreCompleto varchar(50),
@Correo varchar(50),
@Telefono varchar(50),
@Estado bit,
@Resultado bit output,
@Mensaje varchar(500) output
)
as
begin

	set @Resultado = 1
	declare @IDPERSONA INT

	 if not exists(select * from AP_cliente where Documento = @Documento and IdCliente != @IdCliente)
	 begin

		update AP_cliente set
		Documento = @Documento,
		NombreCompleto = @NombreCompleto,
		Correo = @Correo,
		Telefono = @Telefono,
		Estado = @Estado
		where IdCliente = @IdCliente
	 end
	 else
	 begin
		set @Resultado = 0
		set @Mensaje = 'No se puede repetir el documento para mas de un usuario'
	end
end

GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Actualiza los datos de contacto y estado de un cliente existente, validando que el documento no esté asociado a otro cliente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_EDITARCLIENTE';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir el cliente identificado para que el UPDATE tenga efecto; El documento no debe estar registrado en otro cliente distinto', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_EDITARCLIENTE';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El documento debe ser único entre clientes distintos: nunca se actualiza si otro IdCliente ya posee ese Documento; Resultado se inicializa en 1 (éxito) y solo cambia a 0 cuando se detecta documento duplicado', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_EDITARCLIENTE';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cliente; Documento de identidad; Estado del cliente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_EDITARCLIENTE';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] dbo.AP_cliente: Cuando no existe otro registro con el mismo Documento y distinto IdCliente, se actualizan Documento, NombreCompleto, Correo, Telefono y Estado del cliente indicado; [RETURN_RESULT] dbo.AP_cliente: Cuando ya existe otro cliente con el mismo Documento, se retorna Resultado=0 y mensaje ''No se puede repetir el documento para mas de un usuario'' sin modificar datos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_EDITARCLIENTE';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si No existe otro cliente distinto con el mismo Documento → Actualiza los datos del cliente y deja Resultado=1 else No actualiza; devuelve Resultado=0 y mensaje ''No se puede repetir el documento para mas de un usuario''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_EDITARCLIENTE';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AP_cliente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_EDITARCLIENTE';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_EDITARCLIENTE';
-- GO
