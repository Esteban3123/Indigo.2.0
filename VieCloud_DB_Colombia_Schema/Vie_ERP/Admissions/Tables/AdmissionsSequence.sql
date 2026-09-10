CREATE TABLE [Admissions].[AdmissionsSequence] (
    [Id]         INT         IDENTITY (1, 1) NOT NULL,
    [IdForm]     VARCHAR (5) NOT NULL,
    [IsManual]   BIT         CONSTRAINT [DF_AdmissionsSequence_IdManual] DEFAULT ((1)) NOT NULL,
    [Scope]      VARCHAR (2) CONSTRAINT [DF_AdmissionsSequence_Scope] DEFAULT ('O') NOT NULL,
    [Sequential] BIT         CONSTRAINT [DF_AdmissionsSequence_Sequential] DEFAULT ((0)) NOT NULL,
    [Rate]       TINYINT     CONSTRAINT [DF_AdmissionsSequence_Rate] DEFAULT ((3)) NOT NULL,
    CONSTRAINT [PK_AdmissionsSequence__Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tasa o cantidad de secuencias a generar por cada petición cuando la secuencia NO es continua/consecutiva. Tipo: TINYINT, rango 0-255, default 3.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'AdmissionsSequence', @level2type = N'COLUMN', @level2name = N'Rate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tasa de secuencias a generar por cada petición cuando la secuencia NO es continua', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'AdmissionsSequence', @level2type = N'COLUMN', @level2name = N'Rate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'AdmissionsSequence', @level2type = N'COLUMN', @level2name = N'Rate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) que determina si la secuencia es continua y garantiza un consecutivo sin saltos. Valor 1=secuencia continua, 0=no continua. Default 0.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'AdmissionsSequence', @level2type = N'COLUMN', @level2name = N'Sequential';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor que indica si la secuencia es continua y se garantiza un consecutivo', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'AdmissionsSequence', @level2type = N'COLUMN', @level2name = N'Sequential';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'AdmissionsSequence', @level2type = N'COLUMN', @level2name = N'Sequential';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ámbito o nivel de aplicación de la secuencia: O=Organización (nivel global), OU=Unidad Operativa (nivel descentralizado). Tipo VARCHAR(2), default ''''O''''.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'AdmissionsSequence', @level2type = N'COLUMN', @level2name = N'Scope';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ambito de la secuencia. O-Organización, OU-Unidad Operativa', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'AdmissionsSequence', @level2type = N'COLUMN', @level2name = N'Scope';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'AdmissionsSequence', @level2type = N'COLUMN', @level2name = N'Scope';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) que identifica si la generación de secuencias es manual (1) o automática (0). Controla modo operativo. Default 1.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'AdmissionsSequence', @level2type = N'COLUMN', @level2name = N'IsManual';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Permite saber si es automatica o manual', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'AdmissionsSequence', @level2type = N'COLUMN', @level2name = N'IsManual';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'AdmissionsSequence', @level2type = N'COLUMN', @level2name = N'IsManual';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del formulario/frontal (interfaz de usuario) al que aplica esta configuración de secuencia. Tipo VARCHAR(5), clave para asociar reglas a módulos específicos de admisión/atención.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'AdmissionsSequence', @level2type = N'COLUMN', @level2name = N'IdForm';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del frontal alq ue aplica la secuencia', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'AdmissionsSequence', @level2type = N'COLUMN', @level2name = N'IdForm';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'AdmissionsSequence', @level2type = N'COLUMN', @level2name = N'IdForm';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro (clave primaria). IDENTITY(1,1), entero secuencial que indexa cada configuración de secuencia en el sistema.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'AdmissionsSequence', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'AdmissionsSequence', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'AdmissionsSequence', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Configuración de secuencias y numeración para los formularios de admisión. Define si la numeración es manual o automática, el alcance y la tasa de generación de consecutivos en los procesos de admisión e ingreso de pacientes.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'AdmissionsSequence';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'AdmissionsSequence';
