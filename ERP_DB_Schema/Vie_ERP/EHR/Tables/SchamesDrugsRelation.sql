CREATE TABLE [EHR].[SchamesDrugsRelation] (
    [ID]                     INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdSchemesDrugs]         INT             NOT NULL,
    [DrugCode]               CHAR (20)       NOT NULL,
    [CostMinimumUnitMeasure] DECIMAL (18, 2) CONSTRAINT [DF_SchamesDrugsRelation_CostMinimumUnitMeasure] DEFAULT ((1)) NULL,
    CONSTRAINT [PK_SchamesDrugsRelation] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_SchamesDrugsRelation_SchemesDrugs] FOREIGN KEY ([IdSchemesDrugs]) REFERENCES [EHR].[SchemesDrugs] ([Id])
);


GO
ALTER TABLE [EHR].[SchamesDrugsRelation] NOCHECK CONSTRAINT [FK_SchamesDrugsRelation_SchemesDrugs];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Costo por minima unidad de medida:', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchamesDrugsRelation', @level2type = N'COLUMN', @level2name = N'CostMinimumUnitMeasure';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Código de drogas', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchamesDrugsRelation', @level2type = N'COLUMN', @level2name = N'DrugCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el IdEsquemasDrogas', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchamesDrugsRelation', @level2type = N'COLUMN', @level2name = N'IdSchemesDrugs';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchamesDrugsRelation', @level2type = N'COLUMN', @level2name = N'ID';

