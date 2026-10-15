CREATE TABLE [Portfolio].[Circular030Detail] (
    [Id]                           INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Circular030Id]                INT             NOT NULL,
    [IdentificationTypeERP]        VARCHAR (2)     NOT NULL,
    [IdentificationNumberERP]      VARCHAR (25)    NULL,
    [NameERP]                      VARCHAR (250)   NOT NULL,
    [IdentificationTypeIPS_EPSS]   VARCHAR (2)     NOT NULL,
    [IdentificationNumberIPS_EPSS] VARCHAR (12)    NOT NULL,
    [PaymentType]                  VARCHAR (1)     NOT NULL,
    [InvoicePrefix]                VARCHAR (6)     NULL,
    [InvoiceNumber]                VARCHAR (20)    NOT NULL,
    [UpdateIndicator]              VARCHAR (1)     NOT NULL,
    [InvoiceValue]                 DECIMAL (18, 2) NOT NULL,
    [InvoiceDate]                  DATE            NOT NULL,
    [RadicateDate]                 DATE            NOT NULL,
    [DevolutionDate]               DATE            NULL,
    [TotalValuePayments]           DECIMAL (18, 2) NOT NULL,
    [ObjectionValue]               DECIMAL (18, 2) NOT NULL,
    [ObjectionWithAnswer]          BIT             NOT NULL,
    [InvoiceBalance]               DECIMAL (18, 2) NOT NULL,
    [InvoiceJudicialRecovery]      BIT             NOT NULL,
    [JudicialRecoveryStatus]       TINYINT         NOT NULL,
    [AccountReceivableId]          INT             NULL,
    CONSTRAINT [PK_Circular030Detail__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Circular030Detail_AccountReceivable] FOREIGN KEY ([AccountReceivableId]) REFERENCES [Portfolio].[AccountReceivable] ([Id]),
    CONSTRAINT [FK_Circular030Detail_Circular030] FOREIGN KEY ([Circular030Id]) REFERENCES [Portfolio].[Circular030] ([Id])
);


GO
ALTER TABLE [Portfolio].[Circular030Detail] NOCHECK CONSTRAINT [FK_Circular030Detail_AccountReceivable];


GO
ALTER TABLE [Portfolio].[Circular030Detail] NOCHECK CONSTRAINT [FK_Circular030Detail_Circular030];


GO
CREATE NONCLUSTERED INDEX [IX_Circular030Detail__Circular030Id__INC__DevolutionDate__IdentificationNumberERP__IdentificationNumberIPS_EPSS__IdentificationT]
    ON [Portfolio].[Circular030Detail]([Circular030Id] ASC)
    INCLUDE([IdentificationTypeERP], [IdentificationNumberERP], [NameERP], [IdentificationTypeIPS_EPSS], [IdentificationNumberIPS_EPSS], [PaymentType], [UpdateIndicator], [RadicateDate], [DevolutionDate], [TotalValuePayments], [ObjectionValue], [ObjectionWithAnswer], [JudicialRecoveryStatus], [InvoicePrefix], [InvoiceNumber], [InvoiceValue], [InvoiceDate], [InvoiceBalance], [InvoiceJudicialRecovery]);


