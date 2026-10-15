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
ALTER TABLE [Billing].[RCM_WorkflowHistory] NOCHECK CONSTRAINT [FK__RCM_Workf__Track__634D59AC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena el estado nuevo de la historia de flujo de trabajo.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_WorkflowHistory', @level2type = N'COLUMN', @level2name = N'NewStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena el estado anterior de la historia de flujo de trabajo.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_WorkflowHistory', @level2type = N'COLUMN', @level2name = N'OldStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Establece la fecha de cambio del flujo de trabajo.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_WorkflowHistory', @level2type = N'COLUMN', @level2name = N'ChangeDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo que guarda el dato por el que se cambia la historia.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_WorkflowHistory', @level2type = N'COLUMN', @level2name = N'ChangedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del seguimiento.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_WorkflowHistory', @level2type = N'COLUMN', @level2name = N'TrackingID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la historia.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_WorkflowHistory', @level2type = N'COLUMN', @level2name = N'HistoryID';

