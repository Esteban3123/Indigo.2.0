CREATE TABLE [Payroll].[AgreementsC] (
    [Id]                            INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Consecutive]                   INT             NOT NULL,
    [GroupId]                       INT             NULL,
    [EmployeeId]                    INT             NOT NULL,
    [CompanyId]                     INT             NOT NULL,
    [ConceptId]                     INT             NOT NULL,
    [KindsAgreementsId]             INT             NOT NULL,
    [Comments]                      VARCHAR (250)   NULL,
    [LiquidationType]               TINYINT         NOT NULL,
    [TermType]                      TINYINT         NOT NULL,
    [AgreementValue]                DECIMAL (18, 2) NOT NULL,
    [NumberShares]                  INT             NOT NULL,
    [State]                         VARCHAR (1)     NOT NULL,
    [StartingDate]                  DATETIME        NOT NULL,
    [CurrentBalance]                DECIMAL (18, 2) NULL,
    [EndDateSuspend]                DATETIME        NULL,
    [CommentChangeState]            VARCHAR (250)   NULL,
    [TimeStamp]                     ROWVERSION      NOT NULL,
    [PaidVacation]                  BIT             CONSTRAINT [DF_AgreementsC_PaidVacation] DEFAULT ((0)) NULL,
    [Transfertype]                  TINYINT         NULL,
    [AccountReceivableAccountingId] INT             NULL,
    CONSTRAINT [PK_AgreementsC__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Agreements_AccountReceivableAccounting] FOREIGN KEY ([AccountReceivableAccountingId]) REFERENCES [Portfolio].[AccountReceivableAccounting] ([Id]),
    CONSTRAINT [FK_Agreements_Company] FOREIGN KEY ([CompanyId]) REFERENCES [Payroll].[Company] ([Id]),
    CONSTRAINT [FK_Agreements_Concept] FOREIGN KEY ([ConceptId]) REFERENCES [Payroll].[Concept] ([Id]),
    CONSTRAINT [FK_Agreements_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [Payroll].[Employee] ([Id]),
    CONSTRAINT [FK_Agreements_Group] FOREIGN KEY ([GroupId]) REFERENCES [Payroll].[Group] ([Id]),
    CONSTRAINT [FK_AgreementsC_KindsAgreements] FOREIGN KEY ([KindsAgreementsId]) REFERENCES [Payroll].[KindsAgreements] ([Id]),
    CONSTRAINT [UQ_AgreementsC_Consecutive] UNIQUE NONCLUSTERED ([Consecutive] ASC)
);




GO



GO



GO



GO



GO



GO



