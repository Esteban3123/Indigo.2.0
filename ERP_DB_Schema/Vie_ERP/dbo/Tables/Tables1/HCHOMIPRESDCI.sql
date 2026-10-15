CREATE TABLE [dbo].[HCHOMIPRESDCI] (
    [ID]          INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODMIPRES]   VARCHAR (6)    NOT NULL,
    [DESCRIPCION] VARCHAR (1500) NOT NULL,
    [CODINDIGO]   CHAR (20)      NOT NULL,
    [ESTADO]      BIT            NOT NULL,
    CONSTRAINT [PK__HCHOMIPRESDCI] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCHOMIPRESDCI_IHLISTPRO] FOREIGN KEY ([CODINDIGO]) REFERENCES [dbo].[IHLISTPRO] ([CODPRODUC])
);


GO
ALTER TABLE [dbo].[HCHOMIPRESDCI] NOCHECK CONSTRAINT [FK_HCHOMIPRESDCI_IHLISTPRO];


GO
CREATE NONCLUSTERED INDEX [IX_HCHOMIPRESDCI_CODINDIGO]
    ON [dbo].[HCHOMIPRESDCI]([CODINDIGO] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado de registro en mipres', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESDCI', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de medicamento en Indigo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESDCI', @level2type = N'COLUMN', @level2name = N'CODINDIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripción DCI', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESDCI', @level2type = N'COLUMN', @level2name = N'DESCRIPCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código DCI mipres', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESDCI', @level2type = N'COLUMN', @level2name = N'CODMIPRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador de la tabla  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESDCI', @level2type = N'COLUMN', @level2name = N'ID';

