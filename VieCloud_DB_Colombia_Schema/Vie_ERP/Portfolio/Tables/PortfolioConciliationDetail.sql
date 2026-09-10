CREATE TABLE [Portfolio].[PortfolioConciliationDetail] (
    [Id]                              INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PortfolioConciliationId]         INT             NOT NULL,
    [InvoiceId]                       INT             NOT NULL,
    [AccountReceivableDate]           DATETIME        NOT NULL,
    [RadicatedNumber]                 INT             NOT NULL,
    [RadicatedDate]                   DATETIME        NOT NULL,
    [Balance]                         DECIMAL (20, 2) NOT NULL,
    [ValueGlosado]                    DECIMAL (20, 2) NOT NULL,
    [ValueEntity]                     DECIMAL (20, 2) NOT NULL,
    [PortfolioStatus]                 TINYINT         NOT NULL,
    [ValueAcceptedFirstInstance]      DECIMAL (20, 2) NOT NULL,
    [ValueAcceptedSecondInstance]     DECIMAL (20, 2) NOT NULL,
    [ValueGlosadoConciliation]        DECIMAL (20, 2) NOT NULL,
    [BalanceConciliation]             DECIMAL (20, 2) NOT NULL,
    [StateConciliation]               BIT             NOT NULL,
    [StatePortfolioConciliation]      TINYINT         NOT NULL,
    [Comment]                         VARCHAR (100)   NULL,
    [PortfolioConciliationConceptsId] INT             NULL,
    [PortfolioDifferenceConciliation] DECIMAL (20, 2) NULL,
    CONSTRAINT [PK_PortfolioConciliationDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PortfolioConciliationDetail_ConciliationConcepts] FOREIGN KEY ([PortfolioConciliationConceptsId]) REFERENCES [Portfolio].[PortfolioConciliationConcepts] ([Id]),
    CONSTRAINT [FK_PortfolioConciliationDetail_Invoice] FOREIGN KEY ([InvoiceId]) REFERENCES [Billing].[Invoice] ([Id]),
    CONSTRAINT [FK_PortfolioConciliationDetail_PortfolioConciliationId] FOREIGN KEY ([PortfolioConciliationId]) REFERENCES [Portfolio].[PortfolioConciliation] ([Id])
);




GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Diferencia o variación del saldo de la factura en la conciliación (DECIMAL 20,2, nullable). Brecha entre valor facturado y valor conciliado.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioConciliationDetail', @level2type = N'COLUMN', @level2name = N'PortfolioDifferenceConciliation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Diferencia saldo factura', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioConciliationDetail', @level2type = N'COLUMN', @level2name = N'PortfolioDifferenceConciliation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioConciliationDetail', @level2type = N'COLUMN', @level2name = N'PortfolioDifferenceConciliation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de relación (FK, nullable) con la tabla PortfolioConciliationConcepts. Vincula el detalle a un concepto o motivo de glosa/ajuste.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioConciliationDetail', @level2type = N'COLUMN', @level2name = N'PortfolioConciliationConceptsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la relacion con la tabla PortfolioConciliationConcepts', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioConciliationDetail', @level2type = N'COLUMN', @level2name = N'PortfolioConciliationConceptsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioConciliationDetail', @level2type = N'COLUMN', @level2name = N'PortfolioConciliationConceptsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observación, nota o comentario adicional sobre el detalle de conciliación (VARCHAR 100, nullable). Espacio libre para anotaciones del proceso.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioConciliationDetail', @level2type = N'COLUMN', @level2name = N'Comment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observacion', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioConciliationDetail', @level2type = N'COLUMN', @level2name = N'Comment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioConciliationDetail', @level2type = N'COLUMN', @level2name = N'Comment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de cartera en conciliación (TINYINT): 1=Sin Radicar, 2=Radicada sin Confirmar, 3=Radicada Entidad, 7=Certificada Parcial, 8=Certificada Total, 14=Devolución Factura, 15=Cuenta de Difícil Recaudo, 16=Cobro Jurídico.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioConciliationDetail', @level2type = N'COLUMN', @level2name = N'StatePortfolioConciliation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'      1 - Sin Radicar      2 - Radicada sin Confirmar      3 - Radicada Entidad      7 - Certificada Parcial      8 - Certificada Total      14 - Devolución Factura      15 - Cuenta de Dificil Recaudo      16 - Cobro Jurídico', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioConciliationDetail', @level2type = N'COLUMN', @level2name = N'StatePortfolioConciliation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioConciliationDetail', @level2type = N'COLUMN', @level2name = N'StatePortfolioConciliation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de conciliación: 0=Sin conciliar, 1=Conciliada (BIT). Indica si el detalle ha sido reconciliado entre proveedor y entidad.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioConciliationDetail', @level2type = N'COLUMN', @level2name = N'StateConciliation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N' 0- sin conciliar                   1- conciliada', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioConciliationDetail', @level2type = N'COLUMN', @level2name = N'StateConciliation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioConciliationDetail', @level2type = N'COLUMN', @level2name = N'StateConciliation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Saldo de la factura reconocido por el cliente en la conciliación (DECIMAL 20,2). Monto que ambas partes acuerdan como pendiente.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioConciliationDetail', @level2type = N'COLUMN', @level2name = N'BalanceConciliation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Saldo de la factura que tiene pore lado del cliente', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioConciliationDetail', @level2type = N'COLUMN', @level2name = N'BalanceConciliation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioConciliationDetail', @level2type = N'COLUMN', @level2name = N'BalanceConciliation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor glosado del cliente en la conciliación (DECIMAL 20,2). Monto en desacuerdo reconocido por la institución en el proceso de reconciliación.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioConciliationDetail', @level2type = N'COLUMN', @level2name = N'ValueGlosadoConciliation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'valor glosado del cliente', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioConciliationDetail', @level2type = N'COLUMN', @level2name = N'ValueGlosadoConciliation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioConciliationDetail', @level2type = N'COLUMN', @level2name = N'ValueGlosadoConciliation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor aceptado o aprobado en la segunda instancia de revisión (DECIMAL 20,2). Monto confirmado tras apelación o revisión adicional.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioConciliationDetail', @level2type = N'COLUMN', @level2name = N'ValueAcceptedSecondInstance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'valor aceptado segunda instancia', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioConciliationDetail', @level2type = N'COLUMN', @level2name = N'ValueAcceptedSecondInstance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioConciliationDetail', @level2type = N'COLUMN', @level2name = N'ValueAcceptedSecondInstance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor aceptado o aprobado en la primera instancia de revisión (DECIMAL 20,2). Monto que supera la primera validación administrativa.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioConciliationDetail', @level2type = N'COLUMN', @level2name = N'ValueAcceptedFirstInstance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el valor asignado en la primera instancia.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioConciliationDetail', @level2type = N'COLUMN', @level2name = N'ValueAcceptedFirstInstance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioConciliationDetail', @level2type = N'COLUMN', @level2name = N'ValueAcceptedFirstInstance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de cartera registrado en la entidad (TINYINT). Código numérico que refleja el status actual del documento en el tercero pagador.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioConciliationDetail', @level2type = N'COLUMN', @level2name = N'PortfolioStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'estado de cartera registrado en la entidad', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioConciliationDetail', @level2type = N'COLUMN', @level2name = N'PortfolioStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioConciliationDetail', @level2type = N'COLUMN', @level2name = N'PortfolioStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de la factura reconocido o pagado por la entidad (DECIMAL 20,2). Monto que la aseguradora/pagador acepta.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioConciliationDetail', @level2type = N'COLUMN', @level2name = N'ValueEntity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'valor factura entidad', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioConciliationDetail', @level2type = N'COLUMN', @level2name = N'ValueEntity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioConciliationDetail', @level2type = N'COLUMN', @level2name = N'ValueEntity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor total glosado (rechazado) al corte del período (DECIMAL 20,2). Monto de la factura impugnado por la entidad.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioConciliationDetail', @level2type = N'COLUMN', @level2name = N'ValueGlosado';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'valor glosado al corte', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioConciliationDetail', @level2type = N'COLUMN', @level2name = N'ValueGlosado';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioConciliationDetail', @level2type = N'COLUMN', @level2name = N'ValueGlosado';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Saldo vigente de la factura al corte (DECIMAL 20,2). Monto adeudado sin descontar glosas o pagos parciales.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioConciliationDetail', @level2type = N'COLUMN', @level2name = N'Balance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'saldo al corte de la factura', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioConciliationDetail', @level2type = N'COLUMN', @level2name = N'Balance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioConciliationDetail', @level2type = N'COLUMN', @level2name = N'Balance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de radicación o presentación formal ante la entidad (DATETIME). Marca el inicio del proceso de cobro o glosa.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioConciliationDetail', @level2type = N'COLUMN', @level2name = N'RadicatedDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'fecha de radicado', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioConciliationDetail', @level2type = N'COLUMN', @level2name = N'RadicatedDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioConciliationDetail', @level2type = N'COLUMN', @level2name = N'RadicatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de radicado o radicación ante la entidad (INT). Identificador del trámite presentado ante el tercero pagador.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioConciliationDetail', @level2type = N'COLUMN', @level2name = N'RadicatedNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de radicado', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioConciliationDetail', @level2type = N'COLUMN', @level2name = N'RadicatedNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioConciliationDetail', @level2type = N'COLUMN', @level2name = N'RadicatedNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de registro o corte de la cuenta por cobrar (DATETIME). Marca el período de reconocimiento de la deuda.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioConciliationDetail', @level2type = N'COLUMN', @level2name = N'AccountReceivableDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de la cuenta por Cobrar', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioConciliationDetail', @level2type = N'COLUMN', @level2name = N'AccountReceivableDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioConciliationDetail', @level2type = N'COLUMN', @level2name = N'AccountReceivableDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de relación (FK) con la factura o saldo de factura (tabla Billing.Invoice). Vincula el detalle al documento de cobro/facturación.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioConciliationDetail', @level2type = N'COLUMN', @level2name = N'InvoiceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de relacion con saldos de facturas', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioConciliationDetail', @level2type = N'COLUMN', @level2name = N'InvoiceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioConciliationDetail', @level2type = N'COLUMN', @level2name = N'InvoiceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de relación (FK) con la cabecera de conciliación de cartera (tabla PortfolioConciliation). Agrupa detalles bajo una conciliación padre.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioConciliationDetail', @level2type = N'COLUMN', @level2name = N'PortfolioConciliationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de relacion con la cabecera de conciliacion', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioConciliationDetail', @level2type = N'COLUMN', @level2name = N'PortfolioConciliationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioConciliationDetail', @level2type = N'COLUMN', @level2name = N'PortfolioConciliationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico (INT IDENTITY) del detalle de conciliación de cartera. Clave primaria de la tabla PortfolioConciliationDetail.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioConciliationDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de detalle de conciliacion', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioConciliationDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioConciliationDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de cada factura incluida en un proceso de conciliación de cartera con entidades pagadoras. Registra los valores radicados, glosados, aceptados en primera y segunda instancia, saldos y el estado de la conciliación para el seguimiento y cierre de cuentas por cobrar.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioConciliationDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioConciliationDetail';
