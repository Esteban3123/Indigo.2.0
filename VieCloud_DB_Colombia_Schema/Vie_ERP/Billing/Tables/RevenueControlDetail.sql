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
ALTER TABLE [Billing].[RevenueControlDetail] ENABLE CHANGE_TRACKING WITH (TRACK_COLUMNS_UPDATED = OFF);




GO
ALTER TABLE [Billing].[RevenueControlDetail] NOCHECK CONSTRAINT [CK_RevenueControlDetail];





GO
ALTER TABLE [Billing].[RevenueControlDetail] NOCHECK CONSTRAINT [CK_RevenueControlDetail];


GO



GO



GO



GO



GO



GO



GO



GO



GO



GO



GO



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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT FK | Identificador del folio cuenta madre enlazado (referencias aseguradora y paciente). Permite consolidación de folios relacionados en jerarquía de facturación. NULL si es folio independiente.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'RevenueControlDetailMasterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del folio cuenta madre enlazado, al folio aseguradora y paciente', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'RevenueControlDetailMasterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'RevenueControlDetailMasterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TINYINT | Tipo de cuenta madre: 0=No es madre, 1=Folio cuenta madre, 2=Cuenta madre separada (entidad), 3=Cuenta madre origen, 4=Cuenta madre separada (paciente). Define rol jerárquico en liquidación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'IsMasterAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'0- No es cuenta Madre  1- folio Cuenta Madre  2- Cuenta Madre Separada (Entidad)  3- Cuenta Madre Origen  4- Cuenta Madre Separada (Paciente)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'IsMasterAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'IsMasterAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'NUMERIC(20,5) | Tasa de cambio representativo del mercado (TRM) aplicada en cálculo de valores del folio. Por defecto 1. Usado para conversión de moneda en facturación internacional.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'TRMValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'TRM con el que se calculo el valor', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'TRMValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'TRMValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT FK | Identificador de moneda parametrizada (dólar, peso, euro). Referencia a catálogo de monedas para conversión con TRM.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'CurrencyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la Moneda Parametrizada.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'CurrencyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'CurrencyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT FK | Identificador del tercero (aseguradora, entidad) responsable de cuota de recuperación del paciente. Vincula entidad pagadora de copago/cuota moderadora.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'PatientQuotaResponsibleThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tercero responsable de la cuota del paciente.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'PatientQuotaResponsibleThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'PatientQuotaResponsibleThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT FK | Identificador del concepto de estado del folio (registrado, facturado, bloqueado, etc.). Referencia tabla ConceptsCausesStatusFolio para estado actual.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'StatusFolioId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del concepto de Estado de Folio', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'StatusFolioId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'StatusFolioId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TIMESTAMP | Marca temporal automática: registra instante exacto de creación, modificación o evento en el folio. Usado para auditoría y control de versiones.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATETIME | Fecha y hora última modificación del registro de folio. NULL si nunca fue editado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(20) | Usuario que realizó última modificación del folio. NULL si nunca fue editado. Auditoría de cambios.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATETIME | Fecha y hora creación del registro de folio. Marca inicio del ciclo de facturación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(20) | Usuario que creó el registro de folio. Trazabilidad inicial de documento.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TINYINT | Estado del documento folio: 1=Registrado, 2=Facturado, 3=Bloqueado, 4=Anulado, 5=Reconocimiento ingresos, 6=Factura asociada, 7=Folio cerrado. Ciclo de vida de facturación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del Documento   Registrado = 1  Facturado = 2  Bloqueado = 3  Anulado = 4  Reconocimiento Ingresos = 5  Factura Asociada = 6  Folio Cerrado =7', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'CHAR(4) | Código diagnóstico CIE-10 de egreso del paciente. NULL si no aplica. Especifica motivo cierre de atención.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'OutputDiagnosis';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Diagnostico de Egreso', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'OutputDiagnosis';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'OutputDiagnosis';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT | Indicador booleano: 1=es corte de cuenta (liquidación periódica), 0=folio normal. Distingue cortes administrativos en facturación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'IsCutAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si es un Corte de Cuenta', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'IsCutAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'IsCutAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATETIME | Fecha egreso del paciente o fecha de corte de cuenta. NULL si atención abierta. Marca fin de período de facturación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'OutputDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la Fecha de Egreso o de Corte', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'OutputDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'OutputDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT FK | Identificador de categoría de factura (indica si folio es No POS o categoría especial). NULL si es folio POS estándar.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'InvoiceCategoryId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si el folio es No POS', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'InvoiceCategoryId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'InvoiceCategoryId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(MAX) | Descripción o nota libre del folio. Observaciones administrativas, clínicas o de liquidación. TEXT ilimitado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'Observation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion del Folio', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'Observation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'Observation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'NUMERIC(20,2) | Valor en moneda local de bono/voucher asignado al paciente para reducción de cuota. Por defecto 0.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'ValueVoucher';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el valor del bono que le corresponde al paciente', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'ValueVoucher';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'ValueVoucher';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'NUMERIC(20,2) | Valor monetario de cuota moderadora cobrada en folio (copago porcentual). Por defecto 0. Aporte del paciente asegurado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'ValueFeeModerator';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de la cuota moderadora que se cobra en el folio', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'ValueFeeModerator';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'ValueFeeModerator';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'NUMERIC(20,2) | Valor monetario de cuota de recuperación (copago fijo) cobrada en factura/folio al paciente. Por defecto 0.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'ValueCopay';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de la cuota de recuperacion que se conbro en la factura o folio', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'ValueCopay';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'ValueCopay';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'NUMERIC(20,2) | Total final cobrado al paciente por cuota de recuperación, con descuento ya aplicado. Valor neto a pagar.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'TotalPatientWithDiscount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor real cobrado al paciente por concepto de cuota de recuperacion con descuento incluido.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'TotalPatientWithDiscount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'TotalPatientWithDiscount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'NUMERIC(5,2) | Porcentaje descuento aplicado a cuota de recuperación del paciente (ej: 10.5%). Por defecto 0.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'PatientDiscountPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje de descuento aplicado a la cuota de recuperacion a cargo del paciente', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'PatientDiscountPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'PatientDiscountPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'NUMERIC(20,2) | Valor absoluto descuento en moneda local sobre cuota de recuperación del paciente. Por defecto 0.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'PatientDiscount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de descuento aplicado a la cuota de recuperacion a cargo del paciente', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'PatientDiscount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'PatientDiscount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'NUMERIC(20,2) | Total cuota de recuperación antes de descuento (suma componentes de copago). Valor bruto a paciente.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'TotalPatientSalesPrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Corresponde al total de la cuota de recuperacion en el folio, es decir que es la sumatoria del (SubtotalPatientSalesPrice)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'TotalPatientSalesPrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'TotalPatientSalesPrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TINYINT | Responsable de pago cuota recuperación: 1=Ninguno, 2=Paciente, 3=Tercero (aseguradora). Define quién cobra/paga copago.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'ResponsibleRecoveryFee';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Responsable de la Cuota de Recuperacion  1 - Ninguno  2 - Paciente  3 - Tercero', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'ResponsibleRecoveryFee';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'ResponsibleRecoveryFee';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'NUMERIC(20,2) | Monto total del folio en moneda local (todos servicios, procedimientos, medicamentos). Valor facturado por IPS.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'TotalFolio';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Total Del Folio', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'TotalFolio';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'TotalFolio';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT FK | Identificador del grupo de atención/área funcional (urgencias, hospitalización, consulta externa). Vincula folio a unidad operativa.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'CareGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del grupo de atencion', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'CareGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'CareGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT FK | Identificador del tercero/entidad facturadora (IPS, clínica, laboratorio). Proveedor de servicios sanitarios.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tercero de la entidad', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT FK | Identificador del tercero con quien se realiza el contrato (EAPB, aseguradora, entidad pagadora). Contraparte comercial.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'HealthAdministratorId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tercero con quien se realiza el contrato', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'HealthAdministratorId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'HealthAdministratorId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT FK | Identificador entidad de contrato (cliente administrativo). Vincula folio a relación contractual específica.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'ContractEntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la entidad de contrato', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'ContractEntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'ContractEntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TINYINT | Tipo de liquidación: 1=Pago por servicios, 2=Capitación, 3=Factura global, 4=Capitación global, 5=PGP (Pago global prospectivo). Modelo financiero.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'LiquidationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de liquidacion  1 - Pago por Servicios  2 - Capitacion  3 - Factura Global  4 - Capitacion Global  5 - Pago Global Prospectivo - PGP', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'LiquidationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'LiquidationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TINYINT | Tipo de folio: 1=EAPB con contrato, 2=EAPB sin contrato, 3=Particulares, 4=Aseguradoras. Clasifica relación comercial.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'FolioType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el tipo de folio  1 - EAPB con contrato  2 - EAPB sin contrato  3 - Particulares  4 - Aseguradoras', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'FolioType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'FolioType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TINYINT | Número de orden secuencial para organizar folios dentro de lote. Define ordenamiento en reportes/listados de facturación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'FolioOrder';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el orden en el que van a estar organizados los folios', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'FolioOrder';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'FolioOrder';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT FK | Identificador de autorización de facturación (genera número de factura). NULL en órdenes, obligatorio en liquidación. Vincula folio a aprobación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'BillingAuthorizationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el id de la autorizacion de facturacion, permite null ya que se debe crear desde ordene de servicios, pero en liquidacion se debe pedir de forma obligatoria ya que a través de esta se saca el numero de factura', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'BillingAuthorizationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'BillingAuthorizationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT FK | Identificador de cabecera/control de ingresos del folio. Agrupa detalle en estructura de facturación maestra.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'RevenueControlId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera de control de folio', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'RevenueControlId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'RevenueControlId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT PK | Identificador único autonumérico (identity). Clave primaria de detalle de folio en tabla RevenueControlDetail.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de los folios de facturación asociados a un control de ingresos. Registra el desglose por tipo de folio, entidad contratante, tercero responsable, cuotas moderadoras, copagos, descuentos al paciente y totales liquidados, permitiendo trazabilidad de la facturación por atención.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlDetail';

GO
CREATE NONCLUSTERED INDEX [IX_RevenueControlDetail_RevenueControlId]
    ON [Billing].[RevenueControlDetail]([RevenueControlId] ASC)
    INCLUDE([Status], [IsMasterAccount], [FolioType]);
