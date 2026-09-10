CREATE TABLE [MixingStation].[MixingStationSequence] (
    [Id]         INT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdForm]     VARCHAR (5) NOT NULL,
    [IsManual]   BIT         CONSTRAINT [DF_MixingStationSequence_IdManual] DEFAULT ((1)) NOT NULL,
    [Scope]      VARCHAR (2) CONSTRAINT [DF_SequenseMixingStationC_Scope] DEFAULT ('O') NOT NULL,
    [Sequential] BIT         CONSTRAINT [DF_SequenseMixingStationC_Sequential] DEFAULT ((0)) NOT NULL,
    [Rate]       TINYINT     CONSTRAINT [DF_SequenseMixingStationC_Rate] DEFAULT ((3)) NOT NULL,
    CONSTRAINT [PK_SequenseMixingStationC] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_MixingStationSequence_IdForm]
    ON [MixingStation].[MixingStationSequence]([IdForm] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tasa de secuencias a generar por cada petición cuando la secuencia NO es continua. Cantidad de registros consecutivos asignados. Tipo: TINYINT, Defecto: 3.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MixingStationSequence', @level2type = N'COLUMN', @level2name = N'Rate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tasa de secuencias a generar por cada petición cuando la secuencia NO es continua', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MixingStationSequence', @level2type = N'COLUMN', @level2name = N'Rate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MixingStationSequence', @level2type = N'COLUMN', @level2name = N'Rate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano que señala si la secuencia es continua y garantiza numeración consecutiva sin saltos. Tipo: BIT, Defecto: 0 (no secuencial).', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MixingStationSequence', @level2type = N'COLUMN', @level2name = N'Sequential';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor que indica si la secuencia es continua y se garantiza un consecutivo', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MixingStationSequence', @level2type = N'COLUMN', @level2name = N'Sequential';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MixingStationSequence', @level2type = N'COLUMN', @level2name = N'Sequential';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ámbito o alcance de la secuencia: O=Organización (empresa), OU=Unidad Operativa (sucursal/centro). Tipo: VARCHAR(2), Defecto: ''''O''''.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MixingStationSequence', @level2type = N'COLUMN', @level2name = N'Scope';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ambito de la secuencia. O-Organización, OU-Unidad Operativa', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MixingStationSequence', @level2type = N'COLUMN', @level2name = N'Scope';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MixingStationSequence', @level2type = N'COLUMN', @level2name = N'Scope';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador que distingue si la generación de secuencias es manual (1) o automática (0). Tipo: BIT, Defecto: 1 (manual).', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MixingStationSequence', @level2type = N'COLUMN', @level2name = N'IsManual';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Permite saber si es automatica o manual', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MixingStationSequence', @level2type = N'COLUMN', @level2name = N'IsManual';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MixingStationSequence', @level2type = N'COLUMN', @level2name = N'IsManual';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del formulario o interfaz de usuario (frontend) al que aplica esta configuración de secuencia. Tipo: VARCHAR(5). Clave foránea lógica.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MixingStationSequence', @level2type = N'COLUMN', @level2name = N'IdForm';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del frontal alq ue aplica la secuencia', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MixingStationSequence', @level2type = N'COLUMN', @level2name = N'IdForm';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MixingStationSequence', @level2type = N'COLUMN', @level2name = N'IdForm';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (clave primaria) del registro de configuración de secuencia en Mixing Station. Tipo: INT IDENTITY, autonumérico.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MixingStationSequence', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MixingStationSequence', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MixingStationSequence', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Configuración de la secuencia de preparación en la estación de mezcla (farmacia/nutrición parenteral). Define si cada formulario de mezcla se procesa de forma manual o automática, el alcance de aplicación, el orden secuencial y la velocidad o tasa de elaboración.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MixingStationSequence';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MixingStationSequence';
