CREATE TABLE [Billing].[RCM_ActivityType] (
    [ActivityTypeID] INT            IDENTITY (1, 1) NOT NULL,
    [Class]          INT            NULL,
    [WorkflowTypeID] INT            NULL,
    [Name]           NVARCHAR (255) NULL,
    [Description]    NVARCHAR (500) NULL,
    [CreationDate]   DATETIME       NULL,
    [CreationUserID] INT            NULL,
    [Status]         NVARCHAR (50)  NULL,
    CONSTRAINT [PK__RCM_Acti__95CEDE6EE65130A6] PRIMARY KEY CLUSTERED ([ActivityTypeID] ASC),
    CONSTRAINT [FK__RCM_Activ__Workf__5BAC37E4] FOREIGN KEY ([WorkflowTypeID]) REFERENCES [Billing].[RCM_WorkflowType] ([WorkflowTypeID])
);


GO
ALTER TABLE [Billing].[RCM_ActivityType] NOCHECK CONSTRAINT [FK__RCM_Activ__Workf__5BAC37E4];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica el estado del registro de tipo de actividad RCM.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_ActivityType', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del usuario de creación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_ActivityType', @level2type = N'COLUMN', @level2name = N'CreationUserID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica la fecha de creación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_ActivityType', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica la descripción del tipo de actividad de RCM.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_ActivityType', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena el nombre del tipo de actividad.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_ActivityType', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del tipo de flujo de trabajo.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_ActivityType', @level2type = N'COLUMN', @level2name = N'WorkflowTypeID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica la clase del tipo de actividad de RCM.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_ActivityType', @level2type = N'COLUMN', @level2name = N'Class';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del tipo de actividad.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_ActivityType', @level2type = N'COLUMN', @level2name = N'ActivityTypeID';

