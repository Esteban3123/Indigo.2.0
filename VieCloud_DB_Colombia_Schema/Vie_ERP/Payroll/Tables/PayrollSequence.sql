CREATE TABLE [Payroll].[PayrollSequence] (
    [Id]         INT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdForm]     VARCHAR (5) NOT NULL,
    [IsManual]   BIT         CONSTRAINT [DF_PayrollSequence_IdManual] DEFAULT ((1)) NOT NULL,
    [Scope]      VARCHAR (2) CONSTRAINT [DF_PayrollSequence_Scope] DEFAULT ('O') NOT NULL,
    [Sequential] BIT         CONSTRAINT [DF_PayrollSequence_Sequential] DEFAULT ((0)) NOT NULL,
    [Rate]       TINYINT     CONSTRAINT [DF_PayrollSequence_Rate] DEFAULT ((3)) NOT NULL,
    CONSTRAINT [PK_PayrollSequence] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tasa de secuencias a generar por cada petición cuando la secuencia NO es continua. Tipo: TINYINT. Valor por defecto: 3. Define la cantidad de números consecutivos a reservar en modo no-secuencial.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSequence', @level2type = N'COLUMN', @level2name = N'Rate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tasa de secuencias a generar por cada petición cuando la secuencia NO es continua', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSequence', @level2type = N'COLUMN', @level2name = N'Rate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSequence', @level2type = N'COLUMN', @level2name = N'Rate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) que determina si la secuencia es continua y garantiza numeración consecutiva sin saltos. Valor por defecto: 0 (falso). Aplica a formularios, facturas, recetas, RIPS y documentos de auditoría.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSequence', @level2type = N'COLUMN', @level2name = N'Sequential';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor que indica si la secuencia es continua y se garantiza un consecutivo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSequence', @level2type = N'COLUMN', @level2name = N'Sequential';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSequence', @level2type = N'COLUMN', @level2name = N'Sequential';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ámbito o alcance de la secuencia: O=Organización (global), OU=Unidad Operativa (local). Tipo: VARCHAR(2). Valor por defecto: ''''O''''. Define si el numerador es centralizado o por centro de atención.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSequence', @level2type = N'COLUMN', @level2name = N'Scope';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ambito de la secuencia. O-Organización, OU-Unidad Operativa', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSequence', @level2type = N'COLUMN', @level2name = N'Scope';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSequence', @level2type = N'COLUMN', @level2name = N'Scope';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) que especifica si la secuencia es generada manualmente (1=Sí) o automáticamente (0=No) por el sistema. Valor por defecto: 1. Tipo: BIT.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSequence', @level2type = N'COLUMN', @level2name = N'IsManual';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Permite saber si es automatica o manual', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSequence', @level2type = N'COLUMN', @level2name = N'IsManual';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSequence', @level2type = N'COLUMN', @level2name = N'IsManual';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del formulario o frontal al que se aplica la secuencia. Tipo: VARCHAR(5). Referencia a la aplicación, módulo o pantalla que utiliza esta regla de numeración (ej: facturación, RIPS, recetas, ingresos).', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSequence', @level2type = N'COLUMN', @level2name = N'IdForm';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del frontal alq ue aplica la secuencia', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSequence', @level2type = N'COLUMN', @level2name = N'IdForm';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSequence', @level2type = N'COLUMN', @level2name = N'IdForm';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de configuración de secuencia. Tipo: INT IDENTITY(1,1). Clave primaria clusterizada. No replica.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSequence', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSequence', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSequence', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Configuración de secuencias de nómina: define para cada formulario de nómina si la numeración es manual o automática, el alcance de la secuencia (por ejemplo, organización o sucursal), si es correlativa y la tasa o periodicidad aplicada.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSequence';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSequence';
