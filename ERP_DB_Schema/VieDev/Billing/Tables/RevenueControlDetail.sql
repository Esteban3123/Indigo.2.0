CREATE TABLE [Billing].[RevenueControlDetail] (
    [Id]                                  INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [RevenueControlId]                    INT             NOT NULL,
    [BillingAuthorizationId]              INT             NULL,
    [FolioOrder]                          TINYINT         NOT NULL,
    [FolioType]                           TINYINT         NOT NULL,
    [LiquidationType]                     TINYINT         CONSTRAINT [DF_RevenueControlDetail_LiquidationType] DEFAULT ((1)) NOT NULL,
    [ContractEntityId]                    INT             NULL,
    [HealthAdministratorId]               INT             NULL,
    [ThirdPartyId]                        INT             NOT NULL,
    [CareGroupId]                         INT             NOT NULL,
    [TotalFolio]                          NUMERIC (20, 2) NOT NULL,
    [ResponsibleRecoveryFee]              TINYINT         CONSTRAINT [DF_RevenueControlDetail_ResponsibleRecoveryFee] DEFAULT ((1)) NOT NULL,
    [TotalPatientSalesPrice]              NUMERIC (20, 2) CONSTRAINT [DF_RevenueControlDetail_TotalPatientSalesPrice] DEFAULT ((0)) NOT NULL,
    [PatientDiscount]                     NUMERIC (20, 2) CONSTRAINT [DF_RevenueControlDetail_PatientDiscount] DEFAULT ((0)) NOT NULL,
    [PatientDiscountPercentage]           NUMERIC (5, 2)  CONSTRAINT [DF_RevenueControlDetail_PatientDiscountPercentage] DEFAULT ((0)) NOT NULL,
    [TotalPatientWithDiscount]            NUMERIC (20, 2) CONSTRAINT [DF_RevenueControlDetail_TotalPatientWithDiscount] DEFAULT ((0)) NOT NULL,
    [ValueCopay]                          NUMERIC (20, 2) CONSTRAINT [DF_RevenueControlDetail_ValueCopay] DEFAULT ((0)) NOT NULL,
    [ValueFeeModerator]                   NUMERIC (20, 2) CONSTRAINT [DF_RevenueControlDetail_ValueFeeModerator] DEFAULT ((0)) NOT NULL,
    [ValueVoucher]                        NUMERIC (20, 2) CONSTRAINT [DF_RevenueControlDetail_ValueVoucher] DEFAULT ((0)) NOT NULL,
    [Observation]                         VARCHAR (MAX)   NULL,
    [InvoiceCategoryId]                   INT             NULL,
    [OutputDate]                          DATETIME        NULL,
    [IsCutAccount]                        BIT             CONSTRAINT [DF_RevenueControlDetail_IsCutAccount] DEFAULT ((0)) NOT NULL,
    [OutputDiagnosis]                     CHAR (4)        NULL,
    [Status]                              TINYINT         NOT NULL,
    [CreationUser]                        VARCHAR (20)    NOT NULL,
    [CreationDate]                        DATETIME        NOT NULL,
    [ModificationUser]                    VARCHAR (20)    NULL,
    [ModificationDate]                    DATETIME        NULL,
    [TimeStamp]                           ROWVERSION      NOT NULL,
    [StatusFolioId]                       INT             NULL,
    [PatientQuotaResponsibleThirdPartyId] INT             NULL,
    [CurrencyId]                          INT             NULL,
    [TRMValue]                            NUMERIC (20, 5) CONSTRAINT [DF__RevenueCo__TRMVa__20340A56] DEFAULT ((1)) NOT NULL,
    [IsMasterAccount]                     TINYINT         CONSTRAINT [DF__RevenueCo__IsMas__75549949] DEFAULT ((0)) NOT NULL,
    [RevenueControlDetailMasterId]        INT             NULL,
    CONSTRAINT [PK_RevenueControlDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [CK_RevenueControlDetail] CHECK ([LiquidationType]<>(0)),
    CONSTRAINT [FK_RevenueControlDetail_BillingAuthorization] FOREIGN KEY ([BillingAuthorizationId]) REFERENCES [Billing].[BillingAuthorization] ([Id]),
    CONSTRAINT [FK_RevenueControlDetail_ContractEntity] FOREIGN KEY ([ContractEntityId]) REFERENCES [Contract].[ContractEntity] ([Id]),
    CONSTRAINT [FK_RevenueControlDetail_Currency] FOREIGN KEY ([CurrencyId]) REFERENCES [Common].[Currency] ([Id]),
    CONSTRAINT [FK_RevenueControlDetail_HealthAdministrator] FOREIGN KEY ([HealthAdministratorId]) REFERENCES [Contract].[HealthAdministrator] ([Id]),
    CONSTRAINT [FK_RevenueControlDetail_InvoiceCategories] FOREIGN KEY ([InvoiceCategoryId]) REFERENCES [Billing].[InvoiceCategories] ([Id]),
    CONSTRAINT [FK_RevenueControlDetail_MasterAccount] FOREIGN KEY ([RevenueControlDetailMasterId]) REFERENCES [Billing].[RevenueControlDetail] ([Id]),
    CONSTRAINT [FK_RevenueControlDetail_RevenueControl] FOREIGN KEY ([RevenueControlId]) REFERENCES [Billing].[RevenueControl] ([Id]),
    CONSTRAINT [FK_RevenueControlDetail_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id]),
    CONSTRAINT [FK_RevenueControlDetail_ThirdParty_Quota_Responsible] FOREIGN KEY ([PatientQuotaResponsibleThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id]),
    CONSTRAINT [FK_SettingsBilling_StatusFolio] FOREIGN KEY ([StatusFolioId]) REFERENCES [Billing].[ConceptsCausesStatusFolio] ([Id])
);


