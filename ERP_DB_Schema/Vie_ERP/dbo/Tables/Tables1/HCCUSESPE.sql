CREATE TABLE [dbo].[HCCUSESPE] (
    [CODCONCEC] INT      NOT NULL,
    [CODESPECI] CHAR (3) NOT NULL,
    CONSTRAINT [PK_HCCUSESPE] PRIMARY KEY CLUSTERED ([CODCONCEC] ASC, [CODESPECI] ASC),
    CONSTRAINT [FK_HCCUSESPE_HCCUSTOMI] FOREIGN KEY ([CODCONCEC]) REFERENCES [dbo].[HCCUSTOMI] ([AUTO]),
    CONSTRAINT [FK_HCCUSESPE_INESPECIA] FOREIGN KEY ([CODESPECI]) REFERENCES [dbo].[INESPECIA] ([CODESPECI])
);


GO
ALTER TABLE [dbo].[HCCUSESPE] NOCHECK CONSTRAINT [FK_HCCUSESPE_HCCUSTOMI];


GO
ALTER TABLE [dbo].[HCCUSESPE] NOCHECK CONSTRAINT [FK_HCCUSESPE_INESPECIA];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la especialidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUSESPE', @level2type = N'COLUMN', @level2name = N'CODESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUSESPE', @level2type = N'COLUMN', @level2name = N'CODCONCEC';

