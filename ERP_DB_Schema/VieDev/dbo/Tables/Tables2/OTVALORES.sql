CREATE TABLE [dbo].[OTVALORES] (
    [ID]           INT           IDENTITY (1, 1) NOT NULL,
    [IDHCHISPACA]  INT           NOT NULL,
    [IDGRUPO]      INT           NOT NULL,
    [IDOTVARIABLE] INT           NOT NULL,
    [VALOR]        VARCHAR (MAX) NOT NULL,
    [IDITEMLISTA]  INT           NULL,
    CONSTRAINT [PK_OTVALORES] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_OTVALORES_HCHISPACA] FOREIGN KEY ([IDHCHISPACA]) REFERENCES [dbo].[HCHISPACA] ([ID]),
    CONSTRAINT [FK_OTVALORES_OTGRUPO] FOREIGN KEY ([IDGRUPO]) REFERENCES [dbo].[OTGRUPO] ([ID]),
    CONSTRAINT [FK_OTVALORES_OTVALORES] FOREIGN KEY ([ID]) REFERENCES [dbo].[OTVALORES] ([ID]),
    CONSTRAINT [FK_OTVALORES_OTVARIABLES] FOREIGN KEY ([IDOTVARIABLE]) REFERENCES [dbo].[OTVARIABLES] ([ID]),
    CONSTRAINT [FK_OTVALORES_OTVARIABLESL] FOREIGN KEY ([IDITEMLISTA]) REFERENCES [dbo].[OTVARIABLESL] ([ID])
);


GO
ALTER TABLE [dbo].[OTVALORES] NOCHECK CONSTRAINT [FK_OTVALORES_HCHISPACA];


GO
ALTER TABLE [dbo].[OTVALORES] NOCHECK CONSTRAINT [FK_OTVALORES_OTGRUPO];


GO
ALTER TABLE [dbo].[OTVALORES] NOCHECK CONSTRAINT [FK_OTVALORES_OTVALORES];


GO
ALTER TABLE [dbo].[OTVALORES] NOCHECK CONSTRAINT [FK_OTVALORES_OTVARIABLES];


GO
ALTER TABLE [dbo].[OTVALORES] NOCHECK CONSTRAINT [FK_OTVALORES_OTVARIABLESL];


GO
CREATE NONCLUSTERED INDEX [IX_OTVALORES_IDHCHISPACA_IDOTVARIABLE_IDITEMLISTA_VALOR]
    ON [dbo].[OTVALORES]([IDHCHISPACA] ASC, [IDOTVARIABLE] ASC)
    INCLUDE([IDITEMLISTA], [VALOR]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de la lista en caso de que la variable sea de tipo combo.  se relaciona con la tabla OTVARIABLESL', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OTVALORES', @level2type = N'COLUMN', @level2name = N'IDITEMLISTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el valor ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OTVALORES', @level2type = N'COLUMN', @level2name = N'VALOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de la variable que se esta almacenando ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OTVALORES', @level2type = N'COLUMN', @level2name = N'IDOTVARIABLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Id del grupo ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OTVALORES', @level2type = N'COLUMN', @level2name = N'IDGRUPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de la relacion con el tablero de la Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OTVALORES', @level2type = N'COLUMN', @level2name = N'IDHCHISPACA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OTVALORES', @level2type = N'COLUMN', @level2name = N'ID';

