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



GO
CREATE NONCLUSTERED INDEX [IX_Billing_ElectronicsRIPS_ElectronicsPropertiesId]
    ON [Billing].[ElectronicsRIPS]([ElectronicsPropertiesId] ASC)
    INCLUDE([Id], [sendDate], [CosmoDBId]) WITH (FILLFACTOR = 90);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la última modificación del registro RIPS. DATETIME nullable. Trazabilidad de actualizaciones post-radicación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicsRIPS', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicsRIPS', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicsRIPS', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario del sistema que realizó la última modificación al registro RIPS. Auditoría de cambios. VARCHAR(20).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicsRIPS', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicsRIPS', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicsRIPS', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro RIPS en la base de datos. DATETIME. Marca inicio del ciclo de facturación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicsRIPS', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicsRIPS', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicsRIPS', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario del sistema (login/operador) que creó el registro RIPS. Auditoría de origen. VARCHAR(20).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicsRIPS', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicsRIPS', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicsRIPS', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ruta o path completo donde se almacenó el archivo .json del RIPS electrónico en el sistema de archivos. VARCHAR(250).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicsRIPS', @level2type = N'COLUMN', @level2name = N'FilePath';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ruta donde se guardo el archivo .json', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicsRIPS', @level2type = N'COLUMN', @level2name = N'FilePath';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicsRIPS', @level2type = N'COLUMN', @level2name = N'FilePath';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del documento almacenado en la base de datos NoSQL CosmoDB (referencia externa). Usado para trazabilidad y auditoría de RIPS. VARCHAR(1024).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicsRIPS', @level2type = N'COLUMN', @level2name = N'CosmoDBId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro guardado en la base de datos NOSQL CosmoDB', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicsRIPS', @level2type = N'COLUMN', @level2name = N'CosmoDBId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicsRIPS', @level2type = N'COLUMN', @level2name = N'CosmoDBId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contador de reintentos de validación del RIPS. Indica cuántas veces se ha intentado procesar o revalidar el archivo de facturación electrónica. INT.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicsRIPS', @level2type = N'COLUMN', @level2name = N'Retry';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de reintentos de validacion del rips', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicsRIPS', @level2type = N'COLUMN', @level2name = N'Retry';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicsRIPS', @level2type = N'COLUMN', @level2name = N'Retry';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora del envío a validación/procesamiento del RIPS electrónico. Marca el inicio del ciclo de validación de factura.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicsRIPS', @level2type = N'COLUMN', @level2name = N'sendDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del envio a validacion del RIPS', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicsRIPS', @level2type = N'COLUMN', @level2name = N'sendDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicsRIPS', @level2type = N'COLUMN', @level2name = N'sendDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de radicación oficial del RIPS ante la entidad reguladora (MINSALUD/ADRES). DATETIME.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicsRIPS', @level2type = N'COLUMN', @level2name = N'RadicateDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de radicacion del RIPS', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicsRIPS', @level2type = N'COLUMN', @level2name = N'RadicateDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicsRIPS', @level2type = N'COLUMN', @level2name = N'RadicateDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK al registro de configuración/propiedades del RIPS en ElectronicsProperties. Referencia la estructura y metadatos del envío de factura electrónica.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicsRIPS', @level2type = N'COLUMN', @level2name = N'ElectronicsPropertiesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro de RIPS', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicsRIPS', @level2type = N'COLUMN', @level2name = N'ElectronicsPropertiesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicsRIPS', @level2type = N'COLUMN', @level2name = N'ElectronicsPropertiesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (PK) del registro RIPS electrónico en la tabla ElectronicsRIPS. IDENTITY INT.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicsRIPS', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador único del RIPS electrónico.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicsRIPS', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicsRIPS', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de envíos electrónicos de RIPS (Registros Individuales de Prestación de Servicios de Salud). Guarda el historial de radicaciones y reenvíos de archivos RIPS enviados electrónicamente, incluyendo fechas, rutas de archivo y referencias en base de datos documental (CosmoDB).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicsRIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicsRIPS';
