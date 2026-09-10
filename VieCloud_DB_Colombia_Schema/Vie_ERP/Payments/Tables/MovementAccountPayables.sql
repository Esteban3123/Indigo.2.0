CREATE TABLE [Payments].[MovementAccountPayables] (
    [Id]                    INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdAccountPayable]      INT             NOT NULL,
    [IdAccountPayableShare] INT             NOT NULL,
    [EntityId]              INT             NOT NULL,
    [EntityCode]            VARCHAR (20)    NOT NULL,
    [EntityName]            VARCHAR (250)   NOT NULL,
    [MovementDate]          DATETIME        NOT NULL,
    [Value]                 DECIMAL (18, 2) NOT NULL,
    [TimeStamp]             ROWVERSION      NOT NULL,
    CONSTRAINT [PK_MovementAccountPayables] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MovementAccountPayables_AccountPayable] FOREIGN KEY ([IdAccountPayable]) REFERENCES [Payments].[AccountPayable] ([Id]),
    CONSTRAINT [FK_MovementAccountPayables_AccountPayableShares] FOREIGN KEY ([IdAccountPayableShare]) REFERENCES [Payments].[AccountPayableShares] ([Id])
);




GO





GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal (TIMESTAMP) del evento de movimiento: instante de creación, registro o modificación del movimiento de cuenta por pagar. Auditoría temporal.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'MovementAccountPayables', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'MovementAccountPayables', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'MovementAccountPayables', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor monetario (DECIMAL 18,2) del movimiento de cuenta por pagar. Monto de la transacción, pago, abono o ajuste.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'MovementAccountPayables', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'MovementAccountPayables', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'MovementAccountPayables', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) del movimiento de la cuenta por pagar. Fecha de transacción, pago, glosa, ajuste o evento de factura.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'MovementAccountPayables', @level2type = N'COLUMN', @level2name = N'MovementDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Movimiento', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'MovementAccountPayables', @level2type = N'COLUMN', @level2name = N'MovementDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'MovementAccountPayables', @level2type = N'COLUMN', @level2name = N'MovementDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre (VARCHAR 250) de la entidad que genera el documento de cuenta por pagar: proveedor, IPS, clínica, hospital, centro de atención u organización acreedor.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'MovementAccountPayables', @level2type = N'COLUMN', @level2name = N'EntityName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la entidad quien genera el documento', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'MovementAccountPayables', @level2type = N'COLUMN', @level2name = N'EntityName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'MovementAccountPayables', @level2type = N'COLUMN', @level2name = N'EntityName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador (VARCHAR 20) de la entidad generadora: código proveedor, código IPS, NIT o identificación de la institución de salud acreedora.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'MovementAccountPayables', @level2type = N'COLUMN', @level2name = N'EntityCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de la entidad quien genera el documento', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'MovementAccountPayables', @level2type = N'COLUMN', @level2name = N'EntityCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'MovementAccountPayables', @level2type = N'COLUMN', @level2name = N'EntityCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT) de la entidad que genera el documento: proveedor, IPS, clínica, hospital o centro de atención acreedor en el sistema.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'MovementAccountPayables', @level2type = N'COLUMN', @level2name = N'EntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la entidad quien genera el documento', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'MovementAccountPayables', @level2type = N'COLUMN', @level2name = N'EntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'MovementAccountPayables', @level2type = N'COLUMN', @level2name = N'EntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) de la cuota o cuotificación de la cuenta por pagar. Referencia a la distribución de pago.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'MovementAccountPayables', @level2type = N'COLUMN', @level2name = N'IdAccountPayableShare';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuota', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'MovementAccountPayables', @level2type = N'COLUMN', @level2name = N'IdAccountPayableShare';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'MovementAccountPayables', @level2type = N'COLUMN', @level2name = N'IdAccountPayableShare';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT, FK) de la cuenta por pagar matriz. Referencia a factura, ingreso, atención, urgencia o procedimiento acreedor.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'MovementAccountPayables', @level2type = N'COLUMN', @level2name = N'IdAccountPayable';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta por pagar', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'MovementAccountPayables', @level2type = N'COLUMN', @level2name = N'IdAccountPayable';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'MovementAccountPayables', @level2type = N'COLUMN', @level2name = N'IdAccountPayable';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) del registro de movimiento en la tabla MovementAccountPayables. Clave primaria del evento de transacción.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'MovementAccountPayables', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'MovementAccountPayables', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'MovementAccountPayables', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra los movimientos o transacciones asociados a las cuentas por pagar y sus cuotas, indicando la entidad involucrada (proveedor, aseguradora u otro tercero), la fecha del movimiento y el valor monetario aplicado. Permite hacer seguimiento del historial de pagos, abonos o ajustes sobre obligaciones financieras pendientes.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'MovementAccountPayables';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'MovementAccountPayables';
