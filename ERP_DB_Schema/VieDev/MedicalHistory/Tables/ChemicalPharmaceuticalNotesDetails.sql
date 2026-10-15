CREATE TABLE [MedicalHistory].[ChemicalPharmaceuticalNotesDetails] (
    [Id]                            INT       IDENTITY (1, 1) NOT NULL,
    [ChemicalPharmaceuticalNotesId] INT       NOT NULL,
    [CODPRODUC]                     CHAR (20) NOT NULL,
    CONSTRAINT [PK_ChemicalPharmaceuticalNotesDetails] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el codigo de producto', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ChemicalPharmaceuticalNotesDetails', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el quimico famaceutico Notas ID', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ChemicalPharmaceuticalNotesDetails', @level2type = N'COLUMN', @level2name = N'ChemicalPharmaceuticalNotesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ChemicalPharmaceuticalNotesDetails', @level2type = N'COLUMN', @level2name = N'Id';

