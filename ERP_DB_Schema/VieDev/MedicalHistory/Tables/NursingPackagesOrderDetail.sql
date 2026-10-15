CREATE TABLE [MedicalHistory].[NursingPackagesOrderDetail] (
    [Id]                     INT       IDENTITY (1, 1) NOT NULL,
    [IdNursingPackagesOrder] INT       NOT NULL,
    [CODPRODUC]              CHAR (20) NOT NULL,
    [Quantity]               INT       NOT NULL,
    [CurrentQuantity]        INT       NULL,
    CONSTRAINT [PK_HcfarmepdNursingPackagesDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_NursingPackagesOrderDetail_NursingPackagesOrder] FOREIGN KEY ([IdNursingPackagesOrder]) REFERENCES [MedicalHistory].[NursingPackagesOrder] ([Id])
);


GO
ALTER TABLE [MedicalHistory].[NursingPackagesOrderDetail] NOCHECK CONSTRAINT [FK_NursingPackagesOrderDetail_NursingPackagesOrder];


GO
CREATE NONCLUSTERED INDEX [IX_NursingPackagesOrderDetail_CODPRODUC_IdNursingPackagesOrder_CurrentQuantity]
    ON [MedicalHistory].[NursingPackagesOrderDetail]([CODPRODUC] ASC, [IdNursingPackagesOrder] ASC)
    INCLUDE([CurrentQuantity]);


GO
CREATE NONCLUSTERED INDEX [IX_NursingPackagesOrderDetail_Quantity_IdNursingPackagesOrder]
    ON [MedicalHistory].[NursingPackagesOrderDetail]([Quantity] ASC, [IdNursingPackagesOrder] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la Cantidad actual', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'NursingPackagesOrderDetail', @level2type = N'COLUMN', @level2name = N'CurrentQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la cantidad', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'NursingPackagesOrderDetail', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  el codigo del producto', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'NursingPackagesOrderDetail', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Id Enfermería Paquetes Orden', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'NursingPackagesOrderDetail', @level2type = N'COLUMN', @level2name = N'IdNursingPackagesOrder';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'NursingPackagesOrderDetail', @level2type = N'COLUMN', @level2name = N'Id';

