CREATE TABLE [Payments].[DeferredCausation] (
    [Id]                INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdAccountPayable]  INT             NOT NULL,
    [BillNumber]        VARCHAR (20)    NULL,
    [IdMainAccount]     INT             NOT NULL,
    [PeriodsNumber]     INT             NOT NULL,
    [TypeDistribution]  TINYINT         NOT NULL,
    [InitialDate]       DATETIME        NOT NULL,
    [EndDate]           DATETIME        CONSTRAINT [DF_DeferredCausation_EndDate] DEFAULT (((8)/(1))/(2015)) NOT NULL,
    [IdThirdParty]      INT             NOT NULL,
    [IdCostCenter]      INT             NULL,
    [ValueCreditPeriod] DECIMAL (20, 2) NOT NULL,
    [Status]            TINYINT         NOT NULL,
    [CreationUser]      VARCHAR (20)    NOT NULL,
    [CreationDate]      DATETIME        NOT NULL,
    [ModificationUser]  VARCHAR (20)    NULL,
    [ModificationDate]  DATETIME        NULL,
    [ConfirmationUser]  VARCHAR (20)    NULL,
    [ConfirmationDate]  DATETIME        NULL,
    [AnnulmentUser]     VARCHAR (20)    NULL,
    [AnnulmentDate]     DATETIME        NULL,
    [TimeStamp]         ROWVERSION      NOT NULL,
    CONSTRAINT [PK_DeferredCausation] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DeferredCausation_AccountPayable] FOREIGN KEY ([IdAccountPayable]) REFERENCES [Payments].[AccountPayable] ([Id]),
    CONSTRAINT [FK_DeferredCausation_CostCenter] FOREIGN KEY ([IdCostCenter]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_DeferredCausation_MainAccounts] FOREIGN KEY ([IdMainAccount]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_DeferredCausation_ThirdParty] FOREIGN KEY ([IdThirdParty]) REFERENCES [Common].[ThirdParty] ([Id])
);




GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal de auditoría (TIMESTAMP). Registra automáticamente el instante exacto de creación, modificación o cambio de estado del registro de causación diferida.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausation', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausation', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausation', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de anulación (DATETIME). Momento en que se anuló o revocó el registro de causación diferida.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausation', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Anulación', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausation', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausation', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que anuló (VARCHAR 20). Identificación del usuario que ejecutó la anulación o revocación del registro.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausation', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Anulación', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausation', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausation', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de confirmación (DATETIME). Momento en que se confirmó o aprobó la causación diferida.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausation', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Confirmación', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausation', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausation', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que confirmó (VARCHAR 20). Identificación del usuario autorizador o aprobador del registro.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausation', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Confirmación', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausation', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausation', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de última modificación (DATETIME). Instante del último cambio registrado en los datos de causación.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausation', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausation', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausation', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que modificó (VARCHAR 20). Identificación del usuario responsable del último cambio.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausation', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausation', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausation', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación (DATETIME). Momento en que se originó el registro de causación diferida.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausation', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausation', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausation', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario creador (VARCHAR 20). Identificación del usuario que generó el registro inicial.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausation', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausation', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausation', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del registro (TINYINT). Indicador numérico del estado actual: activo, confirmado, anulado o en tránsito.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausation', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del registro', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausation', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausation', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de la cuota o período (DECIMAL 20,2). Monto en moneda de la causación asignada a cada período diferido.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausation', @level2type = N'COLUMN', @level2name = N'ValueCreditPeriod';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de la cuota', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausation', @level2type = N'COLUMN', @level2name = N'ValueCreditPeriod';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausation', @level2type = N'COLUMN', @level2name = N'ValueCreditPeriod';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del centro de costo (INT, FK). Referencia a la unidad o departamento funcional donde se imputa el gasto.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausation', @level2type = N'COLUMN', @level2name = N'IdCostCenter';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Centro de costo', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausation', @level2type = N'COLUMN', @level2name = N'IdCostCenter';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausation', @level2type = N'COLUMN', @level2name = N'IdCostCenter';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tercero beneficiario (INT, FK). Referencia al proveedor, acreedor o beneficiario del pago diferido.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausation', @level2type = N'COLUMN', @level2name = N'IdThirdParty';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tercero beneficiario', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausation', @level2type = N'COLUMN', @level2name = N'IdThirdParty';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausation', @level2type = N'COLUMN', @level2name = N'IdThirdParty';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha final de causación (DATETIME). Fecha de término del período de diferimiento o causación.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausation', @level2type = N'COLUMN', @level2name = N'EndDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Final de la causacion', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausation', @level2type = N'COLUMN', @level2name = N'EndDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausation', @level2type = N'COLUMN', @level2name = N'EndDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha inicial de causación (DATETIME). Fecha de inicio del período de diferimiento o causación.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausation', @level2type = N'COLUMN', @level2name = N'InitialDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Inicial', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausation', @level2type = N'COLUMN', @level2name = N'InitialDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausation', @level2type = N'COLUMN', @level2name = N'InitialDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de distribución (TINYINT). Parámetro numérico: 1=Proporcional (por período), 2=Valor fijo por cuota.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausation', @level2type = N'COLUMN', @level2name = N'TypeDistribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de distribucion:(1=Proporcional, 2=Valor)', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausation', @level2type = N'COLUMN', @level2name = N'TypeDistribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausation', @level2type = N'COLUMN', @level2name = N'TypeDistribution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de períodos diferidos (INT). Cantidad de cuotas o ciclos en que se divide la causación.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausation', @level2type = N'COLUMN', @level2name = N'PeriodsNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tiempo diferido', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausation', @level2type = N'COLUMN', @level2name = N'PeriodsNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausation', @level2type = N'COLUMN', @level2name = N'PeriodsNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de cuenta contable principal (INT, FK). Referencia a la cuenta del Mayor General donde se registra la causación.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausation', @level2type = N'COLUMN', @level2name = N'IdMainAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta activo', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausation', @level2type = N'COLUMN', @level2name = N'IdMainAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausation', @level2type = N'COLUMN', @level2name = N'IdMainAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de factura o documento (VARCHAR 20). Referencia o número de la factura de origen del gasto a diferir.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausation', @level2type = N'COLUMN', @level2name = N'BillNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Factura correspondiente ', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausation', @level2type = N'COLUMN', @level2name = N'BillNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausation', @level2type = N'COLUMN', @level2name = N'BillNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de cuenta por pagar (INT, FK). Referencia al registro de obligación de pago en [Payments].[AccountPayable].', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausation', @level2type = N'COLUMN', @level2name = N'IdAccountPayable';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta por pagar', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausation', @level2type = N'COLUMN', @level2name = N'IdAccountPayable';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausation', @level2type = N'COLUMN', @level2name = N'IdAccountPayable';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de causación diferida (INT PK). Clave primaria que identifica unívocamente el registro de causación.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausation', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la causación diferida', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausation', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausation', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de causaciones diferidas de cuentas por pagar: almacena la programación de distribución de un pago o factura en varios períodos contables, con sus fechas, montos por período, estado y trazabilidad de auditoría (creación, confirmación y anulación).', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausation';
