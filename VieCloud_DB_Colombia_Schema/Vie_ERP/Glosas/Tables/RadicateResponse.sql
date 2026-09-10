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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de respuesta de radicación (DATETIME, auditoría de creación)', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'RadicateResponse', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'RadicateResponse', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'RadicateResponse', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario o identificación de quien creó el registro de respuesta de radicación (VARCHAR 20, auditoría)', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'RadicateResponse', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de creacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'RadicateResponse', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'RadicateResponse', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Comentarios, observaciones o notas adicionales sobre la respuesta de radicación de glosa (VARCHAR 200, opcional)', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'RadicateResponse', @level2type = N'COLUMN', @level2name = N'Comments';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Comentarios', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'RadicateResponse', @level2type = N'COLUMN', @level2name = N'Comments';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'RadicateResponse', @level2type = N'COLUMN', @level2name = N'Comments';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación o nombre de quién envía/remite la respuesta de radicación de glosa (VARCHAR 60, remitente)', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'RadicateResponse', @level2type = N'COLUMN', @level2name = N'WhoSend';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Quien envio', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'RadicateResponse', @level2type = N'COLUMN', @level2name = N'WhoSend';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'RadicateResponse', @level2type = N'COLUMN', @level2name = N'WhoSend';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación o nombre de quién recibe la respuesta de radicación de glosa (VARCHAR 50, destinatario)', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'RadicateResponse', @level2type = N'COLUMN', @level2name = N'WhoReceive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Quien recibe', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'RadicateResponse', @level2type = N'COLUMN', @level2name = N'WhoReceive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'RadicateResponse', @level2type = N'COLUMN', @level2name = N'WhoReceive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de radicación oficial de la respuesta de glosa ante la entidad receptora (DATETIME, timestamp legal)', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'RadicateResponse', @level2type = N'COLUMN', @level2name = N'DateRadicate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de radicacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'RadicateResponse', @level2type = N'COLUMN', @level2name = N'DateRadicate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'RadicateResponse', @level2type = N'COLUMN', @level2name = N'DateRadicate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (PK, IDENTITY) del registro de respuesta de radicación de glosa (INT, clave primaria)', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'RadicateResponse', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla respuesta de radicación', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'RadicateResponse', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'RadicateResponse', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de respuestas de radicación de glosas: guarda cada notificación o respuesta enviada y recibida durante el proceso de radicación de glosas entre la IPS y el pagador (aseguradora/EPS), incluyendo quién envía, quién recibe, fecha y observaciones del intercambio.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'RadicateResponse';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'RadicateResponse';
