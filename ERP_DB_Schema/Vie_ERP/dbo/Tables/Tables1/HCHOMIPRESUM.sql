CREATE TABLE [dbo].[HCHOMIPRESUM] (
    [ID]           INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODMIPRES]    VARCHAR (5)  NOT NULL,
    [DESCRIPCION2] VARCHAR (30) NOT NULL,
    [DESCRIPCION]  VARCHAR (70) NOT NULL,
    [CODINDIGO]    VARCHAR (20) NOT NULL,
    [ESTADO]       BIT          NOT NULL,
    CONSTRAINT [PK__HCHOMIPRESUM] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCHOMIPRESUM_INUNIMEDI] FOREIGN KEY ([CODINDIGO]) REFERENCES [dbo].[INUNIMEDI] ([CODUNIMED])
);


GO
ALTER TABLE [dbo].[HCHOMIPRESUM] NOCHECK CONSTRAINT [FK_HCHOMIPRESUM_INUNIMEDI];


GO
CREATE NONCLUSTERED INDEX [IX_HCHOMIPRESUM_CODINDIGO]
    ON [dbo].[HCHOMIPRESUM]([CODINDIGO] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado de registro en mipres', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESUM', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de unidad de medida en Indigo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESUM', @level2type = N'COLUMN', @level2name = N'CODINDIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripción de unidad de medida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESUM', @level2type = N'COLUMN', @level2name = N'DESCRIPCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripción de unidad de medida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESUM', @level2type = N'COLUMN', @level2name = N'DESCRIPCION2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de unidad de medida mipres', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESUM', @level2type = N'COLUMN', @level2name = N'CODMIPRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador  de la tabla ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESUM', @level2type = N'COLUMN', @level2name = N'ID';

