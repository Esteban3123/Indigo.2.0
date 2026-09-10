CREATE TABLE [Common].[ThirdParty] (
    [Id]                                  INT                                                                      IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PersonId]                            INT                                                                      NOT NULL,
    [Nit]                                 VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Nit_Ofuscado", 0)')    NOT NULL,
    [DigitVerification]                   VARCHAR (1)                                                              CONSTRAINT [DF_ThirdParty_DigitVerification] DEFAULT ((1)) NULL,
    [Name]                                VARCHAR (405) MASKED WITH (FUNCTION = 'partial(0, "Third_Ofuscado", 0)') NOT NULL,
    [PersonType]                          TINYINT                                                                  NOT NULL,
    [RetentionType]                       TINYINT                                                                  NOT NULL,
    [ContributionType]                    TINYINT                                                                  NOT NULL,
    [StateEnterpriseType]                 TINYINT                                                                  CONSTRAINT [DF_ThirdParty_StateEnterpriseType] DEFAULT ((0)) NOT NULL,
    [IVARetentionAccountPayableConceptId] INT                                                                      NULL,
    [Ica]                                 BIT                                                                      NOT NULL,
    [IcaPercentage]                       NUMERIC (5, 3)                                                           NOT NULL,
    [IcaTop]                              BIT                                                                      NOT NULL,
    [IcaTopValue]                         NUMERIC (18, 2)                                                          NOT NULL,
    [EntityCode]                          VARCHAR (15)                                                             NULL,
    [EconomicActivityId]                  INT                                                                      NULL,
    [Class]                               TINYINT                                                                  NULL,
    [DigitalSignature]                    VARBINARY (MAX)                                                          NULL,
    [CodeCIIU]                            VARCHAR (10)                                                             NULL,
    [State]                               BIT                                                                      CONSTRAINT [DF_ThirdParty_State] DEFAULT ((0)) NOT NULL,
    [CreationDate]                        DATETIME                                                                 NOT NULL,
    [UserId]                              INT                                                                      NOT NULL,
    [HandlesBranchOffice]                 BIT                                                                      CONSTRAINT [DF_ThirdParty_HandlesBranchOffice] DEFAULT ((0)) NOT NULL,
    [CodeDivipola]                        VARCHAR (20)                                                             NULL,
    [IVARetentionConceptId]               INT                                                                      NULL,
    [ElectronicBiller]                    BIT                                                                      CONSTRAINT [DF__ThirdPart__Elect__722D4884] DEFAULT ((0)) NOT NULL,
    [CreationUser]                        VARCHAR (20)                                                             NULL,
    [ModificationUser]                    VARCHAR (20)                                                             NULL,
    [ModificationDate]                    DATETIME                                                                 NULL,
    CONSTRAINT [PK_ThirdParty__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ThirdParty_AccountPayableConcepts] FOREIGN KEY ([IVARetentionAccountPayableConceptId]) REFERENCES [Payments].[AccountPayableConcepts] ([Id]),
    CONSTRAINT [FK_ThirdParty_EconomicActivity] FOREIGN KEY ([EconomicActivityId]) REFERENCES [Common].[EconomicActivity] ([Id]),
    CONSTRAINT [FK_ThirdParty_Person] FOREIGN KEY ([PersonId]) REFERENCES [Common].[Person] ([Id]),
    CONSTRAINT [FK_ThirdParty_RetentionConcepts] FOREIGN KEY ([IVARetentionConceptId]) REFERENCES [GeneralLedger].[RetentionConcepts] ([Id])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [Common].[ThirdParty].[Nit]
    WITH (LABEL = 'Confidential - Financial', INFORMATION_TYPE = 'Financial');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [Common].[ThirdParty].[Name]
    WITH (LABEL = 'Confidential', INFORMATION_TYPE = 'Name');




GO

CREATE NONCLUSTERED INDEX [IX_ThirdParty_AccountReceivableDateByAge]
    ON [Common].[ThirdParty]([Nit] ASC)
    INCLUDE([Name], [PersonId], [PersonType]);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_ThirdParty__Nit]
    ON [Common].[ThirdParty]([Nit] ASC);


GO
CREATE NONCLUSTERED INDEX [nci_wi_ThirdParty_E0A415FF1E7850F34015A768B185D078]
    ON [Common].[ThirdParty]([PersonId] ASC)
    INCLUDE([Name], [Nit]);


