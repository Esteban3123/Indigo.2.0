



create proc SP_EliminarCategoria(
@IdCategoria int,
@Resultado int output,
@Mensaje varchar(500) output
)
as
begin
	set @Resultado = 1
	if not exists (
	select *from AP_categoria c 
	inner join AP_producto p on p.IdCategoria = c.IdCategoria
	where c.IdCategoria = @IdCategoria
	)
	begin
		delete top(1) from AP_categoria where IdCategoria = @IdCategoria
	end
	ELSE
	begin
		set @Resultado = 0
		set @Mensaje = 'La categoria se encuentra relacionada a un producto'
	end
end

GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Elimina una categoría únicamente si no está referenciada por ningún producto, devolviendo un indicador de resultado y mensaje en caso de bloqueo.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_EliminarCategoria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La categoría a eliminar debe existir identificada por su Id; No deben existir productos asociados a la categoría en AP_producto', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_EliminarCategoria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Nunca se elimina una categoría que tenga productos asociados (integridad referencial lógica); El valor por defecto de Resultado es 1 (éxito) y solo cambia a 0 si la eliminación es bloqueada; La eliminación se limita a un único registro por ejecución (DELETE TOP(1))', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_EliminarCategoria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'categoría; producto', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_EliminarCategoria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[DELETE] AP_categoria: Si no existe ningún producto en AP_producto vinculado a la categoría indicada, se elimina un registro (TOP 1) de AP_categoria con ese Id; [RETURN_RESULT] @Resultado/@Mensaje: Cuando la categoría está relacionada a un producto, se retorna Resultado=0 y Mensaje=''La categoria se encuentra relacionada a un producto''; en caso contrario Resultado=1', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_EliminarCategoria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si No existe ningún producto en AP_producto asociado a la categoría → Se ejecuta el DELETE sobre AP_categoria else Se asigna Resultado=0 y Mensaje informando la relación con un producto, sin eliminar', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_EliminarCategoria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'AP_categoria; AP_producto', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_EliminarCategoria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_EliminarCategoria';
-- GO
