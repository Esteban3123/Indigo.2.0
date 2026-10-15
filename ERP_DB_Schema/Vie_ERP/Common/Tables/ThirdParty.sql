CREATE TABLE [Common].[ThirdParty] (
    [Id]                                  INT                                                                      IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PersonId]                            INT                                                                      NOT NULL,
    [Nit]                                 VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Nit_Ofuscado", 0)')    NOT NULL,
    [DigitVerification]                   VARCHAR (1)                                                              CONSTRAINT [DF_ThirdParty_DigitVerification] DEFAULT ((1)) NULL,
    [Name]                                VARCHAR (300) MASKED WITH (FUNCTION = 'partial(0, "Third_Ofuscado", 0)') NOT NULL,
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
ALTER TABLE [Common].[ThirdParty] NOCHECK CONSTRAINT [FK_ThirdParty_AccountPayableConcepts];


GO
ALTER TABLE [Common].[ThirdParty] NOCHECK CONSTRAINT [FK_ThirdParty_EconomicActivity];


GO
ALTER TABLE [Common].[ThirdParty] NOCHECK CONSTRAINT [FK_ThirdParty_Person];


GO
ALTER TABLE [Common].[ThirdParty] NOCHECK CONSTRAINT [FK_ThirdParty_RetentionConcepts];


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
CREATE INDEX IX_ThirdParty_PersonType
    ON Common.ThirdParty (PersonType)
    INCLUDE (Id, Nit, Name, PersonId);
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario de modificación', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario de creación ', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Establece si al tercero debe de generarle un documento soporte electrónico. 0 - No responsable de facturar electronicamente, 1 - Responsable de facturar electronicamente.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'ElectronicBiller';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del concepto de retención. Concepto de Retencion IVA para que este aplique a Ventas ya que el existente esta relacionado con Conceptos de CXP y aunque se maneja el mismo porcentaje tanto en Ventas como en CXP se debe dejar por separado ya tercero no siempre cumple la condicion de vendedor y comprador', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'IVARetentionConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Divipola ', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'CodeDivipola';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Escargado sucursal ', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'HandlesBranchOffice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'campo oculto**', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'UserId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de creacion de tercero', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del tercero 1 - Activo 0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Codigo CIIU, Clasificacion Internacional Industrial Uniforme, Siempre se pide este codigo pero no es obligatorio', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'CodeCIIU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Frima digital', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'DigitalSignature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Clase del tercero, solo se solicita si es de tipo juridico  1 - Nacional  2 - Extranjero', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'Class';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la actividad economica, esta solo se solicita si el tercero es de tipo juridico', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'EconomicActivityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de  la entidad, solo se solicita si el tipo de contribuyente es Empresa Estatal', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'EntityCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de tope', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'IcaTopValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Maneja Tope de ReteICA', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'IcaTop';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Porcentaje ICA', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'IcaPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Maneja ICA', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'Ica';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del concepto de cuentas por pagar para la Retencion al IVA, Este concepto de pagos debe de ser de tipo de retencion y debe ser de tipo Especifico, Este campo solo se llena si el Tipo de Contribuyente es Comun(1)', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'IVARetentionAccountPayableConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Tipo de Empesa estatal  0 - No Aplica  1 - Municipal  2 - Departamental  3 - Distrital', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'StateEnterpriseType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de contribuyente 0 - No responsable de Iva, 1 - Responsables de Iva, 2 - Empresa estatal, 3 - Gran Contribuyente, 4 - Regimen Simple,  5- Exento', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'ContributionType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de retencion 0 - Ninguna 1 - Exento de retencion 2 - Hace Retencion 3 - Autoretenedor', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'RetentionType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de persona 1 - Natural, 2 - Jurídico', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'PersonType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de tercero', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el digito de verificacion para el nit ingresado', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'DigitVerification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nit de tercero', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'Nit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la Persona', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'PersonId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdParty', @level2type = N'COLUMN', @level2name = N'Id';

