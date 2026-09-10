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



GO



GO
CREATE NONCLUSTERED INDEX [IX_Circular030Detail__Circular030Id__INC__DevolutionDate__IdentificationNumberERP__IdentificationNumberIPS_EPSS__IdentificationT]
    ON [Portfolio].[Circular030Detail]([Circular030Id] ASC)
    INCLUDE([IdentificationTypeERP], [IdentificationNumberERP], [NameERP], [IdentificationTypeIPS_EPSS], [IdentificationNumberIPS_EPSS], [PaymentType], [UpdateIndicator], [RadicateDate], [DevolutionDate], [TotalValuePayments], [ObjectionValue], [ObjectionWithAnswer], [JudicialRecoveryStatus], [InvoicePrefix], [InvoiceNumber], [InvoiceValue], [InvoiceDate], [InvoiceBalance], [InvoiceJudicialRecovery]);


GO
CREATE NONCLUSTERED INDEX [IX_Circular030Detail__Circular030Id__InvoicePrefix__InvoiceNumber]
    ON [Portfolio].[Circular030Detail]([Circular030Id] ASC, [InvoicePrefix] ASC, [InvoiceNumber] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cuenta por cobrar (FK); referencias a Portfolio.AccountReceivable. Asocia el detalle de factura a su registro de cartera.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail', @level2type = N'COLUMN', @level2name = N'AccountReceivableId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta por cobrar', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail', @level2type = N'COLUMN', @level2name = N'AccountReceivableId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail', @level2type = N'COLUMN', @level2name = N'AccountReceivableId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del proceso judicial (TINYINT 0-8): 0=No en proceso, 1=Admisión demanda, 2=Mandamiento pago, 3=Audiencia conciliación, 4=Pruebas, 5=Alegato, 6=Sentencia 1ra instancia, 7=Reposición, 8=Sentencia 2da instancia. Indica avance de recobro judicial.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail', @level2type = N'COLUMN', @level2name = N'JudicialRecoveryStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado en el que se encuentra el proceso juridico:  0 = No se encuentra en proceso  1 = Admisión de demanda  2 = Mandamiento de pago  3 = Audiencia previa de conciliación  4 = Pruebas  5 = Alegato de conciliación  6 = Sentencia 1ra instancia  7 = Reposición y aceptación  8 = Sentencia 2da instancia', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail', @level2type = N'COLUMN', @level2name = N'JudicialRecoveryStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail', @level2type = N'COLUMN', @level2name = N'JudicialRecoveryStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera (BIT) que indica si la factura o recobro está en proceso de cobro judicial. Valores: 0=No en cobro judicial, 1=Sí en cobro judicial.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail', @level2type = N'COLUMN', @level2name = N'InvoiceJudicialRecovery';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor que indica si la factura se encuentra en cobro juridico.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail', @level2type = N'COLUMN', @level2name = N'InvoiceJudicialRecovery';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail', @level2type = N'COLUMN', @level2name = N'InvoiceJudicialRecovery';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Saldo pendiente de cancelación (DECIMAL 18,2; separador punto). Cálculo: Valor factura - Glosa aceptada - Pagos aplicados. Representa deuda vigente de la factura o recobro.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail', @level2type = N'COLUMN', @level2name = N'InvoiceBalance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Saldo pendiente de cancelación de la factura o recobro. Debe ser igual al valor de la factura menos la glosa aceptada y menos los pagos aplicados. El formato del campo permite 2 decimales y separador decimal es el punto.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail', @level2type = N'COLUMN', @level2name = N'InvoiceBalance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail', @level2type = N'COLUMN', @level2name = N'InvoiceBalance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera (BIT) que indica si la glosa/objeción de la factura fue respondida. Valores: 0=Sin respuesta, 1=Con respuesta. Evidencia de tramitación.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail', @level2type = N'COLUMN', @level2name = N'ObjectionWithAnswer';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor que indica si la glosa fue respondida.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail', @level2type = N'COLUMN', @level2name = N'ObjectionWithAnswer';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail', @level2type = N'COLUMN', @level2name = N'ObjectionWithAnswer';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de glosa aceptada (DECIMAL 18,2; separador punto). Monto de la factura o recobro que fue objetado y aceptado por la entidad responsable de pago.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail', @level2type = N'COLUMN', @level2name = N'ObjectionValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor glosa aceptada de la factura o recobro. El formato del campo permite 2 decimales y el separador decimal es el punto.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail', @level2type = N'COLUMN', @level2name = N'ObjectionValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail', @level2type = N'COLUMN', @level2name = N'ObjectionValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sumatoria total de pagos realizados a esta factura o recobro (DECIMAL 18,2; separador punto). Acumulado de abonos aplicados.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail', @level2type = N'COLUMN', @level2name = N'TotalValuePayments';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Sumatoria de pagos realizados a esta factura. El formato del campo permite 2 decimales opcionales y el separador debe ser el punto.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail', @level2type = N'COLUMN', @level2name = N'TotalValuePayments';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail', @level2type = N'COLUMN', @level2name = N'TotalValuePayments';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha (DATE) en que se devolvió la factura a la IPS/EPS-S. NULL si no hubo devolución. Marca fin de trámite administrativo.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail', @level2type = N'COLUMN', @level2name = N'DevolutionDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha en la que se devolvió la factura. Si no hubo devolución se deja en blanco', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail', @level2type = N'COLUMN', @level2name = N'DevolutionDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail', @level2type = N'COLUMN', @level2name = N'DevolutionDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha (DATE) en que se radicó/presentó la factura ante la entidad responsable de pago (ERP). Inicia proceso de pago.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail', @level2type = N'COLUMN', @level2name = N'RadicateDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha en la que se radicó la factura ante la entidad responsable de pago.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail', @level2type = N'COLUMN', @level2name = N'RadicateDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail', @level2type = N'COLUMN', @level2name = N'RadicateDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha (DATE) de emisión de la factura (IPS) o recobro (EPS-S). Origen del documento de cobro.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail', @level2type = N'COLUMN', @level2name = N'InvoiceDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha en la cual se emitió la factura por parte de la IPS o recobro por parte de la EPS-S.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail', @level2type = N'COLUMN', @level2name = N'InvoiceDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail', @level2type = N'COLUMN', @level2name = N'InvoiceDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de la factura o recobro a cancelar (DECIMAL 18,2; separador punto). Monto total inicialmente cobrable por entidad responsable de pago.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail', @level2type = N'COLUMN', @level2name = N'InvoiceValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de la factura o recobro a cancelar por la entidad responsable de pago. El formato del campo permite 2 decimales opcionales y el separador decimal es el punto.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail', @level2type = N'COLUMN', @level2name = N'InvoiceValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail', @level2type = N'COLUMN', @level2name = N'InvoiceValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de movimiento (VARCHAR 1): I=Ingreso/nuevo, A=Actualización, E=Eliminación. Señala si se reporta por primera vez, se modifica o se cancela en el Circular030.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail', @level2type = N'COLUMN', @level2name = N'UpdateIndicator';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indicador de actualización para saber si la factura se reporta por primera vez, se actualiza o se elimina.  I: Ingreso o recobro.  A: Actualiza o recobro.  E: Elimina la factura o recobro.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail', @level2type = N'COLUMN', @level2name = N'UpdateIndicator';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail', @level2type = N'COLUMN', @level2name = N'UpdateIndicator';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número secuencial de la factura o recobro (VARCHAR 20). Identificador único del comprobante de pago dentro del prefijo.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de la factura o recobro.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Prefijo alfabético/numérico de la factura o recobro (VARCHAR 6). Vacío si no aplica. Complementa InvoiceNumber para identidad completa.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail', @level2type = N'COLUMN', @level2name = N'InvoicePrefix';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Prefijo de la factura o recobro. En caso de no tener se debe dejar en blanco.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail', @level2type = N'COLUMN', @level2name = N'InvoicePrefix';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail', @level2type = N'COLUMN', @level2name = N'InvoicePrefix';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de documento (VARCHAR 1): F=Factura (IPS), R=Recobro (EPS-S). Clasifica si es cobro directo o cobro de retorno.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail', @level2type = N'COLUMN', @level2name = N'PaymentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'F: Si corresponde a una factura presentada por la IPS.  R: Si corresponde a un recobro presentado por una EPS-S.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail', @level2type = N'COLUMN', @level2name = N'PaymentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail', @level2type = N'COLUMN', @level2name = N'PaymentType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'NIT de la IPS o EPS-S sin dígito verificador (VARCHAR 12). Identificación PII de la entidad prestadora o aseguradora. Usado para validar responsable clínico/asegurador.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail', @level2type = N'COLUMN', @level2name = N'IdentificationNumberIPS_EPSS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Corresponde al número de identificación de la IPS o EPS-S. Número de Nit sin digito de verificación. En éste caso no se usa ningún caracter de relleno', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail', @level2type = N'COLUMN', @level2name = N'IdentificationNumberIPS_EPSS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail', @level2type = N'COLUMN', @level2name = N'IdentificationNumberIPS_EPSS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de identificación de IPS/EPS-S (VARCHAR 2): siempre ''''NI'''' (Nit). Estandariza formato de identificación de proveedores de salud.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail', @level2type = N'COLUMN', @level2name = N'IdentificationTypeIPS_EPSS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'El único valor valido es NI (Nit)', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail', @level2type = N'COLUMN', @level2name = N'IdentificationTypeIPS_EPSS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail', @level2type = N'COLUMN', @level2name = N'IdentificationTypeIPS_EPSS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Razón social completa de la entidad ERP responsable de pago (VARCHAR 250). Nombre legal de municipio, departamento, distrito, aseguradora o entidad territorial.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail', @level2type = N'COLUMN', @level2name = N'NameERP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Corresponde a la razon social de la ERP ante la cual se presento la factura o recobro', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail', @level2type = N'COLUMN', @level2name = N'NameERP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail', @level2type = N'COLUMN', @level2name = N'NameERP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación de la entidad responsable pago (VARCHAR 25): NIT sin verificador, código Divipola DANE (municipio/departamento/distrito). Cédula PII de ente pagador.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail', @level2type = N'COLUMN', @level2name = N'IdentificationNumberERP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código ó número de identificación de la Enlidad que es Responsable de Pago de acuerdo al tipo del campo anterior, en este caso no se usa ningun carácter de relleno.   - Para el caso de NI, este campo contiene el número de NIT de la entidad sin digito de verificación.   - Pera el caso de   MU (Municipio),  DE (Departamento),  DI (Dislrito),  este campo contiene el código Divipola del DANE del departamenlo o distrito o municipio que reporta.  Ejemplo   Para NI: 860120380   Para MU o DI:11001   Para DE: 25', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail', @level2type = N'COLUMN', @level2name = N'IdentificationNumberERP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail', @level2type = N'COLUMN', @level2name = N'IdentificationNumberERP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo identificación responsable pago (VARCHAR 2): NI=Nit, MU=Municipio, DE=Departamento, DI=Distrito. Clasifica si es entidad territorial o privada.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail', @level2type = N'COLUMN', @level2name = N'IdentificationTypeERP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Identificación de le entidad rasponsable de pago.   -Para el caso de entidades territoriales el tipo de identificación es   MU (Municipio)   DE (Departamento)   DI (Distnto).  Para las demás entidades el tipo de identificacion es  NI (Nit).', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail', @level2type = N'COLUMN', @level2name = N'IdentificationTypeERP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail', @level2type = N'COLUMN', @level2name = N'IdentificationTypeERP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) de la cabecera Circular030 (FK). Agrupa detalles de facturación bajo circular normativa de reporte RIPS.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail', @level2type = N'COLUMN', @level2name = N'Circular030Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la cabecera ', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail', @level2type = N'COLUMN', @level2name = N'Circular030Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail', @level2type = N'COLUMN', @level2name = N'Circular030Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autonumérico (INT IDENTITY) del registro detalle. Clave primaria única de cada línea de factura en Circular030.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de la Circular 030 de cartera: registra cada factura reportada a EPS/aseguradoras con información del deudor (ERP e IPS/EPS), valores de factura, pagos, glosas, saldos pendientes y estado de cobro judicial, permitiendo hacer seguimiento de la cartera y cumplimiento del reporte normativo.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030Detail';
