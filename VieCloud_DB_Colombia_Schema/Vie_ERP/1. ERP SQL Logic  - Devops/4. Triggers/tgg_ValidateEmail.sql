/*==============================================================================================================================
	Author: Oscar steven Astudillo
	BUG : #20970
	Sprint : SCM_week_40-42 (2024)
==============================================================================================================================*/
CREATE TRIGGER tgg_ValidateEmail
ON [Common].[Email]
INSTEAD OF INSERT, UPDATE
AS
BEGIN
    -- Validar que no se inserten o actualicen valores que contengan espacios
    IF EXISTS (
        SELECT *
        FROM inserted
        WHERE Email LIKE '% %'               -- Espacios en cualquier parte
           OR Email <> LTRIM(RTRIM(Email))   -- Espacios al inicio o al final
    )
    BEGIN
        RAISERROR('El valor de Email no puede contener espacios al inicio, al final o en ninguna parte.', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END


    IF EXISTS (SELECT 1 FROM inserted WHERE Id  > 0 AND Id IS NOT NULL )
    BEGIN
        -- Manejar update
        UPDATE e
        SET 
			e.IdPerson = i.IdPerson,
            e.Email = i.Email,
            e.State = i.State,
            e.Synchronized = i.Synchronized,
            e.Type = i.Type
        FROM [Common].[Email] e
        INNER JOIN inserted i ON e.Id = i.Id;
    END
    ELSE
    BEGIN
        -- Manejo de Insert
        INSERT INTO [Common].[Email] (IdPerson, Email, State, Synchronized, Type)
        SELECT IdPerson, Email, State, Synchronized, Type
        FROM inserted WHERE (Id IS NULL OR Id = 0);
    END
END;

