CREATE TABLE [Security].[Form] (
    [Id]                    INT           NOT NULL,
    [Name]                  VARCHAR (100) NOT NULL,
    [PrintEvents]           VARCHAR (15)  NULL,
    [HasSequence]           BIT           NOT NULL,
    [IsNativeForm]          BIT           NOT NULL,
    [HasForm]               BIT           NOT NULL,
    [ClassName]             VARCHAR (60)  NULL,
    [AssemblyName]          VARCHAR (60)  NULL,
    [HandlesMassiveConfirm] BIT           NOT NULL,
    [SequenceModule]        VARCHAR (50)  NULL,
    [State]                 BIT           NOT NULL,
    CONSTRAINT [PK_Form] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla de Formulario', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Form';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Llave Principal', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Form', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de Formulario', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Form', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera Secuencia de Codigo Automatica', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Form', @level2type = N'COLUMN', @level2name = N'HasSequence';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Es formulario Nativo', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Form', @level2type = N'COLUMN', @level2name = N'IsNativeForm';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tuvo Formulario', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Form', @level2type = N'COLUMN', @level2name = N'HasForm';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del formulario en dll', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Form', @level2type = N'COLUMN', @level2name = N'ClassName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de dll a buscar formulario', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Form', @level2type = N'COLUMN', @level2name = N'AssemblyName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Maneja Confirmacion Masiva', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Form', @level2type = N'COLUMN', @level2name = N'HandlesMassiveConfirm';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del Formulario', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Form', @level2type = N'COLUMN', @level2name = N'State';

