CREATE TABLE [MedicalFees].[CausationRecognitionDetail] (
    [Id]                       INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CausationRecognitionId]   INT             NOT NULL,
    [MedicalFeesCausationId]   INT             NOT NULL,
    [HealthProfessionalCode]   CHAR (20)       NOT NULL,
    [ThirdPartyId]             INT             NOT NULL,
    [MedicalFeesContractId]    INT             NULL,
    [PerformsFunctionalUnitId] INT             NULL,
    [CostCenterId]             INT             NULL,
    [InvoiceQuantity]          INT             CONSTRAINT [DF_CausationRecognitionDetail_InvoiceQuantity] DEFAULT ((0)) NOT NULL,
    [AmountPayable]            NUMERIC (18, 2) CONSTRAINT [DF_CausationRecognitionDetail_AmountPayable] DEFAULT ((0)) NOT NULL,
    [TotalAmountPayable]       NUMERIC (18, 2) CONSTRAINT [DF_CausationRecognitionDetail_TotalAmountPayable] DEFAULT ((0)) NOT NULL,
    [AdmissionNumber]          VARCHAR (10)    NULL,
    CONSTRAINT [PK_CausationRecognitionDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CausationRecognitionDetail_CausationRecognition] FOREIGN KEY ([CausationRecognitionId]) REFERENCES [MedicalFees].[CausationRecognition] ([Id]),
    CONSTRAINT [FK_CausationRecognitionDetail_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_CausationRecognitionDetail_FunctionalUnit] FOREIGN KEY ([PerformsFunctionalUnitId]) REFERENCES [Payroll].[FunctionalUnit] ([Id]),
    CONSTRAINT [FK_CausationRecognitionDetail_MedicalFeesCausation] FOREIGN KEY ([MedicalFeesCausationId]) REFERENCES [MedicalFees].[MedicalFeesCausation] ([Id]),
    CONSTRAINT [FK_CausationRecognitionDetail_MedicalFeesContract] FOREIGN KEY ([MedicalFeesContractId]) REFERENCES [MedicalFees].[MedicalFeesContract] ([Id]),
    CONSTRAINT [FK_CausationRecognitionDetail_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);




GO



GO



GO



GO



GO



GO



GO
CREATE NONCLUSTERED INDEX [IX_CausationRecognitionDetail_MedicalFeesCausationId]
    ON [MedicalFees].[CausationRecognitionDetail]([MedicalFeesCausationId] ASC)
    INCLUDE([CausationRecognitionId], [TotalAmountPayable]);


GO
CREATE NONCLUSTERED INDEX [IX_CausationRecognitionDetail_CausationRecognitionId]
    ON [MedicalFees].[CausationRecognitionDetail]([CausationRecognitionId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico (IDENTITY) de cada línea de detalle en el reconocimiento de causación.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognitionDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Llave autonumérica', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognitionDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognitionDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (FK) hacia CausationRecognition. Identifica la cabecera/documento padre del reconocimiento de costos al cual pertenece esta línea.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognitionDetail', @level2type = N'COLUMN', @level2name = N'CausationRecognitionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera del reconocimiento', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognitionDetail', @level2type = N'COLUMN', @level2name = N'CausationRecognitionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognitionDetail', @level2type = N'COLUMN', @level2name = N'CausationRecognitionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (FK) hacia MedicalFeesCausation. Referencia la causación específica de honorarios médicos, procedimiento o servicio profesional causado.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognitionDetail', @level2type = N'COLUMN', @level2name = N'MedicalFeesCausationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la causación de honorarios médicos (referencia a MedicalFeesCausation)', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognitionDetail', @level2type = N'COLUMN', @level2name = N'MedicalFeesCausationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognitionDetail', @level2type = N'COLUMN', @level2name = N'MedicalFeesCausationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del profesional de la salud (médico, especialista, auxiliar) que prestó el servicio. CHAR(20), típicamente cédula o registro profesional.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognitionDetail', @level2type = N'COLUMN', @level2name = N'HealthProfessionalCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del profesional de la salud', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognitionDetail', @level2type = N'COLUMN', @level2name = N'HealthProfessionalCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognitionDetail', @level2type = N'COLUMN', @level2name = N'HealthProfessionalCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (FK) hacia ThirdParty. Identifica la entidad (EPS, IPS, asegurador, paciente) asociada a esta causación y pago.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognitionDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tercero asociado', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognitionDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognitionDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (FK) hacia MedicalFeesContract. Referencia el contrato de tarifas/honorarios médicos bajo el cual se reconoce este costo. Opcional (NULL).', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognitionDetail', @level2type = N'COLUMN', @level2name = N'MedicalFeesContractId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del contrato de honorarios médicos', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognitionDetail', @level2type = N'COLUMN', @level2name = N'MedicalFeesContractId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognitionDetail', @level2type = N'COLUMN', @level2name = N'MedicalFeesContractId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (FK) hacia FunctionalUnit. Identifica la unidad funcional (urgencias, hospitalización, consulta externa, quirófano, laboratorio) que realizó el servicio. Opcional (NULL).', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognitionDetail', @level2type = N'COLUMN', @level2name = N'PerformsFunctionalUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad funcional que realiza el servicio', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognitionDetail', @level2type = N'COLUMN', @level2name = N'PerformsFunctionalUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognitionDetail', @level2type = N'COLUMN', @level2name = N'PerformsFunctionalUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (FK) hacia CostCenter. Centro de costo asociado para contabilización y presupuesto. Opcional (NULL).', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognitionDetail', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del centro de costo', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognitionDetail', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognitionDetail', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de servicios/actos facturados en esta línea. Entero (INT), default 0. Ej: número de consultas, procedimientos o unidades prestadas.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognitionDetail', @level2type = N'COLUMN', @level2name = N'InvoiceQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad facturada', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognitionDetail', @level2type = N'COLUMN', @level2name = N'InvoiceQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognitionDetail', @level2type = N'COLUMN', @level2name = N'InvoiceQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor unitario a pagar por cada servicio/acto. NUMERIC(18,2), default 0. Moneda local. Base para cálculo de total.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognitionDetail', @level2type = N'COLUMN', @level2name = N'AmountPayable';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor unitario a pagar', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognitionDetail', @level2type = N'COLUMN', @level2name = N'AmountPayable';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognitionDetail', @level2type = N'COLUMN', @level2name = N'AmountPayable';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor total a pagar por la línea (AmountPayable × InvoiceQuantity). NUMERIC(18,2), default 0. Moneda local.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognitionDetail', @level2type = N'COLUMN', @level2name = N'TotalAmountPayable';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor total a pagar (AmountPayable x InvoiceQuantity)', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognitionDetail', @level2type = N'COLUMN', @level2name = N'TotalAmountPayable';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognitionDetail', @level2type = N'COLUMN', @level2name = N'TotalAmountPayable';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número o código de ingreso/atención del paciente en la IPS. VARCHAR(10), opcional (NULL). Vincula la causación al episodio de atención específico.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognitionDetail', @level2type = N'COLUMN', @level2name = N'AdmissionNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de ingreso del paciente', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognitionDetail', @level2type = N'COLUMN', @level2name = N'AdmissionNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognitionDetail', @level2type = N'COLUMN', @level2name = N'AdmissionNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla de detalle que almacena las líneas/items de cada reconocimiento de costos de causaciones médicas. Registra honorarios, servicios prestados y montos a pagar por profesional, tercero y contrato.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognitionDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tabla de detalle que almacena las causaciones incluidas en cada reconocimiento de costos', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognitionDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognitionDetail';

