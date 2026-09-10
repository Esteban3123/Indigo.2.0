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



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Días específicos de administración del medicamento dentro del ciclo oncológico; días de tratamiento (VARCHAR 100)', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDrugs', @level2type = N'COLUMN', @level2name = N'Days';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda los  dias', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDrugs', @level2type = N'COLUMN', @level2name = N'Days';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDrugs', @level2type = N'COLUMN', @level2name = N'Days';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Frecuencia de repetición del ciclo de quimioterapia; patrón temporal de aplicación del esquema (VARCHAR 250)', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDrugs', @level2type = N'COLUMN', @level2name = N'CycleFrequency';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la Frecuencia de ciclo', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDrugs', @level2type = N'COLUMN', @level2name = N'CycleFrequency';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDrugs', @level2type = N'COLUMN', @level2name = N'CycleFrequency';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Duración en días de la administración del fármaco oncológico por ciclo; período de aplicación (TINYINT)', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDrugs', @level2type = N'COLUMN', @level2name = N'Duration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la duración', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDrugs', @level2type = N'COLUMN', @level2name = N'Duration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDrugs', @level2type = N'COLUMN', @level2name = N'Duration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dosis máxima permitida del medicamento oncológico; límite superior de dosificación (NUMERIC 18,6, nullable)', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDrugs', @level2type = N'COLUMN', @level2name = N'MaximumDose';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la Dosis máxima', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDrugs', @level2type = N'COLUMN', @level2name = N'MaximumDose';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDrugs', @level2type = N'COLUMN', @level2name = N'MaximumDose';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de medida de la dosis; referencia a INUNIMEDI (mg, ml, UI, etc.) (VARCHAR 20, FK)', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDrugs', @level2type = N'COLUMN', @level2name = N'MeasurementUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la Unidad de medida', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDrugs', @level2type = N'COLUMN', @level2name = N'MeasurementUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDrugs', @level2type = N'COLUMN', @level2name = N'MeasurementUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dosis teórica del fármaco oncológico por administración; cantidad calculada del medicamento (NUMERIC 18,6)', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDrugs', @level2type = N'COLUMN', @level2name = N'Dose';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la dosis teorica', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDrugs', @level2type = N'COLUMN', @level2name = N'Dose';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDrugs', @level2type = N'COLUMN', @level2name = N'Dose';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ruta o vía de administración del medicamento; referencia a HCVIAADMI (intravenosa, oral, intramuscular, etc.) (CHAR 20, FK)', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDrugs', @level2type = N'COLUMN', @level2name = N'RouteOfAdministration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la ruta o via  de administración', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDrugs', @level2type = N'COLUMN', @level2name = N'RouteOfAdministration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDrugs', @level2type = N'COLUMN', @level2name = N'RouteOfAdministration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del medicamento o droga oncológica; referencia a IHLISTPRO para identificación del fármaco (CHAR 20, FK)', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDrugs', @level2type = N'COLUMN', @level2name = N'DrugCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Código de medicamento o drogas', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDrugs', @level2type = N'COLUMN', @level2name = N'DrugCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDrugs', @level2type = N'COLUMN', @level2name = N'DrugCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del esquema oncológico asociado; referencia a OncologicalSchemes para trazabilidad del tratamiento (INT, FK)', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDrugs', @level2type = N'COLUMN', @level2name = N'OncologycalShemeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el id esquemas oncologicos', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDrugs', @level2type = N'COLUMN', @level2name = N'OncologycalShemeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDrugs', @level2type = N'COLUMN', @level2name = N'OncologycalShemeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (consecutivo) de la relación medicamento-esquema oncológico; clave primaria (INT IDENTITY)', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDrugs', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDrugs', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDrugs', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de medicamentos (drogas) que componen cada esquema oncológico: dosis, vía de administración, frecuencia de ciclos y días de aplicación para cada fármaco dentro de un protocolo de quimioterapia u oncología.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDrugs';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDrugs';
