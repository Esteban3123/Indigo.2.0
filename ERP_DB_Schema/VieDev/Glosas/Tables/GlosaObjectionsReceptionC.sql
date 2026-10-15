CREATE TABLE [Glosas].[GlosaObjectionsReceptionC] (
    [Id]                         INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [RadicatedConsecutive]       INT           NOT NULL,
    [DocumentNumber]             VARCHAR (30)  NOT NULL,
    [CustomerId]                 INT           NOT NULL,
    [RadicatedDate]              DATETIME      NOT NULL,
    [DocumentDate]               DATETIME      NOT NULL,
    [Comment]                    VARCHAR (250) NOT NULL,
    [State]                      CHAR (1)      NOT NULL,
    [DateResponsePostDocument]   DATETIME      NULL,
    [DateRadicatedDocumentReply] DATETIME      NULL,
    [ReceivesTheSettled]         VARCHAR (200) NULL,
    [DocumentCommentRadicated]   VARCHAR (500) NULL,
    [ConfirmDate]                DATETIME      NULL,
    [RadicatedUser]              INT           NULL,
    [ConfirmUser]                INT           NULL,
    [CreationUser]               VARCHAR (20)  CONSTRAINT [DF_GlosaObjectionsReceptionC_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]               DATETIME      CONSTRAINT [DF_GlosaObjectionsReceptionC_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser]           VARCHAR (20)  NULL,
    [ModificationDate]           DATETIME      NULL,
    [TimeStamp]                  ROWVERSION    NOT NULL,
    [IdRadicateResponse]         INT           NULL,
    CONSTRAINT [PK_GlosaObjectionsReceptionC__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_GlosaObjectionsReceptionC_RadicateResponse] FOREIGN KEY ([IdRadicateResponse]) REFERENCES [Glosas].[RadicateResponse] ([Id]),
    CONSTRAINT [FK_ObjectionsReceptionC_Customer] FOREIGN KEY ([CustomerId]) REFERENCES [Common].[Customer] ([Id]),
    CONSTRAINT [UQ_GlosaObjectionsReceptionC__DocumentNumber__RadicatedConsecutive] UNIQUE NONCLUSTERED ([DocumentNumber] ASC, [RadicatedConsecutive] ASC)
);


GO
ALTER TABLE [Glosas].[GlosaObjectionsReceptionC] NOCHECK CONSTRAINT [FK_GlosaObjectionsReceptionC_RadicateResponse];


GO
ALTER TABLE [Glosas].[GlosaObjectionsReceptionC] NOCHECK CONSTRAINT [FK_ObjectionsReceptionC_Customer];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado de la cabecera   1: Sin Confirmar   2: Confirmado Radicado   3: Oficio Con respuesta Enviada   4: Anulada', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaObjectionsReceptionC', @level2type = N'COLUMN', @level2name = N'State';

