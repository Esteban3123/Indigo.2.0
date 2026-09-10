CREATE TABLE [Admissions].[GenderTypes] (
    [Id]               INT           IDENTITY (1, 1) NOT NULL,
    [Code]             VARCHAR (3)   NOT NULL,
    [Name]             VARCHAR (200) NOT NULL,
    [Status]           BIT           NOT NULL,
    [UserCreation]     CHAR (20)     NOT NULL,
    [DateCreation]     DATETIME      NOT NULL,
    [UserModification] CHAR (20)     NULL,
    [DateModification] DATETIME      NULL,
    [TypeGender]       VARCHAR (4)   NULL,
    CONSTRAINT [PK_GenderTypes_1] PRIMARY KEY CLUSTERED ([Id] ASC)
);




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la última modificación del registro de tipo de género (DATETIME, nullable si nunca fue editado)', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'GenderTypes', @level2type = N'COLUMN', @level2name = N'DateModification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha que la modificó el registro ', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'GenderTypes', @level2type = N'COLUMN', @level2name = N'DateModification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'GenderTypes', @level2type = N'COLUMN', @level2name = N'DateModification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario/cuenta que realizó la última modificación del registro de tipo de género (CHAR 20, nullable si no ha sido editado)', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'GenderTypes', @level2type = N'COLUMN', @level2name = N'UserModification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario quien la modificó el registro ', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'GenderTypes', @level2type = N'COLUMN', @level2name = N'UserModification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'GenderTypes', @level2type = N'COLUMN', @level2name = N'UserModification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de tipo de género en el sistema (DATETIME, auditoria)', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'GenderTypes', @level2type = N'COLUMN', @level2name = N'DateCreation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha la que creo el registro', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'GenderTypes', @level2type = N'COLUMN', @level2name = N'DateCreation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'GenderTypes', @level2type = N'COLUMN', @level2name = N'DateCreation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario/cuenta que creó el registro de tipo de género en el sistema (CHAR 20, auditoria obligatoria)', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'GenderTypes', @level2type = N'COLUMN', @level2name = N'UserCreation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario quien creo el registo', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'GenderTypes', @level2type = N'COLUMN', @level2name = N'UserCreation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'GenderTypes', @level2type = N'COLUMN', @level2name = N'UserCreation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado activo/inactivo del tipo de género: 1=Activo, 0=Inactivo (BIT, controla disponibilidad en admisiones)', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'GenderTypes', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del registro 1 ->Activo  0->Inactivo', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'GenderTypes', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'GenderTypes', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre descriptivo del tipo de género (VARCHAR 200, ej: Masculino, Femenino, Otro, No especificado)', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'GenderTypes', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del tipo de genero', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'GenderTypes', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'GenderTypes', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código abreviado del tipo de género (VARCHAR 3, ej: M, F, O, para referencia rápida e identificación única)', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'GenderTypes', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del tipo de genero', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'GenderTypes', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'GenderTypes', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el codigo de tipo de genero  = 01 Masculino, 02 Femenino, 03 Trasgenero, 04 Neutro, 05 No lo declara', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'GenderTypes', @level2type = N'COLUMN', @level2name = N'TypeGender';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de tipos de género utilizados en el módulo de admisiones. Registra las opciones de género (masculino, femenino, otro, etc.) disponibles para clasificar a los pacientes al momento del ingreso.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'GenderTypes';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'GenderTypes';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador numérico interno, clave única autogenerada para cada tipo de género.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'GenderTypes', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'GenderTypes', @level2type = N'COLUMN', @level2name = N'Id';
