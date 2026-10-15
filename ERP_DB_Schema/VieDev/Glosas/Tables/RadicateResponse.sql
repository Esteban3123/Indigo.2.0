CREATE TABLE [Glosas].[RadicateResponse] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [DateRadicate] DATETIME      NOT NULL,
    [WhoReceive]   VARCHAR (50)  NOT NULL,
    [WhoSend]      VARCHAR (60)  NOT NULL,
    [Comments]     VARCHAR (200) NULL,
    [CreationUser] VARCHAR (20)  NOT NULL,
    [CreationDate] DATETIME      NOT NULL,
    CONSTRAINT [PK_RadicateResponse] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'RadicateResponse', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario de creacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'RadicateResponse', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Comentarios', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'RadicateResponse', @level2type = N'COLUMN', @level2name = N'Comments';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Quien envio', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'RadicateResponse', @level2type = N'COLUMN', @level2name = N'WhoSend';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Quien recibe', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'RadicateResponse', @level2type = N'COLUMN', @level2name = N'WhoReceive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de radicacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'RadicateResponse', @level2type = N'COLUMN', @level2name = N'DateRadicate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla respuesta de radicación', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'RadicateResponse', @level2type = N'COLUMN', @level2name = N'Id';

