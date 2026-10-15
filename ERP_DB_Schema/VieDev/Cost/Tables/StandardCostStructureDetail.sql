CREATE TABLE [Cost].[StandardCostStructureDetail] (
    [Id]                      INT          IDENTITY (1, 1) NOT NULL,
    [StandardCostStructureId] INT          NOT NULL,
    [CUPSEntityId]            INT          NOT NULL,
    [InitialDate]             DATE         NOT NULL,
    [EndDate]                 DATE         NOT NULL,
    [Observations]            VARCHAR (50) NULL,
    CONSTRAINT [PK_StandardCostStructureDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_StandardCostStructure_StandardCostStructureDetail] FOREIGN KEY ([StandardCostStructureId]) REFERENCES [Cost].[StandardCostStructure] ([Id]),
    CONSTRAINT [FK_StandardCostStructureDetail_CUPSEntity] FOREIGN KEY ([CUPSEntityId]) REFERENCES [Contract].[CUPSEntity] ([Id])
);


GO
ALTER TABLE [Cost].[StandardCostStructureDetail] NOCHECK CONSTRAINT [FK_StandardCostStructure_StandardCostStructureDetail];


GO
ALTER TABLE [Cost].[StandardCostStructureDetail] NOCHECK CONSTRAINT [FK_StandardCostStructureDetail_CUPSEntity];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones del detalle de la estructura de costos estándar', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'StandardCostStructureDetail', @level2type = N'COLUMN', @level2name = N'Observations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha final.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'StandardCostStructureDetail', @level2type = N'COLUMN', @level2name = N'EndDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha inicial.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'StandardCostStructureDetail', @level2type = N'COLUMN', @level2name = N'InitialDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la entidad del CUPS.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'StandardCostStructureDetail', @level2type = N'COLUMN', @level2name = N'CUPSEntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la estructura de costos estándar - tabla StandardCostStructure', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'StandardCostStructureDetail', @level2type = N'COLUMN', @level2name = N'StandardCostStructureId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'StandardCostStructureDetail', @level2type = N'COLUMN', @level2name = N'Id';

