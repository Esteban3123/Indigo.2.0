-- =============================================  
-- Author:  <Author,,Name>  
-- ALTER date: <ALTER Date,,>  
-- Description: <Description,,>  
-- =============================================  
CREATE PROCEDURE [Common].[InsertPersonFreeTimeUseInformation]   
-- Add the parameters for the stored procedure here  
@IdentificationNumber VARCHAR(20), 
@FreeTimeUseId        INT
AS
    BEGIN  
        -- SET NOCOUNT ON added to prevent extra result sets from  
        -- interfering with SELECT statements.  
        SET NOCOUNT ON;

        -- Insert statements for procedure here  

        DECLARE @Count INT;
        DECLARE @Result INT;
        SET @Count =
        (
            SELECT COUNT(*)
            FROM [Common].[PersonFreeTimeUse]
            WHERE([PersonFreeTimeUse].[PersonId] =
            (
                SELECT [Person].[Id]
                FROM [Common].[Person]
                WHERE [Common].[Person].[IdentificationNumber] = @IdentificationNumber
            )
                  AND [PersonFreeTimeUse].FreeTimeUseId = @FreeTimeUseId)
        );
        IF @Count = 0
            BEGIN
                INSERT INTO [Common].[PersonFreeTimeUse]
                (PersonId, 
                 FreeTimeUseId
                )
                VALUES
                (
                (
                    SELECT [Person].[Id]
                    FROM [Common].[Person]
                    WHERE [Common].[Person].[IdentificationNumber] = @IdentificationNumber
                ), 
                @FreeTimeUseId
                );

/*  
  SET @Result = 1  
  RETURN(1)*/

                SELECT '001' AS CodeMessage, 
                       'Se insertó correctamente' AS Mensaje;
        END;
            ELSE
            BEGIN

/*  
  SET @Result = 0   
  RETURN(0)*/

                SELECT '999' AS CodeMessage, 
                       'No se insertó la Actividad porque ya existe' AS Mensaje;
        END;
    END;
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra una nueva actividad de tiempo libre asociada a una persona, identificada por su número de documento (cédula). Busca a la persona en el maestro de personas usando el número de identificación, y si la combinación persona-actividad no existe previamente, la inserta en el registro de actividades de tiempo libre. Retorna un mensaje de éxito (''001'') si la inserción fue exitosa, o un aviso (''999'') si la actividad ya estaba registrada para esa persona, evitando duplicados.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'PROCEDURE', @level1name = N'InsertPersonFreeTimeUseInformation';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'PROCEDURE', @level1name = N'InsertPersonFreeTimeUseInformation';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Registra la asociación entre una persona (identificada por su número de documento) y una actividad de uso del tiempo libre, evitando duplicados.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'InsertPersonFreeTimeUseInformation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El número de identificación debe corresponder a una persona existente en Common.Person para que el Id resuelto no sea NULL; El identificador de actividad de tiempo libre debe ser válido conforme a la integridad referencial de PersonFreeTimeUse', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'InsertPersonFreeTimeUseInformation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'No se permiten duplicados de la combinación (PersonId, FreeTimeUseId) en PersonFreeTimeUse; La persona se identifica siempre a través de su número de identificación, no por su Id directo; Siempre se devuelve un resultado con código de mensaje (''001'' éxito o ''999'' duplicado)', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'InsertPersonFreeTimeUseInformation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Persona; Identificación de persona; Uso del tiempo libre; Actividad de tiempo libre', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'InsertPersonFreeTimeUseInformation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] Common.PersonFreeTimeUse: Cuando no existe registro previo con el mismo PersonId (resuelto desde IdentificationNumber) y FreeTimeUseId, se inserta la nueva asociación; [RETURN_RESULT] (resultset): Si se insertó, retorna CodeMessage ''001'' con mensaje ''Se insertó correctamente''; si ya existía, retorna ''999'' con ''No se insertó la Actividad porque ya existe''', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'InsertPersonFreeTimeUseInformation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si No existe registro previo en PersonFreeTimeUse para la persona (resuelta por número de identificación) y la actividad de tiempo libre indicada → Inserta la asociación persona-actividad y retorna código ''001'' con mensaje de éxito else No inserta y retorna código ''999'' indicando que la actividad ya existe', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'InsertPersonFreeTimeUseInformation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Common.PersonFreeTimeUse; Common.Person', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'InsertPersonFreeTimeUseInformation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'InsertPersonFreeTimeUseInformation';
-- GO
