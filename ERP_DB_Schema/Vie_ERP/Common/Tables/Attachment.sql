CREATE TABLE [Common].[Attachment] (
    [Id]               INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [FormId]           INT             NOT NULL,
    [EntityName]       VARCHAR (250)   NOT NULL,
    [EntityId]         INT             NOT NULL,
    [Name]             VARCHAR (250)   NOT NULL,
    [Extension]        VARCHAR (10)    NOT NULL,
    [Description]      VARCHAR (MAX)   NOT NULL,
    [FileAttached]     VARBINARY (MAX) NOT NULL,
    [CreationUser]     VARCHAR (20)    NOT NULL,
    [CreationDate]     DATETIME        NOT NULL,
    [EntityDetailId]   INT             NULL,
    [EntityDetailName] VARCHAR (250)   NULL,
    CONSTRAINT [PK_Common_Attachment] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de la entidad del detalle', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Attachment', @level2type = N'COLUMN', @level2name = N'EntityDetailName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del registro de la entidad detalle', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Attachment', @level2type = N'COLUMN', @level2name = N'EntityDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de subida del archivo', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Attachment', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario que sube el archivo', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Attachment', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Archivo adjunto', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Attachment', @level2type = N'COLUMN', @level2name = N'FileAttached';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripción agregada al archivo', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Attachment', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Extensión del archivo', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Attachment', @level2type = N'COLUMN', @level2name = N'Extension';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del archivo', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Attachment', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del registro de la entidad', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Attachment', @level2type = N'COLUMN', @level2name = N'EntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de la entidad', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Attachment', @level2type = N'COLUMN', @level2name = N'EntityName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del Formulario', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Attachment', @level2type = N'COLUMN', @level2name = N'FormId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Attachment', @level2type = N'COLUMN', @level2name = N'Id';

