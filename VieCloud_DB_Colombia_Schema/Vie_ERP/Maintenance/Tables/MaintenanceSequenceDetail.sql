CREATE TABLE [Maintenance].[MaintenanceSequenceDetail] (
    [Id]                     INT    IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdSequenseMaintenanceC] INT    NOT NULL,
    [IdSequense]             INT    NOT NULL,
    [IdOperatingUnit]        INT    NULL,
    [Next]                   BIGINT CONSTRAINT [DF_SequenseMaintenanceD_Next] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_SequenseMaintenanceD] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SequenseMaintenanceD_OperatingUnit] FOREIGN KEY ([IdOperatingUnit]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_SequenseMaintenanceD_Sequense] FOREIGN KEY ([IdSequense]) REFERENCES [Common].[Sequense] ([Id]),
    CONSTRAINT [FK_SequenseMaintenanceD_SequenseMaintenanceC] FOREIGN KEY ([IdSequenseMaintenanceC]) REFERENCES [Maintenance].[MaintenanceSequence] ([Id])
);




GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Siguiente número a generar en la secuencia de mantenimiento. Contador (BIGINT) que incrementa para garantizar unicidad en la numeración de documentos, registros o transacciones asociadas.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceSequenceDetail', @level2type = N'COLUMN', @level2name = N'Next';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Siguiente numero a generar con la secuenacia', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceSequenceDetail', @level2type = N'COLUMN', @level2name = N'Next';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceSequenceDetail', @level2type = N'COLUMN', @level2name = N'Next';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la unidad operativa (UO, centro de atención o unidad funcional) asignada a la secuencia. Solo se completa si el alcance es por unidad operativa; nulo si el alcance es global o institucional. FK a Common.OperatingUnit.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceSequenceDetail', @level2type = N'COLUMN', @level2name = N'IdOperatingUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad operativa asignada a la secuencia. Solo cuando el ambito es UO-Unidad Operativa, de lo contrario el campo es nulo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceSequenceDetail', @level2type = N'COLUMN', @level2name = N'IdOperatingUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceSequenceDetail', @level2type = N'COLUMN', @level2name = N'IdOperatingUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la secuencia base común (FK a Common.Sequense). Referencia a la configuración de secuencia compartida.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceSequenceDetail', @level2type = N'COLUMN', @level2name = N'IdSequense';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la secuencia base', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceSequenceDetail', @level2type = N'COLUMN', @level2name = N'IdSequense';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceSequenceDetail', @level2type = N'COLUMN', @level2name = N'IdSequense';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cabecera de secuencia de mantenimiento (FK a MaintenanceSequence). Agrupa detalles bajo una secuencia padre.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceSequenceDetail', @level2type = N'COLUMN', @level2name = N'IdSequenseMaintenanceC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceSequenceDetail', @level2type = N'COLUMN', @level2name = N'IdSequenseMaintenanceC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceSequenceDetail', @level2type = N'COLUMN', @level2name = N'IdSequenseMaintenanceC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) del detalle de secuencia de mantenimiento. Clave primaria del registro.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceSequenceDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceSequenceDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceSequenceDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de las secuencias de mantenimiento configuradas en el sistema. Registra cada paso o elemento de una secuencia de mantenimiento, asociado a una unidad operativa, con el valor del siguiente número correlativo a utilizar.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceSequenceDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceSequenceDetail';
