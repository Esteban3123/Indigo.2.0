CREATE TABLE [Maintenance].[TechnicalEquipmentSheet] (
    [Id]                   INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Terms]                VARCHAR (8000) NOT NULL,
    [GeneralData]          VARCHAR (8000) NOT NULL,
    [OperationDescription] VARCHAR (8000) NOT NULL,
    [HandlingPrecautions]  VARCHAR (8000) NOT NULL,
    [Cleaning]             VARCHAR (8000) NOT NULL,
    [IdEquipmentReception] INT            NULL,
    CONSTRAINT [PK_TénicaEquipmentSheet] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TechnicalEquipmentSheet_EquipmentRegistration] FOREIGN KEY ([IdEquipmentReception]) REFERENCES [Maintenance].[EquipmentRegistration] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) de la recepción del equipo médico, referencia a EquipmentRegistration para vincular la ficha técnica con el registro de ingreso del equipo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TechnicalEquipmentSheet', @level2type = N'COLUMN', @level2name = N'IdEquipmentReception';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la recepcion del equipo detalle', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TechnicalEquipmentSheet', @level2type = N'COLUMN', @level2name = N'IdEquipmentReception';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TechnicalEquipmentSheet', @level2type = N'COLUMN', @level2name = N'IdEquipmentReception';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimientos y protocolos de limpieza, desinfección y mantenimiento de la higiene del equipo médico, incluyendo frecuencia y productos recomendados', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TechnicalEquipmentSheet', @level2type = N'COLUMN', @level2name = N'Cleaning';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Limpieza del equipo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TechnicalEquipmentSheet', @level2type = N'COLUMN', @level2name = N'Cleaning';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TechnicalEquipmentSheet', @level2type = N'COLUMN', @level2name = N'Cleaning';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Precauciones, advertencias y medidas de seguridad para el manejo correcto del equipo, protección del personal y prevención de daños', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TechnicalEquipmentSheet', @level2type = N'COLUMN', @level2name = N'HandlingPrecautions';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Preucuaciones de manejo del equipo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TechnicalEquipmentSheet', @level2type = N'COLUMN', @level2name = N'HandlingPrecautions';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TechnicalEquipmentSheet', @level2type = N'COLUMN', @level2name = N'HandlingPrecautions';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción detallada del funcionamiento, operación normal, secuencia de uso y características técnicas de desempeño del equipo médico', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TechnicalEquipmentSheet', @level2type = N'COLUMN', @level2name = N'OperationDescription';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion de Funcionamiento', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TechnicalEquipmentSheet', @level2type = N'COLUMN', @level2name = N'OperationDescription';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TechnicalEquipmentSheet', @level2type = N'COLUMN', @level2name = N'OperationDescription';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Información general del equipo: marca, modelo, número de serie, especificaciones técnicas, año de fabricación y datos de identificación', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TechnicalEquipmentSheet', @level2type = N'COLUMN', @level2name = N'GeneralData';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Datos generales del equipo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TechnicalEquipmentSheet', @level2type = N'COLUMN', @level2name = N'GeneralData';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TechnicalEquipmentSheet', @level2type = N'COLUMN', @level2name = N'GeneralData';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Condiciones ambientales de almacenamiento, temperatura, humedad, presión atmosférica y requerimientos del entorno para preservar la integridad del equipo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TechnicalEquipmentSheet', @level2type = N'COLUMN', @level2name = N'Terms';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Condiciones Del entorno del almacenamiento', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TechnicalEquipmentSheet', @level2type = N'COLUMN', @level2name = N'Terms';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TechnicalEquipmentSheet', @level2type = N'COLUMN', @level2name = N'Terms';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) de la ficha técnica del equipo, generado automáticamente', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TechnicalEquipmentSheet', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TechnicalEquipmentSheet', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TechnicalEquipmentSheet', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ficha técnica de equipos del módulo de mantenimiento. Registra las especificaciones, condiciones de uso, precauciones de manejo y procedimientos de limpieza de cada equipo, vinculada a su recepción en el inventario.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TechnicalEquipmentSheet';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TechnicalEquipmentSheet';
