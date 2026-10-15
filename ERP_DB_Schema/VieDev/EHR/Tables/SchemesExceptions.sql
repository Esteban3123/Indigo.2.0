CREATE TABLE [EHR].[SchemesExceptions] (
    [Id]              INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [SchemesId]       INT             NOT NULL,
    [SchemesDrugsId]  INT             NOT NULL,
    [Cycle]           INT             NOT NULL,
    [Day]             INT             NOT NULL,
    [Dose]            NUMERIC (18, 2) NULL,
    [DiluentDrugCode] CHAR (20)       NULL,
    [ExcludesDay]     BIT             NOT NULL,
    CONSTRAINT [PK_SchemesExceptions] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SchemesExceptions_Schemes] FOREIGN KEY ([SchemesId]) REFERENCES [EHR].[Schemes] ([Id]),
    CONSTRAINT [FK_SchemesExceptions_SchemesDrugs] FOREIGN KEY ([SchemesDrugsId]) REFERENCES [EHR].[SchemesDrugs] ([Id])
);


GO
ALTER TABLE [EHR].[SchemesExceptions] NOCHECK CONSTRAINT [FK_SchemesExceptions_Schemes];


GO
ALTER TABLE [EHR].[SchemesExceptions] NOCHECK CONSTRAINT [FK_SchemesExceptions_SchemesDrugs];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Determina si para ese dia el medicamento no se aplica en el ciclo', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesExceptions', @level2type = N'COLUMN', @level2name = N'ExcludesDay';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de producto Diluyente', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesExceptions', @level2type = N'COLUMN', @level2name = N'DiluentDrugCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dosis de Excepción', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesExceptions', @level2type = N'COLUMN', @level2name = N'Dose';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de día de excepción', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesExceptions', @level2type = N'COLUMN', @level2name = N'Day';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de ciclo de la excepción', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesExceptions', @level2type = N'COLUMN', @level2name = N'Cycle';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del esquema X medicamento (EHR.SchemesDrugs)', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesExceptions', @level2type = N'COLUMN', @level2name = N'SchemesDrugsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del esquema', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesExceptions', @level2type = N'COLUMN', @level2name = N'SchemesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesExceptions', @level2type = N'COLUMN', @level2name = N'Id';

