CREATE TABLE [Admissions].[FuripsParameters] (
    [Id]                 INT       IDENTITY (1, 1) NOT NULL,
    [Code]               INT       NOT NULL,
    [IdentificationType] INT       NOT NULL,
    [UserCreation]       CHAR (20) NOT NULL,
    [DateCreation]       DATETIME  NOT NULL,
    [UserModification]   CHAR (20) NULL,
    [DateModification]   DATETIME  NULL,
    CONSTRAINT [PK_FuripsParameters] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'guarda la fecha de modificación ', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'FuripsParameters', @level2type = N'COLUMN', @level2name = N'DateModification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'guarda el usuario quien lo modifico', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'FuripsParameters', @level2type = N'COLUMN', @level2name = N'UserModification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'guarda la fecha de creación o de la parametrización de los diferentes tipos de identificación', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'FuripsParameters', @level2type = N'COLUMN', @level2name = N'DateCreation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'guarda el usuario de creacion', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'FuripsParameters', @level2type = N'COLUMN', @level2name = N'UserCreation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'guarda el tipo de identificación ala que pertenece:  1. tipos victima  2. tipos identificacion propietario 3. tipo identificacion conductor 4. tipo identificación médico', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'FuripsParameters', @level2type = N'COLUMN', @level2name = N'IdentificationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'guarda el codigo de cada tipo de identificación', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'FuripsParameters', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'FuripsParameters', @level2type = N'COLUMN', @level2name = N'Id';