GO
CREATE NONCLUSTERED INDEX [IX_Circular030Detail__Circular030Id__InvoicePrefix__InvoiceNumber]
    ON [Portfolio].[Circular030Detail]([Circular030Id] ASC, [InvoicePrefix] ASC, [InvoiceNumber] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta por cobrar', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail', @level2type = N'COLUMN', @level2name = N'AccountReceivableId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado en el que se encuentra el proceso juridico:  0 = No se encuentra en proceso  1 = Admisión de demanda  2 = Mandamiento de pago  3 = Audiencia previa de conciliación  4 = Pruebas  5 = Alegato de conciliación  6 = Sentencia 1ra instancia  7 = Reposición y aceptación  8 = Sentencia 2da instancia', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail', @level2type = N'COLUMN', @level2name = N'JudicialRecoveryStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor que indica si la factura se encuentra en cobro juridico.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail', @level2type = N'COLUMN', @level2name = N'InvoiceJudicialRecovery';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Saldo pendiente de cancelación de la factura o recobro. Debe ser igual al valor de la factura menos la glosa aceptada y menos los pagos aplicados. El formato del campo permite 2 decimales y separador decimal es el punto.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail', @level2type = N'COLUMN', @level2name = N'InvoiceBalance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor que indica si la glosa fue respondida.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail', @level2type = N'COLUMN', @level2name = N'ObjectionWithAnswer';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor glosa aceptada de la factura o recobro. El formato del campo permite 2 decimales y el separador decimal es el punto.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail', @level2type = N'COLUMN', @level2name = N'ObjectionValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Sumatoria de pagos realizados a esta factura. El formato del campo permite 2 decimales opcionales y el separador debe ser el punto.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail', @level2type = N'COLUMN', @level2name = N'TotalValuePayments';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha en la que se devolvió la factura. Si no hubo devolución se deja en blanco', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail', @level2type = N'COLUMN', @level2name = N'DevolutionDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha en la que se radicó la factura ante la entidad responsable de pago.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail', @level2type = N'COLUMN', @level2name = N'RadicateDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha en la cual se emitió la factura por parte de la IPS o recobro por parte de la EPS-S.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail', @level2type = N'COLUMN', @level2name = N'InvoiceDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de la factura o recobro a cancelar por la entidad responsable de pago. El formato del campo permite 2 decimales opcionales y el separador decimal es el punto.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail', @level2type = N'COLUMN', @level2name = N'InvoiceValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indicador de actualización para saber si la factura se reporta por primera vez, se actualiza o se elimina.  I: Ingreso o recobro.  A: Actualiza o recobro.  E: Elimina la factura o recobro.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail', @level2type = N'COLUMN', @level2name = N'UpdateIndicator';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de la factura o recobro.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Prefijo de la factura o recobro. En caso de no tener se debe dejar en blanco.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail', @level2type = N'COLUMN', @level2name = N'InvoicePrefix';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'F: Si corresponde a una factura presentada por la IPS.  R: Si corresponde a un recobro presentado por una EPS-S.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail', @level2type = N'COLUMN', @level2name = N'PaymentType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Corresponde al número de identificación de la IPS o EPS-S. Número de Nit sin digito de verificación. En éste caso no se usa ningún caracter de relleno', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail', @level2type = N'COLUMN', @level2name = N'IdentificationNumberIPS_EPSS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'El único valor valido es NI (Nit)', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail', @level2type = N'COLUMN', @level2name = N'IdentificationTypeIPS_EPSS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Corresponde a la razon social de la ERP ante la cual se presento la factura o recobro', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail', @level2type = N'COLUMN', @level2name = N'NameERP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código ó número de identificación de la Enlidad que es Responsable de Pago de acuerdo al tipo del campo anterior, en este caso no se usa ningun carácter de relleno.   - Para el caso de NI, este campo contiene el número de NIT de la entidad sin digito de verificación.   - Pera el caso de   MU (Municipio),  DE (Departamento),  DI (Dislrito),  este campo contiene el código Divipola del DANE del departamenlo o distrito o municipio que reporta.  Ejemplo   Para NI: 860120380   Para MU o DI:11001   Para DE: 25', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail', @level2type = N'COLUMN', @level2name = N'IdentificationNumberERP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Identificación de le entidad rasponsable de pago.   -Para el caso de entidades territoriales el tipo de identificación es   MU (Municipio)   DE (Departamento)   DI (Distnto).  Para las demás entidades el tipo de identificacion es  NI (Nit).', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail', @level2type = N'COLUMN', @level2name = N'IdentificationTypeERP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de la cabecera ', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail', @level2type = N'COLUMN', @level2name = N'Circular030Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail', @level2type = N'COLUMN', @level2name = N'Id';

