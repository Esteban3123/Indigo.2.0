CREATE TABLE [GeneralLedger].[RetentionConceptByCity] (
    [Id]                 INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [RetentionConceptId] INT            NOT NULL,
    [CityId]             INT            NOT NULL,
    [Rate]               DECIMAL (5, 3) NOT NULL,
    [TimeStamp]          ROWVERSION     NOT NULL,
    CONSTRAINT [PK_RetentionConceptByCity] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RetentionConceptByCity_City] FOREIGN KEY ([CityId]) REFERENCES [Common].[City] ([Id]),
    CONSTRAINT [FK_RetentionConceptByCity_RetentionConcept] FOREIGN KEY ([RetentionConceptId]) REFERENCES [GeneralLedger].[RetentionConcepts] ([Id])
);


GO
ALTER TABLE [GeneralLedger].[RetentionConceptByCity] NOCHECK CONSTRAINT [FK_RetentionConceptByCity_City];


GO
ALTER TABLE [GeneralLedger].[RetentionConceptByCity] NOCHECK CONSTRAINT [FK_RetentionConceptByCity_RetentionConcept];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'RetentionConceptByCity', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Porcentaje de retencion', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'RetentionConceptByCity', @level2type = N'COLUMN', @level2name = N'Rate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la ciudad', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'RetentionConceptByCity', @level2type = N'COLUMN', @level2name = N'CityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del concepto de retencion', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'RetentionConceptByCity', @level2type = N'COLUMN', @level2name = N'RetentionConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'RetentionConceptByCity', @level2type = N'COLUMN', @level2name = N'Id';

