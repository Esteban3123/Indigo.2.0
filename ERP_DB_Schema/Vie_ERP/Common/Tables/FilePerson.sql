CREATE TABLE [Common].[FilePerson] (
    [Id]           UNIQUEIDENTIFIER ROWGUIDCOL NOT NULL,
    [IdPerson]     INT              NOT NULL,
    [Photo]        VARBINARY (MAX)  NULL,
    [Signature]    VARBINARY (MAX)  NULL,
    [TimeStamp]    ROWVERSION       NOT NULL,
    [State]        BIT              NOT NULL,
    [Synchronized] CHAR (1)         NOT NULL,
    CONSTRAINT [PK_FilesPerson] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FilesPerson_Person] FOREIGN KEY ([IdPerson]) REFERENCES [Common].[Person] ([Id])
);


GO
ALTER TABLE [Common].[FilePerson] NOCHECK CONSTRAINT [FK_FilesPerson_Person];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1-Estado ni Sincronizado 2- Estado no Sincronizado', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'FilePerson', @level2type = N'COLUMN', @level2name = N'Synchronized';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado Eliminado para Sincronizacion de DB', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'FilePerson', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Control Concurrencia', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'FilePerson', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Firma del Profesional', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'FilePerson', @level2type = N'COLUMN', @level2name = N'Signature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Foto del Profesional', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'FilePerson', @level2type = N'COLUMN', @level2name = N'Photo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id persona ', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'FilePerson', @level2type = N'COLUMN', @level2name = N'IdPerson';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'FilePerson', @level2type = N'COLUMN', @level2name = N'Id';

