CREATE TABLE [Glosas].[GlosaDevolutionsReceptionC] (
    [Id]                         INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [RadicatedConsecutive]       INT           NOT NULL,
    [DocumentNumber]             VARCHAR (30)  NOT NULL,
    [CustomerId]                 INT           NOT NULL,
    [RadicatedDate]              DATETIME      NOT NULL,
    [DocumentDate]               DATETIME      NOT NULL,
    [Comment]                    VARCHAR (250) NOT NULL,
    [State]                      CHAR (1)      NOT NULL,
    [ConfirmDate]                DATETIME      NULL,
    [ReceivesDevolution]         VARCHAR (50)  NULL,
    [ReceivesDevolutionPosition] VARCHAR (50)  NULL,
    [PersonSends]                VARCHAR (50)  NULL,
    [PersonSendsPosition]        VARCHAR (50)  NULL,
    [CreationUser]               VARCHAR (20)  CONSTRAINT [DF_GlosaDevolutionsReceptionC_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]               DATETIME      CONSTRAINT [DF_GlosaDevolutionsReceptionC_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser]           VARCHAR (20)  NULL,
    [ModificationDate]           DATETIME      NULL,
    [TimeStamp]                  ROWVERSION    NOT NULL,
    CONSTRAINT [PK_GlosaDevolutionsReceptionC__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_GlosaDevolutionsReceptionC_Customer] FOREIGN KEY ([CustomerId]) REFERENCES [Common].[Customer] ([Id])
);




GO



GO
CREATE NONCLUSTERED INDEX [IX_GlosaDevolutionsReceptionC__CustomerId]
    ON [Glosas].[GlosaDevolutionsReceptionC]([CustomerId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal TIMESTAMP de auditoría que registra automáticamente el instante de creación, modificación o cambio de estado del registro de devolución de glosa en el sistema.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionC', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionC', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionC', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de la última modificación del registro de devolución de glosa; nulo si no ha sido modificado desde su creación.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionC', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'fecha de modificacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionC', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionC', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de usuario (VARCHAR 20) que realizó la última modificación del registro de devolución; auditoría de cambios en glosas.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionC', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'usuario de modificacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionC', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionC', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de creación del registro de devolución de glosa; se asigna automáticamente por el sistema al radicar.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionC', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'fecha de creacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionC', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionC', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de usuario (VARCHAR 20) que creó el registro de devolución; auditoría de origen del registro de glosa.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionC', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'usuario de creacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionC', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionC', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cargo o título del profesional de la salud, administrativo o gestor que envía el oficio de devolución de glosa; facilita trazabilidad.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionC', @level2type = N'COLUMN', @level2name = N'PersonSendsPosition';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cargo persona que envia  oficio', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionC', @level2type = N'COLUMN', @level2name = N'PersonSendsPosition';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionC', @level2type = N'COLUMN', @level2name = N'PersonSendsPosition';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre completo del profesional, administrativo o gestor que envía el oficio de devolución de glosa al cliente/IPS.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionC', @level2type = N'COLUMN', @level2name = N'PersonSends';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Persona que envia el oficio', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionC', @level2type = N'COLUMN', @level2name = N'PersonSends';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionC', @level2type = N'COLUMN', @level2name = N'PersonSends';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cargo o título del responsable en el cliente/IPS que recibe la devolución de glosa; contacto de recepción.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionC', @level2type = N'COLUMN', @level2name = N'ReceivesDevolutionPosition';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'cargo persona recibe', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionC', @level2type = N'COLUMN', @level2name = N'ReceivesDevolutionPosition';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionC', @level2type = N'COLUMN', @level2name = N'ReceivesDevolutionPosition';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre completo del profesional o administrativo del cliente/IPS que recibe la devolución de glosa; trazabilidad de recepción.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionC', @level2type = N'COLUMN', @level2name = N'ReceivesDevolution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre persona que recibe la devolucion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionC', @level2type = N'COLUMN', @level2name = N'ReceivesDevolution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionC', @level2type = N'COLUMN', @level2name = N'ReceivesDevolution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME NULL) en que se confirma la recepción y procesamiento de la devolución de glosa; nulo hasta confirmación.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionC', @level2type = N'COLUMN', @level2name = N'ConfirmDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'fecha de confirmacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionC', @level2type = N'COLUMN', @level2name = N'ConfirmDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionC', @level2type = N'COLUMN', @level2name = N'ConfirmDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del proceso de devolución: 1=Sin Confirmar, 2=Confirmado Radicado, 3=Oficio con Respuesta Enviada, 4=Anulado; controla flujo de glosa.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionC', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1:Sin Confirmar 2:Confirmado Radicado 3:Oficio con Respuesta Enviada 4:Anulado', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionC', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionC', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Comentario o observación (VARCHAR 250) sobre la devolución de glosa, motivo de rechazo, aclaración o nota administrativa.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionC', @level2type = N'COLUMN', @level2name = N'Comment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'comentario', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionC', @level2type = N'COLUMN', @level2name = N'Comment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionC', @level2type = N'COLUMN', @level2name = N'Comment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del oficio o documento de devolución de glosa según emisor; diferente de fecha de radicación en sistema.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionC', @level2type = N'COLUMN', @level2name = N'DocumentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'fecha del radicado', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionC', @level2type = N'COLUMN', @level2name = N'DocumentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionC', @level2type = N'COLUMN', @level2name = N'DocumentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de radicación en el sistema de la devolución de glosa; marca entrada en el ERP/EHR.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionC', @level2type = N'COLUMN', @level2name = N'RadicatedDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'fecha del sistema del  radicado', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionC', @level2type = N'COLUMN', @level2name = N'RadicatedDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionC', @level2type = N'COLUMN', @level2name = N'RadicatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) de la relación con cliente/IPS/prestador; clave foránea a [Common].[Customer]; agrupa devoluciones por entidad.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionC', @level2type = N'COLUMN', @level2name = N'CustomerId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id relacion con el cliente', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionC', @level2type = N'COLUMN', @level2name = N'CustomerId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionC', @level2type = N'COLUMN', @level2name = N'CustomerId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de oficio (VARCHAR 30) de la devolución de glosa; identificador único del documento administrativo enviado.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionC', @level2type = N'COLUMN', @level2name = N'DocumentNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de oficio', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionC', @level2type = N'COLUMN', @level2name = N'DocumentNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionC', @level2type = N'COLUMN', @level2name = N'DocumentNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de radicado secuencial (INT) asignado por el sistema al ingresar la devolución de glosa; trazabilidad de orden.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionC', @level2type = N'COLUMN', @level2name = N'RadicatedConsecutive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'numero de radicado', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionC', @level2type = N'COLUMN', @level2name = N'RadicatedConsecutive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionC', @level2type = N'COLUMN', @level2name = N'RadicatedConsecutive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) de cabecera del registro de devolución de glosa; clave primaria del proceso de devoluciones.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionC', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico cabecera de devoluciones', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionC', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionC', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de recepciones de devoluciones de glosas. Almacena cada devolución formal de documentos glosados enviada por una entidad pagadora (aseguradora/EPS), incluyendo quién la recibe, quién la envía, la fecha de radicación y el estado del proceso de devolución.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionC';
