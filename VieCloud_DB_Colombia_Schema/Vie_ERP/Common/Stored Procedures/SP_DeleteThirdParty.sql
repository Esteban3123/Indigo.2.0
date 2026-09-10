
-- =============================================
-- Author:		Carlos Jhefersson Muñoz Ramirez
-- Create date: 20/03/2017
-- Description:	Procedimiento para eliminar el Tercero
-- =============================================
CREATE PROCEDURE [Common].[SP_DeleteThirdParty]

	@NitThirdParty as varchar(20) 
	
AS
BEGIN
	--declare @NitThirdParty as varchar(20) 
	--set @NitThirdParty = '899999327'
	declare @PersonId as Integer = (select PersonId from Common.ThirdParty where Nit = @NitThirdParty)
	
	Begin try
		delete Common.Phone where IdPerson = @PersonId
		delete Common.[Address] where IdPerson = @PersonId
		delete Common.Email where IdPerson = @PersonId
		delete Common.ThirdParty where nit = @NitThirdParty 
		delete Common.Person where id = @PersonId

		--commit transaction
		select 0 as CodeMessage, 'Se Elimino correctamente' as Message
	End try
	Begin Catch

		--rollback transaction
		select 999 as CodeMessage, ERROR_MESSAGE() as Message
		--select 999 as CodeMessage, ERROR_NUMBER() as Message

	End Catch
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Elimina completamente un tercero del sistema (proveedor, contratista, aseguradora u otra entidad externa) a partir de su NIT. Antes de borrar el registro principal del tercero y su persona asociada, elimina en cascada toda la información de contacto vinculada: teléfonos, direcciones y correos electrónicos. Se utiliza cuando se requiere dar de baja definitiva a un tercero y limpiar todos sus datos de contacto relacionados en el maestro de personas.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'PROCEDURE', @level1name = N'SP_DeleteThirdParty';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'PROCEDURE', @level1name = N'SP_DeleteThirdParty';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Elimina un tercero y todos sus datos personales asociados (teléfonos, direcciones, correos y persona) a partir de su identificación tributaria.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_DeleteThirdParty';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un tercero en Common.ThirdParty cuyo Nit coincida con el valor recibido para obtener el PersonId asociado.; No deben existir referencias foráneas activas a las filas eliminadas que impidan el borrado.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_DeleteThirdParty';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El borrado de datos relacionados (Phone, Address, Email) se realiza siempre antes de borrar al tercero y a la persona, respetando dependencias jerárquicas.; La identificación del registro a eliminar siempre se hace por Nit para el tercero y por PersonId derivado para la persona y sus contactos.; El procedimiento siempre devuelve un conjunto de resultados con CodeMessage y Message, independientemente del éxito o falla.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_DeleteThirdParty';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Tercero; Persona; Nit; Teléfono; Dirección; Correo electrónico', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_DeleteThirdParty';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[DELETE] Common.Phone: Elimina todos los teléfonos cuyo IdPerson corresponda a la persona vinculada al tercero identificado por el Nit.; [DELETE] Common.Address: Elimina todas las direcciones cuyo IdPerson corresponda a la persona vinculada al tercero identificado por el Nit.; [DELETE] Common.Email: Elimina todos los correos cuyo IdPerson corresponda a la persona vinculada al tercero identificado por el Nit.; [DELETE] Common.ThirdParty: Elimina el tercero cuyo Nit coincida con el recibido.; [DELETE] Common.Person: Elimina la persona cuyo Id coincida con el PersonId obtenido del tercero.; [RETURN_RESULT] -: Si el bloque TRY finaliza sin error, retorna CodeMessage=0 con mensaje de éxito; si ocurre excepción, retorna CodeMessage=999 con ERROR_MESSAGE().', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_DeleteThirdParty';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Ocurre una excepción durante cualquiera de los DELETE (TRY/CATCH) → Retorna un resultado con CodeMessage=999 y el mensaje del error capturado else Retorna CodeMessage=0 con mensaje ''Se Elimino correctamente''', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_DeleteThirdParty';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Common.ThirdParty', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_DeleteThirdParty';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_DeleteThirdParty';
-- GO
