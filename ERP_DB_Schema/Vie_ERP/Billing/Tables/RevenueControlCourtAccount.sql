CREATE TABLE [Billing].[RevenueControlCourtAccount] (
    [Id]               INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [RevenueControlId] INT          NOT NULL,
    [CourtDate]        DATE         NOT NULL,
    [CreationUser]     VARCHAR (20) NOT NULL,
    [CreationDate]     DATETIME     NOT NULL,
    CONSTRAINT [PK_RevenueControlCourtAccount] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RevenueControlCourtAccount_RevenueControl] FOREIGN KEY ([RevenueControlId]) REFERENCES [Billing].[RevenueControl] ([Id])
);


GO
ALTER TABLE [Billing].[RevenueControlCourtAccount] NOCHECK CONSTRAINT [FK_RevenueControlCourtAccount_RevenueControl];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlCourtAccount', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlCourtAccount', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la fecha de Corte', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlCourtAccount', @level2type = N'COLUMN', @level2name = N'CourtDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera del Ingreso', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlCourtAccount', @level2type = N'COLUMN', @level2name = N'RevenueControlId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlCourtAccount', @level2type = N'COLUMN', @level2name = N'Id';

