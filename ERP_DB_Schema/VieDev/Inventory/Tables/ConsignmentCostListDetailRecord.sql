CREATE TABLE [Inventory].[ConsignmentCostListDetailRecord] (
    [Id]                          INT             IDENTITY (1, 1) NOT NULL,
    [ConsignmentCostListDetailId] INT             NOT NULL,
    [Cost]                        NUMERIC (12, 2) NULL,
    [CreationUser]                VARCHAR (20)    NOT NULL,
    [CreationDate]                DATETIME        NOT NULL,
    CONSTRAINT [PK_ConsignmentCostListDetailRecord] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ConsignmentCostListDetailRecord_ConsignmentCostListDetail] FOREIGN KEY ([ConsignmentCostListDetailId]) REFERENCES [Inventory].[ConsignmentCostListDetail] ([Id])
);


GO
ALTER TABLE [Inventory].[ConsignmentCostListDetailRecord] NOCHECK CONSTRAINT [FK_ConsignmentCostListDetailRecord_ConsignmentCostListDetail];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de creacion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentCostListDetailRecord', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario de creacion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentCostListDetailRecord', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del costo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentCostListDetailRecord', @level2type = N'COLUMN', @level2name = N'Cost';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del la tabla detalle', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentCostListDetailRecord', @level2type = N'COLUMN', @level2name = N'ConsignmentCostListDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentCostListDetailRecord', @level2type = N'COLUMN', @level2name = N'Id';

