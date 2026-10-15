CREATE TABLE [Common].[BotControl] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [TableName]   VARCHAR (200) NOT NULL,
    [LastId]      INT           NULL,
    [LastDate]    DATETIME      NOT NULL,
    [TypeControl] TINYINT       NOT NULL,
    [DateRanges]  VARCHAR (200) NOT NULL,
    [Disabled]    BIT           DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_BotControl] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Parametro para desactivar o activar el bot, 0 - activado ; 1 - desactivado', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BotControl', @level2type = N'COLUMN', @level2name = N'Disabled';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Rango por intervalos de fecha: estructura del dato(#@(especifica si es Dia: dd, mes: mm, año: aa)                   Ej: 1@dd (intervalo de 1 dia);                    1@mm (intervalos de 1 mes)                    1@aa (intervalos de 1 año)) ', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BotControl', @level2type = N'COLUMN', @level2name = N'DateRanges';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '0-Sincronizar por Id; 1-sincronizar por fecha', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BotControl', @level2type = N'COLUMN', @level2name = N'TypeControl';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ultima fecha del registro de la tabla insertado', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BotControl', @level2type = N'COLUMN', @level2name = N'LastDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ultimo Id', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BotControl', @level2type = N'COLUMN', @level2name = N'LastId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de la tabla', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BotControl', @level2type = N'COLUMN', @level2name = N'TableName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BotControl', @level2type = N'COLUMN', @level2name = N'Id';

