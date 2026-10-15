CREATE TABLE [Billing].[RCM_Responsibles] (
    [ResponsibleID] INT            IDENTITY (1, 1) NOT NULL,
    [USerID]        INT            NOT NULL,
    [Status]        NVARCHAR (255) NOT NULL,
    CONSTRAINT [PK__RCM_Resp__C95AB0550A3B4C82] PRIMARY KEY CLUSTERED ([ResponsibleID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena el estado del responsable.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_Responsibles', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del usuario del responsable RCM.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_Responsibles', @level2type = N'COLUMN', @level2name = N'USerID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del responsable RCM.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_Responsibles', @level2type = N'COLUMN', @level2name = N'ResponsibleID';

