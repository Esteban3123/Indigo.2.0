

CREATE PROCEDURE [Glosas].[HomologacionClientes]

AS
BEGIN

	BEGIN TRY

begin transaction 

	
	
		
	declare @count as integer = (select count(*) from HomologarClientes)
	select @count
	declare @contador as integer = 0

	WHILE @contador < @count BEGIN

		set @contador = @contador + 1

		--print @contador
		declare @nit as varchar(20) 

		select  @nit = Nit from HomologarClientes where Id = @contador
		
		update HomologarClientes set IdVie = (select top 1 Id from Common.Customer where Nit = @Nit  ) 	where Id = @contador  

		print @contador
	END 
	
	

	--Commit Transaction
		rollback transaction
	END TRY
	BEGIN CATCH
	rollback transaction
		SELECT
			ERROR_NUMBER() AS CodigoMensaje,
			ERROR_MESSAGE() AS  Mensaje
	END CATCH

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de homologación que cruza una tabla temporal de trabajo llamada HomologarClientes (con NITs de entidades pagadoras a homologar) contra el catálogo oficial de clientes del sistema (Common.Customer) para obtener el identificador interno (IdVie) correspondiente a cada NIT. Recorre registro por registro y actualiza el campo IdVie con el ID encontrado en el maestro de clientes (EPS, aseguradoras, empresas). Está orientado al proceso de glosas, permitiendo unificar o migrar referencias de entidades pagadoras cuando sus identificadores difieren entre sistemas. Actualmente la transacción se revierte (rollback) al finalizar, lo que sugiere que el procedimiento se usa en modo de validación o prueba antes de confirmar los cambios definitivos.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'HomologacionClientes';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'HomologacionClientes';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Homologa clientes recorriendo una tabla temporal de NITs y asignando el Id correspondiente del maestro de clientes, aunque finalmente revierte los cambios.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'HomologacionClientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La tabla HomologarClientes debe contener registros con Id secuencial desde 1 hasta el total de filas; Cada registro debe tener un Nit para buscar coincidencia en Common.Customer', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'HomologacionClientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Todas las modificaciones se revierten siempre mediante ROLLBACK TRANSACTION (el COMMIT está comentado), por lo que el procedimiento no persiste cambios; Si existen múltiples clientes con el mismo Nit, se toma solo el primero (TOP 1 sin ORDER BY, no determinístico); Ante cualquier error se ejecuta rollback y se retorna el detalle del error sin relanzarlo', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'HomologacionClientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Homologación de clientes; NIT; Cliente/entidad pagadora', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'HomologacionClientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] Glosas.HomologarClientes: Para cada fila con Id = @contador, se actualiza IdVie con el primer Id de Common.Customer cuyo Nit coincida con el Nit de la fila; [RETURN_RESULT] N/A: Devuelve el conteo total de filas de HomologarClientes antes de iterar; [RETURN_RESULT] N/A: En caso de error en el TRY, retorna ERROR_NUMBER y ERROR_MESSAGE', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'HomologacionClientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @contador < @count → Incrementa contador, lee Nit de la fila actual y actualiza IdVie con el Id de Common.Customer correspondiente', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'HomologacionClientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Glosas.HomologarClientes; Common.Customer', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'HomologacionClientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'HomologacionClientes';
-- GO
