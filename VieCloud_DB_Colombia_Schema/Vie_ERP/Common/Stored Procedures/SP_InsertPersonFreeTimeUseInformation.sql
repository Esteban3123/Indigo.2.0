-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Common].[SP_InsertPersonFreeTimeUseInformation] 
	-- Add the parameters for the stored procedure here
	@IdentificationNumber Varchar(20),
	@FreeTimeUseId INT
	
	

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	
	declare @Count INT
	declare @Result INT 
	SET @Count = (SELECT COUNT(*)
	FROM [Common].[PersonFreeTimeUse]
	WHERE ([PersonFreeTimeUse].[PersonId] = (Select [Person].[Id] from [Common].[Person] WHERE [Common].[Person].[IdentificationNumber] = @IdentificationNumber) AND [PersonFreeTimeUse].FreeTimeUseId = @FreeTimeUseId))
	
	IF @Count = 0
	BEGIN
		INSERT INTO [Common].[PersonFreeTimeUse] (PersonId, FreeTimeUseId) VALUES ((Select [Person].[Id] from [Common].[Person] WHERE [Common].[Person].[IdentificationNumber] = @IdentificationNumber) ,@FreeTimeUseId);
		/*
		SET @Result = 1
		RETURN(1)*/
		SELECT '001' AS CodeMessage, 'Se insertó correctamente' as Mensaje
	END
	ELSE
	BEGIN 
	/*
		SET @Result = 0	
		RETURN(0)*/
		SELECT '999' AS CodeMessage, 'No se insertó la Actividad porque ya existe' as Mensaje
	END

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra una nueva actividad de tiempo libre asociada a una persona, identificada por su número de cédula o documento de identidad. Antes de insertar, verifica si la combinación persona-actividad ya existe en el sistema para evitar duplicados; si ya está registrada, informa que no se realizó la operación. Utiliza la tabla maestra de personas para obtener el identificador interno a partir del número de identificación, y luego vincula ese identificador con la actividad de tiempo libre en la tabla de relación. Se usa en el módulo de caracterización socioeconómica o de perfil del paciente para registrar sus actividades o usos del tiempo libre.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'PROCEDURE', @level1name = N'SP_InsertPersonFreeTimeUseInformation';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'PROCEDURE', @level1name = N'SP_InsertPersonFreeTimeUseInformation';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Registra la asociación entre una persona (identificada por su número de documento) y una actividad de uso del tiempo libre, evitando duplicados e informando el resultado mediante un código de mensaje.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_InsertPersonFreeTimeUseInformation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir una persona en Common.Person cuyo IdentificationNumber coincida con el valor recibido; en caso contrario el PersonId resuelto será NULL; El identificador de la actividad de tiempo libre debe ser válido para la tabla de actividades', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_InsertPersonFreeTimeUseInformation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'No se permiten duplicados de la combinación persona + actividad de tiempo libre; El identificador interno de la persona se obtiene siempre a partir del número de identificación contra el maestro de personas; La operación es idempotente: si ya existe la asociación, no se realiza ninguna modificación', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_InsertPersonFreeTimeUseInformation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Persona; Identificación de persona; Actividad de tiempo libre; Caracterización socioeconómica', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_InsertPersonFreeTimeUseInformation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] Common.PersonFreeTimeUse: Cuando no existe ya un registro con el mismo PersonId (resuelto desde IdentificationNumber) y FreeTimeUseId, se inserta la nueva asociación; [RETURN_RESULT] Common.PersonFreeTimeUse: Si la inserción se realizó, devuelve un resultset con CodeMessage=''001'' y mensaje ''Se insertó correctamente''; si ya existía, devuelve CodeMessage=''999'' y mensaje ''No se insertó la Actividad porque ya existe''', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_InsertPersonFreeTimeUseInformation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si No existe registro previo en PersonFreeTimeUse para la persona y la actividad de tiempo libre indicadas (@Count = 0) → Inserta la asociación persona-actividad y retorna mensaje ''001'' indicando inserción correcta else Retorna mensaje ''999'' indicando que la actividad ya existe y no se insertó', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_InsertPersonFreeTimeUseInformation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Common.PersonFreeTimeUse; Common.Person', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_InsertPersonFreeTimeUseInformation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_InsertPersonFreeTimeUseInformation';
-- GO
