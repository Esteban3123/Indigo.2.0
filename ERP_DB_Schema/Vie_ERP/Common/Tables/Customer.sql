CREATE TABLE [Common].[Customer] (
    [Id]                      INT                                                                         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Nit]                     VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Nit_Ofuscado", 0)')       NOT NULL,
    [Name]                    VARCHAR (100) MASKED WITH (FUNCTION = 'partial(0, "Customer_Ofuscado", 0)') NOT NULL,
    [EPSCode]                 VARCHAR (20)                                                                NULL,
    [ThirdPartyId]            INT                                                                         CONSTRAINT [DF_Customer_ThirdPartyId] DEFAULT ((4)) NOT NULL,
    [MainAccountReceivableId] INT                                                                         NOT NULL,
    [Term]                    INT                                                                         NOT NULL,
    [State]                   BIT                                                                         NOT NULL,
    [CreationUser]            VARCHAR (20)                                                                NOT NULL,
    [CreationDate]            DATETIME                                                                    NOT NULL,
    [ModificationUser]        VARCHAR (20)                                                                NULL,
    [ModificationDate]        DATETIME                                                                    NULL,
    [TimeStamp]               ROWVERSION                                                                  NOT NULL,
    [BasicBillingDeteriorationClassificationId] INT                                                       NULL,
	[HealthInvoiceDeteriorationClassificationId] INT                                                      NULL,
    CONSTRAINT [PK_Customer__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Customer_MainAccounts] FOREIGN KEY ([MainAccountReceivableId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_Customer_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id]),
    CONSTRAINT [UQ_Customer__Nit] UNIQUE NONCLUSTERED ([Nit] ASC)
);


GO
ALTER TABLE [Common].[Customer] NOCHECK CONSTRAINT [FK_Customer_MainAccounts];


GO
ALTER TABLE [Common].[Customer] NOCHECK CONSTRAINT [FK_Customer_ThirdParty];

GO
ALTER TABLE [Common].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_Customer_BasicBillingDeterioration] FOREIGN KEY([BasicBillingDeteriorationClassificationId])
REFERENCES [Portfolio].[PortfolioDeteriorationClassification] ([Id])


GO
ALTER TABLE [Common].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_Customer_HealthInvoiceDeterioration] FOREIGN KEY([HealthInvoiceDeteriorationClassificationId])
REFERENCES [Portfolio].[PortfolioDeteriorationClassification] ([Id])

GO
CREATE NONCLUSTERED INDEX [IX_Customer_ThirdPartyId]
    ON [Common].[Customer]([ThirdPartyId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del concepto general  1 - Activo  0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Plazo de la cuenta (En dias)', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'Term';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta por cobrar del cliente, este campo se usa cuando se hace un documento de Cuenta por Cobrar', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'MainAccountReceivableId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Tercero', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de la eps', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'EPSCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del tercero', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nit del tercero.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'Nit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de terceros', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'Id';

