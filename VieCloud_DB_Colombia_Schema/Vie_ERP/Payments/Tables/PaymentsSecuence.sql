CREATE TABLE [Payments].[PaymentsSecuence] (
    [Id]         INT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdForm]     VARCHAR (5) NOT NULL,
    [IsManual]   BIT         CONSTRAINT [DF_PaymentsSecuence_IdManual] DEFAULT ((1)) NOT NULL,
    [Scope]      VARCHAR (2) CONSTRAINT [DF_SequensePaymentsC_Scope] DEFAULT ('O') NOT NULL,
    [Sequential] BIT         CONSTRAINT [DF_SequensePaymentsC_Sequential] DEFAULT ((0)) NOT NULL,
    [Rate]       TINYINT     CONSTRAINT [DF_SequensePaymentsC_Rate] DEFAULT ((3)) NOT NULL,
    CONSTRAINT [PK_SequensePaymentsC] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tasa/cantidad de secuencias a generar por petición cuando la secuencia NO es continua (TINYINT); por defecto 3 registros por solicitud', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsSecuence', @level2type = N'COLUMN', @level2name = N'Rate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tasa de secuencias a generar por cada petición cuando la secuencia NO es continua', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsSecuence', @level2type = N'COLUMN', @level2name = N'Rate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsSecuence', @level2type = N'COLUMN', @level2name = N'Rate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) que especifica si la secuencia es continua y garantiza consecutivos sin saltos (1) o permite interrupciones (0)', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsSecuence', @level2type = N'COLUMN', @level2name = N'Sequential';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor que indica si la secuencia es continua y se garantiza un consecutivo', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsSecuence', @level2type = N'COLUMN', @level2name = N'Sequential';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsSecuence', @level2type = N'COLUMN', @level2name = N'Sequential';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ámbito o alcance de la secuencia (VARCHAR 2): O=Organización completa, OU=Unidad Operativa; controla dónde aplica el consecutivo', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsSecuence', @level2type = N'COLUMN', @level2name = N'Scope';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ambito de la secuencia. O-Organización, OU-Unidad Operativa', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsSecuence', @level2type = N'COLUMN', @level2name = N'Scope';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsSecuence', @level2type = N'COLUMN', @level2name = N'Scope';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) que determina si la generación de secuencias es automática (0) o manual (1); por defecto manual', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsSecuence', @level2type = N'COLUMN', @level2name = N'IsManual';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Permite saber si es automatica o manual', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsSecuence', @level2type = N'COLUMN', @level2name = N'IsManual';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsSecuence', @level2type = N'COLUMN', @level2name = N'IsManual';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del formulario/frontal (VARCHAR 5) al que se aplica la secuencia de pagos; vinculado al módulo de facturación o RIPS', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsSecuence', @level2type = N'COLUMN', @level2name = N'IdForm';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del frontal alq ue aplica la secuencia', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsSecuence', @level2type = N'COLUMN', @level2name = N'IdForm';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsSecuence', @level2type = N'COLUMN', @level2name = N'IdForm';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) del registro de configuración de secuencia de pagos', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsSecuence', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsSecuence', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsSecuence', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Configuración de secuencias y numeración para los formularios de pagos. Define si cada tipo de formulario usa numeración manual o automática, el alcance de la secuencia y la tasa de incremento aplicada.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsSecuence';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsSecuence';
