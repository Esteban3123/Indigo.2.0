CREATE TABLE [Admissions].[RisksType] (
    [Id]               INT           IDENTITY (1, 1) NOT NULL,
    [Code]             VARCHAR (3)   NOT NULL,
    [Name]             VARCHAR (100) NOT NULL,
    [Status]           BIT           NOT NULL,
    [UserCreation]     CHAR (20)     NOT NULL,
    [DateCreation]     DATETIME      NOT NULL,
    [UserModification] CHAR (20)     NULL,
    [DateModification] DATETIME      NULL,
    CONSTRAINT [PK_RisksType] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'fecha de modificacion del tipo de riesgo desde el maestro tipo de riesgo', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'RisksType', @level2type = N'COLUMN', @level2name = N'DateModification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que modificó el tipo de riesgo desde el maestro tipo de riesgo', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'RisksType', @level2type = N'COLUMN', @level2name = N'UserModification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'fecha de creacion del tipo de riesgo creado desde el maestro tipo de riesgo', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'RisksType', @level2type = N'COLUMN', @level2name = N'DateCreation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creo el tipo de riesgo creado desde el maestro tipo de riesgo', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'RisksType', @level2type = N'COLUMN', @level2name = N'UserCreation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'estado del tipo de riesgo creado desde el maestro tipo de riesgo', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'RisksType', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del tipo de tiesgo creado desde el maestro tipo de riesgo', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'RisksType', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Codigo del tipo de riesgo creado desde el maestro tipo de riesgo', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'RisksType', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla maestra del módulo de Admisiones que cataloga los tipos de riesgo disponibles para su clasificación. Cada registro almacena un código corto (hasta 3 caracteres) y un nombre descriptivo, con un indicador de estado activo/inactivo. Registra auditoría de creación y modificación con usuario y fecha.', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'TABLE', @level1name=N'RisksType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'TABLE', @level1name=N'RisksType';
GO
