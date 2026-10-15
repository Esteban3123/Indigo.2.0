CREATE TABLE [MixingStation].[ExternalCareCenterUsers] (
    [Id]                   INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ExternalCareCenterId] INT          NOT NULL,
    [UserId]               INT          NOT NULL,
    [UserCode]             VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_ExternalCareCenterUsers] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ExternalCareCenterUsers_ExternalCareCenter] FOREIGN KEY ([ExternalCareCenterId]) REFERENCES [MixingStation].[ExternalCareCenter] ([Id])
);


GO
ALTER TABLE [MixingStation].[ExternalCareCenterUsers] NOCHECK CONSTRAINT [FK_ExternalCareCenterUsers_ExternalCareCenter];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del usuario de seguridad', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ExternalCareCenterUsers', @level2type = N'COLUMN', @level2name = N'UserCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del usuario que tiene permiso , el usuario de seguridad', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ExternalCareCenterUsers', @level2type = N'COLUMN', @level2name = N'UserId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ExternalCareCenterUsers', @level2type = N'COLUMN', @level2name = N'ExternalCareCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ExternalCareCenterUsers', @level2type = N'COLUMN', @level2name = N'Id';

