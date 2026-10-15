CREATE TABLE [Inventory].[MedicalFormulaPharmaceuticalDispensing] (
    [Id]                           INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [MedicalFormulaId]             INT          NOT NULL,
    [PharmaceuticalDispensingId]   INT          NOT NULL,
    [PharmaceuticalDispensingCode] VARCHAR (20) NOT NULL,
    CONSTRAINT [PK_MedicalFormulaPharmaceuticalDispensing_1] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MedicalFormulaPharmaceuticalDispensing_MedicalFormula] FOREIGN KEY ([MedicalFormulaId]) REFERENCES [Inventory].[MedicalFormula] ([Id]),
    CONSTRAINT [FK_MedicalFormulaPharmaceuticalDispensing_PharmaceuticalDispensing] FOREIGN KEY ([PharmaceuticalDispensingId]) REFERENCES [Inventory].[PharmaceuticalDispensing] ([Id])
);


GO
ALTER TABLE [Inventory].[MedicalFormulaPharmaceuticalDispensing] NOCHECK CONSTRAINT [FK_MedicalFormulaPharmaceuticalDispensing_MedicalFormula];


GO
ALTER TABLE [Inventory].[MedicalFormulaPharmaceuticalDispensing] NOCHECK CONSTRAINT [FK_MedicalFormulaPharmaceuticalDispensing_PharmaceuticalDispensing];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de dispensación farmacéutica', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaPharmaceuticalDispensing', @level2type = N'COLUMN', @level2name = N'PharmaceuticalDispensingCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de dispensación farmacéutica', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaPharmaceuticalDispensing', @level2type = N'COLUMN', @level2name = N'PharmaceuticalDispensingId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de fórmula médica', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaPharmaceuticalDispensing', @level2type = N'COLUMN', @level2name = N'MedicalFormulaId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaPharmaceuticalDispensing', @level2type = N'COLUMN', @level2name = N'Id';

