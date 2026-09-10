CREATE TABLE [Lactation].[ParametersConfiguration] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [Code]         VARCHAR (3)   NOT NULL,
    [Description]  VARCHAR (100) NULL,
    [Status]       BIT           NOT NULL,
    [CreatedBy]    CHAR (20)     NULL,
    [CreatedDate]  DATETIME      DEFAULT (getdate()) NULL,
    [ModifiedBy]   CHAR (20)     NULL,
    [ModifiedDate] DATETIME      NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de última modificación del registro de configuración del lactario. Timestamp (DATETIME) que indica cuándo se actualizó por última vez el parámetro o su estado.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersConfiguration', @level2type = N'COLUMN', @level2name = N'ModifiedDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de última modificación', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersConfiguration', @level2type = N'COLUMN', @level2name = N'ModifiedDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersConfiguration', @level2type = N'COLUMN', @level2name = N'ModifiedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que modificó por última vez la configuración del lactario. Identificador o nombre del usuario (CHAR 20, potencial PII) que realizó el cambio más reciente al parámetro.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersConfiguration', @level2type = N'COLUMN', @level2name = N'ModifiedBy';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario que modificó por última vez', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersConfiguration', @level2type = N'COLUMN', @level2name = N'ModifiedBy';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersConfiguration', @level2type = N'COLUMN', @level2name = N'ModifiedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de configuración del lactario. Timestamp (DATETIME) que marca cuándo se ingresó por primera vez el parámetro en la base de datos.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersConfiguration', @level2type = N'COLUMN', @level2name = N'CreatedDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creación del registro', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersConfiguration', @level2type = N'COLUMN', @level2name = N'CreatedDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersConfiguration', @level2type = N'COLUMN', @level2name = N'CreatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó el registro de configuración del lactario. Identificador o nombre de usuario (CHAR 20, potencial PII) que registró el parámetro inicialmente en el sistema.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersConfiguration', @level2type = N'COLUMN', @level2name = N'CreatedBy';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario que creó el registro', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersConfiguration', @level2type = N'COLUMN', @level2name = N'CreatedBy';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersConfiguration', @level2type = N'COLUMN', @level2name = N'CreatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado activo o inactivo del lactario o parámetro de configuración. Bit booleano (1=activo, 0=inactivo) que controla si la configuración está habilitada u operativa.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersConfiguration', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado activo o inactivo del lactario', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersConfiguration', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersConfiguration', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción del lactario o parámetro de configuración. Texto de hasta 100 caracteres que detalla el propósito o características del ajuste configurado en el servicio de lactancia.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersConfiguration', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripción del lactario', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersConfiguration', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersConfiguration', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único de configuración del lactario. Identificador alfanumérico de 3 caracteres (VARCHAR 3) que distingue cada parámetro de configuración. Clave principal de consulta.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersConfiguration', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código único de configuración', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersConfiguration', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersConfiguration', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla principal de configuración de lactario. Almacena parámetros y ajustes de funcionamiento del servicio de lactancia materna, incluyendo código, descripción y estado de cada configuración.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersConfiguration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tabla principal de configuración de lactario', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersConfiguration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersConfiguration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único interno del parámetro de configuración, generado automáticamente por el sistema.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersConfiguration', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersConfiguration', @level2type = N'COLUMN', @level2name = N'Id';
