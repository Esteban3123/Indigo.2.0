CREATE TABLE [Glosas].[GlosaMovementDevolutions] (
    [Id]                      INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdDevolutionsReceptionD] INT           NOT NULL,
    [InvoiceNumber]           VARCHAR (50)  NOT NULL,
    [DevolutionValue]         MONEY         NOT NULL,
    [TypeDevolution]          CHAR (1)      NOT NULL,
    [IdConceptGlosa]          INT           NOT NULL,
    [Comment]                 VARCHAR (MAX) NULL,
    [Answer]                  VARCHAR (MAX) NULL,
    [State]                   CHAR (1)      NOT NULL,
    [CreationUser]            VARCHAR (20)  NULL,
    [CreationDate]            DATETIME      NULL,
    [ModificationUser]        VARCHAR (20)  NULL,
    [ModificationDate]        DATETIME      NULL,
    [TimeStamp]               ROWVERSION    NOT NULL,
    CONSTRAINT [PK_GlosaMovementDevolutions__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_GlosaMovementDevolutions_ConceptGlosas] FOREIGN KEY ([IdConceptGlosa]) REFERENCES [Common].[ConceptGlosas] ([Id]),
    CONSTRAINT [FK_GlosaMovementDevolutions_GlosaDevolutionsReceptionD] FOREIGN KEY ([IdDevolutionsReceptionD]) REFERENCES [Glosas].[GlosaDevolutionsReceptionD] ([Id])
);


GO
ALTER TABLE [Glosas].[GlosaMovementDevolutions] NOCHECK CONSTRAINT [FK_GlosaMovementDevolutions_ConceptGlosas];


GO
ALTER TABLE [Glosas].[GlosaMovementDevolutions] NOCHECK CONSTRAINT [FK_GlosaMovementDevolutions_GlosaDevolutionsReceptionD];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementDevolutions', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'fecha de modificacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementDevolutions', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'usuario de modificacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementDevolutions', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de creacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementDevolutions', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'usuario de creacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementDevolutions', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1- sin confirmar 2 confirmada', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementDevolutions', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'respuesta de devolucion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementDevolutions', @level2type = N'COLUMN', @level2name = N'Answer';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'comentario o motivo devolucion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementDevolutions', @level2type = N'COLUMN', @level2name = N'Comment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'id relacion de concepto devolucion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementDevolutions', @level2type = N'COLUMN', @level2name = N'IdConceptGlosa';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1: Justificada, 2: Injustificada', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementDevolutions', @level2type = N'COLUMN', @level2name = N'TypeDevolution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'valor devolucion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementDevolutions', @level2type = N'COLUMN', @level2name = N'DevolutionValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'numero de factura', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementDevolutions', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id relacion detalle de devolucion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementDevolutions', @level2type = N'COLUMN', @level2name = N'IdDevolutionsReceptionD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico movimiento de devoluciones', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementDevolutions', @level2type = N'COLUMN', @level2name = N'Id';

