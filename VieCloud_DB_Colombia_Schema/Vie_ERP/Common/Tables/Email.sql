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
ADD SENSITIVITY CLASSIFICATION TO
    [Common].[Email].[Email]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Contact Info');


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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de correo electrónico según uso: 1=Notificaciones varias del sistema, 2=Notificación de Facturación Electrónica (RIPS, factura). Clasificación de destinatario: paciente, profesional de salud, centro de atención.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Email', @level2type = N'COLUMN', @level2name = N'Type';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de correo electrónico, el cual depende de cual será su uso:  1. Notificaciones varias  2. Notificación Facturación Electrónica', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Email', @level2type = N'COLUMN', @level2name = N'Type';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Email', @level2type = N'COLUMN', @level2name = N'Type';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de sincronización con bases de datos externas: 1=Sincronizado, 0=No sincronizado. Estado de réplica de correo en sistemas remotos.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Email', @level2type = N'COLUMN', @level2name = N'Synchronized';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1-Estado ni Sincronizado 2- Estado no Sincronizado', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Email', @level2type = N'COLUMN', @level2name = N'Synchronized';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Email', @level2type = N'COLUMN', @level2name = N'Synchronized';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bit de estado lógico: 1=Activo, 0=Eliminado/Inactivo. Marca exclusión lógica para sincronización de base de datos y réplicas.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Email', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado Eliminado para Sincronizacion de DB', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Email', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Email', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dirección de correo electrónico (PII enmascarado). Campo: VARCHAR(100) con máscara tipo email(). Contacto principal para notificaciones, facturación electrónica y comunicaciones del centro de atención.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Email', @level2type = N'COLUMN', @level2name = N'Email';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Correo electronico', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Email', @level2type = N'COLUMN', @level2name = N'Email';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Email', @level2type = N'COLUMN', @level2name = N'Email';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador numérico de la Persona (paciente, profesional de salud o usuario). Llave foránea a tabla [Common].[Person]. Vincula correo a entidad del sistema.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Email', @level2type = N'COLUMN', @level2name = N'IdPerson';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Persona ', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Email', @level2type = N'COLUMN', @level2name = N'IdPerson';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Email', @level2type = N'COLUMN', @level2name = N'IdPerson';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único auto-incremental (IDENTITY) de la tabla Email. Clave primaria. No se replica en servidores secundarios (NOT FOR REPLICATION).', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Email', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Email', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Email', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda las direcciones de correo electrónico registradas para las personas en el sistema, indicando el tipo de email, su estado activo/inactivo y si fue sincronizado con otros sistemas.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Email';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Email';
