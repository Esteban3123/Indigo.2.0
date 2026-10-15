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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del folio cuenta madre enlazado, al folio aseguradora y paciente', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'RevenueControlDetailMasterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '0- No es cuenta Madre  1- folio Cuenta Madre  2- Cuenta Madre Separada (Entidad)  3- Cuenta Madre Origen  4- Cuenta Madre Separada (Paciente)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'IsMasterAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'TRM con el que se calculo el valor', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'TRMValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la Moneda Parametrizada.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'CurrencyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del tercero responsable de la cuota del paciente.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'PatientQuotaResponsibleThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del concepto de Estado de Folio', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'StatusFolioId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del Documento   Registrado = 1  Facturado = 2  Bloqueado = 3  Anulado = 4  Reconocimiento Ingresos = 5  Factura Asociada = 6  Folio Cerrado =7', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Diagnostico de Egreso', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'OutputDiagnosis';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si es un Corte de Cuenta', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'IsCutAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la Fecha de Egreso o de Corte', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'OutputDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si el folio es No POS', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'InvoiceCategoryId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion del Folio', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'Observation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el valor del bono que le corresponde al paciente', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'ValueVoucher';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de la cuota moderadora que se cobra en el folio', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'ValueFeeModerator';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de la cuota de recuperacion que se conbro en la factura o folio', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'ValueCopay';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor real cobrado al paciente por concepto de cuota de recuperacion con descuento incluido.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'TotalPatientWithDiscount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Porcentaje de descuento aplicado a la cuota de recuperacion a cargo del paciente', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'PatientDiscountPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de descuento aplicado a la cuota de recuperacion a cargo del paciente', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'PatientDiscount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Corresponde al total de la cuota de recuperacion en el folio, es decir que es la sumatoria del (SubtotalPatientSalesPrice)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'TotalPatientSalesPrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Responsable de la Cuota de Recuperacion  1 - Ninguno  2 - Paciente  3 - Tercero', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'ResponsibleRecoveryFee';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Total Del Folio', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'TotalFolio';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del grupo de atencion', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'CareGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tercero de la entidad', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tercero con quien se realiza el contrato', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'HealthAdministratorId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la entidad de contrato', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'ContractEntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de liquidacion  1 - Pago por Servicios  2 - Capitacion  3 - Factura Global  4 - Capitacion Global  5 - Pago Global Prospectivo - PGP', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'LiquidationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el tipo de folio  1 - EAPB con contrato  2 - EAPB sin contrato  3 - Particulares  4 - Aseguradoras', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'FolioType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el orden en el que van a estar organizados los folios', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'FolioOrder';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el id de la autorizacion de facturacion, permite null ya que se debe crear desde ordene de servicios, pero en liquidacion se debe pedir de forma obligatoria ya que a través de esta se saca el numero de factura', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'BillingAuthorizationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera de control de folio', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'RevenueControlId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'Id';

