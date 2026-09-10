CREATE TABLE [Maintenance].[WorkOrderTools] (
    [Id]             INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [WorkOrderId]    INT NOT NULL,
    [ProtocolToolId] INT NOT NULL,
    CONSTRAINT [PK_WorkOrderTools__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_WorkOrderTools_ProtocolTools] FOREIGN KEY ([ProtocolToolId]) REFERENCES [Maintenance].[ProtocolTools] ([Id]),
    CONSTRAINT [FK_WorkOrderTools_WorkOrder] FOREIGN KEY ([WorkOrderId]) REFERENCES [Maintenance].[WorkOrder] ([Id])
);




GO



GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_WorkOrderTools]
    ON [Maintenance].[WorkOrderTools]([WorkOrderId] ASC, [ProtocolToolId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la herramienta de mantenimiento según protocolo seleccionado. Referencia a Maintenance.ProtocolTools. Define qué herramienta/equipo se utiliza en la orden de trabajo conforme al protocolo de mantenimiento establecido.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrderTools', @level2type = N'COLUMN', @level2name = N'ProtocolToolId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la herramienta de acuerdo al protocolo seleccionado', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrderTools', @level2type = N'COLUMN', @level2name = N'ProtocolToolId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrderTools', @level2type = N'COLUMN', @level2name = N'ProtocolToolId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la orden de trabajo de mantenimiento. Referencia a Maintenance.WorkOrder. Vincula la herramienta a la orden de trabajo específica que requiere su uso.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrderTools', @level2type = N'COLUMN', @level2name = N'WorkOrderId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la orden de trabajo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrderTools', @level2type = N'COLUMN', @level2name = N'WorkOrderId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrderTools', @level2type = N'COLUMN', @level2name = N'WorkOrderId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico (IDENTITY) de la asociación herramienta-orden. Clave primaria de la tabla WorkOrderTools.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrderTools', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrderTools', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrderTools', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de herramientas o insumos asignados a una orden de trabajo de mantenimiento. Vincula cada orden de trabajo con las herramientas definidas en el protocolo correspondiente.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrderTools';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'WorkOrderTools';
