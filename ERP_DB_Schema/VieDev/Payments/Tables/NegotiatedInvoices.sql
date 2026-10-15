CREATE TABLE [Payments].[NegotiatedInvoices] (
    [Id]               INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CodeHash]         VARCHAR (36) NOT NULL,
    [DateNegotiation]  DATETIME     NOT NULL,
    [CreationDate]     DATETIME     NOT NULL,
    [Status]           TINYINT      CONSTRAINT [DF_NegotiatedInvoices_Status] DEFAULT ((1)) NOT NULL,
    [ModificationUser] VARCHAR (20) NULL,
    [ModificationDate] DATETIME     NULL,
    [TimeStamp]        ROWVERSION   NOT NULL,
    CONSTRAINT [PK_NegotiatedInvoices] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoices', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoices', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario quien confirma', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoices', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '0: Inactiva. 1:Pending, 2: OK, 3: Error, 4: Se negocia con errores', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoices', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creacion', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoices', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Negociacion', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoices', @level2type = N'COLUMN', @level2name = N'DateNegotiation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo hash', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoices', @level2type = N'COLUMN', @level2name = N'CodeHash';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de las verificaciones de negociaciones', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoices', @level2type = N'COLUMN', @level2name = N'Id';

