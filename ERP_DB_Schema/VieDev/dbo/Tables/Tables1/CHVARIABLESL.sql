CREATE TABLE [dbo].[CHVARIABLESL] (
    [ID]           INT           IDENTITY (1, 1) NOT NULL,
    [IDCHVARIABLE] INT           NOT NULL,
    [CODIGO]       VARCHAR (50)  NOT NULL,
    [NOMBRE]       VARCHAR (100) NOT NULL,
    [DESCRIPCION]  VARCHAR (500) NULL,
    [ESTADO]       BIT           NOT NULL,
    CONSTRAINT [PK_CHVARIABLESL] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_CHVARIABLESL_CHVARIABLES] FOREIGN KEY ([IDCHVARIABLE]) REFERENCES [dbo].[CHVARIABLES] ([ID])
);


GO
ALTER TABLE [dbo].[CHVARIABLESL] NOCHECK CONSTRAINT [FK_CHVARIABLESL_CHVARIABLES];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado Activo o Inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVARIABLESL', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Comentario del campo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVARIABLESL', @level2type = N'COLUMN', @level2name = N'DESCRIPCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre  Item de la lista', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVARIABLESL', @level2type = N'COLUMN', @level2name = N'NOMBRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Item de la lista', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVARIABLESL', @level2type = N'COLUMN', @level2name = N'CODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la Variable padre', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVARIABLESL', @level2type = N'COLUMN', @level2name = N'IDCHVARIABLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVARIABLESL', @level2type = N'COLUMN', @level2name = N'ID';

