CREATE TABLE [Inventory].[LoanMerchandiseDevolutionDetail] (
    [Id]                          INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [LoanMerchandiseDevolutionId] INT NOT NULL,
    [LoanMerchandiseDetaillId]    INT NOT NULL,
    [Quantity]                    INT NOT NULL,
    CONSTRAINT [PK_LoanMerchandiseDevolutionDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_LoanMerchandiseDevolutionDetail_LoanMerchandiseDetail] FOREIGN KEY ([LoanMerchandiseDetaillId]) REFERENCES [Inventory].[LoanMerchandiseDetail] ([Id]),
    CONSTRAINT [FK_LoanMerchandiseDevolutionDetail_LoanMerchandiseDevolution] FOREIGN KEY ([LoanMerchandiseDevolutionId]) REFERENCES [Inventory].[LoanMerchandiseDevolution] ([Id])
);


GO
ALTER TABLE [Inventory].[LoanMerchandiseDevolutionDetail] NOCHECK CONSTRAINT [FK_LoanMerchandiseDevolutionDetail_LoanMerchandiseDetail];


GO
ALTER TABLE [Inventory].[LoanMerchandiseDevolutionDetail] NOCHECK CONSTRAINT [FK_LoanMerchandiseDevolutionDetail_LoanMerchandiseDevolution];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad que se va a devolver', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolutionDetail', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle del prestamo de mercancia que se va a devolver', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolutionDetail', @level2type = N'COLUMN', @level2name = N'LoanMerchandiseDetaillId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera de la devolucion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolutionDetail', @level2type = N'COLUMN', @level2name = N'LoanMerchandiseDevolutionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle de la devolucion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolutionDetail', @level2type = N'COLUMN', @level2name = N'Id';

