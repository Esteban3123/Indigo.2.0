CREATE TABLE [dbo].[AGESPECIA] (
    [CODESPECI] CHAR (2)       NOT NULL,
    [DESESPECI] NVARCHAR (150) NOT NULL,
    [ESTESPECI] BIT            NOT NULL,
    CONSTRAINT [PK_AGESPECIA] PRIMARY KEY CLUSTERED ([CODESPECI] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado Especialidad Activo=1;Inactivo=0', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGESPECIA', @level2type = N'COLUMN', @level2name = N'ESTESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion de la especialidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGESPECIA', @level2type = N'COLUMN', @level2name = N'DESESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la especialidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGESPECIA', @level2type = N'COLUMN', @level2name = N'CODESPECI';

