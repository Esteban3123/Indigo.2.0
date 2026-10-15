CREATE TABLE [Billing].[RCM_WorkflowType] (
    [WorkflowTypeID] INT            IDENTITY (1, 1) NOT NULL,
    [Name]           NVARCHAR (255) NULL,
    [Description]    NVARCHAR (500) NULL,
    [CreationDate]   DATETIME       NULL,
    [CreationUserID] INT            NULL,
    [Status]         NVARCHAR (50)  NULL,
    CONSTRAINT [PK__RCM_Work__077F14C9F571A30E] PRIMARY KEY CLUSTERED ([WorkflowTypeID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Establece el estado del registro', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_WorkflowType', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del usuario de creación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_WorkflowType', @level2type = N'COLUMN', @level2name = N'CreationUserID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica la fecha de la creación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_WorkflowType', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica la descripción del flujo de trabajo de RCM', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_WorkflowType', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Establece el nombre', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_WorkflowType', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del tipo del flujo de trabajo', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_WorkflowType', @level2type = N'COLUMN', @level2name = N'WorkflowTypeID';

