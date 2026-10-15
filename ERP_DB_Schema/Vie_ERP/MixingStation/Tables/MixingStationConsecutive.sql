CREATE TABLE [MixingStation].[MixingStationConsecutive] (
    [Id]                INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]              INT           NOT NULL,
    [Descripcion]       VARCHAR (100) NOT NULL,
    [NumberConsecutive] DECIMAL (18)  NOT NULL,
    [ConsecutiveDate]   DATETIME      NOT NULL,
    CONSTRAINT [PK_Consecutive] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena la fecha del dia en que se esta creando los lotes', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MixingStationConsecutive', @level2type = N'COLUMN', @level2name = N'ConsecutiveDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del consecutivo', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MixingStationConsecutive', @level2type = N'COLUMN', @level2name = N'NumberConsecutive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del consecutivo', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MixingStationConsecutive', @level2type = N'COLUMN', @level2name = N'Descripcion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de los consecutivos', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MixingStationConsecutive', @level2type = N'COLUMN', @level2name = N'Id';

