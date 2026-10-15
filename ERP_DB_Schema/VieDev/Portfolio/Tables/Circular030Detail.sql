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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado en el que se encuentra el proceso juridico:  0 = No se encuentra en proceso  1 = Admisión de demanda  2 = Mandamiento de pago  3 = Audiencia previa de conciliación  4 = Pruebas  5 = Alegato de conciliación  6 = Sentencia 1ra instancia  7 = Reposición y aceptación  8 = Sentencia 2da instancia', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail', @level2type = N'COLUMN', @level2name = N'JudicialRecoveryStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indicador de actualización para saber si la factura se reporta por primera vez, se actualiza o se elimina.  I: Ingreso o recobro.  A: Actualiza o recobro.  E: Elimina la factura o recobro.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail', @level2type = N'COLUMN', @level2name = N'UpdateIndicator';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'F: Si corresponde a una factura presentada por la IPS.  R: Si corresponde a un recobro presentado por una EPS-S.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail', @level2type = N'COLUMN', @level2name = N'PaymentType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código ó número de identificación de la Enlidad que es Responsable de Pago de acuerdo al tipo del campo anterior, en este caso no se usa ningun carácter de relleno.   - Para el caso de NI, este campo contiene el número de NIT de la entidad sin digito de verificación.   - Pera el caso de   MU (Municipio),  DE (Departamento),  DI (Dislrito),  este campo contiene el código Divipola del DANE del departamenlo o distrito o municipio que reporta.  Ejemplo   Para NI: 860120380   Para MU o DI:11001   Para DE: 25', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail', @level2type = N'COLUMN', @level2name = N'IdentificationNumberERP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Identificación de le entidad rasponsable de pago.   -Para el caso de entidades territoriales el tipo de identificación es   MU (Municipio)   DE (Departamento)   DI (Distnto).  Para las demás entidades el tipo de identificacion es  NI (Nit).', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail', @level2type = N'COLUMN', @level2name = N'IdentificationTypeERP';

