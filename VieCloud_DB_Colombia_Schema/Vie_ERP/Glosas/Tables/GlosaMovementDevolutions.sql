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



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal ROWVERSION (tipo binario) que registra automáticamente el instante exacto de creación, modificación o cambio de estado del movimiento de devolución en la glosa.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementDevolutions', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementDevolutions', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementDevolutions', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de la última modificación o actualización del registro de devolución de glosa.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementDevolutions', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'fecha de modificacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementDevolutions', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementDevolutions', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (VARCHAR 20) del usuario que realizó la última modificación o cambio en el movimiento de devolución.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementDevolutions', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'usuario de modificacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementDevolutions', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementDevolutions', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) en que se creó originalmente el registro del movimiento de devolución de glosa.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementDevolutions', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementDevolutions', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementDevolutions', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (VARCHAR 20) del usuario que creó el registro inicial del movimiento de devolución.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementDevolutions', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'usuario de creacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementDevolutions', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementDevolutions', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del movimiento de devolución (CHAR 1): 1=Sin confirmar, 2=Confirmada. Indica si la devolución fue validada.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementDevolutions', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1- sin confirmar 2 confirmada', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementDevolutions', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementDevolutions', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Respuesta o justificación (VARCHAR MAX) del asegurador o entidad que devuelve la glosa, explicando la aceptación o rechazo.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementDevolutions', @level2type = N'COLUMN', @level2name = N'Answer';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'respuesta de devolucion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementDevolutions', @level2type = N'COLUMN', @level2name = N'Answer';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementDevolutions', @level2type = N'COLUMN', @level2name = N'Answer';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Comentario, motivo o causa (VARCHAR MAX) documentada para la devolución de glosa por parte del prestador o auditor.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementDevolutions', @level2type = N'COLUMN', @level2name = N'Comment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'comentario o motivo devolucion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementDevolutions', @level2type = N'COLUMN', @level2name = N'Comment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementDevolutions', @level2type = N'COLUMN', @level2name = N'Comment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) del concepto o causa de glosa asociado al movimiento de devolución. Referencia a [Common].[ConceptGlosas].', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementDevolutions', @level2type = N'COLUMN', @level2name = N'IdConceptGlosa';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'id relacion de concepto devolucion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementDevolutions', @level2type = N'COLUMN', @level2name = N'IdConceptGlosa';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementDevolutions', @level2type = N'COLUMN', @level2name = N'IdConceptGlosa';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de devolución (CHAR 1): 1=Justificada (con motivo válido), 2=Injustificada (sin causa válida). Clasifica la naturaleza de la devolución.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementDevolutions', @level2type = N'COLUMN', @level2name = N'TypeDevolution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1: Justificada, 2: Injustificada', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementDevolutions', @level2type = N'COLUMN', @level2name = N'TypeDevolution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementDevolutions', @level2type = N'COLUMN', @level2name = N'TypeDevolution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Monto en moneda local (MONEY) del valor devuelto, facturado o pendiente por pagar en el movimiento de devolución.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementDevolutions', @level2type = N'COLUMN', @level2name = N'DevolutionValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'valor devolucion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementDevolutions', @level2type = N'COLUMN', @level2name = N'DevolutionValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementDevolutions', @level2type = N'COLUMN', @level2name = N'DevolutionValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de factura (VARCHAR 50) o comprobante fiscal asociado al movimiento de devolución de glosa.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementDevolutions', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'numero de factura', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementDevolutions', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementDevolutions', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) del detalle de recepción de devolución. Referencia a [Glosas].[GlosaDevolutionsReceptionD].', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementDevolutions', @level2type = N'COLUMN', @level2name = N'IdDevolutionsReceptionD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id relacion detalle de devolucion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementDevolutions', @level2type = N'COLUMN', @level2name = N'IdDevolutionsReceptionD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementDevolutions', @level2type = N'COLUMN', @level2name = N'IdDevolutionsReceptionD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Autonumeral único (INT IDENTITY) que identifica cada movimiento o transacción individual de devolución de glosa.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementDevolutions', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico movimiento de devoluciones', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementDevolutions', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementDevolutions', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Movimientos de devolución de glosas: registra cada devolución asociada a una glosa, incluyendo el valor devuelto, el tipo de devolución, el concepto de glosa aplicado, comentarios, respuestas y el estado del movimiento. Permite rastrear el ciclo de vida de las devoluciones en el proceso de glosas de facturación.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementDevolutions';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementDevolutions';
