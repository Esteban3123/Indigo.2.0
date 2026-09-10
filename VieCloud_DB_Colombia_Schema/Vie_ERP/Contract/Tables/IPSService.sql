CREATE TABLE [Contract].[IPSService] (
    [Id]                               INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                             VARCHAR (20)    NOT NULL,
    [Name]                             VARCHAR (300)   NOT NULL,
    [ServiceManual]                    TINYINT         NOT NULL,
    [ServiceClass]                     TINYINT         NULL,
    [BillingConceptId]                 INT             NULL,
    [AssociatedMaterialIPSServiceId]   INT             NULL,
    [ServiceType]                      TINYINT         NOT NULL,
    [Presentation]                     TINYINT         NOT NULL,
    [SurgicalGroupId]                  INT             NULL,
    [UVRNumber]                        INT             CONSTRAINT [DF_IPSService_UVRNumber] DEFAULT ((0)) NOT NULL,
    [Score]                            NUMERIC (18, 2) CONSTRAINT [DF_IPSService_Score] DEFAULT ((0)) NOT NULL,
    [ApplyChangeScore]                 BIT             CONSTRAINT [DF_IPSService_ApplyChangeScore] DEFAULT ((0)) NOT NULL,
    [NewScore]                         NUMERIC (18, 2) CONSTRAINT [DF_IPSService_NewScore] DEFAULT ((0)) NOT NULL,
    [InPatientRecoveryFeeType]         TINYINT         CONSTRAINT [DF_IPSService_InPatientRecoveryFeeType] DEFAULT ((1)) NOT NULL,
    [OutPatientRecoveryFeeType]        TINYINT         CONSTRAINT [DF_IPSService_OutPatientRecoveryFeeType] DEFAULT ((1)) NOT NULL,
    [AuthorizationLevel]               TINYINT         NOT NULL,
    [ContributionsWeeks]               INT             NOT NULL,
    [Procedure]                        TINYINT         NOT NULL,
    [SubattentionCode]                 TINYINT         NOT NULL,
    [MinimunAgeUnit]                   TINYINT         NOT NULL,
    [MinimunAge]                       INT             NOT NULL,
    [MaximumAgeUnit]                   TINYINT         NOT NULL,
    [MaximumAge]                       INT             NOT NULL,
    [InMale]                           BIT             NOT NULL,
    [InFemale]                         BIT             NOT NULL,
    [ChildbirthAbortion]               BIT             NOT NULL,
    [POS]                              BIT             NOT NULL,
    [ComplexityLevel]                  TINYINT         NOT NULL,
    [PromotionAndPrevention]           BIT             NOT NULL,
    [PromotionAndPreventionActivities] VARCHAR (MAX)   NULL,
    [SurgeryArtroscopica]              BIT             NOT NULL,
    [PathologyService]                 BIT             NOT NULL,
    [Status]                           BIT             NOT NULL,
    [CreationUser]                     VARCHAR (20)    NOT NULL,
    [CreationDate]                     DATETIME        NOT NULL,
    [ModificationUser]                 VARCHAR (20)    NULL,
    [ModificationDate]                 DATETIME        NULL,
    [IVAId]                            INT             NULL,
    [SalesLedgerAccountId]             INT             NULL,
    [TaxedProduct]                     BIT             CONSTRAINT [DF__IPSServic__Taxed__5A60ABEB] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_IPSService__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_IPSService_BillingConcept] FOREIGN KEY ([BillingConceptId]) REFERENCES [Billing].[BillingConcept] ([Id]),
    CONSTRAINT [FK_IPSService_GeneralLedgerIVA] FOREIGN KEY ([IVAId]) REFERENCES [GeneralLedger].[GeneralLedgerIVA] ([Id]),
    CONSTRAINT [FK_IPSService_IPSService] FOREIGN KEY ([AssociatedMaterialIPSServiceId]) REFERENCES [Contract].[IPSService] ([Id]),
    CONSTRAINT [FK_IPSService_MainAccounts] FOREIGN KEY ([SalesLedgerAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_IPSService_SurgicalGroup] FOREIGN KEY ([SurgicalGroupId]) REFERENCES [Contract].[SurgicalGroup] ([Id])
);




