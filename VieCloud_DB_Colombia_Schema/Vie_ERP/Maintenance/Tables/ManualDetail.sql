CREATE TABLE [Maintenance].[ManualDetail] (
    [Id]                   INT      IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdEquipmentReception] INT      NOT NULL,
    [Manual]               CHAR (1) NOT NULL,
    CONSTRAINT [PK_ManualDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ManualDetail_EquipmentRegistration] FOREIGN KEY ([IdEquipmentReception]) REFERENCES [Maintenance].[EquipmentRegistration] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de manual seleccionado (CHAR(1)): 1=Operación, 2=Mantenimiento, 3=Partes, 4=Despieces. Clasificación del documento de referencia técnica para el equipo biomédico.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ManualDetail', @level2type = N'COLUMN', @level2name = N'Manual';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Manual Seleccionado 1-Operacion 2-Mantenimiento 3-Partes 4-Despieces', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ManualDetail', @level2type = N'COLUMN', @level2name = N'Manual';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ManualDetail', @level2type = N'COLUMN', @level2name = N'Manual';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK INT) de la recepción del equipo en Maintenance.EquipmentRegistration. Referencia a equipo biomédico ingresado al centro de atención para mantenimiento o reparación.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ManualDetail', @level2type = N'COLUMN', @level2name = N'IdEquipmentReception';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Recepción del equipo ', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ManualDetail', @level2type = N'COLUMN', @level2name = N'IdEquipmentReception';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ManualDetail', @level2type = N'COLUMN', @level2name = N'IdEquipmentReception';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autonumérico (INT IDENTITY) clave primaria de la tabla ManualDetail. Índice único de cada registro de manual asociado a equipo.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ManualDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ManualDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ManualDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de detalle de mantenimiento manual asociado a la recepción de equipos. Indica si cada equipo recibido requiere o tiene asociado un proceso de mantenimiento manual.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ManualDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ManualDetail';
