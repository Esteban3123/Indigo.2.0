CREATE TABLE [MedicalHistory].[DetailPhysicalCUM] (
    [Id]                INT              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCFISIPRO]       INT              NOT NULL,
    [ProductId]         INT              NOT NULL,
    [BatchCode]         VARCHAR (50)     NOT NULL,
    [DateExpiration]    DATETIME         NOT NULL,
    [Hour]              DATETIME         NULL,
    [DispensedQuantity] INT              NOT NULL,
    [UsedQuantity]      INT              NOT NULL,
    [GroupingCodeDose]  UNIQUEIDENTIFIER NULL,
    [ReturnedQuantity]  INT              DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_DetailPhysicalCUM] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la Cantidad devuelta', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'DetailPhysicalCUM', @level2type = N'COLUMN', @level2name = N'ReturnedQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la agrupacion de dosis', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'DetailPhysicalCUM', @level2type = N'COLUMN', @level2name = N'GroupingCodeDose';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad utilizada, esto cuando se aplica el medicamento desde enfermeria, aca se va  aumentando la cantidad utilizada.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'DetailPhysicalCUM', @level2type = N'COLUMN', @level2name = N'UsedQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad dispensada por vie erp', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'DetailPhysicalCUM', @level2type = N'COLUMN', @level2name = N'DispensedQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la Hora', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'DetailPhysicalCUM', @level2type = N'COLUMN', @level2name = N'Hour';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la fecha de vencimiento', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'DetailPhysicalCUM', @level2type = N'COLUMN', @level2name = N'DateExpiration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el código del Lote', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'DetailPhysicalCUM', @level2type = N'COLUMN', @level2name = N'BatchCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Id del producto', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'DetailPhysicalCUM', @level2type = N'COLUMN', @level2name = N'ProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Id examen fisico de los producto', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'DetailPhysicalCUM', @level2type = N'COLUMN', @level2name = N'IDHCFISIPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'DetailPhysicalCUM', @level2type = N'COLUMN', @level2name = N'Id';

