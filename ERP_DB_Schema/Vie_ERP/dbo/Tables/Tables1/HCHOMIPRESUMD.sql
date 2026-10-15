CREATE TABLE [dbo].[HCHOMIPRESUMD] (
    [ID]            INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODMIPRES]     VARCHAR (5)  NOT NULL,
    [DESCRIPCION]   VARCHAR (70) NOT NULL,
    [UNIMEDPRINACT] VARCHAR (30) NOT NULL,
    [CODINDIGO]     VARCHAR (20) NOT NULL,
    [ESTADO]        BIT          NOT NULL,
    CONSTRAINT [PK__HCHOMIPRESUMD] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCHOMIPRESUMD_INUNIMEDI] FOREIGN KEY ([CODINDIGO]) REFERENCES [dbo].[INUNIMEDI] ([CODUNIMED])
);


GO
ALTER TABLE [dbo].[HCHOMIPRESUMD] NOCHECK CONSTRAINT [FK_HCHOMIPRESUMD_INUNIMEDI];


GO
CREATE NONCLUSTERED INDEX [IX_HCHOMIPRESUMD_CODINDIGO]
    ON [dbo].[HCHOMIPRESUMD]([CODINDIGO] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado de registro en mipres', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESUMD', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de unidad de medida en Indigo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESUMD', @level2type = N'COLUMN', @level2name = N'CODINDIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad de medida del pricipio activo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESUMD', @level2type = N'COLUMN', @level2name = N'UNIMEDPRINACT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripción de unidad de medida dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESUMD', @level2type = N'COLUMN', @level2name = N'DESCRIPCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de unidad de medida dosis mipres', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESUMD', @level2type = N'COLUMN', @level2name = N'CODMIPRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador de  la tabla ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESUMD', @level2type = N'COLUMN', @level2name = N'ID';

