CREATE TABLE [Common].[Email] (
    [Id]           INT                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdPerson]     INT                                              NOT NULL,
    [Email]        VARCHAR (100) MASKED WITH (FUNCTION = 'email()') NOT NULL,
    [State]        BIT                                              NOT NULL,
    [Synchronized] CHAR (1)                                         NOT NULL,
    [Type]         TINYINT                                          CONSTRAINT [DF_Email_Type] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_Emails] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Emails_Person] FOREIGN KEY ([IdPerson]) REFERENCES [Common].[Person] ([Id])
);


GO
ALTER TABLE [Common].[Email] NOCHECK CONSTRAINT [FK_Emails_Person];


GO
CREATE NONCLUSTERED INDEX [IX_Email_IdPerson]
    ON [Common].[Email]([IdPerson] ASC)
    INCLUDE([Email], [State], [Synchronized], [Type]);


GO

CREATE TRIGGER [Common].[tgg_ValidateEmail]
ON [Common].[Email]
AFTER   INSERT, UPDATE
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
        RAISERROR('Mensaje desde trigger de la tabla Common.Email: El valor de email no puede contener espacios al inicio, al final o en ninguna parte.', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END
END;
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de correo electrónico, el cual depende de cual será su uso:  1. Notificaciones varias  2. Notificación Facturación Electrónica', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Email', @level2type = N'COLUMN', @level2name = N'Type';

