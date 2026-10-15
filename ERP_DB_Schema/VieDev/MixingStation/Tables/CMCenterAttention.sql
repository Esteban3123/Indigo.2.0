CREATE TABLE [MixingStation].[CMCenterAttention] (
    [Id]                  INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdMixingStation]     INT       NOT NULL,
    [IdCenterAttention]   INT       NULL,
    [IdProductionLine]    INT       NOT NULL,
    [CodeCenterAttention] CHAR (10) CONSTRAINT [DF_CMCenterAttention_CodeCenterAttention] DEFAULT ('          ') NOT NULL,
    [StateCA]             BIT       NULL,
    CONSTRAINT [PK_CMCenterAttention] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CMCenterAttention_CMConfiguration] FOREIGN KEY ([IdMixingStation]) REFERENCES [MixingStation].[CMConfiguration] ([Id]),
    CONSTRAINT [FK_CMCenterAttention_ProductionLine] FOREIGN KEY ([IdProductionLine]) REFERENCES [MixingStation].[ProductionLine] ([Id])
);


GO
ALTER TABLE [MixingStation].[CMCenterAttention] NOCHECK CONSTRAINT [FK_CMCenterAttention_CMConfiguration];


GO
ALTER TABLE [MixingStation].[CMCenterAttention] NOCHECK CONSTRAINT [FK_CMCenterAttention_ProductionLine];

