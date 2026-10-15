CREATE TABLE [Payroll].[ContractModificationReason] (
    [Id]               INT           IDENTITY (1, 1) NOT NULL,
    [Code]             VARCHAR (20)  NOT NULL,
    [Name]             VARCHAR (50)  NOT NULL,
    [Description]      VARCHAR (MAX) NOT NULL,
    [State]            BIT           NOT NULL,
    [CreationUser]     VARCHAR (20)  NOT NULL,
    [CreationDate]     DATETIME      NOT NULL,
    [ModificationUser] VARCHAR (20)  NULL,
    [ModificationDate] DATETIME      NULL,
    [TimeStamp]        ROWVERSION    NOT NULL,
    [NoveltyType]      TINYINT       NOT NULL,
    CONSTRAINT [PK_ContractModificationReason] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de Novedad. 1 - Cambio de Sueldo, 2 - Cambio de Cargo, 3 - Otros', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractModificationReason', @level2type = N'COLUMN', @level2name = N'NoveltyType';

