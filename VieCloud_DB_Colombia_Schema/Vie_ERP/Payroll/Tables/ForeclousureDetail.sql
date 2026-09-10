CREATE TABLE [Payroll].[ForeclousureDetail] (
    [Id]             INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdForeclousure] INT           NOT NULL,
    [ShareValuePaid] NUMERIC (18)  NOT NULL,
    [DatePayment]    DATETIME      NOT NULL,
    [TypePayment]    TINYINT       NOT NULL,
    [StateShare]     VARCHAR (250) NOT NULL,
    [Timestamp]      ROWVERSION    NOT NULL,
    CONSTRAINT [PK_ForeclousureDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ForeclousureDetail_Foreclousure] FOREIGN KEY ([IdForeclousure]) REFERENCES [Payroll].[Foreclousure] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal DATETIME (ROWVERSION) del evento de embargo: captura automáticamente el instante de creación, registro o modificación del detalle de embargo en la base de datos.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ForeclousureDetail', @level2type = N'COLUMN', @level2name = N'Timestamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ForeclousureDetail', @level2type = N'COLUMN', @level2name = N'Timestamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ForeclousureDetail', @level2type = N'COLUMN', @level2name = N'Timestamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado VARCHAR(250) de la cuota o abono en embargo: refleja la condición actual del pago (ej: pendiente, pagado, anulado, rechazado).', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ForeclousureDetail', @level2type = N'COLUMN', @level2name = N'StateShare';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ForeclousureDetail', @level2type = N'COLUMN', @level2name = N'StateShare';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ForeclousureDetail', @level2type = N'COLUMN', @level2name = N'StateShare';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de pago TINYINT del abono en embargo: 1=Por Nómina, 2=Manual, 3=Por archivo, 4=Por Vacaciones, 5=Por Cesantías, 6=Por Primas 1er Semestre, 7=Por Primas 2do Semestre.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ForeclousureDetail', @level2type = N'COLUMN', @level2name = N'TypePayment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Pago:   1. Por Nomina  2. Manual  3. Por archivo  4. Por Vacaciones  5. Por Cesantias  6. Por Primas 1er Semestre  7. Por Primas 2do Semestre', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ForeclousureDetail', @level2type = N'COLUMN', @level2name = N'TypePayment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ForeclousureDetail', @level2type = N'COLUMN', @level2name = N'TypePayment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha DATETIME del pago o abono realizado contra el embargo, cuando se acreditó la cuota descontada.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ForeclousureDetail', @level2type = N'COLUMN', @level2name = N'DatePayment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Pago', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ForeclousureDetail', @level2type = N'COLUMN', @level2name = N'DatePayment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ForeclousureDetail', @level2type = N'COLUMN', @level2name = N'DatePayment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor NUMERIC(18) de la cuota o abono pagado en descuento por embargo (monto numérico sin decimales implícitos).', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ForeclousureDetail', @level2type = N'COLUMN', @level2name = N'ShareValuePaid';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de la Cuota', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ForeclousureDetail', @level2type = N'COLUMN', @level2name = N'ShareValuePaid';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ForeclousureDetail', @level2type = N'COLUMN', @level2name = N'ShareValuePaid';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador INT FK que referencia el encabezado del embargo en [Payroll].[Foreclousure], vinculando este detalle de pago a su embargo padre.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ForeclousureDetail', @level2type = N'COLUMN', @level2name = N'IdForeclousure';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Cabecera Embargos', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ForeclousureDetail', @level2type = N'COLUMN', @level2name = N'IdForeclousure';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ForeclousureDetail', @level2type = N'COLUMN', @level2name = N'IdForeclousure';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador INT IDENTITY autoincremental (1,1) y clave primaria clustered de cada línea de detalle de pago en embargo.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ForeclousureDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ForeclousureDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ForeclousureDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro del detalle de pagos de embargos o descuentos de nómina: cada fila representa una cuota o abono realizado sobre un embargo activo, indicando el valor pagado, la fecha, el tipo de pago y el estado de la cuota.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ForeclousureDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ForeclousureDetail';
