CREATE TABLE [Cost].[CostSecuence] (
    [Id]         INT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdForm]     VARCHAR (5) NOT NULL,
    [IsManual]   BIT         CONSTRAINT [DF_CostInteropCostSecuence_IsManual] DEFAULT ((1)) NOT NULL,
    [Scope]      VARCHAR (2) CONSTRAINT [DF_CostInteropCostSecuence_Scope] DEFAULT ('O') NOT NULL,
    [Sequential] BIT         CONSTRAINT [DF_CostInteropCostSecuence_Sequential] DEFAULT ((0)) NOT NULL,
    [Rate]       TINYINT     CONSTRAINT [DF_CostInteropCostSecuence_Rate] DEFAULT ((3)) NOT NULL,
    CONSTRAINT [PK_CostInteropCostSecuence] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de secuencias a generar por solicitud (TINYINT 1-255) cuando la secuencia NO es continua, rango de incremento por petición', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostSecuence', @level2type = N'COLUMN', @level2name = N'Rate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tasa de secuencias a generar por cada petición cuando la secuencia NO es continua', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostSecuence', @level2type = N'COLUMN', @level2name = N'Rate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostSecuence', @level2type = N'COLUMN', @level2name = N'Rate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) que determina si la secuencia es continua/consecutiva (1) o puede tener saltos/espacios (0)', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostSecuence', @level2type = N'COLUMN', @level2name = N'Sequential';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor que indica si la secuencia es continua y se garantiza un consecutivo', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostSecuence', @level2type = N'COLUMN', @level2name = N'Sequential';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostSecuence', @level2type = N'COLUMN', @level2name = N'Sequential';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ámbito o alcance de la secuencia (VARCHAR 2): O=Organización (nivel global), OU=Unidad Operativa (nivel centro de atención)', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostSecuence', @level2type = N'COLUMN', @level2name = N'Scope';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ambito de la secuencia. O-Organización, OU-Unidad Operativa', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostSecuence', @level2type = N'COLUMN', @level2name = N'Scope';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostSecuence', @level2type = N'COLUMN', @level2name = N'Scope';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) que especifica si la secuencia se genera de forma manual (1) o automática (0) en el sistema', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostSecuence', @level2type = N'COLUMN', @level2name = N'IsManual';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Permite saber si es automatica o manual', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostSecuence', @level2type = N'COLUMN', @level2name = N'IsManual';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostSecuence', @level2type = N'COLUMN', @level2name = N'IsManual';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del formulario/frontal (VARCHAR 5) al que se aplica la regla de generación de secuencia, equivalente a módulo de atención, ingreso, factura o procedimiento', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostSecuence', @level2type = N'COLUMN', @level2name = N'IdForm';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del frontal alq ue aplica la secuencia', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostSecuence', @level2type = N'COLUMN', @level2name = N'IdForm';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostSecuence', @level2type = N'COLUMN', @level2name = N'IdForm';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) del registro de configuración de secuencia de costos', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostSecuence', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostSecuence', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostSecuence', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Configuración de secuencias y reglas de costeo por formulario: define si el costo se ingresa manualmente o se calcula automáticamente, el alcance de aplicación y la tasa de referencia para cada tipo de formulario de costos.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostSecuence';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostSecuence';
