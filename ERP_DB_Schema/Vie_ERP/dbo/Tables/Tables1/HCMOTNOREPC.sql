CREATE TABLE [dbo].[HCMOTNOREPC] (
    [ID]          INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODIGO]      VARCHAR (3)    NOT NULL,
    [DESCRIPCION] VARCHAR (1000) NOT NULL,
    [USUCREACION] CHAR (20)      NOT NULL,
    [FECCREACION] DATETIME       NOT NULL,
    [USUMODIFICA] CHAR (20)      NULL,
    [FECMODIFICA] DATETIME       NULL,
    [ESTADO]      BIT            NOT NULL,
    CONSTRAINT [PK_HCMOTNOREPC] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCMOTNOREPC_HCMOTNOREPC] FOREIGN KEY ([ID]) REFERENCES [dbo].[HCMOTNOREPC] ([ID])
);


GO
ALTER TABLE [dbo].[HCMOTNOREPC] NOCHECK CONSTRAINT [FK_HCMOTNOREPC_HCMOTNOREPC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOTNOREPC', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de modificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOTNOREPC', @level2type = N'COLUMN', @level2name = N'FECMODIFICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario que modifica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOTNOREPC', @level2type = N'COLUMN', @level2name = N'USUMODIFICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOTNOREPC', @level2type = N'COLUMN', @level2name = N'FECCREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario de creación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOTNOREPC', @level2type = N'COLUMN', @level2name = N'USUCREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'descripción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOTNOREPC', @level2type = N'COLUMN', @level2name = N'DESCRIPCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOTNOREPC', @level2type = N'COLUMN', @level2name = N'CODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOTNOREPC', @level2type = N'COLUMN', @level2name = N'ID';

