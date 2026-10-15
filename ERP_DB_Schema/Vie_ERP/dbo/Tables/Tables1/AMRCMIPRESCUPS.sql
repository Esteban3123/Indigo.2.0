CREATE TABLE [dbo].[AMRCMIPRESCUPS] (
    [ID]            INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODCONCEC]     INT           NOT NULL,
    [CODSERIPS]     CHAR (20)     NOT NULL,
    [FECHA]         DATETIME      NOT NULL,
    [JUSTIFICACION] VARCHAR (600) NOT NULL,
    CONSTRAINT [PK_AMRCMIPRESCUPS] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_AMRCMIPRESCUPS_HCJUSTECA] FOREIGN KEY ([CODCONCEC]) REFERENCES [dbo].[HCJUSTECA] ([CODCONCEC])
);


GO
ALTER TABLE [dbo].[AMRCMIPRESCUPS] NOCHECK CONSTRAINT [FK_AMRCMIPRESCUPS_HCJUSTECA];


GO
CREATE NONCLUSTERED INDEX [IX_AMRCMIPRESCUPS_CODCONCEC]
    ON [dbo].[AMRCMIPRESCUPS]([CODCONCEC] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Justificacion de la habilitacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMRCMIPRESCUPS', @level2type = N'COLUMN', @level2name = N'JUSTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de creacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMRCMIPRESCUPS', @level2type = N'COLUMN', @level2name = N'FECHA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del servicio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMRCMIPRESCUPS', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'codigo habilitado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMRCMIPRESCUPS', @level2type = N'COLUMN', @level2name = N'CODCONCEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMRCMIPRESCUPS', @level2type = N'COLUMN', @level2name = N'ID';

