CREATE TABLE [dbo].[SyncLog] (
    [Id]             INT           IDENTITY (1, 1) NOT NULL,
    [MessageType]    TINYINT       NOT NULL,
    [Message]        VARCHAR (MAX) NOT NULL,
    [SourceCode]     VARCHAR (50)  NOT NULL,
    [Source]         VARCHAR (50)  NOT NULL,
    [ActionType]     TINYINT       NOT NULL,
    [SourceDataBase] VARCHAR (50)  NOT NULL,
    [TargetDataBase] VARCHAR (50)  NOT NULL,
    [Data]           TEXT          NOT NULL,
    CONSTRAINT [PK_SyncLog] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Determina el tipo de Mensaje:
1 = Error
2 = Advertencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SyncLog', @level2type = N'COLUMN', @level2name = N'MessageType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define el mensaje del log', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SyncLog', @level2type = N'COLUMN', @level2name = N'Message';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define el codigo de la entidad origen.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SyncLog', @level2type = N'COLUMN', @level2name = N'SourceCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define la entidad origen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SyncLog', @level2type = N'COLUMN', @level2name = N'Source';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Determina el tipo de Acción:
0 = No Reconocida
1 = Crear
2 = Modificar
3 = Eliminar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SyncLog', @level2type = N'COLUMN', @level2name = N'ActionType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define la base de datos donde se origina.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SyncLog', @level2type = N'COLUMN', @level2name = N'SourceDataBase';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define la base de datos destino.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SyncLog', @level2type = N'COLUMN', @level2name = N'TargetDataBase';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define la data que fue enviada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SyncLog', @level2type = N'COLUMN', @level2name = N'Data';

