CREATE TABLE [MedicalFees].[GlosaMedicalFeesDetail] (
    [Id]                         INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [GlosaMedicalFeesId]         INT             NOT NULL,
    [AccountPayableId]           INT             NOT NULL,
    [GlosaMedicalFeesConceptsId] INT             NOT NULL,
    [UnitValue]                  DECIMAL (18, 2) NOT NULL,
    [Quantity]                   INT             NOT NULL,
    [TotalValue]                 DECIMAL (18, 2) NOT NULL,
    [Observation]                VARCHAR (300)   NULL,
    [Evaluated]                  BIT             NOT NULL,
    CONSTRAINT [PK_GlosaMedicalFeesDetail__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_GlosaMedicalFeesDetail_AccountPayable] FOREIGN KEY ([AccountPayableId]) REFERENCES [Payments].[AccountPayable] ([Id]),
    CONSTRAINT [FK_GlosaMedicalFeesDetail_GlosaMedicalFees] FOREIGN KEY ([GlosaMedicalFeesId]) REFERENCES [MedicalFees].[GlosaMedicalFees] ([Id]),
    CONSTRAINT [FK_GlosaMedicalFeesDetail_GlosaMedicalFeesConcepts] FOREIGN KEY ([GlosaMedicalFeesConceptsId]) REFERENCES [MedicalFees].[GlosaMedicalFeesConcepts] ([Id])
);


GO
ALTER TABLE [MedicalFees].[GlosaMedicalFeesDetail] NOCHECK CONSTRAINT [FK_GlosaMedicalFeesDetail_AccountPayable];


GO
ALTER TABLE [MedicalFees].[GlosaMedicalFeesDetail] NOCHECK CONSTRAINT [FK_GlosaMedicalFeesDetail_GlosaMedicalFees];


GO
ALTER TABLE [MedicalFees].[GlosaMedicalFeesDetail] NOCHECK CONSTRAINT [FK_GlosaMedicalFeesDetail_GlosaMedicalFeesConcepts];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'identifica si el regitro esta evaluado o no', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFeesDetail', @level2type = N'COLUMN', @level2name = N'Evaluated';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observación', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFeesDetail', @level2type = N'COLUMN', @level2name = N'Observation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor total Glosado', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFeesDetail', @level2type = N'COLUMN', @level2name = N'TotalValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad de los valores unitarios Glosado', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFeesDetail', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor unitario Glosado', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFeesDetail', @level2type = N'COLUMN', @level2name = N'UnitValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla GlosaMedicalFeesConcepts', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFeesDetail', @level2type = N'COLUMN', @level2name = N'GlosaMedicalFeesConceptsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla AccountPayable', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFeesDetail', @level2type = N'COLUMN', @level2name = N'AccountPayableId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla GlosaMedicalFees', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFeesDetail', @level2type = N'COLUMN', @level2name = N'GlosaMedicalFeesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFeesDetail', @level2type = N'COLUMN', @level2name = N'Id';

