CREATE TABLE [MedicalFees].[HealthProfessionalContract] (
    [Id]                     INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [HealthProfessionalCode] CHAR (20) NOT NULL,
    [MedicalFeesContractId]  INT       NOT NULL,
    [LiquidateDefault]       BIT       NOT NULL,
    CONSTRAINT [PK_HealthProfessionalContract] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_HealthProfessionalContract_MedicalFeesContract] FOREIGN KEY ([MedicalFeesContractId]) REFERENCES [MedicalFees].[MedicalFeesContract] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera booleana que indica si este profesional se liquida automáticamente por defecto bajo los términos del contrato de honorarios. Tipo: BIT (1=sí, 0=no).', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'HealthProfessionalContract', @level2type = N'COLUMN', @level2name = N'LiquidateDefault';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica que el contrato se liquida por defecto', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'HealthProfessionalContract', @level2type = N'COLUMN', @level2name = N'LiquidateDefault';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'HealthProfessionalContract', @level2type = N'COLUMN', @level2name = N'LiquidateDefault';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del contrato de honorarios médicos (FK). Vincula a la tabla MedicalFeesContract para determinar tarifas, acuerdos y condiciones de liquidación. Tipo: INT.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'HealthProfessionalContract', @level2type = N'COLUMN', @level2name = N'MedicalFeesContractId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del contrato de honorarios medicos', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'HealthProfessionalContract', @level2type = N'COLUMN', @level2name = N'MedicalFeesContractId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'HealthProfessionalContract', @level2type = N'COLUMN', @level2name = N'MedicalFeesContractId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud (médico, enfermero, especialista, etc.) que realiza el servicio y/o administra el producto. Referencia a tabla Crystal INPROFSAL. Tipo: CHAR(20).', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'HealthProfessionalContract', @level2type = N'COLUMN', @level2name = N'HealthProfessionalCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Codigo del Profesional de la Salud que Realiza el Servicio y/o Administra el Producto.   Estos datos se sacan de la tabla de Crystal INPROFSAL', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'HealthProfessionalContract', @level2type = N'COLUMN', @level2name = N'HealthProfessionalCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'HealthProfessionalContract', @level2type = N'COLUMN', @level2name = N'HealthProfessionalCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (PK) de la asociación entre profesional de salud y contrato de honorarios médicos. Tipo: INT IDENTITY.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'HealthProfessionalContract', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'HealthProfessionalContract', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'HealthProfessionalContract', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación entre profesionales de la salud y contratos de honorarios médicos, indicando qué contrato tarifario aplica a cada profesional y si se liquida por defecto con ese contrato.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'HealthProfessionalContract';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'HealthProfessionalContract';
