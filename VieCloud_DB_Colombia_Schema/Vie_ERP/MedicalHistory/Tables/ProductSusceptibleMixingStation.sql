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
CREATE NONCLUSTERED INDEX [IX_ProductSusceptibleMixingStation_Origin_IdOrigin] ON [MedicalHistory].[ProductSusceptibleMixingStation] ([Origin] ASC, [IdOrigin] ASC)
INCLUDE ([CodeSusceptibleMixingStation]);
GO
CREATE NONCLUSTERED INDEX [IX_ProductSusceptibleMixingStation_CodeSusceptibleMixingStation] ON [MedicalHistory].[ProductSusceptibleMixingStation] ([CodeSusceptibleMixingStation] ASC)
INCLUDE ([Origin], [IdOrigin]);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de mezcla susceptible, timestamp de auditoría (DATETIME)', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ProductSusceptibleMixingStation', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la fecha de creación', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ProductSusceptibleMixingStation', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ProductSusceptibleMixingStation', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador del profesional de salud que realizó la mezcla o aplicación (CHAR 20, FK implícita)', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ProductSusceptibleMixingStation', @level2type = N'COLUMN', @level2name = N'ProfessionalCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Código Profesional', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ProductSusceptibleMixingStation', @level2type = N'COLUMN', @level2name = N'ProfessionalCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ProductSusceptibleMixingStation', @level2type = N'COLUMN', @level2name = N'ProfessionalCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la unidad funcional u área operativa donde se preparó la mezcla (CHAR 10)', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ProductSusceptibleMixingStation', @level2type = N'COLUMN', @level2name = N'FunctionalUnitCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el código de la unidad funcional', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ProductSusceptibleMixingStation', @level2type = N'COLUMN', @level2name = N'FunctionalUnitCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ProductSusceptibleMixingStation', @level2type = N'COLUMN', @level2name = N'FunctionalUnitCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención, sede o institución sanitaria que gestiona la mezcla (CHAR 10)', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ProductSusceptibleMixingStation', @level2type = N'COLUMN', @level2name = N'CenterAttentionCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el código de centro de atención', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ProductSusceptibleMixingStation', @level2type = N'COLUMN', @level2name = N'CenterAttentionCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ProductSusceptibleMixingStation', @level2type = N'COLUMN', @level2name = N'CenterAttentionCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del medicamento principal según clasificación ATC (VARCHAR 20, FK a Inventory.ATC), sinónimo: principio activo', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ProductSusceptibleMixingStation', @level2type = N'COLUMN', @level2name = N'MainDrugCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el  código del medicamento principal', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ProductSusceptibleMixingStation', @level2type = N'COLUMN', @level2name = N'MainDrugCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ProductSusceptibleMixingStation', @level2type = N'COLUMN', @level2name = N'MainDrugCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad total de aplicaciones o dosis del producto susceptible mezclado (INT, entero positivo)', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ProductSusceptibleMixingStation', @level2type = N'COLUMN', @level2name = N'ApplicationsNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Número de aplicaciones', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ProductSusceptibleMixingStation', @level2type = N'COLUMN', @level2name = N'ApplicationsNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ProductSusceptibleMixingStation', @level2type = N'COLUMN', @level2name = N'ApplicationsNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre completo y detallado del producto susceptible de mezcla, incluyendo presentación y concentración (VARCHAR 300)', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ProductSusceptibleMixingStation', @level2type = N'COLUMN', @level2name = N'FullProductName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Nombre completo del producto', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ProductSusceptibleMixingStation', @level2type = N'COLUMN', @level2name = N'FullProductName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ProductSusceptibleMixingStation', @level2type = N'COLUMN', @level2name = N'FullProductName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador numérico de la fuente u origen del producto susceptible (INT, referencia a tabla relacionada)', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ProductSusceptibleMixingStation', @level2type = N'COLUMN', @level2name = N'IdOrigin';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Id de origen', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ProductSusceptibleMixingStation', @level2type = N'COLUMN', @level2name = N'IdOrigin';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ProductSusceptibleMixingStation', @level2type = N'COLUMN', @level2name = N'IdOrigin';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación o tipo de origen del producto susceptible, ej: interno, externo, proveedor (VARCHAR 20)', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ProductSusceptibleMixingStation', @level2type = N'COLUMN', @level2name = N'Origin';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Origen productos susceptible', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ProductSusceptibleMixingStation', @level2type = N'COLUMN', @level2name = N'Origin';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ProductSusceptibleMixingStation', @level2type = N'COLUMN', @level2name = N'Origin';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (GUID) que agrupa y rastrea el lote o paquete de mezcla susceptible (UNIQUEIDENTIFIER, PK funcional)', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ProductSusceptibleMixingStation', @level2type = N'COLUMN', @level2name = N'CodeSusceptibleMixingStation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo identificador del agrupador por paquete', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ProductSusceptibleMixingStation', @level2type = N'COLUMN', @level2name = N'CodeSusceptibleMixingStation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ProductSusceptibleMixingStation', @level2type = N'COLUMN', @level2name = N'CodeSusceptibleMixingStation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador secuencial único y consecutivo de cada registro en la tabla (INT IDENTITY, PK)', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ProductSusceptibleMixingStation', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el consecutivo de la tabla ', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ProductSusceptibleMixingStation', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ProductSusceptibleMixingStation', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de productos o medicamentos susceptibles de mezcla en estación de preparación, indicando su origen (orden médica, receta, etc.), el nombre completo del producto, la cantidad de aplicaciones, el medicamento principal asociado y el profesional, unidad funcional y centro de atención que generaron la solicitud.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ProductSusceptibleMixingStation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ProductSusceptibleMixingStation';
