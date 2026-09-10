CREATE TABLE [AccountManagement].[AccountManagementSequenceDetail] (
    [Id]                           INT    IDENTITY (1, 1) NOT NULL,
    [IdSequenseAccountManagementC] INT    NOT NULL,
    [IdSequense]                   INT    NOT NULL,
    [IdOperatingUnit]              INT    NULL,
    [Next]                         BIGINT CONSTRAINT [DF_SequenseAccountManagementD_Next] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_SequenseAccountManagementD] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SequenseAccountManagementD_OperatingUnit] FOREIGN KEY ([IdOperatingUnit]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_SequenseAccountManagementD_Sequense] FOREIGN KEY ([IdSequense]) REFERENCES [Common].[Sequense] ([Id]),
    CONSTRAINT [FK_SequenseAccountManagementD_SequenseAccountManagementC] FOREIGN KEY ([IdSequenseAccountManagementC]) REFERENCES [AccountManagement].[AccountManagementSequence] ([Id])
);




GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Siguiente número a generar en la secuencia (BIGINT). Valor usado para asignar números correlativo a documentos como facturas, recetas, ingresos o actos administrativos dentro del rango configurado.', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'AccountManagementSequenceDetail', @level2type = N'COLUMN', @level2name = N'Next';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Siguiente numero a generar con la secuenacia', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'AccountManagementSequenceDetail', @level2type = N'COLUMN', @level2name = N'Next';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'AccountManagementSequenceDetail', @level2type = N'COLUMN', @level2name = N'Next';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la unidad operativa (centro de atención, sucursal, sede) asignada a la secuencia cuando el ámbito es ''''UO-Unidad Operativa''''; nulo si el ámbito es global o diferente. FK a [Common].[OperatingUnit].', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'AccountManagementSequenceDetail', @level2type = N'COLUMN', @level2name = N'IdOperatingUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad operativa asignada a la secuencia. Solo cuando el ambito es UO-Unidad Operativa, de lo contrario el campo es nulo', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'AccountManagementSequenceDetail', @level2type = N'COLUMN', @level2name = N'IdOperatingUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'AccountManagementSequenceDetail', @level2type = N'COLUMN', @level2name = N'IdOperatingUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la secuencia base o patrón de numeración (ej: facturación, RIPS, recetas). FK a [Common].[Sequense].', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'AccountManagementSequenceDetail', @level2type = N'COLUMN', @level2name = N'IdSequense';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la secuencia base', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'AccountManagementSequenceDetail', @level2type = N'COLUMN', @level2name = N'IdSequense';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'AccountManagementSequenceDetail', @level2type = N'COLUMN', @level2name = N'IdSequense';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cabecera o encabezado de configuración de gestión de secuencias a la que pertenece este detalle. FK a [AccountManagement].[AccountManagementSequence].', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'AccountManagementSequenceDetail', @level2type = N'COLUMN', @level2name = N'IdSequenseAccountManagementC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'AccountManagementSequenceDetail', @level2type = N'COLUMN', @level2name = N'IdSequenseAccountManagementC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'AccountManagementSequenceDetail', @level2type = N'COLUMN', @level2name = N'IdSequenseAccountManagementC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) del registro detalle de la secuencia de gestión de cuenta. Clave primaria.', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'AccountManagementSequenceDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'AccountManagementSequenceDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'AccountManagementSequenceDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de las secuencias numéricas utilizadas en la gestión de cuentas, registrando el consecutivo o número siguiente disponible para cada secuencia por unidad operativa y tipo de cuenta.', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'AccountManagementSequenceDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'AccountManagementSequenceDetail';
