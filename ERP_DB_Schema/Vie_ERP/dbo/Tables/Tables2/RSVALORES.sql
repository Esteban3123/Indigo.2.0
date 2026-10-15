CREATE TABLE [dbo].[RSVALORES] (
    [ID]           INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCHISPACA]  INT            NOT NULL,
    [IDGRUPO]      INT            NOT NULL,
    [IDRSVARIABLE] INT            NOT NULL,
    [VALOR]        VARCHAR (5000) NOT NULL,
    [IDITEMLISTA]  INT            NULL,
    CONSTRAINT [PK_RSVALORES] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_RSVALORES_HCHISPACA] FOREIGN KEY ([IDHCHISPACA]) REFERENCES [dbo].[HCHISPACA] ([ID]),
    CONSTRAINT [FK_RSVALORES_RSGRUPO] FOREIGN KEY ([IDGRUPO]) REFERENCES [dbo].[RSGRUPO] ([ID]),
    CONSTRAINT [FK_RSVALORES_RSVALORES] FOREIGN KEY ([ID]) REFERENCES [dbo].[RSVALORES] ([ID]),
    CONSTRAINT [FK_RSVALORES_RSVARIABLES] FOREIGN KEY ([IDRSVARIABLE]) REFERENCES [dbo].[RSVARIABLES] ([ID]),
    CONSTRAINT [FK_RSVALORES_RSVARIABLESL] FOREIGN KEY ([IDITEMLISTA]) REFERENCES [dbo].[RSVARIABLESL] ([ID])
);


GO
ALTER TABLE [dbo].[RSVALORES] NOCHECK CONSTRAINT [FK_RSVALORES_HCHISPACA];


GO
ALTER TABLE [dbo].[RSVALORES] NOCHECK CONSTRAINT [FK_RSVALORES_RSGRUPO];


GO
ALTER TABLE [dbo].[RSVALORES] NOCHECK CONSTRAINT [FK_RSVALORES_RSVALORES];


GO
ALTER TABLE [dbo].[RSVALORES] NOCHECK CONSTRAINT [FK_RSVALORES_RSVARIABLES];


GO
ALTER TABLE [dbo].[RSVALORES] NOCHECK CONSTRAINT [FK_RSVALORES_RSVARIABLESL];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de la lista en caso de que la variable sea de tipo combo.  se relaciona con la tabla RSVARIABLESL', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVALORES', @level2type = N'COLUMN', @level2name = N'IDITEMLISTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el valor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVALORES', @level2type = N'COLUMN', @level2name = N'VALOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de la variable que se esta almacenando   Tabla de relacion RSVARIABLES', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVALORES', @level2type = N'COLUMN', @level2name = N'IDRSVARIABLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación con la tabla RSGRUPO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVALORES', @level2type = N'COLUMN', @level2name = N'IDGRUPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de la relacion con el tablero de la Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVALORES', @level2type = N'COLUMN', @level2name = N'IDHCHISPACA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVALORES', @level2type = N'COLUMN', @level2name = N'ID';

