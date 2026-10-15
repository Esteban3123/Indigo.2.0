CREATE TABLE [dbo].[HCCONFTABPARAC] (
    [ID]                  INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [UFUCODIGO]           CHAR (10) NULL,
    [EXIGIRSIEMPRE]       BIT       NULL,
    [CODESPECI]           CHAR (3)  NULL,
    [FECHACREACION]       DATETIME  NOT NULL,
    [USUARIOCREACION]     CHAR (20) NOT NULL,
    [FECHAMODIFICACION]   DATETIME  NULL,
    [USUARIOMODIFICACION] CHAR (20) NULL,
    CONSTRAINT [PK_HCCONFTABPARAC] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCCONFTABPARAC_INESPECIA] FOREIGN KEY ([CODESPECI]) REFERENCES [dbo].[INESPECIA] ([CODESPECI]),
    CONSTRAINT [FK_HCCONFTABPARAC_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[HCCONFTABPARAC] NOCHECK CONSTRAINT [FK_HCCONFTABPARAC_INESPECIA];


GO
ALTER TABLE [dbo].[HCCONFTABPARAC] NOCHECK CONSTRAINT [FK_HCCONFTABPARAC_INUNIFUNC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario que modifica el registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONFTABPARAC', @level2type = N'COLUMN', @level2name = N'USUARIOMODIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de modificación del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONFTABPARAC', @level2type = N'COLUMN', @level2name = N'FECHAMODIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario que crea el registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONFTABPARAC', @level2type = N'COLUMN', @level2name = N'USUARIOCREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de creación del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONFTABPARAC', @level2type = N'COLUMN', @level2name = N'FECHACREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código Especialidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONFTABPARAC', @level2type = N'COLUMN', @level2name = N'CODESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(Aplica para parametrización por Unidad Funcional) Define si para la unidad funcional siempre se exige esta parametrización -- 1 = SI , 0 = N0', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONFTABPARAC', @level2type = N'COLUMN', @level2name = N'EXIGIRSIEMPRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONFTABPARAC', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumérico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONFTABPARAC', @level2type = N'COLUMN', @level2name = N'ID';

