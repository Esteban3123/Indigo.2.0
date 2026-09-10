CREATE TABLE [MixingStation].[LineClearanceCriteria] (
    [Id]                     INT           IDENTITY (1, 1) NOT NULL,
    [Code]                   VARCHAR (20)  NOT NULL,
    [Name]                   VARCHAR (300) NOT NULL,
    [State]                  BIT           NOT NULL,
    [ValidCriteria]          BIT           NOT NULL,
    [SelectOptionToValidate] BIT           NULL,
    [ProductionChemist]      BIT           NOT NULL,
    [ProductionAssistant]    BIT           NOT NULL,
    [Creationuser]           VARCHAR (20)  NOT NULL,
    [CreationDate]           DATETIME      NOT NULL,
    [ModificationUser]       VARCHAR (20)  NULL,
    [ModificationDate]       DATETIME      NULL,
    [TimeStamp]              ROWVERSION    NOT NULL,
    CONSTRAINT [PK_LineClearanceCriteria] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal automática (TIMESTAMP SQL Server) del instante exacto de creación, modificación o registro del criterio de validación de línea de producción. Genera auditoría de cambios.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'LineClearanceCriteria', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'LineClearanceCriteria', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'LineClearanceCriteria', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la última modificación (DATETIME) del criterio de validación. Nulo si nunca fue modificado tras su creación.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'LineClearanceCriteria', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'LineClearanceCriteria', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'LineClearanceCriteria', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Login o identificador del usuario que realizó la última modificación del criterio de validación. Rastreo de auditoría (VARCHAR 20).', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'LineClearanceCriteria', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de modificación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'LineClearanceCriteria', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'LineClearanceCriteria', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación (DATETIME) del criterio de validación de línea de producción. Marca inicial del registro.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'LineClearanceCriteria', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creación ', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'LineClearanceCriteria', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'LineClearanceCriteria', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Login o identificador del usuario que creó el criterio de validación (VARCHAR 20). Rastreo de auditoría inicial.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'LineClearanceCriteria', @level2type = N'COLUMN', @level2name = N'Creationuser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de creación ', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'LineClearanceCriteria', @level2type = N'COLUMN', @level2name = N'Creationuser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'LineClearanceCriteria', @level2type = N'COLUMN', @level2name = N'Creationuser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT): marca si el auxiliar/asistente de producción puede validar o aplicar este criterio de limpieza de línea.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'LineClearanceCriteria', @level2type = N'COLUMN', @level2name = N'ProductionAssistant';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Auxiliar de produccion', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'LineClearanceCriteria', @level2type = N'COLUMN', @level2name = N'ProductionAssistant';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'LineClearanceCriteria', @level2type = N'COLUMN', @level2name = N'ProductionAssistant';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT): marca si el químico o profesional de producción puede validar o aplicar este criterio de limpieza de línea.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'LineClearanceCriteria', @level2type = N'COLUMN', @level2name = N'ProductionChemist';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Quimico de produccion', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'LineClearanceCriteria', @level2type = N'COLUMN', @level2name = N'ProductionChemist';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'LineClearanceCriteria', @level2type = N'COLUMN', @level2name = N'ProductionChemist';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT, nullable): marca si este criterio requiere seleccionar una opción específica para proceder con la validación de limpieza.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'LineClearanceCriteria', @level2type = N'COLUMN', @level2name = N'SelectOptionToValidate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Selecciona la opcion a validar', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'LineClearanceCriteria', @level2type = N'COLUMN', @level2name = N'SelectOptionToValidate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'LineClearanceCriteria', @level2type = N'COLUMN', @level2name = N'SelectOptionToValidate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT): marca si el criterio es actualmente válido y aplicable para la validación de limpieza de línea de producción.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'LineClearanceCriteria', @level2type = N'COLUMN', @level2name = N'ValidCriteria';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valida criterio o no', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'LineClearanceCriteria', @level2type = N'COLUMN', @level2name = N'ValidCriteria';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'LineClearanceCriteria', @level2type = N'COLUMN', @level2name = N'ValidCriteria';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT): estado del criterio (1=activo, 0=inactivo). Habilita o deshabilita el criterio de validación sin eliminarlo.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'LineClearanceCriteria', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Estado', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'LineClearanceCriteria', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'LineClearanceCriteria', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre descriptivo (VARCHAR 300) del criterio de validación de línea de producción. Ej: ''''Limpieza visual'''', ''''Residuos químicos aceptables''''.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'LineClearanceCriteria', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Nombre', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'LineClearanceCriteria', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'LineClearanceCriteria', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único (VARCHAR 20) del criterio de validación de línea. Identificador funcional para búsquedas y referencias. Ej: ''''LIMPVIS01'''', ''''RESID02''''.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'LineClearanceCriteria', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Codigo', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'LineClearanceCriteria', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'LineClearanceCriteria', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincremental (INT IDENTITY) de la tabla LineClearanceCriteria. Clave primaria, generado automáticamente.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'LineClearanceCriteria', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'LineClearanceCriteria', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'LineClearanceCriteria', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Criterios de limpieza y despeje de línea para la estación de mezcla. Registra cada criterio que debe verificarse antes de iniciar o finalizar una producción, indicando quién debe validarlo (químico de producción o asistente) y si el criterio requiere seleccionar una opción para su validación.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'LineClearanceCriteria';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'LineClearanceCriteria';
