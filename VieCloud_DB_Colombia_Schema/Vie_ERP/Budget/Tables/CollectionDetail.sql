CREATE TABLE [Budget].[CollectionDetail] (
    [Id]                      INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CollectionId]            INT          NOT NULL,
    [RecognitionDetailId]     INT          NOT NULL,
    [CollectionType]          TINYINT      NOT NULL,
    [InitialValue]            NUMERIC (18) NOT NULL,
    [DebitModificationValue]  NUMERIC (18) NOT NULL,
    [CreditModificationValue] NUMERIC (18) NOT NULL,
    [Balance]                 NUMERIC (18) NOT NULL,
    CONSTRAINT [PK_CollectionDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CollectionDetail_Collection] FOREIGN KEY ([CollectionId]) REFERENCES [Budget].[Collection] ([Id]),
    CONSTRAINT [FK_CollectionDetail_RecognitionDetail] FOREIGN KEY ([RecognitionDetailId]) REFERENCES [Budget].[RecognitionDetail] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Saldo resultante del recaudo calculado como: Valor Inicial menos Débitos más Créditos (InitialValue - DebitModificationValue + CreditModificationValue). Refleja el monto disponible o pendiente en la línea de recaudación, glosa o facturación.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CollectionDetail', @level2type = N'COLUMN', @level2name = N'Balance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el saldo del recaudo el cual se calcula asi (InitialValue - DebitModificationValue + CreditModificationValue)', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CollectionDetail', @level2type = N'COLUMN', @level2name = N'Balance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CollectionDetail', @level2type = N'COLUMN', @level2name = N'Balance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de modificación por crédito aplicado al rubro de recaudo. Representa abonos, devoluciones, ajustes positivos o acreditaciones en facturación, glosa o ingresos de la institución de salud.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CollectionDetail', @level2type = N'COLUMN', @level2name = N'CreditModificationValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Modificacion credito al Rubro', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CollectionDetail', @level2type = N'COLUMN', @level2name = N'CreditModificationValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CollectionDetail', @level2type = N'COLUMN', @level2name = N'CreditModificationValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de modificación por débito aplicado al rubro de recaudo. Representa descuentos, retenciones, ajustes negativos o castigos en facturación, glosa o ingresos de la institución de salud.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CollectionDetail', @level2type = N'COLUMN', @level2name = N'DebitModificationValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor De modificacion debito al rubro', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CollectionDetail', @level2type = N'COLUMN', @level2name = N'DebitModificationValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CollectionDetail', @level2type = N'COLUMN', @level2name = N'DebitModificationValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor inicial o monto base del recaudo antes de cualquier modificación, débito o crédito. Corresponde al importe originalmente reconocido en facturación, RIPS, contrato o ingreso de paciente.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CollectionDetail', @level2type = N'COLUMN', @level2name = N'InitialValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Inicial', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CollectionDetail', @level2type = N'COLUMN', @level2name = N'InitialValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CollectionDetail', @level2type = N'COLUMN', @level2name = N'InitialValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo o medio de recaudo: 1=Efectivo (dinero en caja), 2=Papeles (cheques, giros, documentos de pago), 3=Otras (transferencias, depósitos, pagos electrónicos). Categoriza la forma de cobro o ingreso de fondos.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CollectionDetail', @level2type = N'COLUMN', @level2name = N'CollectionType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de recaudo  1 = Efectivo, 2 =Papeles, 3 = Otras', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CollectionDetail', @level2type = N'COLUMN', @level2name = N'CollectionType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CollectionDetail', @level2type = N'COLUMN', @level2name = N'CollectionType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del detalle de reconocimiento de ingresos o factura que será modificado. Referencia a la línea específica de ingreso, procedimiento, atención o servicio sanitario que se ajusta.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CollectionDetail', @level2type = N'COLUMN', @level2name = N'RecognitionDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle del reconocimiento que se va a modificar', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CollectionDetail', @level2type = N'COLUMN', @level2name = N'RecognitionDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CollectionDetail', @level2type = N'COLUMN', @level2name = N'RecognitionDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cabecera o encabezado del recaudo. Agrupa los detalles de cobro, facturación o ingreso asociados a un movimiento de caja, contrato o período de facturación.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CollectionDetail', @level2type = N'COLUMN', @level2name = N'CollectionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Cabecera del recaudo', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CollectionDetail', @level2type = N'COLUMN', @level2name = N'CollectionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CollectionDetail', @level2type = N'COLUMN', @level2name = N'CollectionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (IDENTITY) del detalle de recaudo. Clave primaria que identifica cada línea de movimiento en la tabla de detalles de recaudación, facturación y glosas.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CollectionDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CollectionDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CollectionDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de los recaudos o cobros del presupuesto: registra, por cada movimiento de recaudo, el valor inicial, los ajustes (débitos y créditos) y el saldo resultante, vinculando cada línea con el encabezado de recaudo y el detalle de reconocimiento de ingreso correspondiente.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CollectionDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CollectionDetail';
