CREATE TABLE [dbo].[HCTURNREC] (
    [CONSECUTI]       INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODESPECI]       CHAR (3)  NOT NULL,
    [COLORESPE]       INT       NOT NULL,
    [CODCENATE]       CHAR (10) NOT NULL,
    [USUARIOCREACION] CHAR (20) NULL,
    [FECHACREACION]   DATETIME  NULL,
    CONSTRAINT [PK_Resources] PRIMARY KEY CLUSTERED ([CONSECUTI] ASC),
    CONSTRAINT [FK_HCTURNREC_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCTURNREC_INESPECIA] FOREIGN KEY ([CODESPECI]) REFERENCES [dbo].[INESPECIA] ([CODESPECI]),
    CONSTRAINT [FK_HCTURNREC_SEGusuaru_1] FOREIGN KEY ([USUARIOCREACION]) REFERENCES [dbo].[SEGusuaru] ([CODUSUARI])
);


GO
ALTER TABLE [dbo].[HCTURNREC] NOCHECK CONSTRAINT [FK_HCTURNREC_ADCENATEN];


GO
ALTER TABLE [dbo].[HCTURNREC] NOCHECK CONSTRAINT [FK_HCTURNREC_INESPECIA];


GO
ALTER TABLE [dbo].[HCTURNREC] NOCHECK CONSTRAINT [FK_HCTURNREC_SEGusuaru_1];


GO
CREATE NONCLUSTERED INDEX [IX_HCTURNREC__CODCENATE]
    ON [dbo].[HCTURNREC]([CODCENATE] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Creación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTURNREC', @level2type = N'COLUMN', @level2name = N'FECHACREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTURNREC', @level2type = N'COLUMN', @level2name = N'USUARIOCREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTURNREC', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Color de la Especialidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTURNREC', @level2type = N'COLUMN', @level2name = N'COLORESPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Especialidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTURNREC', @level2type = N'COLUMN', @level2name = N'CODESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo Interno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTURNREC', @level2type = N'COLUMN', @level2name = N'CONSECUTI';

