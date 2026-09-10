CREATE TABLE [ClinicalParameters].[DefinitionSamples] (
    [Id]               INT           IDENTITY (1, 1) NOT NULL,
    [Code]             VARCHAR (4)   NOT NULL,
    [Type]             INT           NOT NULL,
    [Description]      VARCHAR (200) NOT NULL,
    [State]            BIT           NOT NULL,
    [CreationUser]     CHAR (20)     NOT NULL,
    [CreationDate]     DATE          NOT NULL,
    [UserModification] CHAR (20)     NULL,
    [ModificationDate] DATE          NULL,
    CONSTRAINT [PK_DefinitionSamples] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de última modificación de la definición de muestra (DATE). Nula si no ha sido editada desde creación.', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'DefinitionSamples', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha modificación', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'DefinitionSamples', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'DefinitionSamples', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario/identificación (CHAR 20) que realizó la última modificación de la definición. Nulo si no hay edición posterior.', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'DefinitionSamples', @level2type = N'COLUMN', @level2name = N'UserModification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario que modifica', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'DefinitionSamples', @level2type = N'COLUMN', @level2name = N'UserModification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'DefinitionSamples', @level2type = N'COLUMN', @level2name = N'UserModification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de creación de la definición de muestra (DATE). Registro de auditoría del origen del dato.', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'DefinitionSamples', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha creación', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'DefinitionSamples', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'DefinitionSamples', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario/identificación (CHAR 20) que creó originalmente la definición de muestra. Auditoría de origen.', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'DefinitionSamples', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario que crea', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'DefinitionSamples', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'DefinitionSamples', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado activo/inactivo (BIT) de la definición de muestra. True=Activo (en uso), False=Inactivo (descontinuado). Controla disponibilidad en laboratorio.', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'DefinitionSamples', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado  de la definción de muestras    True: Activo   False: Inactivo   ', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'DefinitionSamples', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'DefinitionSamples', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o descripción textual (VARCHAR 200) de la definición de muestra. Ej: tipo de contenedor, tubo, anticoagulante, técnica de recolección o medio de transporte.', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'DefinitionSamples', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre o descripción  de la definción de muestras:  ', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'DefinitionSamples', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'DefinitionSamples', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación (INT) de la definición: 1=Espécimen/tipo de muestra, 2=Técnica de recolección, 3=Medio de recolección/contenedor. Categorización para laboratorio y procedimiento clínico.', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'DefinitionSamples', @level2type = N'COLUMN', @level2name = N'Type';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de la definción de muestras:    
1 = Espécimen  
2 = Técnica 
 3 = Medio de recolección', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'DefinitionSamples', @level2type = N'COLUMN', @level2name = N'Type';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'DefinitionSamples', @level2type = N'COLUMN', @level2name = N'Type';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código alfanumérico único (VARCHAR 4) de la definición de muestra. Identificador corto para órdenes de laboratorio, exámenes y procedimientos diagnósticos.', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'DefinitionSamples', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la definición de muestras  ', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'DefinitionSamples', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'DefinitionSamples', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único secuencial (INT IDENTITY). Clave primaria de la definición de muestra en el sistema.', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'DefinitionSamples', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'DefinitionSamples', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'DefinitionSamples', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de muestras clínicas definidas para la toma de parámetros clínicos (por ejemplo: sangre, orina, tejido). Registra los tipos de muestra disponibles, su estado de vigencia y la trazabilidad de creación y modificación.', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'DefinitionSamples';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'DefinitionSamples';
