CREATE TABLE [Inventory].[RequestParamFuncionalUnit] (
    [Id]               INT IDENTITY (1, 1) NOT NULL,
    [RequestParamId]   INT NOT NULL,
    [FunctionalUnitId] INT NOT NULL,
    CONSTRAINT [PK_RequestParamFuncionalUnit] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RequestParamFuncionalUnit_FunctionalUnit] FOREIGN KEY ([FunctionalUnitId]) REFERENCES [Payroll].[FunctionalUnit] ([Id]),
    CONSTRAINT [FK_RequestParamFuncionalUnit_RequestParam] FOREIGN KEY ([RequestParamId]) REFERENCES [Inventory].[RequestParam] ([Id])
);


GO
ALTER TABLE [Inventory].[RequestParamFuncionalUnit] NOCHECK CONSTRAINT [FK_RequestParamFuncionalUnit_FunctionalUnit];


GO
ALTER TABLE [Inventory].[RequestParamFuncionalUnit] NOCHECK CONSTRAINT [FK_RequestParamFuncionalUnit_RequestParam];


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_RequestParamFuncionalUnit]
    ON [Inventory].[RequestParamFuncionalUnit]([FunctionalUnitId] ASC, [RequestParamId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad funcional', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParamFuncionalUnit', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del parámetro de solicitud', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParamFuncionalUnit', @level2type = N'COLUMN', @level2name = N'RequestParamId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id Autoincrementable de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParamFuncionalUnit', @level2type = N'COLUMN', @level2name = N'Id';

