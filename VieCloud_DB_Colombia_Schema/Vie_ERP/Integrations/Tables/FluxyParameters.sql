CREATE TABLE [Integrations].[FluxyParameters] (
    [Id]               INT       IDENTITY (1, 1) NOT NULL,
    [Status]           BIT       CONSTRAINT [DF_FluxyParameters_Status] DEFAULT ((0)) NOT NULL,
    [CareCenterCode]   CHAR (10) NOT NULL,
    [CreationUser]     CHAR (20) NOT NULL,
    [CreationDate]     DATETIME  NOT NULL,
    [ModificationUser] CHAR (20) NULL,
    [ModificationDate] DATETIME  NULL,
    CONSTRAINT [PK_FluxyParameters] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de modificacion', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'FluxyParameters', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario de modificacion', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'FluxyParameters', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de creacion', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'FluxyParameters', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario de creacion', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'FluxyParameters', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Codigo del centro de atencion', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'FluxyParameters', @level2type = N'COLUMN', @level2name = N'CareCenterCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado: 
0 - Inactivo
1 - Activo', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'FluxyParameters', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Almacena los parámetros de configuración de la integración con el sistema Fluxy, asociados a centros de atención identificados por su código. Cada registro indica si la integración está activa o inactiva (0=Inactivo, 1=Activo) para un centro de atención específico. Incluye auditoría de creación y modificación con usuario y fecha.', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'TABLE', @level1name=N'FluxyParameters';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'TABLE', @level1name=N'FluxyParameters';
GO
