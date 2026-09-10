CREATE TABLE [Budget].[AnnualizedCashFlow] (
    [Id]                         INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CategoryId]                 INT             NOT NULL,
    [DocumentSource]             TINYINT         NOT NULL,
    [Month]                      TINYINT         NOT NULL,
    [InitialValue]               NUMERIC (18, 2) NOT NULL,
    [DebitModificationValue]     NUMERIC (18, 2) NOT NULL,
    [CreditModificationValue]    NUMERIC (18, 2) NOT NULL,
    [DebitTransferValue]         NUMERIC (18, 2) NOT NULL,
    [CreditTransferValue]        NUMERIC (18, 2) NOT NULL,
    [TotalScheduled]             NUMERIC (18, 2) NOT NULL,
    [ExecutedValue]              NUMERIC (18, 2) NOT NULL,
    [Balance]                    NUMERIC (18, 2) NOT NULL,
    [ReserveValue]               NUMERIC (18, 2) NOT NULL,
    [DebitReserveModValue]       NUMERIC (18, 2) NOT NULL,
    [CreditReserveModValie]      NUMERIC (18, 2) NOT NULL,
    [DebitReserveTransValue]     NUMERIC (18, 2) NOT NULL,
    [CreditReserveTransValue]    NUMERIC (18, 2) NOT NULL,
    [ExecutedReserveValue]       NUMERIC (18, 2) NOT NULL,
    [CxPValue]                   NUMERIC (18, 2) NOT NULL,
    [DebitCxPModificationValue]  NUMERIC (18, 2) NOT NULL,
    [CreditCxPModificationValue] NUMERIC (18, 2) NOT NULL,
    [DebitCxPTransferValue]      NUMERIC (18, 2) NOT NULL,
    [CreditCxPTransferValue]     NUMERIC (18, 2) NOT NULL,
    [ExecutedCxPValue]           NUMERIC (18, 2) NOT NULL,
    [Status]                     TINYINT         NOT NULL,
    [CreationUser]               VARCHAR (20)    CONSTRAINT [DF_AnnualizedCashFlow_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]               DATETIME        CONSTRAINT [DF_AnnualizedCashFlow_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser]           VARCHAR (20)    NULL,
    [ModificationDate]           DATETIME        NULL,
    [ConfirmationUser]           VARCHAR (20)    NULL,
    [ConfirmationDate]           DATETIME        NULL,
    [AnnulmentUser]              VARCHAR (20)    NULL,
    [AnnulmentDate]              DATETIME        NULL,
    [TimeStamp]                  ROWVERSION      NOT NULL,
    CONSTRAINT [PK_PAC] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PAC_Category] FOREIGN KEY ([CategoryId]) REFERENCES [Budget].[Category] ([Id])
);




GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_AnnualizedCashFlow__CategoryId__Month]
    ON [Budget].[AnnualizedCashFlow]([CategoryId] ASC, [Month] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AnnualizedCashFlow__CategoryId]
    ON [Budget].[AnnualizedCashFlow]([CategoryId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal automática (TIMESTAMP SQL Server) que registra el instante exacto de creación, modificación, anulación o confirmación del flujo de caja anualizado. Auditoría de cambios.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se anuló, canceló o revocó el registro del flujo de caja anualizado. Nulo si no ha sido anulado.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Anulación', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del usuario (login/código) que ejecutó la anulación, cancelación o revocación del flujo de caja. Auditoría de anulación.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Anulación', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se confirmó, validó o aprobó el registro del flujo de caja anualizado. Transición a estado confirmado.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Confirmación', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del usuario (login/código) que confirmó, validó o aprobó el flujo de caja anualizado. Auditoría de confirmación.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Confirmación', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora del último cambio, ajuste o edición realizado al flujo de caja anualizado. Nulo si no ha sido modificado.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del usuario (login/código) que realizó la última modificación, ajuste o edición del flujo de caja. Auditoría de cambios.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación o registro inicial del flujo de caja anualizado en el sistema. Auditoría de origen.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del usuario (login/código) que creó o registró inicialmente el flujo de caja anualizado. Auditoría de creación.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del documento PAC: 1=Registrado (borrador), 2=Confirmado (aprobado), 3=Anulado (cancelado). Controla el flujo de aprobación presupuestaria.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del Documento (Registrado = 1, Confirmado = 2, Anulado = 3)', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Monto ejecutado, desembolsado o gastado efectivamente en la Cuenta por Pagar (CxP) del rubro presupuestario. Numérico 18,2.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'ExecutedCxPValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor ejecutado del rubro CxP', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'ExecutedCxPValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'ExecutedCxPValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor o monto de traslado con movimiento de crédito que se transfirió a la Cuenta por Pagar (CxP) del rubro. Ajuste de movimiento.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'CreditCxPTransferValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor traslado credito a la CxP del rubro', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'CreditCxPTransferValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'CreditCxPTransferValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor o monto de traslado con movimiento de débito que se transfirió a la Cuenta por Pagar (CxP) del rubro. Ajuste de movimiento.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'DebitCxPTransferValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor traslado debito a la CxP del rubro', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'DebitCxPTransferValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'DebitCxPTransferValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor o monto de modificación con movimiento de crédito aplicado a la Cuenta por Pagar (CxP) del rubro. Incremento de obligación.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'CreditCxPModificationValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor modificacion credito a la CxP del rubro', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'CreditCxPModificationValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'CreditCxPModificationValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor o monto de modificación con movimiento de débito aplicado a la Cuenta por Pagar (CxP) del rubro. Reducción de obligación.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'DebitCxPModificationValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor modificacion debito a la CxP del rubro', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'DebitCxPModificationValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'DebitCxPModificationValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor total de la Cuenta por Pagar (CxP), obligación o pasivo registrado en el rubro presupuestario. Numérico 18,2.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'CxPValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de la cuenta por pagar CxP', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'CxPValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'CxPValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Monto ejecutado, liberado o gastado efectivamente de la reserva presupuestaria del rubro. Numérico 18,2.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'ExecutedReserveValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor ejecutado del rubro de la reserva', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'ExecutedReserveValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'ExecutedReserveValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor o monto de traslado con movimiento de crédito hacia la reserva presupuestaria del rubro. Ajuste de reserva.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'CreditReserveTransValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de traslado credito a la reserva del rubro', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'CreditReserveTransValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'CreditReserveTransValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor o monto de traslado con movimiento de débito desde la reserva presupuestaria del rubro. Ajuste de reserva.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'DebitReserveTransValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de traslado debito a la reserva del rubro', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'DebitReserveTransValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'DebitReserveTransValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor o monto de modificación con movimiento de crédito aplicado a la reserva presupuestaria del rubro. Incremento de reserva.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'CreditReserveModValie';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de modificacion credito a la reserva del rubro', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'CreditReserveModValie';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'CreditReserveModValie';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor o monto de modificación con movimiento de débito aplicado a la reserva presupuestaria del rubro. Reducción de reserva.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'DebitReserveModValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de modificacion debito a la reserva del rubro', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'DebitReserveModValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'DebitReserveModValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor total reservado, contingenciado o apartado para el rubro presupuestario como margen de seguridad. Numérico 18,2.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'ReserveValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de la reserva', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'ReserveValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'ReserveValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Saldo disponible del PAC (Plan Anualizado de Caja); fórmula: TotalScheduled - ExecutedValue. Indica fondos pendientes por ejecutar.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'Balance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el saldo del PAC, este se calcula de la siguiente manera  (TotalScheduled - ExecutedValue )', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'Balance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'Balance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Monto ejecutado, desembolsado o gastado efectivamente del rubro presupuestario en el mes. Numérico 18,2.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'ExecutedValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Ejecutado del rubro', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'ExecutedValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'ExecutedValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Total programado o presupuestado del rubro; fórmula: InitialValue - DebitMod + CreditMod - DebitTrans + CreditTrans. Numérico 18,2.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'TotalScheduled';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Total Programado, este se calcula de la siguiente manera  (IniitalValue - DebitModificationValue + CreditModificationValue - DebitTransferValue + CreditTransferValue)', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'TotalScheduled';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'TotalScheduled';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor o monto de traslado con movimiento de crédito (entrada) al rubro presupuestario. Incrementa presupuesto.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'CreditTransferValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor traslado credito al rubro', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'CreditTransferValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'CreditTransferValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor o monto de traslado con movimiento de débito (salida) desde el rubro presupuestario. Reduce presupuesto.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'DebitTransferValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor traslado debito al rubro', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'DebitTransferValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'DebitTransferValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor o monto de modificación con movimiento de crédito (ampliación) del rubro presupuestario. Incrementa budget.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'CreditModificationValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de modificacion credito al rubro', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'CreditModificationValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'CreditModificationValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor o monto de modificación con movimiento de débito (reducción) del rubro presupuestario. Disminuye budget.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'DebitModificationValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de modificacion debito al rubro', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'DebitModificationValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'DebitModificationValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor inicial, presupuesto base o asignación originaria del rubro presupuestario para el mes. Punto de partida. Numérico 18,2.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'InitialValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Inicial', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'InitialValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'InitialValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mes del Presupuesto Anualizado de Caja (PAC): 1=Enero, 2=Febrero, 3=Marzo, 4=Abril, 5=Mayo, 6=Junio, 7=Julio, 8=Agosto, 9=Septiembre, 10=Octubre, 11=Noviembre, 12=Diciembre.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'Month';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mes del PAC (ENERO = 1,FEBRERO = 2,MARZO =  3,ABRIL = 4,MAYO = 5,JUNIO = 6,JULIO = 7,AGOSTO =  8,SEPTIEMBRE = 9,OCTUBRE = 10,NOVIEMBRE =  11,DICIEMBRE = 12)', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'Month';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'Month';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo o fuente del presupuesto: 1=Ingreso (ingresos/ingresos operacionales), 2=Gasto (egresos/gastos operacionales). Clasificación contable.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'DocumentSource';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Tipo de Presupuesto (INGRESO = 1,GASTO = 2)', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'DocumentSource';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'DocumentSource';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la Categoría o Rubro presupuestario asociado. Clave foránea a [Budget].[Category]. Agrupa conceptos presupuestarios.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'CategoryId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del rubro', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'CategoryId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'CategoryId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (PK) del registro de Flujo de Caja Anualizado (PAC). Identity INT auto-incremental. Auditoría y trazabilidad.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del pac', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Flujo de caja anualizado por categoría presupuestal y mes. Registra los valores iniciales, modificaciones (débitos y créditos), traslados, ejecutados y saldos del presupuesto de caja, incluyendo reservas presupuestales y cuentas por pagar (CxP), permitiendo el seguimiento mensual de la ejecución financiera.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlow';
