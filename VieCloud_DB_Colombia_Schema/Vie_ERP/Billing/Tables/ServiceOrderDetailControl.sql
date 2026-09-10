CREATE TABLE [Billing].[ServiceOrderDetailControl] (
    [Id]                   INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ServiceOrderDetailId] INT           NOT NULL,
    [Justification]        VARCHAR (500) NULL,
    [CreationUser]         VARCHAR (20)  NOT NULL,
    [CreationDate]         DATETIME      NOT NULL,
    CONSTRAINT [PK_ServiceOrderDetailControl__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ServiceOrderDetailControl_ServiceOrderDetail] FOREIGN KEY ([ServiceOrderDetailId]) REFERENCES [Billing].[ServiceOrderDetail] ([Id])
);


GO
ALTER TABLE [Billing].[ServiceOrderDetailControl] NOCHECK CONSTRAINT [FK_ServiceOrderDetailControl_ServiceOrderDetail];


GO
CREATE NONCLUSTERED INDEX [ServiceOrderDetailControl_ServiceOrderDetailId]
    ON [Billing].[ServiceOrderDetailControl]([ServiceOrderDetailId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) en que se creó o registró el control, reaprobación o auditoría del detalle de la orden', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailControl', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailControl', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailControl', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario o identificación (VARCHAR 20) del operario, auditor o profesional que registró el control o reautorización', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailControl', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailControl', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailControl', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Motivo o descripción textual (VARCHAR 500) por el cual se repite, reaprueba o reautoriza la orden de servicio; explica cambios, glosas o revalidaciones', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailControl', @level2type = N'COLUMN', @level2name = N'Justification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Justificacion por la cual se repite la autorizacion', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailControl', @level2type = N'COLUMN', @level2name = N'Justification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailControl', @level2type = N'COLUMN', @level2name = N'Justification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) del detalle de la orden de servicio, línea de facturación o procedimiento controlado; vinculado a Billing.ServiceOrderDetail', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailControl', @level2type = N'COLUMN', @level2name = N'ServiceOrderDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle de la orden de servicio', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailControl', @level2type = N'COLUMN', @level2name = N'ServiceOrderDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailControl', @level2type = N'COLUMN', @level2name = N'ServiceOrderDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT) del registro de control en la tabla de auditoría de órdenes de servicio', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailControl', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailControl', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailControl', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de control y auditoría de los detalles de órdenes de servicio; guarda la justificación y trazabilidad de cambios o validaciones realizadas sobre cada ítem de una orden de servicio, incluyendo el usuario y la fecha en que se efectuó la acción.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailControl';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailControl';
