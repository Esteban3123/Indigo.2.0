CREATE TABLE [MedicalHistory].[ProductSusceptibleMixingStation] (
    [Id]                           INT              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CodeSusceptibleMixingStation] UNIQUEIDENTIFIER NOT NULL,
    [Origin]                       VARCHAR (20)     NOT NULL,
    [IdOrigin]                     INT              NOT NULL,
    [FullProductName]              VARCHAR (300)    NOT NULL,
    [ApplicationsNumber]           INT              NOT NULL,
    [MainDrugCode]                 VARCHAR (20)     NULL,
    [CenterAttentionCode]          CHAR (10)        NOT NULL,
    [FunctionalUnitCode]           CHAR (10)        NOT NULL,
    [ProfessionalCode]             CHAR (20)        NOT NULL,
    [CreationDate]                 DATETIME         NOT NULL,
    CONSTRAINT [PK_ProductSusceptibleMixingStation] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProductSusceptibleMixingStation_ATC] FOREIGN KEY ([MainDrugCode]) REFERENCES [Inventory].[ATC] ([Code])
);


GO
ALTER TABLE [MedicalHistory].[ProductSusceptibleMixingStation] NOCHECK CONSTRAINT [FK_ProductSusceptibleMixingStation_ATC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la fecha de creación', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ProductSusceptibleMixingStation', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Código Profesional', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ProductSusceptibleMixingStation', @level2type = N'COLUMN', @level2name = N'ProfessionalCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el código de la unidad funcional', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ProductSusceptibleMixingStation', @level2type = N'COLUMN', @level2name = N'FunctionalUnitCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el código de centro de atención', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ProductSusceptibleMixingStation', @level2type = N'COLUMN', @level2name = N'CenterAttentionCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el  código del medicamento principal', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ProductSusceptibleMixingStation', @level2type = N'COLUMN', @level2name = N'MainDrugCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Número de aplicaciones', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ProductSusceptibleMixingStation', @level2type = N'COLUMN', @level2name = N'ApplicationsNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Nombre completo del producto', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ProductSusceptibleMixingStation', @level2type = N'COLUMN', @level2name = N'FullProductName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Id de origen', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ProductSusceptibleMixingStation', @level2type = N'COLUMN', @level2name = N'IdOrigin';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Origen productos susceptible', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ProductSusceptibleMixingStation', @level2type = N'COLUMN', @level2name = N'Origin';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo identificador del agrupador por paquete', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ProductSusceptibleMixingStation', @level2type = N'COLUMN', @level2name = N'CodeSusceptibleMixingStation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el consecutivo de la tabla ', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ProductSusceptibleMixingStation', @level2type = N'COLUMN', @level2name = N'Id';

