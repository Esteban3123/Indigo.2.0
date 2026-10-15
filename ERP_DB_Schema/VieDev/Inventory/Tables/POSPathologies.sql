CREATE TABLE [Inventory].[POSPathologies] (
    [Id]           INT     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ProductId]    INT     NULL,
    [DiagnosticId] INT     NOT NULL,
    [MedicamentId] INT     NULL,
    [MinimumAge]   TINYINT NULL,
    [MaximumAge]   TINYINT NULL,
    [AgeMeasure]   TINYINT NULL,
    CONSTRAINT [PK_POSPathologies] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_POSPathologies_ATC] FOREIGN KEY ([MedicamentId]) REFERENCES [Inventory].[ATC] ([Id]),
    CONSTRAINT [FK_POSPathologies_Diagnostic] FOREIGN KEY ([DiagnosticId]) REFERENCES [Inventory].[Diagnostic] ([Id]),
    CONSTRAINT [FK_POSPathologies_Product] FOREIGN KEY ([ProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id])
);


GO
ALTER TABLE [Inventory].[POSPathologies] NOCHECK CONSTRAINT [FK_POSPathologies_ATC];


GO
ALTER TABLE [Inventory].[POSPathologies] NOCHECK CONSTRAINT [FK_POSPathologies_Diagnostic];


GO
ALTER TABLE [Inventory].[POSPathologies] NOCHECK CONSTRAINT [FK_POSPathologies_Product];