GO
CREATE NONCLUSTERED INDEX [IX_AgreementsC__EmployeeId]
    ON [Payroll].[AgreementsC]([EmployeeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AgreementsC__CompanyId]
    ON [Payroll].[AgreementsC]([CompanyId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT nullable) del movimiento de factura relacionado; FK a Portfolio.AccountReceivableAccounting si convenio impacta cuentas por cobrar', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsC', @level2type = N'COLUMN', @level2name = N'AccountReceivableAccountingId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Movimiento de la Factura relacionada si la clase de convenio afecta cuentas por cobrar', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsC', @level2type = N'COLUMN', @level2name = N'AccountReceivableAccountingId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsC', @level2type = N'COLUMN', @level2name = N'AccountReceivableAccountingId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de traslado/transferencia (TINYINT nullable): 1=Mismo tercero/beneficiario, 2=Diferente tercero; solo si convenio afecta cuentas por cobrar', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsC', @level2type = N'COLUMN', @level2name = N'Transfertype';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuando la clase de convenio es afecta cuentas por cobrar se solicita el tipo de traslado:  1 - Mismo Tercero  2 - Diferente Tercero', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsC', @level2type = N'COLUMN', @level2name = N'Transfertype';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsC', @level2type = N'COLUMN', @level2name = N'Transfertype';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT, default=0) de descuento por vacaciones con pago inmediato; aplica si el convenio afecta vacaciones', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsC', @level2type = N'COLUMN', @level2name = N'PaidVacation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se utiliza para que el valor del Convenio se descuente por Vacaciones con Pago Inmediato', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsC', @level2type = N'COLUMN', @level2name = N'PaidVacation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsC', @level2type = N'COLUMN', @level2name = N'PaidVacation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Control de concurrencia (TIMESTAMP); marca temporal para bloqueo optimista en actualizaciones simultáneas', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsC', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo de control de concurrencia', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsC', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsC', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Comentario (VARCHAR 250) explicativo del cambio de estado; justificación de suspensión, terminación o anulación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsC', @level2type = N'COLUMN', @level2name = N'CommentChangeState';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Comentario General del cambio de Estado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsC', @level2type = N'COLUMN', @level2name = N'CommentChangeState';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsC', @level2type = N'COLUMN', @level2name = N'CommentChangeState';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de suspensión (DATETIME nullable) del convenio; marca cuándo se pausa la ejecución', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsC', @level2type = N'COLUMN', @level2name = N'EndDateSuspend';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Suspensión del Convenio', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsC', @level2type = N'COLUMN', @level2name = N'EndDateSuspend';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsC', @level2type = N'COLUMN', @level2name = N'EndDateSuspend';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Saldo actual (DECIMAL 18,2) pendiente del convenio; monto restante a liquidar o descuento', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsC', @level2type = N'COLUMN', @level2name = N'CurrentBalance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Saldo Actual del Convenio', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsC', @level2type = N'COLUMN', @level2name = N'CurrentBalance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsC', @level2type = N'COLUMN', @level2name = N'CurrentBalance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha inicial (DATETIME) de vigencia del convenio; cuando comienza el descuento o acuerdo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsC', @level2type = N'COLUMN', @level2name = N'StartingDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Inicial del Convenio', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsC', @level2type = N'COLUMN', @level2name = N'StartingDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsC', @level2type = N'COLUMN', @level2name = N'StartingDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del convenio (VARCHAR 1): 1=Sin confirmar, 2=Confirmado, 3=Suspendido, 4=Terminado, 5=Anulado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsC', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del Convenio: "1 " Sin confirmar - "2" Confirmado - "3" Suspendido - "4" Terminado - "5" Anulado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsC', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsC', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de cuotas (INT) en que se fracciona el convenio; cantidad de pagos o descuentos periódicos', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsC', @level2type = N'COLUMN', @level2name = N'NumberShares';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de cuotas del convenio', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsC', @level2type = N'COLUMN', @level2name = N'NumberShares';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsC', @level2type = N'COLUMN', @level2name = N'NumberShares';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor total (DECIMAL 18,2) del convenio en moneda local; monto principal a liquidar o descontar', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsC', @level2type = N'COLUMN', @level2name = N'AgreementValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Total del Convenio', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsC', @level2type = N'COLUMN', @level2name = N'AgreementValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsC', @level2type = N'COLUMN', @level2name = N'AgreementValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de plazo (TINYINT): 1=Plazo determinado en meses, 2=Plazo indeterminado; define duración del convenio', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsC', @level2type = N'COLUMN', @level2name = N'TermType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Plazo: 1 - Con N° Meses 2 - Sin N° Meses', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsC', @level2type = N'COLUMN', @level2name = N'TermType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsC', @level2type = N'COLUMN', @level2name = N'TermType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de liquidación (TINYINT): 1=Con Monto fijo, 2=Sin Monto especificado; controla forma de cálculo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsC', @level2type = N'COLUMN', @level2name = N'LiquidationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Liquidacion: 1 - Con Monto 2 - Sin Monto', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsC', @level2type = N'COLUMN', @level2name = N'LiquidationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsC', @level2type = N'COLUMN', @level2name = N'LiquidationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observación general (VARCHAR 250) del convenio; notas, motivo o detalles adicionales para auditoría', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsC', @level2type = N'COLUMN', @level2name = N'Comments';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Obervacion general del Convenio', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsC', @level2type = N'COLUMN', @level2name = N'Comments';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsC', @level2type = N'COLUMN', @level2name = N'Comments';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) de la clase/tipo de convenio (anticipo, préstamo, arreglo); FK a Payroll.KindsAgreements', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsC', @level2type = N'COLUMN', @level2name = N'KindsAgreementsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Clase del Convenio', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsC', @level2type = N'COLUMN', @level2name = N'KindsAgreementsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsC', @level2type = N'COLUMN', @level2name = N'KindsAgreementsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) del concepto de nómina utilizado en liquidación del convenio; FK a Payroll.Concept (descuento, bonificación, etc.)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsC', @level2type = N'COLUMN', @level2name = N'ConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Concepto con el que se va a realizar la liquidacion', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsC', @level2type = N'COLUMN', @level2name = N'ConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsC', @level2type = N'COLUMN', @level2name = N'ConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) de la empresa empleadora que suscribe el convenio; FK a Payroll.Company', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsC', @level2type = N'COLUMN', @level2name = N'CompanyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la empresa con la que se suscribe el convenio', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsC', @level2type = N'COLUMN', @level2name = N'CompanyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsC', @level2type = N'COLUMN', @level2name = N'CompanyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) del empleado/trabajador firmante del convenio; FK a Payroll.Employee (PII relacionado)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsC', @level2type = N'COLUMN', @level2name = N'EmployeeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsC', @level2type = N'COLUMN', @level2name = N'EmployeeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsC', @level2type = N'COLUMN', @level2name = N'EmployeeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) del grupo de nómina al cual pertenece el empleado; FK a Payroll.Group para clasificación salarial', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsC', @level2type = N'COLUMN', @level2name = N'GroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Del Grupo En Nomina ', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsC', @level2type = N'COLUMN', @level2name = N'GroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsC', @level2type = N'COLUMN', @level2name = N'GroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número secuencial único (INT) del convenio para referencia administrativa y búsqueda por orden de creación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsC', @level2type = N'COLUMN', @level2name = N'Consecutive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo  para los convenios', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsC', @level2type = N'COLUMN', @level2name = N'Consecutive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsC', @level2type = N'COLUMN', @level2name = N'Consecutive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT) del convenio/acuerdo cabecera en nómina; clave primaria de auditoría y trazabilidad', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsC', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Convenio Cabecera', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsC', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsC', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Acuerdos de descuento o libranza de nómina por empleado. Registra los convenios de pago pactados con el trabajador (préstamos, embargos, cuotas sindicales, etc.), incluyendo valor, número de cuotas, saldo pendiente y estado del acuerdo.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsC';
