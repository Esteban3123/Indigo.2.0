CREATE TABLE [Treasury].[CheckCashingStatus] (
    [Id]          TINYINT      NOT NULL,
    [Description] VARCHAR (20) NULL,
    CONSTRAINT [PK_CheckCashingStatus] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripción', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CheckCashingStatus', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CheckCashingStatus', @level2type = N'COLUMN', @level2name = N'Id';

