CREATE TABLE [Billing].[ElectronicsRIPS] (
    [Id]                      INT            IDENTITY (1, 1) NOT NULL,
    [ElectronicsPropertiesId] INT            NOT NULL,
    [RadicateDate]            DATETIME       NOT NULL,
    [sendDate]                DATETIME       NOT NULL,
    [Retry]                   INT            NOT NULL,
    [CosmoDBId]               VARCHAR (1024) NOT NULL,
    [FilePath]                VARCHAR (250)  NULL,
    [CreationUser]            VARCHAR (20)   NOT NULL,
    [CreationDate]            DATETIME       NOT NULL,
    [ModificationUser]        VARCHAR (20)   NULL,
    [ModificationDate]        DATETIME       NULL,
    CONSTRAINT [PK_ElectronicRIPS] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ElectronicsRIPS_ElectronicsProperties] FOREIGN KEY ([ElectronicsPropertiesId]) REFERENCES [Billing].[ElectronicsProperties] ([Id])
);


GO
ALTER TABLE [Billing].[ElectronicsRIPS] NOCHECK CONSTRAINT [FK_ElectronicsRIPS_ElectronicsProperties];


GO
CREATE NONCLUSTERED INDEX [IX_Billing_ElectronicsRIPS_ElectronicsPropertiesId]
    ON [Billing].[ElectronicsRIPS]([ElectronicsPropertiesId] ASC)
    INCLUDE([Id], [sendDate], [CosmoDBId]) WITH (FILLFACTOR = 90);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicsRIPS', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicsRIPS', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicsRIPS', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicsRIPS', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ruta donde se guardo el archivo .json', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicsRIPS', @level2type = N'COLUMN', @level2name = N'FilePath';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro guardado en la base de datos NOSQL CosmoDB', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicsRIPS', @level2type = N'COLUMN', @level2name = N'CosmoDBId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de reintentos de validacion del rips', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicsRIPS', @level2type = N'COLUMN', @level2name = N'Retry';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha del envio a validacion del RIPS', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicsRIPS', @level2type = N'COLUMN', @level2name = N'sendDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de radicacion del RIPS', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicsRIPS', @level2type = N'COLUMN', @level2name = N'RadicateDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro de RIPS', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicsRIPS', @level2type = N'COLUMN', @level2name = N'ElectronicsPropertiesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del RIPS electrónico.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicsRIPS', @level2type = N'COLUMN', @level2name = N'Id';