GO
ALTER TABLE [Billing].[RevenueControlDetail] NOCHECK CONSTRAINT [CK_RevenueControlDetail];


GO
ALTER TABLE [Billing].[RevenueControlDetail] NOCHECK CONSTRAINT [FK_RevenueControlDetail_BillingAuthorization];


GO
ALTER TABLE [Billing].[RevenueControlDetail] NOCHECK CONSTRAINT [FK_RevenueControlDetail_ContractEntity];


GO
ALTER TABLE [Billing].[RevenueControlDetail] NOCHECK CONSTRAINT [FK_RevenueControlDetail_Currency];


GO
ALTER TABLE [Billing].[RevenueControlDetail] NOCHECK CONSTRAINT [FK_RevenueControlDetail_HealthAdministrator];


GO
ALTER TABLE [Billing].[RevenueControlDetail] NOCHECK CONSTRAINT [FK_RevenueControlDetail_InvoiceCategories];


GO
ALTER TABLE [Billing].[RevenueControlDetail] NOCHECK CONSTRAINT [FK_RevenueControlDetail_MasterAccount];


GO
ALTER TABLE [Billing].[RevenueControlDetail] NOCHECK CONSTRAINT [FK_RevenueControlDetail_RevenueControl];


GO
ALTER TABLE [Billing].[RevenueControlDetail] NOCHECK CONSTRAINT [FK_RevenueControlDetail_ThirdParty];


GO
ALTER TABLE [Billing].[RevenueControlDetail] NOCHECK CONSTRAINT [FK_RevenueControlDetail_ThirdParty_Quota_Responsible];


GO
ALTER TABLE [Billing].[RevenueControlDetail] NOCHECK CONSTRAINT [FK_SettingsBilling_StatusFolio];


GO
ALTER TABLE [Billing].[RevenueControlDetail] ENABLE CHANGE_TRACKING WITH (TRACK_COLUMNS_UPDATED = OFF);


GO
CREATE NONCLUSTERED INDEX [IX_RevenueControlDetail_RevenueControlId_Id]
    ON [Billing].[RevenueControlDetail]([RevenueControlId] ASC, [Id] ASC);


GO
CREATE NONCLUSTERED INDEX [UX_RevenueControlDetail_RevenueControlDetailMasterId]
    ON [Billing].[RevenueControlDetail]([RevenueControlDetailMasterId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_RevenueControlDetail_IsMasterAccount_RevenueControlId]
    ON [Billing].[RevenueControlDetail]([IsMasterAccount] ASC, [RevenueControlId] ASC)
    INCLUDE([CareGroupId], [FolioOrder], [Status]);


GO


-- =============================================
-- Author:		Cristian Camilo Bahamon Casta;o
-- Create date: 16/02/2023
-- Description:	Trigger para crear asignar a los folios creados cual es el folio madre
-- =============================================

CREATE TRIGGER [Billing].[TriggerIsMasterRevenueControlDetail]
   ON [Billing].[RevenueControlDetail]
   FOR INSERT
AS 
BEGIN


	IF EXISTS(
				SELECT 1 FROM Billing.SettingsBilling sb WITH(NOLOCK)
				WHERE sb.LiquidateMasterAccount = 1
			) AND NOT EXISTS (
				SELECT 1 
				FROM Billing.RevenueControlDetail rcd WITH(NOLOCK)
				WHERE rcd.IsMasterAccount in (1,3)
					and rcd.RevenueControlId in (SELECT i.RevenueControlId
													from INSERTED i)
					AND rcd.Id not in (SELECT i.Id
										from INSERTED i)
			)
	BEGIN 

			UPDATE rcd SET rcd.IsMasterAccount = iif(i.FolioType=3,4,1) 
			FROM (select top 1 * from INSERTED) i
			JOIN Billing.RevenueControlDetail rcd ON i.Id=rcd.Id
			WHERE i.FolioOrder=1
	END

END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '0- No es cuenta Madre  1- folio Cuenta Madre  2- Cuenta Madre Separada (Entidad)  3- Cuenta Madre Origen  4- Cuenta Madre Separada (Paciente)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'IsMasterAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la Moneda Parametrizada.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'CurrencyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del tercero responsable de la cuota del paciente.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'PatientQuotaResponsibleThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del Documento   Registrado = 1  Facturado = 2  Bloqueado = 3  Anulado = 4  Reconocimiento Ingresos = 5  Factura Asociada = 6  Folio Cerrado =7', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Responsable de la Cuota de Recuperacion  1 - Ninguno  2 - Paciente  3 - Tercero', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'ResponsibleRecoveryFee';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de liquidacion  1 - Pago por Servicios  2 - Capitacion  3 - Factura Global  4 - Capitacion Global  5 - Pago Global Prospectivo - PGP', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'LiquidationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el tipo de folio  1 - EAPB con contrato  2 - EAPB sin contrato  3 - Particulares  4 - Aseguradoras', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'FolioType';

