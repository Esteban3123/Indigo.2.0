create proc SP_AP_ELIMINARPROVEEDOR(
@IdProveedor int,
@Resultado bit output,
@Mensaje varchar(500) output
)as
begin
	set @Resultado = 1
	if not exists (select *from AP_proveedor P
	INNER JOIN AP_compra c on p.IdProveedor = c.IdProveedor
	where p.IdProveedor = @IDProveedor)
	begin
		delete top(1) from AP_proveedor where IdProveedor = @IdProveedor
	end
	ELSE
	begin
		set @Resultado = 0
		set @Mensaje = 'El proveedor se encuentra relacionado a una compra'
	end

end

GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Elimina un proveedor solo si no tiene compras asociadas; en caso contrario informa el bloqueo mediante bandera y mensaje.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AP_ELIMINARPROVEEDOR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El proveedor a eliminar debe existir identificado por su Id; No deben existir registros en AP_compra que referencien al proveedor', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AP_ELIMINARPROVEEDOR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Nunca se elimina un proveedor que tenga al menos una compra asociada; Resultado se inicializa en 1 (éxito) y solo cambia a 0 si la validación de integridad falla; La eliminación se limita a una sola fila (DELETE TOP(1))', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AP_ELIMINARPROVEEDOR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Proveedor; Compra', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AP_ELIMINARPROVEEDOR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[DELETE] AP_proveedor: Cuando no existe ninguna fila en AP_compra ligada al proveedor, se elimina (TOP 1) el registro correspondiente en AP_proveedor; [RETURN_RESULT] @Resultado/@Mensaje: Cuando el proveedor está relacionado a una compra, se devuelve Resultado=0 y Mensaje=''El proveedor se encuentra relacionado a una compra''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AP_ELIMINARPROVEEDOR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si No existe relación entre el proveedor y AP_compra → Procede a eliminar el proveedor de AP_proveedor else Aborta la eliminación, marca Resultado=0 y asigna mensaje de proveedor relacionado a compra', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AP_ELIMINARPROVEEDOR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AP_proveedor; dbo.AP_compra', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AP_ELIMINARPROVEEDOR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AP_ELIMINARPROVEEDOR';
-- GO
