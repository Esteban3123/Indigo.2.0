CREATE TABLE [Billing].[RCM_Responsibles] (
    [ResponsibleID] INT            IDENTITY (1, 1) NOT NULL,
    [USerID]        INT            NOT NULL,
    [Status]        NVARCHAR (255) NOT NULL,
    CONSTRAINT [PK__RCM_Resp__C95AB0550A3B4C82] PRIMARY KEY CLUSTERED ([ResponsibleID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado actual del responsable RCM (NVARCHAR 255). Indica si el gestor de cobranza está activo, inactivo, suspendido o en otra condición operativa para la gestión de facturas, glosas y recaudación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_Responsibles', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el estado del responsable.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_Responsibles', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_Responsibles', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del usuario responsable RCM (INT, FK a tabla de usuarios). Vincula cada gestor de recaudación/cobranza con su cuenta de usuario en el sistema para auditoría y asignación de tareas.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_Responsibles', @level2type = N'COLUMN', @level2name = N'USerID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del usuario del responsable RCM.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_Responsibles', @level2type = N'COLUMN', @level2name = N'USerID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_Responsibles', @level2type = N'COLUMN', @level2name = N'USerID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT, PK) del responsable RCM. Clave primaria que identifica unívocamente cada gestor de cobranza/recaudación en el módulo de Revenue Cycle Management.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_Responsibles', @level2type = N'COLUMN', @level2name = N'ResponsibleID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del responsable RCM.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_Responsibles', @level2type = N'COLUMN', @level2name = N'ResponsibleID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_Responsibles', @level2type = N'COLUMN', @level2name = N'ResponsibleID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de responsables del ciclo de ingresos (RCM): personas o usuarios asignados como responsables de gestión de cobros, facturación y seguimiento financiero de cuentas. Permite controlar quién tiene a cargo cada proceso de recaudo o cartera.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_Responsibles';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_Responsibles';
