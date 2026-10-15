CREATE TABLE [Cost].[CostSecuenceDetail] (
    [Id]                    INT    IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [SequenseInteropCostId] INT    NOT NULL,
    [IdSequense]            INT    NOT NULL,
    [IdOperatingUnit]       INT    NULL,
    [Next]                  BIGINT CONSTRAINT [DF_CostInteropCostSecuenceDetail_Next] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_CostInteropCostSecuenceDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CostInteropCostSecuenceDetail_CostInteropCostSecuence] FOREIGN KEY ([SequenseInteropCostId]) REFERENCES [Cost].[CostSecuence] ([Id]),
    CONSTRAINT [FK_CostInteropCostSecuenceDetail_OperatingUnit] FOREIGN KEY ([IdOperatingUnit]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_CostInteropCostSecuenceDetail_Sequense] FOREIGN KEY ([IdSequense]) REFERENCES [Common].[Sequense] ([Id])
);


GO
ALTER TABLE [Cost].[CostSecuenceDetail] NOCHECK CONSTRAINT [FK_CostInteropCostSecuenceDetail_CostInteropCostSecuence];


GO
ALTER TABLE [Cost].[CostSecuenceDetail] NOCHECK CONSTRAINT [FK_CostInteropCostSecuenceDetail_OperatingUnit];


GO
ALTER TABLE [Cost].[CostSecuenceDetail] NOCHECK CONSTRAINT [FK_CostInteropCostSecuenceDetail_Sequense];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Siguiente numero a generar con la secuenacia', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostSecuenceDetail', @level2type = N'COLUMN', @level2name = N'Next';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad operativa asignada a la secuencia. Solo cuando el ambito es UO-Unidad Operativa, de lo contrario el campo es nulo', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostSecuenceDetail', @level2type = N'COLUMN', @level2name = N'IdOperatingUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la secuencia base', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostSecuenceDetail', @level2type = N'COLUMN', @level2name = N'IdSequense';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostSecuenceDetail', @level2type = N'COLUMN', @level2name = N'SequenseInteropCostId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostSecuenceDetail', @level2type = N'COLUMN', @level2name = N'Id';

