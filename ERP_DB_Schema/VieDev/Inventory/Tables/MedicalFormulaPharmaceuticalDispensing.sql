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

