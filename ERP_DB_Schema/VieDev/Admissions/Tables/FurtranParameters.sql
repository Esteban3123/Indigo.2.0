CREATE TABLE [Admissions].[FurtranParameters] (
    [Id]                 INT       IDENTITY (1, 1) NOT NULL,
    [Code]               INT       NOT NULL,
    [IdentificationType] INT       NOT NULL,
    [UserCreation]       CHAR (20) NOT NULL,
    [DateCreation]       DATETIME  NOT NULL,
    [UserModification]   CHAR (20) NULL,
    [DateModification]   DATETIME  NULL,
    CONSTRAINT [PK_FurtranpParameters] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Guarda la fecha de modificación o de la parametrización de los diferentes tipos de identificación', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'FurtranParameters', @level2type = N'COLUMN', @level2name = N'DateModification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Guarda el código del usuario de quien lo modificó', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'FurtranParameters', @level2type = N'COLUMN', @level2name = N'UserModification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Guarda la fecha de creación o de la parametrización de los diferentes tipos de identificación', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'FurtranParameters', @level2type = N'COLUMN', @level2name = N'DateCreation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Guarda el código del usuario de quien lo creó', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'FurtranParameters', @level2type = N'COLUMN', @level2name = N'UserCreation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Guarda el tipo de identificación ala que pertenece:   1. Tipo de identificacion víctima  2. Tipo de identificación conductor', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'FurtranParameters', @level2type = N'COLUMN', @level2name = N'IdentificationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Guarda el codigo de cada tipo de identificación', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'FurtranParameters', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Guarda el numero de identificación único del registro presente', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'FurtranParameters', @level2type = N'COLUMN', @level2name = N'Id';

