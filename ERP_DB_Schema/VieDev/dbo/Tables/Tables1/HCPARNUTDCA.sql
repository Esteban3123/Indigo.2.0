CREATE TABLE [dbo].[HCPARNUTDCA] (
    [ID]          INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [HCPARNUTCId] INT       NULL,
    [CODCENATE]   CHAR (10) NULL,
    CONSTRAINT [PK__HCPARNUT__3214EC07290D27DE] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_IDNUTDCADCENATEN] FOREIGN KEY ([HCPARNUTCId]) REFERENCES [dbo].[HCPARNUTC] ([ID]),
    CONSTRAINT [FK_NUTDCAADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE])
);


GO
ALTER TABLE [dbo].[HCPARNUTDCA] NOCHECK CONSTRAINT [FK_IDNUTDCADCENATEN];


GO
ALTER TABLE [dbo].[HCPARNUTDCA] NOCHECK CONSTRAINT [FK_NUTDCAADCENATEN];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del centro de atencion donde se esta parametrizando la nutricion parenteral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTDCA', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de la cabecera de nutricion parenteral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTDCA', @level2type = N'COLUMN', @level2name = N'HCPARNUTCId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTDCA', @level2type = N'COLUMN', @level2name = N'ID';

