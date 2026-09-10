CREATE TABLE [Payroll].[AgreementsD] (
    [Id]             INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AgreementsCId]  INT             NOT NULL,
    [ShareValuePaid] DECIMAL (18, 2) NOT NULL,
    [DatePayment]    DATETIME        NOT NULL,
    [TypePayment]    TINYINT         NOT NULL,
    [StateShare]     VARCHAR (250)   NOT NULL,
    [TimeStamp]      ROWVERSION      NOT NULL,
    CONSTRAINT [PK_AgreementsD__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AgreementsD_AgreementsC] FOREIGN KEY ([AgreementsCId]) REFERENCES [Payroll].[AgreementsC] ([Id])
);




GO



GO
CREATE NONCLUSTERED INDEX [IX_AgreementsD__AgreementsCId]
    ON [Payroll].[AgreementsD]([AgreementsCId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal (TIMESTAMP) de auditoría: registra el instante exacto de creación, modificación o cambio de estado del registro de pago; trazabilidad automática de eventos.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsD', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsD', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsD', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado o condición actual de la cuota pagada (VARCHAR 250): pendiente, pagada, glosa, anulada u otro estado de gestión de pago en el convenio.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsD', @level2type = N'COLUMN', @level2name = N'StateShare';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de la Cuota', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsD', @level2type = N'COLUMN', @level2name = N'StateShare';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsD', @level2type = N'COLUMN', @level2name = N'StateShare';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo o modalidad de pago (TINYINT): 1=Por Nómina, 2=Manual, 3=Por archivo, 4=Por Vacaciones, 5=Liquidación de Contrato; clasificación del origen o método del desembolso.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsD', @level2type = N'COLUMN', @level2name = N'TypePayment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Pago:   1. Por Nomina  2. Manual  3. Por archivo  4. Por Vacaciones  5. Liquidación de Contrato', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsD', @level2type = N'COLUMN', @level2name = N'TypePayment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsD', @level2type = N'COLUMN', @level2name = N'TypePayment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) en que se efectuó o registró el pago de la cuota; fecha de desembolso o acreditación de recursos.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsD', @level2type = N'COLUMN', @level2name = N'DatePayment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Pago', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsD', @level2type = N'COLUMN', @level2name = N'DatePayment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsD', @level2type = N'COLUMN', @level2name = N'DatePayment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor monetario (DECIMAL 18,2) de la cuota o cuotas pagadas en la transacción; monto efectivamente desembolsado al profesional o tercero según el convenio.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsD', @level2type = N'COLUMN', @level2name = N'ShareValuePaid';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Cuota Pagada', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsD', @level2type = N'COLUMN', @level2name = N'ShareValuePaid';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsD', @level2type = N'COLUMN', @level2name = N'ShareValuePaid';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de referencia (FK) al encabezado maestro del convenio o acuerdo laboral en tabla AgreementsC; relaciona cada cuota pagada con su acuerdo padre.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsD', @level2type = N'COLUMN', @level2name = N'AgreementsCId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera del convenio', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsD', @level2type = N'COLUMN', @level2name = N'AgreementsCId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsD', @level2type = N'COLUMN', @level2name = N'AgreementsCId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autonumérico único (INT IDENTITY) de cada registro de detalle de pago de cuota en acuerdos/convenios laborales.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsD', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsD', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsD', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de cuotas o pagos asociados a convenios o acuerdos de nómina. Almacena el detalle de cada pago de cuota realizado: monto, fecha, tipo y estado del pago.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsD';
