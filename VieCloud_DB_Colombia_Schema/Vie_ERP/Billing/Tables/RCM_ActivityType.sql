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



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'NVARCHAR(50); estado del registro del tipo de actividad RCM (Activo/Inactivo/Obsoleto); controla disponibilidad en workflows de facturación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_ActivityType', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el estado del registro de tipo de actividad RCM.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_ActivityType', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_ActivityType', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT; identificador del usuario que creó el registro del tipo de actividad; trazabilidad de auditoría en el sistema.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_ActivityType', @level2type = N'COLUMN', @level2name = N'CreationUserID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del usuario de creación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_ActivityType', @level2type = N'COLUMN', @level2name = N'CreationUserID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_ActivityType', @level2type = N'COLUMN', @level2name = N'CreationUserID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATETIME; fecha y hora de creación del registro del tipo de actividad RCM; marcador temporal de auditoría.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_ActivityType', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la fecha de creación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_ActivityType', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_ActivityType', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'NVARCHAR(500); descripción detallada del tipo de actividad, propósito y alcance en el ciclo de facturación y recaudo (RCM - Revenue Cycle Management).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_ActivityType', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la descripción del tipo de actividad de RCM.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_ActivityType', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_ActivityType', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'NVARCHAR(255); nombre descriptivo del tipo de actividad RCM (ej: Validación Factura, Glosa Facturación, Generación RIPS, Seguimiento Cobranza).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_ActivityType', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el nombre del tipo de actividad.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_ActivityType', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_ActivityType', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT; FK a [Billing].[RCM_WorkflowType]; identifica el flujo de trabajo (proceso de facturación/recaudo) al cual pertenece este tipo de actividad.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_ActivityType', @level2type = N'COLUMN', @level2name = N'WorkflowTypeID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tipo de flujo de trabajo.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_ActivityType', @level2type = N'COLUMN', @level2name = N'WorkflowTypeID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_ActivityType', @level2type = N'COLUMN', @level2name = N'WorkflowTypeID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT; clasificación numérica del tipo de actividad RCM (ej: gestión de glosas, validación de RIPS, cobranza, ajustes contables).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_ActivityType', @level2type = N'COLUMN', @level2name = N'Class';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la clase del tipo de actividad de RCM.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_ActivityType', @level2type = N'COLUMN', @level2name = N'Class';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_ActivityType', @level2type = N'COLUMN', @level2name = N'Class';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT, PK) del tipo de actividad RCM; clave primaria que referencia las actividades en el ciclo de facturación y recaudo.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_ActivityType', @level2type = N'COLUMN', @level2name = N'ActivityTypeID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tipo de actividad.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_ActivityType', @level2type = N'COLUMN', @level2name = N'ActivityTypeID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_ActivityType', @level2type = N'COLUMN', @level2name = N'ActivityTypeID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de tipos de actividad del ciclo de ingresos (RCM). Registra las categorías de actividades de facturación, cobranza y gestión financiera que pueden asignarse a los flujos de trabajo del revenue cycle management.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_ActivityType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RCM_ActivityType';
