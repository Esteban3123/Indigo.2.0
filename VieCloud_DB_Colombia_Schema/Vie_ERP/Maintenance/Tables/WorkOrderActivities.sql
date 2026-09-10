CREATE TABLE [Maintenance].[WorkOrderActivities] (
    [Id]                 INT     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [WorkOrderId]        INT     NOT NULL,
    [ProtocolActivityId] INT     NOT NULL,
    [Time]               INT     NOT NULL,
    [Unit]               TINYINT NOT NULL,
    CONSTRAINT [PK_WorkOrderActivities__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_WorkOrderActivities_ProtocolActivities] FOREIGN KEY ([ProtocolActivityId]) REFERENCES [Maintenance].[ProtocolActivities] ([Id]),
    CONSTRAINT [FK_WorkOrderActivities_WorkOrder] FOREIGN KEY ([WorkOrderId]) REFERENCES [Maintenance].[WorkOrder] ([Id])
);




GO



GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_WorkOrderActivities]
    ON [Maintenance].[WorkOrderActivities]([WorkOrderId] ASC, [ProtocolActivityId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de tiempo para la actividad: 1=Minutos, 2=Horas, 3=Días. TINYINT. Define la escala temporal del campo Time.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrderActivities', @level2type = N'COLUMN', @level2name = N'Unit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad:  1 - Minutos  2 - Horas  3 - Dias', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrderActivities', @level2type = N'COLUMN', @level2name = N'Unit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrderActivities', @level2type = N'COLUMN', @level2name = N'Unit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Duración estimada o real en desarrollar/ejecutar la actividad de mantenimiento. INT. Valor numérico que se interpreta según Unit (minutos, horas o días).', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrderActivities', @level2type = N'COLUMN', @level2name = N'Time';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tiempo en desarrollar la actividad', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrderActivities', @level2type = N'COLUMN', @level2name = N'Time';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrderActivities', @level2type = N'COLUMN', @level2name = N'Time';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la actividad del protocolo de mantenimiento seleccionado. INT. FK a Maintenance.ProtocolActivities. Vincula la orden de trabajo con protocolos predefinidos.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrderActivities', @level2type = N'COLUMN', @level2name = N'ProtocolActivityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la actividad de acuerdo al protocolo seleccionado', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrderActivities', @level2type = N'COLUMN', @level2name = N'ProtocolActivityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrderActivities', @level2type = N'COLUMN', @level2name = N'ProtocolActivityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la orden de trabajo (OT) o solicitud de mantenimiento asociada. INT. FK a Maintenance.WorkOrder. Agrupa actividades bajo una misma OT.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrderActivities', @level2type = N'COLUMN', @level2name = N'WorkOrderId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la orden de trabajo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrderActivities', @level2type = N'COLUMN', @level2name = N'WorkOrderId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrderActivities', @level2type = N'COLUMN', @level2name = N'WorkOrderId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico (Identity) de cada registro de actividad en la orden de trabajo. INT Primary Key. Secuencia automática e incremental.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrderActivities', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrderActivities', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrderActivities', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Actividades registradas dentro de una orden de trabajo de mantenimiento, indicando qué tarea del protocolo se ejecutó y el tiempo empleado en realizarla.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrderActivities';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrderActivities';
