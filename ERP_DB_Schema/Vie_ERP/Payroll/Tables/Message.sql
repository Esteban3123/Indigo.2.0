CREATE TABLE [Payroll].[Message] (
    [Id]            INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [LiquitadionId] INT           NOT NULL,
    [PayrollDate]   DATE          NOT NULL,
    [Error]         BIT           NOT NULL,
    [Description]   VARCHAR (500) NOT NULL,
    CONSTRAINT [PK_Message] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Message_Liquidation] FOREIGN KEY ([LiquitadionId]) REFERENCES [Payroll].[Liquidation] ([Id]) ON DELETE CASCADE
);


GO
ALTER TABLE [Payroll].[Message] NOCHECK CONSTRAINT [FK_Message_Liquidation];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripción del Mensaje', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Message', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Mensaje de Error: 1 - SI 0 - No (Advertencia o Información)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Message', @level2type = N'COLUMN', @level2name = N'Error';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Liquidación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Message', @level2type = N'COLUMN', @level2name = N'PayrollDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la Liquidación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Message', @level2type = N'COLUMN', @level2name = N'LiquitadionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Message', @level2type = N'COLUMN', @level2name = N'Id';

