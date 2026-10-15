CREATE TABLE [GeneralLedger].[RetentionConceptRanges] (
    [Id]             INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [RetentionId]    INT             NOT NULL,
    [Percentage]     DECIMAL (18, 2) NOT NULL,
    [ValueInitial]   DECIMAL (18, 2) NOT NULL,
    [ValueFinish]    DECIMAL (18, 2) NOT NULL,
    [ValueDeducted]  DECIMAL (18, 2) NOT NULL,
    [ValueIncrement] DECIMAL (5, 2)  NOT NULL,
    [UVTIncrement]   DECIMAL (18, 2) CONSTRAINT [DF_RetentionConceptRanges_UVTIncrement] DEFAULT ((0)) NOT NULL,
    [TimeStamp]      ROWVERSION      NOT NULL,
    CONSTRAINT [PK_AccountingRetention] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AccountingRetention_RetentionConcept] FOREIGN KEY ([RetentionId]) REFERENCES [GeneralLedger].[RetentionConcepts] ([Id])
);


GO
ALTER TABLE [GeneralLedger].[RetentionConceptRanges] NOCHECK CONSTRAINT [FK_AccountingRetention_RetentionConcept];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'RetentionConceptRanges', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el incremento expresado en UVT que se ñe va a realizar al impuesto', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'RetentionConceptRanges', @level2type = N'COLUMN', @level2name = N'UVTIncrement';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del incremento que debe ser expresado en porcentaje', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'RetentionConceptRanges', @level2type = N'COLUMN', @level2name = N'ValueIncrement';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor deducido expresado en UVT', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'RetentionConceptRanges', @level2type = N'COLUMN', @level2name = N'ValueDeducted';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Final del rango expresado en UVT', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'RetentionConceptRanges', @level2type = N'COLUMN', @level2name = N'ValueFinish';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor inicial del rango expresado en UVT', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'RetentionConceptRanges', @level2type = N'COLUMN', @level2name = N'ValueInitial';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del porcentaje o valor de la retencion segun el caso (383, 384)', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'RetentionConceptRanges', @level2type = N'COLUMN', @level2name = N'Percentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id retencion', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'RetentionConceptRanges', @level2type = N'COLUMN', @level2name = N'RetentionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la retencion de contabilizacion', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'RetentionConceptRanges', @level2type = N'COLUMN', @level2name = N'Id';

