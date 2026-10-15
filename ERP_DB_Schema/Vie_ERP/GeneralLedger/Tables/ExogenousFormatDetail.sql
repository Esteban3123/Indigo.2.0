CREATE TABLE [GeneralLedger].[ExogenousFormatDetail] (
    [Id]                INT     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ExogenousFormatId] INT     NOT NULL,
    [Concept]           INT     NULL,
    [ConceptType]       TINYINT NULL,
    [MainAccountId]     INT     NOT NULL,
    [Nature]            TINYINT NOT NULL,
    [ThirdPartyBy]      TINYINT NULL,
    CONSTRAINT [PK_ExogenousFormatDetail__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ExogenousFormatDetail_ExogenousFormat] FOREIGN KEY ([ExogenousFormatId]) REFERENCES [GeneralLedger].[ExogenousFormat] ([Id]),
    CONSTRAINT [FK_ExogenousFormatDetail_MainAccount] FOREIGN KEY ([MainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id])
);


GO
ALTER TABLE [GeneralLedger].[ExogenousFormatDetail] NOCHECK CONSTRAINT [FK_ExogenousFormatDetail_ExogenousFormat];


GO
ALTER TABLE [GeneralLedger].[ExogenousFormatDetail] NOCHECK CONSTRAINT [FK_ExogenousFormatDetail_MainAccount];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica de donde se obtendrá el tercero  1 - Documento contable  2 - Documento origen', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'ExogenousFormatDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Naturaleza de la cual obtener la información:  1 - Débito  2 - Crédito  3 - Saldo  4 - Saldo Acumulado  5 - Base Gravable  6 - Valor Facturado', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'ExogenousFormatDetail', @level2type = N'COLUMN', @level2name = N'Nature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta contable parametrizada', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'ExogenousFormatDetail', @level2type = N'COLUMN', @level2name = N'MainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Columna del formato, si este lo maneja', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'ExogenousFormatDetail', @level2type = N'COLUMN', @level2name = N'ConceptType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Concepto del formato, si este lo maneja', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'ExogenousFormatDetail', @level2type = N'COLUMN', @level2name = N'Concept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del formato', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'ExogenousFormatDetail', @level2type = N'COLUMN', @level2name = N'ExogenousFormatId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle del formato', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'ExogenousFormatDetail', @level2type = N'COLUMN', @level2name = N'Id';

