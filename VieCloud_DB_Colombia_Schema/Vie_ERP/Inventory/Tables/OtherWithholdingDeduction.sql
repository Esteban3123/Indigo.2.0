CREATE TABLE [Inventory].[OtherWithholdingDeduction] (
    [Id]                      INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                    VARCHAR (20) NOT NULL,
    [Type]                    TINYINT      NOT NULL,
    [AccountPayableConceptId] INT          NULL,
    [RoundType]               INT          NULL,
    [DeductionValue]          NUMERIC (18) NULL,
    [Status]                  BIT          NOT NULL,
    [CreationUser]            VARCHAR (20) NOT NULL,
    [CreationDate]            DATETIME     NOT NULL,
    [ModificationUser]        VARCHAR (20) NULL,
    [ModificationDate]        DATETIME     NULL,
    [TimeStamp]               ROWVERSION   NOT NULL,
    CONSTRAINT [PK_OtherWithholdingDeduction__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_OtherWithholdingDeduction_AccountPayableConcepts] FOREIGN KEY ([AccountPayableConceptId]) REFERENCES [Payments].[AccountPayableConcepts] ([Id])
);




GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_OtherWithholdingDeduction__Code]
    ON [Inventory].[OtherWithholdingDeduction]([Code] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Versión binaria de marca temporal (TIMESTAMP). Registro automático de instante de creación, modificación o evento en la retención/deducción para control de concurrencia.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'OtherWithholdingDeduction', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'OtherWithholdingDeduction', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'OtherWithholdingDeduction', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de última modificación (DATETIME, nullable). Marca temporal de cambios en parámetros de retención o deducción.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'OtherWithholdingDeduction', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'OtherWithholdingDeduction', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'OtherWithholdingDeduction', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que modificó el registro (VARCHAR 20, nullable). Auditoría de última actualización de retención o deducción.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'OtherWithholdingDeduction', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'OtherWithholdingDeduction', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'OtherWithholdingDeduction', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación (DATETIME). Marca temporal de instantaneidad del registro de retención o deducción en el sistema.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'OtherWithholdingDeduction', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'OtherWithholdingDeduction', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'OtherWithholdingDeduction', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó el registro (VARCHAR 20). Auditoría de origen del registro de retención o deducción.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'OtherWithholdingDeduction', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'OtherWithholdingDeduction', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'OtherWithholdingDeduction', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado activo/inactivo (BIT): 1=Activo, 0=Inactivo. Controla si la retención/deducción está vigente en operaciones de facturación y RIPS.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'OtherWithholdingDeduction', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del manual tarifario  1 - Activo  0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'OtherWithholdingDeduction', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'OtherWithholdingDeduction', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor numérico (NUMERIC 18) de deducción mostrado cuando Type=2. Monto retenido o deducido en la factura, recibo o pago.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'OtherWithholdingDeduction', @level2type = N'COLUMN', @level2name = N'DeductionValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de la deduccion, esta se muestra si el tipo es deduccion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'OtherWithholdingDeduction', @level2type = N'COLUMN', @level2name = N'DeductionValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'OtherWithholdingDeduction', @level2type = N'COLUMN', @level2name = N'DeductionValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de redondeo aplicado solo en retenciones (INT): 10=Décima, 100=Centésima, 1000=Milésima. Fórmula: ROUND(valor/redondeo)*redondeo.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'OtherWithholdingDeduction', @level2type = N'COLUMN', @level2name = N'RoundType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Este se muestra solo si es de tipo retencion  Especifica como se va redondear los valores   10 -- Decima  100 -- Centecima  1000 -- Milesima    La formula que se usa es Round(valor / redondeo) * redondeo  Round(2005/ 10) * 10', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'OtherWithholdingDeduction', @level2type = N'COLUMN', @level2name = N'RoundType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'OtherWithholdingDeduction', @level2type = N'COLUMN', @level2name = N'RoundType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID del concepto de cuentas por pagar (FK a Payments.AccountPayableConcepts). Si Retención: concepto específico con manejo de retención. Si Deducción: concepto específico sin manejo de retención.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'OtherWithholdingDeduction', @level2type = N'COLUMN', @level2name = N'AccountPayableConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Id del concepto por pagar    Si el tipo es Retencion el concepto de la cuenta por pagar que seleccionen debe ser de tipo especifico y debe Manejar Retencion    Si el tipo es de Deduccion el concepto de la cuenta por pagar debe de ser de tipo Especifico y NO Debe Manejar Retencion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'OtherWithholdingDeduction', @level2type = N'COLUMN', @level2name = N'AccountPayableConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'OtherWithholdingDeduction', @level2type = N'COLUMN', @level2name = N'AccountPayableConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de registro (TINYINT): 1=Retención, 2=Deducción. Determina comportamiento, validación de concepto de cuentas por pagar y aplicación de redondeo.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'OtherWithholdingDeduction', @level2type = N'COLUMN', @level2name = N'Type';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo del registro  1 - Retención  2 - Deducción', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'OtherWithholdingDeduction', @level2type = N'COLUMN', @level2name = N'Type';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'OtherWithholdingDeduction', @level2type = N'COLUMN', @level2name = N'Type';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código alfanumérico (VARCHAR 20) único de retención o deducción. Identificador legible para búsqueda y referencia en procesos de facturación y glosa.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'OtherWithholdingDeduction', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la deduccion o retencion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'OtherWithholdingDeduction', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'OtherWithholdingDeduction', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT, PK) de retención o deducción. Generado automáticamente por identidad. Clave primaria de la tabla.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'OtherWithholdingDeduction', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la retencion o deduccion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'OtherWithholdingDeduction', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'OtherWithholdingDeduction', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra las deducciones y retenciones adicionales (otras retenciones) aplicables en el módulo de inventario, como descuentos, retenciones especiales o conceptos de cartera por pagar, incluyendo el tipo de deducción, el valor a retener, la cuenta contable asociada y el tipo de redondeo.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'OtherWithholdingDeduction';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'OtherWithholdingDeduction';
