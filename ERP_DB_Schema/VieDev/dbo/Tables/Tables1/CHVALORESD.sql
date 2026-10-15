CREATE TABLE [dbo].[CHVALORESD] (
    [ID]           INT            IDENTITY (1, 1) NOT NULL,
    [IDCHVALORESC] INT            NOT NULL,
    [IDGRUPO]      INT            NOT NULL,
    [IDCHVARIABLE] INT            NOT NULL,
    [VALOR]        VARCHAR (5000) NOT NULL,
    [IDITEMLISTA]  INT            NULL,
    [LOCATIONY]    INT            NULL,
    CONSTRAINT [PK_CHVALORESD] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_CHVALORESD_CHGRUPO] FOREIGN KEY ([IDGRUPO]) REFERENCES [dbo].[CHGRUPO] ([ID]),
    CONSTRAINT [FK_CHVALORESD_CHVALORESC] FOREIGN KEY ([IDCHVALORESC]) REFERENCES [dbo].[CHVALORESC] ([ID]),
    CONSTRAINT [FK_CHVALORESD_CHVARIABLES] FOREIGN KEY ([IDCHVARIABLE]) REFERENCES [dbo].[CHVARIABLES] ([ID]),
    CONSTRAINT [FK_CHVALORESD_CHVARIABLESL] FOREIGN KEY ([IDITEMLISTA]) REFERENCES [dbo].[CHVARIABLESL] ([ID])
);


GO
ALTER TABLE [dbo].[CHVALORESD] NOCHECK CONSTRAINT [FK_CHVALORESD_CHGRUPO];


GO
ALTER TABLE [dbo].[CHVALORESD] NOCHECK CONSTRAINT [FK_CHVALORESD_CHVALORESC];


GO
ALTER TABLE [dbo].[CHVALORESD] NOCHECK CONSTRAINT [FK_CHVALORESD_CHVARIABLES];


GO
ALTER TABLE [dbo].[CHVALORESD] NOCHECK CONSTRAINT [FK_CHVALORESD_CHVARIABLESL];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda locación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVALORESD', @level2type = N'COLUMN', @level2name = N'LOCATIONY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de la lista en caso de que la variable sea de tipo combo.  se relaciona con la tabla CHVARIABLESL', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVALORESD', @level2type = N'COLUMN', @level2name = N'IDITEMLISTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el valor de la variable dependencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVALORESD', @level2type = N'COLUMN', @level2name = N'VALOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de la variable que se esta almacenando   Tabla de relacion CHVARIABLES', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVALORESD', @level2type = N'COLUMN', @level2name = N'IDCHVARIABLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID del grupo que se esta almacenando   Tabla de relacion CHVARIABLES', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVALORESD', @level2type = N'COLUMN', @level2name = N'IDGRUPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de la cabecera que guarda la informacion de la lista y paciente  Tabla de relacion CHVALORESCC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVALORESD', @level2type = N'COLUMN', @level2name = N'IDCHVALORESC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVALORESD', @level2type = N'COLUMN', @level2name = N'ID';

