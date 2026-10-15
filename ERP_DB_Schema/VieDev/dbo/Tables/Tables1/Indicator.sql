CREATE TABLE [dbo].[Indicator] (
    [Id]                               INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                             VARCHAR (20)    NOT NULL,
    [Name]                             VARCHAR (200)   NOT NULL,
    [InstitutionalDocumentId]          INT             NOT NULL,
    [StandardAccreditation]            VARCHAR (20)    NOT NULL,
    [ObjectiveMeasureUnitId]           INT             NOT NULL,
    [ObjectiveCondition]               TINYINT         NOT NULL,
    [ObjectiveValue]                   NUMERIC (18, 2) NOT NULL,
    [ObjectiveValueTwo]                NUMERIC (18, 2) NULL,
    [Periodicity]                      TINYINT         NOT NULL,
    [GeneratingDataResponsibleId]      INT             NOT NULL,
    [CalculationAnalysisResponsibleId] INT             NOT NULL,
    [DecisionMakingResponsibleId]      INT             NOT NULL,
    [CommitteeId]                      INT             NOT NULL,
    [IndicatorFormula]                 VARCHAR (MAX)   NOT NULL,
    CONSTRAINT [PK_Indicator__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Indicator_InstitutionalDocument] FOREIGN KEY ([InstitutionalDocumentId]) REFERENCES [dbo].[InstitutionalDocument] ([Id])
);


GO
ALTER TABLE [dbo].[Indicator] NOCHECK CONSTRAINT [FK_Indicator_InstitutionalDocument];


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_Indicator__Code]
    ON [dbo].[Indicator]([Code] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el id del comite donde se socializa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Indicator', @level2type = N'COLUMN', @level2name = N'CommitteeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el id del responsable de la toma de decisiones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Indicator', @level2type = N'COLUMN', @level2name = N'DecisionMakingResponsibleId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el id del responsable del calculo y analisis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Indicator', @level2type = N'COLUMN', @level2name = N'CalculationAnalysisResponsibleId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el id del responsable de generar los datos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Indicator', @level2type = N'COLUMN', @level2name = N'GeneratingDataResponsibleId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Periodicidad del indicador  1 - Diario  2 - Semanal  3 - Quincenal  4 - Mensual  5 - Bimensual  6 - Trimensual  7 - Semestral   8 - Anual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Indicator', @level2type = N'COLUMN', @level2name = N'Periodicity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el valor de la meta, este solo se llena si el tipo de condicion es  ENTRE, Ejemplo Entre el 2% y el 5%', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Indicator', @level2type = N'COLUMN', @level2name = N'ObjectiveValueTwo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el valor de la meta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Indicator', @level2type = N'COLUMN', @level2name = N'ObjectiveValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la condicion para la meta   1 - <  2 - <=  3 - >  4 - >=  5 - Entre', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Indicator', @level2type = N'COLUMN', @level2name = N'ObjectiveCondition';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de unidad de medida objetiva', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Indicator', @level2type = N'COLUMN', @level2name = N'ObjectiveMeasureUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estandar de acreditacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Indicator', @level2type = N'COLUMN', @level2name = N'StandardAccreditation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del documento institucional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Indicator', @level2type = N'COLUMN', @level2name = N'InstitutionalDocumentId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del indicador', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Indicator', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del indicador', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Indicator', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del indicador', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Indicator', @level2type = N'COLUMN', @level2name = N'Id';

