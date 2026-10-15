CREATE TABLE [dbo].[HCPARNUTDUF] (
    [ID]          INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [HCPARNUTCId] INT       NULL,
    [UFUCODIGO]   CHAR (10) NULL,
    CONSTRAINT [PK__HCPARNUT__3214EC074BA10146] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_IDNUTCINUNIFUNC] FOREIGN KEY ([HCPARNUTCId]) REFERENCES [dbo].[HCPARNUTC] ([ID]),
    CONSTRAINT [FK_NUTDUFINUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[HCPARNUTDUF] NOCHECK CONSTRAINT [FK_IDNUTCINUNIFUNC];


GO
ALTER TABLE [dbo].[HCPARNUTDUF] NOCHECK CONSTRAINT [FK_NUTDUFINUNIFUNC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la unidad funcional donde se esta parametrizando la nutricion parenteral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTDUF', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de la cabecera de nutricion parenteral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTDUF', @level2type = N'COLUMN', @level2name = N'HCPARNUTCId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTDUF', @level2type = N'COLUMN', @level2name = N'ID';

