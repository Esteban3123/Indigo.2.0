CREATE TABLE [dbo].[HCPAREXFISC] (
    [ID]            INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [UFUCODIGO]     CHAR (10) NULL,
    [EXIGIRSIEMPRE] BIT       NULL,
    [CODESPECI]     CHAR (3)  NULL,
    [TIPOEXFISICO]  TINYINT   NOT NULL,
    CONSTRAINT [PK_HCPAREXFISC] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCPAREXFISC_INESPECIA] FOREIGN KEY ([CODESPECI]) REFERENCES [dbo].[INESPECIA] ([CODESPECI]),
    CONSTRAINT [FK_HCPAREXFISC_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[HCPAREXFISC] NOCHECK CONSTRAINT [FK_HCPAREXFISC_INESPECIA];


GO
ALTER TABLE [dbo].[HCPAREXFISC] NOCHECK CONSTRAINT [FK_HCPAREXFISC_INUNIFUNC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de exámen fisico que se abre en esta parametrización', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAREXFISC', @level2type = N'COLUMN', @level2name = N'TIPOEXFISICO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código Especialidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAREXFISC', @level2type = N'COLUMN', @level2name = N'CODESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(Aplica para parametrización por Unidad Funcional) Define si para la unidad funcional siempre se exige esta parametrización', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAREXFISC', @level2type = N'COLUMN', @level2name = N'EXIGIRSIEMPRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAREXFISC', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumérico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAREXFISC', @level2type = N'COLUMN', @level2name = N'ID';

