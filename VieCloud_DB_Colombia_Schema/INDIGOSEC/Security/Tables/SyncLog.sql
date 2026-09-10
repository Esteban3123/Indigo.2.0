CREATE TABLE [Security].[SyncLog] (
    [Id]              INT           IDENTITY (1, 1) NOT NULL,
    [MessageType]     TINYINT       NOT NULL,
    [Message]         VARCHAR (MAX) NOT NULL,
    [SourceCode]      VARCHAR (50)  NOT NULL,
    [Source]          VARCHAR (50)  NOT NULL,
    [Action]          VARCHAR (10)  NOT NULL,
    [SourceDataBase]  VARCHAR (50)  NOT NULL,
    [TargetDataBase]  VARCHAR (50)  NOT NULL,
    [Data]            TEXT          NOT NULL,
    [CurrentDateTime] DATETIME      CONSTRAINT [DF__SyncLog__Current__70C8B53F] DEFAULT ([Common].[GETDATE]()) NOT NULL,
    CONSTRAINT [PK_SyncLog] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de Mensaje:
1 = Error
2 = Advertencia', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'SyncLog', @level2type = N'COLUMN', @level2name = N'MessageType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mensaje del log', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'SyncLog', @level2type = N'COLUMN', @level2name = N'Message';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Codigo del registro de la entidad donde se originó', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'SyncLog', @level2type = N'COLUMN', @level2name = N'SourceCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Entidad donde se originó.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'SyncLog', @level2type = N'COLUMN', @level2name = N'Source';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de Acción:
0 = No Identificada
1 = Crear
2 = Modificar
3 = Eliminar', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'SyncLog', @level2type = N'COLUMN', @level2name = N'Action';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Base de datos de origen', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'SyncLog', @level2type = N'COLUMN', @level2name = N'SourceDataBase';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Base de datos destino', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'SyncLog', @level2type = N'COLUMN', @level2name = N'TargetDataBase';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Data del evento', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'SyncLog', @level2type = N'COLUMN', @level2name = N'Data';

