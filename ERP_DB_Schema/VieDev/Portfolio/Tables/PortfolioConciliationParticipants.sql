CREATE TABLE [Portfolio].[PortfolioConciliationParticipants] (
    [Id]                      INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PortfolioConciliationId] INT           NOT NULL,
    [FullName]                VARCHAR (100) NOT NULL,
    [Position]                VARCHAR (100) NOT NULL,
    [Type]                    TINYINT       NOT NULL,
    CONSTRAINT [PK_PortfolioConciliationParticipants] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ConciliationParticipants_PortfolioConciliation] FOREIGN KEY ([PortfolioConciliationId]) REFERENCES [Portfolio].[PortfolioConciliation] ([Id])
);


GO
ALTER TABLE [Portfolio].[PortfolioConciliationParticipants] NOCHECK CONSTRAINT [FK_ConciliationParticipants_PortfolioConciliation];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '     1 - IPS,     2 - EAPB', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioConciliationParticipants', @level2type = N'COLUMN', @level2name = N'Type';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cargo del participante', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioConciliationParticipants', @level2type = N'COLUMN', @level2name = N'Position';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre completo', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioConciliationParticipants', @level2type = N'COLUMN', @level2name = N'FullName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'id relacion cabecera de conciliacion', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioConciliationParticipants', @level2type = N'COLUMN', @level2name = N'PortfolioConciliationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de participantes de una conciliacion', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioConciliationParticipants', @level2type = N'COLUMN', @level2name = N'Id';

