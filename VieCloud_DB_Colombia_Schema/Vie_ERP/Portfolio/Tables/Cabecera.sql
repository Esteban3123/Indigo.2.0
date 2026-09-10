CREATE TABLE [Portfolio].[Cabecera] (
    [Code]                                         NVARCHAR (255) NULL,
    [OperatingUnitId]                              NVARCHAR (255) NULL,
    [AccountReceivableType]                        NVARCHAR (255) NULL,
    [ThirdPartyId]                                 NVARCHAR (255) NULL,
    [CustomerId]                                   NVARCHAR (255) NULL,
    [SellerId]                                     NVARCHAR (255) NULL,
    [InvoiceId]                                    NVARCHAR (255) NULL,
    [InvoiceNumber]                                NVARCHAR (255) NULL,
    [AccountReceivableDate]                        DATETIME       NULL,
    [Term]                                         NVARCHAR (255) NULL,
    [ExpiredDate]                                  DATETIME       NULL,
    [Observations]                                 NVARCHAR (255) NULL,
    [PortfolioStatus]                              NVARCHAR (255) NULL,
    [OpeningBalance]                               NVARCHAR (255) NULL,
    [RecognitionId]                                NVARCHAR (255) NULL,
    [PaymentAgreement]                             NVARCHAR (255) NULL,
    [RegistrationAdjusted]                         NVARCHAR (255) NULL,
    [MainAccountWithoutFilingId]                   NVARCHAR (255) NULL,
    [NumberShares]                                 NVARCHAR (255) NULL,
    [Value]                                        FLOAT (53)     NULL,
    [Balance]                                      FLOAT (53)     NULL,
    [DeteriorationBalance]                         NVARCHAR (255) NULL,
    [DeteriorationPayment]                         NVARCHAR (255) NULL,
    [ProvisionBalance]                             NVARCHAR (255) NULL,
    [ProvisionPayment]                             NVARCHAR (255) NULL,
    [Status]                                       NVARCHAR (255) NULL,
    [CostCenterId]                                 NVARCHAR (255) NULL,
    [InvoiceCategoryId]                            NVARCHAR (255) NULL,
    [AccountWithoutRadicateId]                     NVARCHAR (255) NULL,
    [AccountRadicateId]                            NVARCHAR (255) NULL,
    [AccountObjectionRemediedId]                   NVARCHAR (255) NULL,
    [AccountConciliationId]                        NVARCHAR (255) NULL,
    [AccountLegalCollectionId]                     NVARCHAR (255) NULL,
    [AccountHardCollectionId]                      NVARCHAR (255) NULL,
    [AccountDebtorOrder]                           NVARCHAR (255) NULL,
    [AccountCreditorOrder]                         NVARCHAR (255) NULL,
    [CreditProvisionAccountId]                     NVARCHAR (255) NULL,
    [DebitProvisionAccountId]                      NVARCHAR (255) NULL,
    [DebitAccountDeteriorationId]                  NVARCHAR (255) NULL,
    [CreditAccountDeteriorationId]                 NVARCHAR (255) NULL,
    [ReversalAccountDeteriorationId]               NVARCHAR (255) NULL,
    [PreviousPeriodReversalAccountDeteriorationId] NVARCHAR (255) NULL,
    [AffectBudget]                                 NVARCHAR (255) NULL,
    [BudgetId]                                     NVARCHAR (255) NULL,
    [CreationUser]                                 NVARCHAR (255) NULL,
    [CreationDate]                                 NVARCHAR (255) NULL,
    [ModificationUser]                             NVARCHAR (255) NULL,
    [ModificationDate]                             NVARCHAR (255) NULL,
    [ConfirmationUser]                             NVARCHAR (255) NULL,
    [ConfirmationDate]                             NVARCHAR (255) NULL,
    [AnnulmentUser]                                NVARCHAR (255) NULL,
    [AnnulmentDate]                                NVARCHAR (255) NULL,
    [TimeStamp]                                    NVARCHAR (255) NULL,
    [DeteriorationBalanceCurrentYear]              NVARCHAR (255) NULL,
    [DeteriorationBalancePreviousYear]             NVARCHAR (255) NULL,
    [CurrentDeteriorationYear]                     NVARCHAR (255) NULL,
    [CareGroupId]                                  NVARCHAR (255) NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del grupo de atención (centro médico, clínica, hospital); referencia a unidad funcional de salud. NVARCHAR(255)', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'CareGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del grupo de atención.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'CareGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'CareGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Año fiscal actual en el cual se registra el deterioro de cartera; año contable vigente. NVARCHAR(255)', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'CurrentDeteriorationYear';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece el año actual del deterioro.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'CurrentDeteriorationYear';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'CurrentDeteriorationYear';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Saldo acumulado de deterioro de cartera del año fiscal anterior; valor histórico de provisión. NVARCHAR(255)', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'DeteriorationBalancePreviousYear';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece el saldo de deterioro del año anterior.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'DeteriorationBalancePreviousYear';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'DeteriorationBalancePreviousYear';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Saldo de deterioro de cartera del año fiscal actual; provisión vigente por cuentas incobrables. NVARCHAR(255)', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'DeteriorationBalanceCurrentYear';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece el saldo de deterioro del año actual.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'DeteriorationBalanceCurrentYear';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'DeteriorationBalanceCurrentYear';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca de tiempo de auditoría; registro automático del instante de creación, modificación o transacción del registro. NVARCHAR(255), Auditoría', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Sello de tiempo. Guarda el instante tiempo de la creación, registro o modificación de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha en que se anuló o canceló el documento; fecha de reversión o eliminación contable. DATETIME', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica fecha de anulación.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que ejecutó la anulación del registro; identificación del operador administrativo. NVARCHAR(255), Auditoría', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el usuario que realizó la anulación.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de confirmación o validación oficial del documento por autoridad competente. DATETIME', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la fecha de confirmación.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario autorizado que confirmó la cabecera; supervisor, validador o responsable de aprobación. NVARCHAR(255), Auditoría', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el usuario que realizó la confirmación.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del último cambio realizado al registro; auditoría de alteraciones. NVARCHAR(255), Auditoría', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la fecha de modificación.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que realizó la última modificación; trazabilidad de cambios en la cabecera. NVARCHAR(255), Auditoría', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el usuario que realizó la modificación.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de creación original del registro; auditoría de origen. NVARCHAR(255), Auditoría', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica fecha de creación.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó inicialmente el registro; identificación del operador de entrada de datos. NVARCHAR(255), Auditoría', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica usuario de creación.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del presupuesto asociado; vinculación a plan financiero anual o proyecto. NVARCHAR(255), FK', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'BudgetId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del presupuesto.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'BudgetId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'BudgetId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de impacto presupuestal (afecta o no afecta el presupuesto); bandera contable. NVARCHAR(255)', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'AffectBudget';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece la afectación del presupuesto.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'AffectBudget';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'AffectBudget';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuenta contable de reversión de deterioro del período anterior; ajuste de cifras históricas. NVARCHAR(255), FK', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'PreviousPeriodReversalAccountDeteriorationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del deterioro de la cuenta de reversión del período anterior.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'PreviousPeriodReversalAccountDeteriorationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'PreviousPeriodReversalAccountDeteriorationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuenta contable destinada a reversar o ajustar deterioro; contrapartida en mayor. NVARCHAR(255), FK', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'ReversalAccountDeteriorationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del deterioro de la cuenta de reversión.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'ReversalAccountDeteriorationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'ReversalAccountDeteriorationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuenta de crédito (haber) para registrar deterioro de cartera; lado de abono contable. NVARCHAR(255), FK', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'CreditAccountDeteriorationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del deterioro de la cuenta de crédito.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'CreditAccountDeteriorationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'CreditAccountDeteriorationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuenta de débito (debe) para deterioro de cartera; lado de cargo contable. NVARCHAR(255), FK', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'DebitAccountDeteriorationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del deterioro de la cuenta débito.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'DebitAccountDeteriorationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'DebitAccountDeteriorationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuenta de débito (debe) para provisión; gasto por provisión de incobrabilidad. NVARCHAR(255), FK', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'DebitProvisionAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la provisión de la cuenta de débito.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'DebitProvisionAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'DebitProvisionAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuenta de crédito (haber) para provisión; actualización de reserva de cartera. NVARCHAR(255), FK', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'CreditProvisionAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la provisión de la cuenta de crédito.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'CreditProvisionAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'CreditProvisionAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Orden legal de acreedor; número de proceso judicial contra deudor moroso. NVARCHAR(255)', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'AccountCreditorOrder';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Orden de Acreedor de Cuenta', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'AccountCreditorOrder';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'AccountCreditorOrder';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Orden de embargo o cobranza contra deudor; mandamiento judicial o administrativo. NVARCHAR(255)', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'AccountDebtorOrder';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena la orden de deudor de la cuenta.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'AccountDebtorOrder';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'AccountDebtorOrder';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de gestión de cobranza coercitiva; ejecución forzosa o embargos. NVARCHAR(255), FK', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'AccountHardCollectionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del cobro de la cuenta fuerte.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'AccountHardCollectionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'AccountHardCollectionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de cobro por vía legal; gestión jurídica de cuentas por cobrar. NVARCHAR(255), FK', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'AccountLegalCollectionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del cobro de la cuenta legal.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'AccountLegalCollectionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'AccountLegalCollectionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de conciliación o acuerdo entre acreedor y deudor; búsqueda de solución. NVARCHAR(255), FK', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'AccountConciliationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la Reconciliación de cuentas', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'AccountConciliationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'AccountConciliationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de objeción del cliente resuelta o subsanada; reclamo procesado. NVARCHAR(255), FK', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'AccountObjectionRemediedId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la objeción de cuenta corregida.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'AccountObjectionRemediedId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'AccountObjectionRemediedId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de factura radicada (presentada formalmente); documento tramitado. NVARCHAR(255), FK', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'AccountRadicateId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la radicación de cuentas.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'AccountRadicateId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'AccountRadicateId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de factura sin radicar aún; documento no presentado formalmente. NVARCHAR(255), FK', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'AccountWithoutRadicateId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de cuenta sin radicar.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'AccountWithoutRadicateId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'AccountWithoutRadicateId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de categoría de factura; clasificación por tipo (servicios, consultas, procedimientos, etc.). NVARCHAR(255), FK', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'InvoiceCategoryId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de categoría de la factura.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'InvoiceCategoryId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'InvoiceCategoryId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del centro de costo; unidad de negocio, departamento o sección responsable. NVARCHAR(255), FK', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del centro de costo.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado actual de la cabecera del portfolio; Activo, Cancelado, Pendiente, etc. NVARCHAR(255)', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el estado de la cabecera.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Monto o indicador de pago de provisión realizado; abono a reserva de cartera. NVARCHAR(255)', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'ProvisionPayment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece el pago de provisiones.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'ProvisionPayment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'ProvisionPayment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Saldo actual de la provisión por incobrabilidad; valor de reserva constituida. NVARCHAR(255)', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'ProvisionBalance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece el saldo de provisiones.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'ProvisionBalance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'ProvisionBalance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Monto de pago aplicado a cuentas deterioradas; liquidación parcial de cartera. NVARCHAR(255)', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'DeteriorationPayment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el pago por deterioro.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'DeteriorationPayment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'DeteriorationPayment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Saldo de cuentas deterioradas o en riesgo de incobrable; balance de cartera vencida. NVARCHAR(255)', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'DeteriorationBalance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece el deterioro del balance general.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'DeteriorationBalance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'DeteriorationBalance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Saldo total o neto de la situación financiera de la cabecera; diferencia entre activos y pasivos. FLOAT', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'Balance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece el balance general de la situación financiera.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'Balance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'Balance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor monetario inicial o principal de la transacción; monto de factura o documento. FLOAT', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece el valor de la cabecera.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de cuotas o recursos compartidos en el acuerdo; número de fracciones de pago. NVARCHAR(255)', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'NumberShares';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece número de recursos compartidos.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'NumberShares';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'NumberShares';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuenta contable principal que aún no se ha presentado o radicado; pendiente de formalización. NVARCHAR(255), FK', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'MainAccountWithoutFilingId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta principal sin presentación.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'MainAccountWithoutFilingId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'MainAccountWithoutFilingId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de si el registro fue ajustado después de presentación; corrección post-radicación. NVARCHAR(255)', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'RegistrationAdjusted';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece la inscripción ajustada.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'RegistrationAdjusted';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'RegistrationAdjusted';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador o términos de acuerdo de pago pactado con deudor; plan de pagos. NVARCHAR(255)', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'PaymentAgreement';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece acuerdo de pago.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'PaymentAgreement';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'PaymentAgreement';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de reconocimiento contable; referencia a comprobante de ingresos o acreencia. NVARCHAR(255), FK', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'RecognitionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del reconocimiento asociado.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'RecognitionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'RecognitionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Saldo inicial o de apertura al comienzo del período; balance anterior. NVARCHAR(255)', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'OpeningBalance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece el saldo inicial.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'OpeningBalance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'OpeningBalance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del portfolio: 1=Activo (vigente en cobro), 0=Inactivo (cancelado o vencido). NVARCHAR(255)', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'PortfolioStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece el estado del portfolio 1 - Activo, 0 - Inactivo.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'PortfolioStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'PortfolioStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo de notas o comentarios importantes sobre la cabecera; información adicional o restricciones. NVARCHAR(255)', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'Observations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica información importante de la cabecera.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'Observations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'Observations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de vencimiento o caducidad del derecho de cobro; término legal o pactado. DATETIME', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'ExpiredDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica fecha de caducidad.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'ExpiredDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'ExpiredDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Plazo, término o período de validez de la cabecera; duración del acuerdo. NVARCHAR(255)', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'Term';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el término de la cabecera.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'Term';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'Term';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha en que nace el derecho de cobro; fecha de facturación o acreencia. DATETIME', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'AccountReceivableDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica fecha de cobro de la cuenta.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'AccountReceivableDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'AccountReceivableDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número secuencial o identificador de la factura; prefijo + consecutivo. NVARCHAR(255)', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece el numero de la factura.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la factura; clave primaria en tabla Facturas. NVARCHAR(255), FK', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'InvoiceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la factura.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'InvoiceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'InvoiceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del vendedor, profesional de salud o proveedor que emitió el documento. NVARCHAR(255), FK', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'SellerId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del vendedor.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'SellerId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'SellerId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del cliente o paciente a quien se facturó; cédula, documento o código. NVARCHAR(255), FK Identification_Ofuscado', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'CustomerId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del cliente.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'CustomerId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'CustomerId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tercero responsable del pago (asegurador, empresa, responsable civil). NVARCHAR(255), FK', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tercero asociado.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de cuenta por cobrar; clasificación (directa, asignada, garantizada, contingente, etc.). NVARCHAR(255)', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'AccountReceivableType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica tipo de cobro de la cuenta.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'AccountReceivableType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'AccountReceivableType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la unidad operativa (clínica, consultorio, hospital, centro de atención). NVARCHAR(255), FK', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad operativa.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único o identificador legible de la cabecera; clave secundaria o número de seguimiento. NVARCHAR(255)', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código identificador de la cabecera.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cabecera de cartera: registra las cuentas por cobrar de la institución, incluyendo facturas emitidas a terceros (aseguradoras, pacientes, empresas), su estado de cobro, saldos, acuerdos de pago, deterioro y provisiones contables. Es el documento maestro de gestión de cartera y cobro de facturas.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Cabecera';
