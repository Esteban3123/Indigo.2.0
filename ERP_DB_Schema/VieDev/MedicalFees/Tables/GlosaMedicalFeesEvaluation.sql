CREATE TABLE [MedicalFees].[GlosaMedicalFeesEvaluation] (
    [Id]                       INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [GlosaMedicalFeesDetailId] INT             NOT NULL,
    [AcceptedValueProv]        DECIMAL (18, 2) NOT NULL,
    [RaisedValue]              DECIMAL (18, 2) NOT NULL,
    [PendingValue]             DECIMAL (18, 2) NOT NULL,
    [Observation]              VARCHAR (300)   NULL,
    CONSTRAINT [PK_GlosaMedicalFeesEvaluation__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_GlosaMedicalFeesEvaluation_GlosaMedicalFeesDetail] FOREIGN KEY ([GlosaMedicalFeesDetailId]) REFERENCES [MedicalFees].[GlosaMedicalFeesDetail] ([Id])
);


GO
ALTER TABLE [MedicalFees].[GlosaMedicalFeesEvaluation] NOCHECK CONSTRAINT [FK_GlosaMedicalFeesEvaluation_GlosaMedicalFeesDetail];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observación', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFeesEvaluation', @level2type = N'COLUMN', @level2name = N'Observation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Pendiente', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFeesEvaluation', @level2type = N'COLUMN', @level2name = N'PendingValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Levantado ', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFeesEvaluation', @level2type = N'COLUMN', @level2name = N'RaisedValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Aceptado Proveedor', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFeesEvaluation', @level2type = N'COLUMN', @level2name = N'AcceptedValueProv';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla GlosaMedicalFeesDetail', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFeesEvaluation', @level2type = N'COLUMN', @level2name = N'GlosaMedicalFeesDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'GlosaMedicalFeesEvaluation', @level2type = N'COLUMN', @level2name = N'Id';

