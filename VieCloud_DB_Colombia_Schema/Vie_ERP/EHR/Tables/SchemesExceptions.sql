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



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera (BIT) que indica si el medicamento se EXCLUYE (no aplica) en ese día del ciclo, anulando su administración.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesExceptions', @level2type = N'COLUMN', @level2name = N'ExcludesDay';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Determina si para ese dia el medicamento no se aplica en el ciclo', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesExceptions', @level2type = N'COLUMN', @level2name = N'ExcludesDay';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesExceptions', @level2type = N'COLUMN', @level2name = N'ExcludesDay';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del medicamento diluyente o vehículo (CHAR 20). Producto usado para diluir o preparar el medicamento en esa excepción.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesExceptions', @level2type = N'COLUMN', @level2name = N'DiluentDrugCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de producto Diluyente', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesExceptions', @level2type = N'COLUMN', @level2name = N'DiluentDrugCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesExceptions', @level2type = N'COLUMN', @level2name = N'DiluentDrugCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dosis excepcional en unidades (NUMERIC 18,2). Sobreescribe la dosis estándar del medicamento para ese día y ciclo.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesExceptions', @level2type = N'COLUMN', @level2name = N'Dose';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dosis de Excepción', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesExceptions', @level2type = N'COLUMN', @level2name = N'Dose';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesExceptions', @level2type = N'COLUMN', @level2name = N'Dose';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de día del ciclo donde se define la excepción de medicamento, dose o aplicación.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesExceptions', @level2type = N'COLUMN', @level2name = N'Day';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de día de excepción', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesExceptions', @level2type = N'COLUMN', @level2name = N'Day';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesExceptions', @level2type = N'COLUMN', @level2name = N'Day';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ciclo (entero) donde aplica la excepción en el esquema de dosificación periódica o protocolaria.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesExceptions', @level2type = N'COLUMN', @level2name = N'Cycle';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de ciclo de la excepción', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesExceptions', @level2type = N'COLUMN', @level2name = N'Cycle';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesExceptions', @level2type = N'COLUMN', @level2name = N'Cycle';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la relación esquema-medicamento (EHR.SchemesDrugs). FK que vincula el medicamento específico dentro del esquema.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesExceptions', @level2type = N'COLUMN', @level2name = N'SchemesDrugsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del esquema X medicamento (EHR.SchemesDrugs)', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesExceptions', @level2type = N'COLUMN', @level2name = N'SchemesDrugsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesExceptions', @level2type = N'COLUMN', @level2name = N'SchemesDrugsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del esquema terapéutico o de medicación. FK a EHR.Schemes. Agrupa medicamentos por plan de tratamiento.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesExceptions', @level2type = N'COLUMN', @level2name = N'SchemesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del esquema', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesExceptions', @level2type = N'COLUMN', @level2name = N'SchemesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesExceptions', @level2type = N'COLUMN', @level2name = N'SchemesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (IDENTITY) de la excepción de esquema. Clave primaria de la tabla SchemesExceptions.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesExceptions', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesExceptions', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesExceptions', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Excepciones dentro de los esquemas de quimioterapia u oncología: ajustes puntuales a un ciclo y día específico de un esquema de medicamentos, donde se puede modificar la dosis, el diluyente o excluir un día de administración.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesExceptions';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesExceptions';
