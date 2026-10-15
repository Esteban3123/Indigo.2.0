CREATE TABLE [EHR].[OncologycalSchemeByDrugs] (
    [Id]                    INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [OncologycalShemeId]    INT             NOT NULL,
    [DrugCode]              CHAR (20)       NOT NULL,
    [RouteOfAdministration] VARCHAR (20)    NOT NULL,
    [Dose]                  NUMERIC (18, 6) NOT NULL,
    [MeasurementUnit]       VARCHAR (20)    NOT NULL,
    [MaximumDose]           NUMERIC (18, 6) NULL,
    [Duration]              TINYINT         NOT NULL,
    [CycleFrequency]        VARCHAR (250)   NOT NULL,
    [Days]                  VARCHAR (100)   NOT NULL,
    CONSTRAINT [PK_OncologycalSchemesByDrugs] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_OncologycalSchemeByDrugs_OncologicalSchemes] FOREIGN KEY ([OncologycalShemeId]) REFERENCES [EHR].[OncologicalSchemes] ([Id]),
    CONSTRAINT [FK_OncologycalSchemesByDrugs_HCVIAADMI] FOREIGN KEY ([RouteOfAdministration]) REFERENCES [dbo].[HCVIAADMI] ([CODVIAADM]),
    CONSTRAINT [FK_OncologycalSchemesByDrugs_IHLISTPRO] FOREIGN KEY ([DrugCode]) REFERENCES [dbo].[IHLISTPRO] ([CODPRODUC]),
    CONSTRAINT [FK_OncologycalSchemesByDrugs_INUNIMEDI] FOREIGN KEY ([MeasurementUnit]) REFERENCES [dbo].[INUNIMEDI] ([CODUNIMED])
);


GO
ALTER TABLE [EHR].[OncologycalSchemeByDrugs] NOCHECK CONSTRAINT [FK_OncologycalSchemeByDrugs_OncologicalSchemes];


GO
ALTER TABLE [EHR].[OncologycalSchemeByDrugs] NOCHECK CONSTRAINT [FK_OncologycalSchemesByDrugs_HCVIAADMI];


GO
ALTER TABLE [EHR].[OncologycalSchemeByDrugs] NOCHECK CONSTRAINT [FK_OncologycalSchemesByDrugs_IHLISTPRO];


GO
ALTER TABLE [EHR].[OncologycalSchemeByDrugs] NOCHECK CONSTRAINT [FK_OncologycalSchemesByDrugs_INUNIMEDI];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda los  dias', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDrugs', @level2type = N'COLUMN', @level2name = N'Days';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la Frecuencia de ciclo', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDrugs', @level2type = N'COLUMN', @level2name = N'CycleFrequency';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la duración', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDrugs', @level2type = N'COLUMN', @level2name = N'Duration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la Dosis máxima', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDrugs', @level2type = N'COLUMN', @level2name = N'MaximumDose';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la Unidad de medida', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDrugs', @level2type = N'COLUMN', @level2name = N'MeasurementUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la dosis teorica', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDrugs', @level2type = N'COLUMN', @level2name = N'Dose';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la ruta o via  de administración', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDrugs', @level2type = N'COLUMN', @level2name = N'RouteOfAdministration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Código de medicamento o drogas', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDrugs', @level2type = N'COLUMN', @level2name = N'DrugCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el id esquemas oncologicos', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDrugs', @level2type = N'COLUMN', @level2name = N'OncologycalShemeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDrugs', @level2type = N'COLUMN', @level2name = N'Id';

