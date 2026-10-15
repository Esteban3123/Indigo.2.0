CREATE TABLE [Inventory].[MedicalFormulaDetailAnnulled] (
    [Id]                     INT      IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [MedicalFormulaDetailId] INT      NOT NULL,
    [AnnulmentQuantity]      INT      NOT NULL,
    [AnnulmentUser]          INT      NOT NULL,
    [AnnulmentDate]          DATETIME NOT NULL,
    CONSTRAINT [PK_MedicalFormulaDetailAnnulled] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MedicalFormulaDetailAnnulled_MedicalFormulaDetail] FOREIGN KEY ([MedicalFormulaDetailId]) REFERENCES [Inventory].[MedicalFormulaDetail] ([Id])
);


GO
ALTER TABLE [Inventory].[MedicalFormulaDetailAnnulled] NOCHECK CONSTRAINT [FK_MedicalFormulaDetailAnnulled_MedicalFormulaDetail];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha en la que se realiza la aunlación del detalle de la fórmula médica', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailAnnulled', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del usuario que esta realizando la anulación del detalle de la fórmula médica', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailAnnulled', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad del producto que se esta anulando', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailAnnulled', @level2type = N'COLUMN', @level2name = N'AnnulmentQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del detalle de la fórmula médica que se esta anulando', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailAnnulled', @level2type = N'COLUMN', @level2name = N'MedicalFormulaDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del detalle de la fórmula médica anulada', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailAnnulled', @level2type = N'COLUMN', @level2name = N'Id';

