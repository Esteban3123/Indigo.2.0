CREATE TABLE [RegulatoryEngine].[RegulatoryPack] (
    [Id]               BIGINT         IDENTITY (1, 1) NOT NULL,
    [Code]             NVARCHAR (100) NOT NULL,
    [Name]             NVARCHAR (300) NOT NULL,
    [JurisdictionCode] NVARCHAR (20)  NOT NULL,
    [Version]          NVARCHAR (50)  NOT NULL,
    [EffectiveFrom]    DATE           NOT NULL,
    [EffectiveTo]      DATE           NULL,
    [IsActive]         BIT            DEFAULT ((1)) NOT NULL,
    [CreatedAt]        DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [UpdatedAt]        DATETIME2 (7)  NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_RegulatoryPack_CodeVersion]
    ON [RegulatoryEngine].[RegulatoryPack]([Code] ASC, [Version] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Agrupa un conjunto de reglas regulatorias bajo una jurisdicción y versión específica. Un pack representa un marco normativo completo (ej: RIPS Colombia 2024, SOAT Colombia). Todas las reglas de un pack comparten jurisdicción y vigencia. El par Code+Version es único, lo que permite versionar packs normativos sin perder el historial.', @level0type = N'SCHEMA', @level0name = N'RegulatoryEngine', @level1type = N'TABLE', @level1name = N'RegulatoryPack';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del pack regulatorio.', @level0type = N'SCHEMA', @level0name = N'RegulatoryEngine', @level1type = N'TABLE', @level1name = N'RegulatoryPack', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del pack regulatorio (ej: RIPS_CO, SOAT_CO, MIPRES_CO). Junto con Version forma la clave de negocio única del pack. Se usa para identificar el pack desde los servicios sin depender del Id.', @level0type = N'SCHEMA', @level0name = N'RegulatoryEngine', @level1type = N'TABLE', @level1name = N'RegulatoryPack', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre descriptivo del pack regulatorio (ej: RIPS Colombia 2024, SOAT Colombia v2).', @level0type = N'SCHEMA', @level0name = N'RegulatoryEngine', @level1type = N'TABLE', @level1name = N'RegulatoryPack', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la jurisdicción o país al que aplica el pack (ej: CO para Colombia, MX para México, PE para Perú). Permite filtrar packs relevantes por país en implementaciones multi-jurisdicción.', @level0type = N'SCHEMA', @level0name = N'RegulatoryEngine', @level1type = N'TABLE', @level1name = N'RegulatoryPack', @level2type = N'COLUMN', @level2name = N'JurisdictionCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Versión del pack regulatorio (ej: 2024.1, 2025.0). Junto con Code forma la clave de negocio única. Permite mantener múltiples versiones activas en simultáneo durante períodos de transición normativa.', @level0type = N'SCHEMA', @level0name = N'RegulatoryEngine', @level1type = N'TABLE', @level1name = N'RegulatoryPack', @level2type = N'COLUMN', @level2name = N'Version';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha desde la cual el pack regulatorio está vigente (inclusive). Las reglas del pack solo aplican a partir de esta fecha.', @level0type = N'SCHEMA', @level0name = N'RegulatoryEngine', @level1type = N'TABLE', @level1name = N'RegulatoryPack', @level2type = N'COLUMN', @level2name = N'EffectiveFrom';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de vencimiento del pack (inclusive). NULL indica que el pack está vigente indefinidamente. Cuando se supera esta fecha el pack debe desactivarse o ser reemplazado por una versión más reciente.', @level0type = N'SCHEMA', @level0name = N'RegulatoryEngine', @level1type = N'TABLE', @level1name = N'RegulatoryPack', @level2type = N'COLUMN', @level2name = N'EffectiveTo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si el pack está activo. Permite deshabilitar un pack completo sin eliminar sus reglas ni su historial de validación.', @level0type = N'SCHEMA', @level0name = N'RegulatoryEngine', @level1type = N'TABLE', @level1name = N'RegulatoryPack', @level2type = N'COLUMN', @level2name = N'IsActive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro.', @level0type = N'SCHEMA', @level0name = N'RegulatoryEngine', @level1type = N'TABLE', @level1name = N'RegulatoryPack', @level2type = N'COLUMN', @level2name = N'CreatedAt';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la última modificación del registro. NULL si nunca ha sido modificado.', @level0type = N'SCHEMA', @level0name = N'RegulatoryEngine', @level1type = N'TABLE', @level1name = N'RegulatoryPack', @level2type = N'COLUMN', @level2name = N'UpdatedAt';
