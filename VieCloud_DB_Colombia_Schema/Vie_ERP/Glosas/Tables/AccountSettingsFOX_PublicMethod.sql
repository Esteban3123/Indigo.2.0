CREATE TABLE [Glosas].[AccountSettingsFOX_PublicMethod] (
    [Id]                    INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ParametersInterfaceId] INT           NULL,
    [PlanCode]              VARCHAR (20)  NOT NULL,
    [Denomination]          VARCHAR (100) NULL,
    [InvoiceRadicate]       VARCHAR (20)  NULL,
    CONSTRAINT [PK_AccountSettingsFOX_PublicMethod] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AccountSettingsFOX_PublicMethod_GlosasParametersInterface] FOREIGN KEY ([ParametersInterfaceId]) REFERENCES [Glosas].[GlosasParametersInterface] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de radicación o cuenta de orden de facturas radicadas ante autoridades sanitarias; referencia de facturación en glosas y RIPS', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsFOX_PublicMethod', @level2type = N'COLUMN', @level2name = N'InvoiceRadicate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuenta de orden facturas radicadas', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsFOX_PublicMethod', @level2type = N'COLUMN', @level2name = N'InvoiceRadicate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsFOX_PublicMethod', @level2type = N'COLUMN', @level2name = N'InvoiceRadicate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción o nombre del plan de beneficio; denominación comercial o contractual del plan de salud', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsFOX_PublicMethod', @level2type = N'COLUMN', @level2name = N'Denomination';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion o Nombre del plan', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsFOX_PublicMethod', @level2type = N'COLUMN', @level2name = N'Denomination';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsFOX_PublicMethod', @level2type = N'COLUMN', @level2name = N'Denomination';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del plan de beneficio; identificador del plan contractual, afiliación o cobertura sanitaria', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsFOX_PublicMethod', @level2type = N'COLUMN', @level2name = N'PlanCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo plan de beneficio', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsFOX_PublicMethod', @level2type = N'COLUMN', @level2name = N'PlanCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsFOX_PublicMethod', @level2type = N'COLUMN', @level2name = N'PlanCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de configuración de interfaz; clave foránea a parámetros de conexión FOX método público (GlosasParametersInterface)', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsFOX_PublicMethod', @level2type = N'COLUMN', @level2name = N'ParametersInterfaceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo configuracion de interface', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsFOX_PublicMethod', @level2type = N'COLUMN', @level2name = N'ParametersInterfaceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsFOX_PublicMethod', @level2type = N'COLUMN', @level2name = N'ParametersInterfaceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Autonumérico de configuración de interfaces FOX método público; identificador único de registro de settings para integración', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsFOX_PublicMethod', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la configuracion de interfaces version FOX - Metodo Publico', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsFOX_PublicMethod', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsFOX_PublicMethod', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Configuración de cuentas FOX para el módulo de glosas: relaciona planes de pago o contratos con sus denominaciones y datos de radicación de facturas, permitiendo la integración con el sistema externo FOX.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsFOX_PublicMethod';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsFOX_PublicMethod';
