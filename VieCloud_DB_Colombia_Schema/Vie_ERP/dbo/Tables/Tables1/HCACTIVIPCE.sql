CREATE TABLE [dbo].[HCACTIVIPCE] (
    [CODACTPCE] INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [NOMACTPCE] VARCHAR (150) NOT NULL,
    [ESTADO]    INT           NULL,
    [EMINANIOS] INT           NOT NULL,
    [EMINMESES] INT           NOT NULL,
    [EMINDIAS]  INT           NOT NULL,
    [EMAXANIOS] INT           NOT NULL,
    [EMAXMESES] INT           NOT NULL,
    [EMAXDIAS]  INT           NOT NULL,
    [IPSEXO]    CHAR (1)      NOT NULL,
    [TIPOACT]   INT           NULL,
    [APLICAROL] INT           NULL,
    CONSTRAINT [PK_HCACTIVIPCE] PRIMARY KEY CLUSTERED ([CODACTPCE] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Rol profesional aplicable a la actividad: 1=Enfermería, 2=Médico. Determina qué tipo de profesional de salud puede ejecutar esta actividad clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACTIVIPCE', @level2type = N'COLUMN', @level2name = N'APLICAROL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Aplica al rol 1->Enfermeria 2->Medico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACTIVIPCE', @level2type = N'COLUMN', @level2name = N'APLICAROL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACTIVIPCE', @level2type = N'COLUMN', @level2name = N'APLICAROL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de actividad clínica: 1=Continua (repetible en múltiples atenciones), 2=Única vez (única ejecución por proceso). Clasificación de frecuencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACTIVIPCE', @level2type = N'COLUMN', @level2name = N'TIPOACT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de actividad 1->Continua 2->Unica vez', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACTIVIPCE', @level2type = N'COLUMN', @level2name = N'TIPOACT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACTIVIPCE', @level2type = N'COLUMN', @level2name = N'TIPOACT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sexo del paciente aplicable: H=Hombre, M=Mujer, I=Intersexual. Restricción de género para la actividad en cuidado de salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACTIVIPCE', @level2type = N'COLUMN', @level2name = N'IPSEXO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Sexo del paciente Hombre (H), Mujer (M), Intersexual (I) ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACTIVIPCE', @level2type = N'COLUMN', @level2name = N'IPSEXO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACTIVIPCE', @level2type = N'COLUMN', @level2name = N'IPSEXO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad máxima permitida expresada en días. Límite superior de edad para ejecutar o registrar la actividad clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACTIVIPCE', @level2type = N'COLUMN', @level2name = N'EMAXDIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Edad maxima en dias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACTIVIPCE', @level2type = N'COLUMN', @level2name = N'EMAXDIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACTIVIPCE', @level2type = N'COLUMN', @level2name = N'EMAXDIAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad máxima permitida expresada en meses. Componente de edad superior para validación de rango etario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACTIVIPCE', @level2type = N'COLUMN', @level2name = N'EMAXMESES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Edad maxima en meses', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACTIVIPCE', @level2type = N'COLUMN', @level2name = N'EMAXMESES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACTIVIPCE', @level2type = N'COLUMN', @level2name = N'EMAXMESES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad máxima permitida expresada en años. Tope de años para aplicabilidad de la actividad según criterios de protocolo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACTIVIPCE', @level2type = N'COLUMN', @level2name = N'EMAXANIOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Edad maxima en años', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACTIVIPCE', @level2type = N'COLUMN', @level2name = N'EMAXANIOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACTIVIPCE', @level2type = N'COLUMN', @level2name = N'EMAXANIOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad mínima permitida expresada en días. Límite inferior de edad para ejecutar o registrar la actividad clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACTIVIPCE', @level2type = N'COLUMN', @level2name = N'EMINDIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Edad minima en dias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACTIVIPCE', @level2type = N'COLUMN', @level2name = N'EMINDIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACTIVIPCE', @level2type = N'COLUMN', @level2name = N'EMINDIAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad mínima permitida expresada en meses. Componente de edad inferior para validación de rango etario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACTIVIPCE', @level2type = N'COLUMN', @level2name = N'EMINMESES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Edad minima en meses', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACTIVIPCE', @level2type = N'COLUMN', @level2name = N'EMINMESES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACTIVIPCE', @level2type = N'COLUMN', @level2name = N'EMINMESES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad mínima permitida expresada en años. Piso de años para aplicabilidad de la actividad según protocolo clínico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACTIVIPCE', @level2type = N'COLUMN', @level2name = N'EMINANIOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Edad minima en años', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACTIVIPCE', @level2type = N'COLUMN', @level2name = N'EMINANIOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACTIVIPCE', @level2type = N'COLUMN', @level2name = N'EMINANIOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de vigencia: 1=Activo (actividad disponible en procesos clínicos), 0=Inactivo (actividad deshabilitada). Bandera booleana INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACTIVIPCE', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'True = Activo   False = Inactivo ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACTIVIPCE', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACTIVIPCE', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre descriptivo de la actividad clínica o procedimiento en la unidad funcional de paciente-centrado-empresa (PCE). VARCHAR(150).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACTIVIPCE', @level2type = N'COLUMN', @level2name = N'NOMACTPCE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la actividad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACTIVIPCE', @level2type = N'COLUMN', @level2name = N'NOMACTPCE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACTIVIPCE', @level2type = N'COLUMN', @level2name = N'NOMACTPCE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único identificador de la actividad clínica (PK). INT IDENTITY. Llave primaria de HCACTIVIPCE.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACTIVIPCE', @level2type = N'COLUMN', @level2name = N'CODACTPCE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la actividad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACTIVIPCE', @level2type = N'COLUMN', @level2name = N'CODACTPCE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACTIVIPCE', @level2type = N'COLUMN', @level2name = N'CODACTPCE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de actividades o intervenciones de prevención y control para el programa de detección temprana y protección específica (PyP/PCE), con los rangos de edad y sexo en los que aplica cada actividad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACTIVIPCE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACTIVIPCE';
