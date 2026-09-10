CREATE TABLE [HumanTalent].[PvTypeSelectionProcessesDetail] (
    [Id]                       INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [TypeSelectionProcessesId] INT             NOT NULL,
    [SelectionActivitiesId]    INT             NOT NULL,
    [Head]                     INT             NOT NULL,
    [Obligatory]               TINYINT         NOT NULL,
    [CreationUser]             VARCHAR (20)    NOT NULL,
    [CreationDate]             DATETIME        NOT NULL,
    [ModificationUser]         VARCHAR (20)    NULL,
    [ModificationDate]         DATETIME        NULL,
    [Weight]                   DECIMAL (18, 2) CONSTRAINT [DF__PvTypeSel__Weigh__77A62AB8] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_PvTypeSelectionProcessesDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PvTypeSelectionProcessesDetail_SelectionActivities] FOREIGN KEY ([SelectionActivitiesId]) REFERENCES [HumanTalent].[SelectionActivities] ([Id]),
    CONSTRAINT [FK_PvTypeSelectionProcessesDetail_TypeSelectionProcesses] FOREIGN KEY ([TypeSelectionProcessesId]) REFERENCES [HumanTalent].[TypeSelectionProcesses] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Peso o ponderación (DECIMAL 18,2) asignado a la actividad de selección dentro del proceso; valor numérico que determina la importancia relativa de la actividad en la evaluación del proceso de selección de personal.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PvTypeSelectionProcessesDetail', @level2type = N'COLUMN', @level2name = N'Weight';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Peso', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PvTypeSelectionProcessesDetail', @level2type = N'COLUMN', @level2name = N'Weight';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PvTypeSelectionProcessesDetail', @level2type = N'COLUMN', @level2name = N'Weight';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de la última modificación del registro de detalle del proceso de selección; auditoría temporal del cambio realizado.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PvTypeSelectionProcessesDetail', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Feacha de modificación del registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PvTypeSelectionProcessesDetail', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PvTypeSelectionProcessesDetail', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del usuario (VARCHAR 20) que realizó la última modificación del registro; auditoría de cambios en el detalle del proceso de selección.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PvTypeSelectionProcessesDetail', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del usuario que modifica el registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PvTypeSelectionProcessesDetail', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PvTypeSelectionProcessesDetail', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de creación del registro de detalle del proceso de selección; marca temporal de origen del registro.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PvTypeSelectionProcessesDetail', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creación del registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PvTypeSelectionProcessesDetail', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PvTypeSelectionProcessesDetail', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del usuario (VARCHAR 20) que creó el registro de detalle; auditoría de origen del registro en el proceso de selección.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PvTypeSelectionProcessesDetail', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del usuario que crea el registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PvTypeSelectionProcessesDetail', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PvTypeSelectionProcessesDetail', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (TINYINT: 0=No obligatorio, 1=Obligatorio) que determina si la ejecución de la actividad de selección es requerida u opcional dentro del proceso creado; campo de validación binaria.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PvTypeSelectionProcessesDetail', @level2type = N'COLUMN', @level2name = N'Obligatory';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo de selección única, opciones por defecto SI/NO, para determinar si es obligatorio que se ejecuten las actividades seleccionadas dentro del proceso creado o no: 0- no obligatorio, 1- Obligatorio', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PvTypeSelectionProcessesDetail', @level2type = N'COLUMN', @level2name = N'Obligatory';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PvTypeSelectionProcessesDetail', @level2type = N'COLUMN', @level2name = N'Obligatory';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de orden o secuencia (INT) que define la posición y el orden de ejecución de la actividad de selección dentro del proceso; jerarquía de prioridad de las actividades.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PvTypeSelectionProcessesDetail', @level2type = N'COLUMN', @level2name = N'Head';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Para determinar el orden en que se deben ejecutar las actividades seleccionadas dentro del proceso creado.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PvTypeSelectionProcessesDetail', @level2type = N'COLUMN', @level2name = N'Head';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PvTypeSelectionProcessesDetail', @level2type = N'COLUMN', @level2name = N'Head';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) de la actividad de selección asociada; referencia a la tabla SelectionActivities en HumanTalent; vínculo con evaluaciones, pruebas o etapas del proceso de selección de personal.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PvTypeSelectionProcessesDetail', @level2type = N'COLUMN', @level2name = N'SelectionActivitiesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de las actividades de seleccion ()', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PvTypeSelectionProcessesDetail', @level2type = N'COLUMN', @level2name = N'SelectionActivitiesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PvTypeSelectionProcessesDetail', @level2type = N'COLUMN', @level2name = N'SelectionActivitiesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) del tipo de proceso de selección padre o cabecera; referencia a TypeSelectionProcesses en HumanTalent; agrupa múltiples actividades dentro de un proceso de selección.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PvTypeSelectionProcessesDetail', @level2type = N'COLUMN', @level2name = N'TypeSelectionProcessesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de cabecera', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PvTypeSelectionProcessesDetail', @level2type = N'COLUMN', @level2name = N'TypeSelectionProcessesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PvTypeSelectionProcessesDetail', @level2type = N'COLUMN', @level2name = N'TypeSelectionProcessesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) del detalle del proceso de selección; clave primaria que identifica unívocamente cada registro de asociación entre actividad y proceso.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PvTypeSelectionProcessesDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificación de la tabla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PvTypeSelectionProcessesDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PvTypeSelectionProcessesDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de los tipos de procesos de selección de talento humano: relaciona cada tipo de proceso con las actividades de selección que lo componen, indicando si son obligatorias, el peso o ponderación de cada actividad y el responsable (jefe) asignado. Usado en la gestión de reclutamiento y selección de personal.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PvTypeSelectionProcessesDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PvTypeSelectionProcessesDetail';
