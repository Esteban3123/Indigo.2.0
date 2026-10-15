CREATE TABLE [dbo].[HCANALIESP] (
    [ID]              INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ANALITOID]       INT       NOT NULL,
    [CODESPECI]       CHAR (3)  NOT NULL,
    [USUARIOCREACION] CHAR (20) NULL,
    [FECHACREACION]   DATETIME  NULL,
    CONSTRAINT [PK_HCANALIESP] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCANALIESP_HCANALIESP] FOREIGN KEY ([CODESPECI]) REFERENCES [dbo].[INESPECIA] ([CODESPECI]),
    CONSTRAINT [FK_HCANALIESP_HCANALITO] FOREIGN KEY ([ANALITOID]) REFERENCES [dbo].[HCANALITO] ([ID]),
    CONSTRAINT [FK_HCANALIESP_SEGusuaru_1] FOREIGN KEY ([USUARIOCREACION]) REFERENCES [dbo].[SEGusuaru] ([CODUSUARI])
);


GO
ALTER TABLE [dbo].[HCANALIESP] NOCHECK CONSTRAINT [FK_HCANALIESP_HCANALIESP];


GO
ALTER TABLE [dbo].[HCANALIESP] NOCHECK CONSTRAINT [FK_HCANALIESP_HCANALITO];


GO
ALTER TABLE [dbo].[HCANALIESP] NOCHECK CONSTRAINT [FK_HCANALIESP_SEGusuaru_1];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Creación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANALIESP', @level2type = N'COLUMN', @level2name = N'FECHACREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANALIESP', @level2type = N'COLUMN', @level2name = N'USUARIOCREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Especialidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANALIESP', @level2type = N'COLUMN', @level2name = N'CODESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Analito tanla HCANALITO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANALIESP', @level2type = N'COLUMN', @level2name = N'ANALITOID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANALIESP', @level2type = N'COLUMN', @level2name = N'ID';