GO
-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-02-14
-- Description:	Al crear un paciente en el HIS se debe crear el cliente
-- =============================================
CREATE TRIGGER [Common].[InsertCustomer]
   ON  [Common].[ThirdParty]
   AFTER INSERT
AS 
BEGIN
	SET NOCOUNT ON;
    
	/******** INSERTAMOS CLIENTE SI NO EXISTE Y ES UN PACIENTE ********/
	INSERT INTO [Common].[Customer]
    (
		[Nit],[Name],[EPSCode],[ThirdPartyId],[MainAccountReceivableId],[Term],[State],[CreationUser],[CreationDate]
	)
	SELECT	tp.Nit, 
			tp.Name, 
			'' EPSCode,
			tp.Id ThirdPartyId, 
			IIF(cg.CareGroupType = 3, cas.AccountParticularId, cas.AccountRecoveryFeeId) MainAccountId, 
			30 Term, 
			1 State, 
			'999' CreationUser, 
			[Common].[GETDATE]() CreationDate
	FROM dbo.INPACIENT p
	JOIN INSERTED tp ON p.IPCODPACI = tp.Nit
	JOIN Contract.CareGroup cg ON CAST(p.GENCAREGROUP AS VARCHAR(50)) = cg.Code
	JOIN Contract.ContractAccountingStructure cas ON cg.ContractAccountingStructureId = cas.Id
	LEFT JOIN Common.Customer c ON tp.Id = c.ThirdPartyId
	WHERE c.Id IS NULL AND IIF(cg.CareGroupType = 3, cas.AccountParticularId, cas.AccountRecoveryFeeId) IS NOT NULL
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de última modificación del registro del tercero (DATETIME). Auditoría de cambios.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que realizó la última modificación del tercero (VARCHAR 20). Trazabilidad de cambios.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de modificación', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó el registro del tercero (VARCHAR 20). Auditoría de creación.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de creación ', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si el tercero está obligado a facturar electrónicamente (BIT): 0=No responsable de facturación electrónica, 1=Responsable de facturación electrónica. Cumplimiento normativo DIAN.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'ElectronicBiller';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece si al tercero debe de generarle un documento soporte electrónico. 0 - No responsable de facturar electronicamente, 1 - Responsable de facturar electronicamente.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'ElectronicBiller';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'ElectronicBiller';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID del concepto de retención de IVA para ventas (INT, FK→RetentionConcepts). Diferenciado del concepto de CXP. Aplica cuando el tercero es vendedor.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'IVARetentionConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del concepto de retención. Concepto de Retencion IVA para que este aplique a Ventas ya que el existente esta relacionado con Conceptos de CXP y aunque se maneja el mismo porcentaje tanto en Ventas como en CXP se debe dejar por separado ya tercero no siempre cumple la condicion de vendedor y comprador', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'IVARetentionConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'IVARetentionConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código DIVIPOLA de ubicación geográfica del tercero (VARCHAR 20). Clasificación administrativa territorial.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'CodeDivipola';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Divipola ', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'CodeDivipola';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'CodeDivipola';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si el tercero maneja sucursales u oficinas (BIT): 0=No, 1=Sí. Relevante para descentralización operativa.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'HandlesBranchOffice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Escargado sucursal ', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'HandlesBranchOffice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'HandlesBranchOffice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID del usuario asociado al tercero (INT, campo oculto). Relación de propiedad/responsabilidad del registro.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'UserId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo oculto**', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'UserId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'UserId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro del tercero (DATETIME). Auditoría e historial.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creacion de tercero', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado operativo del tercero (BIT): 0=Inactivo, 1=Activo. Habilita/deshabilita para transacciones.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del tercero 1 - Activo 0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CIIU - Clasificación Internacional Industrial Uniforme (VARCHAR 10). Identifica sector económico; solicitado pero no obligatorio.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'CodeCIIU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Codigo CIIU, Clasificacion Internacional Industrial Uniforme, Siempre se pide este codigo pero no es obligatorio', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'CodeCIIU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'CodeCIIU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Firma digital del tercero en formato binario (VARBINARY MAX). Documento electrónico firmado.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'DigitalSignature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Frima digital', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'DigitalSignature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'DigitalSignature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación del tercero persona jurídica (TINYINT): 1=Nacional, 2=Extranjero. Solo se solicita si PersonType=Jurídico.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'Class';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Clase del tercero, solo se solicita si es de tipo juridico  1 - Nacional  2 - Extranjero', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'Class';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'Class';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de la actividad económica del tercero (INT, FK→EconomicActivity). Solo para personas jurídicas.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'EconomicActivityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la actividad economica, esta solo se solicita si el tercero es de tipo juridico', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'EconomicActivityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'EconomicActivityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador de entidad estatal (VARCHAR 15). Requerido solo si ContributionType=Empresa Estatal.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'EntityCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de  la entidad, solo se solicita si el tipo de contribuyente es Empresa Estatal', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'EntityCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'EntityCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor máximo (tope) de retención de ICA en pesos (NUMERIC 18,2). Límite de aplicación del impuesto.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'IcaTopValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de tope', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'IcaTopValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'IcaTopValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si se aplica tope de retención ICA (BIT): 0=No, 1=Sí. Controla límite máximo de retención.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'IcaTop';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Maneja Tope de ReteICA', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'IcaTop';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'IcaTop';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de Impuesto de Industria y Comercio ICA a retener (NUMERIC 5,3). Tarifa tributaria local.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'IcaPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje ICA', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'IcaPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'IcaPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si el tercero está obligado a retención de ICA (BIT): 0=No, 1=Sí. Obligación tributaria municipal.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'Ica';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Maneja ICA', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'Ica';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'Ica';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID del concepto de cuentas por pagar para retención de IVA (INT, FK→AccountPayableConcepts). Solo para ContributionType=Común(1); debe ser concepto de retención específico.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'IVARetentionAccountPayableConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del concepto de cuentas por pagar para la Retencion al IVA, Este concepto de pagos debe de ser de tipo de retencion y debe ser de tipo Especifico, Este campo solo se llena si el Tipo de Contribuyente es Comun(1)', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'IVARetentionAccountPayableConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'IVARetentionAccountPayableConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de empresa estatal (TINYINT): 0=No aplica, 1=Municipal, 2=Departamental, 3=Distrital. Solo si es entidad pública.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'StateEnterpriseType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Tipo de Empesa estatal  0 - No Aplica  1 - Municipal  2 - Departamental  3 - Distrital', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'StateEnterpriseType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'StateEnterpriseType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de contribuyente (TINYINT): 0=No responsable IVA, 1=Responsable IVA, 2=Empresa estatal, 3=Gran contribuyente, 4=Régimen simple, 5=Exento. Clasificación tributaria.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'ContributionType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de contribuyente 0 - No responsable de Iva, 1 - Responsables de Iva, 2 - Empresa estatal, 3 - Gran Contribuyente, 4 - Regimen Simple,  5- Exento', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'ContributionType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'ContributionType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de retención aplicable (TINYINT): 0=Ninguna, 1=Exento de retención, 2=Sujeto a retención, 3=Autoretenedor. Obligación de retención fiscal.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'RetentionType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de retencion 0 - Ninguna 1 - Exento de retencion 2 - Hace Retencion 3 - Autoretenedor', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'RetentionType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'RetentionType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de persona (TINYINT): 1=Natural, 2=Jurídica. Naturaleza legal del tercero; determina campos requeridos.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'PersonType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de persona 1 - Natural, 2 - Jurídico', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'PersonType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'PersonType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o razón social del tercero (VARCHAR 300, enmascarado PII). Identificador comercial del proveedor/cliente.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de tercero', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dígito de verificación del NIT (VARCHAR 1). Validación de checksum del NIT del tercero.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'DigitVerification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el digito de verificacion para el nit ingresado', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'DigitVerification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'DigitVerification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'NIT del tercero (VARCHAR 25, enmascarado PII). Número de identificación tributaria; identificador primario en Colombia.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'Nit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nit de tercero', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'Nit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'Nit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de la persona asociada (INT, FK→Person). Vinculación con datos personales en tabla Person.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'PersonId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la Persona', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'PersonId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'PersonId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID autoincremental único del tercero (INT IDENTITY). Clave primaria de la tabla ThirdParty.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'Id';


GO
CREATE NONCLUSTERED INDEX [IX_ThirdParty_PersonType]
    ON [Common].[ThirdParty]([PersonType] ASC)
    INCLUDE([Id], [Nit], [Name], [PersonId]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Terceros registrados en el sistema: proveedores, contratistas, aseguradoras, entidades o cualquier empresa/persona externa con la que la organización tiene relaciones comerciales, tributarias o contractuales. Incluye datos de identificación fiscal (NIT), configuración de retenciones e IVA, actividad económica y facturación electrónica.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty';
