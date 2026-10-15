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
ALTER TABLE [Glosas].[GlosaDevolutionsReceptionC] NOCHECK CONSTRAINT [FK_GlosaDevolutionsReceptionC_Customer];


GO
CREATE NONCLUSTERED INDEX [IX_GlosaDevolutionsReceptionC__CustomerId]
    ON [Glosas].[GlosaDevolutionsReceptionC]([CustomerId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionC', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'fecha de modificacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionC', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'usuario de modificacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionC', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'fecha de creacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionC', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'usuario de creacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionC', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cargo persona que envia  oficio', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionC', @level2type = N'COLUMN', @level2name = N'PersonSendsPosition';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Persona que envia el oficio', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionC', @level2type = N'COLUMN', @level2name = N'PersonSends';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'cargo persona recibe', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionC', @level2type = N'COLUMN', @level2name = N'ReceivesDevolutionPosition';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre persona que recibe la devolucion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionC', @level2type = N'COLUMN', @level2name = N'ReceivesDevolution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'fecha de confirmacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionC', @level2type = N'COLUMN', @level2name = N'ConfirmDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1:Sin Confirmar 2:Confirmado Radicado 3:Oficio con Respuesta Enviada 4:Anulado', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionC', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'comentario', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionC', @level2type = N'COLUMN', @level2name = N'Comment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'fecha del radicado', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionC', @level2type = N'COLUMN', @level2name = N'DocumentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'fecha del sistema del  radicado', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionC', @level2type = N'COLUMN', @level2name = N'RadicatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id relacion con el cliente', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionC', @level2type = N'COLUMN', @level2name = N'CustomerId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de oficio', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionC', @level2type = N'COLUMN', @level2name = N'DocumentNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'numero de radicado', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionC', @level2type = N'COLUMN', @level2name = N'RadicatedConsecutive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico cabecera de devoluciones', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionC', @level2type = N'COLUMN', @level2name = N'Id';

