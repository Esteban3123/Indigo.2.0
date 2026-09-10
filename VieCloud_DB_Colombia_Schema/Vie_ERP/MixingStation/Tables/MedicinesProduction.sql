CREATE TABLE [MixingStation].[MedicinesProduction] (
    [Id]                INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CMConfigurationId] INT       NOT NULL,
    [ATCId]             INT       NOT NULL,
    [UnitDoseTypeId]    INT       NOT NULL,
    [Status]            BIT       NOT NULL,
    [CenterAttentionId] CHAR (10) NULL,
    [AllowsRemnant]     BIT       CONSTRAINT [DF__Medicines__Allow__62F5F1EC] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_MedicinesProduction] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MedicinesProduction_ATC] FOREIGN KEY ([ATCId]) REFERENCES [Inventory].[ATC] ([Id]),
    CONSTRAINT [FK_MedicinesProduction_CMConfiguration] FOREIGN KEY ([CMConfigurationId]) REFERENCES [MixingStation].[CMConfiguration] ([Id]),
    CONSTRAINT [FK_MedicinesProduction_UnitDoseType] FOREIGN KEY ([UnitDoseTypeId]) REFERENCES [MixingStation].[UnitDoseType] ([Id])
);




GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Permite remitente/remanente (1=Sí, 0=No). Indica si se autoriza guardar dosis sobrante de medicamento tras producción en central de mezclas. Tipo: BIT.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MedicinesProduction', @level2type = N'COLUMN', @level2name = N'AllowsRemnant';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'permite remitente 1 = si  0 = No', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MedicinesProduction', @level2type = N'COLUMN', @level2name = N'AllowsRemnant';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MedicinesProduction', @level2type = N'COLUMN', @level2name = N'AllowsRemnant';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del Centro de Atención, Unidad Funcional, Sucursal o Sede donde se produce el medicamento. Tipo: CHAR(10). Referencia a instalación de salud.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MedicinesProduction', @level2type = N'COLUMN', @level2name = N'CenterAttentionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MedicinesProduction', @level2type = N'COLUMN', @level2name = N'CenterAttentionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MedicinesProduction', @level2type = N'COLUMN', @level2name = N'CenterAttentionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del registro: activo/inactivo (1=Activo, 0=Inactivo). Controla si la configuración de producción está habilitada en central de mezclas. Tipo: BIT.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MedicinesProduction', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del registro', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MedicinesProduction', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MedicinesProduction', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del Tipo de Dosis Unitaria (FK a MixingStation.UnitDoseType). Define forma farmacéutica: comprimido, cápsula, ámpula, jeringa, etc. Tipo: INT.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MedicinesProduction', @level2type = N'COLUMN', @level2name = N'UnitDoseTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tipo de dosis unitaria', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MedicinesProduction', @level2type = N'COLUMN', @level2name = N'UnitDoseTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MedicinesProduction', @level2type = N'COLUMN', @level2name = N'UnitDoseTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del Medicamento según clasificación ATC (FK a Inventory.ATC). Código anatómico-terapéutico-químico del fármaco en producción. Tipo: INT.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MedicinesProduction', @level2type = N'COLUMN', @level2name = N'ATCId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del medicamento', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MedicinesProduction', @level2type = N'COLUMN', @level2name = N'ATCId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MedicinesProduction', @level2type = N'COLUMN', @level2name = N'ATCId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la Central de Mezclas/Configuración (FK a MixingStation.CMConfiguration). Identifica equipo, línea o farmacia que produce dosis unitarias. Tipo: INT.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MedicinesProduction', @level2type = N'COLUMN', @level2name = N'CMConfigurationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la central de mezclas', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MedicinesProduction', @level2type = N'COLUMN', @level2name = N'CMConfigurationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MedicinesProduction', @level2type = N'COLUMN', @level2name = N'CMConfigurationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de Producción de Medicamentos. Clave primaria IDENTITY. Tipo: INT.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MedicinesProduction', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MedicinesProduction', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MedicinesProduction', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Producción de medicamentos en la estación de mezclas: registra qué medicamentos (por clasificación ATC) se producen bajo una configuración de dosis unitaria, en qué centro de atención y si se permite el manejo de remanentes o sobrantes.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MedicinesProduction';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MedicinesProduction';
