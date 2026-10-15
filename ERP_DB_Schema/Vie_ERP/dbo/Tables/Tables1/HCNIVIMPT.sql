CREATE TABLE [dbo].[HCNIVIMPT] (
    [CODNIVIMP] CHAR (2)   NOT NULL,
    [DESNIVIMP] CHAR (100) NOT NULL,
    [COLNIVIMP] CHAR (50)  NOT NULL,
    CONSTRAINT [PK_HCNIVIMPT] PRIMARY KEY CLUSTERED ([CODNIVIMP] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Hexadecimal del Color', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNIVIMPT', @level2type = N'COLUMN', @level2name = N'COLNIVIMP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNIVIMPT', @level2type = N'COLUMN', @level2name = N'DESNIVIMP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Nivel de Importancia para las Notas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNIVIMPT', @level2type = N'COLUMN', @level2name = N'CODNIVIMP';

