CREATE TABLE [MedicalFees].[MedicalFeesContractException] (
    [Id]                     INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [MedicalFeesConctractId] INT            NOT NULL,
    [ExceptionType]          TINYINT        NOT NULL,
    [IPSServiceId]           INT            NULL,
    [CUPSEntityId]           INT            NULL,
    [CUPSSubgroupId]         INT            NULL,
    [CUPSGroupId]            INT            NULL,
    [CareGroupId]            INT            NULL,
    [ContractEntityId]       INT            NULL,
    [RateManualType]         TINYINT        NULL,
    [RateType]               TINYINT        NOT NULL,
    [PercentageRate]         NUMERIC (5, 2) NULL,
    [RateManualId]           INT            NULL,
    [RateVariation]          NUMERIC (5, 2) NULL,
    [AmountPayable]          NUMERIC (18)   NULL,
    CONSTRAINT [PK_MedicalFeesContractGeneralException] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MedicalFeesContractException_CupsGroup] FOREIGN KEY ([CUPSGroupId]) REFERENCES [Contract].[CupsGroup] ([Id]),
    CONSTRAINT [FK_MedicalFeesContractException_CupsSubgroup] FOREIGN KEY ([CUPSSubgroupId]) REFERENCES [Contract].[CupsSubgroup] ([Id]),
    CONSTRAINT [FK_MedicalFeesContractException_IPSService] FOREIGN KEY ([IPSServiceId]) REFERENCES [Contract].[IPSService] ([Id]),
    CONSTRAINT [FK_MedicalFeesContractException_MedicalFeesContract] FOREIGN KEY ([MedicalFeesConctractId]) REFERENCES [MedicalFees].[MedicalFeesContract] ([Id]),
    CONSTRAINT [FK_MedicalFeesContractGeneralException_CareGroup] FOREIGN KEY ([CareGroupId]) REFERENCES [Contract].[CareGroup] ([Id]),
    CONSTRAINT [FK_MedicalFeesContractGeneralException_ContractEntity] FOREIGN KEY ([ContractEntityId]) REFERENCES [Contract].[ContractEntity] ([Id]),
    CONSTRAINT [FK_MedicalFeesContractGeneralException_CUPSEntity] FOREIGN KEY ([CUPSEntityId]) REFERENCES [Contract].[CUPSEntity] ([Id])
);




GO



GO



GO



GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor fijo en pesos a liquidar cuando RateType=3 (tarifa por valor fijo). Tipo: NUMERIC(18). Usado en cálculo de causación y facturación de servicios.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesContractException', @level2type = N'COLUMN', @level2name = N'AmountPayable';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica el valor fijo a liquidar si el tipo es 3', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesContractException', @level2type = N'COLUMN', @level2name = N'AmountPayable';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesContractException', @level2type = N'COLUMN', @level2name = N'AmountPayable';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de variación o ajuste aplicado a la tarifa base (incremento/descuento). Tipo: NUMERIC(5,2). Expresado como porcentaje para modulación de honorarios.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesContractException', @level2type = N'COLUMN', @level2name = N'RateVariation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje de la variacion de la tarifa', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesContractException', @level2type = N'COLUMN', @level2name = N'RateVariation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesContractException', @level2type = N'COLUMN', @level2name = N'RateVariation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del manual tarifario (ISS 2001, ISS 2004, SOAT u otro) usado para cálculo de causación cuando RateType=2. FK a tabla de manuales tarifarios.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesContractException', @level2type = N'COLUMN', @level2name = N'RateManualId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica el manual de tarifas con el que se va a realizar el calculo de la causacion si el tipo es 2', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesContractException', @level2type = N'COLUMN', @level2name = N'RateManualId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesContractException', @level2type = N'COLUMN', @level2name = N'RateManualId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje del valor cobrado a liquidar cuando RateType=1. Tipo: NUMERIC(5,2). Usado para cálculo proporcional de honorarios y contraprestaciones.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesContractException', @level2type = N'COLUMN', @level2name = N'PercentageRate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica el porcentaje a liquidar si el tipo es 1', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesContractException', @level2type = N'COLUMN', @level2name = N'PercentageRate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesContractException', @level2type = N'COLUMN', @level2name = N'PercentageRate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificador del método de tarificación: 1=Porcentaje del valor cobrado, 2=Manual tarifario (ISS, SOAT), 3=Valor fijo. Define lógica de causación de honorarios.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesContractException', @level2type = N'COLUMN', @level2name = N'RateType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Tarifa  1 - Por % del valor cobrado  2 - Por Manual Tarifario  3 - Por Valor Fijo  ', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesContractException', @level2type = N'COLUMN', @level2name = N'RateType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesContractException', @level2type = N'COLUMN', @level2name = N'RateType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificador del manual tarifario aplicable: 1=ISS 2001, 2=ISS 2004, 3=SOAT u otro. Determina tabla de valores para cálculo cuando RateType=2.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesContractException', @level2type = N'COLUMN', @level2name = N'RateManualType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo del manual tarifario  1 - ISS 2001  2 - ISS 2004  3 - SOAT', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesContractException', @level2type = N'COLUMN', @level2name = N'RateManualType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesContractException', @level2type = N'COLUMN', @level2name = N'RateManualType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la entidad contratante (IPS, aseguradora, centro de atención). FK a Contract.ContractEntity. Scope de la excepción a nivel entidad.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesContractException', @level2type = N'COLUMN', @level2name = N'ContractEntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la Entidad', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesContractException', @level2type = N'COLUMN', @level2name = N'ContractEntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesContractException', @level2type = N'COLUMN', @level2name = N'ContractEntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del grupo de atención (especialidad, servicio clínico, unidad funcional). FK a Contract.CareGroup. Scope de la excepción a nivel grupal.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesContractException', @level2type = N'COLUMN', @level2name = N'CareGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Grupo de Atencion', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesContractException', @level2type = N'COLUMN', @level2name = N'CareGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesContractException', @level2type = N'COLUMN', @level2name = N'CareGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del grupo CUPS (conjunto de códigos de procedimientos/servicios). FK a Contract.CupsGroup. Scope de la excepción a nivel grupal CUPS.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesContractException', @level2type = N'COLUMN', @level2name = N'CUPSGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del grupo de CUPS', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesContractException', @level2type = N'COLUMN', @level2name = N'CUPSGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesContractException', @level2type = N'COLUMN', @level2name = N'CUPSGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del subgrupo CUPS (clasificación intermedia de procedimientos). FK a Contract.CupsSubgroup. Scope de la excepción a nivel sub-grupal.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesContractException', @level2type = N'COLUMN', @level2name = N'CUPSSubgroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del subgrupo del CUPS', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesContractException', @level2type = N'COLUMN', @level2name = N'CUPSSubgroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesContractException', @level2type = N'COLUMN', @level2name = N'CUPSSubgroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la entidad CUPS (código específico de procedimiento, servicio, diagnóstico o insumo). FK a Contract.CupsEntity. Scope más granular.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesContractException', @level2type = N'COLUMN', @level2name = N'CUPSEntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del CUPS', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesContractException', @level2type = N'COLUMN', @level2name = N'CUPSEntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesContractException', @level2type = N'COLUMN', @level2name = N'CUPSEntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del servicio específico ofrecido por la IPS (consulta, internación, urgencia, laboratorio, imágenes). FK a Contract.IPSService.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesContractException', @level2type = N'COLUMN', @level2name = N'IPSServiceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Id del servicio IPS', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesContractException', @level2type = N'COLUMN', @level2name = N'IPSServiceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesContractException', @level2type = N'COLUMN', @level2name = N'IPSServiceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificador del alcance de la regla de excepción: 1=Servicio IPS, 2=Código CUPS, 3=Subgrupo CUPS, 4=Grupo CUPS, 5=Grupo de atención, 6=Entidad, 7=Manual tarifario, 8=General. Determina qué FK es relevante.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesContractException', @level2type = N'COLUMN', @level2name = N'ExceptionType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Regla  1 - Servicio IPS  2 - CUPS  3 - SubGrupo CUPS  4 - Grupo CUPS  5 - Grupo de Atencion  6 - Entidad  7 - Tipo de Manual Tarifario  8 - General', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesContractException', @level2type = N'COLUMN', @level2name = N'ExceptionType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesContractException', @level2type = N'COLUMN', @level2name = N'ExceptionType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del contrato de tarifas y honorarios generales. FK a MedicalFees.MedicalFeesContract. Vincula la excepción al acuerdo de tarificación padre.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesContractException', @level2type = N'COLUMN', @level2name = N'MedicalFeesConctractId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla de tarifas para honorarios generales', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesContractException', @level2type = N'COLUMN', @level2name = N'MedicalFeesConctractId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesContractException', @level2type = N'COLUMN', @level2name = N'MedicalFeesConctractId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave primaria autoincremental de la excepción contractual. Tipo: INT IDENTITY. Identifica unívocamente cada regla de tarificación especial en el contrato.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesContractException', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesContractException', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesContractException', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Excepciones o reglas especiales de tarifas dentro de un contrato médico. Permite definir tarifas particulares por servicio, grupo CUPS, entidad o tipo de atención que sobreescriben la tarifa general del contrato.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesContractException';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesContractException';
