CREATE TABLE [Glosas].[AccountSettingsNET_PublicMethod] (
    [Id]                    INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ParametersInterfaceId] INT           NULL,
    [PlanCode]              VARCHAR (20)  NOT NULL,
    [Denomination]          VARCHAR (500) NOT NULL,
    [InvoiceRadicate]       VARCHAR (20)  NULL,
    CONSTRAINT [PK_AccountSettingsNET_PublicMethod] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AccountSettingsNET_PublicMethod_GlosasParametersInterface] FOREIGN KEY ([ParametersInterfaceId]) REFERENCES [Glosas].[GlosasParametersInterface] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de radicación de factura; cuenta de orden de facturas radicadas en glosas; identificador único del trámite de facturación ante entidad pagadora.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsNET_PublicMethod', @level2type = N'COLUMN', @level2name = N'InvoiceRadicate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuenta de orden facturas radicadas', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsNET_PublicMethod', @level2type = N'COLUMN', @level2name = N'InvoiceRadicate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsNET_PublicMethod', @level2type = N'COLUMN', @level2name = N'InvoiceRadicate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o denominación del plan de beneficios; descripción comercial del plan de cobertura de salud.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsNET_PublicMethod', @level2type = N'COLUMN', @level2name = N'Denomination';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de plan', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsNET_PublicMethod', @level2type = N'COLUMN', @level2name = N'Denomination';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsNET_PublicMethod', @level2type = N'COLUMN', @level2name = N'Denomination';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del plan de beneficios; identificador alfanumérico del plan de cobertura asignado por la entidad aseguradora o administradora de riesgos.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsNET_PublicMethod', @level2type = N'COLUMN', @level2name = N'PlanCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Plan de beneficios', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsNET_PublicMethod', @level2type = N'COLUMN', @level2name = N'PlanCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsNET_PublicMethod', @level2type = N'COLUMN', @level2name = N'PlanCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de configuración de interfaces; clave foránea a tabla GlosasParametersInterface que define parámetros de integración con sistemas externos.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsNET_PublicMethod', @level2type = N'COLUMN', @level2name = N'ParametersInterfaceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo configuracion de interfaces', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsNET_PublicMethod', @level2type = N'COLUMN', @level2name = N'ParametersInterfaceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsNET_PublicMethod', @level2type = N'COLUMN', @level2name = N'ParametersInterfaceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico de configuración de método público NET para glosas; clave primaria de la tabla AccountSettingsNET_PublicMethod.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsNET_PublicMethod', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la configuracion de interfaces version NET - Metodo Publico', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsNET_PublicMethod', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsNET_PublicMethod', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Configuración de métodos públicos para la liquidación de cuentas de glosas por plan. Registra los parámetros de interfaz, el plan de salud o contrato asociado, la denominación del método y el radicado de factura vinculado.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsNET_PublicMethod';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsNET_PublicMethod';
