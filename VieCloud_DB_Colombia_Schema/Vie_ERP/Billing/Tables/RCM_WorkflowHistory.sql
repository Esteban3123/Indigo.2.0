CREATE TABLE [Billing].[RCM_WorkflowHistory] (
    [HistoryID]  INT            IDENTITY (1, 1) NOT NULL,
    [TrackingID] INT            NOT NULL,
    [ChangedBy]  INT            NOT NULL,
    [ChangeDate] DATETIME2 (7)  NOT NULL,
    [OldStatus]  NVARCHAR (255) NOT NULL,
    [NewStatus]  NVARCHAR (255) NOT NULL,
    CONSTRAINT [PK__RCM_Work__4D7B4ADD33812315] PRIMARY KEY CLUSTERED ([HistoryID] ASC),
    CONSTRAINT [FK__RCM_Workf__Track__634D59AC] FOREIGN KEY ([TrackingID]) REFERENCES [Billing].[RCM_StateTracking] ([TrackingID])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado nuevo (NVARCHAR 255) del proceso RCM después del cambio; registra el estado actual de factura, glosa, reclamación o cobranza en el flujo de gestión.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_WorkflowHistory', @level2type = N'COLUMN', @level2name = N'NewStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el estado nuevo de la historia de flujo de trabajo.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_WorkflowHistory', @level2type = N'COLUMN', @level2name = N'NewStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_WorkflowHistory', @level2type = N'COLUMN', @level2name = N'NewStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado anterior (NVARCHAR 255) del proceso RCM antes de la transición; almacena el estado previo de factura, glosa, reclamación o cobranza para trazabilidad.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_WorkflowHistory', @level2type = N'COLUMN', @level2name = N'OldStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el estado anterior de la historia de flujo de trabajo.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_WorkflowHistory', @level2type = N'COLUMN', @level2name = N'OldStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_WorkflowHistory', @level2type = N'COLUMN', @level2name = N'OldStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca de tiempo (DATETIME2) que registra cuándo ocurrió el cambio de estado en el flujo RCM; esencial para auditoría temporal y seguimiento de transiciones de facturación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_WorkflowHistory', @level2type = N'COLUMN', @level2name = N'ChangeDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece la fecha de cambio del flujo de trabajo.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_WorkflowHistory', @level2type = N'COLUMN', @level2name = N'ChangeDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_WorkflowHistory', @level2type = N'COLUMN', @level2name = N'ChangeDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID del usuario/profesional (INT) que realizó el cambio de estado en el flujo de trabajo; permite auditoría de quién modificó el estado de la facturación, cobranza o reclamación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_WorkflowHistory', @level2type = N'COLUMN', @level2name = N'ChangedBy';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo que guarda el dato por el que se cambia la historia.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_WorkflowHistory', @level2type = N'COLUMN', @level2name = N'ChangedBy';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_WorkflowHistory', @level2type = N'COLUMN', @level2name = N'ChangedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del seguimiento (FK a RCM_StateTracking); vincula este cambio de estado al proceso de rastreo de la reclamación, factura o glosa en el flujo RCM.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_WorkflowHistory', @level2type = N'COLUMN', @level2name = N'TrackingID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del seguimiento.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_WorkflowHistory', @level2type = N'COLUMN', @level2name = N'TrackingID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_WorkflowHistory', @level2type = N'COLUMN', @level2name = N'TrackingID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT) del registro de historia de cambios en el flujo de trabajo RCM; clave primaria que rastrea cada transición de estado en la gestión de facturación y cobranza.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_WorkflowHistory', @level2type = N'COLUMN', @level2name = N'HistoryID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la historia.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_WorkflowHistory', @level2type = N'COLUMN', @level2name = N'HistoryID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_WorkflowHistory', @level2type = N'COLUMN', @level2name = N'HistoryID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro histórico de cambios de estado en el flujo de trabajo de facturación y gestión del ciclo de ingresos (RCM). Permite rastrear quién cambió el estado de un ítem de facturación, cuándo ocurrió y cuál fue el estado anterior y el nuevo.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_WorkflowHistory';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_WorkflowHistory';
