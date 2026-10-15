CREATE TABLE [Glosas].[GlosasPrivateParametersInterface] (
    [Id]                    INT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ParametersInterfaceId] INT         NULL,
    [YearAccount]           INT         NULL,
    [ConceptPortfolio]      VARCHAR (3) NULL,
    [ConceptDebitNote]      VARCHAR (3) NULL,
    [ConceptCashReceipts]   VARCHAR (3) NULL,
    [ConceptCreditNotes]    VARCHAR (3) NULL,
    [TimeStamp]             ROWVERSION  NOT NULL,
    CONSTRAINT [PK_GlosasPrivateParametersInterface] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_GlosasPrivateParametersInterface_GlosasParametersInterface] FOREIGN KEY ([ParametersInterfaceId]) REFERENCES [Glosas].[GlosasParametersInterface] ([Id])
);


GO
ALTER TABLE [Glosas].[GlosasPrivateParametersInterface] NOCHECK CONSTRAINT [FK_GlosasPrivateParametersInterface_GlosasParametersInterface];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasPrivateParametersInterface', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Concepto de las notas credito', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasPrivateParametersInterface', @level2type = N'COLUMN', @level2name = N'ConceptCreditNotes';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Concepto de los recibos de caja', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasPrivateParametersInterface', @level2type = N'COLUMN', @level2name = N'ConceptCashReceipts';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Concepto de la nota debito', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasPrivateParametersInterface', @level2type = N'COLUMN', @level2name = N'ConceptDebitNote';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuenta contable en donde se va a registrar todas las glosas recepcionadas.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasPrivateParametersInterface', @level2type = N'COLUMN', @level2name = N'ConceptPortfolio';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Año de las cuentas', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasPrivateParametersInterface', @level2type = N'COLUMN', @level2name = N'YearAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de los parametros de interfaz', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasPrivateParametersInterface', @level2type = N'COLUMN', @level2name = N'ParametersInterfaceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasPrivateParametersInterface', @level2type = N'COLUMN', @level2name = N'Id';

