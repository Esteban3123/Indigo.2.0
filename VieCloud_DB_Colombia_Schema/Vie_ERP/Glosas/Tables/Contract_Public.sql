CREATE TABLE [Glosas].[Contract_Public] (
    [ContractCode]       VARCHAR (10) NOT NULL,
    [PlanCode]           VARCHAR (10) NOT NULL,
    [AccountNotRadicate] VARCHAR (20) NULL,
    [AccountRadicate]    VARCHAR (20) NULL,
    CONSTRAINT [PK_Contract_Public] PRIMARY KEY CLUSTERED ([ContractCode] ASC, [PlanCode] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de cuenta o factura radicada ante la aseguradora; identificador del comprobante de pago registrado en RIPS o sistema de facturación (VARCHAR 20, clave para glosas y auditoría).', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'Contract_Public', @level2type = N'COLUMN', @level2name = N'AccountRadicate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'cuenta factura radicadas', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'Contract_Public', @level2type = N'COLUMN', @level2name = N'AccountRadicate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'Contract_Public', @level2type = N'COLUMN', @level2name = N'AccountRadicate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de cuenta o factura pendiente de radicación; comprobante de pago no registrado aún ante la aseguradora o entidad responsable (VARCHAR 20, requisito para resolver glosas).', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'Contract_Public', @level2type = N'COLUMN', @level2name = N'AccountNotRadicate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuenta de facturas sin radicar', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'Contract_Public', @level2type = N'COLUMN', @level2name = N'AccountNotRadicate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'Contract_Public', @level2type = N'COLUMN', @level2name = N'AccountNotRadicate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del plan de beneficios o cobertura contratado; identificador del producto sanitario asociado al contrato (VARCHAR 10, clave foránea a tablas de planes).', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'Contract_Public', @level2type = N'COLUMN', @level2name = N'PlanCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo plan de beneficios', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'Contract_Public', @level2type = N'COLUMN', @level2name = N'PlanCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'Contract_Public', @level2type = N'COLUMN', @level2name = N'PlanCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del contrato o acuerdo entre la institución de salud y la aseguradora; identificador único del convenio para facturación y glosas (VARCHAR 10, parte de clave primaria).', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'Contract_Public', @level2type = N'COLUMN', @level2name = N'ContractCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo contrato', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'Contract_Public', @level2type = N'COLUMN', @level2name = N'ContractCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'Contract_Public', @level2type = N'COLUMN', @level2name = N'ContractCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contratos públicos de glosas: relaciona cada contrato con su plan de beneficios y los números de cuenta asociados, tanto las cuentas no radicadas (pendientes de presentación) como las radicadas (presentadas al pagador). Se usa en el proceso de auditoría y gestión de glosas con entidades públicas.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'Contract_Public';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'Contract_Public';
