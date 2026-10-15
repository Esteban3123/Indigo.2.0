CREATE TABLE [dbo].[HCPARBODEGAS] (
    [ID]        INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODCENATE] CHAR (10)    NOT NULL,
    [UFUCODIGO] CHAR (10)    NOT NULL,
    [CODBODEGA] VARCHAR (20) NOT NULL,
    CONSTRAINT [PK_HCPARBODEGAS] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCPARBODEGAS_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCPARBODEGAS_IHBODEGAS] FOREIGN KEY ([CODBODEGA]) REFERENCES [dbo].[IHBODEGAS] ([CODBODEGA]),
    CONSTRAINT [FK_HCPARBODEGAS_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[HCPARBODEGAS] NOCHECK CONSTRAINT [FK_HCPARBODEGAS_ADCENATEN];


GO
ALTER TABLE [dbo].[HCPARBODEGAS] NOCHECK CONSTRAINT [FK_HCPARBODEGAS_IHBODEGAS];


GO
ALTER TABLE [dbo].[HCPARBODEGAS] NOCHECK CONSTRAINT [FK_HCPARBODEGAS_INUNIFUNC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de Bodega ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARBODEGAS', @level2type = N'COLUMN', @level2name = N'CODBODEGA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo unidad funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARBODEGAS', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo centro atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARBODEGAS', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARBODEGAS', @level2type = N'COLUMN', @level2name = N'ID';

