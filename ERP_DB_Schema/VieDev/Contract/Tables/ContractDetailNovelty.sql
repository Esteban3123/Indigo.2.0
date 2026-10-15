CREATE TABLE [Contract].[ContractDetailNovelty] (
    [Id]               INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ContractDetailId] INT           NOT NULL,
    [NoveltyDate]      DATETIME      NOT NULL,
    [NoveltySource]    TINYINT       NOT NULL,
    [Name]             VARCHAR (200) NOT NULL,
    [Description]      VARCHAR (500) NULL,
    [Status]           TINYINT       NOT NULL,
    CONSTRAINT [PK_ContractDetailNovelty] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ContractDetailNovelty_ContractDetail] FOREIGN KEY ([ContractDetailId]) REFERENCES [Contract].[ContractDetail] ([Id])
);


GO
ALTER TABLE [Contract].[ContractDetailNovelty] NOCHECK CONSTRAINT [FK_ContractDetailNovelty_ContractDetail];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado de la novedad:  1. Aprobada  2. Rechazada  3. Pendiente  4. En Proceso  5. Terminada', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetailNovelty', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Origen de la novedad:  1. Cliente  2. Prestador', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetailNovelty', @level2type = N'COLUMN', @level2name = N'NoveltySource';

