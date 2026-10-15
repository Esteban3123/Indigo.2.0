CREATE TABLE [Inventory].[RequestParamDetailPeriodicity] (
    [Id]              INT          IDENTITY (1, 1) NOT NULL,
    [IdRequestParam]  INT          NOT NULL,
    [Month]           TINYINT      NULL,
    [SequenceOfMonth] VARCHAR (50) NULL,
    [Days]            VARCHAR (50) NULL,
    CONSTRAINT [PK_RequestParamDetailPeriodicity] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RequestParamDetailPeriodicity_RequestParam] FOREIGN KEY ([IdRequestParam]) REFERENCES [Inventory].[RequestParam] ([Id])
);


GO
ALTER TABLE [Inventory].[RequestParamDetailPeriodicity] NOCHECK CONSTRAINT [FK_RequestParamDetailPeriodicity_RequestParam];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Día seleccionado', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParamDetailPeriodicity', @level2type = N'COLUMN', @level2name = N'Days';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Secuencia del día en el mes', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParamDetailPeriodicity', @level2type = N'COLUMN', @level2name = N'SequenceOfMonth';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Mes del año seleccionado', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParamDetailPeriodicity', @level2type = N'COLUMN', @level2name = N'Month';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la solictud del parametro', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParamDetailPeriodicity', @level2type = N'COLUMN', @level2name = N'IdRequestParam';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParamDetailPeriodicity', @level2type = N'COLUMN', @level2name = N'Id';