GO



GO



GO



GO



GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_IPSService__Code]
    ON [Contract].[IPSService]([Code] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) que especifica si el servicio está gravado con IVA; determina aplicación de impuesto en facturación.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'TaxedProduct';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Servicio gravado, con impuesto IVA', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'TaxedProduct';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'TaxedProduct';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) de la cuenta contable de ventas en libro mayor; referencia a MainAccounts para registro contable.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'SalesLedgerAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta contable de ventas', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'SalesLedgerAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'SalesLedgerAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) del tipo de IVA parametrizado; referencia a GeneralLedgerIVA para aplicar alícuota tributaria.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'IVAId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del IVA parametrizado.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'IVAId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'IVAId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de la última modificación del registro del servicio IPS.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (VARCHAR 20) que realizó la última modificación del servicio IPS.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de creación del registro del servicio IPS en el sistema.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (VARCHAR 20) que creó el registro del servicio IPS.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del servicio (BIT): 1=Activo, 0=Inactivo; habilita/deshabilita disponibilidad en contrataciones.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado (Activo = 1, Inactivo = 0)', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) si el servicio es de patología; solo válido cuando Procedure=Laboratorio; referencia a análisis, diagnóstico clínico.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'PathologyService';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'establece si es un servicio de patologia, solo puese ser patologica si el procedimiento es Laboratorio', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'PathologyService';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'PathologyService';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) si la intervención es artroscópica; solo válido cuando Presentation=Quirúrgico o Paquete.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'SurgeryArtroscopica';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece que es una cirugia artroscopica, solo puede ser artroscopica si la presentacion es quirurgica o paquete', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'SurgeryArtroscopica';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'SurgeryArtroscopica';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Texto (VARCHAR MAX) que describe actividades específicas de promoción y prevención; salud pública, educación sanitaria.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'PromotionAndPreventionActivities';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Actividades del servicio de promocion y prevencion', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'PromotionAndPreventionActivities';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'PromotionAndPreventionActivities';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) que marca si el servicio corresponde a promoción y prevención de salud.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'PromotionAndPrevention';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece si es de promocion y prevención', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'PromotionAndPrevention';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'PromotionAndPrevention';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nivel de complejidad (TINYINT): 1=Baja, 2=Media, 3=Alta; orienta asignación de recursos y autorización.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'ComplexityLevel';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece el nivel de complejidad  1 - Baja  2 - Media  3 - Alta', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'ComplexityLevel';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'ComplexityLevel';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) que especifica si el servicio pertenece al Plan Obligatorio de Salud (POS).', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'POS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si el servicio petenece al POS', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'POS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'POS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) que especifica si el servicio corresponde a parto, cesárea o interrupción de embarazo.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'ChildbirthAbortion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si el servicio corresponde a un parto o aun aborto', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'ChildbirthAbortion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'ChildbirthAbortion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) que identifica si el servicio aplica para población femenina; restricción por sexo.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'InFemale';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo para identificar si se aplica al sexo femenino', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'InFemale';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'InFemale';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) que identifica si el servicio aplica para población masculina; restricción por sexo.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'InMale';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo para identificar si se aplica al sexo masculino', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'InMale';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'InMale';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad máxima permitida para acceder al servicio (INT); restringe elegibilidad por rango etario.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'MaximumAge';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Edad Maxima para acceder al servicio', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'MaximumAge';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'MaximumAge';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de medida edad máxima (TINYINT): 1=Años, 2=Meses, 3=Días; define granularidad de restricción.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'MaximumAgeUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad de medida para la edad maxima  1 - Años  2 - Meses  3 - Dias', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'MaximumAgeUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'MaximumAgeUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad mínima requerida para acceder al servicio (INT); restringe elegibilidad por rango etario.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'MinimunAge';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Edad minima para este servicio', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'MinimunAge';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'MinimunAge';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de medida edad mínima (TINYINT): 1=Años, 2=Meses, 3=Días; define granularidad de restricción.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'MinimunAgeUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad de medida para la edad minima  1 - Años  2 - Meses  3 - Dias', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'MinimunAgeUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'MinimunAgeUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de subtipo de atención (TINYINT 1-34): habitación, UCI, consulta, cirugía, laboratorio, imagenología, medicamentos, ambulancia, factura integral; especifica componente de facturación.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'SubattentionCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo subatencion    1 - Ninguno  2 - Estancia Individual  3 - Habitacion compartida  4 - UCI Adultos  5 - UCI Neonatal  6 - UCI Cuidados Medianos  7 - Incubadora  8 - Consulta Medica General  9 - Consulta Especialista  10 - Interconsulta  11 - Visitas Hospitalarias  12 - Honorarios Cirujanos  13 - Honorarios Anestesia  14 - Honorarios Ayudantia  15 - Honorarios Instrumentacion  16 - Derechos Sala  17 - Derecho Anestesia  18 - Derecho Equipo  19 - Insumos Hospitalarios  20 - Material Quirurgico  21 - Medicamentos  22 - Oxigeno  23 - Laboratorio  24 - Radiologia  25 - Tomografias  26 - Medicina Nuclear  27 - Resonancia Magnetica  28 - Examenes Complementarios  29 - Examenes Vasculares  30 - Hemodinamia  31 - Banco Sangre  32 - Terapias  33 - Ambulancia  34 - Factura Integral', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'SubattentionCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'SubattentionCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de procedimiento (TINYINT): 1=Diagnóstico, 2=Laboratorio, 3=Odontología, 4=Consulta-Urgencias, 5=Hospitalización; categoriza servicio clínico.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'Procedure';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Procedimiento del servicio  1 - Diagnostico  2 - Laboratorio  3 - Odontologia  4 - Consulta - Urgencias  5 - Hospitalizacion', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'Procedure';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'Procedure';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Semanas de cotización requeridas (INT) para que afiliado acceda al servicio; requisito de contribución.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'ContributionsWeeks';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Semanas cotizadas para acceder al servicio', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'ContributionsWeeks';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'ContributionsWeeks';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nivel de autorización (TINYINT 1-9) requerido para solicitar el servicio; determina complejidad de aprobación.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'AuthorizationLevel';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece el nivel de autorizacion en rango es de uno a nueve', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'AuthorizationLevel';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'AuthorizationLevel';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de cuota de recuperación ambulatoria (TINYINT): 1=Ninguna, 2=Moderadora, 3=Copago, 4=Bono, 5=Franquicia, 6=Otra; define participación del paciente.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'OutPatientRecoveryFeeType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Tipo de Cuota de Recuperacion Ambulatorio  1. Ninguna  2. Cuota Moderadora  3. Copago  4. Bono  5. Franquicia  6. Otra', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'OutPatientRecoveryFeeType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'OutPatientRecoveryFeeType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de cuota de recuperación hospitalaria (TINYINT): 1=Ninguna, 2=Moderadora, 3=Copago, 4=Bono, 5=Franquicia, 6=Otra; define participación del paciente internado.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'InPatientRecoveryFeeType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Tipo de Cuota de Recuperacion Hospitalario  1. Ninguna  2. Cuota Moderadora  3. Copago  4. Bono  5. Franquicia  6. Otra', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'InPatientRecoveryFeeType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'InPatientRecoveryFeeType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Puntaje a aplicar (NUMERIC 18,2) cuando intervención quirúrgica supera 450 UVR; se usa si ApplyChangeScore=true.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'NewScore';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el puntaje que se debe tomar cuando la intervencion quirurgica es mayor a 450 UVR, este campo solo se llena si el campo (ApplyChangeScore) esta en true', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'NewScore';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'NewScore';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) si el puntaje cambia para intervenciones quirúrgicas mayores a 450 UVR; habilita NewScore.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'ApplyChangeScore';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si el item cambia de puntaje cuando se encuentre en una intervencion quirurgica con mas de 450 UVR', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'ApplyChangeScore';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'ApplyChangeScore';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Puntaje base (NUMERIC 18,2) para items del manual tarifario ISS, no quirúrgicos, de clase Cirujano-Anestesiólogo-Ayudante; honorarios.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'Score';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el valor del puntaje que se debe solicitar unicamente para los items que pertenescan al manual tarifario iss y que sean no quirurjicos y de Clase (Cirujano - Anesteciologo y Ayudantia)', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'Score';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'Score';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de UVR (INT) asociado al procedimiento quirúrgico; unidad de referencia para liquidación SOAT.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'UVRNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el numero de UVR del procedimiento quirurgico', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'UVRNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'UVRNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) del grupo quirúrgico asociado; solo se habilita si Presentation=Quirúrgico o Paquete; vincula Factor SOAT.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'SurgicalGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del grupo quirurgico  -- Solo se habilita si la presentacion del servicio IPS es Quirurjico o Paquete  -- El grupo se asocia al Factor Soat para sacar los porcentajes de liquidacion', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'SurgicalGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'SurgicalGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Presentación del servicio (TINYINT): 1=No quirúrgico, 2=Quirúrgico, 3=Paquete; determina estructura de cobro y autorizaciones.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'Presentation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Presentacion del producto  1 - No quirurgico  2 - Quirurgico  3 - Paquete', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'Presentation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'Presentation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de servicio (TINYINT): 1=Ninguno, 2=Diagnóstico, 3=Terapéutico, 4=Protección específica, 5=Detección temprana enfermedad general, 6=Detección temprana enfermedad profesional; categoriza naturaleza clínica.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'ServiceType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de servicio   1 - Ninguno  2 - Diagnostico  3  -Terapeutico  4 - Proteccion Especifica  5 - Deteccion temprana enfermedad general  6 - Deteccion temprana enfermedad profesional', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'ServiceType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'ServiceType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) del servicio IPS de material asociado a Derecho de Sala; referencia a IPSService para vincular insumos quirúrgicos.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'AssociatedMaterialIPSServiceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del servicio IPS del material que va estar asociado al servicio de Derecho a sala. Es decir que este campo solo se puede llenar cuando se este creando un servicio IPS de tipo Derecho a sala, este campo no es obligatorio', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'AssociatedMaterialIPSServiceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'AssociatedMaterialIPSServiceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) del concepto de facturación; requerido solo para clases de servicio 2-7 (Cirujano, Anestesiólogo, Ayudante, Derecho Sala, Materiales, Instrumentación).', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'BillingConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del concepto de facturacion    Este campo solo se solicita si la clase de servicio es   Clase de servicio  2 - Cirujano  3 - Anesteciologo  4 - Ayudante  5 - Derecho Sala  6 - Materiales Sutura  7 - Instrumentacion Quirurgica', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'BillingConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'BillingConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clase de servicio (TINYINT): 1=Ninguno, 2=Cirujano, 3=Anestesiólogo, 4=Ayudante, 5=Derecho Sala, 6=Materiales Sutura, 7=Instrumentación Quirúrgica; categoriza rol en facturación.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'ServiceClass';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Clase de servicio  1 - Ninguno  2 - Cirujano  3 - Anesteciologo  4 - Ayudante  5 - Derecho Sala  6 - Materiales Sutura  7 - Instrumentacion Quirurgica', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'ServiceClass';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'ServiceClass';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Manual tarifario (TINYINT): 1=ISS 2001, 2=ISS 2004, 3=SOAT, 4=Institucional; especifica fuente normativa de valores.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'ServiceManual';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de manual  
1 - ISS 2001 
2 - ISS 2004  
3 - SOAT
4 - Institucional', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'ServiceManual';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'ServiceManual';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o descripción del servicio IPS (VARCHAR 300); identificación textual legible para búsqueda, receta, procedimiento.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del servicio IPS', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código alfanumérico del servicio IPS (VARCHAR 20); identificador único funcional para facturación, RIPS, autorizaciones.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del servicio IPS', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único numérico (INT, PK) del servicio IPS en base de datos; clave primaria de la tabla.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del servicio IPS', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo maestro de servicios de salud propios de la IPS: procedimientos, exámenes, cirugías y demás prestaciones que la institución ofrece, con sus tarifas, restricciones de edad y género, clasificación CUPS, nivel de complejidad y parámetros de facturación y autorización.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSService';
