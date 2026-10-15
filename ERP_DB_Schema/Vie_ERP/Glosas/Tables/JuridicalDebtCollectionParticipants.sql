CREATE TABLE [Glosas].[JuridicalDebtCollectionParticipants] (
    [Id]              INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ConciliationCId] INT           NOT NULL,
    [Type]            CHAR (1)      NOT NULL,
    [FullName]        VARCHAR (200) NOT NULL,
    [Position]        VARCHAR (200) NOT NULL,
    [TimeStamp]       ROWVERSION    NOT NULL,
    CONSTRAINT [PK_JuridicalDebtCollectionParticipants] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_JuridicalDebtCollectionParticipants_ConciliationC] FOREIGN KEY ([ConciliationCId]) REFERENCES [Glosas].[ConciliationC] ([Id])
);


GO
ALTER TABLE [Glosas].[JuridicalDebtCollectionParticipants] NOCHECK CONSTRAINT [FK_JuridicalDebtCollectionParticipants_ConciliationC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'JuridicalDebtCollectionParticipants', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cargo del participante', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'JuridicalDebtCollectionParticipants', @level2type = N'COLUMN', @level2name = N'Position';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del participante', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'JuridicalDebtCollectionParticipants', @level2type = N'COLUMN', @level2name = N'FullName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - IPS, 2 - Externo', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'JuridicalDebtCollectionParticipants', @level2type = N'COLUMN', @level2name = N'Type';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de relacion con la cabecera de conciliacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'JuridicalDebtCollectionParticipants', @level2type = N'COLUMN', @level2name = N'ConciliationCId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo de los participantes de conciliacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'JuridicalDebtCollectionParticipants', @level2type = N'COLUMN', @level2name = N'Id';

