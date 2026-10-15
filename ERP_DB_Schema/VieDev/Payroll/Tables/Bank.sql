CREATE TABLE [Payroll].[Bank] (
    [Id]                      INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                    VARCHAR (20)  NOT NULL,
    [ThirdPartyId]            INT           NOT NULL,
    [Name]                    VARCHAR (320) NOT NULL,
    [AchCode]                 VARCHAR (10)  NOT NULL,
    [CenitCode]               VARCHAR (4)   CONSTRAINT [DF_Bank_CenitCode] DEFAULT ('') NOT NULL,
    [CenitVerification]       VARCHAR (1)   CONSTRAINT [DF_Bank_CenitVerification] DEFAULT ('') NOT NULL,
    [BankFileCode]            CHAR (3)      NULL,
    [State]                   BIT           NOT NULL,
    [CreationUser]            VARCHAR (20)  NOT NULL,
    [CreationDate]            DATETIME      NOT NULL,
    [ModificationUser]        VARCHAR (20)  NULL,
    [ModificationDate]        DATETIME      NULL,
    [TimeStamp]               ROWVERSION    NOT NULL,
    [BankAccountRegistration] BIT           CONSTRAINT [DF__Bank__BankAccoun__511738D3] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Bank__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Bank_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id]),
    CONSTRAINT [UQ_Bank__Code] UNIQUE NONCLUSTERED ([Code] ASC)
);


GO
ALTER TABLE [Payroll].[Bank] NOCHECK CONSTRAINT [FK_Bank_ThirdParty];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Registro cuenta bancaria  0 - Numérico  1 - Alfanumérico', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Bank', @level2type = N'COLUMN', @level2name = N'BankAccountRegistration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de archivo de banco:   ''001'' -> Bancolombia  ''002'' -> Av Villas  ''003'' -> Banco Popular  ''004'' -> Banco Occidente  ''005'' -> Banco BBVA  ''006'' -> Banco Davivienda  "007", "Plano Banco Caja Social"  "008", "Plano Banco de Bogotá"  "009", "Plano Bancolombia - SAP"  "010", "Plano Banco Itaú"  "011", "Plano Banco Cooperativo Coopcentral  "012", "Plano Banco Scotiabank CRC"  "013", "Plano Banco GNV Sudameris"  "014", "Plano Banco Davivienda CRC"  ', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Bank', @level2type = N'COLUMN', @level2name = N'BankFileCode';

