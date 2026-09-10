CREATE TABLE [dbo].[CHVARIABLES] (
    [ID]            INT            IDENTITY (1, 1) NOT NULL,
    [IDGRUPO]       INT            NOT NULL,
    [VARIABLE]      VARCHAR (150)  NOT NULL,
    [TIPO]          TINYINT        NOT NULL,
    [GENERO]        TINYINT        NOT NULL,
    [EDADMIN]       INT            NOT NULL,
    [EDADMAX]       INT            NOT NULL,
    [MANEJADECIMAL] BIT            NULL,
    [CODUSUCRE]     CHAR (20)      NOT NULL,
    [FECHACREA]     DATETIME       NOT NULL,
    [CODUSUMOD]     CHAR (20)      NULL,
    [FECHAMOD]      DATETIME       NULL,
    [TIPOESCALA]    TINYINT        NULL,
    [TOOLTIP]       BIT            CONSTRAINT [DF__CHVARIABL__TOOLT__35F862EC] DEFAULT ((0)) NOT NULL,
    [TOOLTIPTXT]    VARCHAR (1000) NULL,
    CONSTRAINT [PK_CHVARIABLES] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_CHVARIABLES_CHGRUPO] FOREIGN KEY ([IDGRUPO]) REFERENCES [dbo].[CHGRUPO] ([ID])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Texto descriptivo VARCHAR(1000) para tooltip de ayuda contextual en la variable de lista de chequeo, información adicional que se muestra al usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVARIABLES', @level2type = N'COLUMN', @level2name = N'TOOLTIPTXT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo de texto para tooltip', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVARIABLES', @level2type = N'COLUMN', @level2name = N'TOOLTIPTXT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVARIABLES', @level2type = N'COLUMN', @level2name = N'TOOLTIPTXT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT (0/1) que determina si la variable de lista de chequeo posee tooltip de ayuda habilitado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVARIABLES', @level2type = N'COLUMN', @level2name = N'TOOLTIP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Verificar si tiene tooltip', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVARIABLES', @level2type = N'COLUMN', @level2name = N'TOOLTIP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVARIABLES', @level2type = N'COLUMN', @level2name = N'TOOLTIP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de escala TINYINT para clasificación en lista de chequeo (escalas y clasificaciones según FrmHCVariablesCH)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVARIABLES', @level2type = N'COLUMN', @level2name = N'TIPOESCALA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Escala de la lista de chequeo (mirar el FrmHCVariablesCH escala y clasificación)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVARIABLES', @level2type = N'COLUMN', @level2name = N'TIPOESCALA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVARIABLES', @level2type = N'COLUMN', @level2name = N'TIPOESCALA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora DATETIME del último registro de modificación de la variable en lista de chequeo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVARIABLES', @level2type = N'COLUMN', @level2name = N'FECHAMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVARIABLES', @level2type = N'COLUMN', @level2name = N'FECHAMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVARIABLES', @level2type = N'COLUMN', @level2name = N'FECHAMOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del usuario CHAR(20) que realizó la última modificación de la variable, identificador de auditoría', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVARIABLES', @level2type = N'COLUMN', @level2name = N'CODUSUMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo usuario modificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVARIABLES', @level2type = N'COLUMN', @level2name = N'CODUSUMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVARIABLES', @level2type = N'COLUMN', @level2name = N'CODUSUMOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora DATETIME de creación inicial de la variable en la lista de chequeo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVARIABLES', @level2type = N'COLUMN', @level2name = N'FECHACREA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Creacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVARIABLES', @level2type = N'COLUMN', @level2name = N'FECHACREA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVARIABLES', @level2type = N'COLUMN', @level2name = N'FECHACREA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del usuario CHAR(20) que creó la variable, identificador de auditoría y responsable original', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVARIABLES', @level2type = N'COLUMN', @level2name = N'CODUSUCRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo usuario creación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVARIABLES', @level2type = N'COLUMN', @level2name = N'CODUSUCRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVARIABLES', @level2type = N'COLUMN', @level2name = N'CODUSUCRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT que determina si la variable numérica soporta o maneja valores decimales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVARIABLES', @level2type = N'COLUMN', @level2name = N'MANEJADECIMAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Determina si maneja decimal o no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVARIABLES', @level2type = N'COLUMN', @level2name = N'MANEJADECIMAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVARIABLES', @level2type = N'COLUMN', @level2name = N'MANEJADECIMAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad máxima INT en años para aplicabilidad de la variable en lista de chequeo según rango etario del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVARIABLES', @level2type = N'COLUMN', @level2name = N'EDADMAX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Edad Máxima en años a la que aplica la variable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVARIABLES', @level2type = N'COLUMN', @level2name = N'EDADMAX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVARIABLES', @level2type = N'COLUMN', @level2name = N'EDADMAX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad mínima INT en años para aplicabilidad de la variable en lista de chequeo según rango etario del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVARIABLES', @level2type = N'COLUMN', @level2name = N'EDADMIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Edad Minima en años a la que aplica la variable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVARIABLES', @level2type = N'COLUMN', @level2name = N'EDADMIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVARIABLES', @level2type = N'COLUMN', @level2name = N'EDADMIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Género TINYINT al que aplica la variable: 1=Masculino, 2=Femenino, 3=Ambos géneros, filtro demográfico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVARIABLES', @level2type = N'COLUMN', @level2name = N'GENERO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Genero a la que aplica la variable  1  - Masculino  2 - Femenino  3  - los dos generos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVARIABLES', @level2type = N'COLUMN', @level2name = N'GENERO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVARIABLES', @level2type = N'COLUMN', @level2name = N'GENERO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de dato TINYINT de la variable (1=Boolean/Check, 2=String/Memo, 3=Numérico, 4=Lista/GridLookup, 5-10=Formatos Fecha)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVARIABLES', @level2type = N'COLUMN', @level2name = N'TIPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de variable  1 - BOOLEAN - CONTROL CHECK  2 - STRING - CONTROL MEMOEDIT  3 - NUMERICO - NUMERIC  4- LISTA - CONTROL GRIDLOOKUPEDIT  5- FECHA  (dd/MM/yyyy)  6- FECHA  (MM/dd/yyyy)  7- Escalas y Clasificaciones  8- FECHA  (yyy/MM/dd)  9- FECHA (dd/MMM/yyyy)  10-FECHA (dd \de MMMM \de yyyy)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVARIABLES', @level2type = N'COLUMN', @level2name = N'TIPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVARIABLES', @level2type = N'COLUMN', @level2name = N'TIPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre VARCHAR(150) de la variable de lista de chequeo, identificador textual único de parámetro clínico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVARIABLES', @level2type = N'COLUMN', @level2name = N'VARIABLE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la Variable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVARIABLES', @level2type = N'COLUMN', @level2name = N'VARIABLE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVARIABLES', @level2type = N'COLUMN', @level2name = N'VARIABLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador INT de grupo de parámetros (FK→CHGRUPO), agrupa variables relacionadas en lista de chequeo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVARIABLES', @level2type = N'COLUMN', @level2name = N'IDGRUPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'id del grupo de parametros otros', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVARIABLES', @level2type = N'COLUMN', @level2name = N'IDGRUPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVARIABLES', @level2type = N'COLUMN', @level2name = N'IDGRUPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único INT IDENTITY, clave primaria autonumérica de variable en lista de chequeo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVARIABLES', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVARIABLES', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVARIABLES', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Variables clínicas o de historia clínica definidas para captura de datos del paciente, organizadas por grupos, con restricciones de género, rango de edad y tipo de dato. Permiten configurar formularios o escalas de evaluación clínica en el sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVARIABLES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVARIABLES';
