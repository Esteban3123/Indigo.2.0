CREATE TABLE [MedicalFees].[CausationRecognitionDetail] (
    [Id]                                    INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CausationRecognitionId]                INT             NOT NULL,
    [MedicalFeesCausationId]                INT             NOT NULL,
    [HealthProfessionalCode]                CHAR (20)       NOT NULL,
    [ThirdPartyId]                          INT             NOT NULL,
    [MedicalFeesContractId]                 INT             NULL,
    [PerformsFunctionalUnitId]              INT             NULL,
    [CostCenterId]                          INT             NULL,
    [InvoiceQuantity]                       INT             CONSTRAINT [DF_CausationRecognitionDetail_InvoiceQuantity] DEFAULT ((0)) NOT NULL,
    [AmountPayable]                         NUMERIC (18, 2) CONSTRAINT [DF_CausationRecognitionDetail_AmountPayable] DEFAULT ((0)) NOT NULL,
    [TotalAmountPayable]                    NUMERIC (18, 2) CONSTRAINT [DF_CausationRecognitionDetail_TotalAmountPayable] DEFAULT ((0)) NOT NULL,
    [AdmissionNumber]                       VARCHAR (10)    NULL,
    CONSTRAINT [PK_CausationRecognitionDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CausationRecognitionDetail_CausationRecognition] FOREIGN KEY ([CausationRecognitionId]) REFERENCES [MedicalFees].[CausationRecognition] ([Id]),
    CONSTRAINT [FK_CausationRecognitionDetail_MedicalFeesCausation] FOREIGN KEY ([MedicalFeesCausationId]) REFERENCES [MedicalFees].[MedicalFeesCausation] ([Id]),
    CONSTRAINT [FK_CausationRecognitionDetail_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id]),
    CONSTRAINT [FK_CausationRecognitionDetail_MedicalFeesContract] FOREIGN KEY ([MedicalFeesContractId]) REFERENCES [MedicalFees].[MedicalFeesContract] ([Id]),
    CONSTRAINT [FK_CausationRecognitionDetail_FunctionalUnit] FOREIGN KEY ([PerformsFunctionalUnitId]) REFERENCES [Payroll].[FunctionalUnit] ([Id]),
    CONSTRAINT [FK_CausationRecognitionDetail_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
);


GO
ALTER TABLE [MedicalFees].[CausationRecognitionDetail] NOCHECK CONSTRAINT [FK_CausationRecognitionDetail_CausationRecognition];


GO
ALTER TABLE [MedicalFees].[CausationRecognitionDetail] NOCHECK CONSTRAINT [FK_CausationRecognitionDetail_MedicalFeesCausation];


GO
ALTER TABLE [MedicalFees].[CausationRecognitionDetail] NOCHECK CONSTRAINT [FK_CausationRecognitionDetail_ThirdParty];


GO
ALTER TABLE [MedicalFees].[CausationRecognitionDetail] NOCHECK CONSTRAINT [FK_CausationRecognitionDetail_MedicalFeesContract];


GO
ALTER TABLE [MedicalFees].[CausationRecognitionDetail] NOCHECK CONSTRAINT [FK_CausationRecognitionDetail_FunctionalUnit];


GO
ALTER TABLE [MedicalFees].[CausationRecognitionDetail] NOCHECK CONSTRAINT [FK_CausationRecognitionDetail_CostCenter];


GO
CREATE NONCLUSTERED INDEX [IX_CausationRecognitionDetail_MedicalFeesCausationId]
    ON [MedicalFees].[CausationRecognitionDetail]([MedicalFeesCausationId] ASC)
    INCLUDE([CausationRecognitionId], [TotalAmountPayable]);


GO
CREATE NONCLUSTERED INDEX [IX_CausationRecognitionDetail_CausationRecognitionId]
    ON [MedicalFees].[CausationRecognitionDetail]([CausationRecognitionId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Llave autonumérica', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognitionDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera del reconocimiento', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognitionDetail', @level2type = N'COLUMN', @level2name = N'CausationRecognitionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la causación de honorarios médicos (referencia a MedicalFeesCausation)', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognitionDetail', @level2type = N'COLUMN', @level2name = N'MedicalFeesCausationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del profesional de la salud', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognitionDetail', @level2type = N'COLUMN', @level2name = N'HealthProfessionalCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tercero asociado', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognitionDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del contrato de honorarios médicos', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognitionDetail', @level2type = N'COLUMN', @level2name = N'MedicalFeesContractId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad funcional que realiza el servicio', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognitionDetail', @level2type = N'COLUMN', @level2name = N'PerformsFunctionalUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del centro de costo', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognitionDetail', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad facturada', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognitionDetail', @level2type = N'COLUMN', @level2name = N'InvoiceQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor unitario a pagar', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognitionDetail', @level2type = N'COLUMN', @level2name = N'AmountPayable';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor total a pagar (AmountPayable x InvoiceQuantity)', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognitionDetail', @level2type = N'COLUMN', @level2name = N'TotalAmountPayable';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de ingreso del paciente', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognitionDetail', @level2type = N'COLUMN', @level2name = N'AdmissionNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla de detalle que almacena las causaciones incluidas en cada reconocimiento de costos', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationRecognitionDetail';

