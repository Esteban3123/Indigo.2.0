CREATE TABLE [InteropCost].[InteropCostSecuenceDetail] (
    [Id]                    INT    IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [SequenseInteropCostId] INT    NOT NULL,
    [IdSequense]            INT    NOT NULL,
    [IdOperatingUnit]       INT    NULL,
    [Next]                  BIGINT CONSTRAINT [DF_SequenseInteropCostD_Next] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_SequenseInteropCostD] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InteropCostSecuenceDetail_Sequense] FOREIGN KEY ([IdSequense]) REFERENCES [Common].[Sequense] ([Id]),
    CONSTRAINT [FK_SequenseInteropCostD_OperatingUnit] FOREIGN KEY ([IdOperatingUnit]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_SequenseInteropCostD_SequenseInteropCostC] FOREIGN KEY ([SequenseInteropCostId]) REFERENCES [InteropCost].[InteropCostSecuence] ([Id])
);


GO
ALTER TABLE [InteropCost].[InteropCostSecuenceDetail] NOCHECK CONSTRAINT [FK_InteropCostSecuenceDetail_Sequense];


GO
ALTER TABLE [InteropCost].[InteropCostSecuenceDetail] NOCHECK CONSTRAINT [FK_SequenseInteropCostD_OperatingUnit];


GO
ALTER TABLE [InteropCost].[InteropCostSecuenceDetail] NOCHECK CONSTRAINT [FK_SequenseInteropCostD_SequenseInteropCostC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Siguiente numero a generar con la secuenacia', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'InteropCostSecuenceDetail', @level2type = N'COLUMN', @level2name = N'Next';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad operativa asignada a la secuencia. Solo cuando el ambito es UO-Unidad Operativa, de lo contrario el campo es nulo', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'InteropCostSecuenceDetail', @level2type = N'COLUMN', @level2name = N'IdOperatingUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la secuencia base', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'InteropCostSecuenceDetail', @level2type = N'COLUMN', @level2name = N'IdSequense';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'InteropCostSecuenceDetail', @level2type = N'COLUMN', @level2name = N'SequenseInteropCostId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'InteropCostSecuenceDetail', @level2type = N'COLUMN', @level2name = N'Id';

