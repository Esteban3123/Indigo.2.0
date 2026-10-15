CREATE TABLE [Glosas].[GlosaDevolutionsReceptionD] (
    [Id]                           INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [GlosaDevolutionsReceptionCId] INT                                                                              NOT NULL,
    [GlosasParametersInterfaceId]  INT                                                                              NULL,
    [InvoiceNumber]                VARCHAR (50)                                                                     NOT NULL,
    [InvoiceDate]                  DATETIME                                                                         NOT NULL,
    [RadicatedNumber]              VARCHAR (50)                                                                     NULL,
    [RadicatedDate]                DATETIME                                                                         NULL,
    [BalanceInvoice]               MONEY                                                                            NULL,
    [PatientCode]                  VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [PatientName]                  VARCHAR (200) MASKED WITH (FUNCTION = 'partial(0, "Name_Ofuscado", 0)')          NOT NULL,
    [Ingress]                      VARCHAR (15)                                                                     NOT NULL,
    [UserNameInvoice]              VARCHAR (200)                                                                    NOT NULL,
    [Comment]                      VARCHAR (500)                                                                    NULL,
    [PlanCode]                     VARCHAR (20)                                                                     NULL,
    [ContractCode]                 VARCHAR (20)                                                                     NOT NULL,
    [ContractName]                 VARCHAR (200)                                                                    NOT NULL,
    [State]                        CHAR (1)                                                                         NOT NULL,
    [TimeStamp]                    ROWVERSION                                                                       NOT NULL,
    CONSTRAINT [PK_GlosaDevolutionsReceptionD__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_GlosaDevolutionsReceptionD_GlosaDevolutionsReceptionC] FOREIGN KEY ([GlosaDevolutionsReceptionCId]) REFERENCES [Glosas].[GlosaDevolutionsReceptionC] ([Id]),
    CONSTRAINT [FK_GlosaDevolutionsReceptionD_GlosasParametersInterface] FOREIGN KEY ([GlosasParametersInterfaceId]) REFERENCES [Glosas].[GlosasParametersInterface] ([Id])
);


GO
ALTER TABLE [Glosas].[GlosaDevolutionsReceptionD] NOCHECK CONSTRAINT [FK_GlosaDevolutionsReceptionD_GlosaDevolutionsReceptionC];


GO
ALTER TABLE [Glosas].[GlosaDevolutionsReceptionD] NOCHECK CONSTRAINT [FK_GlosaDevolutionsReceptionD_GlosasParametersInterface];


GO
CREATE NONCLUSTERED INDEX [IX_GlosaDevolutionsReceptionD__GlosaDevolutionsReceptionCId]
    ON [Glosas].[GlosaDevolutionsReceptionD]([GlosaDevolutionsReceptionCId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionD', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1:Sin confirmar 2:Confirmado', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionD', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de contrato', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionD', @level2type = N'COLUMN', @level2name = N'ContractName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'codigo de contrato', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionD', @level2type = N'COLUMN', @level2name = N'ContractCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'codigo de plan', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionD', @level2type = N'COLUMN', @level2name = N'PlanCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'comentario', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionD', @level2type = N'COLUMN', @level2name = N'Comment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'usuario de facturacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionD', @level2type = N'COLUMN', @level2name = N'UserNameInvoice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'numero de ingreso', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionD', @level2type = N'COLUMN', @level2name = N'Ingress';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'nombre de paciente', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionD', @level2type = N'COLUMN', @level2name = N'PatientName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'identificacion de paciente', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionD', @level2type = N'COLUMN', @level2name = N'PatientCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'saldo de factura', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionD', @level2type = N'COLUMN', @level2name = N'BalanceInvoice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'fecha de radicado', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionD', @level2type = N'COLUMN', @level2name = N'RadicatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'numero de radicado', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionD', @level2type = N'COLUMN', @level2name = N'RadicatedNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'fecha de factura', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionD', @level2type = N'COLUMN', @level2name = N'InvoiceDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'numero de factura', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionD', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'id relacion con configuracion de interfaces', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionD', @level2type = N'COLUMN', @level2name = N'GlosasParametersInterfaceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id relacion con cabecera devolucion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionD', @level2type = N'COLUMN', @level2name = N'GlosaDevolutionsReceptionCId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico detalle de devolucion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaDevolutionsReceptionD', @level2type = N'COLUMN', @level2name = N'Id';

